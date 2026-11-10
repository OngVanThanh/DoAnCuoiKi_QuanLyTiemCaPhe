# Review & Adversarial Audit Report: Milestone 3 — Relative Paths & Image Normalization

**Date**: 2026-11-10T18:43:00Z  
**Reviewer / Critic**: `reviewer_m3_1`  
**Parent**: `b7a40333-a284-41cc-b74b-3303f0e4cf38` (sub_orch_m3)  
**Milestone**: Milestone 3 (Relative Paths & Image Normalization)  
**Verdict**: **APPROVE**  
**Integrity Assessment**: **CLEAN (No integrity violations, no hardcoded results, no dummy facades)**

---

## 1. Observation

Direct empirical observations made during independent review and verification:

1. **Source Code Audit for `Image.FromFile`**:
   - Command: `Get-ChildItem -Recurse -Filter '*.cs' | Select-String -Pattern 'Image\.FromFile'`
   - Result: Verbatim 0 matches across all `.cs` files in the repository.
2. **Source Code Audit for Hardcoded Local Drive Letters (`C:\`, `D:\`)**:
   - Command: `Get-ChildItem -Recurse -Filter '*.cs' | Select-String -Pattern '[A-Za-z]:\\\\' | Where-Object { $_.Line -notmatch 'http://' -and $_.Line -notmatch 'https://' -and $_.Path -notmatch '\\.agents\\' -and $_.Path -notmatch '\\obj\\' }`
   - Result: Verbatim 0 matches.
   - Command: `Get-ChildItem -Filter '*.config' | Select-String -Pattern '[A-Za-z]:[\\/]' | Where-Object { $_.Line -notmatch 'http://' -and $_.Line -notmatch 'https://' }`
   - Result: Verbatim 0 matches.
3. **`Utilities\UIHelper.cs` (lines 103-241)**:
   - `GetApplicationDirectory()` checks `typeof(UIHelper).Assembly.Location` first, falling back to `Application.StartupPath` and `AppDomain.CurrentDomain.BaseDirectory`.
   - `LoadImageToPictureBox(PictureBox pic, string relativePath)`:
     * Line 134-143: Checks `string.IsNullOrWhiteSpace(relativePath)` and safely disposes `pic.Image`.
     * Lines 145-199: Strips leading slashes (`cleanRelative = trimmed.TrimStart('\\', '/')`) and resolves multi-tier paths across Application directory, `StartupPath`, `BaseDirectory`, `CurrentDirectory`, and fallback index of `"Images"`.
     * Lines 206-215: Reads bytes via `File.ReadAllBytes(fullPath)`, loads into `MemoryStream`, instantiates detached `new Bitmap(temp)`, disposes old image `oldImage?.Dispose()`, and sets `pic.SizeMode = PictureBoxSizeMode.Zoom`.
     * Lines 228-240: Entire block wrapped in `try ... catch`, safely clearing `pic.Image = null` and disposing old image on corrupted, locked, or malformed data.
4. **`UserControl\UCEmployeeAdd.cs` (lines 146-180) & `UCEmployeeEdit.cs` (lines 169-204)**:
   - When picking an avatar, target directory `Path.Combine(baseDir, "Images", "Employees")` is created if missing.
   - File is copied to `destFolder` as `$"emp_{DateTime.Now.Ticks}{extension}"`.
   - Stored path is strictly relative: `Path.Combine("Images", "Employees", fileName)`.
   - Image preview is displayed via `UIHelper.LoadImageToPictureBox(ptbAvatar, _imagePath)`.
5. **`BLL\EmployeeService.cs` (lines 68-102 & 115-168)**:
   - `SaveEmployee(EmployeeDTO dto, out string errorMsg)`: If `dto.Image` is rooted and exists on disk, automatically copies to `Images\Employees\emp_{ticks}{ext}` and updates `dto.Image` to the relative path before persisting.
   - `SaveEmployee(EmployeeDTO dto, string sourceImagePath, out string errorMsg)` and `SaveEmployee(EmployeeDTO dto, string sourceImagePath)` implemented to satisfy `PROJECT.md` interface contract.
6. **`Form\TuanHung\FrmProduct.cs` (lines 68, 83)**:
   - Product image loading and browsing both use `UIHelper.LoadImageToPictureBox(picProductImage, ...)` eliminating previous raw `FileStream` usage.
7. **MSBuild Rebuild Verification**:
   - Command: `& "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" DoAnCuoiKi_QuanLyTiemCaPhe.csproj /t:Rebuild /p:Configuration=Debug`
   - Output: `Build succeeded. 0 Warning(s) 0 Error(s)`.
8. **Test Execution Verification**:
   - Challenger Suite (`tests\Test_M3_Challenger.ps1`): **22 run, 22 passed, 0 failed** (100%).
   - Master E2E Suite (`tests\run_e2e_tests.ps1`): **204 run, 203 passed, 0 failed, 1 known pending** (classic `.sln` planned for M5), pass rate **99.5%**.

---

## 2. Logic Chain

1. **Non-Locking In-Memory Streaming**:
   - Observation 3 shows `File.ReadAllBytes` immediately releases file handles. Instantiating `new Bitmap(temp)` creates an independent in-memory GDI+ raster object.
   - Verified via independent PowerShell test: while a PictureBox holds an image loaded via `UIHelper.LoadImageToPictureBox`, the underlying file on disk was opened for exclusive `[FileAccess]::ReadWrite` with `[FileShare]::None` successfully without `IOException`.
   - Rapid reload stress test (50 consecutive reloads) executed cleanly with zero GDI handle exhaustion or memory leaks because `oldImage?.Dispose()` is called on every iteration.
2. **Absolute vs Relative Path Compliance**:
   - Observations 1 & 2 confirm zero `Image.FromFile` and zero hardcoded drive letters in source code or configuration files.
   - Observations 4 & 5 show user controls and service layer enforce relative path generation (`Images\Employees\emp_{ticks}{ext}`) and relative persistence in the database.
   - Adversarial testing confirmed that passing an absolute path to `EmployeeService.SaveEmployee` automatically transforms the path to a relative path and copies the image into the project assets folder.
3. **Resilience to Malformed, Traversal, and Corrupted Inputs**:
   - Adversarial testing tested:
     * Leading slashes (`/Images/...`): Resolved successfully (True).
     * Forward slashes (`Images/Products/...`): Resolved successfully (True).
     * Legacy machine paths (`Z:\OtherMachine\...\Images\Products\...`): Resolved successfully via relative fallback (True).
     * Locked files on disk: Handled gracefully without crash, `pic.Image` set to null (True).
     * Path traversal (`..\..\..\Windows\System32\cmd.exe`): Handled safely without crash, `pic.Image` set to null (True).
     * Corrupted byte files (4 random bytes): Handled safely without crash, `pic.Image` set to null (True).
4. **Architectural & Interface Contract Conformance**:
   - Conforms 100% to `PROJECT.md` Milestone 3 Interface Contract:
     * `EmployeeService.SaveEmployee(EmployeeDTO dto, string sourceImagePath)`
     * `UIHelper.LoadImageToPictureBox(PictureBox pb, string relativePath)`
5. **No Integrity Violations**:
   - No mock bypasses, fake test assertions, or hardcoded strings found. Real implementation operates with actual GDI+, file system, DTO, and database layers.

---

## 3. Caveats

- **No Caveats**: The implementation covers all Milestone 3 objectives. Edge cases, boundary conditions, concurrency, and memory management were independently verified and passed.

---

## 4. Conclusion

- **Verdict**: **APPROVE**
- Milestone 3 satisfies all acceptance criteria from `ORIGINAL_REQUEST.md`, `PROJECT.md`, and `SCOPE.md`.
- Code changes in `Utilities\UIHelper.cs`, `UserControl\UCEmployeeAdd.cs`, `UserControl\UCEmployeeEdit.cs`, `UserControl\UCEmployeeInform.cs`, `BLL\EmployeeService.cs`, and `Form\TuanHung\FrmProduct.cs` are clean, robust, and verified.

---

## 5. Verification Method

To independently reproduce the verification results:

```powershell
# 1. Verify 0 Image.FromFile
Get-ChildItem -Path Form, UserControl, BLL, DAL, Utilities -Recurse -Filter "*.cs" | Select-String -Pattern "Image\.FromFile"

# 2. Verify 0 hardcoded drive paths
Get-ChildItem -Path Form, UserControl, BLL, DAL, Utilities -Recurse -Filter "*.cs" | Select-String -Pattern '(?i)[a-z]:[\\/]' | Where-Object { $_.Line -notmatch 'http://' -and $_.Line -notmatch 'https://' -and $_.Line -notmatch ':\\n' }

# 3. Clean MSBuild Rebuild
& "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" DoAnCuoiKi_QuanLyTiemCaPhe.csproj /t:Rebuild /p:Configuration=Debug

# 4. Run Milestone 3 Challenger Stress Suite
powershell.exe -ExecutionPolicy Bypass -File tests\Test_M3_Challenger.ps1

# 5. Run Master E2E Automated Suite
powershell.exe -ExecutionPolicy Bypass -File tests\run_e2e_tests.ps1
```

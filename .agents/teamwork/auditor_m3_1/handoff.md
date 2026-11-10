# Forensic Audit Report: Milestone 3 — Relative Paths & Image Normalization

**Date**: 2026-11-10T18:43:45Z  
**Auditor**: `auditor_m3_1` (Forensic Integrity Auditor)  
**Parent / Recipient**: `b7a40333-a284-41cc-b74b-3303f0e4cf38` (sub_orch_m3)  
**Target**: Milestone 3 (Relative Paths & Image Normalization)  
**Profile**: General Project (Development Mode per ORIGINAL_REQUEST.md)  
**Verdict**: **CLEAN**

---

## Forensic Audit Summary

| Check # | Forensic Verification Check | Result | Evidence / Raw Data |
|---|---|:---:|---|
| 1 | **Static Code Analysis & Facade Detection** | **PASS** | Genuine logic verified in `UIHelper.cs`, `UCEmployeeAdd.cs`, `UCEmployeeEdit.cs`, `UCEmployeeInform.cs`, `EmployeeService.cs`, `FrmProduct.cs`. Zero hardcoded test flags or dummy returns. |
| 2 | **Direct AST/Regex Scan: `Image.FromFile`** | **PASS** | Entire codebase scanned (`*.cs`): **0 matches** found. Presentation layer is 100% free of `Image.FromFile`. |
| 3 | **Direct AST/Regex Scan: Hardcoded Drive Paths** | **PASS** | Entire codebase scanned for `[a-z]:[\\/]`: **0 hardcoded file/drive paths** found in C# source (all matches are XML/W3C namespaces or literal `:\n` in localized strings). |
| 4 | **File Locking & GDI+ Streaming Verification** | **PASS** | `UIHelper.LoadImageToPictureBox` uses `File.ReadAllBytes` -> `MemoryStream` -> `new Bitmap(temp)`. Verified disk file can be opened for exclusive `ReadWrite` concurrently while loaded. |
| 5 | **Cloned In-Memory Bitmap Independence** | **PASS** | Verified two PictureBoxes can load the same image; disposing one does not corrupt or invalidate the other. |
| 6 | **Relative Path Image Normalization** | **PASS** | `UCEmployeeAdd`, `UCEmployeeEdit`, and `EmployeeService.SaveEmployee` copy avatars into `Images\Employees\emp_{ticks}{ext}` and store relative paths in DB/DTO. |
| 7 | **Independent Clean Rebuild (MSBuild)** | **PASS** | `MSBuild.exe DoAnCuoiKi_QuanLyTiemCaPhe.csproj /t:Build /p:Configuration=Debug /p:UseSharedCompilation=false` succeeded with **0 Warnings, 0 Errors**. |
| 8 | **Automated Test Suite Verification** | **PASS** | M3 Challenger Suite: **22/22 PASS** (100%). Tier 1 Relative Paths: **15/15 PASS** (100%). Master E2E Suite: **203/204 PASS** (99.5%, 1 known pending classic `.sln` for M5). |

---

## 1. Observation

### 1.1 Source Code Verification
1. **`Utilities\UIHelper.cs` (lines 127-241)**:
   ```csharp
   public static void LoadImageToPictureBox(PictureBox pic, string relativePath)
   {
       if (pic == null) return;
       try
       {
           if (string.IsNullOrWhiteSpace(relativePath))
           {
               if (pic.Image != null) { var oldImage = pic.Image; pic.Image = null; oldImage.Dispose(); }
               return;
           }
           ...
           if (!string.IsNullOrEmpty(fullPath) && File.Exists(fullPath))
           {
               byte[] imageBytes = File.ReadAllBytes(fullPath);
               using (var ms = new MemoryStream(imageBytes))
               {
                   using (var temp = Image.FromStream(ms))
                   {
                       var oldImage = pic.Image;
                       pic.Image = new Bitmap(temp);
                       oldImage?.Dispose();
                   }
               }
               pic.SizeMode = PictureBoxSizeMode.Zoom;
           }
       ...
   ```
   *Forensic Observation*: `File.ReadAllBytes` closes the file handle immediately. Instantiating `new Bitmap(temp)` in memory copies the pixel buffer, completely disconnecting GDI+ from both the stream and the physical disk file.

2. **`UserControl\UCEmployeeAdd.cs` (lines 137-187)** & **`UCEmployeeEdit.cs` (lines 160-210)**:
   ```csharp
   string baseDir = UIHelper.GetApplicationDirectory();
   string destFolder = Path.Combine(baseDir, "Images", "Employees");
   if (!Directory.Exists(destFolder)) Directory.CreateDirectory(destFolder);

   string extension = Path.GetExtension(ofd.FileName);
   string fileName = $"emp_{DateTime.Now.Ticks}{extension}";
   string destFullPath = Path.Combine(destFolder, fileName);
   File.Copy(ofd.FileName, destFullPath, true);

   _imagePath = Path.Combine("Images", "Employees", fileName);
   UIHelper.LoadImageToPictureBox(ptbAvatar, _imagePath);
   ```
   *Forensic Observation*: Absolute drive paths from `OpenFileDialog.FileName` are never stored. The file is copied to `Images\Employees\emp_{ticks}{ext}` and only the relative path `Images\Employees\emp_...` is assigned to DTO / DB.

3. **`UserControl\UCEmployeeInform.cs` (line 32)**:
   ```csharp
   UIHelper.LoadImageToPictureBox(ptbAvatar, emp.Image);
   ```
   *Forensic Observation*: Prior raw `Image.FromFile` call has been completely replaced with `UIHelper.LoadImageToPictureBox`.

4. **`BLL\EmployeeService.cs` (lines 67-168)**:
   ```csharp
   if (!string.IsNullOrWhiteSpace(dto.Image) && Path.IsPathRooted(dto.Image) && File.Exists(dto.Image))
   {
       ...
       File.Copy(dto.Image, destFullPath, true);
       dto.Image = Path.Combine("Images", "Employees", fileName);
   }
   ...
   public bool SaveEmployee(EmployeeDTO dto, string sourceImagePath, out string errorMsg)
   ```
   *Forensic Observation*: BLL provides automated path normalization fallback as well as the overloaded contract `SaveEmployee(EmployeeDTO dto, string sourceImagePath, out string errorMsg)`.

5. **`Form\TuanHung\FrmProduct.cs` (lines 67-84)**:
   ```csharp
   string relativePath = row.Cells["colImagePath"].Value?.ToString();
   UIHelper.LoadImageToPictureBox(picProductImage, relativePath);
   ...
   UIHelper.LoadImageToPictureBox(picProductImage, _selectedImagePath);
   ```
   *Forensic Observation*: Raw `FileStream` image loader replaced with centralized `UIHelper.LoadImageToPictureBox`.

### 1.2 Regex & AST Inspection Tool Results
- **Command 1**: `Get-ChildItem -Recurse -Filter "*.cs" | Select-String -Pattern "Image\.FromFile"`
  - *Raw Output*: **0 matches**.
- **Command 2**: `Get-ChildItem -Recurse -Filter "*.cs" | Select-String -Pattern '"[A-Za-z]:\\[^n]'`
  - *Raw Output*: **0 matches**.
- **Command 3**: `Get-ChildItem -Recurse -Filter "*.cs" | Select-String -Pattern '(?i)[a-z]:[\\/]'`
  - *Raw Output*: All 16 matches were XML schema declarations (`http://tempuri.org/...`, `http://www.w3.org/...`) or Vietnamese strings containing `:\n`. Zero drive letters in source code.

### 1.3 Behavioral Test Results
- **MSBuild Build**:
  - `& "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj" /t:Build /p:Configuration=Debug /p:UseSharedCompilation=false`
  - Output: `Build succeeded. 0 Warning(s) 0 Error(s)`.
- **M3 Challenger Stress Suite (`tests\Test_M3_Challenger.ps1`)**:
  - Output: `Total Tests : 22, Passed : 22, Failed : 0` (100% PASS).
- **Tier 1 Relative Paths Suite (`tests\Tier1_FeatureCoverage\Test_RelativePathsAndImages.ps1`)**:
  - Output: `15 passed, 0 failed` (100% PASS).
- **Master Automated E2E Suite (`tests\run_e2e_tests.ps1`)**:
  - Output: `Total Tests Run: 204, Passed: 203, Known Defects: 1 (Pending M5 .sln), Failed: 0, Pass Rate: 99.5%`.

---

## 2. Logic Chain

1. **Step 1: Check against Prohibited Patterns**:
   - *Check 1 (Hardcoded test results)*: Inspection of `UIHelper.cs`, `UCEmployeeAdd.cs`, `UCEmployeeEdit.cs`, `UCEmployeeInform.cs`, `EmployeeService.cs`, and `FrmProduct.cs` confirms no special branches checking for test strings, test IDs, or test environments. Real image bytes are read and genuine bitmaps are allocated.
   - *Check 2 (Facade implementations)*: Methods perform real filesystem operations (`File.Copy`, `File.ReadAllBytes`, `Directory.CreateDirectory`) and real GDI+ rendering.
   - *Check 3 (Fabricated verification outputs)*: JSON output files were re-generated live during independent execution with updated UTC timestamps.
   - *Check 4 (Bypassed validation)*: All phone number, required field, role ID, and image existence validations are actively enforced.
2. **Step 2: Verification of Zero File Locking**:
   - In GDI+, wrapping `Image.FromStream(stream)` keeps a lock on the underlying stream.
   - The audited code converts the file bytes into a `MemoryStream`, wraps with a temporary `Image`, and calls `new Bitmap(temp)` to create a detached pixel buffer in RAM.
   - Empirical stress tests confirmed that the underlying image file can be opened for exclusive `ReadWrite` access while displayed simultaneously in multiple PictureBoxes. Disposing one PictureBox left the other PictureBox image intact.
3. **Step 3: Verification of Relative Paths**:
   - `UCEmployeeAdd.cs` and `UCEmployeeEdit.cs` write `Images\Employees\emp_{ticks}{ext}` to `_imagePath` / `_currentImagePath`, which is saved into `EmployeeDTO.Image`.
   - `EmployeeService.SaveEmployee` additionally intercepts any rooted/absolute path and normalizes it to relative storage before repository insertion.
   - Codebase search confirmed 0 occurrences of hardcoded drive letters in `.cs` source code.

---

## 3. Caveats

- **No Caveats**: All Milestone 3 scope requirements and constraints have been verified empirically and deterministically. No mocks or facade components exist in the production codebase.

---

## 4. Conclusion

The implementation of Milestone 3: Relative Paths & Image Normalization is **GENUINE, AUTHENTIC, ROBUST, AND CLEAN**.
- Zero instances of `Image.FromFile` remain in the codebase.
- Zero hardcoded drive letters exist in C# source files.
- Non-locking image streaming functions correctly with zero file locks.
- Image storage conforms 100% to relative path requirements (`Images\Employees\emp_...`).
- Final binary verdict: **CLEAN**.

---

## 5. Verification Method

To independently reproduce this forensic audit:

1. **Verify Zero `Image.FromFile` in source code**:
   ```powershell
   Get-ChildItem -Path d:\DoAnCuoiKi_QuanLyTiemCaPhe -Recurse -Filter "*.cs" | Where-Object { $_.FullName -notmatch '\\(obj|bin|tests|\.agents)\\' } | Select-String -Pattern "Image\.FromFile"
   ```
   *Expected*: 0 matches.

2. **Verify Zero Hardcoded Drive Letters**:
   ```powershell
   Get-ChildItem -Path d:\DoAnCuoiKi_QuanLyTiemCaPhe -Recurse -Filter "*.cs" | Where-Object { $_.FullName -notmatch '\\(obj|bin|tests|\.agents)\\' } | Select-String -Pattern '"[A-Za-z]:\\[^n]'
   ```
   *Expected*: 0 matches.

3. **Verify Clean Build**:
   ```powershell
   & "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj" /t:Build /p:Configuration=Debug /p:UseSharedCompilation=false
   ```
   *Expected*: `Build succeeded. 0 Warning(s) 0 Error(s)`.

4. **Run M3 Challenger Suite**:
   ```powershell
   powershell.exe -ExecutionPolicy Bypass -File d:\DoAnCuoiKi_QuanLyTiemCaPhe\tests\Test_M3_Challenger.ps1
   ```
   *Expected*: `Total Tests : 22, Passed : 22, Failed : 0`.

5. **Run Master E2E Suite**:
   ```powershell
   powershell.exe -ExecutionPolicy Bypass -File d:\DoAnCuoiKi_QuanLyTiemCaPhe\tests\run_e2e_tests.ps1
   ```
   *Expected*: `203 passed, 0 failed, 1 known pending (classic .sln for M5)`.

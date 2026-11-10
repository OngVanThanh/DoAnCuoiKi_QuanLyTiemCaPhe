# Challenger 2 Handoff Report: Milestone 3 — Relative Paths & Image Normalization

**Date**: 2026-11-10T18:44:00Z  
**Challenger**: `challenger_m3_2` (Empirical Challenger)  
**Parent / Recipient**: `b7a40333-a284-41cc-b74b-3303f0e4cf38` (sub_orch_m3)  
**Milestone**: Milestone 3 (Relative Paths & Image Normalization)  
**Verdict**: **APPROVE** (42/42 Challenger Stress Tests Pass | 203/204 Master E2E Tests Pass | 0 GDI+ Handle Leaks | Zero File Locks)

---

## 1. Observation

### 1.1 Source Code Inspection
1. **`Utilities\UIHelper.cs` (lines 128-241)**:
   ```csharp
   public static void LoadImageToPictureBox(PictureBox pic, string relativePath)
   {
       if (pic == null) return;
       try
       {
           if (string.IsNullOrWhiteSpace(relativePath))
           {
               if (pic.Image != null)
               {
                   var oldImage = pic.Image;
                   pic.Image = null;
                   oldImage.Dispose();
               }
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
           else
           {
               if (pic.Image != null)
               {
                   var oldImage = pic.Image;
                   pic.Image = null;
                   oldImage.Dispose();
               }
           }
       }
       catch
       {
           if (pic.Image != null)
           {
               try { var oldImage = pic.Image; pic.Image = null; oldImage.Dispose(); } catch { }
           }
       }
   }
   ```
   *Direct Observation*: 
   - Uses `File.ReadAllBytes` which opens, reads, and closes file handles immediately.
   - Decouples GDI+ from the underlying stream by allocating `new Bitmap(temp)` in memory.
   - Cleans up previously bound images via `oldImage?.Dispose()` across all normal, empty, missing, and exception paths.

2. **`BLL\EmployeeService.cs` (lines 67-102 and 115-168)**:
   - `SaveEmployee(EmployeeDTO dto, out string errorMsg)`:
     ```csharp
     if (!string.IsNullOrWhiteSpace(dto.Image) && Path.IsPathRooted(dto.Image) && File.Exists(dto.Image))
     {
         ...
         string baseDir = Utilities.UIHelper.GetApplicationDirectory();
         string destFolder = Path.Combine(baseDir, "Images", "Employees");
         ...
         string fileName = $"emp_{DateTime.Now.Ticks}{extension}";
         string destFullPath = Path.Combine(destFolder, fileName);
         File.Copy(dto.Image, destFullPath, true);
         ...
         dto.Image = Path.Combine("Images", "Employees", fileName);
     }
     ```
   - Overloaded `SaveEmployee(EmployeeDTO dto, string sourceImagePath, out string errorMsg)`:
     Safely copies `sourceImagePath` into `Images\Employees\emp_{ticks}{ext}` and mutates `dto.Image` to the relative path `Images\Employees\emp_...`.

3. **`UserControl\UCEmployeeAdd.cs` (lines 148-180)** and **`UserControl\UCEmployeeEdit.cs` (lines 171-203)**:
   - Avatar picker creates `destFolder = Path.Combine(baseDir, "Images", "Employees")`.
   - Copies file to `$"emp_{DateTime.Now.Ticks}{extension}"`.
   - Sets `_imagePath` / `_currentImagePath = Path.Combine("Images", "Employees", fileName)`.
   - Calls `UIHelper.LoadImageToPictureBox(ptbAvatar, _imagePath)`. Zero raw `Image.FromFile`.

### 1.2 Empirical Execution Results
1. **Adversarial Stress Test Suite (`tests\Test_Challenger2_M3_EmpiricalStress.ps1`)**:
   Command: `powershell.exe -ExecutionPolicy Bypass -File tests\Test_Challenger2_M3_EmpiricalStress.ps1`
   - **Total Tests Run**: 42
   - **Passed**: 42 (100%)
   - **Failed**: 0
   - **Suite 1 (File Locking & Stream Independence)**:
     * Exclusive ReadWrite file access while displayed in PictureBox: PASS (`FileShare.None` opened successfully without `IOException`).
     * `File.WriteAllBytes` overwrite while displayed: PASS.
     * `File.Move` (rename) while displayed: PASS.
     * `File.Delete` while displayed: PASS.
     * Cross-process deletion via separate `powershell.exe` process: PASS (Exit code 0).
     * GDI+ canvas render / clone after file deleted from disk: PASS (Zero GDI+ `ExternalException`).
   - **Suite 2 (Concurrency, Rapid 100x Reload & GDI Leak Audit)**:
     * 100 rapid reloads in tight loop: PASS (completed in 142 ms).
     * Win32 `GetGuiResources(process, GR_GDIOBJECTS)` before vs after 100 loads: Baseline = 3, Post-100 = 3, **Delta = +0** (Zero GDI handles leaked).
     * Loading `$null` reset GDI handles back to baseline: PASS.
     * Parallel multi-threaded execution across 20 background tasks via C# CLR `Parallel.For`: PASS without race condition or collision.
   - **Suite 3 (Employee Avatar Save Integrity)**:
     * Absolute source path outside project (`$env:TEMP\c2_emp_avatar_source.jpg`): PASS. `dto.Image` transformed to `Images\Employees\emp_*.jpg` with zero drive letters. Destination file created and byte size (1,030 bytes) matched exactly.
     * DTO.Image passed with absolute path to `SaveEmployee(dto, out msg)`: PASS. Auto-normalized to `Images\Employees\emp_*.png`.
     * Non-existent and empty paths: PASS (no unhandled crashes).
   - **Suite 4 (Adversarial Edge Cases & Path Robustness)**:
     * Unicode Vietnamese filename with diacritics (`nhân_viên_tiệm_cà_phê.png`): PASS.
     * Forward-slash paths (`Images/Employees/...`): PASS.
     * Leading slashes (`\Images\...` and `/Images/...`): PASS.
     * Transition from valid image to non-existent path: PASS (`pb.Image` reset to null, old image disposed).
     * Zero-byte file: PASS (safely handled, `pb.Image = null`).
     * Corrupted text header disguised as PNG: PASS (safely handled, `pb.Image = null`).
   - **Suite 5 (Static Codebase Scan)**:
     * `Image.FromFile` in `Form, UserControl, BLL, DAL, DTO, Utilities`: **0 occurrences**.
     * Hardcoded drive letters (`[A-Za-z]:\`) in `Form, UserControl, BLL, DAL, DTO, Utilities`: **0 occurrences**.

2. **Master E2E Automated Suite (`tests\run_e2e_tests.ps1`)**:
   Command: `powershell.exe -ExecutionPolicy Bypass -File tests\run_e2e_tests.ps1`
   - **Total Tests Run**: 204
   - **Passed**: 203 (99.5%)
   - **Failed**: 0
   - **Known Defects**: 1 (Pending M5 classic .sln solution file)

3. **MSBuild Rebuild**:
   Command: `& "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" DoAnCuoiKi_QuanLyTiemCaPhe.csproj /t:Build /p:Configuration=Debug`
   - Result: `Build succeeded. 0 Warning(s), 0 Error(s)`.

---

## 2. Logic Chain

1. **File Locking Elimination**:
   - `UIHelper.LoadImageToPictureBox` reads all bytes via `File.ReadAllBytes(fullPath)` into a byte array in managed memory.
   - The file handle opened by the OS kernel is closed immediately when `ReadAllBytes` returns.
   - A `MemoryStream` wraps the byte array and constructs `new Bitmap(temp)`.
   - *Direct Empirical Evidence*: While the `PictureBox` actively held the image, our empirical harness opened the file for exclusive `ReadWrite` with `FileShare.None`, wrote bytes to it, renamed it via `File.Move`, overwrote it via `File.WriteAllBytes`, and deleted it via an external background process. All operations succeeded with zero `IOException`. Furthermore, drawing `pb.Image` to a new Graphics context after file deletion succeeded without GDI+ `ExternalException`, demonstrating complete decoupling.

2. **GDI+ Handle & Memory Leak Prevention**:
   - In GDI+, replacing `pb.Image` without calling `Dispose()` on the previous instance leaks Windows GDI handles (`HBITMAP`).
   - `UIHelper.LoadImageToPictureBox` executes `var oldImage = pic.Image; pic.Image = new Bitmap(temp); oldImage?.Dispose();`.
   - *Direct Empirical Evidence*: P/Invoke `GetGuiResources(process, GR_GDIOBJECTS)` tracked GDI handles before and after 100 tight-loop reload cycles. GDI handle count remained exactly identical (Delta = +0). Passing `$null` returned GDI handle count to baseline. Multi-threaded CLR stress across 20 parallel threads executed without memory corruption or race conditions.

3. **Avatar Save Integrity & Path Normalization**:
   - Both UI avatar pickers (`UCEmployeeAdd`, `UCEmployeeEdit`) and BLL `EmployeeService` automatically intercept image paths:
     1. Destination directory `Images\Employees` is ensured to exist.
     2. Files are uniquely named `emp_{DateTime.Now.Ticks}{extension}`.
     3. Files are copied to destination.
     4. `dto.Image` is mutated to purely relative `Images\Employees\emp_{ticks}{ext}`.
   - *Direct Empirical Evidence*: Supplying an absolute path from `$env:TEMP` resulted in `dto.Image` having 0 drive letters, matching relative regex `^Images\\Employees\\emp_.*\.jpg$`. The physical file was created in `Images\Employees`, and file contents matched source bytes exactly.

---

## 3. Caveats

- **No Caveats**: All 4 areas specified in the Mission (File locking verification, rapid concurrency / GDI handle leak audit, employee avatar save integrity, and edge-case resilience) were empirically tested with dedicated test scripts and passed 100%.

---

## 4. Conclusion

**Verdict: APPROVE**

Milestone 3 implementation for Relative Paths and Image Normalization is robust, leak-free, non-locking, and fully compliant with all architectural and instructor constraints:
- **Zero File Locks**: Validated across read, write, overwrite, rename, delete, and cross-process deletion.
- **Zero GDI+ Handle Leaks**: Validated via native Win32 `GetGuiResources` across 100 rapid reload cycles (Delta: +0).
- **Zero Drive Paths**: Validated across all source files and employee save workflows.
- **Zero `Image.FromFile`**: Completely replaced by `UIHelper.LoadImageToPictureBox`.
- **42/42 Challenger Stress Tests PASS (100%)**.
- **203/204 Master E2E Tests PASS (99.5%)**.
- Ready to proceed to Milestone 4.

---

## 5. Verification Method

To independently reproduce and verify this assessment:

1. **Run Challenger 2 Empirical Stress Suite**:
   ```powershell
   powershell.exe -ExecutionPolicy Bypass -File tests\Test_Challenger2_M3_EmpiricalStress.ps1
   ```
   *Expected output*: `Total Tests Run: 42, Passed: 42, Failed: 0, Pass Rate: 100%`.

2. **Run Challenger 1 Stress Suite**:
   ```powershell
   powershell.exe -ExecutionPolicy Bypass -File tests\Test_M3_Challenger.ps1
   ```
   *Expected output*: `Total Tests: 22, Passed: 22, Failed: 0`.

3. **Run Master E2E Test Suite**:
   ```powershell
   powershell.exe -ExecutionPolicy Bypass -File tests\run_e2e_tests.ps1
   ```
   *Expected output*: `Total Tests Run: 204, Passed: 203, Known Defects: 1, Failed: 0, Pass Rate: 99.5%`.

4. **Verify Zero `Image.FromFile` and Zero Hardcoded Drive Paths**:
   ```powershell
   Get-ChildItem -Path Form, UserControl, BLL, DAL, DTO, Utilities -Filter "*.cs" -Recurse | Select-String -Pattern "Image\.FromFile"
   Get-ChildItem -Path Form, UserControl, BLL, DAL, DTO, Utilities -Filter "*.cs" -Recurse | Select-String -Pattern '(?i)[a-z]:[\\/]' | Where-Object { $_.Line -notmatch 'http://' -and $_.Line -notmatch 'https://' -and $_.Line -notmatch ':\\n' }
   ```
   *Expected output*: 0 matches for both queries.

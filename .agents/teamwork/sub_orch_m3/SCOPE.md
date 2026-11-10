# Scope: Milestone 3 — Relative Paths & Image Normalization

## Objectives
1. **Employee Avatar Relative Storage**:
   - In `UserControl\UCEmployeeAdd.cs` and `UserControl\UCEmployeeEdit.cs`:
     * When user picks an image file via `OpenFileDialog`, ensure target directory `Path.Combine(Application.StartupPath, "Images", "Employees")` exists.
     * Copy the chosen image file to `Application.StartupPath\Images\Employees\emp_{DateTime.Now.Ticks}{extension}`.
     * Save only the relative path `Images\Employees\emp_...` into `EmployeeDTO.Image` and database.
2. **Non-Locking Relative Image Loading**:
   - In `UserControl\UCEmployeeInform.cs`, `UserControl\UCEmployeeEdit.cs`, `UserControl\UCEmployeeAdd.cs`:
     * Eliminate all direct calls to `Image.FromFile()`.
     * Use `UIHelper.LoadImageToPictureBox(ptbAvatar, relativePath)` which resolves relative to `Application.StartupPath` and streams via `MemoryStream` to prevent file locking and missing-drive crashes.
3. **100% Relative Path Audit**:
   - Ensure ZERO occurrences of `Image.FromFile` or hardcoded drive letters (`C:\`, `D:\`) exist in `UserControl\` or Presentation layer.
   - Ensure the solution builds cleanly with 0 warnings, 0 errors.

## Relevant Files & Paths
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\TEST_READY.md`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\Utilities\UIHelper.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\UserControl\UCEmployeeAdd.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\UserControl\UCEmployeeEdit.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\UserControl\UCEmployeeInform.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\BLL\EmployeeService.cs`

## Verification
- MSBuild rebuild: 0 errors, 0 warnings.
- `Select-String -Path UserControl\*.cs -Pattern "Image\.FromFile"` returns 0 matches.
- Automated tests in `tests\run_e2e_tests.ps1` pass 100%.

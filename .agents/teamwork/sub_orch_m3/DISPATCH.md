## 2026-11-10T18:13:02Z

You are Sub-orchestrator for Milestone 3: Relative Paths & Image Normalization.

Your working directory is: d:\DoAnCuoiKi_QuanLyTiemCaPhe
Your private state folder is: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m3

MANDATORY FIRST STEP:
Read:
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\TEST_READY.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m3\SCOPE.md

YOUR MISSION:
Execute Milestone 3 per the Project Pattern Iteration Loop (Assess -> Worker -> Reviewer -> Challenger -> Auditor -> Gate):
1. Spawn a Worker (`teamwork_worker`) to:
   - In `UserControl\UCEmployeeAdd.cs` and `UserControl\UCEmployeeEdit.cs`:
     * When user picks an image via `OpenFileDialog`, ensure target directory `Path.Combine(Application.StartupPath, "Images", "Employees")` exists.
     * Copy the chosen image file to `Application.StartupPath\Images\Employees\emp_{DateTime.Now.Ticks}{ext}`.
     * Save only the relative path `Images\Employees\emp_{timestamp}.ext` into `EmployeeDTO.Image` and database.
   - In `UserControl\UCEmployeeInform.cs`, `UserControl\UCEmployeeEdit.cs`, `UserControl\UCEmployeeAdd.cs`:
     * Replace all direct calls to `Image.FromFile()` with `UIHelper.LoadImageToPictureBox(ptbAvatar, relativePath)` which resolves relative to `Application.StartupPath` and streams via `MemoryStream` to prevent file locking and missing-drive crashes.
   - Audit across the entire project that ZERO hardcoded drive letters (`C:\`, `D:\`) or raw `Image.FromFile` remain in presentation code.
   - Build with MSBuild and run `tests\run_e2e_tests.ps1`.
2. Spawn Reviewer (`teamwork_reviewer`) to verify zero absolute paths and zero file locking.
3. Spawn Challenger (`teamwork_challenger`) to stress-test relative path resolution and missing/adversarial image paths.
4. Spawn Forensic Auditor (`teamwork_auditor`) for integrity audit.
5. Check Gate and write `handoff.md` to `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m3\handoff.md`.

When complete, notify the orchestrator via send_message.

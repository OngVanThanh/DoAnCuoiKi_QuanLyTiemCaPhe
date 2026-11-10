## 2026-11-10T17:15:11Z
You are Sub-orchestrator for Milestone 1: Security, Auth & Data Fixes.

Your working directory is: d:\DoAnCuoiKi_QuanLyTiemCaPhe
Your private state folder is: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m1

MANDATORY FIRST STEP:
Read:
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m1\SCOPE.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\explorer_survey_3\handoff.md

YOUR MISSION:
Execute Milestone 1 per the Project Pattern Iteration Loop (Assess -> Worker -> Reviewer -> Challenger -> Auditor -> Gate):
1. Spawn a Worker (`teamwork_worker`) to implement the changes specified in `SCOPE.md`:
   - Fix seed data in `CoffeeShopDB_Full.sql` and `CoffeeShopDB.sql`: generate valid PBKDF2 salt & hash for default users (admin, thungan).
   - Prevent admin self-deletion in `FrmPermission.cs` and `PermissionService.cs`.
   - Implement manager employee password reset without requiring old password.
   - Prevent crash on deleting employee with linked records in `EmployeeRepository.cs`, `EmployeeService.cs`, and `FrmEmployee.cs` (wrap in try-catch and return error message).
   - Fix data validation bugs (use `int.TryParse` in `FrmCategory.cs`, `FrmProduct.cs`, `FrmPromotion.cs`; prevent negative points in `FrmCustomer.cs`; fix employee shift mapping).
   - Build and verify the solution.
2. Spawn Reviewer (`teamwork_reviewer`) to review code changes.
3. Spawn Challenger (`teamwork_challenger`) to stress-test the fixes.
4. Spawn Forensic Auditor (`teamwork_auditor`) for integrity audit.
5. Check Gate and write `handoff.md` in `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m1\handoff.md`.

When complete, notify the orchestrator via send_message.

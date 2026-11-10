# Dispatch for challenger_m1_2
Milestone 1 adversarial challenge


## 2026-11-10T17:29:34Z
You are Challenger 2 for Milestone 1: Security, Auth & Data Fixes.

Working directory: d:\DoAnCuoiKi_QuanLyTiemCaPhe
State folder: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\challenger_m1_2

MANDATORY INPUT FILES TO READ FIRST:
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m1\SCOPE.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\worker_m1\handoff.md

YOUR TASK:
Empirically stress-test FK deletion handling and data validation hardening:
1. Write and execute test harness scripts (e.g. PowerShell or C# reflection script against compiled `bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe`):
   - Test `CustomerService.Add` and `CustomerService.Update`:
     Pass negative points (`RewardPoints = -1`, `RewardPoints = -100`, `RewardPoints = int.MinValue`) -> MUST be rejected (returns false with validation message).
     Pass valid non-negative points (0, 10, 500) -> MUST pass validation.
   - Test `EmployeeService.DeleteEmployee` and `EmployeeRepository.DeleteEmployee`:
     Verify that calling deletion with an ID that has FK references returns false with error message, and does NOT throw an unhandled exception.
   - Scan / inspect `FrmCategory.cs`, `FrmProduct.cs`, `FrmPromotion.cs`:
     Verify that zero unsafe `int.Parse` remains on user-facing textboxes. Test parsing logic with invalid inputs (empty string, letters, symbols, overflow numbers) to confirm safe handling.
2. Record all empirical test cases, pass/fail counts, and robustness findings.

Write your report to `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\challenger_m1_2\handoff.md`.
Clearly state your verdict: APPROVE or REJECT.
Notify me via send_message when complete.

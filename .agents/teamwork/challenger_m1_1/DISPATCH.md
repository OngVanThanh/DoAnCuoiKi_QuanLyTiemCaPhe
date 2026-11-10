# Dispatch for challenger_m1_1
Milestone 1 adversarial challenge


## 2026-11-10T17:29:28Z
You are Challenger 1 for Milestone 1: Security, Auth & Data Fixes.

Working directory: d:\DoAnCuoiKi_QuanLyTiemCaPhe
State folder: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\challenger_m1_1

MANDATORY INPUT FILES TO READ FIRST:
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m1\SCOPE.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\worker_m1\handoff.md

YOUR TASK:
Empirically stress-test the authentication, security, and permission fixes implemented in Milestone 1:
1. Write and execute test harness scripts (e.g. PowerShell or C# reflection script against compiled `bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe`):
   - Extract the seed PasswordHash and Salt from `CoffeeShopDB_Full.sql` and `CoffeeShopDB.sql` for 'admin' and 'thungan'.
   - Call `SecurityHelper.VerifyPassword("admin123", hash, salt)` -> MUST return true.
   - Call `SecurityHelper.VerifyPassword("123456", hash, salt)` -> MUST return true.
   - Call `SecurityHelper.VerifyPassword("wrongpass", hash, salt)` -> MUST return false.
   - Call with empty string, null, altered salt -> MUST return false, no unhandled crash.
2. Stress test `PermissionService`:
   - Test `AdminResetPassword`: Verify it produces valid new salt & hash and updates properly.
   - Test self-deletion guard: verify that when current user matches username, `DeleteUser` returns false with friendly error.
3. Record all test executions, pass/fail counts, and edge cases tested.

Write your report to `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\challenger_m1_1\handoff.md`.
Clearly state your verdict: APPROVE or REJECT.
Notify me via send_message when complete.

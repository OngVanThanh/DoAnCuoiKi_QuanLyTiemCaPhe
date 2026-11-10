## 2026-11-10T17:16:21Z
You are the Worker for Milestone 1: Security, Auth & Data Fixes.

Working directory: d:\DoAnCuoiKi_QuanLyTiemCaPhe
State folder: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\worker_m1

MANDATORY INPUT FILES TO READ FIRST:
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m1\SCOPE.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\explorer_survey_3\handoff.md

FILE WRITE OWNERSHIP:
You have exclusive write ownership of:
- CoffeeShopDB_Full.sql
- CoffeeShopDB.sql
- BLL\PermissionService.cs
- BLL\EmployeeService.cs
- BLL\CustomerService.cs
- DAL\EmployeeRepository.cs
- Form\VanThanh\FrmPermission.cs
- Form\VanThanh\FrmEmployee.cs
- Form\VanThanh\FrmShiftSchedule.cs
- Form\TuanHung\FrmCategory.cs
- Form\TuanHung\FrmProduct.cs
- Form\TuanHung\FrmPromotion.cs
- Form\QuangHung\FrmCustomer.cs

MANDATORY INTEGRITY WARNING:
DO NOT CHEAT. All implementations must be genuine. DO NOT hardcode test results, create dummy/facade implementations, or circumvent the intended task. A teamwork_auditor will independently verify your work. Integrity violations WILL be detected and your work WILL be rejected.

TASKS:
1. SQL Seed Data Password Hash Fix:
   - Check `Utilities\SecurityHelper.cs` to see exact PBKDF2 parameters (iterations, salt length, hash length, algorithm).
   - Generate valid PBKDF2 hash & salt for 'admin' (password: 'admin123') and 'thungan' (password: '123456').
   - Update `CoffeeShopDB_Full.sql` and `CoffeeShopDB.sql` seed data for Users table with the computed Base64 PasswordHash and Salt.
2. Manager Self-Deletion Guard:
   - In `Form\VanThanh\FrmPermission.cs` and `BLL\PermissionService.cs`, prevent a logged-in manager from deleting their own account or deleting the last active manager in the system.
3. Manager Employee Password Reset:
   - In `BLL\PermissionService.cs`, add `AdminResetPassword(string username, string newPassword, out string message)` or similar method allowing a manager to reset an employee's password without needing the old password.
   - In `Form\VanThanh\FrmPermission.cs`, update the password reset flow so that when an admin resets an employee's password, it does not prompt for the employee's old password.
4. FK Constraint Violation Handling on Employee Deletion:
   - In `DAL\EmployeeRepository.cs`, `BLL\EmployeeService.cs`, and `Form\VanThanh\FrmEmployee.cs`:
     Wrap deletion logic in try-catch. If an employee has linked records (Orders, EmployeeShifts, InventoryTransactions) or SqlException is caught, return `false` with a descriptive error message (e.g. "Không thể xóa nhân viên đã có dữ liệu giao dịch hoặc ca làm việc liên kết!") instead of crashing.
5. Data Validation Hardening:
   - In `Form\TuanHung\FrmCategory.cs`, `Form\TuanHung\FrmProduct.cs`, `Form\TuanHung\FrmPromotion.cs`: replace unsafe `int.Parse` with `int.TryParse`.
   - In `Form\QuangHung\FrmCustomer.cs` and `BLL\CustomerService.cs`: ensure `RewardPoints >= 0`.
   - In `Form\VanThanh\FrmShiftSchedule.cs`: bind/lookup employees by ID instead of employee name string to avoid collisions with identical names.
6. Build Verification:
   - Run MSBuild on `DoAnCuoiKi_QuanLyTiemCaPhe.csproj` or the solution to ensure 0 errors and 0 warnings if possible.
   - Document build output and test verification in your handoff report.

Write your final report to: `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\worker_m1\handoff.md`.
Notify me via send_message when complete.

# Scope: Milestone 1 — Security, Auth & Data Fixes

## Status: DONE

## Objectives
1. **SQL Seed Data Password Hash Fix**:
   - In `CoffeeShopDB_Full.sql` (and `CoffeeShopDB.sql`), updated seed users (`admin`, `thungan`) so they have valid PBKDF2 hash and salt matching `SecurityHelper.HashPassword()`.
   - Default accounts successfully authenticate in `AuthService.ValidateUser()` and `SecurityHelper.VerifyPassword()`.
2. **Manager Self-Deletion Guard**:
   - In `Form\VanThanh\FrmPermission.cs` and `BLL\PermissionService.cs`, prevented a logged-in manager from deleting their own account or deleting the last active manager in the system.
3. **Manager Employee Password Reset**:
   - In `Form\VanThanh\FrmPermission.cs` and `BLL\PermissionService.cs`, implemented a manager reset password feature (`AdminResetPassword`) for employee accounts without requiring the employee's old password.
4. **FK Constraint Violation Handling on Employee Deletion**:
   - In `DAL\EmployeeRepository.cs`, `BLL\EmployeeService.cs`, and `Form\VanThanh\FrmEmployee.cs`, added proactive checks across Orders, EmployeeShifts, InventoryTransactions, Users, and wrapped deletion in try-catch returning clear Vietnamese status messages instead of crashing.
5. **Data Validation Hardening**:
   - Replaced all unsafe `int.Parse` with `int.TryParse` across `Form\TuanHung\FrmCategory.cs`, `Form\TuanHung\FrmProduct.cs`, `Form\TuanHung\FrmPromotion.cs`.
   - In `Form\QuangHung\FrmCustomer.cs` and `BLL\CustomerService.cs`, enforced `RewardPoints >= 0`.
   - In `Form\VanThanh\FrmShiftSchedule.cs`, bound employees by ID to avoid collisions with identical names.

## Modified Files
- `CoffeeShopDB_Full.sql`
- `CoffeeShopDB.sql`
- `BLL\PermissionService.cs`
- `BLL\EmployeeService.cs`
- `BLL\CustomerService.cs`
- `DAL\EmployeeRepository.cs`
- `Form\VanThanh\FrmPermission.cs`
- `Form\VanThanh\FrmEmployee.cs`
- `Form\VanThanh\FrmShiftSchedule.cs`
- `Form\TuanHung\FrmCategory.cs`
- `Form\TuanHung\FrmProduct.cs`
- `Form\TuanHung\FrmPromotion.cs`
- `Form\QuangHung\FrmCustomer.cs`

## Gate Verification Summary
- **Worker**: MSBuild Rebuild clean (0 warnings, 0 errors).
- **Reviewer 1**: APPROVE.
- **Reviewer 2**: APPROVE.
- **Challenger 1**: APPROVE (PBKDF2 auth, fuzzing, AdminResetPassword, self-deletion guard confirmed).
- **Challenger 2**: APPROVE (103/103 empirical tests passed).
- **Forensic Auditor**: CLEAN (18/18 forensic checks passed, 0 integrity violations).

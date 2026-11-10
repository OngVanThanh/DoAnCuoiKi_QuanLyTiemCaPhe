# REVIEW & ADVERSARIAL CHALLENGE REPORT — MILESTONE 1

**Reviewer**: Reviewer 1 (`reviewer_m1_1`)  
**Target**: Milestone 1 (Security, Auth & Data Fixes) by `worker_m1`  
**Date**: 2026-11-10T17:38:00Z  
**Verdict**: **APPROVE**  
**Integrity Assessment**: **NO INTEGRITY VIOLATION DETECTED**

---

## 1. Observation

### 1.1 SQL Seed Data Password Hash Fix
- In `CoffeeShopDB_Full.sql` (lines 275–277) and `CoffeeShopDB.sql` (lines 181–183):
  ```sql
  INSERT INTO Users (EmployeeId, Username, PasswordHash, Salt, RoleId, IsActive) VALUES 
  (1, 'admin', '4YUma6Q69CiQ97YVG17kFFFVD1HGqb2exAjnTpdQe0g=', 'MvzYIcst8H+HCiQecovlBA==', 1, 1),
  (2, 'thungan', 'ExGmL2DJ+2fEVbrgGAtbpkBl0c2eNsKncAH/rmQawuY=', 'RAq3VsoVEEY92GOgn+/wCA==', 2, 1);
  ```
- In `Utilities\SecurityHelper.cs` (lines 10–39):
  PBKDF2 uses 16-byte salt (`SaltSize = 16`), 32-byte hash (`HashSize = 32`), and 100,000 iterations (`Iterations = 100000`).
- Independent verification via PowerShell:
  ```powershell
  # Admin ('admin123'):
  [Convert]::ToBase64String((New-Object System.Security.Cryptography.Rfc2898DeriveBytes('admin123', [Convert]::FromBase64String('MvzYIcst8H+HCiQecovlBA=='), 100000)).GetBytes(32))
  # Output: 4YUma6Q69CiQ97YVG17kFFFVD1HGqb2exAjnTpdQe0g= (Exact Match)

  # Thungan ('123456'):
  [Convert]::ToBase64String((New-Object System.Security.Cryptography.Rfc2898DeriveBytes('123456', [Convert]::FromBase64String('RAq3VsoVEEY92GOgn+/wCA=='), 100000)).GetBytes(32))
  # Output: ExGmL2DJ+2fEVbrgGAtbpkBl0c2eNsKncAH/rmQawuY= (Exact Match)
  ```

### 1.2 Manager Self-Deletion & Last Manager Guard
- In `BLL\PermissionService.cs` (lines 130–180):
  `DeleteUser(string username, out string message)` validates against whitespace, retrieves `targetUser`, checks `if (UserSession.CurrentUser != null && string.Equals(UserSession.CurrentUser.Username, username, StringComparison.OrdinalIgnoreCase))` to block self-deletion, and checks `if (targetUser.RoleId == 1 && activeManagerCount <= 1)` to block deleting the last active manager.
- In `Form\VanThanh\FrmPermission.cs` (lines 125–131):
  `ExecuteDeleteAction` validates `UserSession.CurrentUser` client-side before confirmation dialog, preventing manager self-deletion in GUI.

### 1.3 Manager Employee Password Reset
- In `BLL\PermissionService.cs` (lines 188–231):
  `AdminResetPassword(string username, string newPassword, out string message)` validates username and password (enforcing minimum 6 characters), generates a fresh salt (`SecurityHelper.GenerateSalt()`), computes hash (`SecurityHelper.HashPassword(newPassword, newSalt)`), and calls `_userRepository.UpdatePassword(username.Trim(), newHash, newSalt)`. It does not require or ask for the employee's old password.
- In `Form\VanThanh\FrmPermission.cs` (lines 168–250):
  `ExecuteChangePasswordAction` calls `ShowAdminResetPasswordDialog(username)`, providing an admin modal prompting for new password and confirmation without asking for the old password.

### 1.4 FK Constraint Violation Handling on Employee Deletion
- In `DAL\EmployeeRepository.cs` (lines 161–207):
  `DeleteEmployee(int employeeId, out string errorMessage)` performs proactive foreign-key checks:
  ```csharp
  bool hasOrders = db.Orders.Any(o => o.EmployeeId == employeeId);
  bool hasShifts = db.EmployeeShifts.Any(s => s.EmployeeId == employeeId);
  bool hasInventory = db.InventoryTransactions.Any(it => it.EmployeeId == employeeId);
  bool hasUser = db.Users.Any(u => u.EmployeeId == employeeId);
  ```
  It wraps deletion in `catch (System.Data.SqlClient.SqlException)` and `catch (Exception ex)`, returning `false` with a descriptive error message instead of crashing. It provides backwards-compatible overload `DeleteEmployee(int employeeId) => DeleteEmployee(employeeId, out _)`.
- In `BLL\EmployeeService.cs` (lines 99–114):
  `DeleteEmployee(int employeeId, out string message)` validates `employeeId <= 0` and forwards to repository.
- In `Form\VanThanh\FrmEmployee.cs` (lines 186–213):
  The call is wrapped and displays `message` to the user via `MessageBox.Show`.

### 1.5 Data Validation Hardening
- In `Form\TuanHung\FrmCategory.cs` (lines 84, 108):
  Replaced all `int.Parse` calls with `int.TryParse`.
- In `Form\TuanHung\FrmProduct.cs` (lines 135, 144, 183, 211):
  Replaced `decimal.Parse` and `int.Parse` with `decimal.TryParse` and `int.TryParse`.
- In `Form\TuanHung\FrmPromotion.cs` (lines 91, 100, 137):
  Replaced `double.Parse` and `int.Parse` with `double.TryParse` and `int.TryParse`.
- Verification of zero remaining `int.Parse`:
  `git grep "int.Parse" Form/TuanHung/` returned 0 matches across all three forms.
- In `BLL\CustomerService.cs` (lines 65, 96) & `Form\QuangHung\FrmCustomer.cs` (lines 63, 113):
  `customer.RewardPoints < 0` is strictly rejected with `"Điểm tích lũy không được nhỏ hơn 0!"`.
- In `Form\VanThanh\FrmShiftSchedule.cs` (lines 19, 37–50, 97–102, 128–142, 227–233, 248–258):
  Created `EmployeeScheduleItem` with `Id` and `DisplayText`, bound `ValueMember = "Id"`, `DisplayMember = "DisplayText"`. Shift lookup, daily collision checks, and database updates now reference employee `Id` directly instead of matching by string name.

### 1.6 Build and Test Execution
- MSBuild rebuild command:
  ```powershell
  & "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj" /t:Rebuild /p:Configuration=Debug /p:UseSharedCompilation=false
  ```
  Result: **Build succeeded. 0 Warning(s), 0 Error(s)** in 2.43s.
- Automated Test Suite Execution:
  Command: `powershell -ExecutionPolicy Bypass -Command "& 'd:\DoAnCuoiKi_QuanLyTiemCaPhe\tests\run_e2e_tests.ps1' -Tier @(1, 2)"`
  Result: **144 Tests Run, 142 Passed, 2 Known Pending Defects (Milestone 2 & Milestone 5), 0 Failed (98.6% overall, 100% Tier 2 boundary cases passed)**.

---

## 2. Logic Chain

1. **Seed Data Integrity**:
   - Observation 1.1 proves mathematically that the hash strings in `CoffeeShopDB_Full.sql` and `CoffeeShopDB.sql` were generated by `SecurityHelper.HashPassword` using RFC2898DeriveBytes with 100,000 iterations and the exact 16-byte base64 salts provided.
   - Therefore, newly deployed databases will allow successful authentication for default administrative accounts `admin` and `thungan`.

2. **Access Control & Self-Deletion**:
   - Observation 1.2 demonstrates dual-layer defense: `FrmPermission.cs` intercepts self-deletion at the UI layer, and `PermissionService.DeleteUser` intercepts it at the BLL layer before database deletion.
   - Furthermore, active manager count check (`activeManagerCount <= 1`) ensures that the system cannot be orphaned without an active administrator.

3. **Credential Management**:
   - Observation 1.3 shows that `AdminResetPassword` fulfills the managerial requirement of resetting employee credentials without requiring the employee's current secret.
   - It enforces password policy (minimum 6 characters, non-empty) and regenerates cryptographic salt per user.

4. **Fault Tolerance on Deletion**:
   - Observation 1.4 confirms that `DeleteEmployee` in `DAL\EmployeeRepository.cs` contains proactive checks for all 4 linked relational entities (`Orders`, `EmployeeShifts`, `InventoryTransactions`, `Users`) and catches `SqlException` defensively.
   - This eliminates unhandled 547 FK constraint crashes, safely returning Vietnamese error notifications to the end user.

5. **Input Sanitation & Reliability**:
   - Observation 1.5 confirms complete elimination of `int.Parse` and `decimal.Parse` across `FrmCategory`, `FrmProduct`, and `FrmPromotion`, avoiding `FormatException` on empty or non-numeric inputs.
   - Customer reward points are guarded at both presentation and business logic layers against negative values.
   - Employee shift scheduling binds by primary key `Id`, preventing ambiguous assignments when two staff members share a name.

6. **Build & Regression Freedom**:
   - Observation 1.6 confirms clean compilation with 0 warnings, 0 errors, and 100% pass rate across all Tier 2 boundary tests.

---

## 3. Caveats & Adversarial Findings

### 3.1 Minor Optimization Finding (Fail-Fast Ordering)
- **Where**: `BLL\PermissionService.cs` lines 142–155.
- **What**: In `DeleteUser`, `_userRepository.GetByUserName(username)` is called at line 142 *before* the in-memory check `if (UserSession.CurrentUser != null && string.Equals(UserSession.CurrentUser.Username, username, ...))` at line 150.
- **Why**: An in-memory check against the active session should ideally execute before initiating a database query. In an environment where the database is disconnected or timing out, checking `UserSession.CurrentUser` first would fail fast in memory without requiring a database round-trip.
- **Severity**: Minor (Non-blocking). In the UI layer (`FrmPermission.cs`), the self-deletion check is already evaluated prior to calling the BLL service, and in normal execution the BLL check still fires before `_userRepository.DeleteUser`.
- **Recommendation**: For future refactoring passes, move the `UserSession.CurrentUser` comparison to line 141 before `GetByUserName`.

### 3.2 Out-of-Scope Scope Verification
- Certain repository methods outside Milestone 1 write scope (such as LINQ query syntax in `EmployeeRepository.GetEmployeesWithoutAccount`) remain pending Milestone 2 refactoring as scheduled in `PROJECT.md`.
- No integrity violations or unauthorized scope creep were detected.

---

## 4. Conclusion

**Verdict: APPROVE**

Worker `worker_m1` has completely and correctly addressed all requirements for Milestone 1:
- Cryptographically verified PBKDF2 hashes for seed accounts in both SQL scripts.
- Robust manager self-deletion guard and single-manager safeguard.
- Manager password reset for employees without old password prompt.
- FK crash vector on employee deletion completely resolved with proactive checks and graceful try-catch.
- Complete replacement of unsafe string parsing with `TryParse` and non-negative reward points validation.
- Employee shift schedule bound by unique numeric `Id`.
- 100% clean build (0 warnings, 0 errors) and 142 passing tests.

---

## 5. Verification Method

To independently verify this report:

1. **Rebuild Solution**:
   ```powershell
   & "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj" /t:Rebuild /p:Configuration=Debug /p:UseSharedCompilation=false
   ```
   *Expected*: `Build succeeded. 0 Warning(s), 0 Error(s)`.

2. **Verify PBKDF2 Hashes**:
   ```powershell
   [Convert]::ToBase64String((New-Object System.Security.Cryptography.Rfc2898DeriveBytes('admin123', [Convert]::FromBase64String('MvzYIcst8H+HCiQecovlBA=='), 100000)).GetBytes(32))
   # Returns: 4YUma6Q69CiQ97YVG17kFFFVD1HGqb2exAjnTpdQe0g=
   [Convert]::ToBase64String((New-Object System.Security.Cryptography.Rfc2898DeriveBytes('123456', [Convert]::FromBase64String('RAq3VsoVEEY92GOgn+/wCA=='), 100000)).GetBytes(32))
   # Returns: ExGmL2DJ+2fEVbrgGAtbpkBl0c2eNsKncAH/rmQawuY=
   ```

3. **Run 4-Tier Automated Suite (Tiers 1 & 2)**:
   ```powershell
   powershell -ExecutionPolicy Bypass -Command "& 'd:\DoAnCuoiKi_QuanLyTiemCaPhe\tests\run_e2e_tests.ps1' -Tier @(1, 2)"
   ```
   *Expected*: 142 Passed, 0 Failed, 2 Known Pending Defects.

4. **Verify No Remaining `int.Parse` in Form/TuanHung**:
   ```powershell
   git grep "int.Parse" Form/TuanHung/
   ```
   *Expected*: 0 matches.

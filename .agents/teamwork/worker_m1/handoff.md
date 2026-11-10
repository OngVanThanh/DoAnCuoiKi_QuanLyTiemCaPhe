# HANDOFF REPORT — MILESTONE 1: SECURITY, AUTH & DATA FIXES

## 1. Observation
- **SQL Seed Data Password Hash Bug**:
  - In `CoffeeShopDB_Full.sql` (lines 274-277) and `CoffeeShopDB.sql` (lines 180-184), default user passwords were stored in plaintext (`'admin123'`, `'123456'`) with empty salt (`''`).
  - In `BLL\PermissionService.cs` (lines 64-70), `VerifyPassword` immediately rejected login if `user.Salt` was empty or `user.PasswordHash` was not a valid 32-byte Base64 string.
  - In `Utilities\SecurityHelper.cs`, PBKDF2 parameters are: Salt size = 16 bytes, Hash size = 32 bytes, Iterations = 100,000, Base64 encoding.
- **Manager Self-Deletion & Last Manager Vulnerability**:
  - In `Form\VanThanh\FrmPermission.cs` (lines 108-158) and `BLL\PermissionService.cs` (lines 128-135), `DeleteUser` performed deletion without checking whether the target user was the currently logged-in account (`UserSession.CurrentUser`) or the only active manager (`RoleId == 1`).
- **Manager Employee Password Reset**:
  - In `Form\VanThanh\FrmPermission.cs` (lines 159-175), clicking "Đổi mật khẩu" opened `FrmChangePassword` which required `txtOldPassword` (the employee's current password). A manager could not reset an employee's forgotten password without knowing their old password.
- **FK Constraint Crash on Employee Deletion**:
  - In `DAL\EmployeeRepository.cs` (lines 161-174) and `Form\VanThanh\FrmEmployee.cs` (lines 186-206), `DeleteEmployee` executed `db.Employees.DeleteOnSubmit(existing)` and `db.SubmitChanges()` with no `try-catch`.
  - When deleting an employee with linked records in `Orders`, `EmployeeShifts`, or `InventoryTransactions`, an unhandled `SqlException` (REFERENCE constraint conflict) was thrown, crashing the application.
- **Data Validation Deficiencies**:
  - In `Form\TuanHung\FrmCategory.cs` (lines 84, 102), `Form\TuanHung\FrmProduct.cs` (lines 143, 173), and `Form\TuanHung\FrmPromotion.cs` (lines 99, 127), `int.Parse` was called on user input textboxes, throwing `FormatException` on empty or non-numeric strings.
  - In `Form\QuangHung\FrmCustomer.cs` (lines 60, 101) and `BLL\CustomerService.cs` (lines 44-91), negative reward points were not validated, violating DB CHECK constraints.
  - In `Form\VanThanh\FrmShiftSchedule.cs` (lines 19, 35-48, 216), employee assignment in the schedule grid was bound by full name string and resolved via `_empRepo.GetEmployeeByName(empName)`, causing collisions when employees shared the same name.

## 2. Logic Chain
1. **From Observation 1**: Generating cryptographically valid PBKDF2 hashes (16-byte random salt, 32-byte hash, 100k iterations) and updating `CoffeeShopDB_Full.sql` and `CoffeeShopDB.sql` ensures that database instances initialized from scripts contain matching PBKDF2 hash/salt values:
   - `admin` (pwd: `admin123`):
     - Salt: `MvzYIcst8H+HCiQecovlBA==`
     - PasswordHash: `4YUma6Q69CiQ97YVG17kFFFVD1HGqb2exAjnTpdQe0g=`
   - `thungan` (pwd: `123456`):
     - Salt: `RAq3VsoVEEY92GOgn+/wCA==`
     - PasswordHash: `ExGmL2DJ+2fEVbrgGAtbpkBl0c2eNsKncAH/rmQawuY=`
   - Reflection unit test verified `SecurityHelper.VerifyPassword("admin123", hash, salt) == true` and `SecurityHelper.VerifyPassword("123456", hash, salt) == true`.
2. **From Observation 2**:
   - In `BLL\PermissionService.cs`, updated `DeleteUser(string username, out string message)` with guards:
     - Checked `if (UserSession.CurrentUser != null && string.Equals(UserSession.CurrentUser.Username, username, StringComparison.OrdinalIgnoreCase))` to prevent self-deletion.
     - Checked if target is Manager (`RoleId == 1`) and active manager count `_userRepository.GetAllUsersWithRoles().Count(u => u.RoleId == 1 && (u.IsActive ?? true)) <= 1` to block deleting the last manager.
   - In `Form\VanThanh\FrmPermission.cs`, added client-side check against `UserSession.CurrentUser` and displayed service messages to the manager.
3. **From Observation 3**:
   - Added `AdminResetPassword(string username, string newPassword, out string message)` to `BLL\PermissionService.cs`, generating a fresh PBKDF2 salt/hash and calling `_userRepository.UpdatePassword(username, newHash, newSalt)`.
   - In `Form\VanThanh\FrmPermission.cs`, replaced `FrmChangePassword` call in `ExecuteChangePasswordAction` with `ShowAdminResetPasswordDialog(username)`, allowing the admin to set a new password and confirm it without prompting for the employee's old password.
4. **From Observation 4**:
   - In `DAL\EmployeeRepository.cs`, enhanced `DeleteEmployee(int employeeId, out string errorMessage)`:
     - Proactively checked `db.Orders.Any(...)`, `db.EmployeeShifts.Any(...)`, `db.InventoryTransactions.Any(...)`, `db.Users.Any(...)`.
     - Wrapped deletion in `try-catch (SqlException)` and `try-catch (Exception)`.
     - Returned clear message: `"Không thể xóa nhân viên đã có dữ liệu giao dịch hoặc ca làm việc liên kết!"`.
   - Propagated signature through `BLL\EmployeeService.cs` (`DeleteEmployee(int employeeId, out string message)`).
   - In `Form\VanThanh\FrmEmployee.cs`, handled the result and caught unexpected errors with user-friendly error alerts instead of crashing.
5. **From Observation 5**:
   - In `Form\TuanHung\FrmCategory.cs`, `Form\TuanHung\FrmProduct.cs`, `Form\TuanHung\FrmPromotion.cs`, replaced all 6 unsafe `int.Parse` calls with `int.TryParse`.
   - In `BLL\CustomerService.cs` (`Add` and `Update`) and `Form\QuangHung\FrmCustomer.cs` (`btnAdd_Click` and `btnEdit_Click`), enforced `RewardPoints >= 0`, rejecting negative values with descriptive validation warnings.
   - In `Form\VanThanh\FrmShiftSchedule.cs`, created `EmployeeScheduleItem` storing unique `int Id` and formatted `DisplayText` (`$"{emp.FullName} (#{emp.Id})"`), bound `ValueMember = "Id"` and `DisplayMember = "DisplayText"` across all shift ComboBox columns, and tracked assignments by `EmployeeId` directly.

## 3. Caveats
- Database connectivity in local testing relies on whatever local SQL Server instance is running; fallback offline modes in repositories are preserved and unaffected.
- Other milestones (M2 through M5) will address LINQ Lambda conversion across all repositories, avatar relative image storage, and third-party UI integration. Milestone 1 strictly maintained boundaries within its exclusive file write ownership list.

## 4. Conclusion
Milestone 1 (Security, Auth & Data Fixes) is 100% complete:
- SQL seed scripts updated with genuine PBKDF2 hash & salt.
- Manager self-deletion and last manager deletion prevented.
- Admin password reset for employees without old password prompt implemented.
- FK constraint crashes on employee deletion eliminated with graceful messaging.
- Robust validation applied across Category, Product, Promotion, Customer, and Shift Schedule forms.
- Project builds cleanly with **0 Errors and 0 Warnings** via MSBuild.

## 5. Verification Method
1. **Build Verification**:
   ```powershell
   & "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj" /t:Rebuild /p:Configuration=Debug /p:UseSharedCompilation=false
   ```
   *Expected Result*: `Build succeeded. 0 Warning(s), 0 Error(s)`.
2. **PBKDF2 Hash & Security Verification**:
   Run the reflection test script against `bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe`:
   - Admin password `admin123` validates against seed hash `4YUma6Q69CiQ97YVG17kFFFVD1HGqb2exAjnTpdQe0g=`.
   - Cashier password `123456` validates against seed hash `ExGmL2DJ+2fEVbrgGAtbpkBl0c2eNsKncAH/rmQawuY=`.
   - Negative customer reward points (-10) are rejected with `"Điểm tích lũy không được nhỏ hơn 0!"`.
   - `PermissionService.AdminResetPassword`, `PermissionService.DeleteUser(username, out message)`, and `EmployeeService.DeleteEmployee(id, out message)` are confirmed compiled and available.
3. **Invalidation Conditions**:
   - Reverting PBKDF2 hash strings in SQL files to plaintext.
   - Allowing `DeleteUser` to succeed when `username == UserSession.CurrentUser.Username`.
   - Calling `int.Parse` directly on empty text in Product/Category/Promotion forms.

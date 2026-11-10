# ADVERSARIAL & ROBUSTNESS REVIEW REPORT — MILESTONE 1

## Review Summary

**Verdict**: **APPROVE**  
**Integrity Assessment**: **NO INTEGRITY VIOLATION DETECTED**. No hardcoded expected test results, no dummy/facade implementations, no task-bypassing shortcuts, and no fabricated attestation artifacts were found. All implementations provide real, working logic.

---

## 1. Observation

Direct code observations from inspected files and execution results:

1. **Robustness of Security Guards (`PermissionService.cs`, `FrmPermission.cs`)**:
   - In `BLL\PermissionService.cs` (lines 149-155) and `Form\VanThanh\FrmPermission.cs` (lines 126-131):
     ```csharp
     if (UserSession.CurrentUser != null &&
         string.Equals(UserSession.CurrentUser.Username, username, StringComparison.OrdinalIgnoreCase))
     {
         message = "Không thể tự xóa tài khoản đang đăng nhập trong phiên làm việc hiện tại!";
         return false;
     }
     ```
     `UserSession.CurrentUser` is verified not-null before referencing `.Username`, and username comparison uses `StringComparison.OrdinalIgnoreCase`.
   - In `BLL\PermissionService.cs` (lines 142-148), `var targetUser = _userRepository.GetByUserName(username);` is invoked on the database *before* the `UserSession.CurrentUser` check is reached.
   - In `BLL\PermissionService.cs` (lines 158-168):
     ```csharp
     if (targetUser.RoleId == 1)
     {
         var allUsers = _userRepository.GetAllUsersWithRoles();
         int activeManagerCount = allUsers.Count(u => u.RoleId == 1 && (u.IsActive ?? true));
         if (activeManagerCount <= 1)
         {
             message = "Không thể xóa tài khoản Quản lý này vì đây là Quản lý đang hoạt động duy nhất trong hệ thống!";
             return false;
         }
     }
     ```
     When an inactive manager (`targetUser.RoleId == 1` and `targetUser.IsActive == false`) is targeted for deletion in a system with 1 active manager, `activeManagerCount` equals 1, blocking deletion and displaying an inaccurate message.
   - In `BLL\PermissionService.cs` (lines 194-209) and `Form\VanThanh\FrmPermission.cs` (lines 208-228), `AdminResetPassword` validates:
     - `string.IsNullOrWhiteSpace(newPassword)` -> rejects empty or whitespace passwords with `"Mật khẩu mới không được để trống!"`.
     - `newPassword.Length < 6` -> rejects passwords shorter than 6 characters with `"Mật khẩu mới phải có độ dài tối thiểu 6 ký tự!"`.
     - New salt and hash are generated via `SecurityHelper.GenerateSalt()` and `SecurityHelper.HashPassword()`.

2. **Robustness of FK Constraint Handling (`EmployeeRepository.cs`, `EmployeeService.cs`, `FrmEmployee.cs`)**:
   - In `DAL\EmployeeRepository.cs` (lines 175-197):
     ```csharp
     bool hasOrders = db.Orders.Any(o => o.EmployeeId == employeeId);
     bool hasShifts = db.EmployeeShifts.Any(s => s.EmployeeId == employeeId);
     bool hasInventory = db.InventoryTransactions.Any(it => it.EmployeeId == employeeId);
     bool hasUser = db.Users.Any(u => u.EmployeeId == employeeId);
     ```
     All 4 tables referencing `Employees (Id)` in `CoffeeShopDB_Full.sql` (`Orders`, `EmployeeShifts`, `InventoryTransactions`, `Users`) are proactively queried using pure LINQ Lambda `.Any()`.
   - The deletion is executed inside `using (var db = GetContext())` and wrapped in `try-catch (System.Data.SqlClient.SqlException)` and `try-catch (Exception ex)`.
   - On error, `errorMessage` is assigned and `false` is returned without rethrowing. The `using` statement guarantees the `DataContext` connection is disposed and any uncommitted transaction is rolled back cleanly.
   - In `BLL\EmployeeService.cs` (lines 99-109), `employeeId <= 0` is guarded, returning `"Mã nhân viên không hợp lệ!"`.
   - In `Form\VanThanh\FrmEmployee.cs` (lines 186-213), the result is consumed and displayed via `MessageBox.Show` with `MessageBoxIcon.Warning` / `MessageBoxIcon.Error` instead of crashing.

3. **Input Validation Completeness**:
   - Entire project scan for `int.Parse` yielded **0 occurrences**.
   - `Form\TuanHung\FrmCategory.cs` (lines 84, 108): 2 unsafe `int.Parse` calls were replaced with `int.TryParse`.
   - `Form\TuanHung\FrmProduct.cs` (lines 144, 183): 2 unsafe `int.Parse` calls were replaced with `int.TryParse`.
   - `Form\TuanHung\FrmPromotion.cs` (lines 100, 137): 2 unsafe `int.Parse` calls were replaced with `int.TryParse`.
   - `Form\QuangHung\FrmCustomer.cs` (lines 63, 113) and `BLL\CustomerService.cs` (lines 65, 96): Enforced `RewardPoints >= 0` in both Form input handling and Service layer.
   - `Form\VanThanh\FrmShiftSchedule.cs`: Replaced string matching `GetEmployeeByName` with `EmployeeScheduleItem` (binding by `int Id`), using `int.TryParse` on ComboBox cell values, and added `dgvSchedule.DataError += (s, ev) => { ev.Cancel = true; };` to eliminate runtime grid crashes.

4. **Architecture Conformance**:
   - DAL -> BLL -> Form boundaries are maintained.
   - No direct DataContext access was found in the Form layer.

5. **Build Verification**:
   - Executing MSBuild Rebuild command:
     `& "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj" /t:Rebuild /p:Configuration=Debug /p:UseSharedCompilation=false`
     Output: `Build succeeded. 0 Warning(s), 0 Error(s)`. Time elapsed: `00:00:02.43`.

---

## 2. Logic Chain

1. **Security Guards**:
   - `string.Equals(UserSession.CurrentUser.Username, username, StringComparison.OrdinalIgnoreCase)` guarantees case-insensitive self-deletion prevention (`admin`, `Admin`, `ADMIN` are all blocked).
   - Explicit check `UserSession.CurrentUser != null` ensures no `NullReferenceException` is thrown when unauthenticated.
   - `string.IsNullOrWhiteSpace(newPassword)` evaluates `true` for empty string, string of spaces, or `null`, ensuring no blank or whitespace passwords can be stored via `AdminResetPassword`.
   - Independent reflection test (`verify_security_logic.ps1`) verified:
     - Empty username: rejected (`{"Success":false,"Message":"Tên đăng nhập không được để trống!"}`).
     - Whitespace username: rejected.
     - Empty password: rejected (`{"Success":false,"Message":"Mật khẩu mới không được để trống!"}`).
     - Whitespace password: rejected (`{"Success":false,"Message":"Mật khẩu mới không được để trống!"}`).
     - Passwords < 6 chars: rejected (`{"Success":false,"Message":"Mật khẩu mới phải có độ dài tối thiểu 6 ký tự!"}`).

2. **FK Handling**:
   - Proactive inspection of `Orders`, `EmployeeShifts`, `InventoryTransactions`, and `Users` prevents SQL exceptions from firing under normal operations.
   - The outer `try-catch (SqlException)` catches concurrency race conditions (e.g., order created between check and delete) without crashing the application.
   - Scoped `using (var db = GetContext())` ensures database connections are automatically closed and uncommitted transactions are cleanly terminated.

3. **Data Validation**:
   - Scanning the entire codebase confirmed that all legacy `int.Parse` calls have been eradicated in favor of `int.TryParse`.
   - Validation in both UI and BLL layers for customer reward points (`RewardPoints >= 0`) establishes defense-in-depth against negative database constraint violations.
   - Shift scheduling bound by `EmployeeId` rather than `FullName` completely prevents accidental overwrites or collisions when two employees share the same name.

4. **Cryptographic Integrity**:
   - In `CoffeeShopDB_Full.sql` and `CoffeeShopDB.sql`, the stored PBKDF2 hashes for `admin` (`admin123`) and `thungan` (`123456`) were verified using the compiled `SecurityHelper.VerifyPassword` method in `verify_hashes.ps1`:
     - `admin123` -> `True`
     - `123456` -> `True`
     - `wrongpass` -> `False`
   - No mock or facade logic is present.

---

## 3. Findings

### [Minor] Finding 1: Sub-optimal Guard Ordering in `PermissionService.DeleteUser`
- **What**: In `BLL\PermissionService.cs`, database lookup `_userRepository.GetByUserName(username)` is called at line 142 *before* evaluating the in-memory self-deletion guard `UserSession.CurrentUser` at line 150.
- **Where**: `BLL\PermissionService.cs`, lines 140-155.
- **Why**: If a logged-in user attempts to delete their own account, the method queries the database unnecessarily. If the database connection is offline or experiencing latency, this will throw an exception instead of immediately returning a fail-fast message based on session state.
- **Suggestion**: Move the `UserSession.CurrentUser` self-deletion check above the database query `_userRepository.GetByUserName(username)`.

### [Minor] Finding 2: Inactive Manager Deletion Block Logic
- **What**: When deleting a user with `RoleId == 1`, `PermissionService.DeleteUser` checks `if (activeManagerCount <= 1)` regardless of whether the target user is currently active or inactive.
- **Where**: `BLL\PermissionService.cs`, lines 158-168.
- **Why**: If there is 1 active manager and 1 inactive manager, an administrator attempting to delete the inactive manager account will be blocked with the message `"Không thể xóa tài khoản Quản lý này vì đây là Quản lý đang hoạt động duy nhất trong hệ thống!"`, which is logically inaccurate because the target account is inactive.
- **Suggestion**: Check `if ((targetUser.IsActive ?? true) && activeManagerCount <= 1)` so that inactive manager accounts can be deleted without hindrance.

---

## 4. Verified Claims

| Claim | Verification Method | Result |
|---|---|---|
| PBKDF2 hashes in SQL scripts match `SecurityHelper` | Independent reflection invocation via `verify_hashes.ps1` | **PASS** |
| Manager cannot delete self (case-insensitive) | Inspected `PermissionService.cs` & `FrmPermission.cs` using `OrdinalIgnoreCase` | **PASS** |
| Admin cannot reset employee password with empty/whitespace/<6 chars | Independent test via `verify_security_logic.ps1` | **PASS** |
| Proactive FK constraint validation in `DeleteEmployee` | Code inspection of all 4 referencing tables in `EmployeeRepository.cs` | **PASS** |
| No unhandled `SqlException` or dangling transactions in `DeleteEmployee` | Checked `using` block and `try-catch` return pattern | **PASS** |
| No unsafe `int.Parse` in `FrmCategory`, `FrmProduct`, `FrmPromotion` | PowerShell AST / regex project-wide scan | **PASS** (0 occurrences) |
| Negative customer reward points blocked in UI and BLL | Inspected `FrmCustomer.cs` and `CustomerService.cs` | **PASS** |
| Shift schedule bound by unique Employee ID | Inspected `FrmShiftSchedule.cs` `EmployeeScheduleItem` | **PASS** |
| MSBuild clean compilation | Executed `MSBuild.exe` Rebuild on `.csproj` | **PASS** (0 Warnings, 0 Errors) |

---

## 5. Adversarial Challenge & Stress Test Report

### Overall Risk Assessment: LOW

### Challenge 1: Self-deletion attempt with case variations
- **Assumption challenged**: Usernames might differ in case (`admin` vs `ADMIN`).
- **Attack scenario**: Logged in as `admin`, invoking deletion on `ADMIN`.
- **Result**: Handled correctly via `StringComparison.OrdinalIgnoreCase`. Both GUI and Service layers reject the attempt. **PASS**.

### Challenge 2: Null UserSession invocation
- **Assumption challenged**: Calling `DeleteUser` when no user is logged in (`UserSession.CurrentUser == null`).
- **Attack scenario**: Direct service invocation in disconnected/batch mode.
- **Result**: Explicit `UserSession.CurrentUser != null` check avoids `NullReferenceException`. Target user is deleted or checked against manager rules. **PASS**.

### Challenge 3: Reset password with Unicode whitespace or control characters
- **Assumption challenged**: Passwords consisting of tabs, multiple spaces, or newlines.
- **Attack scenario**: Submitting `"      "` or `"\t\t\t\t\t\t"`.
- **Result**: Caught by `string.IsNullOrWhiteSpace()`, rejecting input before hashing. **PASS**.

### Challenge 4: Concurrent employee deletion during transaction activity
- **Assumption challenged**: An order is placed by employee between the proactive check and `SubmitChanges()`.
- **Attack scenario**: Race condition between order creation and deletion.
- **Result**: Proactive check misses race condition, but `try-catch (SqlException)` catches the foreign key violation, returning `false` with a descriptive error message without crashing. **PASS**.

---

## 6. Caveats

- Live UI interaction with actual Windows Forms controls was not tested via automation; review was conducted via static code analysis, reflection unit testing, and compilation verification.
- Runtime database tests relied on local environment data structures and offline fallback models.

---

## 7. Conclusion

Milestone 1 successfully delivers all required security, authentication, and data integrity fixes.
- Zero integrity violations were detected.
- All acceptance criteria in `SCOPE.md` and `ORIGINAL_REQUEST.md` for Milestone 1 are satisfied.
- The two minor findings regarding guard ordering and inactive manager deletion are non-blocking advisory suggestions for future refactoring.
- **Final Verdict: APPROVE**.

---

## 8. Verification Method

To independently reproduce and verify this review:

1. **Rebuild Solution**:
   ```powershell
   & "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj" /t:Rebuild /p:Configuration=Debug /p:UseSharedCompilation=false
   ```
2. **Verify PBKDF2 Hash and Salt**:
   ```powershell
   powershell -ExecutionPolicy Bypass -File "d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\reviewer_m1_2\verify_hashes.ps1"
   ```
3. **Verify Password Reset Boundary Conditions**:
   ```powershell
   powershell -ExecutionPolicy Bypass -File "d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\reviewer_m1_2\verify_security_logic.ps1"
   ```
4. **Confirm Zero `int.Parse` Across Project**:
   ```powershell
   powershell -Command "Get-ChildItem -Recurse -Filter '*.cs' | Select-String -Pattern '\bint\.Parse\b'"
   ```

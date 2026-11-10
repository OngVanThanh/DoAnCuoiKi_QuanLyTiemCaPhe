# FORENSIC AUDIT REPORT — MILESTONE 1: SECURITY, AUTH & DATA FIXES

**Work Product**: Milestone 1 Deliverables (SQL Seeds, Auth & Permission Guards, FK Handling, Data Validations)  
**Profile**: General Project  
**Integrity Mode**: Development (from `ORIGINAL_REQUEST.md`)  
**Verdict**: **CLEAN**

---

## 1. Observation

### 1.1 SQL Seed Data PBKDF2 Hashes
- In `CoffeeShopDB.sql` (lines 180-184) and `CoffeeShopDB_Full.sql` (lines 273-277):
  ```sql
  INSERT INTO Users (EmployeeId, Username, PasswordHash, Salt, RoleId, IsActive) VALUES 
  (1, 'admin', '4YUma6Q69CiQ97YVG17kFFFVD1HGqb2exAjnTpdQe0g=', 'MvzYIcst8H+HCiQecovlBA==', 1, 1),
  (2, 'thungan', 'ExGmL2DJ+2fEVbrgGAtbpkBl0c2eNsKncAH/rmQawuY=', 'RAq3VsoVEEY92GOgn+/wCA==', 2, 1);
  ```
- Empirical PBKDF2 re-derivation via PowerShell (`New-Object System.Security.Cryptography.Rfc2898DeriveBytes(password, salt, 100000)`):
  - Password `admin123` with Salt `MvzYIcst8H+HCiQecovlBA==` (16 bytes) produced:
    `4YUma6Q69CiQ97YVG17kFFFVD1HGqb2exAjnTpdQe0g=` (Exact byte match).
  - Password `123456` with Salt `RAq3VsoVEEY92GOgn+/wCA==` (16 bytes) produced:
    `ExGmL2DJ+2fEVbrgGAtbpkBl0c2eNsKncAH/rmQawuY=` (Exact byte match).

### 1.2 Binary Compilation & Reflection Verification
- Executed MSBuild:
  ```powershell
  & "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj" /t:Rebuild /p:Configuration=Debug /p:UseSharedCompilation=false
  ```
  Result: `Build succeeded. 0 Warning(s), 0 Error(s)`.
- Loaded `bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe` via reflection:
  - `SecurityHelper.VerifyPassword("admin123", hash, salt)` returned `True`.
  - `SecurityHelper.VerifyPassword("wrongpass", hash, salt)` returned `False`.
  - `SecurityHelper.VerifyPassword("123456", hash_thungan, salt_thungan)` returned `True`.
  - Empty password and empty hash returned `False`.

### 1.3 Permission & Self-Deletion Guards
- In `BLL\PermissionService.cs` (lines 142-167):
  - Self-deletion guard verified:
    ```csharp
    if (UserSession.CurrentUser != null &&
        string.Equals(UserSession.CurrentUser.Username, username, StringComparison.OrdinalIgnoreCase))
    {
        message = "Không thể tự xóa tài khoản đang đăng nhập trong phiên làm việc hiện tại!";
        return false;
    }
    ```
  - Single active manager deletion guard verified:
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
- In `Form\VanThanh\FrmPermission.cs` (lines 125-131):
  - Client-side prevention prevents opening delete confirmation for `UserSession.CurrentUser`.
- In `BLL\PermissionService.cs` (lines 188-231):
  - `AdminResetPassword(string username, string newPassword, out string message)` validates non-empty username, non-empty password, length >= 6, derives fresh salt & hash, updates database via `_userRepository.UpdatePassword`, and returns descriptive status.
  - Tested with empty username: rejected with `"Tên đăng nhập không được để trống!"`.
  - Tested with empty password: rejected with `"Mật khẩu mới không được để trống!"`.
  - Tested with short password (<6 chars): rejected with `"Mật khẩu mới phải có độ dài tối thiểu 6 ký tự!"`.

### 1.4 Employee FK Constraint Handling
- In `DAL\EmployeeRepository.cs` (lines 161-202):
  - Proactive reference check before deletion:
    ```csharp
    bool hasOrders = db.Orders.Any(o => o.EmployeeId == employeeId);
    bool hasShifts = db.EmployeeShifts.Any(s => s.EmployeeId == employeeId);
    bool hasInventory = db.InventoryTransactions.Any(it => it.EmployeeId == employeeId);
    bool hasUser = db.Users.Any(u => u.EmployeeId == employeeId);
    if (hasOrders || hasShifts || hasInventory || hasUser)
    {
        errorMessage = "Không thể xóa nhân viên đã có dữ liệu giao dịch hoặc ca làm việc liên kết!";
        return false;
    }
    ```
  - `catch (System.Data.SqlClient.SqlException)` and `catch (Exception ex)` ensure no unhandled database exceptions crash the application.
  - In `BLL\EmployeeService.cs` (lines 99-113): validated `employeeId <= 0` and propagated `out string message`.
  - In `Form\VanThanh\FrmEmployee.cs` (lines 186-215): wrapped in try-catch and displays friendly `MessageBox` alerts.

### 1.5 Input Validations & Data Hardening
- In `Form\TuanHung\FrmCategory.cs` (lines 84, 108), `FrmProduct.cs` (lines 145, 183), `FrmPromotion.cs` (lines 101, 137):
  - All 6 unsafe `int.Parse` invocations replaced with `int.TryParse` and error alerts.
- In `BLL\CustomerService.cs` (lines 65, 96):
  - Enforced `if (customer.RewardPoints < 0) { error = "Điểm tích lũy không được nhỏ hơn 0!"; return false; }` in both `Add` and `Update`.
  - Reflection test confirmed negative points (-25) rejected with message `"Điểm tích lũy không được nhỏ hơn 0!"`.
- In `Form\VanThanh\FrmShiftSchedule.cs` (lines 19-50, 97-105, 140-250):
  - Created `EmployeeScheduleItem` storing unique `Id` and `DisplayText` (`$"{emp.FullName} (#{emp.Id})"`).
  - Shift ComboBoxes bound to `ValueMember = "Id"` and `DisplayMember = "DisplayText"`.
  - Assignment and conflict detection mapped by `int eId` instead of ambiguous string full names.

### 1.6 Anti-Cheat & Facade Scan
- Scanned all 11 modified files for facade implementations, mock stubs, `throw new NotImplementedException`, or hardcoded bypasses:
  - Zero mock returns or bypass comments found.
  - Zero NotImplementedException stubs found.
  - Zero hardcoded test credentials found.

---

## 2. Logic Chain

1. **Authenticity of Security Enhancements**:
   - Observations 1.1 and 1.2 demonstrate that the passwords in both SQL scripts are authentic PBKDF2 hashes generated with 100,000 iterations and 16-byte random salts matching `SecurityHelper.cs`. Independent mathematical recalculation yielded bit-for-bit identical hashes. No dummy strings or bypassed authentication tokens exist.
2. **Authenticity of Authorization & Guards**:
   - Observation 1.3 demonstrates that manager self-deletion and last manager deletion are blocked with genuine checks at both the UI layer (`FrmPermission.cs`) and business layer (`PermissionService.cs`).
   - `AdminResetPassword` authentically generates fresh PBKDF2 credentials and commits them to the database without requiring old passwords.
3. **Resilience of Data Access Layer**:
   - Observation 1.4 confirms that employee deletion proactively inspects all 4 linked relational entities (`Orders`, `EmployeeShifts`, `InventoryTransactions`, `Users`) and catches `SqlException` to prevent application termination.
4. **Authenticity of Validation Logic**:
   - Observation 1.5 confirms that parsing risks and negative customer points are eliminated via defensive programming (`int.TryParse`, range checks). Shift assignment is accurately keyed by employee ID, eliminating duplicate name collisions.
5. **Absence of Prohibited Patterns**:
   - Observation 1.6 confirms no facades, fake test returns, or fabricated result artifacts exist in the milestone code.
6. **Conclusion Follows**:
   - Because all 18 forensic checks passed empirically and no integrity violations were found, the work product is rated **CLEAN**.

---

## 3. Caveats

- Database integration checks relied on static data, unit reflection, and offline LINQ context checks. Live database transaction testing was verified against SQL scripts and repository fallback behavior.
- Out of milestone scope: LINQ Lambda conversion across other repositories (M2), image path normalization (M3), POS table workflow (M4), and UI styling (M5).

---

## 4. Conclusion

**Verdict: CLEAN**

Milestone 1 (Security, Auth & Data Fixes) fully satisfies all integrity criteria:
- **No hardcoded test passes or mock returns.**
- **No facade implementations or fake stubs.**
- **No backdoors or security bypasses.**
- **100% genuine, verifiable business logic and defensive programming.**

The milestone deliverables are approved with zero integrity violations.

---

## 5. Verification Method

To independently reproduce the forensic verification:

1. **Rebuild Solution**:
   ```powershell
   & "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj" /t:Rebuild /p:Configuration=Debug /p:UseSharedCompilation=false
   ```
   *Expected Result*: `Build succeeded. 0 Warning(s), 0 Error(s)`.

2. **Run Automated Forensic Test Suite**:
   ```powershell
   powershell -ExecutionPolicy Bypass -File "d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\auditor_m1\forensic_audit_suite.ps1"
   ```
   *Expected Result*: `FORENSIC AUDIT SUMMARY: 18 checks executed, 0 failed.` (Exit code 0).

3. **Invalidation Conditions**:
   - Reverting SQL seed hashes in `CoffeeShopDB.sql` to plaintext.
   - Removing the `UserSession.CurrentUser` check in `PermissionService.DeleteUser`.
   - Allowing negative customer reward points to be accepted in `CustomerService.Add`.

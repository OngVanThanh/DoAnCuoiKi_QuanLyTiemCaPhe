# ==============================================================================
# CHALLENGER 1 EMPIRICAL STRESS TEST HARNESS — MILESTONE 1
# Adversarial testing of Authentication, Security, and Permission Fixes
# ==============================================================================

param(
    [string]$ProjectRoot = "d:\DoAnCuoiKi_QuanLyTiemCaPhe"
)

$ErrorActionPreference = "Continue"

Write-Host "==========================================================================" -ForegroundColor Cyan
Write-Host "         CHALLENGER 1: ADVERSARIAL STRESS TEST FOR MILESTONE 1            " -ForegroundColor Yellow
Write-Host "==========================================================================" -ForegroundColor Cyan

$testResults = [System.Collections.Generic.List[PSObject]]::new()

function Assert-Check {
    param(
        [string]$Category,
        [string]$TestName,
        [bool]$Passed,
        [string]$Expected,
        [string]$Actual,
        [string]$Details = ""
    )

    $obj = [PSCustomObject]@{
        Category = $Category
        TestName = $TestName
        Passed   = $Passed
        Expected = $Expected
        Actual   = $Actual
        Details  = $Details
    }
    $testResults.Add($obj)

    $badge = if ($Passed) { "[PASS]" } else { "[FAIL]" }
    $color = if ($Passed) { "Green" } else { "Red" }
    Write-Host "  $badge " -ForegroundColor $color -NoNewline
    Write-Host "[$Category] $TestName" -ForegroundColor White
    if (-not $Passed) {
        Write-Host "         Expected: $Expected" -ForegroundColor DarkCyan
        Write-Host "         Actual:   $Actual" -ForegroundColor Magenta
        if ($Details) {
            Write-Host "         Details:  $Details" -ForegroundColor DarkRed
        }
    }
}

# --- Load Assembly ---
$binPath = Join-Path $ProjectRoot "bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe"
if (-not (Test-Path $binPath)) {
    Write-Host "[FATAL] Assembly not found at $binPath" -ForegroundColor Red
    exit 1
}

[System.Reflection.Assembly]::LoadWithPartialName("System.Drawing") | Out-Null
[System.Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms") | Out-Null
[System.Reflection.Assembly]::LoadWithPartialName("System.Configuration") | Out-Null
[System.Reflection.Assembly]::LoadWithPartialName("System.Data.Linq") | Out-Null

$asm = [System.Reflection.Assembly]::LoadFrom($binPath)
$secType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.SecurityHelper")
$userType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.User")
$userSessionType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Utilities.UserSession")
$permServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.PermissionService")
$empServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.EmployeeService")
$custServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.CustomerService")
$custDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.CustomerDTO")

Write-Host "`n>>> [SECTION 1] SQL SEED DATA EXTRACTION & HASH VERIFICATION <<<" -ForegroundColor Cyan

function Extract-SeedUsersFromSql {
    param([string]$FilePath)
    $content = Get-Content -Raw -Path $FilePath
    # Look for INSERT INTO Users ... VALUES ...
    $pattern = "INSERT\s+INTO\s+Users\s*\([^)]*\)\s*VALUES\s*([^;]+);"
    $match = [regex]::Match($content, $pattern, [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
    $users = @{}
    if ($match.Success) {
        $valuesBlock = $match.Groups[1].Value
        # Match each row: (EmployeeId, 'Username', 'PasswordHash', 'Salt', RoleId, IsActive)
        $rowPattern = "\(\s*(\d+)\s*,\s*'([^']+)'\s*,\s*'([^']+)'\s*,\s*'([^']*)'\s*,\s*(\d+)\s*,\s*(\d+)\s*\)"
        $rowMatches = [regex]::Matches($valuesBlock, $rowPattern)
        foreach ($rm in $rowMatches) {
            $uName = $rm.Groups[2].Value
            $pHash = $rm.Groups[3].Value
            $salt = $rm.Groups[4].Value
            $users[$uName] = @{
                Username     = $uName
                PasswordHash = $pHash
                Salt         = $salt
                RoleId       = [int]$rm.Groups[5].Value
            }
        }
    }
    return $users
}

$fullSqlPath = Join-Path $ProjectRoot "CoffeeShopDB_Full.sql"
$dbSqlPath = Join-Path $ProjectRoot "CoffeeShopDB.sql"

$seedFull = Extract-SeedUsersFromSql $fullSqlPath
$seedDb = Extract-SeedUsersFromSql $dbSqlPath

Assert-Check "SQL_Seed" "CoffeeShopDB_Full.sql contains seed user 'admin'" ($seedFull.ContainsKey("admin")) "True" "$($seedFull.ContainsKey('admin'))"
Assert-Check "SQL_Seed" "CoffeeShopDB_Full.sql contains seed user 'thungan'" ($seedFull.ContainsKey("thungan")) "True" "$($seedFull.ContainsKey('thungan'))"
Assert-Check "SQL_Seed" "CoffeeShopDB.sql contains seed user 'admin'" ($seedDb.ContainsKey("admin")) "True" "$($seedDb.ContainsKey('admin'))"
Assert-Check "SQL_Seed" "CoffeeShopDB.sql contains seed user 'thungan'" ($seedDb.ContainsKey("thungan")) "True" "$($seedDb.ContainsKey('thungan'))"

# Verify exact match between both SQL scripts
if ($seedFull.ContainsKey("admin") -and $seedDb.ContainsKey("admin")) {
    $matchAdminHash = ($seedFull["admin"].PasswordHash -eq $seedDb["admin"].PasswordHash)
    $matchAdminSalt = ($seedFull["admin"].Salt -eq $seedDb["admin"].Salt)
    Assert-Check "SQL_Seed" "Admin seed Hash identical in Full and DB sql scripts" $matchAdminHash "True" "$matchAdminHash"
    Assert-Check "SQL_Seed" "Admin seed Salt identical in Full and DB sql scripts" $matchAdminSalt "True" "$matchAdminSalt"
}
if ($seedFull.ContainsKey("thungan") -and $seedDb.ContainsKey("thungan")) {
    $matchTnHash = ($seedFull["thungan"].PasswordHash -eq $seedDb["thungan"].PasswordHash)
    $matchTnSalt = ($seedFull["thungan"].Salt -eq $seedDb["thungan"].Salt)
    Assert-Check "SQL_Seed" "Thungan seed Hash identical in Full and DB sql scripts" $matchTnHash "True" "$matchTnHash"
    Assert-Check "SQL_Seed" "Thungan seed Salt identical in Full and DB sql scripts" $matchTnSalt "True" "$matchTnSalt"
}

# Empirical PBKDF2 verification against seed data
# Admin: admin123
$adminHash = $seedFull["admin"].PasswordHash
$adminSalt = $seedFull["admin"].Salt
$verifyAdminValid = [bool]$secType.GetMethod("VerifyPassword").Invoke($null, @("admin123", $adminHash, $adminSalt))
Assert-Check "PBKDF2_Seed" "VerifyPassword('admin123', adminHash, adminSalt) returns True" $verifyAdminValid "True" "$verifyAdminValid"

$verifyAdminWrong = [bool]$secType.GetMethod("VerifyPassword").Invoke($null, @("wrongpass", $adminHash, $adminSalt))
Assert-Check "PBKDF2_Seed" "VerifyPassword('wrongpass', adminHash, adminSalt) returns False" (-not $verifyAdminWrong) "False" "$verifyAdminWrong"

$verifyAdminCase = [bool]$secType.GetMethod("VerifyPassword").Invoke($null, @("Admin123", $adminHash, $adminSalt))
Assert-Check "PBKDF2_Seed" "VerifyPassword('Admin123' [case diff], adminHash, adminSalt) returns False" (-not $verifyAdminCase) "False" "$verifyAdminCase"

# Thungan: 123456
$tnHash = $seedFull["thungan"].PasswordHash
$tnSalt = $seedFull["thungan"].Salt
$verifyTnValid = [bool]$secType.GetMethod("VerifyPassword").Invoke($null, @("123456", $tnHash, $tnSalt))
Assert-Check "PBKDF2_Seed" "VerifyPassword('123456', tnHash, tnSalt) returns True" $verifyTnValid "True" "$verifyTnValid"

$verifyTnWrong = [bool]$secType.GetMethod("VerifyPassword").Invoke($null, @("wrongpass", $tnHash, $tnSalt))
Assert-Check "PBKDF2_Seed" "VerifyPassword('wrongpass', tnHash, tnSalt) returns False" (-not $verifyTnWrong) "False" "$verifyTnWrong"

# Edge inputs against seed credentials
$verifyEmpty = [bool]$secType.GetMethod("VerifyPassword").Invoke($null, @("", $adminHash, $adminSalt))
Assert-Check "PBKDF2_Edge" "VerifyPassword with empty password returns False" (-not $verifyEmpty) "False" "$verifyEmpty"

$verifyNull = [bool]$secType.GetMethod("VerifyPassword").Invoke($null, @($null, $adminHash, $adminSalt))
Assert-Check "PBKDF2_Edge" "VerifyPassword with null password returns False" (-not $verifyNull) "False" "$verifyNull"

$verifyNullHash = [bool]$secType.GetMethod("VerifyPassword").Invoke($null, @("admin123", $null, $adminSalt))
Assert-Check "PBKDF2_Edge" "VerifyPassword with null hash returns False" (-not $verifyNullHash) "False" "$verifyNullHash"

$verifyNullSalt = [bool]$secType.GetMethod("VerifyPassword").Invoke($null, @("admin123", $adminHash, $null))
Assert-Check "PBKDF2_Edge" "VerifyPassword with null salt returns False" (-not $verifyNullSalt) "False" "$verifyNullSalt"

# Altered salt
$alteredSaltBytes = [Convert]::FromBase64String($adminSalt)
$alteredSaltBytes[0] = $alteredSaltBytes[0] -bxor 0xAA
$alteredSalt = [Convert]::ToBase64String($alteredSaltBytes)
$verifyAlteredSalt = [bool]$secType.GetMethod("VerifyPassword").Invoke($null, @("admin123", $adminHash, $alteredSalt))
Assert-Check "PBKDF2_Edge" "VerifyPassword with altered salt returns False" (-not $verifyAlteredSalt) "False" "$verifyAlteredSalt"

# Altered hash
$alteredHashBytes = [Convert]::FromBase64String($adminHash)
$alteredHashBytes[0] = $alteredHashBytes[0] -bxor 0x55
$alteredHash = [Convert]::ToBase64String($alteredHashBytes)
$verifyAlteredHash = [bool]$secType.GetMethod("VerifyPassword").Invoke($null, @("admin123", $alteredHash, $adminSalt))
Assert-Check "PBKDF2_Edge" "VerifyPassword with altered hash returns False" (-not $verifyAlteredHash) "False" "$verifyAlteredHash"


Write-Host "`n>>> [SECTION 2] EXTENSIVE FUZZING & EXCEPTION LEAK TESTS <<<" -ForegroundColor Cyan

$fuzzPasswords = @(
    "",
    " ",
    "   ",
    "`t`r`n",
    "a",
    "1234567890",
    ("x" * 1000),      # 1,000 chars
    ("y" * 10000),     # 10,000 chars
    "Cà phê phin Sài Gòn - 2026! ☕ 🍰",
    "Tài khoản quản lý & phân quyền: <script>alert('XSS')</script>",
    "admin' OR '1'='1' --",
    "'; DROP TABLE Users; --",
    "password`0with_null_byte",
    "パスワード日本語",
    "ПАСПОРТ_РОССИЯ"
)

foreach ($pw in $fuzzPasswords) {
    $displayName = if ($pw.Length -gt 25) { $pw.Substring(0, 22) + "..." } else { $pw }
    $displayName = $displayName.Replace("`n", "\n").Replace("`r", "\r").Replace("`t", "\t").Replace("`0", "\0")
    $threw = $false
    $ret = $null
    try {
        $ret = $secType.GetMethod("VerifyPassword").Invoke($null, @($pw, $adminHash, $adminSalt))
    } catch {
        $threw = $true
        $ret = $_.Exception.ToString()
    }
    Assert-Check "Fuzz_Password" "VerifyPassword handles input: '$displayName' without crash" (-not $threw) "No Exception" "$ret"
}

# Malformed salts / hashes
$malformedInputs = @(
    "not_base64!@#$%",
    "====",
    "A",
    "AA==",
    "AAAA",
    "AAAAAA==",
    "AAAAAAAAAAAAAAAAAAAAAA==", # 16 bytes base64
    "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=" # 32 bytes base64
)

foreach ($mf in $malformedInputs) {
    $threw = $false
    $ret = $null
    try {
        $ret = $secType.GetMethod("VerifyPassword").Invoke($null, @("admin123", $adminHash, $mf))
    } catch {
        $threw = $true
        $ret = $_.Exception.ToString()
    }
    Assert-Check "Fuzz_Salt" "VerifyPassword safely handles malformed salt '$mf'" (-not $threw) "No Exception (returns false)" "$ret"
}

foreach ($mf in $malformedInputs) {
    $threw = $false
    $ret = $null
    try {
        $ret = $secType.GetMethod("VerifyPassword").Invoke($null, @("admin123", $mf, $adminSalt))
    } catch {
        $threw = $true
        $ret = $_.Exception.ToString()
    }
    Assert-Check "Fuzz_Hash" "VerifyPassword safely handles malformed hash '$mf'" (-not $threw) "No Exception (returns false)" "$ret"
}


Write-Host "`n>>> [SECTION 3] PERMISSION SERVICE ADVERSARIAL STRESS TESTS <<<" -ForegroundColor Cyan

$permService = [Activator]::CreateInstance($permServiceType)

# Find DeleteUser(string, out string) method safely
$delMethod = $permServiceType.GetMethods() | Where-Object { $_.Name -eq "DeleteUser" -and $_.GetParameters().Length -eq 2 } | Select-Object -First 1
Assert-Check "Permission_Guard" "DeleteUser(string, out string) method exists" ($delMethod -ne $null) "NotNull" "$delMethod"

# 3.1 Self-Deletion Guard Tests
# Mock UserSession.CurrentUser = admin
$adminUserObj = [Activator]::CreateInstance($userType)
$adminUserObj.Id = 1
$adminUserObj.Username = "admin"
$adminUserObj.RoleId = 1
$adminUserObj.IsActive = $true

$userSessionType.GetMethod("Login").Invoke($null, @($adminUserObj))

# Attempt 1: Delete exact matching username
$paramArray = [object[]]@("admin", "")
$delResult = $delMethod.Invoke($permService, $paramArray)
$delMsg = $paramArray[1]

Assert-Check "Permission_Guard" "Self-deletion blocked for exact username 'admin'" (-not $delResult) "False" "$delResult"
Assert-Check "Permission_Guard" "Self-deletion error message mentions session/self-deletion" ($delMsg -like "*tự xóa*") "Contains 'tự xóa'" "$delMsg"

# Attempt 2: Delete uppercase matching username 'ADMIN'
$paramArrayUpper = [object[]]@("ADMIN", "")
$delResultUpper = $delMethod.Invoke($permService, $paramArrayUpper)
$delMsgUpper = $paramArrayUpper[1]
Assert-Check "Permission_Guard" "Self-deletion blocked for case-variant 'ADMIN'" (-not $delResultUpper) "False" "$delResultUpper"
Assert-Check "Permission_Guard" "Self-deletion error message for case-variant mentions self-deletion" ($delMsgUpper -like "*tự xóa*") "Contains 'tự xóa'" "$delMsgUpper"

# Attempt 3: Delete with whitespace '  admin  '
$paramArraySpace = [object[]]@("  admin  ", "")
$delResultSpace = $delMethod.Invoke($permService, $paramArraySpace)
$delMsgSpace = $paramArraySpace[1]
Assert-Check "Permission_Guard" "Self-deletion blocked for whitespace-padded '  admin  '" (-not $delResultSpace) "False" "$delResultSpace"
Assert-Check "Permission_Guard" "Self-deletion error message for whitespace padded mentions self-deletion" ($delMsgSpace -like "*tự xóa*") "Contains 'tự xóa'" "$delMsgSpace"

# Logout and check behavior when CurrentUser is null
$userSessionType.GetMethod("Logout").Invoke($null, @())
$threwOnNullSession = $false
$delMsgNullSess = ""
try {
    $paramArrayNullSess = [object[]]@("nonexistent_user_abcxyz", "")
    $delResNullSess = $delMethod.Invoke($permService, $paramArrayNullSess)
    $delMsgNullSess = $paramArrayNullSess[1]
} catch {
    $threwOnNullSession = $true
    $delMsgNullSess = $_.Exception.ToString()
}
Assert-Check "Permission_Guard" "DeleteUser safely handles null UserSession.CurrentUser without NullReferenceException" (-not $threwOnNullSession) "No Exception" "Handled: $delMsgNullSess"

# Attempt 4: Delete non-existent user
$paramArrayNonExistent = [object[]]@("ghost_user_9999", "")
$delResultGhost = $delMethod.Invoke($permService, $paramArrayNonExistent)
$delMsgGhost = $paramArrayNonExistent[1]
Assert-Check "Permission_Guard" "DeleteUser rejects non-existent user" (-not $delResultGhost) "False" "$delResultGhost"
Assert-Check "Permission_Guard" "DeleteUser message states user not found" ($delMsgGhost -like "*Không tìm thấy*") "Contains 'Không tìm thấy'" "$delMsgGhost"

# Attempt 5: Delete with empty string / null username
$paramArrayEmptyUser = [object[]]@("", "")
$delResultEmpty = $delMethod.Invoke($permService, $paramArrayEmptyUser)
$delMsgEmpty = $paramArrayEmptyUser[1]
Assert-Check "Permission_Guard" "DeleteUser rejects empty username" (-not $delResultEmpty) "False" "$delResultEmpty"
Assert-Check "Permission_Guard" "DeleteUser empty username message indicates invalid username" ($delMsgEmpty -like "*không hợp lệ*") "Contains 'không hợp lệ'" "$delMsgEmpty"

$paramArrayNullUser = [object[]]@($null, "")
$delResultNull = $delMethod.Invoke($permService, $paramArrayNullUser)
$delMsgNull = $paramArrayNullUser[1]
Assert-Check "Permission_Guard" "DeleteUser rejects null username" (-not $delResultNull) "False" "$delResultNull"
Assert-Check "Permission_Guard" "DeleteUser null username message indicates invalid username" ($delMsgNull -like "*không hợp lệ*") "Contains 'không hợp lệ'" "$delMsgNull"


# 3.2 AdminResetPassword Stress Tests
$resetMethod = $permServiceType.GetMethods() | Where-Object { $_.Name -eq "AdminResetPassword" -and $_.GetParameters().Length -eq 3 } | Select-Object -First 1
Assert-Check "Permission_Reset" "AdminResetPassword method exists on PermissionService" ($resetMethod -ne $null) "NotNull" "$resetMethod"

# Case: Null username
$paramReset = [object[]]@($null, "newpass123", "")
$res = $resetMethod.Invoke($permService, $paramReset)
$msg = $paramReset[2]
Assert-Check "Permission_Reset" "AdminResetPassword rejects null username" (-not $res) "False" "$res ($msg)"

# Case: Empty username
$paramReset = [object[]]@("", "newpass123", "")
$res = $resetMethod.Invoke($permService, $paramReset)
$msg = $paramReset[2]
Assert-Check "Permission_Reset" "AdminResetPassword rejects empty username" (-not $res) "False" "$res ($msg)"

# Case: Null password
$paramReset = [object[]]@("thungan", $null, "")
$res = $resetMethod.Invoke($permService, $paramReset)
$msg = $paramReset[2]
Assert-Check "Permission_Reset" "AdminResetPassword rejects null password" (-not $res) "False" "$res ($msg)"

# Case: Empty password
$paramReset = [object[]]@("thungan", "", "")
$res = $resetMethod.Invoke($permService, $paramReset)
$msg = $paramReset[2]
Assert-Check "Permission_Reset" "AdminResetPassword rejects empty password" (-not $res) "False" "$res ($msg)"

# Case: Whitespace password
$paramReset = [object[]]@("thungan", "      ", "")
$res = $resetMethod.Invoke($permService, $paramReset)
$msg = $paramReset[2]
Assert-Check "Permission_Reset" "AdminResetPassword rejects whitespace password" (-not $res) "False" "$res ($msg)"

# Case: Password too short (< 6 chars)
$paramReset = [object[]]@("thungan", "12345", "")
$res = $resetMethod.Invoke($permService, $paramReset)
$msg = $paramReset[2]
Assert-Check "Permission_Reset" "AdminResetPassword rejects password shorter than 6 chars" (-not $res) "False" "$res ($msg)"
Assert-Check "Permission_Reset" "AdminResetPassword message specifies minimum 6 chars" ($msg -like "*tối thiểu 6*") "Contains 'tối thiểu 6'" "$msg"

# Case: Non-existent user
$paramReset = [object[]]@("ghost_emp_888", "newValidPass2026!", "")
$res = $resetMethod.Invoke($permService, $paramReset)
$msg = $paramReset[2]
Assert-Check "Permission_Reset" "AdminResetPassword rejects non-existent user" (-not $res) "False" "$res ($msg)"
Assert-Check "Permission_Reset" "AdminResetPassword message indicates user not found" ($msg -like "*Không tìm thấy*") "Contains 'Không tìm thấy'" "$msg"


Write-Host "`n>>> [SECTION 4] EMPLOYEE DELETION FK GUARD TESTS <<<" -ForegroundColor Cyan

$empService = [Activator]::CreateInstance($empServiceType)
$empDelMethod = $empServiceType.GetMethods() | Where-Object { $_.Name -eq "DeleteEmployee" -and $_.GetParameters().Length -eq 2 } | Select-Object -First 1
Assert-Check "Employee_Guard" "DeleteEmployee(int, out string) method exists on EmployeeService" ($empDelMethod -ne $null) "NotNull" "$empDelMethod"

# Case: ID = 0
$pEmp = [object[]]@(0, "")
$resEmp = $empDelMethod.Invoke($empService, $pEmp)
$msgEmp = $pEmp[1]
Assert-Check "Employee_Guard" "DeleteEmployee rejects ID = 0" (-not $resEmp) "False" "$resEmp ($msgEmp)"

# Case: Negative ID
$pEmp = [object[]]@(-99, "")
$resEmp = $empDelMethod.Invoke($empService, $pEmp)
$msgEmp = $pEmp[1]
Assert-Check "Employee_Guard" "DeleteEmployee rejects negative ID (-99)" (-not $resEmp) "False" "$resEmp ($msgEmp)"

# Case: Non-existent high ID (handling without crash)
$threwEmp = $false
$resEmpHigh = $false
$msgEmpHigh = ""
try {
    $pEmpHigh = [object[]]@(999999, "")
    $resEmpHigh = $empDelMethod.Invoke($empService, $pEmpHigh)
    $msgEmpHigh = $pEmpHigh[1]
} catch {
    $threwEmp = $true
    $msgEmpHigh = $_.Exception.ToString()
}
Assert-Check "Employee_Guard" "DeleteEmployee handles missing entity without unhandled crash" (-not $threwEmp) "No Crash" "Handled gracefully: $msgEmpHigh"


Write-Host "`n>>> [SECTION 5] DATA VALIDATION & REWARD POINTS BOUNDARY <<<" -ForegroundColor Cyan

$custService = [Activator]::CreateInstance($custServiceType)

# Find CustomerService.Add(CustomerDTO, out string)
$addMethod = $custServiceType.GetMethods() | Where-Object { $_.Name -eq "Add" -and $_.GetParameters().Length -eq 2 } | Select-Object -First 1
Assert-Check "Customer_Validation" "CustomerService.Add(CustomerDTO, out string) exists" ($addMethod -ne $null) "NotNull" "$addMethod"

# Case: Add customer with negative reward points
$newCust = [Activator]::CreateInstance($custDtoType)
$newCust.CustomerName = "Nguyễn Văn Test"
$newCust.Phone = "0901234567"
$newCust.RewardPoints = -50
$newCust.MembershipTier = "Đồng"

$pCust = [object[]]@($newCust, "")
$resCust = $addMethod.Invoke($custService, $pCust)
$msgCust = $pCust[1]

Assert-Check "Customer_Validation" "CustomerService.Add rejects negative RewardPoints (-50)" (-not $resCust) "False" "$resCust"
Assert-Check "Customer_Validation" "CustomerService.Add error message mentions points cannot be negative" ($msgCust -like "*không được nhỏ hơn 0*") "Contains 'không được nhỏ hơn 0'" "$msgCust"

# Find CustomerService.Update(CustomerDTO, out string)
$updMethod = $custServiceType.GetMethods() | Where-Object { $_.Name -eq "Update" -and $_.GetParameters().Length -eq 2 } | Select-Object -First 1
Assert-Check "Customer_Validation" "CustomerService.Update(CustomerDTO, out string) exists" ($updMethod -ne $null) "NotNull" "$updMethod"

# Case: Update customer with negative reward points
$newCust.Id = 1
$newCust.RewardPoints = -1
$pCustUpd = [object[]]@($newCust, "")
$resCustUpd = $updMethod.Invoke($custService, $pCustUpd)
$msgCustUpd = $pCustUpd[1]

Assert-Check "Customer_Validation" "CustomerService.Update rejects negative RewardPoints (-1)" (-not $resCustUpd) "False" "$resCustUpd"
Assert-Check "Customer_Validation" "CustomerService.Update error message mentions points cannot be negative" ($msgCustUpd -like "*không được nhỏ hơn 0*") "Contains 'không được nhỏ hơn 0'" "$msgCustUpd"


Write-Host "`n==========================================================================" -ForegroundColor Cyan
Write-Host "                       CHALLENGER SUMMARY REPORT                          " -ForegroundColor Yellow
Write-Host "==========================================================================" -ForegroundColor Cyan

$total = $testResults.Count
$passed = @($testResults | Where-Object { $_.Passed }).Count
$failed = @($testResults | Where-Object { -not $_.Passed }).Count
$rate = if ($total -gt 0) { [Math]::Round(($passed / $total) * 100, 1) } else { 0 }

Write-Host "Total Stress Tests Run : $total" -ForegroundColor White
Write-Host "Passed                 : $passed" -ForegroundColor Green
Write-Host "Failed                 : $failed" -ForegroundColor $(if ($failed -eq 0) { "Green" } else { "Red" })
Write-Host "Stress Pass Rate       : $rate %" -ForegroundColor $(if ($rate -ge 100) { "Green" } else { "Red" })

# Return JSON summary for machine parsing
$jsonOutput = $testResults | ConvertTo-Json -Depth 3
$jsonPath = Join-Path $ProjectRoot "tests\Challenger_M1_Results.json"
Set-Content -Path $jsonPath -Value $jsonOutput -Encoding UTF8
Write-Host "Detailed test results saved to: $jsonPath" -ForegroundColor Gray

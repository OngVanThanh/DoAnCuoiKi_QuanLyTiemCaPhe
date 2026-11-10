# ==============================================================================
# Comprehensive Forensic Audit Test Suite - Milestone 1
# ==============================================================================
$ErrorActionPreference = "Stop"

$projectRoot = "d:\DoAnCuoiKi_QuanLyTiemCaPhe"
$binPath = Join-Path $projectRoot "bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe"
$sqlPath1 = Join-Path $projectRoot "CoffeeShopDB.sql"
$sqlPath2 = Join-Path $projectRoot "CoffeeShopDB_Full.sql"

$results = [System.Collections.Generic.List[PSObject]]::new()

function Record-Check([string]$name, [bool]$passed, [string]$details) {
    $results.Add([PSCustomObject]@{
        Name    = $name
        Passed  = $passed
        Details = $details
    })
    $status = if ($passed) { "[PASS]" } else { "[FAIL]" }
    Write-Host "$status $name - $details" -ForegroundColor $(if ($passed) { "Green" } else { "Red" })
}

# ------------------------------------------------------------------------------
# 1. SQL Seed Hash Forensic Verification
# ------------------------------------------------------------------------------
$sql1Content = Get-Content $sqlPath1 -Raw
$sql2Content = Get-Content $sqlPath2 -Raw

$adminSalt = "MvzYIcst8H+HCiQecovlBA=="
$adminHash = "4YUma6Q69CiQ97YVG17kFFFVD1HGqb2exAjnTpdQe0g="
$tnSalt    = "RAq3VsoVEEY92GOgn+/wCA=="
$tnHash    = "ExGmL2DJ+2fEVbrgGAtbpkBl0c2eNsKncAH/rmQawuY="

$sql1HasAdmin = $sql1Content.Contains($adminHash) -and $sql1Content.Contains($adminSalt)
$sql1HasTn    = $sql1Content.Contains($tnHash) -and $sql1Content.Contains($tnSalt)
$sql2HasAdmin = $sql2Content.Contains($adminHash) -and $sql2Content.Contains($adminSalt)
$sql2HasTn    = $sql2Content.Contains($tnHash) -and $sql2Content.Contains($tnSalt)

Record-Check "SQL Seed Hash Presence (CoffeeShopDB.sql)" ($sql1HasAdmin -and $sql1HasTn) "Contains PBKDF2 hash & salt for admin and thungan"
Record-Check "SQL Seed Hash Presence (CoffeeShopDB_Full.sql)" ($sql2HasAdmin -and $sql2HasTn) "Contains PBKDF2 hash & salt for admin and thungan"

# Independent PBKDF2 re-derivation
function Compute-Pbkdf2([string]$password, [string]$saltBase64) {
    $saltBytes = [Convert]::FromBase64String($saltBase64)
    $pbkdf2 = New-Object System.Security.Cryptography.Rfc2898DeriveBytes($password, $saltBytes, 100000)
    $hashBytes = $pbkdf2.GetBytes(32)
    return [Convert]::ToBase64String($hashBytes)
}

$derivedAdminHash = Compute-Pbkdf2 "admin123" $adminSalt
$derivedTnHash    = Compute-Pbkdf2 "123456" $tnSalt

Record-Check "Admin PBKDF2 Match" ($derivedAdminHash -eq $adminHash) "admin123 + salt yields $derivedAdminHash"
Record-Check "Thungan PBKDF2 Match" ($derivedTnHash -eq $tnHash) "123456 + salt yields $derivedTnHash"

# ------------------------------------------------------------------------------
# 2. Binary Reflection Verification
# ------------------------------------------------------------------------------
[System.Reflection.Assembly]::LoadWithPartialName("System.Drawing") | Out-Null
[System.Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms") | Out-Null
[System.Reflection.Assembly]::LoadWithPartialName("System.Configuration") | Out-Null
[System.Reflection.Assembly]::LoadWithPartialName("System.Data.Linq") | Out-Null

$asm = [System.Reflection.Assembly]::LoadFrom($binPath)

# 2.1 SecurityHelper.VerifyPassword
$secHelper = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.SecurityHelper", $true)
$verifyMethod = $secHelper.GetMethod("VerifyPassword", [System.Reflection.BindingFlags]"Public,Static")

$v1 = $verifyMethod.Invoke($null, @("admin123", $adminHash, $adminSalt))
$v2 = $verifyMethod.Invoke($null, @("wrongpass", $adminHash, $adminSalt))
$v3 = $verifyMethod.Invoke($null, @("123456", $tnHash, $tnSalt))
$v4 = $verifyMethod.Invoke($null, @("", $tnHash, $tnSalt))
$v5 = $verifyMethod.Invoke($null, @("123456", "", $tnSalt))

Record-Check "SecurityHelper Admin Valid Password" ($v1 -eq $true) "Returns true for admin123"
Record-Check "SecurityHelper Admin Invalid Password" ($v2 -eq $false) "Returns false for incorrect password"
Record-Check "SecurityHelper Cashier Valid Password" ($v3 -eq $true) "Returns true for 123456"
Record-Check "SecurityHelper Empty Password Protection" ($v4 -eq $false) "Rejects empty password"
Record-Check "SecurityHelper Empty Hash Protection" ($v5 -eq $false) "Rejects empty hash"

# 2.2 CustomerService RewardPoints validation
$custServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.CustomerService", $true)
$custDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.CustomerDTO", $true)
$custService = [Activator]::CreateInstance($custServiceType)

$addMethod = $custServiceType.GetMethod("Add", [System.Reflection.BindingFlags]"Public,Instance", $null, @($custDtoType, [string].MakeByRefType()), $null)
$updateMethod = $custServiceType.GetMethod("Update", [System.Reflection.BindingFlags]"Public,Instance", $null, @($custDtoType, [string].MakeByRefType()), $null)

$custNeg = [Activator]::CreateInstance($custDtoType)
$custNeg.Phone = "0911223344"
$custNeg.CustomerName = "Test Auditor"
$custNeg.RewardPoints = -25

$addArgs = [object[]]@($custNeg, [string]"")
$addRet = $addMethod.Invoke($custService, $addArgs)
$addMsg = $addArgs[1]

Record-Check "CustomerService Add Negative Points" ($addRet -eq $false -and $addMsg -like "*không được nhỏ hơn 0*") "Add rejected: $addMsg"

$updateArgs = [object[]]@($custNeg, [string]"")
$updateRet = $updateMethod.Invoke($custService, $updateArgs)
$updateMsg = $updateArgs[1]

Record-Check "CustomerService Update Negative Points" ($updateRet -eq $false -and $updateMsg -like "*không được nhỏ hơn 0*") "Update rejected: $updateMsg"

# 2.3 EmployeeService validation
$empServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.EmployeeService", $true)
$empService = [Activator]::CreateInstance($empServiceType)
$empDelMethod = $empServiceType.GetMethod("DeleteEmployee", [System.Reflection.BindingFlags]"Public,Instance", $null, @([int], [string].MakeByRefType()), $null)

$empArgs0 = [object[]]@(0, [string]"")
$empRet0 = $empDelMethod.Invoke($empService, $empArgs0)
$empMsg0 = $empArgs0[1]

$empArgsNeg = [object[]]@(-99, [string]"")
$empRetNeg = $empDelMethod.Invoke($empService, $empArgsNeg)
$empMsgNeg = $empArgsNeg[1]

Record-Check "EmployeeService Delete ID=0" ($empRet0 -eq $false -and $empMsg0 -like "*không hợp lệ*") "Rejected ID 0: $empMsg0"
Record-Check "EmployeeService Delete Negative ID" ($empRetNeg -eq $false -and $empMsgNeg -like "*không hợp lệ*") "Rejected negative ID: $empMsgNeg"

# 2.4 PermissionService AdminResetPassword parameter validation
$permServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.PermissionService", $true)
$permService = [Activator]::CreateInstance($permServiceType)

$resetMethod = $permServiceType.GetMethod("AdminResetPassword", [System.Reflection.BindingFlags]"Public,Instance", $null, @([string], [string], [string].MakeByRefType()), $null)

$rArgsEmptyUser = [object[]]@("", "newpassword123", [string]"")
$rRetEmptyUser = $resetMethod.Invoke($permService, $rArgsEmptyUser)
$rMsgEmptyUser = $rArgsEmptyUser[2]

Record-Check "AdminResetPassword Empty Username" ($rRetEmptyUser -eq $false -and $rMsgEmptyUser -like "*không được để trống*") "Rejected empty username: $rMsgEmptyUser"

$rArgsEmptyPass = [object[]]@("testuser", "", [string]"")
$rRetEmptyPass = $resetMethod.Invoke($permService, $rArgsEmptyPass)
$rMsgEmptyPass = $rArgsEmptyPass[2]

Record-Check "AdminResetPassword Empty Password" ($rRetEmptyPass -eq $false -and $rMsgEmptyPass -like "*không được để trống*") "Rejected empty password: $rMsgEmptyPass"

$rArgsShortPass = [object[]]@("testuser", "12345", [string]"")
$rRetShortPass = $resetMethod.Invoke($permService, $rArgsShortPass)
$rMsgShortPass = $rArgsShortPass[2]

Record-Check "AdminResetPassword Short Password (<6 chars)" ($rRetShortPass -eq $false -and $rMsgShortPass -like "*tối thiểu 6 ký tự*") "Rejected short password: $rMsgShortPass"

# 2.5 ShiftSchedule EmployeeScheduleItem
$schedItemType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.EmployeeScheduleItem", $false)
$hasSchedItem = $schedItemType -ne $null
$hasIdProp = $hasSchedItem -and ($schedItemType.GetProperty("Id") -ne $null)
$hasDisplayProp = $hasSchedItem -and ($schedItemType.GetProperty("DisplayText") -ne $null)

Record-Check "EmployeeScheduleItem Type & Properties" ($hasSchedItem -and $hasIdProp -and $hasDisplayProp) "EmployeeScheduleItem with Id and DisplayText exists"

# ------------------------------------------------------------------------------
# 3. Source Code Forensic Pattern Analysis (Anti-Cheat Scan)
# ------------------------------------------------------------------------------
$filesToCheck = @(
    "BLL\PermissionService.cs",
    "BLL\EmployeeService.cs",
    "BLL\CustomerService.cs",
    "DAL\EmployeeRepository.cs",
    "Form\VanThanh\FrmPermission.cs",
    "Form\VanThanh\FrmEmployee.cs",
    "Form\VanThanh\FrmShiftSchedule.cs",
    "Form\TuanHung\FrmCategory.cs",
    "Form\TuanHung\FrmProduct.cs",
    "Form\TuanHung\FrmPromotion.cs",
    "Form\QuangHung\FrmCustomer.cs"
)

$suspiciousCount = 0
foreach ($rel in $filesToCheck) {
    $fullPath = Join-Path $projectRoot $rel
    $content = Get-Content $fullPath -Raw
    
    # Check for hardcoded test bypass patterns
    if ($content -match "return\s+true\s*;\s*//\s*(bypass|test|cheat|mock)") {
        $suspiciousCount++
        Record-Check "Anti-Cheat: $rel" $false "Found hardcoded bypass comment"
    }
    # Check for NotImplemented stubs
    if ($content -match "throw\s+new\s+NotImplementedException") {
        $suspiciousCount++
        Record-Check "Anti-Cheat: $rel" $false "Found NotImplementedException stub"
    }
}

Record-Check "Anti-Cheat Scan across M1 Source Files" ($suspiciousCount -eq 0) "Zero suspicious stubs, bypasses, or fake mocks detected in $filesToCheck"

# ------------------------------------------------------------------------------
# Summary
# ------------------------------------------------------------------------------
$failCount = ($results | Where-Object { -not $_.Passed }).Count
Write-Host "`n=======================================================" -ForegroundColor Cyan
Write-Host "FORENSIC AUDIT SUMMARY: $($results.Count) checks executed, $failCount failed." -ForegroundColor $(if ($failCount -eq 0) { "Green" } else { "Red" })
Write-Host "=======================================================" -ForegroundColor Cyan

if ($failCount -gt 0) {
    exit 1
} else {
    exit 0
}

# ==============================================================================
# Tier 2 - Test_InvalidAuthentication.ps1
# Boundary: Non-existent users, wrong password, inactive account, SQL injection attempts
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 2: Invalid Authentication & Account Boundary Tests ---" -ForegroundColor Cyan

$asm = Get-ProjectAssembly
$secType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.SecurityHelper")
$userType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.User")
$permServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.PermissionService")
$authServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.AuthService")

$permService = [Activator]::CreateInstance($permServiceType)
$authService = [Activator]::CreateInstance($authServiceType)

# Prepare a known user entity
$correctPass = "SecureManager#2026"
$salt = $secType.GetMethod("GenerateSalt").Invoke($null, @())
$hash = $secType.GetMethod("HashPassword").Invoke($null, @($correctPass, $salt))

$activeUser = [Activator]::CreateInstance($userType)
$activeUser.Id = 10
$activeUser.Username = "test_manager"
$activeUser.PasswordHash = $hash
$activeUser.Salt = $salt
$activeUser.IsActive = $true
$activeUser.RoleId = 1

# Test 2.1: VerifyPassword with correct credentials on active user
$vPass = $permService.VerifyPassword($correctPass, $activeUser)
Assert-True $vPass "PermissionService.VerifyPassword accepts valid credentials" "Tier 2" "Invalid_Auth"

# Test 2.2: VerifyPassword with wrong password
$vWrong = $permService.VerifyPassword("WrongPassword123", $activeUser)
Assert-False $vWrong "PermissionService.VerifyPassword rejects incorrect password" "Tier 2" "Invalid_Auth"

# Test 2.3: VerifyPassword with empty or null user entity
$vNullUser = $permService.VerifyPassword($correctPass, $null)
Assert-False $vNullUser "PermissionService.VerifyPassword rejects null user entity" "Tier 2" "Invalid_Auth"

# Test 2.4: VerifyPassword with missing password hash or salt
$corruptUser = [Activator]::CreateInstance($userType)
$corruptUser.Username = "corrupt_user"
$corruptUser.PasswordHash = $null
$corruptUser.Salt = $salt

$vCorrupt = $permService.VerifyPassword($correctPass, $corruptUser)
Assert-False $vCorrupt "PermissionService.VerifyPassword rejects user with null PasswordHash" "Tier 2" "Invalid_Auth"

# Test 2.5: SQL Injection strings in credentials
$sqlInjectionPayloads = @(
    "' OR '1'='1",
    "admin' --",
    "'; DROP TABLE Users; --",
    "admin' /*",
    "' UNION SELECT NULL, NULL, NULL --"
)

foreach ($payload in $sqlInjectionPayloads) {
    # Verify Hash & Verify functions safely handle SQL metacharacters without unhandled exceptions
    $threw = $false
    try {
        $vSqlInj = $permService.VerifyPassword($payload, $activeUser)
        Assert-False $vSqlInj "Reject SQL injection string in password: $payload" "Tier 2" "Invalid_Auth"
    } catch {
        $threw = $true
    }
    Assert-False $threw "System safely evaluates SQL injection string without crashing" "Tier 2" "Invalid_Auth"
}

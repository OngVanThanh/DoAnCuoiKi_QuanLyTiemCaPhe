# ==============================================================================
# Tier 2 - Test_EmptyAndNullInputs.ps1
# Boundary: Empty strings, whitespace, null inputs across security and order services
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 2: Empty & Null Inputs Tests ---" -ForegroundColor Cyan

$asm = Get-ProjectAssembly
$secType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.SecurityHelper")
$authType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.AuthService")
$orderServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.OrderService")

$orderService = [Activator]::CreateInstance($orderServiceType)
$authService = [Activator]::CreateInstance($authType)

# Test 2.1: SecurityHelper.HashPassword with empty password throws ArgumentException
$salt = $secType.GetMethod("GenerateSalt").Invoke($null, @())

Assert-Throws {
    try {
        $secType.GetMethod("HashPassword").Invoke($null, @("", $salt))
    } catch [System.Reflection.TargetInvocationException] {
        throw $_.Exception.InnerException
    }
} "ArgumentException" "HashPassword throws ArgumentException for empty password" "Tier 2" "Empty_Null_Inputs"

# Test 2.2: SecurityHelper.HashPassword with null password throws ArgumentException
Assert-Throws {
    try {
        $secType.GetMethod("HashPassword").Invoke($null, @($null, $salt))
    } catch [System.Reflection.TargetInvocationException] {
        throw $_.Exception.InnerException
    }
} "ArgumentException" "HashPassword throws ArgumentException for null password" "Tier 2" "Empty_Null_Inputs"

# Test 2.3: SecurityHelper.VerifyPassword with empty / null fields returns false
$validHash = $secType.GetMethod("HashPassword").Invoke($null, @("validPass123", $salt))

$v1 = $secType.GetMethod("VerifyPassword").Invoke($null, @("", $validHash, $salt))
Assert-False $v1 "VerifyPassword returns False for empty password" "Tier 2" "Empty_Null_Inputs"

$v2 = $secType.GetMethod("VerifyPassword").Invoke($null, @($null, $validHash, $salt))
Assert-False $v2 "VerifyPassword returns False for null password" "Tier 2" "Empty_Null_Inputs"

$v3 = $secType.GetMethod("VerifyPassword").Invoke($null, @("validPass123", "", $salt))
Assert-False $v3 "VerifyPassword returns False for empty hash" "Tier 2" "Empty_Null_Inputs"

$v4 = $secType.GetMethod("VerifyPassword").Invoke($null, @("validPass123", $validHash, ""))
Assert-False $v4 "VerifyPassword returns False for empty salt" "Tier 2" "Empty_Null_Inputs"

# Test 2.4: AuthService.ValidateUser with empty/whitespace credentials
$u1 = $authService.ValidateUser("", "")
Assert-Equal $u1 $null "ValidateUser returns null for empty username & password" "Tier 2" "Empty_Null_Inputs"

$u2 = $authService.ValidateUser("   ", "   ")
Assert-Equal $u2 $null "ValidateUser returns null for whitespace username & password" "Tier 2" "Empty_Null_Inputs"

$u3 = $authService.ValidateUser($null, $null)
Assert-Equal $u3 $null "ValidateUser returns null for null credentials" "Tier 2" "Empty_Null_Inputs"

# Test 2.5: OrderService.CalculateDiscount with empty/null input returns 0 discount
$promoId = $null; $discInfo = ""
$d1 = $orderService.CalculateDiscount([decimal]100000, "", [ref]$promoId, [ref]$discInfo)
Assert-Equal $d1 ([decimal]0) "CalculateDiscount returns 0 for empty discount input" "Tier 2" "Empty_Null_Inputs"

$d2 = $orderService.CalculateDiscount([decimal]100000, "   ", [ref]$promoId, [ref]$discInfo)
Assert-Equal $d2 ([decimal]0) "CalculateDiscount returns 0 for whitespace discount input" "Tier 2" "Empty_Null_Inputs"

$d3 = $orderService.CalculateDiscount([decimal]0, "10", [ref]$promoId, [ref]$discInfo)
Assert-Equal $d3 ([decimal]0) "CalculateDiscount returns 0 when total amount is 0" "Tier 2" "Empty_Null_Inputs"

# ==============================================================================
# Tier 2 - Test_DiscountLimits.ps1
# Boundary: Extreme discounts, discount > total amount, negative discount, invalid codes
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 2: Discount Limits & Boundary Tests ---" -ForegroundColor Cyan

$asm = Get-ProjectAssembly
$orderServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.OrderService")
$orderService = [Activator]::CreateInstance($orderServiceType)

$totalAmount = [decimal]100000 # 100,000 VND

# Test 2.1: Percentage Discount Boundary (100% discount)
$promoId = $null; $discInfo = ""
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$d100 = $orderService.CalculateDiscount($totalAmount, "100", [ref]$promoId, [ref]$discInfo)
$sw.Stop()

Assert-Equal $d100 ([decimal]100000) "100% discount yields entire total amount (100,000 VND)" "Tier 2" "Discount_Limits" -DurationMs $sw.TotalMilliseconds
Assert-Match $discInfo "100%" "Discount description indicates 100%" "Tier 2" "Discount_Limits"

# Test 2.2: Percentage Discount Boundary (0% discount)
$d0 = $orderService.CalculateDiscount($totalAmount, "0", [ref]$promoId, [ref]$discInfo)
Assert-Equal $d0 ([decimal]0) "0% discount yields 0 VND" "Tier 2" "Discount_Limits"

# Test 2.3: Direct Cash Discount Exceeding Total (e.g., 500,000 VND on 100,000 VND bill)
# Criteria: Discount MUST NOT exceed totalAmount (cannot result in negative bill)
$dExcessive = $orderService.CalculateDiscount($totalAmount, "500000", [ref]$promoId, [ref]$discInfo)
Assert-Equal $dExcessive ([decimal]100000) "Cash discount greater than total is capped at total amount (100,000 VND)" "Tier 2" "Discount_Limits"

# Test 2.4: Negative Discount Input (e.g., "-50")
# Criteria: Must not return negative discount or increase bill
$dNegative = $orderService.CalculateDiscount($totalAmount, "-50", [ref]$promoId, [ref]$discInfo)
Assert-True ($dNegative -ge [decimal]0) "Negative discount input does not produce negative discount" "Tier 2" "Discount_Limits"

# Test 2.5: Invalid / Non-existent Promo Code
$dInvalidPromo = $orderService.CalculateDiscount($totalAmount, "NON_EXISTENT_PROMO_CODE_999", [ref]$promoId, [ref]$discInfo)
Assert-Equal $dInvalidPromo ([decimal]0) "Invalid promo code yields 0 discount" "Tier 2" "Discount_Limits"
Assert-Match $discInfo "không hợp lệ|hết hạn" "Info indicates promo code is invalid or expired" "Tier 2" "Discount_Limits"

# Test 2.6: Malformed / Special Character Discount Input
$malformedInputs = @("@#$%", "ABC123XYZ", "INV_CODE!", "; DROP TABLE Promotions;", "--")
foreach ($input in $malformedInputs) {
    $threw = $false
    $dRes = [decimal]0
    try {
        $dRes = $orderService.CalculateDiscount($totalAmount, $input, [ref]$promoId, [ref]$discInfo)
    } catch {
        $threw = $true
    }
    Assert-False $threw "System safely parses malformed discount input '$input' without exception" "Tier 2" "Discount_Limits"
    Assert-Equal $dRes ([decimal]0) "Malformed input '$input' safely yields 0 discount" "Tier 2" "Discount_Limits"
}

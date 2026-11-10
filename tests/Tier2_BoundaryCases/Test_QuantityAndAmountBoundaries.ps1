# ==============================================================================
# Tier 2 - Test_QuantityAndAmountBoundaries.ps1
# Boundary: Zero & Negative Quantities, Invalid Identifiers, Zero Item Checkouts
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 2: Quantity & Amount Boundaries Tests ---" -ForegroundColor Cyan

$asm = Get-ProjectAssembly
$orderServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.OrderService")
$orderService = [Activator]::CreateInstance($orderServiceType)

# Test 2.1: Zero Quantity Boundary
$msg = ""
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$resZeroQty = $orderService.AddProductToTableOrder(1, 1, 1, 0, [ref]$msg)
$sw.Stop()

Assert-False $resZeroQty "Reject quantity = 0" "Tier 2" "Boundaries_Quantity" -DurationMs $sw.TotalMilliseconds
Assert-Match $msg "lớn hơn 0" "Error message states quantity must be greater than 0" "Tier 2" "Boundaries_Quantity"

# Test 2.2: Negative Quantity Boundary
$msg = ""
$resNegQty = $orderService.AddProductToTableOrder(1, 1, 1, -5, [ref]$msg)
Assert-False $resNegQty "Reject negative quantity = -5" "Tier 2" "Boundaries_Quantity"
Assert-Match $msg "lớn hơn 0" "Error message rejects negative quantity" "Tier 2" "Boundaries_Quantity"

# Test 2.3: Invalid Table ID Boundary (tableId <= 0)
$msg = ""
$resInvalidTable = $orderService.AddProductToTableOrder(0, 1, 1, 1, [ref]$msg)
Assert-False $resInvalidTable "Reject tableId = 0" "Tier 2" "Boundaries_Quantity"
Assert-Match $msg "chọn bàn" "Error message prompts table selection" "Tier 2" "Boundaries_Quantity"

$msg = ""
$resNegTable = $orderService.AddProductToTableOrder(-99, 1, 1, 1, [ref]$msg)
Assert-False $resNegTable "Reject tableId = -99" "Tier 2" "Boundaries_Quantity"

# Test 2.4: Invalid Product ID Boundary (productId <= 0)
$msg = ""
$resInvalidProd = $orderService.AddProductToTableOrder(1, 1, 0, 1, [ref]$msg)
Assert-False $resInvalidProd "Reject productId = 0" "Tier 2" "Boundaries_Quantity"

$msg = ""
$resNegProd = $orderService.AddProductToTableOrder(1, 1, -12, 1, [ref]$msg)
Assert-False $resNegProd "Reject productId = -12" "Tier 2" "Boundaries_Quantity"

# Test 2.5: Non-existent Table Boundary
$msg = ""
$resGhostTable = $orderService.AddProductToTableOrder(999999, 1, 1, 1, [ref]$msg)
Assert-False $resGhostTable "Reject non-existent tableId = 999999" "Tier 2" "Boundaries_Quantity"

# Test 2.6: Checkout table with zero orders
$paidDto = $null; $details = $null; $checkoutMsg = ""
$resEmptyCheckout = $orderService.CheckoutTableOrder(999999, "", [ref]$paidDto, [ref]$details, [ref]$checkoutMsg)
Assert-False $resEmptyCheckout "Checkout on table without order returns False" "Tier 2" "Boundaries_Quantity"

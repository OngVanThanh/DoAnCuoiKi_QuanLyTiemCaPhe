# ==============================================================================
# Tier 4 - Scenario_TakeawayWorkflow.ps1
# Real-World: Takeaway / Counter Rapid Turnaround Workflow ("Khách tại quầy / Mang đi")
# Journey: Customer 1 Walk-in -> Rapid Order -> Immediate Payment -> Table Continuity ->
#          Customer 2 Walk-in -> Distinct Order -> Immediate Payment
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 4: Scenario 2 - Rapid Takeaway Service Workflow ---" -ForegroundColor Cyan

$asm = Get-ProjectAssembly

$orderServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.OrderService")
$tableRepoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DAL.TableRepository")

$orderService = [Activator]::CreateInstance($orderServiceType)
$tableRepo = [Activator]::CreateInstance($tableRepoType)

$counterTableId = 1 # Khách tại quầy / Mang đi

# Clear any lingering unpaid order on counter table
$lingering = $orderService.GetUnpaidOrderByTable($counterTableId)
if ($lingering) {
    $p = $null; $dt = $null; $m = ""
    $orderService.CheckoutTableOrder($counterTableId, "", [ref]$p, [ref]$dt, [ref]$m)
}

# --- Transaction 1: First Takeaway Customer ---
Write-Host "   [Customer 1] Walk-in ordering 2x Cà phê sữa đá to go..." -ForegroundColor Gray
$msg1 = ""
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$orderOk1 = $orderService.AddProductToTableOrder($counterTableId, 1, 2, 2, [ref]$msg1)
$sw.Stop()

Assert-True $orderOk1 "Customer 1: Order added to counter table" "Tier 4" "Scenario_Takeaway" -DurationMs $sw.TotalMilliseconds

$paidOrder1 = $null; $details1 = $null; $checkoutMsg1 = ""
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$payOk1 = $orderService.CheckoutTableOrder($counterTableId, "", [ref]$paidOrder1, [ref]$details1, [ref]$checkoutMsg1)
$sw.Stop()

Assert-True $payOk1 "Customer 1: Instant counter checkout completed" "Tier 4" "Scenario_Takeaway" -DurationMs $sw.TotalMilliseconds
Assert-Equal $paidOrder1.TotalAmount ([decimal]58000) "Customer 1: Total bill is 58,000 VND" "Tier 4" "Scenario_Takeaway"

# --- Verification: Counter Table Readiness for Subsequent Customer ---
# Crucial Cafe Requirement: Counter table must NEVER lock or prevent consecutive walk-in orders
$unpaidAfterCust1 = $orderService.GetUnpaidOrderByTable($counterTableId)
Assert-Equal $unpaidAfterCust1 $null "Counter table has no unpaid backlog after customer 1 checkout" "Tier 4" "Scenario_Takeaway"

# --- Transaction 2: Second Consecutive Takeaway Customer ---
Write-Host "   [Customer 2] Next customer in queue ordering 1x Sinh tố bơ sáp..." -ForegroundColor Gray
$msg2 = ""
$sw = [System.Diagnostics.Stopwatch]::StartNew()
# 1x Sinh tố bơ sáp (Product 7: 45k)
$orderOk2 = $orderService.AddProductToTableOrder($counterTableId, 1, 7, 1, [ref]$msg2)
$sw.Stop()

Assert-True $orderOk2 "Customer 2: Immediate consecutive order placed without table collision" "Tier 4" "Scenario_Takeaway" -DurationMs $sw.TotalMilliseconds

$paidOrder2 = $null; $details2 = $null; $checkoutMsg2 = ""
$payOk2 = $orderService.CheckoutTableOrder($counterTableId, "", [ref]$paidOrder2, [ref]$details2, [ref]$checkoutMsg2)

Assert-True $payOk2 "Customer 2: Checkout completed successfully" "Tier 4" "Scenario_Takeaway"
Assert-Equal $paidOrder2.TotalAmount ([decimal]45000) "Customer 2: Total bill is 45,000 VND" "Tier 4" "Scenario_Takeaway"

# Verify the two orders have distinct identifiers
Assert-True ($paidOrder1.Id -ne $paidOrder2.Id) "Consecutive takeaway transactions generated distinct order IDs (#$($paidOrder1.Id) vs #$($paidOrder2.Id))" "Tier 4" "Scenario_Takeaway"

# ==============================================================================
# Tier 3 - Test_OrderBillingTableSync.ps1
# Cross-Feature: Ordering -> Billing -> Table Status Update -> Revenue Chart Synchronization
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 3: Ordering -> Billing -> Table Status -> Chart Sync ---" -ForegroundColor Cyan

$asm = Get-ProjectAssembly
$orderServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.OrderService")
$tableRepoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DAL.TableRepository")
$orderRepoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DAL.OrderRepository")

$orderService = [Activator]::CreateInstance($orderServiceType)
$tableRepo = [Activator]::CreateInstance($tableRepoType)

$testTableId = 4

# Ensure Table 4 exists in FallbackTables for isolated testing
$tableType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Table")
$fallbackTablesField = $tableRepoType.GetField("FallbackTables", [System.Reflection.BindingFlags]"NonPublic,Static")
if ($fallbackTablesField) {
    $fallbackTables = $fallbackTablesField.GetValue($null)
    $found = $fallbackTables | Where-Object { $_.Id -eq $testTableId }
    if (-not $found) {
        $newT = [Activator]::CreateInstance($tableType)
        $newT.Id = $testTableId
        $newT.TableName = "Bàn 0$testTableId"
        $newT.AreaId = 1
        $newT.Status = "Trống"
        $fallbackTables.Add($newT)
    } else {
        $found.Status = "Trống"
    }
}

# Clear any lingering unpaid order on Table 4
$existing = $orderService.GetUnpaidOrderByTable($testTableId)
if ($existing) {
    $p = $null; $dt = $null; $cm = ""
    $orderService.CheckoutTableOrder($testTableId, "", [ref]$p, [ref]$dt, [ref]$cm)
}

$tableInitial = $tableRepo.GetTableById($testTableId)
Assert-NotNull $tableInitial "Target table (Table ID $testTableId) exists" "Tier 3" "Order_Bill_Table_Sync"

# Step 1: Add items to Table 4
# Product 2 (Cà phê sữa đá - 29k x 2 = 58k)
# Product 6 (Nước ép cam tươi - 38k x 1 = 38k)
# Expected subtotal = 96,000 VND
$msg = ""
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$add1 = $orderService.AddProductToTableOrder($testTableId, 1, 2, 2, [ref]$msg)
$add2 = $orderService.AddProductToTableOrder($testTableId, 1, 6, 1, [ref]$msg)
$sw.Stop()

Assert-True ($add1 -and $add2) "Step 1: Successfully added multiple items to Table $testTableId" "Tier 3" "Order_Bill_Table_Sync" -DurationMs $sw.TotalMilliseconds

# Step 2: Verify Table Status changed to 'Có khách'
$tableOccupied = $tableRepo.GetTableById($testTableId)
Assert-Equal $tableOccupied.Status "Có khách" "Step 2: Table status updated to 'Có khách' upon ordering" "Tier 3" "Order_Bill_Table_Sync"

# Step 3: Verify Order Details and Line Item Aggregation
$details = $orderService.GetOrderDetailsByTable($testTableId)
Assert-Equal $details.Count 2 "Step 3: Table cart contains 2 distinct items" "Tier 3" "Order_Bill_Table_Sync"
$totalCalculated = [decimal]0
foreach ($d in $details) { $totalCalculated += $d.TotalPrice }
Assert-Equal $totalCalculated ([decimal]96000) "Step 3: Line items sum matches exactly 96,000 VND" "Tier 3" "Order_Bill_Table_Sync"

# Step 4: Apply 15% Promotion (VIPCOFFEE)
$tot = [decimal]0; $disc = [decimal]0; $vatP = [decimal]0; $vatA = [decimal]0; $fin = [decimal]0; $promoId = $null; $discInfo = ""
$orderService.CalculateOrderSummary($testTableId, "VIPCOFFEE", [ref]$tot, [ref]$disc, [ref]$vatP, [ref]$vatA, [ref]$fin, [ref]$promoId, [ref]$discInfo)

$expectedDiscount = [Math]::Round(96000 * 0.15, 0) # 14,400
Assert-Equal $disc ([decimal]$expectedDiscount) "Step 4: VIPCOFFEE discount calculated as $expectedDiscount VND" "Tier 3" "Order_Bill_Table_Sync"

# Step 5: Checkout Order
$paidDto = $null; $paidDetails = $null; $checkoutMsg = ""
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$checkoutRes = $orderService.CheckoutTableOrder($testTableId, "VIPCOFFEE", [ref]$paidDto, [ref]$paidDetails, [ref]$checkoutMsg)
$sw.Stop()

Assert-True $checkoutRes "Step 5: Checkout executed successfully" "Tier 3" "Order_Bill_Table_Sync" -DurationMs $sw.TotalMilliseconds
Assert-Equal $paidDto.Status "Đã thanh toán" "Step 5: Paid order status is 'Đã thanh toán'" "Tier 3" "Order_Bill_Table_Sync"
Assert-Equal $paidDto.TotalAmount ([decimal]96000) "Step 5: Paid order recorded TotalAmount = 96,000 VND" "Tier 3" "Order_Bill_Table_Sync"

# Step 6: Verify Table Status returned to 'Trống'
$tableAfterCheckout = $tableRepo.GetTableById($testTableId)
Assert-Equal $tableAfterCheckout.Status "Trống" "Step 6: Table status reset to 'Trống' after bill payment" "Tier 3" "Order_Bill_Table_Sync"

# Step 7: Revenue Chart Data Synchronization
# When orders are completed, revenue statistics must include the paid order in today's revenue
$fallbackOrdersField = $orderRepoType.GetField("FallbackOrders", [System.Reflection.BindingFlags]"NonPublic,Static")
$fallbackOrders = $fallbackOrdersField.GetValue($null)

$todayPaidOrders = @($fallbackOrders | Where-Object { $_.Status -eq "Đã thanh toán" -and $_.OrderDate.Date -eq [DateTime]::Today })
Assert-True ($todayPaidOrders.Count -ge 1) "Step 7: Revenue dataset synchronizes completed orders for today's charts" "Tier 3" "Order_Bill_Table_Sync"

$todayRevenue = [decimal]0
foreach ($o in $todayPaidOrders) { $todayRevenue += $o.FinalAmount }
Assert-True ($todayRevenue -ge $paidDto.FinalAmount) "Step 7: Revenue chart aggregate includes the paid order amount" "Tier 3" "Order_Bill_Table_Sync"

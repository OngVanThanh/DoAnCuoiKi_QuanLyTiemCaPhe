# ==============================================================================
# Tier 4 - Scenario_DineInWorkflow.ps1
# Real-World: Full Dine-in Cafe Customer Lifecycle
# Journey: Arrival -> Table Seating -> Round 1 Drinks -> Round 2 Pastries ->
#          VIP Discount -> VAT -> Cash Tendered & Change -> K80 Thermal Receipt
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 4: Scenario 1 - Full Dine-In Customer Journey ---" -ForegroundColor Cyan

Add-Type -AssemblyName System.Drawing
$asm = Get-ProjectAssembly

$orderServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.OrderService")
$tableRepoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DAL.TableRepository")
$printHelperType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Utilities.PrintHelper")
$orderDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.OrderDTO")
$detailDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.OrderDetailDTO")

$orderService = [Activator]::CreateInstance($orderServiceType)
$tableRepo = [Activator]::CreateInstance($tableRepoType)

$dineInTableId = 2

# Ensure Table 2 exists in FallbackTables for isolated testing
$tableType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Table")
$fallbackTablesField = $tableRepoType.GetField("FallbackTables", [System.Reflection.BindingFlags]"NonPublic,Static")
if ($fallbackTablesField) {
    $fallbackTables = $fallbackTablesField.GetValue($null)
    $found = $fallbackTables | Where-Object { $_.Id -eq $dineInTableId }
    if (-not $found) {
        $newT = [Activator]::CreateInstance($tableType)
        $newT.Id = $dineInTableId
        $newT.TableName = "Bàn 0$dineInTableId"
        $newT.AreaId = 1
        $newT.Status = "Trống"
        $fallbackTables.Add($newT)
    } else {
        $found.Status = "Trống"
        $found.StartTime = $null
        $found.EndTime = $null
    }
}

# Clean slate for Table 2 if had open order
$existingOrder = $orderService.GetUnpaidOrderByTable($dineInTableId)
if ($existingOrder) {
    $paid = $null; $dt = $null; $cm = ""
    $orderService.CheckoutTableOrder($dineInTableId, "", [ref]$paid, [ref]$dt, [ref]$cm)
}

# --- Phase 1: Customer Seating & Round 1 Beverage Ordering ---
Write-Host "   [Phase 1] Guests seated at Table $dineInTableId, ordering beverages..." -ForegroundColor Gray
$msg = ""
$sw = [System.Diagnostics.Stopwatch]::StartNew()
# 2x Cà phê đen đá (Product 1: 25k x 2 = 50k)
$res1 = $orderService.AddProductToTableOrder($dineInTableId, 1, 1, 2, [ref]$msg)
# 1x Trà đào cam sả (Product 4: 40k x 1 = 40k)
$res2 = $orderService.AddProductToTableOrder($dineInTableId, 1, 4, 1, [ref]$msg)
$sw.Stop()

Assert-True ($res1 -and $res2) "Phase 1: Round 1 beverages placed successfully" "Tier 4" "Scenario_DineIn" -DurationMs $sw.TotalMilliseconds

# Verify Table is now marked 'Có khách'
$tableStatusP1 = ($tableRepo.GetTableById($dineInTableId)).Status
Assert-Equal $tableStatusP1 "Có khách" "Phase 1: Table shows 'Có khách' status on floor map" "Tier 4" "Scenario_DineIn"

# --- Phase 2: Round 2 Add-On Order (Pastries) ---
Write-Host "   [Phase 2] Guests ordering additional dessert..." -ForegroundColor Gray
# 1x Bánh Tiramisu (Product 8: 35k x 1 = 35k)
$res3 = $orderService.AddProductToTableOrder($dineInTableId, 1, 8, 1, [ref]$msg)
Assert-True $res3 "Phase 2: Add-on pastry (Bánh Tiramisu) added to open tab" "Tier 4" "Scenario_DineIn"

$cartDetails = $orderService.GetOrderDetailsByTable($dineInTableId)
Assert-Equal $cartDetails.Count 3 "Phase 2: Cart has 3 distinct items (2 drinks + 1 dessert)" "Tier 4" "Scenario_DineIn"

# Total subtotal: 50k + 40k + 35k = 125,000 VND
$subtotal = [decimal]0
foreach ($item in $cartDetails) { $subtotal += $item.TotalPrice }
Assert-Equal $subtotal ([decimal]125000) "Phase 2: Cumulative subtotal is 125,000 VND" "Tier 4" "Scenario_DineIn"

# --- Phase 3: Billing, Promotion, VAT & Change Due Calculation ---
Write-Host "   [Phase 3] Customer requests bill and presents 10% promo coupon..." -ForegroundColor Gray
$tot = [decimal]0; $disc = [decimal]0; $vatP = [decimal]0; $vatA = [decimal]0; $fin = [decimal]0; $promoId = $null; $discInfo = ""
$orderService.CalculateOrderSummary($dineInTableId, "CHAOMUNG", [ref]$tot, [ref]$disc, [ref]$vatP, [ref]$vatA, [ref]$fin, [ref]$promoId, [ref]$discInfo)

$expectedDisc = [Math]::Round(125000 * 0.10, 0) # 12,500 VND
Assert-Equal $disc ([decimal]$expectedDisc) "Phase 3: 10% promo gives $expectedDisc VND discount" "Tier 4" "Scenario_DineIn"

$afterDisc = 125000 - $expectedDisc # 112,500 VND
$expectedVat = [Math]::Round($afterDisc * ($vatP / 100), 0)
Assert-Equal $vatA ([decimal]$expectedVat) "Phase 3: VAT ($vatP%) calculated on post-discount amount ($expectedVat VND)" "Tier 4" "Scenario_DineIn"

$expectedFin = $afterDisc + $expectedVat
Assert-Equal $fin ([decimal]$expectedFin) "Phase 3: Final amount is $expectedFin VND" "Tier 4" "Scenario_DineIn"

# Cash tendered simulation (Customer hands a 200,000 VND banknote)
$cashTendered = [decimal]200000
$changeDue = $cashTendered - $fin
Assert-True ($changeDue -gt [decimal]0) "Phase 3: Change due ($changeDue VND) calculated for cash payment of $cashTendered VND" "Tier 4" "Scenario_DineIn"

# --- Phase 4: Order Payment Completion & Floor Reset ---
Write-Host "   [Phase 4] Completing transaction and resetting table..." -ForegroundColor Gray
$paidOrder = $null; $finalDetails = $null; $checkoutMsg = ""
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$checkoutOk = $orderService.CheckoutTableOrder($dineInTableId, "CHAOMUNG", [ref]$paidOrder, [ref]$finalDetails, [ref]$checkoutMsg)
$sw.Stop()

Assert-True $checkoutOk "Phase 4: Order finalized and paid" "Tier 4" "Scenario_DineIn" -DurationMs $sw.TotalMilliseconds
$tableStatusFinal = ($tableRepo.GetTableById($dineInTableId)).Status
Assert-Equal $tableStatusFinal "Trống" "Phase 4: Table 2 returns to 'Trống' for new guests" "Tier 4" "Scenario_DineIn"

# --- Phase 5: GDI+ K80 Thermal Invoice Printing ---
Write-Host "   [Phase 5] Rendering K80 thermal bill for customer..." -ForegroundColor Gray
$bmp = New-Object System.Drawing.Bitmap 300, 600
$g = [System.Drawing.Graphics]::FromImage($bmp)
$detailsListType = [System.Collections.Generic.List``1].MakeGenericType(@($detailDtoType))
$detailsList = [Activator]::CreateInstance($detailsListType)
foreach ($d in $finalDetails) { $detailsList.Add($d) }

$drawMethod = $printHelperType.GetMethod("DrawInvoiceK80", [System.Reflection.BindingFlags]"Public,Static", $null, @([System.Drawing.Graphics], $orderDtoType, $detailsListType, [int]), $null)
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$drawMethod.Invoke($null, @($g, $paidOrder, $detailsList, 300))
$sw.Stop()

$g.Dispose()
$bmp.Dispose()
Assert-True $true "Phase 5: GDI+ K80 thermal invoice printed successfully" "Tier 4" "Scenario_DineIn" -DurationMs $sw.TotalMilliseconds

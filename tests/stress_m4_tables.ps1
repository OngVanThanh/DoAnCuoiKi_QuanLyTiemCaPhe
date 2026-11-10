# ==============================================================================
# EMPIRICAL STRESS TEST HARNESS: Milestone 4 - POS Table & Switch/Merge Workflows
# Challenger 1 Adversarial Suite
# ==============================================================================

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
. (Join-Path $scriptDir "Common\TestHarness.ps1")

$asm = Get-ProjectAssembly

$testsRun = 0
$testsPassed = 0
$testsFailed = 0

function Assert-Stress([bool]$condition, [string]$testName, [string]$failDetail = "") {
    $script:testsRun++
    if ($condition) {
        $script:testsPassed++
        Write-Host "  [PASS] $testName" -ForegroundColor Green
    } else {
        $script:testsFailed++
        Write-Host "  [FAIL] $testName" -ForegroundColor Red
        if ($failDetail) {
            Write-Host "         Detail: $failDetail" -ForegroundColor DarkRed
        }
    }
}

Write-Host "`n==================================================================" -ForegroundColor Cyan
Write-Host "  CHALLENGER 1: EMPIRICAL STRESS TESTS (POS TABLES & MERGE/SWITCH)" -ForegroundColor Cyan
Write-Host "==================================================================" -ForegroundColor Cyan

# ------------------------------------------------------------------------------
# SUITE 1: Table State Normalization & Empty Table Retention
# ------------------------------------------------------------------------------
Write-Host "`n--- SUITE 1: Table State Normalization & Retention ---" -ForegroundColor Yellow

$tableServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.TableService")
$tableService = [Activator]::CreateInstance($tableServiceType)
$tableRepoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DAL.TableRepository")

# Ensure dynamic reserved tables exist for testing per cafe requirements
$currentTables = $tableService.GetAllTablesWithStatus()
if ($null -eq $currentTables -or $currentTables.Count -lt 5) {
    for ($i = 2; $i -le 6; $i++) {
        $msg = ""
        $tableService.AddReservedTable("Khách 0$i", "Khu vực $i", [DateTime]::Now, [DateTime]::Now.AddHours(2), [ref]$msg)
    }
}

$allTables = $tableService.GetAllTablesWithStatus()
Assert-Stress ($null -ne $allTables) "GetAllTablesWithStatus() returns non-null list"
Assert-Stress ($allTables.Count -ge 5) "All physical dining tables returned (found $($allTables.Count))"

# Check counter exclusion
$counterFoundInList = @($allTables | Where-Object { $_.Id -eq 1 -or $_.TableName -like "*quầy*" -or $_.TableName -like "*Mang đi*" })
Assert-Stress ($counterFoundInList.Count -eq 0) "Counter / Takeaway table is excluded from dining table grid (Count: $($counterFoundInList.Count))"

# Check empty tables are never omitted
$emptyTables = @($allTables | Where-Object { $_.Status -eq "Trống" })
Assert-Stress ($emptyTables.Count -ge 1) "Empty dining tables ('Trống') are actively returned and not filtered out (Count: $($emptyTables.Count))"

# Check table state normalization with dirty inputs (null, empty, whitespace)
$tableDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.TableDTO")

# Check each table has non-empty valid status
$invalidStatusCount = 0
foreach ($t in $allTables) {
    if ([string]::IsNullOrWhiteSpace($t.Status)) {
        $invalidStatusCount++
    }
}
Assert-Stress ($invalidStatusCount -eq 0) "All tables have normalized non-whitespace status (invalid: $invalidStatusCount)"

# Test fallback table list mutation to stress normalize empty/null statuses
$fallbackField = $tableRepoType.GetField("FallbackTables", [System.Reflection.BindingFlags]"NonPublic,Public,Static")
if ($null -ne $fallbackField) {
    $fallbackList = $fallbackField.GetValue($null)
    $tableClassType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Table")
    
    # Insert dirty test table with null status
    $dirtyTable = [Activator]::CreateInstance($tableClassType)
    $dirtyTable.Id = 9991
    $dirtyTable.TableName = "Bàn Test Dirty Null"
    $dirtyTable.AreaId = 1
    $dirtyTable.Status = $null
    $fallbackList.Add($dirtyTable)
    
    # Insert dirty test table with whitespace status
    $dirtyTable2 = [Activator]::CreateInstance($tableClassType)
    $dirtyTable2.Id = 9992
    $dirtyTable2.TableName = "Bàn Test Dirty Spaces"
    $dirtyTable2.AreaId = 1
    $dirtyTable2.Status = "   "
    $fallbackList.Add($dirtyTable2)
    
    $refreshedTables = $tableService.GetAllTablesWithStatus()
    $foundDirty1 = $refreshedTables | Where-Object { $_.Id -eq 9991 }
    $foundDirty2 = $refreshedTables | Where-Object { $_.Id -eq 9992 }
    
    Assert-Stress ($foundDirty1 -ne $null -and $foundDirty1.Status -eq "Trống") "Dirty null status table normalized to 'Trống'"
    Assert-Stress ($foundDirty2 -ne $null -and $foundDirty2.Status -eq "Trống") "Dirty whitespace status table normalized to 'Trống'"
    
    # Cleanup dirty test tables
    $fallbackList.Remove($dirtyTable) | Out-Null
    $fallbackList.Remove($dirtyTable2) | Out-Null
}

# ------------------------------------------------------------------------------
# SUITE 2: Table Switching - Occupied to Empty Table
# ------------------------------------------------------------------------------
Write-Host "`n--- SUITE 2: Switch Table from Occupied to Empty ---" -ForegroundColor Yellow

$orderServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.OrderService")
$orderService = [Activator]::CreateInstance($orderServiceType)
$orderRepoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DAL.OrderRepository")

# Find two empty tables
$tablesNow = $tableService.GetAllTablesWithStatus()
$targetEmpty = @($tablesNow | Where-Object { $_.Status -eq "Trống" })

if ($targetEmpty.Count -lt 2) {
    # If not enough empty tables, create/reset
    Write-Host "  Preparing clean test tables..." -ForegroundColor Gray
}

# Use Table 4 (Bàn 02) and Table 5 (Bàn 03)
$sourceTableId = 4
$targetTableId = 5

# Ensure clean starting state for tables 4 and 5
$msgOut = ""
$activeOrder4 = $orderService.GetUnpaidOrderByTable($sourceTableId)
if ($null -ne $activeOrder4) {
    $orderService.CheckoutTableOrder($sourceTableId, "", [ref]$null, [ref]$null, [ref]$msgOut) | Out-Null
}
$activeOrder5 = $orderService.GetUnpaidOrderByTable($targetTableId)
if ($null -ne $activeOrder5) {
    $orderService.CheckoutTableOrder($targetTableId, "", [ref]$null, [ref]$null, [ref]$msgOut) | Out-Null
}

# Step 1: Add product to Table 4
$addSuccess = $orderService.AddProductToTableOrder($sourceTableId, 1, 1, 2, [ref]$msgOut)
Assert-Stress ($addSuccess) "Add product 1 (qty 2) to Table 4 succeeded" "Message: $msgOut"

$order4 = $orderService.GetUnpaidOrderByTable($sourceTableId)
Assert-Stress ($null -ne $order4) "Table 4 now has an unpaid order (OrderId: $($order4.Id))"
$t4Info = $tableService.GetTableById($sourceTableId)
Assert-Stress ($t4Info.Status -eq "Có khách") "Table 4 status updated to 'Có khách'"

# Step 2: Switch Table 4 -> Table 5 (empty)
$switchMsg = ""
$switchResult = $orderService.SwitchOrMergeTable($sourceTableId, $targetTableId, [ref]$switchMsg)
Assert-Stress ($switchResult) "Switch Table 4 -> Table 5 succeeded: '$switchMsg'"
Assert-Stress ($switchMsg -like "*chuyển bàn thành công*") "Success message correctly indicates Switch (not merge)"

# Step 3: Verify state after switch
$order4After = $orderService.GetUnpaidOrderByTable($sourceTableId)
Assert-Stress ($null -eq $order4After) "Source Table 4 has NO unpaid order after switch"
$t4After = $tableService.GetTableById($sourceTableId)
Assert-Stress ($t4After.Status -eq "Trống") "Source Table 4 status reset to 'Trống'"

$order5After = $orderService.GetUnpaidOrderByTable($targetTableId)
Assert-Stress ($null -ne $order5After) "Target Table 5 has the unpaid order"
$t5After = $tableService.GetTableById($targetTableId)
Assert-Stress ($t5After.Status -eq "Có khách") "Target Table 5 status set to 'Có khách'"

$details5 = $orderService.GetOrderDetailsByTable($targetTableId)
Assert-Stress ($details5.Count -ge 1 -and $details5[0].ProductId -eq 1 -and $details5[0].Quantity -eq 2) "Target Table 5 contains the transferred items (Product 1, qty 2)"

# ------------------------------------------------------------------------------
# SUITE 3: Table Merging - Occupied to Occupied Table
# ------------------------------------------------------------------------------
Write-Host "`n--- SUITE 3: Merge Table from Occupied to Occupied ---" -ForegroundColor Yellow

# Table 5 currently has Product 1 (qty 2).
# Let Table 6 (Bàn 04) have Product 1 (qty 3) AND Product 2 (qty 1).
$table6Id = 6
$activeOrder6 = $orderService.GetUnpaidOrderByTable($table6Id)
if ($null -ne $activeOrder6) {
    $orderService.CheckoutTableOrder($table6Id, "", [ref]$null, [ref]$null, [ref]$msgOut) | Out-Null
}

$add6_1 = $orderService.AddProductToTableOrder($table6Id, 1, 1, 3, [ref]$msgOut)
$add6_2 = $orderService.AddProductToTableOrder($table6Id, 1, 2, 1, [ref]$msgOut)
Assert-Stress ($add6_1 -and $add6_2) "Table 6 populated with Product 1 (qty 3) and Product 2 (qty 1)"

$order6Before = $orderService.GetUnpaidOrderByTable($table6Id)
Assert-Stress ($null -ne $order6Before) "Table 6 has active order"

# Merge Table 5 (qty 2 of prod 1) into Table 6 (qty 3 of prod 1, qty 1 of prod 2)
$mergeMsg = ""
$mergeResult = $orderService.SwitchOrMergeTable(5, 6, [ref]$mergeMsg)
Assert-Stress ($mergeResult) "Merge Table 5 -> Table 6 succeeded: '$mergeMsg'"
Assert-Stress ($mergeMsg -like "*gộp hóa đơn sang bàn đích thành công*") "Success message correctly indicates Merge"

# Verify source table 5 is freed
$order5PostMerge = $orderService.GetUnpaidOrderByTable(5)
Assert-Stress ($null -eq $order5PostMerge) "Source Table 5 order cleared after merge"
$t5PostMerge = $tableService.GetTableById(5)
Assert-Stress ($t5PostMerge.Status -eq "Trống") "Source Table 5 status reset to 'Trống'"

# Verify target table 6 merged quantities
$details6PostMerge = $orderService.GetOrderDetailsByTable(6)
$itemProd1 = $details6PostMerge | Where-Object { $_.ProductId -eq 1 }
$itemProd2 = $details6PostMerge | Where-Object { $_.ProductId -eq 2 }

Assert-Stress ($null -ne $itemProd1 -and $itemProd1.Quantity -eq 5) "Merged Product 1 quantity summed correctly: expected 5 (2 + 3), found $($itemProd1.Quantity)"
Assert-Stress ($null -ne $itemProd2 -and $itemProd2.Quantity -eq 1) "Merged Product 2 retained quantity: 1"

$expectedTotal = (5 * $itemProd1.UnitPrice) + (1 * $itemProd2.UnitPrice)
$order6PostMerge = $orderService.GetUnpaidOrderByTable(6)
Assert-Stress ($order6PostMerge.TotalAmount -eq $expectedTotal) "Target Table 6 TotalAmount correctly recomputed ($expectedTotal VND)"

# ------------------------------------------------------------------------------
# SUITE 4: Invalid Table IDs & Boundary Cases
# ------------------------------------------------------------------------------
Write-Host "`n--- SUITE 4: Invalid IDs & Boundary Cases ---" -ForegroundColor Yellow

# 1. Invalid source <= 0
$resInvalidSrc = $orderService.SwitchOrMergeTable(0, 6, [ref]$msgOut)
Assert-Stress (-not $resInvalidSrc) "Rejected sourceTableId = 0 ($msgOut)"

$resNegSrc = $orderService.SwitchOrMergeTable(-10, 6, [ref]$msgOut)
Assert-Stress (-not $resNegSrc) "Rejected sourceTableId = -10 ($msgOut)"

# 2. Invalid target <= 0
$resInvalidTgt = $orderService.SwitchOrMergeTable(6, 0, [ref]$msgOut)
Assert-Stress (-not $resInvalidTgt) "Rejected targetTableId = 0 ($msgOut)"

$resNegTgt = $orderService.SwitchOrMergeTable(6, -5, [ref]$msgOut)
Assert-Stress (-not $resNegTgt) "Rejected targetTableId = -5 ($msgOut)"

# 3. Source == Target
$resSame = $orderService.SwitchOrMergeTable(6, 6, [ref]$msgOut)
Assert-Stress (-not $resSame -and $msgOut -like "*khác với bàn hiện tại*") "Rejected source == target ($msgOut)"

# 4. Switch from empty table
$resEmptySrc = $orderService.SwitchOrMergeTable(4, 5, [ref]$msgOut)
Assert-Stress (-not $resEmptySrc -and $msgOut -like "*đang trống*") "Rejected switch from empty table ($msgOut)"

# 5. Non-existent source table ID
$resNonExistSrc = $orderService.SwitchOrMergeTable(99999, 6, [ref]$msgOut)
Assert-Stress (-not $resNonExistSrc) "Rejected non-existent source table ID 99999"

# 6. Non-existent target table ID
$resNonExistTgt = $orderService.SwitchOrMergeTable(6, 99999, [ref]$msgOut)
Assert-Stress (-not $resNonExistTgt) "Rejected non-existent target table ID 99999"

# ------------------------------------------------------------------------------
# SUITE 5: Consecutive Operations & Concurrency (A -> B -> C -> Checkout)
# ------------------------------------------------------------------------------
Write-Host "`n--- SUITE 5: Consecutive Chain (A -> B -> C -> Checkout) ---" -ForegroundColor Yellow

# Clean tables 4, 5, 7
$orderService.CheckoutTableOrder(6, "", [ref]$null, [ref]$null, [ref]$msgOut) | Out-Null
$orderService.CheckoutTableOrder(4, "", [ref]$null, [ref]$null, [ref]$msgOut) | Out-Null
$orderService.CheckoutTableOrder(5, "", [ref]$null, [ref]$null, [ref]$msgOut) | Out-Null
$orderService.CheckoutTableOrder(7, "", [ref]$null, [ref]$null, [ref]$msgOut) | Out-Null

$tableA = 4
$tableB = 5
$tableC = 7

# Start: Place order on A
$orderService.AddProductToTableOrder($tableA, 1, 1, 4, [ref]$msgOut) | Out-Null
Assert-Stress (($tableService.GetTableById($tableA)).Status -eq "Có khách") "Step 1: Table A (4) occupied"
Assert-Stress (($tableService.GetTableById($tableB)).Status -eq "Trống") "Step 1: Table B (5) empty"
Assert-Stress (($tableService.GetTableById($tableC)).Status -eq "Trống") "Step 1: Table C (7) empty"

# Chain 1: Switch A -> B
$chain1 = $orderService.SwitchOrMergeTable($tableA, $tableB, [ref]$msgOut)
Assert-Stress ($chain1) "Step 2: Switch A -> B succeeded ($msgOut)"
Assert-Stress (($tableService.GetTableById($tableA)).Status -eq "Trống") "Step 2: Table A reset to 'Trống'"
Assert-Stress (($tableService.GetTableById($tableB)).Status -eq "Có khách") "Step 2: Table B set to 'Có khách'"
Assert-Stress (($tableService.GetTableById($tableC)).Status -eq "Trống") "Step 2: Table C still 'Trống'"

# Chain 2: Switch B -> C
$chain2 = $orderService.SwitchOrMergeTable($tableB, $tableC, [ref]$msgOut)
Assert-Stress ($chain2) "Step 3: Switch B -> C succeeded ($msgOut)"
Assert-Stress (($tableService.GetTableById($tableA)).Status -eq "Trống") "Step 3: Table A remains 'Trống'"
Assert-Stress (($tableService.GetTableById($tableB)).Status -eq "Trống") "Step 3: Table B reset to 'Trống'"
Assert-Stress (($tableService.GetTableById($tableC)).Status -eq "Có khách") "Step 3: Table C set to 'Có khách'"

# Verify C details
$detailsC = $orderService.GetOrderDetailsByTable($tableC)
Assert-Stress ($detailsC.Count -ge 1 -and $detailsC[0].Quantity -eq 4) "Step 3: Table C has preserved the original 4 items"

# Step 4: Checkout Table C
$paidOrderC = $null
$paidDetailsC = $null
$checkoutSuccess = $orderService.CheckoutTableOrder($tableC, "", [ref]$paidOrderC, [ref]$paidDetailsC, [ref]$msgOut)
Assert-Stress ($checkoutSuccess) "Step 4: Checkout Table C succeeded ($msgOut)"
Assert-Stress (($tableService.GetTableById($tableC)).Status -eq "Trống") "Step 4: Table C status reset to 'Trống' upon payment"

# Step 5: Verify floor map retention: A, B, C must all be present in GetAllTablesWithStatus()
$finalTables = $tableService.GetAllTablesWithStatus()
$foundA = $finalTables | Where-Object { $_.Id -eq $tableA }
$foundB = $finalTables | Where-Object { $_.Id -eq $tableB }
$foundC = $finalTables | Where-Object { $_.Id -eq $tableC }

Assert-Stress ($foundA -ne $null -and $foundA.Status -eq "Trống") "Table A remains on floor map with status 'Trống'"
Assert-Stress ($foundB -ne $null -and $foundB.Status -eq "Trống") "Table B remains on floor map with status 'Trống'"
Assert-Stress ($foundC -ne $null -and $foundC.Status -eq "Trống") "Table C remains on floor map with status 'Trống'"

# ------------------------------------------------------------------------------
# SUITE 6: Reservation Interaction & Accidental Cancel Protection
# ------------------------------------------------------------------------------
Write-Host "`n--- SUITE 6: Reservation Countdown & Accidental Cancel Protection ---" -ForegroundColor Yellow

$cardType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.UcReservedTableCard")
$card = [Activator]::CreateInstance($cardType)

# 1. Test Reserved Table Card: must show remaining countdown minutes and cancel button
$resTable = [Activator]::CreateInstance($tableDtoType)
$resTable.Id = 201
$resTable.TableName = "Bàn 01 - Đặt trước VIP"
$resTable.AreaName = "Khu máy lạnh"
$resTable.Status = "Đặt trước"
$resTable.StartTime = [DateTime]::Now.AddMinutes(-10)
$resTable.EndTime = [DateTime]::Now.AddMinutes(50)

$card.BindData($resTable, $false)

$lblTimeRangeField = $cardType.GetField("lblTimeRange", [System.Reflection.BindingFlags]"NonPublic,Instance")
$lblTimeRange = $lblTimeRangeField.GetValue($card)
$btnCancelField = $cardType.GetField("btnCancelTable", [System.Reflection.BindingFlags]"NonPublic,Instance")
$btnCancel = $btnCancelField.GetValue($card)

Assert-Stress ($lblTimeRange.Text -like "*còn *p*") "Reserved table card shows remaining countdown minutes (Value: '$($lblTimeRange.Text)')"
Assert-Stress ($btnCancel.Visible -eq $true) "Reserved table card displays cancel button (Visible = True)"

# 2. Test Empty Dining Table Card: cancel button MUST BE HIDDEN
$emptyTable = [Activator]::CreateInstance($tableDtoType)
$emptyTable.Id = 202
$emptyTable.TableName = "Bàn 02"
$emptyTable.AreaName = "Sân vườn"
$emptyTable.Status = "Trống"

$card.BindData($emptyTable, $false)
Assert-Stress ($btnCancel.Visible -eq $false) "Empty dining table HIDES cancel button (Visible = False, prevents accidental deletion)"
Assert-Stress ($lblTimeRange.Text -like "*Bàn trống*") "Empty dining table displays green ready indicator: '$($lblTimeRange.Text)'"

# 3. Test Occupied Dining Table Card: cancel button MUST BE HIDDEN
$occTable = [Activator]::CreateInstance($tableDtoType)
$occTable.Id = 203
$occTable.TableName = "Bàn 03"
$occTable.AreaName = "Sân vườn"
$occTable.Status = "Có khách"

$card.BindData($occTable, $false)
Assert-Stress ($btnCancel.Visible -eq $false) "Occupied dining table HIDES cancel button (Visible = False, prevents accidental deletion)"
Assert-Stress ($lblTimeRange.Text -like "*Đang phục vụ*") "Occupied dining table displays active service indicator: '$($lblTimeRange.Text)'"

# 4. Test TableService DeleteTable Protection
# - Attempt to delete counter table (Id = 1) -> must fail
$delCounter = $tableService.DeleteTable(1, [ref]$msgOut)
Assert-Stress (-not $delCounter -and $msgOut -like "*Không thể xóa chế độ Khách tại quầy*") "Guard: Cannot delete default Counter table ($msgOut)"

# - Attempt to delete table with unpaid order -> must fail
$orderService.AddProductToTableOrder(4, 1, 1, 1, [ref]$msgOut) | Out-Null
$delOccupied = $tableService.DeleteTable(4, [ref]$msgOut)
Assert-Stress (-not $delOccupied -and $msgOut -like "*đang có hóa đơn chưa thanh toán*") "Guard: Cannot delete table with active unpaid order ($msgOut)"

# Cleanup order on Table 4
$orderService.CheckoutTableOrder(4, "", [ref]$null, [ref]$null, [ref]$msgOut) | Out-Null
$card.Dispose()

# ------------------------------------------------------------------------------
# SUITE 7: UI Dialog Integrity & Action Binding
# ------------------------------------------------------------------------------
Write-Host "`n--- SUITE 7: FrmSwitchTable & FrmOrder Action Integration ---" -ForegroundColor Yellow

$frmSwitchType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.FrmSwitchTable")
$frmSwitchDlg = [Activator]::CreateInstance($frmSwitchType, @([int]4, [string]"Bàn 02", [bool]$true))

# Check merge mode initialization
$rdoMergeField = $frmSwitchType.GetField("rdoMerge", [System.Reflection.BindingFlags]"NonPublic,Instance")
$rdoMerge = $rdoMergeField.GetValue($frmSwitchDlg)
$rdoSwitchField = $frmSwitchType.GetField("rdoSwitch", [System.Reflection.BindingFlags]"NonPublic,Instance")
$rdoSwitch = $rdoSwitchField.GetValue($frmSwitchDlg)

# Trigger Load event via reflection
$loadMethod = $frmSwitchType.GetMethod("FrmSwitchTable_Load", [System.Reflection.BindingFlags]"NonPublic,Instance")
$loadMethod.Invoke($frmSwitchDlg, @($frmSwitchDlg, [EventArgs]::Empty))

Assert-Stress ($rdoMerge.Checked -eq $true) "FrmSwitchTable initialized with Merge mode checked when isMergeMode=true"
Assert-Stress ($frmSwitchDlg.IsMergeMode -eq $true) "FrmSwitchTable.IsMergeMode property returns True"

# Toggle to Switch
$rdoSwitch.Checked = $true
Assert-Stress ($frmSwitchDlg.IsMergeMode -eq $false) "FrmSwitchTable.IsMergeMode toggles to False on switch mode"

$cboField = $frmSwitchType.GetField("cboTargetTable", [System.Reflection.BindingFlags]"NonPublic,Instance")
$cboTarget = $cboField.GetValue($frmSwitchDlg)
Assert-Stress ($cboTarget.Items.Count -ge 1 -or $cboTarget.DataSource -ne $null) "cboTargetTable populated with valid target tables"

$frmSwitchDlg.Dispose()

# ------------------------------------------------------------------------------
# SUMMARY
# ------------------------------------------------------------------------------
Write-Host "`n==================================================================" -ForegroundColor Cyan
$summaryColor = if ($testsFailed -eq 0) { "Green" } else { "Red" }
Write-Host "CHALLENGER 1 STRESS SUMMARY: $testsPassed / $testsRun passed ($testsFailed failed)" -ForegroundColor $summaryColor
Write-Host "==================================================================" -ForegroundColor Cyan

if ($testsFailed -gt 0) {
    exit 1
}
exit 0

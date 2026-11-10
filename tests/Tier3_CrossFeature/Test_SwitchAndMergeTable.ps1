# ==============================================================================
# Tier 3 - Test_SwitchAndMergeTable.ps1
# Cross-Feature: Table Switching & Table Merging Multi-Table Coordination
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 3: Switch & Merge Table Multi-Table Workflows ---" -ForegroundColor Cyan

$asm = Get-ProjectAssembly
$orderServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.OrderService")
$tableRepoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DAL.TableRepository")
$tableType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Table")

$orderService = [Activator]::CreateInstance($orderServiceType)
$tableRepo = [Activator]::CreateInstance($tableRepoType)

# Ensure Tables 5, 6, 7 exist in FallbackTables for isolated multi-table workflow testing
$fallbackTablesField = $tableRepoType.GetField("FallbackTables", [System.Reflection.BindingFlags]"NonPublic,Static")
if ($fallbackTablesField) {
    $fallbackTables = $fallbackTablesField.GetValue($null)
    foreach ($tid in @(5, 6, 7)) {
        $found = $fallbackTables | Where-Object { $_.Id -eq $tid }
        if (-not $found) {
            $newT = [Activator]::CreateInstance($tableType)
            $newT.Id = $tid
            $newT.TableName = "Bàn 0$tid"
            $newT.AreaId = 1
            $newT.Status = "Trống"
            $fallbackTables.Add($newT)
        } else {
            $found.Status = "Trống"
        }
    }
}

$tableA = 5
$tableB = 6
$tableC = 7

# Clear any lingering unpaid orders on Tables A, B, C
foreach ($tId in @($tableA, $tableB, $tableC)) {
    $existing = $orderService.GetUnpaidOrderByTable($tId)
    if ($existing) {
        $p = $null; $dt = $null; $cm = ""
        $orderService.CheckoutTableOrder($tId, "", [ref]$p, [ref]$dt, [ref]$cm)
    }
}

# Setup for Table A: Add 2x Product 1 (Cà phê đen đá)
$msg = ""
$addOk = $orderService.AddProductToTableOrder($tableA, 1, 1, 2, [ref]$msg)
Assert-True $addOk "Setup: Added 2x Product 1 to Table $tableA" "Tier 3" "Switch_Merge_Table"

$orderA = $orderService.GetUnpaidOrderByTable($tableA)
Assert-NotNull $orderA "Setup: Table $tableA created unpaid order" "Tier 3" "Switch_Merge_Table"

# Test 3.1: Switch Table (Table A -> Table B)
$swMsg = ""
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$switchSuccess = $orderService.SwitchOrMergeTable($tableA, $tableB, [ref]$swMsg)
$sw.Stop()

Assert-True $switchSuccess "Switch Table $tableA -> Table $tableB succeeded" "Tier 3" "Switch_Merge_Table" -DurationMs $sw.TotalMilliseconds
Assert-Match $swMsg "chuyển bàn" "Confirmation message states table switched" "Tier 3" "Switch_Merge_Table"

# Verify Table A is now empty and Table B holds the order
$orderAAfter = $orderService.GetUnpaidOrderByTable($tableA)
$orderBAfter = $orderService.GetUnpaidOrderByTable($tableB)
Assert-Equal $orderAAfter $null "Source Table $tableA is now empty" "Tier 3" "Switch_Merge_Table"
Assert-NotNull $orderBAfter "Destination Table $tableB now holds the order" "Tier 3" "Switch_Merge_Table"

$detailsB = $orderService.GetOrderDetailsByTable($tableB)
$itemP1 = $detailsB | Where-Object { $_.ProductId -eq 1 }
Assert-Equal $itemP1.Quantity 2 "Table $tableB order retains 2x Product 1" "Tier 3" "Switch_Merge_Table"

# Test 3.2: Merge Table (Table B -> Table C)
# First give Table C an existing order: 1x Product 1, 1x Product 2
$orderService.AddProductToTableOrder($tableC, 1, 1, 1, [ref]$msg)
$orderService.AddProductToTableOrder($tableC, 1, 2, 1, [ref]$msg)

$detailsCBefore = $orderService.GetOrderDetailsByTable($tableC)
Assert-Equal $detailsCBefore.Count 2 "Target Table $tableC has 2 items before merge" "Tier 3" "Switch_Merge_Table"

# Now merge Table B into Table C
$mergeMsg = ""
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$mergeSuccess = $orderService.SwitchOrMergeTable($tableB, $tableC, [ref]$mergeMsg)
$sw.Stop()

Assert-True $mergeSuccess "Merge Table $tableB into Table $tableC succeeded" "Tier 3" "Switch_Merge_Table" -DurationMs $sw.TotalMilliseconds
Assert-Match $mergeMsg "gộp" "Confirmation message states tables merged" "Tier 3" "Switch_Merge_Table"

# Verify Table B is emptied and Table C has combined quantities
$orderBAfterMerge = $orderService.GetUnpaidOrderByTable($tableB)
Assert-Equal $orderBAfterMerge $null "Source Table $tableB is empty after merge" "Tier 3" "Switch_Merge_Table"

$detailsCAfter = $orderService.GetOrderDetailsByTable($tableC)
$mergedP1 = $detailsCAfter | Where-Object { $_.ProductId -eq 1 }
$mergedP2 = $detailsCAfter | Where-Object { $_.ProductId -eq 2 }

# Product 1 was 2 from Table B + 1 from Table C = 3
Assert-Equal $mergedP1.Quantity 3 "Product 1 quantity merged: 2 (from Table B) + 1 (from Table C) = 3" "Tier 3" "Switch_Merge_Table"
Assert-Equal $mergedP2.Quantity 1 "Product 2 quantity remains 1" "Tier 3" "Switch_Merge_Table"

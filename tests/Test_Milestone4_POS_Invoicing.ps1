# ==============================================================================
# TEST SUITE: Milestone 4 - POS Table & Invoicing Workflow
# ==============================================================================

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
. (Join-Path $scriptDir "Common\TestHarness.ps1")

$asm = Get-ProjectAssembly

$testsRun = 0
$testsPassed = 0
$testsFailed = 0

function Assert-M4([bool]$condition, [string]$testName) {
    $script:testsRun++
    if ($condition) {
        $script:testsPassed++
        Write-Host "  [PASS] $testName" -ForegroundColor Green
    } else {
        $script:testsFailed++
        Write-Host "  [FAIL] $testName" -ForegroundColor Red
    }
}

Write-Host "`n=== MILESTONE 4 EMPIRICAL VERIFICATION SUITE ===" -ForegroundColor Cyan

# ------------------------------------------------------------------------------
# 1. POS Table Visibility & Status Tests
# ------------------------------------------------------------------------------
Write-Host "`n--- 1. POS Table Visibility & Normalized Statuses ---" -ForegroundColor Yellow

$tableServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.TableService")
$tableService = [Activator]::CreateInstance($tableServiceType)
$allTables = $tableService.GetAllTablesWithStatus()

Assert-M4 ($null -ne $allTables) "GetAllTablesWithStatus() returns non-null list"
Assert-M4 ($allTables.Count -ge 6) "GetAllTablesWithStatus() returns all cafe tables (Count: $($allTables.Count))"

# Check if tables include empty tables (so empty tables do NOT disappear)
$emptyTables = @($allTables | Where-Object { $_.Status -eq "Trống" })
Assert-M4 ($emptyTables.Count -ge 1) "Empty tables ('Trống') are returned and not filtered out (Count: $($emptyTables.Count))"

# Check table names include standard tables (e.g. Bàn 1, Bàn 2, or Bàn 01)
$tableNames = @($allTables | ForEach-Object { $_.TableName })
$hasStandardTables = ($tableNames | Where-Object { $_ -like "Bàn*" }).Count -ge 5
Assert-M4 ($hasStandardTables) "Tables include standard dining tables (e.g. $($tableNames -join ', '))"

# ------------------------------------------------------------------------------
# 2. Switch & Merge Table UI & Logic Tests
# ------------------------------------------------------------------------------
Write-Host "`n--- 2. Switch & Merge Table Modal & Integration ---" -ForegroundColor Yellow

# Test FrmSwitchTable exists and has necessary controls
$frmSwitchType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.FrmSwitchTable")
Assert-M4 ($frmSwitchType -ne $null) "FrmSwitchTable class exists in assembly"
$frmSwitch = [Activator]::CreateInstance($frmSwitchType, @([int]2, [string]"Bàn 2", [bool]$false))
Assert-M4 ($frmSwitch -ne $null) "FrmSwitchTable instance created successfully"
Assert-M4 ($frmSwitch.Controls.Find("cboTargetTable", $true).Length -eq 1) "FrmSwitchTable contains 'cboTargetTable'"
Assert-M4 ($frmSwitch.Controls.Find("rdoSwitch", $true).Length -eq 1) "FrmSwitchTable contains 'rdoSwitch'"
Assert-M4 ($frmSwitch.Controls.Find("rdoMerge", $true).Length -eq 1) "FrmSwitchTable contains 'rdoMerge'"
Assert-M4 ($frmSwitch.Controls.Find("btnConfirm", $true).Length -eq 1) "FrmSwitchTable contains 'btnConfirm'"
Assert-M4 ($frmSwitch.Controls.Find("btnCancel", $true).Length -eq 1) "FrmSwitchTable contains 'btnCancel'"
$frmSwitch.Dispose()

# Test FrmOrder has Switch and Merge Table action buttons
$frmOrderType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.FrmOrder")
Assert-M4 ($frmOrderType.GetField("btnSwitchTable", [System.Reflection.BindingFlags]"NonPublic,Instance") -ne $null) "FrmOrder has 'btnSwitchTable' field"
Assert-M4 ($frmOrderType.GetField("btnMergeTable", [System.Reflection.BindingFlags]"NonPublic,Instance") -ne $null) "FrmOrder has 'btnMergeTable' field"

# ------------------------------------------------------------------------------
# 3. Cash Tendered & Change Due Real-time Calculation & Storage Tests
# ------------------------------------------------------------------------------
Write-Host "`n--- 3. Cash Tendered & Change Due Workflow ---" -ForegroundColor Yellow

# Check FrmOrder controls for cash tendered & change due
Assert-M4 ($frmOrderType.GetField("txtCashTendered", [System.Reflection.BindingFlags]"NonPublic,Instance") -ne $null) "FrmOrder has 'txtCashTendered' field"
Assert-M4 ($frmOrderType.GetField("lblChangeDue", [System.Reflection.BindingFlags]"NonPublic,Instance") -ne $null) "FrmOrder has 'lblChangeDue' field"
Assert-M4 ($frmOrderType.GetField("btnCashExact", [System.Reflection.BindingFlags]"NonPublic,Instance") -ne $null) "FrmOrder has 'btnCashExact' field"
Assert-M4 ($frmOrderType.GetField("btnCash50k", [System.Reflection.BindingFlags]"NonPublic,Instance") -ne $null) "FrmOrder has 'btnCash50k' field"
Assert-M4 ($frmOrderType.GetField("btnCash100k", [System.Reflection.BindingFlags]"NonPublic,Instance") -ne $null) "FrmOrder has 'btnCash100k' field"
Assert-M4 ($frmOrderType.GetField("btnCash200k", [System.Reflection.BindingFlags]"NonPublic,Instance") -ne $null) "FrmOrder has 'btnCash200k' field"
Assert-M4 ($frmOrderType.GetField("btnCash500k", [System.Reflection.BindingFlags]"NonPublic,Instance") -ne $null) "FrmOrder has 'btnCash500k' field"

# Test OrderDTO CashTendered and ChangeDue properties
$orderDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.OrderDTO")
$orderDto = [Activator]::CreateInstance($orderDtoType)
$orderDto.TotalAmount = [decimal]100000
$orderDto.FinalAmount = [decimal]90000
$orderDto.CashTendered = [decimal]100000
$orderDto.ChangeDue = [decimal]10000
Assert-M4 ($orderDto.CashTendered -eq 100000) "OrderDTO stores CashTendered (100,000 VND)"
Assert-M4 ($orderDto.ChangeDue -eq 10000) "OrderDTO stores ChangeDue (10,000 VND)"

# ------------------------------------------------------------------------------
# 4. Print Preview & RDLC Invoice Verification Tests
# ------------------------------------------------------------------------------
Write-Host "`n--- 4. K80 Thermal Preview & RDLC Invoice Parameters ---" -ForegroundColor Yellow

# Verify RDLC Report Parameters
$rdlcPath = Join-Path $global:ProjectRoot "Form\TanPhat\RptInvoiceK80.rdlc"
[xml]$rdlcXml = Get-Content $rdlcPath
$paramNames = @($rdlcXml.Report.ReportParameters.ReportParameter | ForEach-Object { $_.Name })
Assert-M4 ($paramNames -contains "pCashTendered") "RptInvoiceK80.rdlc defines 'pCashTendered' ReportParameter"
Assert-M4 ($paramNames -contains "pChangeDue") "RptInvoiceK80.rdlc defines 'pChangeDue' ReportParameter"

# Verify FrmPrintReview controls
$frmPrintType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.FrmPrintReview")
Assert-M4 ($frmPrintType -ne $null) "FrmPrintReview class exists in assembly"
Assert-M4 ($frmPrintType.GetField("lblCashTenderedValue", [System.Reflection.BindingFlags]"NonPublic,Instance") -ne $null) "FrmPrintReview has 'lblCashTenderedValue' field"
Assert-M4 ($frmPrintType.GetField("lblChangeDueValue", [System.Reflection.BindingFlags]"NonPublic,Instance") -ne $null) "FrmPrintReview has 'lblChangeDueValue' field"

# Test GDI+ DrawInvoiceK80 rendering with cash tendered and change due
$bmp = New-Object System.Drawing.Bitmap 300, 600
$g = [System.Drawing.Graphics]::FromImage($bmp)
$orderDetailDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.OrderDetailDTO")
$detailsListType = [Type]::GetType("System.Collections.Generic.List``1").MakeGenericType($orderDetailDtoType)
$details = [Activator]::CreateInstance($detailsListType)

$det1 = [Activator]::CreateInstance($orderDetailDtoType)
$det1.ProductName = "Cà phê sữa đá"
$det1.Quantity = [int]2
$det1.UnitPrice = [decimal]29000
$det1.TotalPrice = [decimal]58000
$details.Add($det1)

$renderException = $null
$renderSuccess = $false
try {
    $printHelperType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Utilities.PrintHelper")
    $drawMethod = $printHelperType.GetMethod("DrawInvoiceK80", [System.Reflection.BindingFlags]"Public,Static")
    $drawMethod.Invoke($null, @($g, $orderDto, $details, [int]280)) | Out-Null
    $renderSuccess = $true
} catch {
    $renderException = $_.Exception.ToString()
    $renderSuccess = $false
} finally {
    $g.Dispose()
    $bmp.Dispose()
}
Assert-M4 ($renderSuccess) "PrintHelper.DrawInvoiceK80 renders without exception with CashTendered & ChangeDue"

# ------------------------------------------------------------------------------
# Summary
# ------------------------------------------------------------------------------
Write-Host "`n=================================================================="
$summaryColor = if ($testsFailed -eq 0) { "Green" } else { "Red" }
Write-Host "MILESTONE 4 TEST SUMMARY: $testsPassed / $testsRun passed ($testsFailed failed)" -ForegroundColor $summaryColor
Write-Host "=================================================================="

if ($testsFailed -gt 0) {
    exit 1
}
exit 0

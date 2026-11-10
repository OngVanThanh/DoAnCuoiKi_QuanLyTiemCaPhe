# ==============================================================================
# Tier 1 - Test_PrintTemplates.ps1
# Rubric: In ấn (Printing & Invoicing) - RDLC Templates & GDI+ K80 Thermal Canvas
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 1: Print Templates & Invoicing Tests ---" -ForegroundColor Cyan

# Test 1.1: RDLC Thermal K80 Invoice Template Verification
$invoiceRdlcPath = Join-Path $global:ProjectRoot "Form\TanPhat\RptInvoiceK80.rdlc"
$sw = [System.Diagnostics.Stopwatch]::StartNew()
Assert-True (Test-Path $invoiceRdlcPath) "RptInvoiceK80.rdlc exists" "Tier 1" "Print_Templates" -DurationMs $sw.ElapsedMilliseconds

[xml]$invoiceXml = Get-Content $invoiceRdlcPath
Assert-NotNull $invoiceXml.Report "RptInvoiceK80.rdlc is valid XML Report Definition" "Tier 1" "Print_Templates"

# Verify 12 required ReportParameters
$params = @($invoiceXml.Report.ReportParameters.ReportParameter | ForEach-Object { $_.Name })
$expectedParams = @(
    "pShopName", "pShopAddress", "pShopPhone", "pOrderId",
    "pTableName", "pOrderDate", "pCashier", "pTotalAmount",
    "pDiscountAmount", "pVatAmount", "pFinalAmount", "pWifiInfo"
)

foreach ($ep in $expectedParams) {
    Assert-True ($params -contains $ep) "RptInvoiceK80 contains parameter '$ep'" "Tier 1" "Print_Templates"
}

# Test 1.2: RDLC Management Word Reports Verification
$wordReports = @("RptRevenue.rdlc", "RptBestSeller.rdlc", "RptTopCustomers.rdlc")
foreach ($rpt in $wordReports) {
    $rptPath = Join-Path $global:ProjectRoot "Form\QuangHung\$rpt"
    $exists = Test-Path $rptPath
    Assert-True $exists "Management report '$rpt' exists in Form\QuangHung\" "Tier 1" "Print_Templates"
    if ($exists) {
        [xml]$rptXml = Get-Content $rptPath
        Assert-NotNull $rptXml.Report "Report '$rpt' is well-formed XML" "Tier 1" "Print_Templates"
    }
}

# Test 1.3: GDI+ Thermal K80 Canvas Rendering Test
Add-Type -AssemblyName System.Drawing
$asm = Get-ProjectAssembly
$orderDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.OrderDTO")
$detailDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.OrderDetailDTO")
$printHelperType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Utilities.PrintHelper")

Assert-NotNull $printHelperType "PrintHelper utility class exists" "Tier 1" "Print_Templates"

$order = [Activator]::CreateInstance($orderDtoType)
$order.Id = 8888
$order.TableName = "Bàn 05"
$order.EmployeeName = "Trần Thị Thu Ngân"
$order.OrderDate = [DateTime]::Now
$order.TotalAmount = [decimal]100000
$order.DiscountAmount = [decimal]10000
$order.VATPercent = [decimal]8
$order.VATAmount = [decimal]7200
$order.FinalAmount = [decimal]97200
$order.PromoCode = "VIPCOFFEE (-15%)"

$detailsListType = [System.Collections.Generic.List``1].MakeGenericType(@($detailDtoType))
$details = [Activator]::CreateInstance($detailsListType)

$d1 = [Activator]::CreateInstance($detailDtoType)
$d1.ProductName = "Cà phê sữa đá"
$d1.Quantity = 2
$d1.UnitPrice = [decimal]29000
$d1.TotalPrice = [decimal]58000
$details.Add($d1)

$d2 = [Activator]::CreateInstance($detailDtoType)
$d2.ProductName = "Trà đào cam sả"
$d2.Quantity = 1
$d2.UnitPrice = [decimal]40000
$d2.TotalPrice = [decimal]40000
$details.Add($d2)

$sw = [System.Diagnostics.Stopwatch]::StartNew()
$bmp = New-Object System.Drawing.Bitmap 400, 800
$g = [System.Drawing.Graphics]::FromImage($bmp)
$g.Clear([System.Drawing.Color]::White)

$drawMethod = $printHelperType.GetMethod("DrawInvoiceK80", [System.Reflection.BindingFlags]"Public,Static", $null, @([System.Drawing.Graphics], $orderDtoType, $detailsListType, [int]), $null)
$drawMethod.Invoke($null, @($g, $order, $details, 400))
$sw.Stop()

# Sample pixel color check to verify graphics painted content onto bitmap
$pixelNonWhiteFound = $false
for ($y = 20; $y -lt 300; $y += 10) {
    for ($x = 50; $x -lt 350; $x += 10) {
        $c = $bmp.GetPixel($x, $y)
        if ($c.R -lt 200 -or $c.G -lt 200 -or $c.B -lt 200) {
            $pixelNonWhiteFound = $true
            break
        }
    }
    if ($pixelNonWhiteFound) { break }
}

$g.Dispose()
$bmp.Dispose()

Assert-True $pixelNonWhiteFound "GDI+ DrawInvoiceK80 rendered receipt content to canvas" "Tier 1" "Print_Templates" -DurationMs $sw.TotalMilliseconds

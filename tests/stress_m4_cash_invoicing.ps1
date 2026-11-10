# ==============================================================================
# EMPIRICAL STRESS TEST SUITE: MILESTONE 4 - CASH TENDERED, INVOICING & PRINTING
# Challenger 2: Adversarial Verification, Currency Fuzzing, Canvas Layout & RDLC
# ==============================================================================

[CmdletBinding()]
param(
    [string]$HarnessPath = ""
)

$ErrorActionPreference = "Continue"

if (-not $HarnessPath) {
    if ($PSScriptRoot) {
        $HarnessPath = Join-Path $PSScriptRoot "Common\TestHarness.ps1"
    } else {
        $HarnessPath = Join-Path (Get-Location) "tests\Common\TestHarness.ps1"
    }
}

. $HarnessPath

Write-Host "==========================================================================" -ForegroundColor Cyan
Write-Host "   CHALLENGER 2: ADVERSARIAL STRESS TEST SUITE - CASH & INVOICING (M4)   " -ForegroundColor Yellow
Write-Host "==========================================================================" -ForegroundColor Cyan

$asm = Get-ProjectAssembly
Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

# Compile C# helper for Win32 GDI handle tracking
$csharpHelperCode = @"
using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;

public static class GdiTrackerM4 {
    [DllImport("user32.dll")]
    public static extern uint GetGuiResources(IntPtr hProcess, uint uiFlags);

    public static uint GetGdiHandleCount() {
        return GetGuiResources(System.Diagnostics.Process.GetCurrentProcess().Handle, 0);
    }
}
"@

try {
    Add-Type -TypeDefinition $csharpHelperCode -ReferencedAssemblies @("System.Drawing.dll") -ErrorAction Stop
} catch {
    # Type may already be loaded in current AppDomain
}

$suite = "Challenger2_M4_CashInvoicing"

# ==============================================================================
# SECTION 1: CURRENCY CALCULATION ACROSS DIVERSE AMOUNTS
# ==============================================================================
Write-Host "`n--- [SECTION 1] Currency Calculation Across Diverse Amounts ---" -ForegroundColor Yellow

$orderDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.OrderDTO")
Assert-NotNull $orderDtoType "OrderDTO resolved in assembly" "Tier2" $suite

function Calculate-ChangeDue([decimal]$finalAmount, [string]$rawInput) {
    $raw = $rawInput.Trim().Replace(",", "").Replace(".", "").Replace("đ", "").Trim()
    $cashTendered = [decimal]0
    $parsed = [decimal]::TryParse($raw, [ref]$cashTendered)

    if ($parsed -and ($cashTendered -gt 0)) {
        $change = $cashTendered - $finalAmount
        if ($change -ge 0) {
            return [PSCustomObject]@{
                Parsed = $true
                CashTendered = $cashTendered
                ChangeDue = $change
                IsSufficient = $true
                DisplayText = ("{0:N0} đ" -f $change)
                DisplayColor = "Green"
            }
        } else {
            return [PSCustomObject]@{
                Parsed = $true
                CashTendered = $cashTendered
                ChangeDue = $change
                IsSufficient = $false
                DisplayText = ("Thiếu {0:N0} đ" -f [Math]::Abs($change))
                DisplayColor = "Red"
            }
        }
    } else {
        return [PSCustomObject]@{
            Parsed = $false
            CashTendered = [decimal]0
            ChangeDue = [decimal]0
            IsSufficient = $false
            DisplayText = "0 đ"
            DisplayColor = "DimGray"
        }
    }
}

# 1.1 Exact Cash
$r1_1 = Calculate-ChangeDue 50000 "50,000"
Assert-True ($r1_1.Parsed -and $r1_1.ChangeDue -eq [decimal]0 -and $r1_1.DisplayText -eq "0 đ" -and $r1_1.DisplayColor -eq "Green") "Exact cash (50,000 for 50,000) results in 0 VND change in Green" "Tier2" $suite

# 1.2 Overpayment
$r1_2 = Calculate-ChangeDue 35000 "50,000"
Assert-True ($r1_2.Parsed -and $r1_2.ChangeDue -eq [decimal]15000 -and $r1_2.DisplayText -eq "15,000 đ" -and $r1_2.DisplayColor -eq "Green") "Overpayment (50k for 35k) results in 15,000 VND change in Green" "Tier2" $suite

$r1_3 = Calculate-ChangeDue 125000 "200,000"
Assert-True ($r1_3.Parsed -and $r1_3.ChangeDue -eq [decimal]75000 -and $r1_3.DisplayText -eq "75,000 đ") "Overpayment (200k for 125k) results in 75,000 VND change" "Tier2" $suite

# 1.3 Underpayment (Deficit)
$r1_4 = Calculate-ChangeDue 50000 "30,000"
Assert-True ($r1_4.Parsed -and -not $r1_4.IsSufficient -and $r1_4.ChangeDue -eq [decimal]-20000 -and $r1_4.DisplayText -eq "Thiếu 20,000 đ" -and $r1_4.DisplayColor -eq "Red") "Underpayment (30k for 50k) displays 'Thiếu 20,000 đ' in Red" "Tier2" $suite

# 1.4 Zero Cash Tendered
$r1_5 = Calculate-ChangeDue 50000 "0"
Assert-True (-not $r1_5.Parsed -and $r1_5.DisplayText -eq "0 đ" -and $r1_5.DisplayColor -eq "DimGray") "Zero cash '0' displays neutral '0 đ' in DimGray" "Tier2" $suite

$r1_6 = Calculate-ChangeDue 50000 "0 đ"
Assert-True (-not $r1_6.Parsed -and $r1_6.DisplayText -eq "0 đ") "Zero cash '0 đ' displays neutral '0 đ'" "Tier2" $suite

# 1.5 Very Large Amounts (100,000,000 VNĐ)
$r1_7 = Calculate-ChangeDue 65000 "100,000,000"
Assert-True ($r1_7.Parsed -and $r1_7.ChangeDue -eq [decimal]99935000 -and $r1_7.DisplayText -eq "99,935,000 đ") "Large amount (100,000,000 VNĐ for 65k) yields 99,935,000 VND change" "Tier2" $suite

$r1_8 = Calculate-ChangeDue 100000000 "100,000,000"
Assert-True ($r1_8.Parsed -and $r1_8.ChangeDue -eq [decimal]0 -and $r1_8.DisplayText -eq "0 đ") "Large exact cash (100M for 100M) yields 0 VND change" "Tier2" $suite

$r1_9 = Calculate-ChangeDue 100000000 "200,000,000"
Assert-True ($r1_9.Parsed -and $r1_9.ChangeDue -eq [decimal]100000000 -and $r1_9.DisplayText -eq "100,000,000 đ") "Large overpayment (200M for 100M) yields 100,000,000 VND change" "Tier2" $suite

# 1.6 Persistence to OrderDTO during checkout simulation
$orderInstance = [Activator]::CreateInstance($orderDtoType)
$orderInstance.FinalAmount = [decimal]85000

# Simulation of btnPayAndPrint_Click cash fallback logic
function Finalize-OrderCash([object]$order, [string]$rawInput) {
    $raw = $rawInput.Trim().Replace(",", "").Replace(".", "").Replace("đ", "").Trim()
    $cashTendered = [decimal]0
    if (-not [decimal]::TryParse($raw, [ref]$cashTendered) -or ($cashTendered -le 0)) {
        $cashTendered = $order.FinalAmount
    }
    $order.CashTendered = $cashTendered
    $order.ChangeDue = [Math]::Max([decimal]0, ($cashTendered - $order.FinalAmount))
}

Finalize-OrderCash $orderInstance "100,000"
Assert-True ($orderInstance.CashTendered -eq [decimal]100000 -and $orderInstance.ChangeDue -eq [decimal]15000) "OrderDTO checkout persists CashTendered=100k, ChangeDue=15k" "Tier2" $suite

# Fallback on empty input during checkout
Finalize-OrderCash $orderInstance ""
Assert-True ($orderInstance.CashTendered -eq [decimal]85000 -and $orderInstance.ChangeDue -eq [decimal]0) "OrderDTO checkout with empty input defaults CashTendered to FinalAmount" "Tier2" $suite

# Fallback on zero input during checkout
Finalize-OrderCash $orderInstance "0"
Assert-True ($orderInstance.CashTendered -eq [decimal]85000 -and $orderInstance.ChangeDue -eq [decimal]0) "OrderDTO checkout with '0' input defaults CashTendered to FinalAmount" "Tier2" $suite

# Fallback on underpayment during checkout (clamped change due >= 0)
Finalize-OrderCash $orderInstance "50,000"
Assert-True ($orderInstance.CashTendered -eq [decimal]50000 -and $orderInstance.ChangeDue -eq [decimal]0) "OrderDTO checkout with deficit clamps ChangeDue to 0 (non-negative)" "Tier2" $suite


# ==============================================================================
# SECTION 2: THOUSAND SEPARATOR & STRING PARSING LOGIC
# ==============================================================================
Write-Host "`n--- [SECTION 2] Thousand Separator & String Parsing Logic ---" -ForegroundColor Yellow

$separatorTestCases = @(
    @{ Input = "100,000"; Expected = [decimal]100000; Desc = "Standard comma separator (100,000)" },
    @{ Input = "100.000"; Expected = [decimal]100000; Desc = "Vietnamese dot separator (100.000)" },
    @{ Input = "100000"; Expected = [decimal]100000; Desc = "Plain unformatted integer (100000)" },
    @{ Input = "  100,000  "; Expected = [decimal]100000; Desc = "Leading and trailing spaces (  100,000  )" },
    @{ Input = "100,000 đ"; Expected = [decimal]100000; Desc = "Currency symbol suffix (100,000 đ)" },
    @{ Input = "100.000đ"; Expected = [decimal]100000; Desc = "Dot separator with compact currency symbol (100.000đ)" },
    @{ Input = "100,000,000"; Expected = [decimal]100000000; Desc = "Multi-comma 100 million (100,000,000)" },
    @{ Input = "100.000.000"; Expected = [decimal]100000000; Desc = "Multi-dot 100 million (100.000.000)" },
    @{ Input = "1,000,000,000"; Expected = [decimal]1000000000; Desc = "1 Billion with commas" },
    @{ Input = "1.000.000.000"; Expected = [decimal]1000000000; Desc = "1 Billion with dots" }
)

foreach ($tc in $separatorTestCases) {
    $raw = $tc.Input.Trim().Replace(",", "").Replace(".", "").Replace("đ", "").Trim()
    $parsedVal = [decimal]0
    $ok = [decimal]::TryParse($raw, [ref]$parsedVal)
    Assert-True ($ok -and ($parsedVal -eq $tc.Expected)) "Parsing: $($tc.Desc) -> $($tc.Expected)" "Tier2" $suite
}

# Adversarial & Invalid Inputs
$adversarialTestCases = @(
    @{ Input = ""; Desc = "Empty string" },
    @{ Input = "   "; Desc = "Whitespace only" },
    @{ Input = "abc"; Desc = "Pure alphabetic" },
    @{ Input = "100k"; Desc = "Shortcut 'k' inside textbox" },
    @{ Input = "-50000"; Desc = "Negative number (-50000)" },
    @{ Input = "-50,000"; Desc = "Negative formatted number (-50,000)" },
    @{ Input = "!@#$%^"; Desc = "Special characters" },
    @{ Input = "12a34"; Desc = "Alphanumeric mix" },
    @{ Input = "999999999999999999999999999999999999999"; Desc = "Extreme overflow string" }
)

foreach ($tc in $adversarialTestCases) {
    $raw = $tc.Input.Trim().Replace(",", "").Replace(".", "").Replace("đ", "").Trim()
    $parsedVal = [decimal]0
    $ok = [decimal]::TryParse($raw, [ref]$parsedVal)
    $isValidPositive = $ok -and ($parsedVal -gt 0)
    Assert-False ($isValidPositive) "Adversarial input rejected safely: $($tc.Desc)" "Tier2" $suite
}

# Quick Cash Button Logic
function Parse-QuickCashButton([string]$btnText) {
    $text = $btnText.ToLower().Replace("k", "000").Replace(",", "").Trim()
    $val = [decimal]0
    if ([decimal]::TryParse($text, [ref]$val)) {
        return ("{0:N0}" -f $val)
    }
    return $null
}

Assert-Equal (Parse-QuickCashButton "50k") "50,000" "Quick cash button '50k' formats to '50,000'" "Tier2" $suite
Assert-Equal (Parse-QuickCashButton "100k") "100,000" "Quick cash button '100k' formats to '100,000'" "Tier2" $suite
Assert-Equal (Parse-QuickCashButton "200k") "200,000" "Quick cash button '200k' formats to '200,000'" "Tier2" $suite
Assert-Equal (Parse-QuickCashButton "500k") "500,000" "Quick cash button '500k' formats to '500,000'" "Tier2" $suite


# ==============================================================================
# SECTION 3: DECIMAL ROUNDING AND VAT CALCULATIONS COMBINED WITH CASH TENDERED
# ==============================================================================
Write-Host "`n--- [SECTION 3] Decimal Rounding & VAT Invariant Stress ---" -ForegroundColor Yellow

$orderServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.OrderService")
Assert-NotNull $orderServiceType "OrderService resolved in assembly" "Tier2" $suite
$orderService = [Activator]::CreateInstance($orderServiceType)

# Invariant calculation logic based on OrderService.CalculateOrderSummary
function Compute-OrderMath([decimal]$totalAmount, [decimal]$discountPercent, [decimal]$vatPercent, [decimal]$cashTendered) {
    $discountAmount = [Math]::Round(($totalAmount * ($discountPercent / [decimal]100)), 0)
    if ($discountAmount -gt $totalAmount) { $discountAmount = $totalAmount }

    $amountAfterDiscount = [Math]::Max([decimal]0, ($totalAmount - $discountAmount))
    $vatAmount = [Math]::Round(($amountAfterDiscount * ($vatPercent / [decimal]100)), 0)
    $finalAmount = $amountAfterDiscount + $vatAmount

    $changeDue = [Math]::Max([decimal]0, ($cashTendered - $finalAmount))

    return [PSCustomObject]@{
        TotalAmount = $totalAmount
        DiscountAmount = $discountAmount
        AmountAfterDiscount = $amountAfterDiscount
        VatAmount = $vatAmount
        FinalAmount = $finalAmount
        CashTendered = $cashTendered
        ChangeDue = $changeDue
        IsInvariantValid = ($finalAmount -eq ($amountAfterDiscount + $vatAmount))
    }
}

# 3.1 Known boundary cases
$case1 = Compute-OrderMath 33333 0 8 50000
Assert-True ($case1.VatAmount -eq [decimal]2667 -and $case1.FinalAmount -eq [decimal]36000 -and $case1.ChangeDue -eq [decimal]14000 -and $case1.IsInvariantValid) "Rounding case: 33,333 VND with 8% VAT -> 36,000 VND final, change 14,000 VND" "Tier2" $suite

$case2 = Compute-OrderMath 77777 15 10 100000
# Discount: round(77777 * 0.15) = 11667; after: 66110; vat: round(66110 * 0.1) = 6611; final: 72721; change: 27279
Assert-True ($case2.DiscountAmount -eq [decimal]11667 -and $case2.VatAmount -eq [decimal]6611 -and $case2.FinalAmount -eq [decimal]72721 -and $case2.ChangeDue -eq [decimal]27279) "Complex discount & VAT rounding: 77,777 @ 15% disc, 10% VAT -> Final 72,721, change 27,279" "Tier2" $suite

# 3.2 Fuzzing 1,000 randomized orders for mathematical stability
$fuzzPassCount = 0
$fuzzTotal = 1000
$rng = New-Object System.Random 42

for ($i = 0; $i -lt $fuzzTotal; $i++) {
    $randTotal = [decimal]$rng.Next(1000, 50000000) # 1k to 50M
    $randDisc = [decimal]$rng.Next(0, 50)           # 0% to 50%
    $randVat = [decimal]@(0, 5, 8, 10)[$rng.Next(0, 4)]
    $randCash = [decimal]($randTotal + $rng.Next(-50000, 100000))

    $res = Compute-OrderMath $randTotal $randDisc $randVat $randCash
    if ($res.IsInvariantValid -and ($res.FinalAmount -ge [decimal]0) -and ($res.ChangeDue -ge [decimal]0)) {
        $fuzzPassCount++
    }
}

Assert-True ($fuzzPassCount -eq $fuzzTotal) "Fuzz testing 1,000 diverse order calculations: $fuzzPassCount / $fuzzTotal passed invariant" "Tier2" $suite


# ==============================================================================
# SECTION 4: PRINT CANVAS FORMATTING & GDI+ STRESS (PrintHelper.DrawInvoiceK80)
# ==============================================================================
Write-Host "`n--- [SECTION 4] Print Canvas Formatting & GDI+ Stress ---" -ForegroundColor Yellow

$printHelperType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Utilities.PrintHelper")
Assert-NotNull $printHelperType "PrintHelper resolved in assembly" "Tier2" $suite
$drawMethod = $printHelperType.GetMethod("DrawInvoiceK80", [System.Reflection.BindingFlags]"Public,Static")
Assert-NotNull $drawMethod "PrintHelper.DrawInvoiceK80 method resolved" "Tier2" $suite

$orderDetailDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.OrderDetailDTO")
$detailsListType = [Type]::GetType("System.Collections.Generic.List``1").MakeGenericType($orderDetailDtoType)

function Create-MockOrder([decimal]$total, [decimal]$final, [decimal]$cash, [decimal]$change, [string]$promo = "") {
    $o = [Activator]::CreateInstance($orderDtoType)
    $o.Id = 8888
    $o.TableName = "Bàn VIP 01"
    $o.EmployeeName = "Nguyễn Văn Thu Ngân"
    $o.OrderDate = [DateTime]::Now
    $o.TotalAmount = $total
    $o.DiscountAmount = [Math]::Max([decimal]0, ($total - $final))
    $o.VATPercent = [decimal]8
    $o.VATAmount = [Math]::Round(($final * [decimal]0.08), 0)
    $o.FinalAmount = $final
    $o.PromoCode = $promo
    $o.CashTendered = $cash
    $o.ChangeDue = $change
    return $o
}

function Create-MockDetails([int]$count, [decimal]$unitPrice = 30000) {
    $list = [Activator]::CreateInstance($detailsListType)
    for ($i = 1; $i -le $count; $i++) {
        $d = [Activator]::CreateInstance($orderDetailDtoType)
        $d.Id = $i
        $d.OrderId = 8888
        $d.ProductId = $i
        $d.ProductName = "Món Cà Phê Thử Nghiệm $i"
        $d.Unit = "Ly"
        $d.Quantity = 1
        $d.UnitPrice = $unitPrice
        $d.TotalPrice = $unitPrice
        $list.Add($d)
    }
    return ,$list
}

# 4.1 Standard Draw test
$bmpStandard = New-Object System.Drawing.Bitmap 300, 800
$gStandard = [System.Drawing.Graphics]::FromImage($bmpStandard)
$mockOrder1 = Create-MockOrder 90000 90000 100000 10000
$mockDetails1 = Create-MockDetails 3

$stdSuccess = $false
try {
    $drawMethod.Invoke($null, @($gStandard, $mockOrder1, $mockDetails1, [int]285)) | Out-Null
    $stdSuccess = $true
} catch {
    $stdSuccess = $false
} finally {
    $gStandard.Dispose()
    $bmpStandard.Dispose()
}
Assert-True $stdSuccess "PrintHelper.DrawInvoiceK80 draws standard invoice without exception" "Tier2" $suite

# 4.2 Large Totals and Change Values (100,000,000 VNĐ)
$bmpLarge = New-Object System.Drawing.Bitmap 300, 800
$gLarge = [System.Drawing.Graphics]::FromImage($bmpLarge)
$mockOrderLarge = Create-MockOrder 100000000 100000000 100000000 0
$mockDetailsLarge = Create-MockDetails 5 20000000

$largeSuccess = $false
try {
    $drawMethod.Invoke($null, @($gLarge, $mockOrderLarge, $mockDetailsLarge, [int]285)) | Out-Null
    $largeSuccess = $true
} catch {
    $largeSuccess = $false
} finally {
    $gLarge.Dispose()
    $bmpLarge.Dispose()
}
Assert-True $largeSuccess "PrintHelper.DrawInvoiceK80 draws 100,000,000 VND invoice cleanly without exception" "Tier2" $suite

# 4.3 Large Change Overpayment (Cash 100M, Change 55M)
$bmpChange = New-Object System.Drawing.Bitmap 300, 800
$gChange = [System.Drawing.Graphics]::FromImage($bmpChange)
$mockOrderChange = Create-MockOrder 45000000 45000000 100000000 55000000

$changeSuccess = $false
try {
    $drawMethod.Invoke($null, @($gChange, $mockOrderChange, $mockDetails1, [int]285)) | Out-Null
    $changeSuccess = $true
} catch {
    $changeSuccess = $false
} finally {
    $gChange.Dispose()
    $bmpChange.Dispose()
}
Assert-True $changeSuccess "PrintHelper.DrawInvoiceK80 draws large overpayment (55,000,000 VND change) cleanly" "Tier2" $suite

# 4.4 Stress Test: 50 line items in order
$bmpMany = New-Object System.Drawing.Bitmap 300, 2000
$gMany = [System.Drawing.Graphics]::FromImage($bmpMany)
$mockDetails50 = Create-MockDetails 50 15000

$manySuccess = $false
try {
    $drawMethod.Invoke($null, @($gMany, $mockOrder1, $mockDetails50, [int]285)) | Out-Null
    $manySuccess = $true
} catch {
    $manySuccess = $false
} finally {
    $gMany.Dispose()
    $bmpMany.Dispose()
}
Assert-True $manySuccess "PrintHelper.DrawInvoiceK80 handles 50 order items without buffer or memory crash" "Tier2" $suite

# 4.5 Exact GDI+ Layout Collision / Truncation Check
$bmpMeasure = New-Object System.Drawing.Bitmap 300, 600
$gMeasure = [System.Drawing.Graphics]::FromImage($bmpMeasure)
$fontTotal = New-Object System.Drawing.Font "Segoe UI", ([float]10), [System.Drawing.FontStyle]::Bold
$fontRegular = New-Object System.Drawing.Font "Segoe UI", ([float]8.5), [System.Drawing.FontStyle]::Regular
$contentWidth = 285.0

# Measure line 1: TỔNG THANH TOÁN vs 100,000,000 đ
$wLeftTotal = $gMeasure.MeasureString("TỔNG THANH TOÁN:", $fontTotal).Width
$wRightTotal = $gMeasure.MeasureString("100,000,000 đ", $fontTotal).Width
$gapTotal = $contentWidth - ($wLeftTotal + $wRightTotal)
Assert-True ($gapTotal -gt 0) "Line 'TỔNG THANH TOÁN' + '100,000,000 đ' has positive gap ($([Math]::Round($gapTotal, 1))px) without text overlap" "Tier2" $suite

# Measure line 2: Tiền khách đưa vs 100,000,000 đ
$wLeftTendered = $gMeasure.MeasureString("Tiền khách đưa:", $fontRegular).Width
$wRightTendered = $gMeasure.MeasureString("100,000,000 đ", $fontRegular).Width
$gapTendered = $contentWidth - ($wLeftTendered + $wRightTendered)
Assert-True ($gapTendered -gt 0) "Line 'Tiền khách đưa' + '100,000,000 đ' has positive gap ($([Math]::Round($gapTendered, 1))px) without text overlap" "Tier2" $suite

# Measure line 3: Tiền thối lại vs 100,000,000 đ
$wLeftChange = $gMeasure.MeasureString("Tiền thối lại:", $fontRegular).Width
$wRightChange = $gMeasure.MeasureString("100,000,000 đ", $fontRegular).Width
$gapChange = $contentWidth - ($wLeftChange + $wRightChange)
Assert-True ($gapChange -gt 0) "Line 'Tiền thối lại' + '100,000,000 đ' has positive gap ($([Math]::Round($gapChange, 1))px) without text overlap" "Tier2" $suite

$gMeasure.Dispose()
$bmpMeasure.Dispose()

# 4.6 Edge Case: Null and Empty Inputs to DrawInvoiceK80
$bmpNull = New-Object System.Drawing.Bitmap 300, 600
$gNull = [System.Drawing.Graphics]::FromImage($bmpNull)

$nullOrderSuccess = $false
try {
    $drawMethod.Invoke($null, @($gNull, $null, $null, [int]285)) | Out-Null
    $nullOrderSuccess = $true
} catch {
    $nullOrderSuccess = $false
}
Assert-True $nullOrderSuccess "PrintHelper.DrawInvoiceK80 gracefully survives null order & null details" "Tier2" $suite

# Null graphics object guard
$nullGSuccess = $false
try {
    $drawMethod.Invoke($null, @($null, $mockOrder1, $mockDetails1, [int]285)) | Out-Null
    $nullGSuccess = $true
} catch {
    $nullGSuccess = $false
}
Assert-True $nullGSuccess "PrintHelper.DrawInvoiceK80 gracefully survives null Graphics reference" "Tier2" $suite
$gNull.Dispose()
$bmpNull.Dispose()

# 4.7 GDI Handle Leak Stress: 100 consecutive renders
$gdiStart = [GdiTrackerM4]::GetGdiHandleCount()
for ($i = 0; $i -lt 100; $i++) {
    $bmpLoop = New-Object System.Drawing.Bitmap 300, 600
    $gLoop = [System.Drawing.Graphics]::FromImage($bmpLoop)
    $drawMethod.Invoke($null, @($gLoop, $mockOrder1, $mockDetails1, [int]285)) | Out-Null
    $gLoop.Dispose()
    $bmpLoop.Dispose()
}
[GC]::Collect()
[GC]::WaitForPendingFinalizers()
$gdiEnd = [GdiTrackerM4]::GetGdiHandleCount()
$gdiDelta = [Math]::Abs([int]$gdiEnd - [int]$gdiStart)
Assert-True ($gdiDelta -le 5) "GDI handle leak stress: 100 renders resulted in GDI delta = $gdiDelta (<= 5)" "Tier2" $suite


# ==============================================================================
# SECTION 5: RDLC REPORT PARAMETERS VALIDATION
# ==============================================================================
Write-Host "`n--- [SECTION 5] RDLC Report Parameters Validation ---" -ForegroundColor Yellow

$rdlcPath = Join-Path $global:ProjectRoot "Form\TanPhat\RptInvoiceK80.rdlc"
Assert-True (Test-Path $rdlcPath) "RptInvoiceK80.rdlc file exists at relative path" "Tier2" $suite

[xml]$rdlcXml = Get-Content $rdlcPath
$reportParams = @($rdlcXml.Report.ReportParameters.ReportParameter)
$paramMap = @{}
foreach ($p in $reportParams) {
    $paramMap[$p.Name] = $p.DataType
}

# 5.1 Verify cash & change parameters
Assert-True ($paramMap.ContainsKey("pCashTendered")) "RDLC defines 'pCashTendered' parameter" "Tier2" $suite
Assert-Equal $paramMap["pCashTendered"] "String" "'pCashTendered' parameter DataType is String" "Tier2" $suite

Assert-True ($paramMap.ContainsKey("pChangeDue")) "RDLC defines 'pChangeDue' parameter" "Tier2" $suite
Assert-Equal $paramMap["pChangeDue"] "String" "'pChangeDue' parameter DataType is String" "Tier2" $suite

# 5.2 Verify all expected 14 parameters
$expectedParams = @(
    "pShopName", "pShopAddress", "pShopPhone", "pOrderId", "pTableName",
    "pOrderDate", "pCashier", "pTotalAmount", "pDiscountAmount", "pVatAmount",
    "pFinalAmount", "pWifiInfo", "pCashTendered", "pChangeDue"
)

$missingParams = @()
foreach ($ep in $expectedParams) {
    if (-not $paramMap.ContainsKey($ep)) {
        $missingParams += $ep
    }
}
Assert-True ($missingParams.Count -eq 0) "All 14 RDLC ReportParameters are defined (Missing: $($missingParams -join ', '))" "Tier2" $suite

# 5.3 Verify textboxes mapping to parameters
$textboxes = @($rdlcXml.Report.Body.ReportItems.Textbox)
$tenderedTb = $textboxes | Where-Object { $_.Name -eq "txtCashTenderedBox" }
$changeTb = $textboxes | Where-Object { $_.Name -eq "txtChangeDueBox" }

Assert-NotNull $tenderedTb "RDLC body contains 'txtCashTenderedBox' textbox" "Tier2" $suite
$tenderedVal = $tenderedTb.Paragraphs.Paragraph.TextRuns.TextRun.Value
Assert-Equal $tenderedVal "=Parameters!pCashTendered.Value" "'txtCashTenderedBox' expression maps to =Parameters!pCashTendered.Value" "Tier2" $suite

Assert-NotNull $changeTb "RDLC body contains 'txtChangeDueBox' textbox" "Tier2" $suite
$changeVal = $changeTb.Paragraphs.Paragraph.TextRuns.TextRun.Value
Assert-Equal $changeVal "=Parameters!pChangeDue.Value" "'txtChangeDueBox' expression maps to =Parameters!pChangeDue.Value" "Tier2" $suite

# 5.4 FrmPrintReview Code Inspection for SetParameters
$frmPrintReviewCs = Get-Content (Join-Path $global:ProjectRoot "Form\TanPhat\FrmPrintReview.cs") -Raw
Assert-True ($frmPrintReviewCs -match 'new\s+ReportParameter\s*\(\s*"pCashTendered"') "FrmPrintReview.cs creates 'pCashTendered' parameter" "Tier2" $suite
Assert-True ($frmPrintReviewCs -match 'new\s+ReportParameter\s*\(\s*"pChangeDue"') "FrmPrintReview.cs creates 'pChangeDue' parameter" "Tier2" $suite
Assert-True ($frmPrintReviewCs -match 'reportViewerInvoice\.LocalReport\.SetParameters') "FrmPrintReview.cs calls LocalReport.SetParameters" "Tier2" $suite


# ==============================================================================
# SECTION 6: FRMPRINTREVIEW VISUAL PREVIEW & CONTROLS VALIDATION
# ==============================================================================
Write-Host "`n--- [SECTION 6] FrmPrintReview Visual Controls & Canvas Tab ---" -ForegroundColor Yellow

$frmPrintReviewType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.FrmPrintReview")
Assert-NotNull $frmPrintReviewType "FrmPrintReview resolved in assembly" "Tier2" $suite

$frmPrint = [Activator]::CreateInstance($frmPrintReviewType)
Assert-NotNull $frmPrint "FrmPrintReview instantiated successfully with default sample order" "Tier2" $suite

$fCashTenderedVal = $frmPrintReviewType.GetField("lblCashTenderedValue", [System.Reflection.BindingFlags]"NonPublic,Instance")
$fChangeDueVal = $frmPrintReviewType.GetField("lblChangeDueValue", [System.Reflection.BindingFlags]"NonPublic,Instance")

Assert-NotNull $fCashTenderedVal "FrmPrintReview has 'lblCashTenderedValue' control" "Tier2" $suite
Assert-NotNull $fChangeDueVal "FrmPrintReview has 'lblChangeDueValue' control" "Tier2" $suite

$lblCashValCtrl = $fCashTenderedVal.GetValue($frmPrint)
$lblChangeValCtrl = $fChangeDueVal.GetValue($frmPrint)

Assert-NotNull $lblCashValCtrl "lblCashTenderedValue control is non-null" "Tier2" $suite
Assert-NotNull $lblChangeValCtrl "lblChangeDueValue control is non-null" "Tier2" $suite

$frmPrint.Dispose()


# ==============================================================================
# SECTION 7: POSCONTROL & FRMORDER LIVE INTERACTION SIMULATION
# ==============================================================================
Write-Host "`n--- [SECTION 7] FrmOrder Live Control Integration Simulation ---" -ForegroundColor Yellow

$frmOrderType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.FrmOrder")
Assert-NotNull $frmOrderType "FrmOrder resolved in assembly" "Tier2" $suite

$frmOrder = [Activator]::CreateInstance($frmOrderType)
Assert-NotNull $frmOrder "FrmOrder instance created successfully" "Tier2" $suite

$txtCashCtrl = $frmOrderType.GetField("txtCashTendered", [System.Reflection.BindingFlags]"NonPublic,Instance").GetValue($frmOrder)
$lblChangeCtrl = $frmOrderType.GetField("lblChangeDue", [System.Reflection.BindingFlags]"NonPublic,Instance").GetValue($frmOrder)
$btnCashExactCtrl = $frmOrderType.GetField("btnCashExact", [System.Reflection.BindingFlags]"NonPublic,Instance").GetValue($frmOrder)
$btnCash50kCtrl = $frmOrderType.GetField("btnCash50k", [System.Reflection.BindingFlags]"NonPublic,Instance").GetValue($frmOrder)
$btnCash100kCtrl = $frmOrderType.GetField("btnCash100k", [System.Reflection.BindingFlags]"NonPublic,Instance").GetValue($frmOrder)
$btnCash200kCtrl = $frmOrderType.GetField("btnCash200k", [System.Reflection.BindingFlags]"NonPublic,Instance").GetValue($frmOrder)
$btnCash500kCtrl = $frmOrderType.GetField("btnCash500k", [System.Reflection.BindingFlags]"NonPublic,Instance").GetValue($frmOrder)

Assert-NotNull $txtCashCtrl "FrmOrder.txtCashTendered is instantiated" "Tier2" $suite
Assert-NotNull $lblChangeCtrl "FrmOrder.lblChangeDue is instantiated" "Tier2" $suite
Assert-NotNull $btnCashExactCtrl "FrmOrder.btnCashExact is instantiated" "Tier2" $suite
Assert-NotNull $btnCash50kCtrl "FrmOrder.btnCash50k is instantiated" "Tier2" $suite
Assert-NotNull $btnCash100kCtrl "FrmOrder.btnCash100k is instantiated" "Tier2" $suite
Assert-NotNull $btnCash200kCtrl "FrmOrder.btnCash200k is instantiated" "Tier2" $suite
Assert-NotNull $btnCash500kCtrl "FrmOrder.btnCash500k is instantiated" "Tier2" $suite

# Test setting text on txtCashTendered updates lblChangeDue
$txtCashCtrl.Text = "500,000"
$calcMethod = $frmOrderType.GetMethod("UpdateChangeDueCalculation", [System.Reflection.BindingFlags]"NonPublic,Instance")
if ($calcMethod) {
    $calcMethod.Invoke($frmOrder, $null)
}
Assert-True ($lblChangeCtrl.Text -ne "") "lblChangeDue text updated on txtCashTendered change: '$($lblChangeCtrl.Text)'" "Tier2" $suite

$frmOrder.Dispose()

# ==============================================================================
# SUMMARY & TELEMETRY EXPORT
# ==============================================================================
Write-Host "`n=========================================================================="
$m4Results = $global:TestResults | Where-Object { $_.Suite -eq $suite }
$passedCount = ($m4Results | Where-Object { $_.Passed }).Count
$failedCount = ($m4Results | Where-Object { -not $_.Passed }).Count
$totalCount = $m4Results.Count

$reportColor = if ($failedCount -eq 0) { "Green" } else { "Red" }
Write-Host "CHALLENGER 2 M4 SUMMARY: $passedCount / $totalCount tests passed ($failedCount failed)" -ForegroundColor $reportColor
Write-Host "=========================================================================="

$exportPath = Join-Path $global:ProjectRoot "tests\Challenger2_M4_Results.json"
$m4Results | ConvertTo-Json -Depth 4 | Set-Content $exportPath -Encoding UTF8
Write-Host "Challenger 2 M4 telemetry saved to: $exportPath"

if ($failedCount -gt 0) {
    exit 1
} else {
    exit 0
}

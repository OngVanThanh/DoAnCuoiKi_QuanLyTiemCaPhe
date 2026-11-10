# ==============================================================================
# Verification Test: Area Reservation Restructuring & Strict Area Collision Avoidance
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\Common\TestHarness.ps1")
. $HarnessPath

$asm = Get-ProjectAssembly

$tableServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.TableService")
$tableService = [Activator]::CreateInstance($tableServiceType)

$testsPassed = 0
$testsFailed = 0

function Assert-Test($condition, $description) {
    if ($condition) {
        Write-Host "  [PASS] $description" -ForegroundColor Green
        $script:testsPassed++
    } else {
        Write-Host "  [FAIL] $description" -ForegroundColor Red
        $script:testsFailed++
    }
}

Write-Host "--- TEST SUITE: Area Reservation & Overlap Collision Check ---" -ForegroundColor Cyan

# 1. UI Control Verification on FrmOrder
$frmOrderType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.FrmOrder")
$frmOrder = [Activator]::CreateInstance($frmOrderType)

$cboReserveAreaField = $frmOrderType.GetField("cboReserveArea", [System.Reflection.BindingFlags]"NonPublic,Instance")
$txtCustomerNameField = $frmOrderType.GetField("txtReserveCustomerName", [System.Reflection.BindingFlags]"NonPublic,Instance")
$lblReserveNameField = $frmOrderType.GetField("lblReserveName", [System.Reflection.BindingFlags]"NonPublic,Instance")

Assert-Test ($null -ne $cboReserveAreaField) "FrmOrder contains ComboBox 'cboReserveArea'"
Assert-Test ($null -ne $txtCustomerNameField) "FrmOrder contains TextBox 'txtReserveCustomerName'"
Assert-Test ($null -ne $lblReserveNameField) "FrmOrder contains Label 'lblReserveName'"

$lblReserveName = $lblReserveNameField.GetValue($frmOrder)
Assert-Test ($lblReserveName.Text -eq "Tên khách hàng:") "lblReserveName.Text is 'Tên khách hàng:'"

$cboReserveArea = $cboReserveAreaField.GetValue($frmOrder)
Assert-Test ($cboReserveArea.DropDownStyle -eq [System.Windows.Forms.ComboBoxStyle]::DropDown) "cboReserveArea allows both typing and selecting (DropDownStyle = DropDown)"

$frmOrder.Dispose()

# 2. Card Presentation Verification on UcReservedTableCard
$cardType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.UcReservedTableCard")
$card = [Activator]::CreateInstance($cardType)

$tableDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.TableDTO")
$testDto = [Activator]::CreateInstance($tableDtoType)
$testDto.Id = 99
$testDto.TableName = "Nguyễn Văn Test"
$testDto.AreaName = "Phòng VIP 1"
$testDto.Status = "Đặt trước"
$testDto.StartTime = [DateTime]::Now.AddHours(1)
$testDto.EndTime = [DateTime]::Now.AddHours(3)

$card.BindData($testDto, $false)

$lblTableNameField = $cardType.GetField("lblTableName", [System.Reflection.BindingFlags]"NonPublic,Instance")
$lblAreaNameField = $cardType.GetField("lblAreaName", [System.Reflection.BindingFlags]"NonPublic,Instance")

$lblTableName = $lblTableNameField.GetValue($card)
$lblAreaName = $lblAreaNameField.GetValue($card)

Assert-Test ($lblTableName.Text -eq "KH: Nguyễn Văn Test") "UcReservedTableCard displays customer name with 'KH: ' prefix"
Assert-Test ($lblAreaName.Text -eq "Khu vực: Phòng VIP 1") "UcReservedTableCard displays area name on second line"

$card.Dispose()

# 3. Collision Logic Tests in TableService
$testArea = "Khu Vực Kiểm Thử Trùng Lặp " + (Get-Random -Minimum 1000 -Maximum 9999)
$startTime1 = [DateTime]::Now.AddHours(5)
$endTime1 = [DateTime]::Now.AddHours(7)

# Test 3.1: First booking in the area should succeed
$msg1 = ""
$res1 = $tableService.AddReservedTable("Khách An", $testArea, $startTime1, $endTime1, [ref]$msg1)
Assert-Test ($res1 -eq $true) "First reservation in area '$testArea' succeeds"

# Test 3.2: Second booking in SAME area with OVERLAPPING time should be rejected
$startTime2 = [DateTime]::Now.AddHours(6) # 6h is between 5h and 7h -> Overlap!
$endTime2 = [DateTime]::Now.AddHours(8)
$msg2 = ""
$res2 = $tableService.AddReservedTable("Khách Bình", $testArea, $startTime2, $endTime2, [ref]$msg2)
Assert-Test ($res2 -eq $false) "Second reservation in SAME area during overlapping hours is REJECTED"
Assert-Test ($msg2 -like "*đã có lịch đặt trước của khách*") "Collision error message explicitly mentions area and existing booking"

# Test 3.3: Booking in a DIFFERENT area at the SAME overlapping time should SUCCEED
$diffArea = "Khu Vực Khác " + (Get-Random -Minimum 1000 -Maximum 9999)
$msg3 = ""
$res3 = $tableService.AddReservedTable("Khách Cúc", $diffArea, $startTime2, $endTime2, [ref]$msg3)
Assert-Test ($res3 -eq $true) "Reservation in DIFFERENT area during same timeframe SUCCEEDS"

# Test 3.4: Booking in the SAME area at NON-OVERLAPPING time (after first ends) should SUCCEED
$startTime4 = $endTime1 # 7h, right when first booking ends
$endTime4 = [DateTime]::Now.AddHours(9)
$msg4 = ""
$res4 = $tableService.AddReservedTable("Khách Dũng", $testArea, $startTime4, $endTime4, [ref]$msg4)
Assert-Test ($res4 -eq $true) "Reservation in SAME area at non-overlapping consecutive timeframe SUCCEEDS"

Write-Host "`n==================================================================" -ForegroundColor Cyan
Write-Host "TEST SUMMARY: $testsPassed passed, $testsFailed failed" -ForegroundColor $(if ($testsFailed -eq 0) { "Green" } else { "Red" })
Write-Host "==================================================================" -ForegroundColor Cyan

if ($testsFailed -gt 0) { exit 1 } else { exit 0 }

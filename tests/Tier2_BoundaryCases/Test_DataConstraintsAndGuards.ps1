# ==============================================================================
# Tier 2 - Test_DataConstraintsAndGuards.ps1
# Boundary: Foreign Key constraints, deletion crash prevention, loyalty rank calculations
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 2: Data Constraints & Guard Boundary Tests ---" -ForegroundColor Cyan

$asm = Get-ProjectAssembly
$empServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.EmployeeService")
$custServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.CustomerService")

$empService = [Activator]::CreateInstance($empServiceType)
$custService = [Activator]::CreateInstance($custServiceType)

# Test 2.1: Employee Deletion with Invalid ID (id <= 0)
$msg = ""
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$delInvalid = $empService.DeleteEmployee(0, [ref]$msg)
$sw.Stop()

Assert-False $delInvalid "DeleteEmployee rejects ID = 0" "Tier 2" "Data_Guards" -DurationMs $sw.TotalMilliseconds
Assert-Match $msg "không hợp lệ" "Error message states invalid employee ID" "Tier 2" "Data_Guards"

# Test 2.2: Employee Deletion with Negative ID
$msg = ""
$delNeg = $empService.DeleteEmployee(-15, [ref]$msg)
Assert-False $delNeg "DeleteEmployee rejects negative ID (-15)" "Tier 2" "Data_Guards"

# Test 2.3: Employee FK Constraint Guard Analysis (M1 Feature 4)
# Criteria: DeleteEmployee must catch SqlException gracefully and not throw unhandled exception
$threw = $false
$caughtMsg = ""
try {
    $empService.DeleteEmployee(999999, [ref]$caughtMsg)
} catch {
    $threw = $true
}
Assert-False $threw "DeleteEmployee handles missing/FK constrained entities without unhandled exception" "Tier 2" "Data_Guards"

# Test 2.4: Customer Loyalty Points & Rank Calculation Boundaries
# 1000+ -> Kim Cương, 500+ -> Vàng, 200+ -> Bạc, <200 -> Thường
$rank0 = $custService.CalculateRank(0)
Assert-Equal $rank0 "Thường" "Rank for 0 points is 'Thường'" "Tier 2" "Data_Guards"

$rank199 = $custService.CalculateRank(199)
Assert-Equal $rank199 "Thường" "Rank for 199 points is 'Thường' (boundary just below Bạc)" "Tier 2" "Data_Guards"

$rank200 = $custService.CalculateRank(200)
Assert-Equal $rank200 "Bạc" "Rank for 200 points is 'Bạc' (exact boundary)" "Tier 2" "Data_Guards"

$rank499 = $custService.CalculateRank(499)
Assert-Equal $rank499 "Bạc" "Rank for 499 points is 'Bạc' (boundary just below Vàng)" "Tier 2" "Data_Guards"

$rank500 = $custService.CalculateRank(500)
Assert-Equal $rank500 "Vàng" "Rank for 500 points is 'Vàng' (exact boundary)" "Tier 2" "Data_Guards"

$rank999 = $custService.CalculateRank(999)
Assert-Equal $rank999 "Vàng" "Rank for 999 points is 'Vàng' (boundary just below Kim Cương)" "Tier 2" "Data_Guards"

$rank1000 = $custService.CalculateRank(1000)
Assert-Equal $rank1000 "Kim Cương" "Rank for 1000 points is 'Kim Cương' (exact boundary)" "Tier 2" "Data_Guards"

$rankExtreme = $custService.CalculateRank(999999)
Assert-Equal $rankExtreme "Kim Cương" "Rank for extreme points (999,999) is 'Kim Cương'" "Tier 2" "Data_Guards"

# Test 2.5: Customer Creation with empty phone/name boundary
$err = ""
$addNullCust = $custService.Add($null, [ref]$err)
Assert-False $addNullCust "CustomerService.Add rejects null customer entity" "Tier 2" "Data_Guards"
Assert-Match $err "không hợp lệ" "Error message indicates invalid customer data" "Tier 2" "Data_Guards"

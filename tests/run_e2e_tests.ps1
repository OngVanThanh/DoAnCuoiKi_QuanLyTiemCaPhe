# ==============================================================================
# Master E2E Test Runner for Quản lý Tiệm Cà Phê (.NET Framework 4.8 / WinForms)
# 4-Tier Automated Test Suite
# Usage:
#   powershell.exe -ExecutionPolicy Bypass -File tests\run_e2e_tests.ps1
#   powershell.exe -ExecutionPolicy Bypass -File tests\run_e2e_tests.ps1 -Tier 1,2
# ==============================================================================

[CmdletBinding()]
param(
    [int[]]$Tier = @(1, 2, 3, 4),
    [string]$SuiteFilter = "",
    [string]$JsonReportPath = "$PSScriptRoot\TestResults.json"
)

$ErrorActionPreference = "Continue"

Write-Host "==========================================================================" -ForegroundColor Cyan
Write-Host "          E2E AUTOMATED TEST RUNNER: QUẢN LÝ TIỆM CÀ PHÊ                  " -ForegroundColor Yellow
Write-Host "==========================================================================" -ForegroundColor Cyan
Write-Host "Project Root : $PSScriptRoot\.." -ForegroundColor Gray
Write-Host "Target Tiers : $($Tier -join ', ')" -ForegroundColor Gray
Write-Host "Timestamp    : $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss') UTC" -ForegroundColor Gray
Write-Host "==========================================================================" -ForegroundColor Cyan

# Load shared harness
$harnessPath = Join-Path $PSScriptRoot "Common\TestHarness.ps1"
. $harnessPath

# Reset test results list for a clean run across all suites
$global:TestResults = [System.Collections.Generic.List[PSObject]]::new()

if ([string]::IsNullOrWhiteSpace($JsonReportPath) -or $JsonReportPath -eq "\TestResults.json") {
    $JsonReportPath = Join-Path $global:ProjectRoot "tests\TestResults.json"
}

$suiteFiles = @()

# Gather Tier 1
if ($Tier -contains 1) {
    $tier1Dir = Join-Path $PSScriptRoot "Tier1_FeatureCoverage"
    $suiteFiles += Get-ChildItem -Path $tier1Dir -Filter "*.ps1" | Sort-Object Name
}

# Gather Tier 2
if ($Tier -contains 2) {
    $tier2Dir = Join-Path $PSScriptRoot "Tier2_BoundaryCases"
    $suiteFiles += Get-ChildItem -Path $tier2Dir -Filter "*.ps1" | Sort-Object Name
}

# Gather Tier 3
if ($Tier -contains 3) {
    $tier3Dir = Join-Path $PSScriptRoot "Tier3_CrossFeature"
    $suiteFiles += Get-ChildItem -Path $tier3Dir -Filter "*.ps1" | Sort-Object Name
}

# Gather Tier 4
if ($Tier -contains 4) {
    $tier4Dir = Join-Path $PSScriptRoot "Tier4_RealWorldScenarios"
    $suiteFiles += Get-ChildItem -Path $tier4Dir -Filter "*.ps1" | Sort-Object Name
}

if ($SuiteFilter) {
    $suiteFiles = $suiteFiles | Where-Object { $_.BaseName -like "*$SuiteFilter*" }
}

$overallSw = [System.Diagnostics.Stopwatch]::StartNew()

# Execute each suite file
foreach ($file in $suiteFiles) {
    try {
        & $file.FullName -HarnessPath $harnessPath
    } catch {
        Write-Host "   [CRITICAL SUITE ERROR] in $($file.Name): $($_.Exception.Message)" -ForegroundColor Red
        Record-TestResult -Tier "Runner" -Suite $file.BaseName -TestName "Suite Execution" `
            -Passed $false -Expected "Clean Execution" -Actual "Exception: $($_.Exception.Message)" `
            -ErrorMessage $_.Exception.ToString()
    }
}

$overallSw.Stop()

# Calculate Summary
$allResults = $global:TestResults
$totalCount = $allResults.Count
$passedCount = @($allResults | Where-Object { $_.Passed }).Count
$knownDefectCount = @($allResults | Where-Object { $_.Status -eq "KNOWN_DEFECT" }).Count
$failedCount = @($allResults | Where-Object { -not $_.Passed -and $_.Status -ne "KNOWN_DEFECT" }).Count
$passRate = if ($totalCount -gt 0) { [Math]::Round(($passedCount / $totalCount) * 100, 1) } else { 0 }

Write-Host "`n==========================================================================" -ForegroundColor Cyan
Write-Host "                        TEST EXECUTION SUMMARY                            " -ForegroundColor Yellow
Write-Host "==========================================================================" -ForegroundColor Cyan
Write-Host "Total Tests Run  : $totalCount" -ForegroundColor White
Write-Host "Passed           : $passedCount" -ForegroundColor Green
Write-Host "Known Defects    : $knownDefectCount (Pending Milestone implementations)" -ForegroundColor Yellow
Write-Host "Failed           : $failedCount" -ForegroundColor $(if ($failedCount -eq 0) { "Green" } else { "Red" })
Write-Host "Pass Rate        : $passRate %" -ForegroundColor $(if ($passRate -ge 90) { "Green" } else { "Yellow" })
Write-Host "Total Time       : $([Math]::Round($overallSw.TotalSeconds, 2)) seconds" -ForegroundColor Gray
Write-Host "--------------------------------------------------------------------------" -ForegroundColor DarkGray

# Per-Tier Breakdown
Write-Host "Breakdown by Tier:" -ForegroundColor Cyan
$groupedTiers = $allResults | Group-Object Tier
foreach ($grp in $groupedTiers) {
    $tPass = @($grp.Group | Where-Object { $_.Passed }).Count
    $tDefect = @($grp.Group | Where-Object { $_.Status -eq "KNOWN_DEFECT" }).Count
    $tFail = @($grp.Group | Where-Object { -not $_.Passed -and $_.Status -ne "KNOWN_DEFECT" }).Count
    $tTotal = $grp.Group.Count
    $rate = if ($tTotal -gt 0) { [Math]::Round(($tPass / $tTotal) * 100, 1) } else { 0 }
    Write-Host "  - $($grp.Name.PadRight(10)) : $tTotal tests | $tPass passed | $tDefect known pending | $tFail failed ($rate%)" -ForegroundColor White
}

# Export JSON report
$jsonSaved = Export-TestSummaryJson -OutputPath $JsonReportPath
Write-Host "`nDetailed JSON report exported to: $jsonSaved" -ForegroundColor Cyan
Write-Host "==========================================================================" -ForegroundColor Cyan

# Exit code logic: If unexpected failures exist, exit code 1; else exit code 0
if ($failedCount -gt 0) {
    exit 1
} else {
    exit 0
}

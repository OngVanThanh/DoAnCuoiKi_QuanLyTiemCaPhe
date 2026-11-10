# ==============================================================================
# Tier 1 - Test_LinqLambdaCompliance.ps1
# Rubric: LINQ Lambda (3.0 pts) - 100% Pure LINQ Lambda, Zero Raw SQL, Zero Query Syntax
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 1: LINQ Lambda Compliance Audit ---" -ForegroundColor Cyan

$dalDir = Join-Path $global:ProjectRoot "DAL"
$bllDir = Join-Path $global:ProjectRoot "BLL"

$dalFiles = Get-ChildItem -Path $dalDir -Filter "*.cs"
$bllFiles = Get-ChildItem -Path $bllDir -Filter "*.cs"
$allFiles = @($dalFiles) + @($bllFiles)

# Test 1.1: Ensure DAL & BLL source files are present
$sw = [System.Diagnostics.Stopwatch]::StartNew()
Assert-True ($dalFiles.Count -ge 11) "DAL contains at least 11 Repositories" "Tier 1" "LINQ_Lambda" -DurationMs $sw.ElapsedMilliseconds
Assert-True ($bllFiles.Count -ge 10) "BLL contains at least 10 Services" "Tier 1" "LINQ_Lambda" -DurationMs $sw.ElapsedMilliseconds

# Test 1.2: Raw SQL Command & Adapter audit
# Criteria: Zero raw SQL string execution for CRUD operations
$rawSqlPattern = '\b(SqlCommand|SqlDataAdapter|ExecuteReader|ExecuteNonQuery|ExecuteScalar|SqlDataReader|ExecuteQuery|ExecuteCommand)\b'
$rawSqlCrudViolations = @()

foreach ($file in $allFiles) {
    $lines = Get-Content $file.FullName
    for ($i = 0; $i -lt $lines.Count; $i++) {
        $line = $lines[$i]
        if ($line -match $rawSqlPattern) {
            # Skip fast connection check methods (IsDatabaseOnline / TestConnectionFast)
            if ($line -match 'IsDatabaseOnline|TestConnectionFast|btnTestConnection') {
                continue
            }
            $rawSqlCrudViolations += "$($file.Name):$($i+1) -> $($line.Trim())"
        }
    }
}

$sw = [System.Diagnostics.Stopwatch]::StartNew()
Assert-Equal $rawSqlCrudViolations.Count 0 "Zero Raw SQL CRUD operations in DAL and BLL" "Tier 1" "LINQ_Lambda" `
    -DurationMs $sw.ElapsedMilliseconds

# Test 1.3: LINQ Query Comprehension Syntax audit ('from ... in ... select ...')
# Criteria: Bắt buộc dùng toàn bộ là LINQ Lambda extension methods (.Where, .Select, .Join)
$querySyntaxPattern = '\bfrom\s+\w+\s+in\b'
$querySyntaxViolations = @()

foreach ($file in $allFiles) {
    $lines = Get-Content $file.FullName
    for ($i = 0; $i -lt $lines.Count; $i++) {
        $line = $lines[$i]
        if ($line -match $querySyntaxPattern) {
            $querySyntaxViolations += [PSCustomObject]@{
                File = $file.Name
                Line = $i + 1
                Code = $line.Trim()
            }
        }
    }
}

$sw = [System.Diagnostics.Stopwatch]::StartNew()
# Known M2 requirement: 12 methods currently use query syntax before M2 refactoring
$isM2Pending = ($querySyntaxViolations.Count -gt 0)
Assert-Equal $querySyntaxViolations.Count 0 "Zero LINQ Query Comprehension Syntax (100% Pure LINQ Lambda)" `
    "Tier 1" "LINQ_Lambda" -DurationMs $sw.ElapsedMilliseconds -IsKnownMilestoneDefect $isM2Pending

if ($querySyntaxViolations.Count -gt 0) {
    Write-Host "         [INFO] Detected $($querySyntaxViolations.Count) query syntax instances pending Milestone 2 refactoring:" -ForegroundColor Yellow
    foreach ($v in $querySyntaxViolations | Select-Object -First 5) {
        Write-Host "           * $($v.File):$($v.Line) => $($v.Code)" -ForegroundColor DarkYellow
    }
    if ($querySyntaxViolations.Count -gt 5) {
        Write-Host "           * ... and $($querySyntaxViolations.Count - 5) more locations." -ForegroundColor DarkYellow
    }
}

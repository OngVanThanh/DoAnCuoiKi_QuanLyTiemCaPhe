# ==============================================================================
# E2E Test Harness: Shared Assertions, Telemetry & Reporting for Quản lý Tiệm Cafe
# ==============================================================================

Set-StrictMode -Off

if ($null -eq $global:TestResults) {
    $global:TestResults = [System.Collections.Generic.List[PSObject]]::new()
}

# Resolve project root by walking up from current script location
$scriptFile = if ($PSScriptRoot) { $PSScriptRoot } else { Split-Path $MyInvocation.MyCommand.Path }
$searchDir = $scriptFile
while ($searchDir -and -not (Test-Path (Join-Path $searchDir "DoAnCuoiKi_QuanLyTiemCaPhe.csproj"))) {
    $parent = Split-Path $searchDir -Parent
    if ($parent -eq $searchDir) { break }
    $searchDir = $parent
}
$global:ProjectRoot = if ($searchDir) { $searchDir } else { "d:\DoAnCuoiKi_QuanLyTiemCaPhe" }
$global:BinPath = Join-Path $global:ProjectRoot "bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe"
$global:ProjectAssembly = $null

function Get-ProjectAssembly {
    if ($global:ProjectAssembly -ne $null) {
        return $global:ProjectAssembly
    }

    if (-not (Test-Path $global:BinPath)) {
        throw "Assembly not found at: $global:BinPath. Please build the project first."
    }

    [System.Reflection.Assembly]::LoadWithPartialName("System.Drawing") | Out-Null
    [System.Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms") | Out-Null
    [System.Reflection.Assembly]::LoadWithPartialName("System.Configuration") | Out-Null
    [System.Reflection.Assembly]::LoadWithPartialName("System.Data.Linq") | Out-Null

    $global:ProjectAssembly = [System.Reflection.Assembly]::LoadFrom($global:BinPath)
    return $global:ProjectAssembly
}

function Record-TestResult {
    param(
        [string]$Tier,
        [string]$Suite,
        [string]$TestName,
        [bool]$Passed,
        [object]$Expected,
        [object]$Actual,
        [string]$ErrorMessage = "",
        [double]$DurationMs = 0,
        [bool]$IsKnownMilestoneDefect = $false
    )

    $result = [PSCustomObject]@{
        Timestamp    = [DateTime]::UtcNow.ToString("o")
        Tier         = $Tier
        Suite        = $Suite
        TestName     = $TestName
        Passed       = $Passed
        Status       = if ($Passed) { "PASSED" } elseif ($IsKnownMilestoneDefect) { "KNOWN_DEFECT" } else { "FAILED" }
        Expected     = "$Expected"
        Actual       = "$Actual"
        ErrorMessage = $ErrorMessage
        DurationMs   = [Math]::Round($DurationMs, 2)
    }

    $global:TestResults.Add($result)

    $badge = if ($Passed) {
        "[PASS]"
    } elseif ($IsKnownMilestoneDefect) {
        "[DEFECT-PENDING]"
    } else {
        "[FAIL]"
    }

    $color = if ($Passed) { "Green" } elseif ($IsKnownMilestoneDefect) { "Yellow" } else { "Red" }
    Write-Host "  $badge " -ForegroundColor $color -NoNewline
    Write-Host "$TestName " -NoNewline
    if ($DurationMs -gt 0) {
        Write-Host "(${result.DurationMs} ms)" -ForegroundColor DarkGray
    } else {
        Write-Host ""
    }

    if (-not $Passed) {
        Write-Host "         Expected: $Expected" -ForegroundColor DarkCyan
        Write-Host "         Actual:   $Actual" -ForegroundColor Magenta
        if ($ErrorMessage) {
            Write-Host "         Error:    $ErrorMessage" -ForegroundColor DarkRed
        }
    }
}

function Assert-Equal {
    param(
        [object]$Actual,
        [object]$Expected,
        [string]$TestName,
        [string]$Tier = "T1",
        [string]$Suite = "General",
        [double]$DurationMs = 0,
        [bool]$IsKnownMilestoneDefect = $false
    )

    $passed = $false
    try {
        if ($Actual -is [decimal] -or $Expected -is [decimal]) {
            $passed = ([decimal]$Actual -eq [decimal]$Expected)
        } elseif ($Actual -is [string] -and $Expected -is [string]) {
            $passed = ($Actual.Trim() -eq $Expected.Trim())
        } else {
            $passed = ($Actual -eq $Expected)
        }
    } catch {
        $passed = ($Actual -eq $Expected)
    }
    
    $err = if ($passed) { "" } else { "Values do not match." }
    Record-TestResult -Tier $Tier -Suite $Suite -TestName $TestName -Passed $passed `
        -Expected $Expected -Actual $Actual -ErrorMessage $err -DurationMs $DurationMs `
        -IsKnownMilestoneDefect $IsKnownMilestoneDefect
    return $passed
}

function Assert-True {
    param(
        [bool]$Condition,
        [string]$TestName,
        [string]$Tier = "T1",
        [string]$Suite = "General",
        [string]$FailureMessage = "Condition is not true",
        [double]$DurationMs = 0,
        [bool]$IsKnownMilestoneDefect = $false
    )

    $err = if ($Condition) { "" } else { $FailureMessage }
    Record-TestResult -Tier $Tier -Suite $Suite -TestName $TestName -Passed $Condition `
        -Expected "True" -Actual $Condition -ErrorMessage $err -DurationMs $DurationMs `
        -IsKnownMilestoneDefect $IsKnownMilestoneDefect
    return $Condition
}

function Assert-False {
    param(
        [bool]$Condition,
        [string]$TestName,
        [string]$Tier = "T1",
        [string]$Suite = "General",
        [string]$FailureMessage = "Condition is not false",
        [double]$DurationMs = 0,
        [bool]$IsKnownMilestoneDefect = $false
    )

    $passed = (-not $Condition)
    $err = if ($passed) { "" } else { $FailureMessage }
    Record-TestResult -Tier $Tier -Suite $Suite -TestName $TestName -Passed $passed `
        -Expected "False" -Actual $Condition -ErrorMessage $err -DurationMs $DurationMs `
        -IsKnownMilestoneDefect $IsKnownMilestoneDefect
    return $passed
}

function Assert-NotNull {
    param(
        [object]$Value,
        [string]$TestName,
        [string]$Tier = "T1",
        [string]$Suite = "General",
        [double]$DurationMs = 0,
        [bool]$IsKnownMilestoneDefect = $false
    )

    $passed = ($null -ne $Value)
    $err = if ($passed) { "" } else { "Object was null." }
    Record-TestResult -Tier $Tier -Suite $Suite -TestName $TestName -Passed $passed `
        -Expected "Non-Null Object" -Actual $(if ($passed) { "Object Present" } else { "null" }) `
        -ErrorMessage $err -DurationMs $DurationMs -IsKnownMilestoneDefect $IsKnownMilestoneDefect
    return $passed
}

function Assert-Throws {
    param(
        [scriptblock]$ScriptBlock,
        [string]$ExpectedExceptionType = "System.Exception",
        [string]$TestName,
        [string]$Tier = "T2",
        [string]$Suite = "Boundaries",
        [double]$DurationMs = 0
    )

    $sw = [System.Diagnostics.Stopwatch]::StartNew()
    $threw = $false
    $actualType = "None"
    $caughtMsg = ""

    try {
        & $ScriptBlock
    } catch {
        $threw = $true
        $currEx = $_.Exception
        while ($currEx) {
            if ($currEx.GetType().FullName -like "*$ExpectedExceptionType*") {
                $actualType = $currEx.GetType().FullName
                $caughtMsg = $currEx.Message
                break
            }
            if ($currEx.InnerException) {
                $currEx = $currEx.InnerException
            } else {
                $actualType = $currEx.GetType().FullName
                $caughtMsg = $currEx.Message
                break
            }
        }
    }
    $sw.Stop()

    $passed = $threw -and ($actualType -like "*$ExpectedExceptionType*" -or $ExpectedExceptionType -eq "System.Exception")
    $err = if ($passed) { "" } else { "Expected exception [$ExpectedExceptionType], caught: [$actualType]" }
    Record-TestResult -Tier $Tier -Suite $Suite -TestName $TestName -Passed $passed `
        -Expected "Exception: $ExpectedExceptionType" -Actual "Caught: $actualType ($caughtMsg)" `
        -ErrorMessage $err -DurationMs $sw.TotalMilliseconds
    return $passed
}

function Assert-Match {
    param(
        [string]$Actual,
        [string]$Pattern,
        [string]$TestName,
        [string]$Tier = "T1",
        [string]$Suite = "General",
        [double]$DurationMs = 0,
        [bool]$IsKnownMilestoneDefect = $false
    )

    $passed = ($Actual -match $Pattern)
    $err = if ($passed) { "" } else { "String '$Actual' does not match pattern '$Pattern'" }
    Record-TestResult -Tier $Tier -Suite $Suite -TestName $TestName -Passed $passed `
        -Expected "Regex pattern: $Pattern" -Actual $Actual -ErrorMessage $err -DurationMs $DurationMs `
        -IsKnownMilestoneDefect $IsKnownMilestoneDefect
    return $passed
}

function Export-TestSummaryJson {
    param([string]$OutputPath)
    if ([string]::IsNullOrWhiteSpace($OutputPath)) {
        $OutputPath = Join-Path $global:ProjectRoot "tests\TestResults.json"
    }
    $json = $global:TestResults | ConvertTo-Json -Depth 5
    Set-Content -Path $OutputPath -Value $json -Encoding UTF8
    return $OutputPath
}

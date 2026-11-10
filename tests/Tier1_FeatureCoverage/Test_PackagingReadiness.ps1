# ==============================================================================
# Tier 1 - Test_PackagingReadiness.ps1
# Rubric: Đóng gói (1.0 pt) - ClickOnce Deployment Configuration & Build Integrity
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 1: Packaging & Deployment Readiness Audit ---" -ForegroundColor Cyan

$csprojPath = Join-Path $global:ProjectRoot "DoAnCuoiKi_QuanLyTiemCaPhe.csproj"
Assert-True (Test-Path $csprojPath) "Project file DoAnCuoiKi_QuanLyTiemCaPhe.csproj exists" "Tier 1" "Packaging"

[xml]$csprojXml = Get-Content $csprojPath

# Test 1.1: ClickOnce Configuration Tags in .csproj
$propGroups = $csprojXml.Project.PropertyGroup
$allProps = @{}
foreach ($pg in $propGroups) {
    foreach ($child in $pg.ChildNodes) {
        if ($child.Name) {
            $allProps[$child.Name] = $child.InnerText
        }
    }
}

Assert-Equal $allProps["PublishUrl"] "publish\" "PublishUrl configured to 'publish\'" "Tier 1" "Packaging"
Assert-Equal $allProps["Install"] "true" "Install tag configured to 'true'" "Tier 1" "Packaging"
Assert-Equal $allProps["GenerateManifests"] "true" "GenerateManifests tag configured to 'true'" "Tier 1" "Packaging"
Assert-Equal $allProps["IsWebBootstrapper"] "false" "IsWebBootstrapper configured to 'false' (Local Disk Deployment)" "Tier 1" "Packaging"

# Test 1.2: Release Build Binary Verification
$releaseExePath = Join-Path $global:ProjectRoot "bin\Release\DoAnCuoiKi_QuanLyTiemCaPhe.exe"
$hasReleaseExe = Test-Path $releaseExePath

Assert-True $hasReleaseExe "Release executable exists in bin\Release\ (Build Succeeded)" "Tier 1" "Packaging"

# Test 1.3: Classic .sln file presence for backwards compatibility (Survey 1 / M5 Feature 16)
$slnPath = Join-Path $global:ProjectRoot "DoAnCuoiKi_QuanLyTiemCaPhe.sln"
$hasSln = Test-Path $slnPath
Assert-True $hasSln "Classic Visual Studio solution file (.sln) exists alongside .slnx" `
    "Tier 1" "Packaging" -IsKnownMilestoneDefect (-not $hasSln)

# Test 1.4: Publish output folder verification
$publishDir = Join-Path $global:ProjectRoot "publish"
$hasPublishDir = Test-Path $publishDir
Assert-True $hasPublishDir "Publish directory exists for ClickOnce setup artifacts" "Tier 1" "Packaging"

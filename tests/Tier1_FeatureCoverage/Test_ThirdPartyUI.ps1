# ==============================================================================
# Tier 1 - Test_ThirdPartyUI.ps1
# Rubric: UI 3rd-party (1.0 pt) - Guna.UI2 / Krypton / Third-Party WinForms Controls
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 1: Third-Party UI Library Audit ---" -ForegroundColor Cyan

$packagesConfigPath = Join-Path $global:ProjectRoot "packages.config"
$csprojPath = Join-Path $global:ProjectRoot "DoAnCuoiKi_QuanLyTiemCaPhe.csproj"
$loginDesignerPath = Join-Path $global:ProjectRoot "Form\VanThanh\FrmLogin.Designer.cs"

# Test 1: packages.config has 3rd-party UI package
$packagesXml = [xml](Get-Content $packagesConfigPath)
$hasUiPackage = ($packagesXml.packages.package | Where-Object { $_.id -match "Guna\.UI2|Krypton|Bunifu" }) -ne $null
Assert-True $hasUiPackage "packages.config references a modern third-party UI library (Guna.UI2 / Krypton)" "Tier 1" "ThirdPartyUI"

# Test 2: csproj references 3rd-party UI assembly
$csprojContent = Get-Content $csprojPath -Raw
$hasCsprojRef = ($csprojContent -match 'Include="Guna\.UI2|Include="Krypton\.Toolkit')
Assert-True $hasCsprojRef "DoAnCuoiKi_QuanLyTiemCaPhe.csproj references third-party UI assembly" "Tier 1" "ThirdPartyUI"

# Test 3: Assembly exists in output directory
$gunaDllPath = Join-Path $global:ProjectRoot "bin\Debug\Guna.UI2.dll"
Assert-True (Test-Path $gunaDllPath) "Third-party UI binary (Guna.UI2.dll) deployed to bin\Debug\" "Tier 1" "ThirdPartyUI"

# Test 4: Form Designer integrates third-party controls
$loginDesignerContent = Get-Content $loginDesignerPath -Raw
$hasGunaControl = ($loginDesignerContent -match 'Guna\.UI2\.WinForms\.Guna2')
Assert-True $hasGunaControl "FrmLogin.Designer integrates Guna2 UI controls (Guna2Button)" "Tier 1" "ThirdPartyUI"

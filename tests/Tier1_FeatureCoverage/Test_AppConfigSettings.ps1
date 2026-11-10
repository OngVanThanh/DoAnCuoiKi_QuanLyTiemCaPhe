# ==============================================================================
# Tier 1 - Test_AppConfigSettings.ps1
# Rubric: App.config (1.0 pt) - Connection Strings, AppSettings & Relative Paths
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 1: App.config Settings Audit ---" -ForegroundColor Cyan

$appConfigPath = Join-Path $global:ProjectRoot "App.config"
$sw = [System.Diagnostics.Stopwatch]::StartNew()
Assert-True (Test-Path $appConfigPath) "App.config file exists at project root" "Tier 1" "AppConfig" -DurationMs $sw.ElapsedMilliseconds

[xml]$xml = Get-Content $appConfigPath

# Test 1.1: Connection Strings verification
$connStrings = $xml.configuration.connectionStrings.add
$connNames = @($connStrings | ForEach-Object { $_.name })

Assert-True ($connNames -contains "CoffeeShopDB") "ConnectionStrings contains 'CoffeeShopDB'" "Tier 1" "AppConfig"
Assert-True ($connNames -contains "DoAnCuoiKi_QuanLyTiemCaPhe.Properties.Settings.CoffeeShopDBConnectionString") `
    "ConnectionStrings contains Dataset 'CoffeeShopDBConnectionString'" "Tier 1" "AppConfig"
Assert-True ($connNames -contains "DoAnCuoiKi_QuanLyTiemCaPhe.Properties.Settings.CoffeeShopDBConnectionString1") `
    "ConnectionStrings contains Dataset 'CoffeeShopDBConnectionString1'" "Tier 1" "AppConfig"

# Test 1.2: AppSettings cafe metadata verification
$appSettings = @{}
foreach ($setting in $xml.configuration.appSettings.add) {
    $appSettings[$setting.key] = $setting.value
}

Assert-True ($appSettings.ContainsKey("ShopName")) "AppSettings contains 'ShopName'" "Tier 1" "AppConfig"
Assert-True ($appSettings.ContainsKey("ShopAddress")) "AppSettings contains 'ShopAddress'" "Tier 1" "AppConfig"
Assert-True ($appSettings.ContainsKey("ShopPhone")) "AppSettings contains 'ShopPhone'" "Tier 1" "AppConfig"
Assert-True ($appSettings.ContainsKey("VATPercent")) "AppSettings contains 'VATPercent'" "Tier 1" "AppConfig"
Assert-True ($appSettings.ContainsKey("WifiInfo")) "AppSettings contains 'WifiInfo'" "Tier 1" "AppConfig"

# Test 1.3: Absolute local drive path check
# Rubric rule: "Sử dụng địa chỉ tương đối (không dùng địa chỉ tuyệt đối)"
$rawConfigText = Get-Content $appConfigPath -Raw
$hasAbsoluteDrivePath = ($rawConfigText -match '[a-zA-Z]:\\')
Assert-False $hasAbsoluteDrivePath "App.config contains zero hardcoded absolute drive paths" "Tier 1" "AppConfig"

# Test 1.4: Runtime retrieval via ConfigHelper
$asm = Get-ProjectAssembly
$configHelperType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Utilities.ConfigHelper")
Assert-NotNull $configHelperType "Utilities.ConfigHelper class found in assembly" "Tier 1" "AppConfig"

if ($configHelperType) {
    $getConnMethod = $configHelperType.GetMethod("GetConnectionString", [System.Reflection.BindingFlags]"Public,Static")
    $resolvedConn = $getConnMethod.Invoke($null, @("CoffeeShopDB"))
    Assert-True (-not [string]::IsNullOrWhiteSpace($resolvedConn)) "ConfigHelper resolves 'CoffeeShopDB' connection string" "Tier 1" "AppConfig"
}

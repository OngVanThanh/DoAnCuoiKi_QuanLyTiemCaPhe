# ==============================================================================
# Tier 1 - Test_RelativePathsAndImages.ps1
# Rubric: Relative Paths (Lưu ý bắt buộc: Sử dụng địa chỉ tương đối, không dùng địa chỉ tuyệt đối)
# Milestone 3: Relative Paths & Image Normalization
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 1: Relative Paths & Image Normalization Audit ---" -ForegroundColor Cyan

# Test 3.1: Zero Image.FromFile in UserControls
$ucFiles = Get-ChildItem -Path (Join-Path $global:ProjectRoot "UserControl") -Filter "*.cs"
$fromFileMatchesInUC = @($ucFiles | Select-String -Pattern "Image\.FromFile")
Assert-Equal $fromFileMatchesInUC.Count 0 "Zero Image.FromFile calls in UserControl layer" "Tier 1" "RelativePaths"

# Test 3.2: Zero Image.FromFile across the entire Presentation Layer
$presentationFiles = Get-ChildItem -Path (Join-Path $global:ProjectRoot "Form"), (Join-Path $global:ProjectRoot "UserControl") -Filter "*.cs" -Recurse
$fromFileMatchesInPresentation = @($presentationFiles | Select-String -Pattern "Image\.FromFile")
Assert-Equal $fromFileMatchesInPresentation.Count 0 "Zero Image.FromFile calls across entire Presentation Layer" "Tier 1" "RelativePaths"

# Test 3.3: Zero raw Image.FromFile across all source code (.cs)
$allCsFiles = Get-ChildItem -Path $global:ProjectRoot -Filter "*.cs" -Recurse | Where-Object { $_.FullName -notmatch '\\(obj|bin|tests|\.agents)\\' }
$allFromFileMatches = @($allCsFiles | Select-String -Pattern "Image\.FromFile")
Assert-Equal $allFromFileMatches.Count 0 "Zero raw Image.FromFile calls in all project source files" "Tier 1" "RelativePaths"

# Test 3.4: Zero hardcoded local drive paths in source code (.cs)
$allDriveMatches = @($allCsFiles | Select-String -Pattern '(?i)[a-z]:[\\/]' | Where-Object {
    $_.Line -notmatch 'http://' -and $_.Line -notmatch 'https://' -and $_.Line -notmatch ':\\n'
})
Assert-Equal $allDriveMatches.Count 0 "Zero hardcoded local drive paths in project source files" "Tier 1" "RelativePaths"

# Test 3.5: Reflection check on UIHelper.LoadImageToPictureBox
$asm = Get-ProjectAssembly
$uiHelperType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Utilities.UIHelper")
Assert-NotNull $uiHelperType "Utilities.UIHelper class exists in assembly" "Tier 1" "RelativePaths"

$loadImgMethod = $uiHelperType.GetMethod("LoadImageToPictureBox", [System.Reflection.BindingFlags]"Public,Static")
Assert-NotNull $loadImgMethod "UIHelper.LoadImageToPictureBox method exists" "Tier 1" "RelativePaths"

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

# Test 3.6: Graceful handling of null, empty, and non-existent paths
$pb = [System.Windows.Forms.PictureBox]::new()
$loadImgMethod.Invoke($null, [object[]]@($pb, $null))
Assert-True ($pb.Image -eq $null) "UIHelper.LoadImageToPictureBox handles null path gracefully" "Tier 1" "RelativePaths"

$loadImgMethod.Invoke($null, [object[]]@($pb, "   "))
Assert-True ($pb.Image -eq $null) "UIHelper.LoadImageToPictureBox handles whitespace path gracefully" "Tier 1" "RelativePaths"

$loadImgMethod.Invoke($null, [object[]]@($pb, "NonExistentFolder\non_existent_img_12345.png"))
Assert-True ($pb.Image -eq $null) "UIHelper.LoadImageToPictureBox handles non-existent file without throwing" "Tier 1" "RelativePaths"

# Test 3.7: Successful loading of relative image and Zoom SizeMode
$testProductImg = "Images\Products\prod_20261110_004038_b23c8.png"
$fullProductPath = Join-Path $global:ProjectRoot "bin\Debug\$testProductImg"

if (Test-Path $fullProductPath) {
    $loadImgMethod.Invoke($null, [object[]]@($pb, $testProductImg))
    Assert-NotNull $pb.Image "UIHelper loads relative path product image successfully" "Tier 1" "RelativePaths"
    Assert-Equal ([int]$pb.SizeMode) ([int][System.Windows.Forms.PictureBoxSizeMode]::Zoom) "UIHelper sets PictureBoxSizeMode to Zoom" "Tier 1" "RelativePaths"

    # Test 3.8: Non-locking verification (Zero File Lock)
    # If the file were locked by Image.FromFile, opening it for Write will throw an IOException
    $fileLockTestPassed = $false
    try {
        $writeStream = [System.IO.File]::Open($fullProductPath, [System.IO.FileMode]::Open, [System.IO.FileAccess]::ReadWrite, [System.IO.FileShare]::None)
        $writeStream.Close()
        $writeStream.Dispose()
        $fileLockTestPassed = $true
    } catch {
        $fileLockTestPassed = $false
    }
    Assert-True $fileLockTestPassed "Image file on disk is NOT locked after UIHelper.LoadImageToPictureBox" "Tier 1" "RelativePaths"
}

# Test 3.9: EmployeeService relative image normalization
$empServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.EmployeeService")
Assert-NotNull $empServiceType "BLL.EmployeeService class exists in assembly" "Tier 1" "RelativePaths"

$empDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.EmployeeDTO")
Assert-NotNull $empDtoType "DTO.EmployeeDTO class exists in assembly" "Tier 1" "RelativePaths"

# Test 3.10: Ensure Images\Employees target directory exists
$empImgDir = Join-Path $global:ProjectRoot "bin\Debug\Images\Employees"
Assert-True (Test-Path $empImgDir) "Target directory 'Images\Employees' exists in application directory" "Tier 1" "RelativePaths"

# ==============================================================================
# Milestone 3 Challenger Stress Suite: Relative Paths & Image Normalization
# High-rigor empirical verification for Forensic Auditor
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
Write-Host "         CHALLENGER SUITE: MILESTONE 3 - RELATIVE PATHS & IMAGES         " -ForegroundColor Yellow
Write-Host "==========================================================================" -ForegroundColor Cyan

$asm = Get-ProjectAssembly
Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

$uiHelperType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Utilities.UIHelper")
$loadImgMethod = $uiHelperType.GetMethod("LoadImageToPictureBox", [System.Reflection.BindingFlags]"Public,Static")
$empServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.EmployeeService")
$empDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.EmployeeDTO")

$totalM3Tests = 0
$passedM3Tests = 0

function Check-M3([bool]$condition, [string]$name, [string]$details = "") {
    $script:totalM3Tests++
    if ($condition) {
        $script:passedM3Tests++
        Write-Host "  [PASS] $name" -ForegroundColor Green
    } else {
        Write-Host "  [FAIL] $name - $details" -ForegroundColor Red
    }
}

# --- SECTION 1: CODEBASE INTEGRITY AUDIT ---
Write-Host "`n--- [Section 1] Static Code Analysis & Architecture Constraints ---" -ForegroundColor Cyan

$csFiles = Get-ChildItem -Path $global:ProjectRoot -Filter "*.cs" -Recurse | Where-Object { $_.FullName -notmatch '\\(obj|bin|tests|\.agents)\\' }

$fromFileOccurrences = @($csFiles | Select-String -Pattern "Image\.FromFile")
Check-M3 ($fromFileOccurrences.Count -eq 0) "Zero Image.FromFile calls across all C# source code" "Found: $($fromFileOccurrences.Count)"

$driveMatches = @($csFiles | Select-String -Pattern '(?i)[a-z]:[\\/]' | Where-Object {
    $_.Line -notmatch 'http://' -and $_.Line -notmatch 'https://' -and $_.Line -notmatch ':\\n'
})
Check-M3 ($driveMatches.Count -eq 0) "Zero hardcoded local drive paths in all C# source files" "Found: $($driveMatches.Count)"

# --- SECTION 2: UIHELPER EXTREME BOUNDARY TESTING ---
Write-Host "`n--- [Section 2] UIHelper.LoadImageToPictureBox Boundary & Edge Cases ---" -ForegroundColor Cyan

$pb = [System.Windows.Forms.PictureBox]::new()

# Null PictureBox check (should not throw NullReferenceException)
try {
    $loadImgMethod.Invoke($null, [object[]]@($null, "Images\some.jpg"))
    Check-M3 $true "UIHelper handles null PictureBox without throwing"
} catch {
    Check-M3 $false "UIHelper threw on null PictureBox" $_.Exception.Message
}

# Null, Empty, Whitespace, Newline strings
$emptyInputs = @($null, "", "   ", "`t", "`r`n", "   `t`n  ")
$allEmptyPassed = $true
foreach ($inp in $emptyInputs) {
    $loadImgMethod.Invoke($null, [object[]]@($pb, $inp))
    if ($pb.Image -ne $null) { $allEmptyPassed = $false }
}
Check-M3 $allEmptyPassed "UIHelper handles all permutations of null/empty/whitespace strings gracefully"

# Non-existent relative paths
$nonExistentPaths = @(
    "Images\Employees\non_existent_avatar_9999.jpg",
    "DoesNotExist\Sub\img.png",
    "../../outside_file.png",
    "/Images/Employees/missing.jpg",
    "\Images\Employees\missing.jpg"
)
$allMissingPassed = $true
foreach ($path in $nonExistentPaths) {
    try {
        $loadImgMethod.Invoke($null, [object[]]@($pb, $path))
        if ($pb.Image -ne $null) { $allMissingPassed = $false }
    } catch {
        $allMissingPassed = $false
    }
}
Check-M3 $allMissingPassed "UIHelper handles non-existent and malformed relative paths without throwing"

# Zero-byte and corrupted file test
$tempCorruptFile = Join-Path $global:ProjectRoot "bin\Debug\Images\corrupt_test.jpg"
[System.IO.File]::WriteAllBytes($tempCorruptFile, @(0x00, 0x01, 0x02, 0x03))
try {
    $loadImgMethod.Invoke($null, [object[]]@($pb, "Images\corrupt_test.jpg"))
    Check-M3 ($pb.Image -eq $null) "UIHelper handles corrupted/invalid image file safely without throwing"
} catch {
    Check-M3 $false "UIHelper threw exception on corrupted image file" $_.Exception.Message
} finally {
    if (Test-Path $tempCorruptFile) { Remove-Item $tempCorruptFile -Force }
}

# --- SECTION 3: FILE LOCKING & STREAM INTEGRITY ---
Write-Host "`n--- [Section 3] Zero File-Lock Verification & Stress Cycling ---" -ForegroundColor Cyan

# Create a dedicated temporary test image
$testImgDir = Join-Path $global:ProjectRoot "bin\Debug\Images\Employees"
if (-not (Test-Path $testImgDir)) { New-Item -ItemType Directory -Path $testImgDir -Force | Out-Null }

$testBmp = New-Object System.Drawing.Bitmap(64, 64)
$g = [System.Drawing.Graphics]::FromImage($testBmp)
$g.Clear([System.Drawing.Color]::Blue)
$g.Dispose()

$testImgPath = Join-Path $testImgDir "test_lock_verify.png"
$testBmp.Save($testImgPath, [System.Drawing.Imaging.ImageFormat]::Png)
$testBmp.Dispose()

$relTestImg = "Images\Employees\test_lock_verify.png"

# Load via UIHelper
$loadImgMethod.Invoke($null, [object[]]@($pb, $relTestImg))
Check-M3 ($pb.Image -ne $null) "UIHelper loaded valid test image successfully"
Check-M3 ($pb.SizeMode -eq [System.Windows.Forms.PictureBoxSizeMode]::Zoom) "PictureBox.SizeMode is set to Zoom"

# Test 1: Immediate overwrite / delete while PictureBox holds image
$canWriteWhileLoaded = $false
try {
    # Open for write with exclusive access - will throw IOException if UIHelper locked the file
    $fs = [System.IO.File]::Open($testImgPath, [System.IO.FileMode]::Open, [System.IO.FileAccess]::ReadWrite, [System.IO.FileShare]::None)
    $fs.WriteByte(0xFF)
    $fs.Close()
    $fs.Dispose()
    $canWriteWhileLoaded = $true
} catch {
    $canWriteWhileLoaded = $false
}
Check-M3 $canWriteWhileLoaded "Image file can be opened for exclusive ReadWrite while displayed in PictureBox (Zero File Lock)"

# Test 2: Rapid 50-cycle stress test of loading & disposing
$stressPassed = $true
try {
    for ($i = 0; $i -lt 50; $i++) {
        $loadImgMethod.Invoke($null, [object[]]@($pb, $relTestImg))
    }
} catch {
    $stressPassed = $false
}
Check-M3 $stressPassed "Rapid 50x reload cycle completed without GDI+ memory or handle exhaustion"

# Clean up test image
$pb.Image = $null
if (Test-Path $testImgPath) { Remove-Item $testImgPath -Force }

# --- SECTION 4: EMPLOYEE SERVICE RELATIVE STORAGE ---
Write-Host "`n--- [Section 4] EmployeeService Relative Path Normalization ---" -ForegroundColor Cyan

$empService = [Activator]::CreateInstance([type]$empServiceType)
$dto = [Activator]::CreateInstance([type]$empDtoType)

# Create a temporary dummy source image outside the project directory
$tempSourceFile = [System.IO.Path]::GetTempFileName() + ".png"
$srcBmp = New-Object System.Drawing.Bitmap(32, 32)
$srcBmp.Save($tempSourceFile, [System.Drawing.Imaging.ImageFormat]::Png)
$srcBmp.Dispose()

# Test SaveEmployee overload with sourceImagePath
$saveMethodWithSource = $empServiceType.GetMethod("SaveEmployee", [type[]]@($empDtoType, [string], [string].MakeByRefType()))
Check-M3 ($saveMethodWithSource -ne $null) "EmployeeService.SaveEmployee(dto, sourceImagePath, out errorMsg) exists"

$errMsg = ""
$dto.FullName = "Nguyễn Văn Test M3"
$dto.Phone = "0987654321"
$dto.RoleId = 2
$dto.Address = "123 Đường Test"

# Invoke with temporary source image
$invokeArgs = [object[]]@($dto, $tempSourceFile, $errMsg)
$saveResult = $saveMethodWithSource.Invoke($empService, $invokeArgs)

Check-M3 ($dto.Image -like "Images\Employees\emp_*") "DTO.Image was transformed into relative path: '$($dto.Image)'"
Check-M3 ($dto.Image -notmatch '^[A-Za-z]:') "DTO.Image does NOT contain absolute drive letter"

$savedFileOnDisk = Join-Path $global:ProjectRoot "bin\Debug\$($dto.Image)"
if (-not (Test-Path $savedFileOnDisk)) {
    $savedFileOnDisk = Join-Path $global:ProjectRoot $dto.Image
}
Check-M3 (Test-Path $savedFileOnDisk) "Normalized employee image file successfully created in 'Images\Employees'"

# Clean up
if (Test-Path $tempSourceFile) { Remove-Item $tempSourceFile -Force }
if (Test-Path $savedFileOnDisk) { Remove-Item $savedFileOnDisk -Force }

# --- SECTION 5: USERCONTROL INTEGRATION AUDIT ---
Write-Host "`n--- [Section 5] UserControl Instantiation & Avatar Display Verification ---" -ForegroundColor Cyan

$ucAddType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.UCEmployeeAdd")
$ucEditType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.UCEmployeeEdit")
$ucInformType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.UCEmployeeInform")

Check-M3 ($ucAddType -ne $null) "UCEmployeeAdd type resolved"
Check-M3 ($ucEditType -ne $null) "UCEmployeeEdit type resolved"
Check-M3 ($ucInformType -ne $null) "UCEmployeeInform type resolved"

# Instantiate controls safely
$ucAdd = $null
$ucEdit = $null
$ucInform = $null

try {
    $ucAdd = [Activator]::CreateInstance([type]$ucAddType)
    Check-M3 ($ucAdd -ne $null) "UCEmployeeAdd instantiated successfully"
} catch {
    Check-M3 $false "UCEmployeeAdd instantiation failed" $_.Exception.Message
}

try {
    $ucEdit = [Activator]::CreateInstance([type]$ucEditType)
    Check-M3 ($ucEdit -ne $null) "UCEmployeeEdit instantiated successfully"
} catch {
    Check-M3 $false "UCEmployeeEdit instantiation failed" $_.Exception.Message
}

try {
    $ucInform = [Activator]::CreateInstance([type]$ucInformType)
    Check-M3 ($ucInform -ne $null) "UCEmployeeInform instantiated successfully"
} catch {
    Check-M3 $false "UCEmployeeInform instantiation failed" $_.Exception.Message
}

# Test UCEmployeeInform.BindData with empty image
$testDto = [Activator]::CreateInstance([type]$empDtoType)
$testDto.Id = 999
$testDto.FullName = "Avatar Test User"
$testDto.Phone = "0900000000"
$testDto.RoleId = 1
$testDto.Image = ""

if ($ucInform -ne $null) {
    try {
        $ucInformType.GetMethod("BindData").Invoke($ucInform, @($testDto))
        Check-M3 $true "UCEmployeeInform.BindData executed cleanly with empty image"
    } catch {
        Check-M3 $false "UCEmployeeInform.BindData failed on empty image" $_.Exception.Message
    }
}

# Test UCEmployeeEdit.SetEmployeeData with empty image
if ($ucEdit -ne $null) {
    try {
        $ucEditType.GetMethod("SetEmployeeData").Invoke($ucEdit, @($testDto))
        Check-M3 $true "UCEmployeeEdit.SetEmployeeData executed cleanly with empty image"
    } catch {
        Check-M3 $false "UCEmployeeEdit.SetEmployeeData failed on empty image" $_.Exception.Message
    }
}

# Clean up controls
if ($ucAdd -ne $null) { $ucAdd.Dispose() }
if ($ucEdit -ne $null) { $ucEdit.Dispose() }
if ($ucInform -ne $null) { $ucInform.Dispose() }
$pb.Dispose()

Write-Host "`n==========================================================================" -ForegroundColor Cyan
Write-Host "               CHALLENGER SUITE SUMMARY: MILESTONE 3                      " -ForegroundColor Yellow
Write-Host "==========================================================================" -ForegroundColor Cyan
Write-Host "Total Tests : $totalM3Tests" -ForegroundColor White
Write-Host "Passed      : $passedM3Tests" -ForegroundColor Green
Write-Host "Failed      : $($totalM3Tests - $passedM3Tests)" -ForegroundColor $(if ($totalM3Tests -eq $passedM3Tests) { "Green" } else { "Red" })
Write-Host "==========================================================================" -ForegroundColor Cyan

if ($totalM3Tests -ne $passedM3Tests) {
    exit 1
} else {
    exit 0
}

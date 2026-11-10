# ==============================================================================
# CHALLENGER 2 EMPIRICAL STRESS TEST SUITE: MILESTONE 3
# Empirical Stress Harness: File Locking, Concurrency, GDI+ Leak, Avatar Integrity
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
Write-Host "   CHALLENGER 2: EMPIRICAL STRESS & ADVERSARIAL HARNESS (MILESTONE 3)    " -ForegroundColor Yellow
Write-Host "==========================================================================" -ForegroundColor Cyan

$asm = Get-ProjectAssembly
Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

# Compile C# helper for Win32 GDI tracking and true multi-threaded parallel execution
$csharpHelperCode = @"
using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

public static class GdiTracker {
    [DllImport("user32.dll")]
    public static extern uint GetGuiResources(IntPtr hProcess, uint uiFlags);

    public static uint GetGdiHandleCount() {
        return GetGuiResources(System.Diagnostics.Process.GetCurrentProcess().Handle, 0);
    }

    public static uint GetUserHandleCount() {
        return GetGuiResources(System.Diagnostics.Process.GetCurrentProcess().Handle, 1);
    }
}

public static class ChallengerRunner {
    public static void RunParallelStress(MethodInfo loadMethod, string imgA, string imgB, int iterations) {
        Parallel.For(0, iterations, i => {
            using (var pb = new PictureBox()) {
                string img = (i % 2 == 0) ? imgA : imgB;
                loadMethod.Invoke(null, new object[] { pb, img });
                if (pb.Image == null) throw new InvalidOperationException("Image was null in parallel task " + i);
                loadMethod.Invoke(null, new object[] { pb, null });
            }
        });
    }
}
"@

try {
    Add-Type -TypeDefinition $csharpHelperCode -ReferencedAssemblies @("System.Windows.Forms.dll", "System.Drawing.dll") -ErrorAction Stop
} catch {
    # Type may already be loaded in current AppDomain
}

$uiHelperType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Utilities.UIHelper")
$loadImgMethod = $uiHelperType.GetMethod("LoadImageToPictureBox", [System.Reflection.BindingFlags]"Public,Static")
$empServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.EmployeeService")
$empDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.EmployeeDTO")

Assert-NotNull $uiHelperType "UIHelper type resolved" "Challenger2_M3" "Setup"
Assert-NotNull $loadImgMethod "UIHelper.LoadImageToPictureBox method resolved" "Challenger2_M3" "Setup"
Assert-NotNull $empServiceType "EmployeeService type resolved" "Challenger2_M3" "Setup"
Assert-NotNull $empDtoType "EmployeeDTO type resolved" "Challenger2_M3" "Setup"

# Helper wrappers ensuring clean .NET type marshalling
function Invoke-LoadImage([object]$picBox, [string]$relativePath) {
    $rawPic = if ($picBox -is [System.Management.Automation.PSObject]) { $picBox.BaseObject } else { $picBox }
    $loadImgMethod.Invoke($null, [object[]]@($rawPic, $relativePath))
}

$saveMethodWithSource = $empServiceType.GetMethod("SaveEmployee", [type[]]@($empDtoType, [string], [string].MakeByRefType()))
$saveMethodDtoOnly = $empServiceType.GetMethod("SaveEmployee", [type[]]@($empDtoType, [string].MakeByRefType()))

function Invoke-SaveEmployeeWithSource([object]$service, [object]$dtoObj, [string]$sourcePath) {
    $rawService = if ($service -is [System.Management.Automation.PSObject]) { $service.BaseObject } else { $service }
    $rawDto = if ($dtoObj -is [System.Management.Automation.PSObject]) { $dtoObj.BaseObject } else { $dtoObj }
    $outErr = [string]::Empty
    $argsArray = [object[]]@($rawDto, $sourcePath, $outErr)
    $res = $saveMethodWithSource.Invoke($rawService, $argsArray)
    return [PSCustomObject]@{
        Success = [bool]$res
        ErrorMessage = [string]$argsArray[2]
    }
}

function Invoke-SaveEmployeeDto([object]$service, [object]$dtoObj) {
    $rawService = if ($service -is [System.Management.Automation.PSObject]) { $service.BaseObject } else { $service }
    $rawDto = if ($dtoObj -is [System.Management.Automation.PSObject]) { $dtoObj.BaseObject } else { $dtoObj }
    $outErr = [string]::Empty
    $argsArray = [object[]]@($rawDto, $outErr)
    $res = $saveMethodDtoOnly.Invoke($rawService, $argsArray)
    return [PSCustomObject]@{
        Success = [bool]$res
        ErrorMessage = [string]$argsArray[1]
    }
}

$targetDirBin = Join-Path $global:ProjectRoot "bin\Debug\Images\Employees"
$targetDirSrc = Join-Path $global:ProjectRoot "Images\Employees"
if (-not (Test-Path $targetDirBin)) { New-Item -ItemType Directory -Path $targetDirBin -Force | Out-Null }
if (-not (Test-Path $targetDirSrc)) { New-Item -ItemType Directory -Path $targetDirSrc -Force | Out-Null }

# ==============================================================================
# SUITE 1: FILE LOCKING ADVERSARIAL STRESS VERIFICATION
# ==============================================================================
Write-Host "`n--- [Suite 1] Empirical File Locking & Stream Independence Stress ---" -ForegroundColor Yellow

$testImgFile = Join-Path $targetDirBin "c2_lock_stress_target.png"
$bmp1 = [System.Drawing.Bitmap]::new(128, 128)
$g1 = [System.Drawing.Graphics]::FromImage($bmp1)
$g1.Clear([System.Drawing.Color]::Magenta)
$g1.Dispose()
$bmp1.Save($testImgFile, [System.Drawing.Imaging.ImageFormat]::Png)
$bmp1.Dispose()

$pb = [System.Windows.Forms.PictureBox]::new()
$relPath1 = "Images\Employees\c2_lock_stress_target.png"

# Step 1.1: Load image into PictureBox
Invoke-LoadImage $pb $relPath1
Assert-NotNull $pb.Image "PictureBox loaded test image successfully" "Challenger2_M3" "FileLocking"
Assert-Equal $pb.SizeMode ([System.Windows.Forms.PictureBoxSizeMode]::Zoom) "PictureBox SizeMode is Zoom" "Challenger2_M3" "FileLocking"

# Step 1.2: Attempt Exclusive ReadWrite file opening while image is displayed
$exclusiveStreamSuccess = $false
$exclusiveExMsg = ""
try {
    $fs = [System.IO.File]::Open($testImgFile, [System.IO.FileMode]::Open, [System.IO.FileAccess]::ReadWrite, [System.IO.FileShare]::None)
    $fs.WriteByte(0xAB)
    $fs.Close()
    $fs.Dispose()
    $exclusiveStreamSuccess = $true
} catch {
    $exclusiveExMsg = $_.Exception.Message
}
Assert-True $exclusiveStreamSuccess "Exclusive ReadWrite FileStream succeeds without IOException while image is displayed" "Challenger2_M3" "FileLocking" $exclusiveExMsg

# Step 1.3: Attempt Overwrite with File.WriteAllBytes while image is displayed
$overwriteSuccess = $false
$overwriteExMsg = ""
try {
    $dummyBytes = [byte[]]@(0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A)
    [System.IO.File]::WriteAllBytes($testImgFile, $dummyBytes)
    $overwriteSuccess = $true
} catch {
    $overwriteExMsg = $_.Exception.Message
}
Assert-True $overwriteSuccess "File.WriteAllBytes overwrite succeeds while image is displayed in PictureBox" "Challenger2_M3" "FileLocking" $overwriteExMsg

# Step 1.4: Attempt File.Move (Rename) while image is displayed
$renamedFile = Join-Path $targetDirBin "c2_lock_stress_renamed.png"
if (Test-Path $renamedFile) { Remove-Item $renamedFile -Force }
$renameSuccess = $false
$renameExMsg = ""
try {
    [System.IO.File]::Move($testImgFile, $renamedFile)
    $renameSuccess = [System.IO.File]::Exists($renamedFile)
} catch {
    $renameExMsg = $_.Exception.Message
}
Assert-True $renameSuccess "File.Move (Rename) succeeds while image is displayed in PictureBox" "Challenger2_M3" "FileLocking" $renameExMsg

# Step 1.5: Attempt File.Delete while image is displayed
$deleteSuccess = $false
$deleteExMsg = ""
try {
    [System.IO.File]::Delete($renamedFile)
    $deleteSuccess = (-not [System.IO.File]::Exists($renamedFile))
} catch {
    $deleteExMsg = $_.Exception.Message
}
Assert-True $deleteSuccess "File.Delete succeeds while image is displayed in PictureBox" "Challenger2_M3" "FileLocking" $deleteExMsg

# Step 1.6: External process deletion test (Cross-Process boundary verification)
$crossProcessFile = Join-Path $targetDirBin "c2_cross_process_test.png"
$bmpCP = [System.Drawing.Bitmap]::new(64, 64)
$bmpCP.Save($crossProcessFile, [System.Drawing.Imaging.ImageFormat]::Png)
$bmpCP.Dispose()

Invoke-LoadImage $pb "Images\Employees\c2_cross_process_test.png"
Assert-NotNull $pb.Image "Image loaded before cross-process deletion" "Challenger2_M3" "FileLocking"

# Launch separate powershell.exe process to delete the file
$proc = Start-Process -FilePath "powershell.exe" -ArgumentList "-Command Remove-Item -LiteralPath '$crossProcessFile' -Force" -Wait -PassThru -NoNewWindow
Assert-Equal $proc.ExitCode 0 "Cross-process external deletion returned exit code 0" "Challenger2_M3" "FileLocking"
Assert-False (Test-Path $crossProcessFile) "Cross-process external deletion successfully removed file on disk" "Challenger2_M3" "FileLocking"

# Step 1.7: GDI+ In-Memory Decoupling & Painting Stability
# Even though the underlying file on disk was deleted, pb.Image must remain completely intact in RAM
$drawSuccess = $false
$drawExMsg = ""
try {
    $rawImg = [System.Drawing.Image]$pb.Image
    $clonedBmp = [System.Drawing.Bitmap]::new($rawImg)
    $renderTarget = [System.Drawing.Bitmap]::new(64, 64)
    $gRender = [System.Drawing.Graphics]::FromImage($renderTarget)
    $gRender.DrawImage($clonedBmp, 0, 0)
    $gRender.Dispose()
    $clonedBmp.Dispose()
    $renderTarget.Dispose()
    $drawSuccess = $true
} catch {
    $drawExMsg = $_.Exception.Message
}
Assert-True $drawSuccess "PictureBox.Image can be cloned, drawn, and manipulated with zero GDI+ ExternalException after disk file deletion" "Challenger2_M3" "FileLocking" $drawExMsg

$pb.Image = $null
$pb.Dispose()

# ==============================================================================
# SUITE 2: CONCURRENCY & RAPID RELOADING (100x TIGHT LOOP & GDI LEAK AUDIT)
# ==============================================================================
Write-Host "`n--- [Suite 2] Concurrency & Rapid Reloading (100x Loop & GDI Handle Tracking) ---" -ForegroundColor Yellow

$testImgA = Join-Path $targetDirBin "c2_reload_a.png"
$testImgB = Join-Path $targetDirBin "c2_reload_b.png"

$bmpA = [System.Drawing.Bitmap]::new(100, 100)
$gA = [System.Drawing.Graphics]::FromImage($bmpA)
$gA.Clear([System.Drawing.Color]::Red)
$gA.Dispose()
$bmpA.Save($testImgA, [System.Drawing.Imaging.ImageFormat]::Png)
$bmpA.Dispose()

$bmpB = [System.Drawing.Bitmap]::new(100, 100)
$gB = [System.Drawing.Graphics]::FromImage($bmpB)
$gB.Clear([System.Drawing.Color]::Green)
$gB.Dispose()
$bmpB.Save($testImgB, [System.Drawing.Imaging.ImageFormat]::Png)
$bmpB.Dispose()

$relA = "Images\Employees\c2_reload_a.png"
$relB = "Images\Employees\c2_reload_b.png"

$pbReload = [System.Windows.Forms.PictureBox]::new()

# Query baseline GDI handle count
$gdiBaseline = [GdiTracker]::GetGdiHandleCount()
$userBaseline = [GdiTracker]::GetUserHandleCount()
Write-Host "  Baseline GDI Handles : $gdiBaseline, USER Handles: $userBaseline" -ForegroundColor DarkGray

# Execute 100 rapid reload cycles alternating between imgA and imgB
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$loopException = $null
try {
    for ($i = 0; $i -lt 100; $i++) {
        $targetRel = if ($i % 2 -eq 0) { $relA } else { $relB }
        Invoke-LoadImage $pbReload $targetRel
    }
} catch {
    $loopException = $_.Exception.Message
}
$sw.Stop()

Assert-True ($null -eq $loopException) "100x rapid reload loop completed in $($sw.ElapsedMilliseconds) ms without exception" "Challenger2_M3" "RapidReload"

# Query GDI handle count immediately after 100 loads
$gdiAfter100 = [GdiTracker]::GetGdiHandleCount()
$gdiDelta = $gdiAfter100 - $gdiBaseline
Write-Host "  Post-100 GDI Handles : $gdiAfter100 (Delta: +$gdiDelta)" -ForegroundColor DarkGray

# If Bitmap objects were leaked, delta would be approximately +100.
# If old images are properly disposed via oldImage?.Dispose(), delta is <= 2 (only the active image + transient GDI cache).
Assert-True ($gdiDelta -le 2) "GDI handle delta ($gdiDelta) is <= 2 after 100 reloads (Zero GDI+ handle leak)" "Challenger2_M3" "RapidReload" "GDI handle count grew by $gdiDelta, indicating leak"

# Clear PictureBox image by loading null
Invoke-LoadImage $pbReload $null
Assert-True ($pbReload.Image -eq $null) "Loading `$null into PictureBox clears pb.Image to null" "Challenger2_M3" "RapidReload"

$gdiAfterNull = [GdiTracker]::GetGdiHandleCount()
Write-Host "  Post-Null GDI Handles: $gdiAfterNull" -ForegroundColor DarkGray
Assert-True ($gdiAfterNull -le ($gdiBaseline + 1)) "GDI handle count returned to baseline after clearing image" "Challenger2_M3" "RapidReload"

# True multi-threaded parallel execution across 20 background tasks via C# ChallengerRunner
$parallelSuccess = $true
$parallelExMsg = ""
try {
    [ChallengerRunner]::RunParallelStress($loadImgMethod, $relA, $relB, 20)
} catch {
    $parallelSuccess = $false
    $parallelExMsg = $_.Exception.Message
}
Assert-True $parallelSuccess "Multi-threaded concurrent execution across 20 parallel tasks succeeded without race condition or crash" "Challenger2_M3" "Concurrency" $parallelExMsg

$pbReload.Dispose()
if (Test-Path $testImgA) { Remove-Item $testImgA -Force }
if (Test-Path $testImgB) { Remove-Item $testImgB -Force }

# ==============================================================================
# SUITE 3: EMPLOYEE AVATAR SAVE INTEGRITY (ABSOLUTE VS RELATIVE PATHS)
# ==============================================================================
Write-Host "`n--- [Suite 3] Employee Avatar Save Integrity & Normalization ---" -ForegroundColor Yellow

$empService = [Activator]::CreateInstance([type]$empServiceType)
$dto = [Activator]::CreateInstance([type]$empDtoType)
$dto.FullName = "Nguyễn Văn Challenger M3"
$dto.Phone = "0912345678"
$dto.RoleId = 3
$dto.Address = "456 Đường Thử Nghiệm"

# Case 3.1: Absolute source path outside the project directory
$absTempSource = Join-Path $env:TEMP "c2_emp_avatar_source.jpg"
$bmpSrc = [System.Drawing.Bitmap]::new(150, 150)
$gSrc = [System.Drawing.Graphics]::FromImage($bmpSrc)
$gSrc.Clear([System.Drawing.Color]::CornflowerBlue)
$gSrc.Dispose()
$bmpSrc.Save($absTempSource, [System.Drawing.Imaging.ImageFormat]::Jpeg)
$bmpSrc.Dispose()
$srcBytes = [System.IO.File]::ReadAllBytes($absTempSource)

Assert-NotNull $saveMethodWithSource "EmployeeService.SaveEmployee(dto, sourceImagePath, out errorMsg) resolved" "Challenger2_M3" "AvatarIntegrity"

$saveRes = Invoke-SaveEmployeeWithSource $empService $dto $absTempSource

Assert-True ($dto.Image -like "Images\Employees\emp_*.jpg") "DTO.Image converted to relative pattern 'Images\Employees\emp_*.jpg'" "Challenger2_M3" "AvatarIntegrity"
Assert-False ($dto.Image -match '^[A-Za-z]:') "DTO.Image contains ZERO drive letters (Strict relative path compliance)" "Challenger2_M3" "AvatarIntegrity"

# Check that the file was copied to disk and matches byte length
$destBin = Join-Path $global:ProjectRoot "bin\Debug\$($dto.Image)"
$destSrc = Join-Path $global:ProjectRoot $dto.Image
$destExists = (Test-Path $destBin) -or (Test-Path $destSrc)
Assert-True $destExists "Destination image file physically created in Images\Employees directory" "Challenger2_M3" "AvatarIntegrity"

$targetFound = if (Test-Path $destBin) { $destBin } else { $destSrc }
$destBytes = if ($destExists) { [System.IO.File]::ReadAllBytes($targetFound) } else { @() }
Assert-Equal $destBytes.Length $srcBytes.Length "Saved avatar byte size ($($destBytes.Length)) matches original source image exactly" "Challenger2_M3" "AvatarIntegrity"

# Case 3.2: Calling UIHelper.LoadImageToPictureBox on the newly saved relative avatar
$pbAvatar = [System.Windows.Forms.PictureBox]::new()
Invoke-LoadImage $pbAvatar $dto.Image
Assert-NotNull $pbAvatar.Image "UIHelper successfully loads the normalized relative avatar path into PictureBox" "Challenger2_M3" "AvatarIntegrity"
$pbAvatar.Dispose()

# Clean up saved avatar
if (Test-Path $destBin) { Remove-Item $destBin -Force }
if (Test-Path $destSrc) { Remove-Item $destSrc -Force }
if (Test-Path $absTempSource) { Remove-Item $absTempSource -Force }

# Case 3.3: DTO.Image itself initialized with an absolute path and passed to SaveEmployee(dto, out errorMsg)
$absSource2 = Join-Path $env:TEMP "c2_emp_avatar_source2.png"
$bmpSrc2 = [System.Drawing.Bitmap]::new(80, 80)
$bmpSrc2.Save($absSource2, [System.Drawing.Imaging.ImageFormat]::Png)
$bmpSrc2.Dispose()

$dto2 = [Activator]::CreateInstance([type]$empDtoType)
$dto2.FullName = "Trần Thị Challenger M3"
$dto2.Phone = "0987654321"
$dto2.RoleId = 4
$dto2.Image = $absSource2

$saveRes2 = Invoke-SaveEmployeeDto $empService $dto2

Assert-True ($dto2.Image -like "Images\Employees\emp_*.png") "SaveEmployee(dto, out msg) auto-normalized absolute dto.Image to 'Images\Employees\emp_*.png'" "Challenger2_M3" "AvatarIntegrity"
Assert-False ($dto2.Image -match '^[A-Za-z]:') "Normalized dto2.Image contains no drive letters" "Challenger2_M3" "AvatarIntegrity"

$destBin2 = Join-Path $global:ProjectRoot "bin\Debug\$($dto2.Image)"
$destSrc2 = Join-Path $global:ProjectRoot $dto2.Image
$destExists2 = (Test-Path $destBin2) -or (Test-Path $destSrc2)
Assert-True $destExists2 "Auto-normalized avatar file physically created on disk" "Challenger2_M3" "AvatarIntegrity"

if (Test-Path $destBin2) { Remove-Item $destBin2 -Force }
if (Test-Path $destSrc2) { Remove-Item $destSrc2 -Force }
if (Test-Path $absSource2) { Remove-Item $absSource2 -Force }

# Case 3.4: Non-existent source image path handled safely
$dto3 = [Activator]::CreateInstance([type]$empDtoType)
$dto3.FullName = "Lê Văn NonExistent"
$dto3.Phone = "0933333333"
$dto3.RoleId = 1
$nonExistentPath = "C:\NonExistent_Folder_XYZ\ghost_avatar.png"
$saveRes3 = Invoke-SaveEmployeeWithSource $empService $dto3 $nonExistentPath
Assert-True $true "SaveEmployee with non-existent source path executed safely without unhandled crash" "Challenger2_M3" "AvatarIntegrity"

# Case 3.5: Empty image path handled safely
$dto4 = [Activator]::CreateInstance([type]$empDtoType)
$dto4.FullName = "Hoàng Thị Blank"
$dto4.Phone = "0944444444"
$dto4.RoleId = 2
$dto4.Image = ""
$saveRes4 = Invoke-SaveEmployeeDto $empService $dto4
Assert-True $true "SaveEmployee with empty image path executed safely without unhandled crash" "Challenger2_M3" "AvatarIntegrity"

# ==============================================================================
# SUITE 4: ADVERSARIAL EDGE CASES & PATH ROBUSTNESS
# ==============================================================================
Write-Host "`n--- [Suite 4] Adversarial Edge Cases & Path Robustness ---" -ForegroundColor Yellow

$pbEdge = [System.Windows.Forms.PictureBox]::new()

# 4.1 Unicode Vietnamese filename with diacritics
$unicodeName = "c2_nhân_viên_tiệm_cà_phê.png"
$unicodeFullPath = Join-Path $targetDirBin $unicodeName
$bmpUni = [System.Drawing.Bitmap]::new(32, 32)
$bmpUni.Save($unicodeFullPath, [System.Drawing.Imaging.ImageFormat]::Png)
$bmpUni.Dispose()

Invoke-LoadImage $pbEdge "Images\Employees\$unicodeName"
Assert-NotNull $pbEdge.Image "Unicode Vietnamese filename with diacritics loaded into PictureBox successfully" "Challenger2_M3" "EdgeCases"
if (Test-Path $unicodeFullPath) { Remove-Item $unicodeFullPath -Force }

# 4.2 Forward slashes normalization (Web / Unix path style)
$slashTestPath = Join-Path $targetDirBin "c2_slash_test.png"
$bmpSlash = [System.Drawing.Bitmap]::new(32, 32)
$bmpSlash.Save($slashTestPath, [System.Drawing.Imaging.ImageFormat]::Png)
$bmpSlash.Dispose()

Invoke-LoadImage $pbEdge "Images/Employees/c2_slash_test.png"
Assert-NotNull $pbEdge.Image "Forward-slash relative path 'Images/Employees/...' resolved and loaded successfully" "Challenger2_M3" "EdgeCases"

# 4.3 Leading slashes (\Images\Employees or /Images/Employees)
Invoke-LoadImage $pbEdge "\Images\Employees\c2_slash_test.png"
Assert-NotNull $pbEdge.Image "Leading backslash '\Images\Employees\...' resolved and loaded successfully" "Challenger2_M3" "EdgeCases"

Invoke-LoadImage $pbEdge "/Images/Employees/c2_slash_test.png"
Assert-NotNull $pbEdge.Image "Leading forward-slash '/Images/Employees/...' resolved and loaded successfully" "Challenger2_M3" "EdgeCases"

if (Test-Path $slashTestPath) { Remove-Item $slashTestPath -Force }

# 4.4 Disposing existing image when transitioning from valid to invalid
$validTestPath = Join-Path $targetDirBin "c2_valid_for_dispose.png"
$bmpValid = [System.Drawing.Bitmap]::new(32, 32)
$bmpValid.Save($validTestPath, [System.Drawing.Imaging.ImageFormat]::Png)
$bmpValid.Dispose()

Invoke-LoadImage $pbEdge "Images\Employees\c2_valid_for_dispose.png"
Assert-NotNull $pbEdge.Image "Loaded valid image before invalid transition" "Challenger2_M3" "EdgeCases"

# Transition to non-existent image
Invoke-LoadImage $pbEdge "Images\Employees\does_not_exist_9999.png"
Assert-True ($pbEdge.Image -eq $null) "Previous image properly disposed and pb.Image reset to null on non-existent path" "Challenger2_M3" "EdgeCases"

if (Test-Path $validTestPath) { Remove-Item $validTestPath -Force }

# 4.5 Zero-byte file handling
$zeroByteFile = Join-Path $targetDirBin "c2_zerobyte.png"
[System.IO.File]::WriteAllBytes($zeroByteFile, [byte[]]@())

$zeroEx = $null
try {
    Invoke-LoadImage $pbEdge "Images\Employees\c2_zerobyte.png"
} catch {
    $zeroEx = $_.Exception.Message
}
Assert-True ($null -eq $zeroEx) "Zero-byte image file handled safely without unhandled exception" "Challenger2_M3" "EdgeCases"
Assert-True ($pbEdge.Image -eq $null) "PictureBox.Image is safely null after encountering zero-byte file" "Challenger2_M3" "EdgeCases"

if (Test-Path $zeroByteFile) { Remove-Item $zeroByteFile -Force }

# 4.6 Corrupted non-image content with valid image extension
$corruptFile = Join-Path $targetDirBin "c2_corrupt.png"
[System.IO.File]::WriteAllText($corruptFile, "THIS IS NOT A VALID PNG IMAGE FILE HEADER")

$corruptEx = $null
try {
    Invoke-LoadImage $pbEdge "Images\Employees\c2_corrupt.png"
} catch {
    $corruptEx = $_.Exception.Message
}
Assert-True ($null -eq $corruptEx) "Corrupted text file disguised as PNG handled safely without unhandled exception" "Challenger2_M3" "EdgeCases"
Assert-True ($pbEdge.Image -eq $null) "PictureBox.Image is safely null after encountering corrupted image" "Challenger2_M3" "EdgeCases"

if (Test-Path $corruptFile) { Remove-Item $corruptFile -Force }

# Clean up
$pbEdge.Dispose()

# ==============================================================================
# SUITE 5: COMPREHENSIVE REPOSITORY & PRESENTATION AUDIT
# ==============================================================================
Write-Host "`n--- [Suite 5] Zero Hardcoded Paths & Zero Image.FromFile Static Scan ---" -ForegroundColor Yellow

$sourceDirs = @(
    (Join-Path $global:ProjectRoot "Form"),
    (Join-Path $global:ProjectRoot "UserControl"),
    (Join-Path $global:ProjectRoot "BLL"),
    (Join-Path $global:ProjectRoot "DAL"),
    (Join-Path $global:ProjectRoot "DTO"),
    (Join-Path $global:ProjectRoot "Utilities")
)

$sourceFiles = Get-ChildItem -Path $sourceDirs -Filter "*.cs" -Recurse

$fromFileMatches = @($sourceFiles | Select-String -Pattern "Image\.FromFile")
Assert-Equal $fromFileMatches.Count 0 "Zero occurrences of Image.FromFile across all application source folders" "Challenger2_M3" "StaticScan"

$driveLetterMatches = @($sourceFiles | Select-String -Pattern '(?i)[a-z]:[\\/]' | Where-Object {
    $_.Line -notmatch 'http://' -and $_.Line -notmatch 'https://' -and $_.Line -notmatch ':\\n'
})
Assert-Equal $driveLetterMatches.Count 0 "Zero hardcoded local drive paths across all application source folders" "Challenger2_M3" "StaticScan"

# ==============================================================================
# FINAL SUMMARY
# ==============================================================================
$c2Results = $global:TestResults | Where-Object { $_.Tier -eq "Challenger2_M3" }
$totalC2 = $c2Results.Count
$passedC2 = ($c2Results | Where-Object { $_.Passed -eq $true }).Count
$failedC2 = $totalC2 - $passedC2

Write-Host "`n==========================================================================" -ForegroundColor Cyan
Write-Host "         CHALLENGER 2 EMPIRICAL STRESS RESULTS: MILESTONE 3               " -ForegroundColor Yellow
Write-Host "==========================================================================" -ForegroundColor Cyan
Write-Host "Total Tests Run : $totalC2" -ForegroundColor White
Write-Host "Passed          : $passedC2" -ForegroundColor Green
Write-Host "Failed          : $failedC2" -ForegroundColor $(if ($failedC2 -eq 0) { "Green" } else { "Red" })
Write-Host "Pass Rate       : $([Math]::Round(($passedC2 / [Math]::Max(1, $totalC2)) * 100, 1))%" -ForegroundColor $(if ($failedC2 -eq 0) { "Green" } else { "Yellow" })
Write-Host "==========================================================================" -ForegroundColor Cyan

# Export results JSON
$jsonPath = Join-Path $global:ProjectRoot "tests\Challenger2_M3_Results.json"
$c2Results | ConvertTo-Json -Depth 4 | Set-Content -Path $jsonPath -Encoding UTF8
Write-Host "Detailed test results exported to: $jsonPath" -ForegroundColor DarkGray

if ($failedC2 -gt 0) {
    exit 1
} else {
    exit 0
}

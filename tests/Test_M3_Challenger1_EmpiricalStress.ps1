# ==============================================================================
# CHALLENGER 1 EMPIRICAL STRESS TEST HARNESS — MILESTONE 3: RELATIVE PATHS & IMAGES
# Adversarial testing: Directory relocation, boundary strings, path traversal,
# corrupt binaries, path length stress, memory disposal, zero file-locks.
# ==============================================================================

param(
    [string]$ProjectRoot = "d:\DoAnCuoiKi_QuanLyTiemCaPhe"
)

$ErrorActionPreference = "Continue"

Write-Host "==========================================================================" -ForegroundColor Cyan
Write-Host "   CHALLENGER 1: EMPIRICAL ADVERSARIAL STRESS SUITE — MILESTONE 3        " -ForegroundColor Yellow
Write-Host "==========================================================================" -ForegroundColor Cyan

$testResults = [System.Collections.Generic.List[PSObject]]::new()

function Assert-Check {
    param(
        [string]$Category,
        [string]$TestName,
        [bool]$Passed,
        [string]$Expected,
        [string]$Actual,
        [string]$Details = ""
    )

    $obj = [PSCustomObject]@{
        Category = $Category
        TestName = $TestName
        Passed   = $Passed
        Expected = $Expected
        Actual   = $Actual
        Details  = $Details
    }
    $testResults.Add($obj)

    $badge = if ($Passed) { "[PASS]" } else { "[FAIL]" }
    $color = if ($Passed) { "Green" } else { "Red" }
    Write-Host "  $badge " -ForegroundColor $color -NoNewline
    Write-Host "[$Category] $TestName" -ForegroundColor White
    if (-not $Passed) {
        Write-Host "         Expected: $Expected" -ForegroundColor DarkCyan
        Write-Host "         Actual:   $Actual" -ForegroundColor Magenta
        if ($Details) {
            Write-Host "         Details:  $Details" -ForegroundColor DarkRed
        }
    }
}

# --- 1. Load Assembly & Types ---
$binPath = Join-Path $ProjectRoot "bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe"
if (-not (Test-Path $binPath)) {
    Write-Host "[FATAL] Assembly not found at $binPath" -ForegroundColor Red
    exit 1
}

[System.Reflection.Assembly]::LoadWithPartialName("System.Drawing") | Out-Null
[System.Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms") | Out-Null
$asm = [System.Reflection.Assembly]::LoadFrom($binPath)

$uiHelperType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Utilities.UIHelper")
$loadImgMethod = $uiHelperType.GetMethod("LoadImageToPictureBox", [System.Reflection.BindingFlags]"Public,Static")
$empServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.EmployeeService")
$empDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.EmployeeDTO")
$ucAddType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.UCEmployeeAdd")
$ucEditType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.UCEmployeeEdit")
$ucInformType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.UCEmployeeInform")

# Setup temporary test image directories
$empImgDir = Join-Path $ProjectRoot "bin\Debug\Images\Employees"
if (-not (Test-Path $empImgDir)) { New-Item -ItemType Directory -Path $empImgDir -Force | Out-Null }
$rootEmpImgDir = Join-Path $ProjectRoot "Images\Employees"
if (-not (Test-Path $rootEmpImgDir)) { New-Item -ItemType Directory -Path $rootEmpImgDir -Force | Out-Null }

# Generate test valid image 1
$testImg1Path = Join-Path $empImgDir "challenger_valid1.png"
$bmp1 = New-Object System.Drawing.Bitmap(100, 100)
$g1 = [System.Drawing.Graphics]::FromImage($bmp1)
$g1.Clear([System.Drawing.Color]::Teal)
$g1.Dispose()
$bmp1.Save($testImg1Path, [System.Drawing.Imaging.ImageFormat]::Png)
$bmp1.Dispose()

# Also copy to root Images\Employees
Copy-Item $testImg1Path (Join-Path $rootEmpImgDir "challenger_valid1.png") -Force

# Generate test valid image 2
$testImg2Path = Join-Path $empImgDir "challenger_valid2.png"
$bmp2 = New-Object System.Drawing.Bitmap(120, 120)
$g2 = [System.Drawing.Graphics]::FromImage($bmp2)
$g2.Clear([System.Drawing.Color]::Goldenrod)
$g2.Dispose()
$bmp2.Save($testImg2Path, [System.Drawing.Imaging.ImageFormat]::Png)
$bmp2.Dispose()

$pb = [System.Windows.Forms.PictureBox]::new()

# ==============================================================================
# SECTION 1: WORKING DIRECTORY RELOCATION ATTACK
# ==============================================================================
Write-Host "`n--- [Section 1] Working Directory Relocation Attack ---" -ForegroundColor Cyan
$origLocation = Get-Location

$testLocations = @(
    "C:\",
    "C:\Windows\System32",
    $env:TEMP
)

foreach ($loc in $testLocations) {
    if (Test-Path $loc) {
        try {
            [System.IO.Directory]::SetCurrentDirectory($loc)
            Set-Location $loc

            # Test 1.1: Load relative image while in altered directory
            $loadImgMethod.Invoke($null, [object[]]@($pb, "Images\Employees\challenger_valid1.png"))
            $loaded = ($pb.Image -ne $null -and $pb.Image.Width -eq 100)
            Assert-Check "DirRelocation" "Load relative image while CurrentDirectory is '$loc'" $loaded "Image loaded (100x100)" "Loaded=$loaded, Width=$($pb.Image.Width)"

            # Test 1.2: PictureBox SizeMode is Zoom
            Assert-Check "DirRelocation" "PictureBox.SizeMode is Zoom while at '$loc'" ($pb.SizeMode -eq [System.Windows.Forms.PictureBoxSizeMode]::Zoom) "Zoom" "$($pb.SizeMode)"

            # Test 1.3: UCEmployeeInform BindData while in altered directory
            $ucInform = [Activator]::CreateInstance([type]$ucInformType)
            $dto = [Activator]::CreateInstance([type]$empDtoType)
            $dto.Id = 777
            $dto.FullName = "Test Relocation Employee"
            $dto.Image = "Images\Employees\challenger_valid1.png"
            $dto.RoleId = 1

            $informSuccess = $false
            try {
                $ucInformType.GetMethod("BindData").Invoke($ucInform, @($dto))
                $informSuccess = $true
            } catch {
                $informSuccess = $false
            }
            Assert-Check "DirRelocation" "UCEmployeeInform.BindData works while CurrentDirectory is '$loc'" $informSuccess "True" "$informSuccess"
            $ucInform.Dispose()

            # Test 1.4: UCEmployeeEdit SetEmployeeData while in altered directory
            $ucEdit = [Activator]::CreateInstance([type]$ucEditType)
            $editSuccess = $false
            try {
                $ucEditType.GetMethod("SetEmployeeData").Invoke($ucEdit, @($dto))
                $editSuccess = $true
            } catch {
                $editSuccess = $false
            }
            Assert-Check "DirRelocation" "UCEmployeeEdit.SetEmployeeData works while CurrentDirectory is '$loc'" $editSuccess "True" "$editSuccess"
            $ucEdit.Dispose()

        } finally {
            [System.IO.Directory]::SetCurrentDirectory($ProjectRoot)
            Set-Location $origLocation
        }
    }
}

# ==============================================================================
# SECTION 2: BOUNDARY & EXTREME STRING INPUTS
# ==============================================================================
Write-Host "`n--- [Section 2] Boundary & Extreme String Inputs ---" -ForegroundColor Cyan

# Test 2.1: Null PictureBox parameter
$nullPbThrew = $false
try {
    $loadImgMethod.Invoke($null, [object[]]@($null, "Images\Employees\challenger_valid1.png"))
} catch {
    $nullPbThrew = $true
}
Assert-Check "BoundaryStrings" "UIHelper handles null PictureBox safely without throwing" (-not $nullPbThrew) "No exception" "Threw=$nullPbThrew"

# Test 2.2: Null, Empty, Whitespace, Control Whitespace
$blankInputs = @(
    @{ Name = "Null string"; Val = $null },
    @{ Name = "Empty string"; Val = "" },
    @{ Name = "Single space"; Val = " " },
    @{ Name = "Tab string"; Val = "`t`t" },
    @{ Name = "Newline string"; Val = "`r`n`r`n" },
    @{ Name = "Mixed whitespace"; Val = "  `t`r`n   " },
    @{ Name = "Non-breaking space"; Val = "$([char]0x00A0)   " },
    @{ Name = "Zero-width space"; Val = "$([char]0x200B)   " }
)

foreach ($item in $blankInputs) {
    # Preload valid image first to ensure it gets cleared
    $loadImgMethod.Invoke($null, [object[]]@($pb, "Images\Employees\challenger_valid1.png"))
    $hadImageBefore = ($pb.Image -ne $null)

    $loadImgMethod.Invoke($null, [object[]]@($pb, $item.Val))
    $cleared = ($pb.Image -eq $null)
    Assert-Check "BoundaryStrings" "Input '$($item.Name)' clears PictureBox.Image" ($hadImageBefore -and $cleared) "Cleared to null" "Image=$($pb.Image)"
}

# Test 2.3: Control characters & Illegal Path Characters
$illegalPathInputs = @(
    @{ Name = "Null byte in path"; Val = "Images\Employees\`0test.png" },
    @{ Name = "Asterisk wildcard in path"; Val = "Images\Employees\*.jpg" },
    @{ Name = "Question mark wildcard in path"; Val = "Images\Employees\?.png" },
    @{ Name = "Angle brackets in path"; Val = "Images\Employees\<bad>.jpg" },
    @{ Name = "Pipe character in path"; Val = "Images\Employees\|bad.jpg" },
    @{ Name = "Double quote in path"; Val = "Images\Employees\`"bad`".jpg" },
    @{ Name = "Colon in relative path"; Val = "Images\Employees\bad:name.jpg" },
    @{ Name = "Bell control character"; Val = "Images\Employees\`abad.jpg" }
)

foreach ($item in $illegalPathInputs) {
    # Preload valid image
    $loadImgMethod.Invoke($null, [object[]]@($pb, "Images\Employees\challenger_valid1.png"))

    $threw = $false
    $errMsg = ""
    try {
        $loadImgMethod.Invoke($null, [object[]]@($pb, $item.Val))
    } catch {
        $threw = $true
        $errMsg = $_.Exception.Message
    }

    $safeAndCleared = (-not $threw -and $pb.Image -eq $null)
    Assert-Check "BoundaryStrings" "Illegal input '$($item.Name)' handled without exception" $safeAndCleared "0 exceptions, Image=null" "Threw=$threw, Image=$($pb.Image), Err=$errMsg"
}

# ==============================================================================
# SECTION 3: PATH TRAVERSAL ATTACK SUITE
# ==============================================================================
Write-Host "`n--- [Section 3] Path Traversal Attack Suite ---" -ForegroundColor Cyan

$traversalInputs = @(
    @{ Name = "Traverse to Notepad.exe (valid binary outside project)"; Val = "..\..\..\..\..\..\..\..\Windows\System32\notepad.exe" },
    @{ Name = "Traverse to Win.ini (valid text file outside project)"; Val = "..\..\..\..\..\..\..\..\Windows\win.ini" },
    @{ Name = "Traverse to hosts file"; Val = "..\..\..\..\..\..\..\..\Windows\System32\drivers\etc\hosts" },
    @{ Name = "Traverse to non-existent system file"; Val = "..\..\..\..\..\..\..\..\bootmgr" },
    @{ Name = "Traverse to root drive"; Val = "..\..\..\..\..\..\..\..\non_existent_drive_root.png" },
    @{ Name = "Valid internal traversal"; Val = "Images\..\Images\Employees\challenger_valid1.png" }
)

foreach ($item in $traversalInputs) {
    $loadImgMethod.Invoke($null, [object[]]@($pb, "Images\Employees\challenger_valid1.png"))

    $threw = $false
    $errMsg = ""
    try {
        $loadImgMethod.Invoke($null, [object[]]@($pb, $item.Val))
    } catch {
        $threw = $true
        $errMsg = $_.Exception.Message
    }

    if ($item.Name -like "*internal traversal*") {
        # Internal traversal should successfully resolve the valid image
        $passed = (-not $threw -and $pb.Image -ne $null)
        Assert-Check "PathTraversal" "$($item.Name) resolves correctly" $passed "Image loaded" "Threw=$threw, Image=$($pb.Image)"
    } else {
        # Outside non-image or missing file should never crash and should clear image
        $passed = (-not $threw -and $pb.Image -eq $null)
        Assert-Check "PathTraversal" "$($item.Name) blocked/cleared safely" $passed "Image=null, 0 exceptions" "Threw=$threw, Image=$($pb.Image), Err=$errMsg"
    }
}

# ==============================================================================
# SECTION 4: INVALID FILE FORMATS & CORRUPT BINARY FUZZING
# ==============================================================================
Write-Host "`n--- [Section 4] Invalid File Formats & Corrupt Binary Fuzzing ---" -ForegroundColor Cyan

$corruptFilesDir = Join-Path $empImgDir "corrupt_samples"
if (-not (Test-Path $corruptFilesDir)) { New-Item -ItemType Directory -Path $corruptFilesDir -Force | Out-Null }

$corruptTestCases = @(
    @{
        Name = "0-byte empty file"
        Path = (Join-Path $corruptFilesDir "empty.jpg")
        Bytes = [byte[]]@()
    },
    @{
        Name = "1-byte partial file"
        Path = (Join-Path $corruptFilesDir "singlebyte.png")
        Bytes = [byte[]]@(0xFF)
    },
    @{
        Name = "Truncated PNG magic bytes (8 bytes only)"
        Path = (Join-Path $corruptFilesDir "truncated.png")
        Bytes = [byte[]]@(0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A)
    },
    @{
        Name = "Truncated JPEG SOI marker (2 bytes only)"
        Path = (Join-Path $corruptFilesDir "truncated.jpg")
        Bytes = [byte[]]@(0xFF, 0xD8)
    },
    @{
        Name = "Plain ASCII text renamed to .jpg"
        Path = (Join-Path $corruptFilesDir "fake_text.jpg")
        Bytes = [System.Text.Encoding]::ASCII.GetBytes("This is plain text and definitely not a valid JPEG image file.")
    },
    @{
        Name = "C# Source Code renamed to .png"
        Path = (Join-Path $corruptFilesDir "fake_code.png")
        Bytes = [System.Text.Encoding]::UTF8.GetBytes("using System; namespace Exploit { class Crash { } }")
    },
    @{
        Name = "Random pseudo-binary fuzz (4KB noise) renamed to .bmp"
        Path = (Join-Path $corruptFilesDir "random_fuzz.bmp")
        Bytes = (1..4096 | ForEach-Object { [byte](Get-Random -Minimum 0 -Maximum 256) })
    },
    @{
        Name = "MZ Executable Header renamed to .png"
        Path = (Join-Path $corruptFilesDir "fake_exe.png")
        Bytes = [byte[]]@(0x4D, 0x5A, 0x90, 0x00, 0x03, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0xFF, 0xFF)
    }
)

foreach ($tc in $corruptTestCases) {
    [System.IO.File]::WriteAllBytes($tc.Path, $tc.Bytes)
    $relCorruptPath = "Images\Employees\corrupt_samples\" + (Split-Path $tc.Path -Leaf)

    # Preload valid image
    $loadImgMethod.Invoke($null, [object[]]@($pb, "Images\Employees\challenger_valid1.png"))

    $threw = $false
    $errMsg = ""
    try {
        $loadImgMethod.Invoke($null, [object[]]@($pb, $relCorruptPath))
    } catch {
        $threw = $true
        $errMsg = $_.Exception.Message
    }

    $safe = (-not $threw -and $pb.Image -eq $null)
    Assert-Check "CorruptFiles" "Handle $($tc.Name) safely" $safe "Image=null, 0 exceptions" "Threw=$threw, Image=$($pb.Image), Err=$errMsg"
}

# Clean up corrupt directory
if (Test-Path $corruptFilesDir) { Remove-Item $corruptFilesDir -Recurse -Force }

# ==============================================================================
# SECTION 5: EXTREMELY LONG PATH STRINGS (PATH TOO LONG STRESS)
# ==============================================================================
Write-Host "`n--- [Section 5] Extremely Long Path Strings Stress ---" -ForegroundColor Cyan

$longPathLengths = @(250, 260, 300, 1000, 10000, 32767)

foreach ($len in $longPathLengths) {
    $longStr = "Images\Employees\" + ("A" * ($len - 17)) + ".png"

    # Preload valid image
    $loadImgMethod.Invoke($null, [object[]]@($pb, "Images\Employees\challenger_valid1.png"))

    $threw = $false
    $errMsg = ""
    try {
        $loadImgMethod.Invoke($null, [object[]]@($pb, $longStr))
    } catch {
        $threw = $true
        $errMsg = $_.Exception.Message
    }

    $safe = (-not $threw -and $pb.Image -eq $null)
    Assert-Check "PathLengthStress" "Path string of length $len handled safely" $safe "Image=null, 0 exceptions" "Threw=$threw, Image=$($pb.Image), Err=$errMsg"
}

# ==============================================================================
# SECTION 6: MEMORY CLEANLINESS & OLD IMAGE DISPOSAL ORACLE
# ==============================================================================
Write-Host "`n--- [Section 6] Memory Cleanliness & Image Disposal Oracle ---" -ForegroundColor Cyan

# Test 6.1: Load valid image 1, verify disposal when replaced by null
$loadImgMethod.Invoke($null, [object[]]@($pb, "Images\Employees\challenger_valid1.png"))
$oldImg1 = $pb.Image
$loadImgMethod.Invoke($null, [object[]]@($pb, $null))

$img1Disposed = $false
try {
    # In GDI+, calling Clone() on a disposed Bitmap throws ArgumentException: "Parameter is not valid."
    $null = $oldImg1.Clone()
} catch {
    $img1Disposed = $true
}
Assert-Check "MemoryDisposal" "Old image was actively disposed when PictureBox cleared with null" ($img1Disposed -and $pb.Image -eq $null) "Throws ArgumentException (Disposed) & Image=null" "Disposed=$img1Disposed, Image=$($pb.Image)"

# Test 6.2: Load valid image 1, verify disposal when replaced by valid image 2
$loadImgMethod.Invoke($null, [object[]]@($pb, "Images\Employees\challenger_valid1.png"))
$oldImgA = $pb.Image
$loadImgMethod.Invoke($null, [object[]]@($pb, "Images\Employees\challenger_valid2.png"))
$newImgB = $pb.Image

$oldImgADisposed = $false
try {
    $null = $oldImgA.Clone()
} catch {
    $oldImgADisposed = $true
}
$isDifferentInstance = ($newImgB -ne $null -and $newImgB -ne $oldImgA -and $newImgB.Width -eq 120)
Assert-Check "MemoryDisposal" "Old image disposed when replaced by new valid image" ($oldImgADisposed -and $isDifferentInstance) "Old disposed & New loaded" "OldDisposed=$oldImgADisposed, NewLoaded=$isDifferentInstance"

# Test 6.3: Load valid image, verify disposal when replaced by non-existent image
$loadImgMethod.Invoke($null, [object[]]@($pb, "Images\Employees\challenger_valid1.png"))
$oldImgBeforeMissing = $pb.Image
$loadImgMethod.Invoke($null, [object[]]@($pb, "Images\Employees\missing_xyz_404.jpg"))

$oldMissingDisposed = $false
try {
    $null = $oldImgBeforeMissing.Clone()
} catch {
    $oldMissingDisposed = $true
}
Assert-Check "MemoryDisposal" "Old image disposed when replaced by missing file" ($oldMissingDisposed -and $pb.Image -eq $null) "Old disposed & Image=null" "OldDisposed=$oldMissingDisposed, Image=$($pb.Image)"

# ==============================================================================
# SECTION 7: ZERO FILE-LOCK CONCURRENCY & RAPID HIGH-LOAD STRESS
# ==============================================================================
Write-Host "`n--- [Section 7] Zero File-Lock Concurrency & Rapid High-Load Stress ---" -ForegroundColor Cyan

# Test 7.1: Verify image file can be opened for exclusive ReadWrite while displayed
$loadImgMethod.Invoke($null, [object[]]@($pb, "Images\Employees\challenger_valid1.png"))
$exclusiveWriteSuccess = $false
try {
    $fs = [System.IO.File]::Open($testImg1Path, [System.IO.FileMode]::Open, [System.IO.FileAccess]::ReadWrite, [System.IO.FileShare]::None)
    # Write same byte back
    $b = $fs.ReadByte()
    $fs.Position = 0
    $fs.WriteByte([byte]$b)
    $fs.Close()
    $fs.Dispose()
    $exclusiveWriteSuccess = $true
} catch {
    $exclusiveWriteSuccess = $false
}
Assert-Check "ConcurrencyLocks" "File opened with exclusive ReadWrite/FileShare.None while displayed" $exclusiveWriteSuccess "True" "$exclusiveWriteSuccess"

# Test 7.2: Verify image file can be deleted on disk while displayed
$tempDelImg = Join-Path $empImgDir "challenger_delete_me.png"
$delBmp = New-Object System.Drawing.Bitmap(50, 50)
$delBmp.Save($tempDelImg, [System.Drawing.Imaging.ImageFormat]::Png)
$delBmp.Dispose()

$loadImgMethod.Invoke($null, [object[]]@($pb, "Images\Employees\challenger_delete_me.png"))
$deleteOnDiskSuccess = $false
try {
    [System.IO.File]::Delete($tempDelImg)
    $deleteOnDiskSuccess = (-not (Test-Path $tempDelImg))
} catch {
    $deleteOnDiskSuccess = $false
}
Assert-Check "ConcurrencyLocks" "File deleted on disk while actively displayed in PictureBox (Zero lock)" $deleteOnDiskSuccess "True" "$deleteOnDiskSuccess"

# Test 7.3: Rapid 100-cycle stress test alternating among all permutations
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$cycleStressPassed = $true
$cycleExceptionCount = 0

$cycleInputs = @(
    "Images\Employees\challenger_valid1.png",
    "Images\Employees\challenger_valid2.png",
    $null,
    "",
    "   ",
    "Images\Employees\non_existent_stress.jpg",
    "..\..\..\..\Windows\win.ini",
    "Images\Employees\challenger_valid1.png"
)

try {
    for ($i = 0; $i -lt 100; $i++) {
        $inp = $cycleInputs[$i % $cycleInputs.Count]
        $loadImgMethod.Invoke($null, [object[]]@($pb, $inp))
    }
} catch {
    $cycleStressPassed = $false
    $cycleExceptionCount++
}
$sw.Stop()

Assert-Check "ConcurrencyLocks" "100-cycle rapid reload stress finished cleanly in $($sw.ElapsedMilliseconds)ms" ($cycleStressPassed -and $cycleExceptionCount -eq 0) "100 cycles, 0 exceptions" "Exceptions=$cycleExceptionCount, Elapsed=$($sw.ElapsedMilliseconds)ms"

# ==============================================================================
# SECTION 8: USERCONTROL & EMPLOYEESERVICE ADVERSARIAL INTEGRATION
# ==============================================================================
Write-Host "`n--- [Section 8] UserControl & EmployeeService Adversarial Integration ---" -ForegroundColor Cyan

# Test 8.1: EmployeeService SaveEmployee relative path transformation
$empService = [Activator]::CreateInstance([type]$empServiceType)
$testEmpDto = [Activator]::CreateInstance([type]$empDtoType)
$testEmpDto.FullName = "Challenger 1 Empirical Test"
$testEmpDto.Phone = "0911223344"
$testEmpDto.RoleId = 2
$testEmpDto.Address = "456 Stress Ave"

# Create a source file in external temp folder
$tempExtSource = [System.IO.Path]::Combine($env:TEMP, "external_avatar_$([System.Guid]::NewGuid().ToString('N')).png")
$extBmp = New-Object System.Drawing.Bitmap(80, 80)
$extBmp.Save($tempExtSource, [System.Drawing.Imaging.ImageFormat]::Png)
$extBmp.Dispose()

$saveMethodWithSource = $empServiceType.GetMethod("SaveEmployee", [type[]]@($empDtoType, [string], [string].MakeByRefType()))
$saveArgs = [object[]]@($testEmpDto, $tempExtSource, "")
$saveMethodWithSource.Invoke($empService, $saveArgs)

$isRelativeFormat = ($testEmpDto.Image -like "Images\Employees\emp_*")
$noDriveLetter = ($testEmpDto.Image -notmatch '^[A-Za-z]:')
$fileCreatedOnDisk = (Test-Path (Join-Path $ProjectRoot "bin\Debug\$($testEmpDto.Image)")) -or (Test-Path (Join-Path $ProjectRoot $testEmpDto.Image))

Assert-Check "EmployeeService" "SaveEmployee converts external image to relative path" ($isRelativeFormat -and $noDriveLetter) "Images\Employees\emp_*" "$($testEmpDto.Image)"
Assert-Check "EmployeeService" "Copied avatar file exists on disk" $fileCreatedOnDisk "True" "$fileCreatedOnDisk"

# Clean up external temp file
if (Test-Path $tempExtSource) { Remove-Item $tempExtSource -Force }
if ($fileCreatedOnDisk) {
    $f1 = Join-Path $ProjectRoot "bin\Debug\$($testEmpDto.Image)"
    $f2 = Join-Path $ProjectRoot $testEmpDto.Image
    if (Test-Path $f1) { Remove-Item $f1 -Force }
    if (Test-Path $f2) { Remove-Item $f2 -Force }
}

# Test 8.2: UCEmployeeInform robust binding across adversarial inputs
$informControl = [Activator]::CreateInstance([type]$ucInformType)
$informInputs = @(
    @{ Name = "Null employee DTO"; DTO = $null },
    @{ Name = "Empty image path"; DTO = [PSCustomObject]@{ Id = 1; FullName = "A"; Phone = "1"; RoleId = 1; Image = "" } },
    @{ Name = "Non-existent image"; DTO = [PSCustomObject]@{ Id = 2; FullName = "B"; Phone = "2"; RoleId = 1; Image = "Images\Employees\missing.png" } },
    @{ Name = "Valid relative image"; DTO = [PSCustomObject]@{ Id = 3; FullName = "C"; Phone = "3"; RoleId = 1; Image = "Images\Employees\challenger_valid1.png" } }
)

$informAllSafe = $true
foreach ($inf in $informInputs) {
    try {
        if ($inf.DTO -eq $null) {
            $informControl.GetType().GetMethod("BindData").Invoke($informControl, @($null))
        } else {
            $d = [Activator]::CreateInstance([type]$empDtoType)
            $d.Id = $inf.DTO.Id
            $d.FullName = $inf.DTO.FullName
            $d.Phone = $inf.DTO.Phone
            $d.RoleId = $inf.DTO.RoleId
            $d.Image = $inf.DTO.Image
            $informControl.GetType().GetMethod("BindData").Invoke($informControl, @($d))
        }
    } catch {
        $informAllSafe = $false
    }
}
Assert-Check "UserControls" "UCEmployeeInform survives all adversarial BindData inputs" $informAllSafe "0 exceptions" "Safe=$informAllSafe"
$informControl.Dispose()

# Test 8.3: UCEmployeeEdit robust data binding across adversarial inputs
$editControl = [Activator]::CreateInstance([type]$ucEditType)
$editAllSafe = $true
foreach ($inf in $informInputs) {
    try {
        if ($inf.DTO -eq $null) {
            $editControl.GetType().GetMethod("SetEmployeeData").Invoke($editControl, @($null))
        } else {
            $d = [Activator]::CreateInstance([type]$empDtoType)
            $d.Id = $inf.DTO.Id
            $d.FullName = $inf.DTO.FullName
            $d.Phone = $inf.DTO.Phone
            $d.RoleId = $inf.DTO.RoleId
            $d.Image = $inf.DTO.Image
            $editControl.GetType().GetMethod("SetEmployeeData").Invoke($editControl, @($d))
        }
    } catch {
        $editAllSafe = $false
    }
}
Assert-Check "UserControls" "UCEmployeeEdit survives all adversarial SetEmployeeData inputs" $editAllSafe "0 exceptions" "Safe=$editAllSafe"
$editControl.Dispose()

# Clean up temporary test valid images
$pb.Dispose()
if (Test-Path $testImg1Path) { Remove-Item $testImg1Path -Force }
if (Test-Path $testImg2Path) { Remove-Item $testImg2Path -Force }
$rootImg1 = Join-Path $rootEmpImgDir "challenger_valid1.png"
if (Test-Path $rootImg1) { Remove-Item $rootImg1 -Force }

# ==============================================================================
# SUMMARY & PERSISTENCE
# ==============================================================================
$totalTests = $testResults.Count
$passedTests = @($testResults | Where-Object { $_.Passed }).Count
$failedTests = $totalTests - $passedTests

Write-Host "`n==========================================================================" -ForegroundColor Cyan
Write-Host "         CHALLENGER 1 EMPIRICAL RESULTS SUMMARY: MILESTONE 3              " -ForegroundColor Yellow
Write-Host "==========================================================================" -ForegroundColor Cyan
Write-Host "Total Tests Run : $totalTests" -ForegroundColor White
Write-Host "Passed          : $passedTests" -ForegroundColor Green
Write-Host "Failed          : $failedTests" -ForegroundColor $(if ($failedTests -eq 0) { "Green" } else { "Red" })
Write-Host "Pass Rate       : $([Math]::Round(($passedTests / $totalTests) * 100, 1))%" -ForegroundColor $(if ($failedTests -eq 0) { "Green" } else { "Red" })
Write-Host "==========================================================================" -ForegroundColor Cyan

# Persist to Challenger_M3_Results.json
$resultsPath = Join-Path $ProjectRoot "tests\Challenger_M3_Results.json"
$testResults | ConvertTo-Json -Depth 5 | Set-Content $resultsPath -Encoding UTF8
Write-Host "Results written to: $resultsPath" -ForegroundColor DarkGray

if ($failedTests -gt 0) {
    exit 1
} else {
    exit 0
}

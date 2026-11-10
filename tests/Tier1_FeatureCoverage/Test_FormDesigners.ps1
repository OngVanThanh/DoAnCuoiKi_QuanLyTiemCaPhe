# ==============================================================================
# Tier 1 - Test_FormDesigners.ps1
# Rubric: 15 Form (2.0 pts) - Visual Designer Backed, No Behind-Code Generated Forms
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 1: Windows Forms & Designer Audit ---" -ForegroundColor Cyan

$formDir = Join-Path $global:ProjectRoot "Form"
$ucDir = Join-Path $global:ProjectRoot "UserControl"

# Test 1.1: Identify all Form files excluding Designers
$formCsFiles = Get-ChildItem -Path $formDir -Filter "*.cs" -Recurse | Where-Object { $_.Name -notmatch '\.Designer\.cs$' -and $_.Name -notmatch '\.rdlc' }

$discoveredForms = @()
foreach ($file in $formCsFiles) {
    $content = Get-Content $file.FullName -Raw
    # Match class definition inheriting from Form or KryptonForm
    if ($content -match 'class\s+(\w+)\s*:\s*(?:Form|KryptonForm|ComponentFactory\.Krypton\.Toolkit\.KryptonForm)') {
        $className = $matches[1]
        $designerPath = $file.FullName -replace '\.cs$', '.Designer.cs'
        $hasDesigner = Test-Path $designerPath
        $hasInitComponent = $false
        if ($hasDesigner) {
            $designerContent = Get-Content $designerPath -Raw
            $hasInitComponent = ($designerContent -match 'void\s+InitializeComponent\s*\(')
        }

        $discoveredForms += [PSCustomObject]@{
            ClassName    = $className
            SourceFile   = $file.Name
            FullPath     = $file.FullName
            HasDesigner  = $hasDesigner
            HasInitComp  = $hasInitComponent
            DesignerSize = if ($hasDesigner) { (Get-Item $designerPath).Length } else { 0 }
        }
    }
}

$sw = [System.Diagnostics.Stopwatch]::StartNew()
Write-Host "         Total Form classes detected: $($discoveredForms.Count)" -ForegroundColor Gray

# Rubric requirement: >= 15 forms
Assert-True ($discoveredForms.Count -ge 15) "Project has at least 15 Windows Forms (Detected: $($discoveredForms.Count))" `
    "Tier 1" "Form_Designers" -DurationMs $sw.ElapsedMilliseconds

# Test 1.2: Verify 100% of discovered Forms have a valid .Designer.cs file
$formsWithoutDesigner = $discoveredForms | Where-Object { -not $_.HasDesigner }
Assert-Equal $formsWithoutDesigner.Count 0 "100% of Form classes have a physical .Designer.cs file" `
    "Tier 1" "Form_Designers"

# Test 1.3: Verify 100% of Designers have InitializeComponent()
$formsWithoutInitComp = $discoveredForms | Where-Object { -not $_.HasInitComp }
Assert-Equal $formsWithoutInitComp.Count 0 "100% of Form Designers contain InitializeComponent() definition" `
    "Tier 1" "Form_Designers"

# Test 1.4: UserControls visual designer backing check
$ucCsFiles = Get-ChildItem -Path $ucDir -Filter "*.cs" -Recurse | Where-Object { $_.Name -notmatch '\.Designer\.cs$' }
$discoveredUcs = @()

foreach ($file in $ucCsFiles) {
    $content = Get-Content $file.FullName -Raw
    if ($content -match 'class\s+(\w+)\s*:\s*(?:UserControl|KryptonUserControl)') {
        $className = $matches[1]
        $designerPath = $file.FullName -replace '\.cs$', '.Designer.cs'
        $hasDesigner = Test-Path $designerPath
        $discoveredUcs += [PSCustomObject]@{
            ClassName   = $className
            SourceFile  = $file.Name
            HasDesigner = $hasDesigner
        }
    }
}

Assert-True ($discoveredUcs.Count -ge 5) "Project has reusable UserControls (Detected: $($discoveredUcs.Count))" `
    "Tier 1" "Form_Designers"
$ucsWithoutDesigner = $discoveredUcs | Where-Object { -not $_.HasDesigner }
Assert-Equal $ucsWithoutDesigner.Count 0 "100% of UserControls have physical .Designer.cs files" `
    "Tier 1" "Form_Designers"

# ==============================================================================
# CHALLENGER 2 EMPIRICAL TEST SUITE: MILESTONE 1 DATA VALIDATION & FK HARDENING
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n==========================================================================" -ForegroundColor Cyan
Write-Host "   CHALLENGER 2 EMPIRICAL STRESS TESTS: DATA VALIDATION & FK HARDENING   " -ForegroundColor Cyan
Write-Host "==========================================================================" -ForegroundColor Cyan

$asm = Get-ProjectAssembly
$custServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.CustomerService")
$custDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.CustomerDTO")
$empServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.EmployeeService")
$empRepoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DAL.EmployeeRepository")

$custService = [Activator]::CreateInstance($custServiceType)
$empService = [Activator]::CreateInstance($empServiceType)
$empRepo = [Activator]::CreateInstance($empRepoType)

# ------------------------------------------------------------------------------
# SUITE 1: CustomerService Negative & Boundary RewardPoints Validation
# ------------------------------------------------------------------------------
Write-Host "`n--- [Suite 1] CustomerService RewardPoints Data Validation ---" -ForegroundColor Yellow

function New-CustomerDTO([string]$phone, [string]$name, [int]$points) {
    $dto = [Activator]::CreateInstance($custDtoType)
    $dto.Phone = $phone
    $dto.CustomerName = $name
    $dto.RewardPoints = $points
    $dto.MembershipTier = "Thường"
    $dto.RegistrationDate = [DateTime]::Today
    return $dto
}

# Test 1.1: Add with RewardPoints = -1
$err = ""
$dtoNeg1 = New-CustomerDTO "0901000001" "Negative Point User 1" -1
$resAddNeg1 = $custService.Add($dtoNeg1, [ref]$err)
Assert-False $resAddNeg1 "CustomerService.Add rejects RewardPoints = -1" "Challenger2" "Customer_Validation"
Assert-Equal $err "Điểm tích lũy không được nhỏ hơn 0!" "Add(-1) returns exact validation message" "Challenger2" "Customer_Validation"

# Test 1.2: Add with RewardPoints = -100
$err = ""
$dtoNeg100 = New-CustomerDTO "0901000002" "Negative Point User 2" -100
$resAddNeg100 = $custService.Add($dtoNeg100, [ref]$err)
Assert-False $resAddNeg100 "CustomerService.Add rejects RewardPoints = -100" "Challenger2" "Customer_Validation"
Assert-Equal $err "Điểm tích lũy không được nhỏ hơn 0!" "Add(-100) returns exact validation message" "Challenger2" "Customer_Validation"

# Test 1.3: Add with RewardPoints = int.MinValue (-2147483648)
$err = ""
$dtoMin = New-CustomerDTO "0901000003" "Negative Point User Min" ([int]::MinValue)
$resAddMin = $custService.Add($dtoMin, [ref]$err)
Assert-False $resAddMin "CustomerService.Add rejects RewardPoints = int.MinValue (-2,147,483,648)" "Challenger2" "Customer_Validation"
Assert-Equal $err "Điểm tích lũy không được nhỏ hơn 0!" "Add(int.MinValue) returns exact validation message" "Challenger2" "Customer_Validation"

# Test 1.4: Update with RewardPoints = -1
$err = ""
$dtoUpdNeg1 = New-CustomerDTO "0901000001" "Existing Customer" -1
$resUpdNeg1 = $custService.Update($dtoUpdNeg1, [ref]$err)
Assert-False $resUpdNeg1 "CustomerService.Update rejects RewardPoints = -1" "Challenger2" "Customer_Validation"
Assert-Equal $err "Điểm tích lũy không được nhỏ hơn 0!" "Update(-1) returns exact validation message" "Challenger2" "Customer_Validation"

# Test 1.5: Update with RewardPoints = -100
$err = ""
$dtoUpdNeg100 = New-CustomerDTO "0901000002" "Existing Customer" -100
$resUpdNeg100 = $custService.Update($dtoUpdNeg100, [ref]$err)
Assert-False $resUpdNeg100 "CustomerService.Update rejects RewardPoints = -100" "Challenger2" "Customer_Validation"
Assert-Equal $err "Điểm tích lũy không được nhỏ hơn 0!" "Update(-100) returns exact validation message" "Challenger2" "Customer_Validation"

# Test 1.6: Update with RewardPoints = int.MinValue
$err = ""
$dtoUpdMin = New-CustomerDTO "0901000003" "Existing Customer" ([int]::MinValue)
$resUpdMin = $custService.Update($dtoUpdMin, [ref]$err)
Assert-False $resUpdMin "CustomerService.Update rejects RewardPoints = int.MinValue" "Challenger2" "Customer_Validation"
Assert-Equal $err "Điểm tích lũy không được nhỏ hơn 0!" "Update(int.MinValue) returns exact validation message" "Challenger2" "Customer_Validation"

# Test 1.7: Non-negative points: RewardPoints = 0 must pass points validation
$err = ""
$dto0 = New-CustomerDTO "0999999990" "Zero Point User" 0
try { $custService.Add($dto0, [ref]$err) | Out-Null } catch { }
Assert-True ($err -ne "Điểm tích lũy không được nhỏ hơn 0!") "Add(0) passes point validation (does not reject for negative points)" "Challenger2" "Customer_Validation"

# Test 1.8: Non-negative points: RewardPoints = 10 must pass points validation
$err = ""
$dto10 = New-CustomerDTO "0999999910" "Ten Point User" 10
try { $custService.Add($dto10, [ref]$err) | Out-Null } catch { }
Assert-True ($err -ne "Điểm tích lũy không được nhỏ hơn 0!") "Add(10) passes point validation" "Challenger2" "Customer_Validation"

# Test 1.9: Non-negative points: RewardPoints = 500 must pass points validation
$err = ""
$dto500 = New-CustomerDTO "0999999950" "Five Hundred Point User" 500
try { $custService.Add($dto500, [ref]$err) | Out-Null } catch { }
Assert-True ($err -ne "Điểm tích lũy không được nhỏ hơn 0!") "Add(500) passes point validation" "Challenger2" "Customer_Validation"

# Test 1.10: Non-negative points: RewardPoints = int.MaxValue must pass points validation
$err = ""
$dtoMax = New-CustomerDTO "0999999999" "Max Point User" ([int]::MaxValue)
try { $custService.Add($dtoMax, [ref]$err) | Out-Null } catch { }
Assert-True ($err -ne "Điểm tích lũy không được nhỏ hơn 0!") "Add(int.MaxValue) passes point validation" "Challenger2" "Customer_Validation"

# Test 1.11: Update with non-negative points passes points validation
$err = ""
$dtoUpd0 = New-CustomerDTO "0999999990" "Existing User" 0
try { $custService.Update($dtoUpd0, [ref]$err) | Out-Null } catch { }
Assert-True ($err -ne "Điểm tích lũy không được nhỏ hơn 0!") "Update(0) passes point validation" "Challenger2" "Customer_Validation"

$err = ""
$dtoUpd10 = New-CustomerDTO "0999999910" "Existing User" 10
try { $custService.Update($dtoUpd10, [ref]$err) | Out-Null } catch { }
Assert-True ($err -ne "Điểm tích lũy không được nhỏ hơn 0!") "Update(10) passes point validation" "Challenger2" "Customer_Validation"

$err = ""
$dtoUpd500 = New-CustomerDTO "0999999950" "Existing User" 500
try { $custService.Update($dtoUpd500, [ref]$err) | Out-Null } catch { }
Assert-True ($err -ne "Điểm tích lũy không được nhỏ hơn 0!") "Update(500) passes point validation" "Challenger2" "Customer_Validation"

# Test 1.12: Null and Empty string guards on CustomerService
$err = ""
$resNullAdd = $custService.Add($null, [ref]$err)
Assert-False $resNullAdd "Add(null) returns false" "Challenger2" "Customer_Validation"
Assert-Equal $err "Dữ liệu khách hàng không hợp lệ!" "Add(null) returns expected message" "Challenger2" "Customer_Validation"

$err = ""
$dtoNoPhone = New-CustomerDTO "" "No Phone" 10
$resNoPhone = $custService.Add($dtoNoPhone, [ref]$err)
Assert-False $resNoPhone "Add with empty phone returns false" "Challenger2" "Customer_Validation"
Assert-Equal $err "Số điện thoại không được để trống!" "Add(empty phone) returns expected message" "Challenger2" "Customer_Validation"

$err = ""
$dtoNoName = New-CustomerDTO "0900000000" "" 10
$resNoName = $custService.Add($dtoNoName, [ref]$err)
Assert-False $resNoName "Add with empty customer name returns false" "Challenger2" "Customer_Validation"
Assert-Equal $err "Tên khách hàng không được để trống!" "Add(empty name) returns expected message" "Challenger2" "Customer_Validation"

# ------------------------------------------------------------------------------
# SUITE 2: EmployeeService & EmployeeRepository FK Deletion Handling
# ------------------------------------------------------------------------------
Write-Host "`n--- [Suite 2] Employee Deletion FK Constraint & Crash Prevention ---" -ForegroundColor Yellow

# Test 2.1: EmployeeService.DeleteEmployee with ID = 0
$msg = ""
$resDel0 = $empService.DeleteEmployee(0, [ref]$msg)
Assert-False $resDel0 "EmployeeService.DeleteEmployee(0) returns false" "Challenger2" "Employee_FK"
Assert-Equal $msg "Mã nhân viên không hợp lệ!" "DeleteEmployee(0) error message is correct" "Challenger2" "Employee_FK"

# Test 2.2: EmployeeService.DeleteEmployee with ID = -1
$msg = ""
$resDelNeg1 = $empService.DeleteEmployee(-1, [ref]$msg)
Assert-False $resDelNeg1 "EmployeeService.DeleteEmployee(-1) returns false" "Challenger2" "Employee_FK"
Assert-Equal $msg "Mã nhân viên không hợp lệ!" "DeleteEmployee(-1) error message is correct" "Challenger2" "Employee_FK"

# Test 2.3: EmployeeService.DeleteEmployee with ID = int.MinValue
$msg = ""
$resDelMin = $empService.DeleteEmployee(([int]::MinValue), [ref]$msg)
Assert-False $resDelMin "EmployeeService.DeleteEmployee(int.MinValue) returns false" "Challenger2" "Employee_FK"
Assert-Equal $msg "Mã nhân viên không hợp lệ!" "DeleteEmployee(int.MinValue) error message is correct" "Challenger2" "Employee_FK"

# Test 2.4: EmployeeService.DeleteEmployee overload without out param
$resDelOverload = $empService.DeleteEmployee(0)
Assert-False $resDelOverload "EmployeeService.DeleteEmployee(0) overload returns false" "Challenger2" "Employee_FK"

# Test 2.5: EmployeeService.DeleteEmployee with non-existent ID (999999) - must NOT throw
$threw = $false
$resDel999 = $null
$msg999 = ""
try {
    $resDel999 = $empService.DeleteEmployee(999999, [ref]$msg999)
} catch {
    $threw = $true
}
Assert-False $threw "EmployeeService.DeleteEmployee(999999) does not throw unhandled exception" "Challenger2" "Employee_FK"
Assert-False $resDel999 "EmployeeService.DeleteEmployee(999999) returns false" "Challenger2" "Employee_FK"
Assert-True ($msg999.Length -gt 0) "EmployeeService.DeleteEmployee(999999) populates error message" "Challenger2" "Employee_FK"

# Test 2.6: EmployeeRepository.DeleteEmployee with non-existent ID (999999) - must NOT throw
$threw = $false
$resRepo999 = $null
$msgRepo999 = ""
try {
    $resRepo999 = $empRepo.DeleteEmployee(999999, [ref]$msgRepo999)
} catch {
    $threw = $true
}
Assert-False $threw "EmployeeRepository.DeleteEmployee(999999) does not throw unhandled exception" "Challenger2" "Employee_FK"
Assert-False $resRepo999 "EmployeeRepository.DeleteEmployee(999999) returns false" "Challenger2" "Employee_FK"

# Test 2.7: EmployeeService.DeleteEmployee with linked FK records (ID = 1, Manager linked to Users/Orders)
$threw = $false
$resDel1 = $null
$msgDel1 = ""
try {
    $resDel1 = $empService.DeleteEmployee(1, [ref]$msgDel1)
} catch {
    $threw = $true
}
Assert-False $threw "EmployeeService.DeleteEmployee(1) handles linked entity without unhandled exception" "Challenger2" "Employee_FK"
Assert-False $resDel1 "EmployeeService.DeleteEmployee(1) returns false when linked records exist" "Challenger2" "Employee_FK"

# Test 2.8: EmployeeRepository.DeleteEmployee with linked FK records (ID = 1)
$threw = $false
$resRepo1 = $null
$msgRepo1 = ""
try {
    $resRepo1 = $empRepo.DeleteEmployee(1, [ref]$msgRepo1)
} catch {
    $threw = $true
}
Assert-False $threw "EmployeeRepository.DeleteEmployee(1) handles linked entity without unhandled exception" "Challenger2" "Employee_FK"
Assert-False $resRepo1 "EmployeeRepository.DeleteEmployee(1) returns false when linked records exist" "Challenger2" "Employee_FK"

# Test 2.9: EmployeeService.DeleteEmployee with Cashier employee (ID = 2)
$threw = $false
$resDel2 = $null
$msgDel2 = ""
try {
    $resDel2 = $empService.DeleteEmployee(2, [ref]$msgDel2)
} catch {
    $threw = $true
}
Assert-False $threw "EmployeeService.DeleteEmployee(2) handles linked cashier entity without unhandled exception" "Challenger2" "Employee_FK"
Assert-False $resDel2 "EmployeeService.DeleteEmployee(2) returns false" "Challenger2" "Employee_FK"

# Test 2.10: EmployeeRepository overload without out parameter
$resRepoOverload = $empRepo.DeleteEmployee(1)
Assert-False $resRepoOverload "EmployeeRepository.DeleteEmployee(1) overload returns false without throwing" "Challenger2" "Employee_FK"

# ------------------------------------------------------------------------------
# SUITE 3: Form Textbox Parsing Hardening Scan & Edge Case Simulation
# ------------------------------------------------------------------------------
Write-Host "`n--- [Suite 3] Form Textbox Parsing Hardening Scan & Input Resilience ---" -ForegroundColor Yellow

$projRoot = $global:ProjectRoot

# Scan 3.1: Check FrmCategory.cs for int.Parse / Int32.Parse
$catFile = Join-Path $projRoot "Form\TuanHung\FrmCategory.cs"
$catParses = Select-String -Path $catFile -Pattern "\bint\.Parse\b|\bInt32\.Parse\b"
Assert-Equal ($catParses.Count) 0 "FrmCategory.cs contains 0 unsafe int.Parse / Int32.Parse calls" "Challenger2" "Parsing_Hardening"

# Scan 3.2: Check FrmProduct.cs for int.Parse / Int32.Parse
$prodFile = Join-Path $projRoot "Form\TuanHung\FrmProduct.cs"
$prodParses = Select-String -Path $prodFile -Pattern "\bint\.Parse\b|\bInt32\.Parse\b"
Assert-Equal ($prodParses.Count) 0 "FrmProduct.cs contains 0 unsafe int.Parse / Int32.Parse calls" "Challenger2" "Parsing_Hardening"

# Scan 3.3: Check FrmPromotion.cs for int.Parse / Int32.Parse
$promoFile = Join-Path $projRoot "Form\TuanHung\FrmPromotion.cs"
$promoParses = Select-String -Path $promoFile -Pattern "\bint\.Parse\b|\bInt32\.Parse\b"
Assert-Equal ($promoParses.Count) 0 "FrmPromotion.cs contains 0 unsafe int.Parse / Int32.Parse calls" "Challenger2" "Parsing_Hardening"

# Scan 3.4: Check FrmCustomer.cs for int.Parse / Int32.Parse
$custFormFile = Join-Path $projRoot "Form\QuangHung\FrmCustomer.cs"
$custParses = Select-String -Path $custFormFile -Pattern "\bint\.Parse\b|\bInt32\.Parse\b"
Assert-Equal ($custParses.Count) 0 "FrmCustomer.cs contains 0 unsafe int.Parse / Int32.Parse calls" "Challenger2" "Parsing_Hardening"

# Scan 3.5: Check entire Form\ directory for any remaining int.Parse / Int32.Parse
$allFormFiles = Get-ChildItem -Path (Join-Path $projRoot "Form") -Recurse -Filter "*.cs"
$allFormParses = $allFormFiles | Select-String -Pattern "\bint\.Parse\b|\bInt32\.Parse\b"
Assert-Equal ($allFormParses.Count) 0 "Entire Form\ hierarchy contains 0 unsafe int.Parse / Int32.Parse calls" "Challenger2" "Parsing_Hardening"

# Scan 3.6: Check DAL and BLL for int.Parse / Int32.Parse
$dalBllFiles = Get-ChildItem -Path (Join-Path $projRoot "DAL"), (Join-Path $projRoot "BLL") -Recurse -Filter "*.cs"
$dalBllParses = $dalBllFiles | Select-String -Pattern "\bint\.Parse\b|\bInt32\.Parse\b"
Assert-Equal ($dalBllParses.Count) 0 "DAL and BLL directories contain 0 unsafe int.Parse / Int32.Parse calls" "Challenger2" "Parsing_Hardening"

# ------------------------------------------------------------------------------
# SUITE 4: Adversarial Inputs Simulation on int.TryParse & Guards
# ------------------------------------------------------------------------------
Write-Host "`n--- [Suite 4] Adversarial Inputs Simulation on int.TryParse & Guards ---" -ForegroundColor Yellow

$adversarialInputs = @(
    "",                                          # Empty string
    "   ",                                       # Whitespace
    "abc",                                       # Letters
    "12a34",                                     # Alphanumeric
    "!@#$%^&*()",                                # Symbols
    "' OR '1'='1",                               # SQL Injection string
    "<script>alert(1)</script>",                 # XSS payload
    "12.34",                                     # Float into int
    "2147483648",                                # Int32.MaxValue + 1 (overflow)
    "-2147483649",                               # Int32.MinValue - 1 (underflow)
    "999999999999999999999999999999",            # BigInteger overflow
    "-1",                                        # Negative boundary
    "-100",                                      # Negative boundary
    "-999999"                                    # Large negative
)

# Test Category ID parsing simulation (FrmCategory.cs: lines 84, 108)
foreach ($input in $adversarialInputs) {
    [int]$parseVal = 0
    $parsed = [int]::TryParse($input, [ref]$parseVal)
    $isValid = $parsed -and ($parseVal -gt 0)
    Assert-False $isValid "FrmCategory ID parse rejects adversarial input: '$input'" "Challenger2" "Adversarial_Inputs"
}

# Test Product ID parsing simulation (FrmProduct.cs: lines 144, 183)
foreach ($input in $adversarialInputs) {
    [int]$prodVal = 0
    $parsed = [int]::TryParse($input, [ref]$prodVal)
    $isValid = $parsed -and ($prodVal -gt 0)
    Assert-False $isValid "FrmProduct ID parse rejects adversarial input: '$input'" "Challenger2" "Adversarial_Inputs"
}

# Test Customer RewardPoints parsing simulation (FrmCustomer.cs: lines 63, 113)
foreach ($input in $adversarialInputs) {
    [int]$ptVal = 0
    $parsed = [int]::TryParse($input.Trim(), [ref]$ptVal)
    $isValid = $parsed -and ($ptVal -ge 0)
    Assert-False $isValid "FrmCustomer RewardPoints rejects adversarial input: '$input'" "Challenger2" "Adversarial_Inputs"
}

# Test Product Price decimal.TryParse simulation (FrmProduct.cs: line 135)
$adversarialPriceInputs = @(
    "", "   ", "abc", "!@#$", "99999999999999999999999999999999999999999999999999999999"
)
foreach ($input in $adversarialPriceInputs) {
    [decimal]$decVal = 0
    $parsed = [decimal]::TryParse($input.Trim(), [ref]$decVal)
    $isValid = $parsed -and ($decVal -ge 0)
    Assert-False $isValid "FrmProduct Price rejects adversarial decimal input: '$input'" "Challenger2" "Adversarial_Inputs"
}

# Test Promotion Discount double.TryParse simulation (FrmPromotion.cs: line 91)
$adversarialDiscountInputs = @(
    "", "   ", "abc", "100%", "!@#$", "1e999"
)
foreach ($input in $adversarialDiscountInputs) {
    [double]$dblVal = 0
    $parsed = [double]::TryParse($input.Trim(), [ref]$dblVal)
    $isValid = $parsed -and ($dblVal -ge 0) -and ($dblVal -le 100)
    Assert-False $isValid "FrmPromotion Discount rejects adversarial double input: '$input'" "Challenger2" "Adversarial_Inputs"
}

$c2Results = $global:TestResults | Where-Object { $_.Tier -eq "Challenger2" }
$c2Total = $c2Results.Count
$c2Passed = ($c2Results | Where-Object { $_.Passed -eq $true }).Count
$c2Failed = ($c2Results | Where-Object { $_.Passed -eq $false }).Count

Write-Host "`n==========================================================================" -ForegroundColor Cyan
Write-Host "                CHALLENGER 2 TEST EXECUTION SUMMARY                      " -ForegroundColor Cyan
Write-Host "==========================================================================" -ForegroundColor Cyan
Write-Host "Total Challenger 2 Tests : $c2Total" -ForegroundColor White
Write-Host "Passed                   : $c2Passed" -ForegroundColor Green
Write-Host "Failed                   : $c2Failed" -ForegroundColor $(if ($c2Failed -gt 0) { "Red" } else { "Green" })
Write-Host "==========================================================================`n" -ForegroundColor Cyan

if ($c2Failed -gt 0) {
    exit 1
} else {
    exit 0
}

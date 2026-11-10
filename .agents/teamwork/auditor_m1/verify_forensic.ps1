$targetAssembly = 'd:\DoAnCuoiKi_QuanLyTiemCaPhe\bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe'
$asm = [System.Reflection.Assembly]::LoadFrom($targetAssembly)

Write-Host '=== FORENSIC TEST 1: SecurityHelper & PBKDF2 ===' -ForegroundColor Cyan
$secHelper = $asm.GetType('DoAnCuoiKi_QuanLyTiemCaPhe.SecurityHelper', $true)
$verifyMethod = $secHelper.GetMethod('VerifyPassword', [System.Reflection.BindingFlags]'Public,Static')

$adminSalt = 'MvzYIcst8H+HCiQecovlBA=='
$adminHash = '4YUma6Q69CiQ97YVG17kFFFVD1HGqb2exAjnTpdQe0g='
$resAdminCorrect = $verifyMethod.Invoke($null, @('admin123', $adminHash, $adminSalt))
$resAdminWrong = $verifyMethod.Invoke($null, @('wrongpass', $adminHash, $adminSalt))

$tnSalt = 'RAq3VsoVEEY92GOgn+/wCA=='
$tnHash = 'ExGmL2DJ+2fEVbrgGAtbpkBl0c2eNsKncAH/rmQawuY='
$resTnCorrect = $verifyMethod.Invoke($null, @('123456', $tnHash, $tnSalt))
$resTnWrong = $verifyMethod.Invoke($null, @('wrongpass', $tnHash, $tnSalt))

Write-Host ('Admin correct pwd verify: ' + $resAdminCorrect)
Write-Host ('Admin wrong pwd verify: ' + $resAdminWrong)
Write-Host ('Thungan correct pwd verify: ' + $resTnCorrect)
Write-Host ('Thungan wrong pwd verify: ' + $resTnWrong)

if ($resAdminCorrect -ne $true -or $resAdminWrong -ne $false -or $resTnCorrect -ne $true -or $resTnWrong -ne $false) {
    throw 'SecurityHelper PBKDF2 test failed!'
}

Write-Host '=== FORENSIC TEST 2: CustomerService RewardPoints validation ===' -ForegroundColor Cyan
$custServiceType = $asm.GetType('DoAnCuoiKi_QuanLyTiemCaPhe.BLL.CustomerService', $true)
$custDtoType = $asm.GetType('DoAnCuoiKi_QuanLyTiemCaPhe.DTO.CustomerDTO', $true)
$custService = [Activator]::CreateInstance($custServiceType)

$custObj = [Activator]::CreateInstance($custDtoType)
$custObj.Phone = '0988776655'
$custObj.CustomerName = 'Forensic Test'
$custObj.RewardPoints = -50

$addMethod = $custServiceType.GetMethod('Add', [System.Reflection.BindingFlags]'Public,Instance', $null, @($custDtoType, [string].MakeByRefType()), $null)
$argsArr = [object[]]@($custObj, [string]'')
$addResult = $addMethod.Invoke($custService, $argsArr)
$errMsg = $argsArr[1]

Write-Host ('AddCustomer with -50 points result: ' + $addResult + ', ErrorMsg: ' + $errMsg)
if ($addResult -ne $false -or $errMsg -notlike '*không được nhỏ hơn 0*') {
    throw 'CustomerService RewardPoints check failed!'
}

Write-Host '=== FORENSIC TEST 3: PermissionService self-deletion & admin reset validation ===' -ForegroundColor Cyan
$permServiceType = $asm.GetType('DoAnCuoiKi_QuanLyTiemCaPhe.PermissionService', $true)
$userSessionType = $asm.GetType('DoAnCuoiKi_QuanLyTiemCaPhe.UserSession', $true)
$userDtoType = $asm.GetType('DoAnCuoiKi_QuanLyTiemCaPhe.DTO.UserDTO', $true)

$currentUser = [Activator]::CreateInstance($userDtoType)
$currentUser.Username = 'admin'
$currentUser.RoleId = 1
$userSessionType.GetProperty('CurrentUser', [System.Reflection.BindingFlags]'Public,Static').SetValue($null, $currentUser)

$permService = [Activator]::CreateInstance($permServiceType)
$delMethod = $permServiceType.GetMethod('DeleteUser', [System.Reflection.BindingFlags]'Public,Instance', $null, @([string], [string].MakeByRefType()), $null)

$delArgs = [object[]]@('admin', [string]'')
$delResult = $delMethod.Invoke($permService, $delArgs)
$delMsg = $delArgs[1]

Write-Host ('Self-delete current user result: ' + $delResult + ', Message: ' + $delMsg)
if ($delResult -ne $false -or $delMsg -notlike '*Không thể tự xóa*') {
    throw 'Self-deletion guard failed!'
}

$resetMethod = $permServiceType.GetMethod('AdminResetPassword', [System.Reflection.BindingFlags]'Public,Instance')
$resetArgsShort = [object[]]@('thungan', '123', [string]'')
$resetResultShort = $resetMethod.Invoke($permService, $resetArgsShort)
$resetMsgShort = $resetArgsShort[2]

Write-Host ('AdminResetPassword short pass result: ' + $resetResultShort + ', Message: ' + $resetMsgShort)
if ($resetResultShort -ne $false -or $resetMsgShort -notlike '*tối thiểu 6 ký tự*') {
    throw 'AdminResetPassword short password validation failed!'
}

Write-Host '=== FORENSIC TEST 4: EmployeeService validation ===' -ForegroundColor Cyan
$empServiceType = $asm.GetType('DoAnCuoiKi_QuanLyTiemCaPhe.BLL.EmployeeService', $true)
$empService = [Activator]::CreateInstance($empServiceType)
$empDelMethod = $empServiceType.GetMethod('DeleteEmployee', [System.Reflection.BindingFlags]'Public,Instance', $null, @([int], [string].MakeByRefType()), $null)

$empArgs = [object[]]@(-1, [string]'')
$empResult = $empDelMethod.Invoke($empService, $empArgs)
$empMsg = $empArgs[1]

Write-Host ('DeleteEmployee with id -1 result: ' + $empResult + ', Message: ' + $empMsg)
if ($empResult -ne $false -or $empMsg -notlike '*không hợp lệ*') {
    throw 'EmployeeService id validation failed!'
}

Write-Host '=== ALL FORENSIC CHECKS PASSED EMPIRICALLY ===' -ForegroundColor Green

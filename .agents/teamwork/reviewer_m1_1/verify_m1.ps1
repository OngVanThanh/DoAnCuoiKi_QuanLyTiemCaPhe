$binPath = "d:\DoAnCuoiKi_QuanLyTiemCaPhe\bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe"
$asm = [System.Reflection.Assembly]::LoadFrom($binPath)

Write-Host "--- TEST 1: REFLECTION METHOD EXISTENCE ---"
$permServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.PermissionService")
$types = [Type[]]@([string], [string].MakeByRefType())
$mDelUser = $permServiceType.GetMethod("DeleteUser", $types)
Write-Host "DeleteUser(string, out string): " ($mDelUser -ne $null)

$resetTypes = [Type[]]@([string], [string], [string].MakeByRefType())
$mReset = $permServiceType.GetMethod("AdminResetPassword", $resetTypes)
Write-Host "AdminResetPassword(string, string, out string): " ($mReset -ne $null)

$empServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.EmployeeService")
$empTypes = [Type[]]@([int], [string].MakeByRefType())
$mEmp = $empServiceType.GetMethod("DeleteEmployee", $empTypes)
Write-Host "EmployeeService.DeleteEmployee(int, out string): " ($mEmp -ne $null)

$custServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.CustomerService")
$custDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.CustomerDTO")
$custTypes = [Type[]]@($custDtoType, [string].MakeByRefType())
$mCustAdd = $custServiceType.GetMethod("Add", $custTypes)
Write-Host "CustomerService.Add(CustomerDTO, out string): " ($mCustAdd -ne $null)
$mCustUpd = $custServiceType.GetMethod("Update", $custTypes)
Write-Host "CustomerService.Update(CustomerDTO, out string): " ($mCustUpd -ne $null)

Write-Host "`n--- TEST 2: SELF DELETION GUARD ---"
$userSessionType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Utilities.UserSession")
$userType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.User")
$adminUser = [Activator]::CreateInstance($userType)
$adminUser.Id = 1
$adminUser.Username = "admin"
$adminUser.RoleId = 1
$userSessionType.GetMethod("Login").Invoke($null, @($adminUser))

$permService = [Activator]::CreateInstance($permServiceType)
$delParams = [object[]]@("admin", "")
$resDel = $mDelUser.Invoke($permService, $delParams)
Write-Host "Attempt self-delete 'admin': Success=" $resDel " Message=" $delParams[1]

$delParamsCase = [object[]]@("ADMIN", "")
$resDelCase = $mDelUser.Invoke($permService, $delParamsCase)
Write-Host "Attempt self-delete 'ADMIN': Success=" $resDelCase " Message=" $delParamsCase[1]

$delParamsSpace = [object[]]@("  admin  ", "")
$resDelSpace = $mDelUser.Invoke($permService, $delParamsSpace)
Write-Host "Attempt self-delete '  admin  ': Success=" $resDelSpace " Message=" $delParamsSpace[1]

Write-Host "`n--- TEST 3: ADMIN RESET PASSWORD ---"
# Test short password
$resetParamsShort = [object[]]@("thungan", "123", "")
$resResetShort = $mReset.Invoke($permService, $resetParamsShort)
Write-Host "Attempt reset with 3 chars: Success=" $resResetShort " Message=" $resetParamsShort[2]

# Test empty username
$resetParamsEmptyUser = [object[]]@("", "123456", "")
$resResetEmptyUser = $mReset.Invoke($permService, $resetParamsEmptyUser)
Write-Host "Attempt reset empty user: Success=" $resResetEmptyUser " Message=" $resetParamsEmptyUser[2]

Write-Host "`n--- TEST 4: CUSTOMER REWARD POINTS VALIDATION ---"
$custService = [Activator]::CreateInstance($custServiceType)
$custObj = [Activator]::CreateInstance($custDtoType)
$custObj.CustomerName = "Test Neg Points"
$custObj.Phone = "0999999999"
$custObj.RewardPoints = -15
$custAddParams = [object[]]@($custObj, "")
$resCustAdd = $mCustAdd.Invoke($custService, $custAddParams)
Write-Host "Customer.Add with -15 points: Success=" $resCustAdd " Message=" $custAddParams[1]

$custObjUpd = [Activator]::CreateInstance($custDtoType)
$custObjUpd.CustomerName = "Test Neg Points"
$custObjUpd.Phone = "0999999999"
$custObjUpd.RewardPoints = -1
$custUpdParams = [object[]]@($custObjUpd, "")
$resCustUpd = $mCustUpd.Invoke($custService, $custUpdParams)
Write-Host "Customer.Update with -1 points: Success=" $resCustUpd " Message=" $custUpdParams[1]

Write-Host "`n--- TEST 5: EMPLOYEE DELETION GUARD ---"
$empService = [Activator]::CreateInstance($empServiceType)
$delEmpParamsZero = [object[]]@(0, "")
$resDelEmpZero = $mEmp.Invoke($empService, $delEmpParamsZero)
Write-Host "DeleteEmployee ID=0: Success=" $resDelEmpZero " Message=" $delEmpParamsZero[1]

$delEmpParamsNeg = [object[]]@(-5, "")
$resDelEmpNeg = $mEmp.Invoke($empService, $delEmpParamsNeg)
Write-Host "DeleteEmployee ID=-5: Success=" $resDelEmpNeg " Message=" $delEmpParamsNeg[1]

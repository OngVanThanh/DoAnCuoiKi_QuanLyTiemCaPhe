$binDir = "d:\DoAnCuoiKi_QuanLyTiemCaPhe\bin\Debug"
$sqlClientPath = Join-Path $binDir "System.Data.SqlClient.dll"
if (Test-Path $sqlClientPath) {
    [System.Reflection.Assembly]::LoadFrom($sqlClientPath) | Out-Null
}

$binPath = Join-Path $binDir "DoAnCuoiKi_QuanLyTiemCaPhe.exe"
$asm = [System.Reflection.Assembly]::LoadFrom($binPath)

$permServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.PermissionService")
$userSessionType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Utilities.UserSession")
$userType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.User")

$adminUser = [Activator]::CreateInstance($userType)
$adminUser.Id = 1
$adminUser.Username = "admin"
$adminUser.RoleId = 1
$userSessionType.GetMethod("Login").Invoke($null, @($adminUser))

$permService = [Activator]::CreateInstance($permServiceType)
$types = [Type[]]@([string], [string].MakeByRefType())
$mDelUser = $permServiceType.GetMethod("DeleteUser", $types)

$delParams = [object[]]@("admin", "")
try {
    $resDel = $mDelUser.Invoke($permService, $delParams)
    Write-Host "Attempt self-delete 'admin': Success=" $resDel " Message=" $delParams[1]
} catch {
    Write-Host "Error: " $_.Exception.ToString()
    if ($_.Exception.InnerException) {
        Write-Host "InnerError: " $_.Exception.InnerException.ToString()
    }
}

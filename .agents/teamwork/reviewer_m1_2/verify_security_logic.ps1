$asm = [System.Reflection.Assembly]::LoadFrom("d:\DoAnCuoiKi_QuanLyTiemCaPhe\bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe")
$permServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.PermissionService")
$userSessionType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Utilities.UserSession")
$userType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.User")

$service = [Activator]::CreateInstance($permServiceType)

Write-Host "UserSession type resolved: $($userSessionType.FullName)"
Write-Host "User type resolved: $($userType.FullName)"

# Test Case 1: Login user 'admin'
$mockUser = [Activator]::CreateInstance($userType)
$mockUser.Username = "admin"
$mockUser.RoleId = 1

$loginMethod = $userSessionType.GetMethod("Login", [System.Reflection.BindingFlags]"Static,Public")
$loginMethod.Invoke($null, @($mockUser))

$currUser = $userSessionType.GetProperty("CurrentUser", [System.Reflection.BindingFlags]"Static,Public").GetValue($null)
Write-Host "Logged in user: $($currUser.Username)"

# Call DeleteUser with 'admin' (Exact match)
# Note: Since DeleteUser checks targetUser in DB first, let's see how it behaves
$pSelf = [object[]]@("admin", [string]::Empty)
try {
    $resSelf = $permServiceType.GetMethod("DeleteUser", [Type[]]@([string], [string].MakeByRefType())).Invoke($service, $pSelf)
    Write-Host "DeleteUser('admin') when logged in as 'admin': Result=$resSelf, Msg='$($pSelf[1])'"
} catch {
    Write-Host "Exception on DeleteUser('admin'): $($_.Exception.InnerException.Message)"
}

# Logout
$logoutMethod = $userSessionType.GetMethod("Logout", [System.Reflection.BindingFlags]"Static,Public")
$logoutMethod.Invoke($null, $null)
$currUserAfter = $userSessionType.GetProperty("CurrentUser", [System.Reflection.BindingFlags]"Static,Public").GetValue($null)
Write-Host "After logout CurrentUser is null: $($null -eq $currUserAfter)"

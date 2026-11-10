$asm = [System.Reflection.Assembly]::LoadFrom("d:\DoAnCuoiKi_QuanLyTiemCaPhe\bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe")
$type = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.SecurityHelper")
$method = $type.GetMethod("VerifyPassword", [System.Reflection.BindingFlags]"Static,NonPublic,Public")

$r1 = $method.Invoke($null, [object[]]@("admin123", "4YUma6Q69CiQ97YVG17kFFFVD1HGqb2exAjnTpdQe0g=", "MvzYIcst8H+HCiQecovlBA=="))
$r2 = $method.Invoke($null, [object[]]@("123456", "ExGmL2DJ+2fEVbrgGAtbpkBl0c2eNsKncAH/rmQawuY=", "RAq3VsoVEEY92GOgn+/wCA=="))
$r3 = $method.Invoke($null, [object[]]@("wrongpass", "4YUma6Q69CiQ97YVG17kFFFVD1HGqb2exAjnTpdQe0g=", "MvzYIcst8H+HCiQecovlBA=="))

Write-Host "Admin123 match: $r1"
Write-Host "Thungan 123456 match: $r2"
Write-Host "Wrong pass match: $r3"

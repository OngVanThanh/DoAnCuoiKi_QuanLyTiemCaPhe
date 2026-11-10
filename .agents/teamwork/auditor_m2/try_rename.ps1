$exePath = "d:\DoAnCuoiKi_QuanLyTiemCaPhe\bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe"
$bakPath = "d:\DoAnCuoiKi_QuanLyTiemCaPhe\bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe.tmp_rename"
try {
    Rename-Item -Path $exePath -NewName "DoAnCuoiKi_QuanLyTiemCaPhe.exe.tmp_rename" -ErrorAction Stop
    Write-Host "Renamed to tmp_rename successfully."
} catch {
    Write-Host "Rename failed: $($_.Exception.Message)"
}

$exePath = "d:\DoAnCuoiKi_QuanLyTiemCaPhe\bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe"
$bakPath = "d:\DoAnCuoiKi_QuanLyTiemCaPhe\bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe.bak"
try {
    Rename-Item -Path $exePath -NewName "DoAnCuoiKi_QuanLyTiemCaPhe.exe.bak" -ErrorAction Stop
    Write-Host "RENAME_SUCCESS"
    Rename-Item -Path $bakPath -NewName "DoAnCuoiKi_QuanLyTiemCaPhe.exe" -ErrorAction Stop
    Write-Host "RENAME_BACK_SUCCESS"
} catch {
    Write-Host "RENAME_FAILED: $($_.Exception.Message)"
}

$filePath = "d:\DoAnCuoiKi_QuanLyTiemCaPhe\bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe"
try {
    $file = [System.IO.File]::Open($filePath, [System.IO.FileMode]::Open, [System.IO.FileAccess]::ReadWrite, [System.IO.FileShare]::None)
    $file.Close()
    Write-Host "FILE_IS_FREE"
} catch {
    Write-Host "FILE_LOCKED: $($_.Exception.Message)"
}

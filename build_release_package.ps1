# ==========================================================
# SCRIPT ĐÓNG GÓI PHÁT HÀNH (RELEASE PACKAGING SCRIPT)
# Tự động Build Release và tạo file TheCoffeeHome_v1.0_Portable.zip
# ==========================================================

$ErrorActionPreference = "Stop"
$ProgressPreference = 'SilentlyContinue'
$projectDir = $PSScriptRoot
Set-Location $projectDir

Write-Host ">>> [1/5] Tìm kiếm trình biên dịch MSBuild..." -ForegroundColor Cyan
$msbuildPaths = @(
    "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"
)

$msbuild = $msbuildPaths | Where-Object { Test-Path $_ } | Select-Object -First 1
if (-not $msbuild) {
    Write-Error "Không tìm thấy MSBuild! Vui lòng cài đặt Visual Studio."
    exit 1
}

Write-Host ">>> [2/5] Biên dịch Solution ở chế độ Release (Tối ưu hiệu năng)..." -ForegroundColor Cyan
& $msbuild "DoAnCuoiKi_QuanLyTiemCaPhe.sln" /t:Rebuild /p:Configuration=Release /p:Platform="Any CPU" /v:m
if ($LASTEXITCODE -ne 0) {
    Write-Error "Biên dịch thất bại!"
    exit $LASTEXITCODE
}

Write-Host ">>> [3/5] Chuẩn bị thư mục đóng gói sạch (Clean Package Folder)..." -ForegroundColor Cyan
$releaseBin = Join-Path $projectDir "bin\Release"
$packageOutDir = Join-Path $projectDir "Release_Package"
$stagingDir = Join-Path $packageOutDir "TheCoffeeHome_Portable"

if (Test-Path $stagingDir) {
    Remove-Item $stagingDir -Recurse -Force
}
New-Item -ItemType Directory -Path $stagingDir -Force | Out-Null

# Copy toàn bộ file nhị phân từ bin\Release sang staging
Copy-Item "$releaseBin\*" -Destination $stagingDir -Recurse -Force

# Sao chép thư mục Images
if (Test-Path "$projectDir\Images") {
    Copy-Item "$projectDir\Images" -Destination $stagingDir -Recurse -Force
}

# Dọn dẹp các file rác phát triển (*.pdb, *.xml)
Get-ChildItem -Path $stagingDir -Include *.pdb, *.xml -Recurse | Remove-Item -Force -ErrorAction SilentlyContinue

Write-Host ">>> [4/5] Nén thành file Portable ZIP (TheCoffeeHome_v1.0_Portable.zip)..." -ForegroundColor Cyan
$zipFile = Join-Path $packageOutDir "TheCoffeeHome_v1.0_Portable.zip"
if (Test-Path $zipFile) {
    Remove-Item $zipFile -Force
}
Add-Type -AssemblyName System.IO.Compression.FileSystem
[System.IO.Compression.ZipFile]::CreateFromDirectory($stagingDir, $zipFile, [System.IO.Compression.CompressionLevel]::Optimal, $false)

Write-Host ">>> [5/5] Kiểm tra Inno Setup để tạo Setup.exe..." -ForegroundColor Cyan
$isccPaths = @(
    "C:\Program Files (x86)\Inno Setup 6\ISCC.exe",
    "C:\Program Files\Inno Setup 6\ISCC.exe"
)
$iscc = $isccPaths | Where-Object { Test-Path $_ } | Select-Object -First 1

if ($iscc) {
    Write-Host "Đang biên dịch file cài đặt Setup.exe với Inno Setup..." -ForegroundColor Green
    & $iscc (Join-Path $projectDir "installer_script.iss")
    Write-Host "Thành công: Release_Package\TheCoffeeHome_Setup_v1.0.exe đã sẵn sàng!" -ForegroundColor Green
} else {
    Write-Host "Lưu ý: Chưa cài Inno Setup (Để tạo Setup.exe, bạn có thể tải Inno Setup miễn phí hoặc dùng bản Portable ZIP vừa tạo)." -ForegroundColor Yellow
}

Write-Host ""
Write-Host "==========================================================" -ForegroundColor Green
Write-Host "  ĐÓNG GÓI HOÀN TẤT! ĐÃ SẴN SÀNG PUBLIC:" -ForegroundColor Green
Write-Host "  1. Bản Portable ZIP: $zipFile" -ForegroundColor Yellow
Write-Host "  (Giải nén ra bất kỳ máy nào là click chạy ngay, CSDL tự khởi tạo trong 1s)" -ForegroundColor Yellow
Write-Host "==========================================================" -ForegroundColor Green

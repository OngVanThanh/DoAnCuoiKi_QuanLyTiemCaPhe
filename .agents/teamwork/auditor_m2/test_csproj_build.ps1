$origCsproj = Get-Content "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj" -Raw
# Create a test copy without CoffeeShopDBDataSet.Designer.cs
$testCsproj = $origCsproj -replace '<Compile Include="CoffeeShopDBDataSet\.Designer\.cs">[\s\S]*?</Compile>', ''
$testCsprojPath = "d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\auditor_m2\TestBuild.csproj"
Set-Content -Path $testCsprojPath -Value $testCsproj

# Build the test csproj
$msbuild = "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe"
& $msbuild $testCsprojPath /t:Build /p:Configuration=Debug /v:minimal

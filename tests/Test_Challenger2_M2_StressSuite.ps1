# ==============================================================================
# CHALLENGER 2 EMPIRICAL TEST SUITE: MILESTONE 2 LINQ LAMBDA & LAYER STRESS
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n==========================================================================" -ForegroundColor Cyan
Write-Host "   CHALLENGER 2 EMPIRICAL STRESS TESTS: MILESTONE 2 LAYER & SYNC HARNESS  " -ForegroundColor Cyan
Write-Host "==========================================================================" -ForegroundColor Cyan

$asm = Get-ProjectAssembly

# Resolve Types
$tableServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.TableService")
$tableRepoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DAL.TableRepository")
$orderRepoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DAL.OrderRepository")
$appConfigServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.AppConfigService")
$configHelperType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Utilities.ConfigHelper")
$tableDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.TableDTO")
$orderDetailDtoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DTO.OrderDetailDTO")

Assert-NotNull $tableServiceType "TableService type exists in assembly" "Challenger2_M2" "Type_Resolution"
Assert-NotNull $tableRepoType "TableRepository type exists in assembly" "Challenger2_M2" "Type_Resolution"
Assert-NotNull $orderRepoType "OrderRepository type exists in assembly" "Challenger2_M2" "Type_Resolution"
Assert-NotNull $appConfigServiceType "AppConfigService type exists in assembly" "Challenger2_M2" "Type_Resolution"
Assert-NotNull $configHelperType "ConfigHelper type exists in assembly" "Challenger2_M2" "Type_Resolution"

$tableService = [Activator]::CreateInstance($tableServiceType)
$tableRepo = [Activator]::CreateInstance($tableRepoType)
$orderRepo = [Activator]::CreateInstance($orderRepoType)
$appConfigService = [Activator]::CreateInstance($appConfigServiceType)

# ==============================================================================
# SUITE 1: TableService & TableRepository DeleteAreaByName Stress Tests
# ==============================================================================
Write-Host "`n--- [Suite 1] TableService & TableRepository.DeleteAreaByName Stress Tests ---" -ForegroundColor Yellow

# Helper to invoke DeleteAreaByName with out string message via reflection
function Invoke-DeleteArea([object]$target, [string]$areaName) {
    $paramTypes = [Type[]]@([string], [Type]::GetType("System.String&"))
    $method = $target.GetType().GetMethod("DeleteAreaByName", $paramTypes)
    $argsArray = [object[]]@($areaName, [string]::Empty)
    $res = $method.Invoke($target, $argsArray)
    return [PSCustomObject]@{
        Success = [bool]$res
        Message = [string]$argsArray[1]
    }
}

# Test 1.1: Delete area with active tables via TableService (Area 1: 'Khu máy lạnh (Tầng 1)')
$res1_1 = Invoke-DeleteArea $tableService "Khu máy lạnh (Tầng 1)"
Assert-False $res1_1.Success "TableService rejects deleting area with active tables" "Challenger2_M2" "Area_Deletion_Guards"
Assert-True ($res1_1.Message -like "*đang có bàn hoạt động, không thể xóa!*") "TableService returns active tables error message" "Challenger2_M2" "Area_Deletion_Guards"

# Test 1.2: Delete area with active tables via TableRepository directly (Area 3: 'Phòng VIP (Tầng 2)')
$res1_2 = Invoke-DeleteArea $tableRepo "Phòng VIP (Tầng 2)"
Assert-False $res1_2.Success "TableRepository rejects deleting area with active tables" "Challenger2_M2" "Area_Deletion_Guards"
Assert-True ($res1_2.Message -like "*đang có bàn hoạt động, không thể xóa!*") "TableRepository returns active tables error message" "Challenger2_M2" "Area_Deletion_Guards"

# Test 1.3: Delete non-existent area via TableService
$res1_3 = Invoke-DeleteArea $tableService "Khu vực không hề tồn tại 12345"
Assert-False $res1_3.Success "TableService rejects non-existent area name" "Challenger2_M2" "Area_Deletion_Guards"
Assert-True ($res1_3.Message -like "*không tồn tại trong hệ thống!*") "TableService returns non-existent area message" "Challenger2_M2" "Area_Deletion_Guards"

# Test 1.4: Delete non-existent area via TableRepository directly
$res1_4 = Invoke-DeleteArea $tableRepo "NonExistentArea_DAL_Test"
Assert-False $res1_4.Success "TableRepository rejects non-existent area name" "Challenger2_M2" "Area_Deletion_Guards"
Assert-True ($res1_4.Message -like "*không tồn tại trong hệ thống!*") "TableRepository returns non-existent area message" "Challenger2_M2" "Area_Deletion_Guards"

# Test 1.5: Delete area with empty string via TableService
$res1_5 = Invoke-DeleteArea $tableService ""
Assert-False $res1_5.Success "TableService rejects empty string area name" "Challenger2_M2" "Area_Deletion_Guards"
Assert-Equal $res1_5.Message "Tên khu vực không hợp lệ!" "TableService returns invalid name message for empty string" "Challenger2_M2" "Area_Deletion_Guards"

# Test 1.6: Delete area with whitespace-only string via TableService
$res1_6 = Invoke-DeleteArea $tableService "     `t  `n  "
Assert-False $res1_6.Success "TableService rejects whitespace area name" "Challenger2_M2" "Area_Deletion_Guards"
Assert-Equal $res1_6.Message "Tên khu vực không hợp lệ!" "TableService returns invalid name message for whitespace" "Challenger2_M2" "Area_Deletion_Guards"

# Test 1.7: Delete area with null string via TableService
$res1_7 = Invoke-DeleteArea $tableService $null
Assert-False $res1_7.Success "TableService rejects null area name without exception" "Challenger2_M2" "Area_Deletion_Guards"
Assert-Equal $res1_7.Message "Tên khu vực không hợp lệ!" "TableService returns invalid name message for null" "Challenger2_M2" "Area_Deletion_Guards"

# Test 1.8: Delete area with empty string via TableRepository directly
$res1_8 = Invoke-DeleteArea $tableRepo ""
Assert-False $res1_8.Success "TableRepository rejects empty string area name" "Challenger2_M2" "Area_Deletion_Guards"
Assert-Equal $res1_8.Message "Tên khu vực không hợp lệ!" "TableRepository returns invalid name message for empty string" "Challenger2_M2" "Area_Deletion_Guards"

# Test 1.9: Delete area with null string via TableRepository directly
$res1_9 = Invoke-DeleteArea $tableRepo $null
Assert-False $res1_9.Success "TableRepository rejects null area name without exception" "Challenger2_M2" "Area_Deletion_Guards"
Assert-Equal $res1_9.Message "Tên khu vực không hợp lệ!" "TableRepository returns invalid name message for null" "Challenger2_M2" "Area_Deletion_Guards"

# Test 1.10: Delete area WITHOUT active tables (Area 2: 'Sân vườn') via TableService
$res1_10 = Invoke-DeleteArea $tableService "Sân vườn"
Assert-True $res1_10.Success "TableService successfully deletes area without active tables" "Challenger2_M2" "Area_Deletion_Guards"
Assert-True ($res1_10.Message -like "*Đã xóa khu vực 'Sân vườn'*") "TableService returns deletion success message" "Challenger2_M2" "Area_Deletion_Guards"

# Test 1.11: Immediate duplicate deletion of 'Sân vườn' must fail gracefully
$res1_11 = Invoke-DeleteArea $tableService "Sân vườn"
Assert-False $res1_11.Success "Second deletion of 'Sân vườn' fails because area was already removed" "Challenger2_M2" "Area_Deletion_Guards"
Assert-True ($res1_11.Message -like "*không tồn tại trong hệ thống!*") "Second deletion returns not found message" "Challenger2_M2" "Area_Deletion_Guards"

# Restore 'Sân vườn' to fallback areas so subsequent runs have consistent initial state
$fallbackAreasField = $tableRepoType.GetField("_fallbackAreas", [System.Reflection.BindingFlags]"NonPublic, Static")
if ($fallbackAreasField) {
    $fallbackAreasList = $fallbackAreasField.GetValue($null)
    $areaType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Area")
    $restoredArea = [Activator]::CreateInstance($areaType)
    $restoredArea.Id = 2
    $restoredArea.AreaName = "Sân vườn"
    $restoredArea.Description = "Không gian ngoài trời thoáng đãng"
    $fallbackAreasList.Add($restoredArea)
}

# Test 1.12: BLL TableService Architecture Leak Audit
$tableServiceSource = Get-Content -Path (Join-Path $global:ProjectRoot "BLL\TableService.cs") -Raw
$hasDirectDataContextInDelete = $tableServiceSource -match "public bool DeleteAreaByName[\s\S]*?GetContext\(\)"
$hasSubmitChangesInDelete = $tableServiceSource -match "public bool DeleteAreaByName[\s\S]*?SubmitChanges\(\)"
Assert-False $hasDirectDataContextInDelete "TableService.DeleteAreaByName contains ZERO direct DataContext instantiations" "Challenger2_M2" "Architecture_Boundaries"
Assert-False $hasSubmitChangesInDelete "TableService.DeleteAreaByName contains ZERO SubmitChanges() calls" "Challenger2_M2" "Architecture_Boundaries"

# ==============================================================================
# SUITE 2: In-Memory / Fallback Query Behavior Stress Tests
# ==============================================================================
Write-Host "`n--- [Suite 2] In-Memory / Fallback Query Behavior Stress Tests ---" -ForegroundColor Yellow

# Test 2.1: TableRepository.GetAllTablesWithArea returns valid non-null TableDTO list
$allTablesWithArea = $tableRepo.GetAllTablesWithArea()
Assert-NotNull $allTablesWithArea "GetAllTablesWithArea returns non-null list in fallback mode" "Challenger2_M2" "Fallback_Queries"
Assert-True ($allTablesWithArea.Count -ge 3) "GetAllTablesWithArea returns at least 3 fallback tables" "Challenger2_M2" "Fallback_Queries"

# Test 2.2: Verify joined AreaName is populated without NullReferenceException
$table1 = $allTablesWithArea | Where-Object { $_.Id -eq 1 } | Select-Object -First 1
Assert-NotNull $table1 "Table 1 is present in GetAllTablesWithArea results" "Challenger2_M2" "Fallback_Queries"
Assert-Equal $table1.TableName "Khách tại quầy / Mang đi" "Table 1 TableName correctly mapped" "Challenger2_M2" "Fallback_Queries"
Assert-Equal $table1.AreaName "Khu máy lạnh (Tầng 1)" "Table 1 AreaName correctly joined from fallback areas" "Challenger2_M2" "Fallback_Queries"

# Test 2.3: Verify null CustomerId on Table 1 does not throw and produces null CustomerName/Phone
Assert-True ($null -eq $table1.CustomerId -or $table1.CustomerId -eq "") "Table 1 has null/empty CustomerId" "Challenger2_M2" "Fallback_Queries"
Assert-True ($null -eq $table1.CustomerName) "Table 1 CustomerName is null when CustomerId is empty (no NullRef)" "Challenger2_M2" "Fallback_Queries"

# Test 2.4: Stress test GetAllTablesWithArea with simulated orphaned table (unknown AreaId)
$fallbackTablesField = $tableRepoType.GetField("FallbackTables", [System.Reflection.BindingFlags]"NonPublic, Static, Public")
$tableType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Table")
$fallbackTablesList = $fallbackTablesField.GetValue($null)

$orphanedTable = [Activator]::CreateInstance($tableType)
$orphanedTable.Id = 999
$orphanedTable.TableName = "Bàn mồ côi"
$orphanedTable.AreaId = 99999 # No such area in _fallbackAreas
$orphanedTable.Status = "Trống"
$fallbackTablesList.Add($orphanedTable)

# GetAllTablesWithArea must run without NullReferenceException
$tablesAfterOrphan = $null
$orphanQueryException = $null
try {
    $tablesAfterOrphan = $tableRepo.GetAllTablesWithArea()
} catch {
    $orphanQueryException = $_.Exception.Message
}
Assert-True ($null -eq $orphanQueryException) "GetAllTablesWithArea survives orphaned AreaId without NullReferenceException" "Challenger2_M2" "Fallback_Queries"
Assert-NotNull $tablesAfterOrphan "GetAllTablesWithArea returned non-null list even with orphaned table" "Challenger2_M2" "Fallback_Queries"

# Cleanup orphaned table
$fallbackTablesList.Remove($orphanedTable) | Out-Null

# Test 2.5: OrderRepository.GetOrderDetailsByOrderId on non-existent orderId = -9999
$orderDetailsNonExistent = $orderRepo.GetOrderDetailsByOrderId(-9999)
Assert-NotNull $orderDetailsNonExistent "GetOrderDetailsByOrderId(-9999) returns non-null list" "Challenger2_M2" "Fallback_Queries"
Assert-Equal $orderDetailsNonExistent.Count 0 "GetOrderDetailsByOrderId(-9999) returns exactly 0 items" "Challenger2_M2" "Fallback_Queries"

# Test 2.6: OrderRepository.GetOrderDetailsByOrderId on orderId = 0
$orderDetailsZero = $orderRepo.GetOrderDetailsByOrderId(0)
Assert-NotNull $orderDetailsZero "GetOrderDetailsByOrderId(0) returns non-null list" "Challenger2_M2" "Fallback_Queries"
Assert-Equal $orderDetailsZero.Count 0 "GetOrderDetailsByOrderId(0) returns exactly 0 items" "Challenger2_M2" "Fallback_Queries"

# Test 2.7: OrderRepository.GetOrderDetailsByOrderId on orderId with actual items
$fallbackOrderDetailsField = $orderRepoType.GetField("FallbackOrderDetails", [System.Reflection.BindingFlags]"NonPublic, Static, Public")
$fallbackOrderDetailsList = $fallbackOrderDetailsField.GetValue($null)
$orderDetailType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.OrderDetail")

$testDetail = [Activator]::CreateInstance($orderDetailType)
$testDetail.Id = 8888
$testDetail.OrderId = 7777
$testDetail.ProductId = 1 # Cà phê đen đá
$testDetail.Quantity = 3
$testDetail.UnitPrice = [decimal]25000
$testDetail.TotalPrice = [decimal]75000
$fallbackOrderDetailsList.Add($testDetail)

$details7777 = $orderRepo.GetOrderDetailsByOrderId(7777)
Assert-NotNull $details7777 "GetOrderDetailsByOrderId(7777) returns non-null list" "Challenger2_M2" "Fallback_Queries"
Assert-Equal $details7777.Count 1 "GetOrderDetailsByOrderId(7777) returned 1 item" "Challenger2_M2" "Fallback_Queries"
$item7777 = $details7777[0]
Assert-Equal $item7777.ProductName "Cà phê đen đá" "ProductName joined correctly from _fallbackProducts" "Challenger2_M2" "Fallback_Queries"
Assert-Equal $item7777.Unit "Ly" "Unit joined correctly from _fallbackProducts" "Challenger2_M2" "Fallback_Queries"
Assert-Equal $item7777.Quantity 3 "Quantity mapped correctly" "Challenger2_M2" "Fallback_Queries"
Assert-Equal $item7777.TotalPrice ([decimal]75000) "TotalPrice mapped correctly" "Challenger2_M2" "Fallback_Queries"

# Test 2.8: Stress test GetOrderDetailsByOrderId with unknown ProductId = 99999
$orphanDetail = [Activator]::CreateInstance($orderDetailType)
$orphanDetail.Id = 8889
$orphanDetail.OrderId = 7777
$orphanDetail.ProductId = 99999 # No such product in _fallbackProducts
$orphanDetail.Quantity = 1
$orphanDetail.UnitPrice = [decimal]50000
$orphanDetail.TotalPrice = [decimal]50000
$fallbackOrderDetailsList.Add($orphanDetail)

$orphanOrderDetailsException = $null
$detailsWithOrphan = $null
try {
    $detailsWithOrphan = $orderRepo.GetOrderDetailsByOrderId(7777)
} catch {
    $orphanOrderDetailsException = $_.Exception.Message
}
Assert-True ($null -eq $orphanOrderDetailsException) "GetOrderDetailsByOrderId survives unknown ProductId without NullReferenceException" "Challenger2_M2" "Fallback_Queries"
Assert-NotNull $detailsWithOrphan "GetOrderDetailsByOrderId returns valid list" "Challenger2_M2" "Fallback_Queries"

# Clean up simulated order details
$fallbackOrderDetailsList.Remove($testDetail) | Out-Null
$fallbackOrderDetailsList.Remove($orphanDetail) | Out-Null

# ==============================================================================
# SUITE 3: FrmAppConfig Connection String Synchronizer Stress Tests
# ==============================================================================
Write-Host "`n--- [Suite 3] FrmAppConfig Connection String Synchronizer Stress Tests ---" -ForegroundColor Yellow

$appConfigFile = Join-Path $global:ProjectRoot "App.config"
[xml]$appConfigXml = Get-Content -Path $appConfigFile -Raw

# Test 3.1: Verify all 3 connection string keys exist in App.config
$key1 = "CoffeeShopDB"
$key2 = "DoAnCuoiKi_QuanLyTiemCaPhe.Properties.Settings.CoffeeShopDBConnectionString"
$key3 = "DoAnCuoiKi_QuanLyTiemCaPhe.Properties.Settings.CoffeeShopDBConnectionString1"

$cs1 = $appConfigXml.configuration.connectionStrings.add | Where-Object { $_.name -eq $key1 }
$cs2 = $appConfigXml.configuration.connectionStrings.add | Where-Object { $_.name -eq $key2 }
$cs3 = $appConfigXml.configuration.connectionStrings.add | Where-Object { $_.name -eq $key3 }

Assert-NotNull $cs1 "App.config contains '$key1'" "Challenger2_M2" "Config_Sync"
Assert-NotNull $cs2 "App.config contains '$key2'" "Challenger2_M2" "Config_Sync"
Assert-NotNull $cs3 "App.config contains '$key3'" "Challenger2_M2" "Config_Sync"

# Test 3.2: Inspect FrmAppConfig.cs source code for synchronizer compliance
$frmAppConfigSource = Get-Content -Path (Join-Path $global:ProjectRoot "Form\VanThanh\FrmAppConfig.cs") -Raw
$hasSync1 = $frmAppConfigSource -match '_configService\.UpdateConnectionString\("CoffeeShopDB",\s*newConnString\)'
$hasSync2 = $frmAppConfigSource -match '_configService\.UpdateConnectionString\("DoAnCuoiKi_QuanLyTiemCaPhe\.Properties\.Settings\.CoffeeShopDBConnectionString",\s*newConnString\)'
$hasSync3 = $frmAppConfigSource -match '_configService\.UpdateConnectionString\("DoAnCuoiKi_QuanLyTiemCaPhe\.Properties\.Settings\.CoffeeShopDBConnectionString1",\s*newConnString\)'

Assert-True $hasSync1 "FrmAppConfig.cs updates 'CoffeeShopDB'" "Challenger2_M2" "Config_Sync"
Assert-True $hasSync2 "FrmAppConfig.cs updates 'CoffeeShopDBConnectionString'" "Challenger2_M2" "Config_Sync"
Assert-True $hasSync3 "FrmAppConfig.cs updates 'CoffeeShopDBConnectionString1'" "Challenger2_M2" "Config_Sync"

# Test 3.3: AppConfigService.UpdateConnectionString edge cases (null, empty, whitespace)
$resNullName = $appConfigService.UpdateConnectionString($null, "Server=.;Database=Test;Integrated Security=True")
$resEmptyName = $appConfigService.UpdateConnectionString("", "Server=.;Database=Test;Integrated Security=True")
$resNullVal = $appConfigService.UpdateConnectionString("CoffeeShopDB", $null)
$resEmptyVal = $appConfigService.UpdateConnectionString("CoffeeShopDB", "")

Assert-False $resNullName "AppConfigService rejects null connection string name" "Challenger2_M2" "Config_Sync"
Assert-False $resEmptyName "AppConfigService rejects empty connection string name" "Challenger2_M2" "Config_Sync"
Assert-False $resNullVal "AppConfigService rejects null connection string value" "Challenger2_M2" "Config_Sync"
Assert-False $resEmptyVal "AppConfigService rejects empty connection string value" "Challenger2_M2" "Config_Sync"

# Test 3.4: ConfigHelper.InvalidateCache execution without error via reflection
$cacheClearException = $null
try {
    $invalidateMethod = $configHelperType.GetMethod("InvalidateCache", [System.Reflection.BindingFlags]"Public, Static")
    $invalidateMethod.Invoke($null, $null)
} catch {
    $cacheClearException = $_.Exception.Message
}
Assert-True ($null -eq $cacheClearException) "ConfigHelper.InvalidateCache executes cleanly without exception" "Challenger2_M2" "Config_Sync"

# Test 3.5: ConfigHelper.GetConnectionString returns non-empty strings for all 3 keys via reflection
$getConnMethod = $configHelperType.GetMethod("GetConnectionString", [Type[]]@([string]))
$resolved1 = $getConnMethod.Invoke($null, @($key1))
$resolved2 = $getConnMethod.Invoke($null, @($key2))
$resolved3 = $getConnMethod.Invoke($null, @($key3))

Assert-True (-not [string]::IsNullOrWhiteSpace($resolved1)) "ConfigHelper resolves non-empty connection string for '$key1'" "Challenger2_M2" "Config_Sync"
Assert-True (-not [string]::IsNullOrWhiteSpace($resolved2)) "ConfigHelper resolves non-empty connection string for '$key2'" "Challenger2_M2" "Config_Sync"
Assert-True (-not [string]::IsNullOrWhiteSpace($resolved3)) "ConfigHelper resolves non-empty connection string for '$key3'" "Challenger2_M2" "Config_Sync"

# ==============================================================================
# SUITE 4: Zero LINQ Query Comprehension Syntax Verification Across Entire Codebase
# ==============================================================================
Write-Host "`n--- [Suite 4] Zero LINQ Query Comprehension Syntax Audit ---" -ForegroundColor Yellow

$csFiles = Get-ChildItem -Path $global:ProjectRoot -Recurse -Filter "*.cs" |
    Where-Object {
        $_.FullName -notmatch "\\(bin|obj|publish|packages|\.vs)\\" -and
        $_.Name -notmatch "\.Designer\.cs$" -and
        $_.Name -notmatch "CoffeeShopDataContext\.designer\.cs$"
    }

$querySyntaxMatches = @()
foreach ($file in $csFiles) {
    $lines = Get-Content -Path $file.FullName
    for ($i = 0; $i -lt $lines.Count; $i++) {
        $line = $lines[$i]
        $trimmed = $line.Trim()
        if ($trimmed.StartsWith("//") -or $trimmed.StartsWith("/*") -or $trimmed.StartsWith("*")) {
            continue
        }
        if ($line -match "\bfrom\s+[a-zA-Z0-9_]+\s+in\s+") {
            $querySyntaxMatches += [PSCustomObject]@{
                File = $file.FullName.Replace($global:ProjectRoot, "")
                Line = $i + 1
                Text = $trimmed
            }
        }
    }
}

Assert-Equal $querySyntaxMatches.Count 0 "Zero LINQ query comprehension syntax ('from ... in ...') across all non-generated .cs files" "Challenger2_M2" "LINQ_Lambda_Compliance"

Write-Host "`n==========================================================================" -ForegroundColor Cyan
Write-Host "   CHALLENGER 2 EMPIRICAL STRESS TESTS COMPLETE                          " -ForegroundColor Cyan
Write-Host "==========================================================================" -ForegroundColor Cyan

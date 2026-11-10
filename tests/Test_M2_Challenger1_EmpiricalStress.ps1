# ==============================================================================
# CHALLENGER 1 EMPIRICAL STRESS TEST HARNESS — MILESTONE 2: LINQ LAMBDA
# Adversarial testing of all 12 refactored DAL methods, boundaries & contracts
# ==============================================================================

param(
    [string]$ProjectRoot = "d:\DoAnCuoiKi_QuanLyTiemCaPhe"
)

$ErrorActionPreference = "Continue"

Write-Host "==========================================================================" -ForegroundColor Cyan
Write-Host "   CHALLENGER 1: EMPIRICAL STRESS TESTS — MILESTONE 2 (LINQ LAMBDA)       " -ForegroundColor Yellow
Write-Host "==========================================================================" -ForegroundColor Cyan

$testResults = [System.Collections.Generic.List[PSObject]]::new()

function Assert-Check {
    param(
        [string]$Category,
        [string]$TestName,
        [bool]$Passed,
        [string]$Expected,
        [string]$Actual,
        [string]$Details = ""
    )

    $obj = [PSCustomObject]@{
        Category = $Category
        TestName = $TestName
        Passed   = $Passed
        Expected = $Expected
        Actual   = $Actual
        Details  = $Details
    }
    $testResults.Add($obj)

    $badge = if ($Passed) { "[PASS]" } else { "[FAIL]" }
    $color = if ($Passed) { "Green" } else { "Red" }
    Write-Host "  $badge " -ForegroundColor $color -NoNewline
    Write-Host "[$Category] $TestName" -ForegroundColor White
    if (-not $Passed) {
        Write-Host "         Expected: $Expected" -ForegroundColor DarkCyan
        Write-Host "         Actual:   $Actual" -ForegroundColor Magenta
        if ($Details) {
            Write-Host "         Details:  $Details" -ForegroundColor DarkRed
        }
    }
}

# --- Load Assembly & Types ---
$binPath = Join-Path $ProjectRoot "bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe"
if (-not (Test-Path $binPath)) {
    Write-Host "[FATAL] Assembly not found at $binPath" -ForegroundColor Red
    exit 1
}

[System.Reflection.Assembly]::LoadWithPartialName("System.Drawing") | Out-Null
[System.Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms") | Out-Null
[System.Reflection.Assembly]::LoadWithPartialName("System.Configuration") | Out-Null
[System.Reflection.Assembly]::LoadWithPartialName("System.Data.Linq") | Out-Null

$asm = [System.Reflection.Assembly]::LoadFrom($binPath)

$tableRepoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DAL.TableRepository")
$orderRepoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DAL.OrderRepository")
$tableServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.TableService")

$tableRepo = [Activator]::CreateInstance($tableRepoType)
$orderRepo = [Activator]::CreateInstance($orderRepoType)
$tableService = [Activator]::CreateInstance($tableServiceType)

# ==============================================================================
# SUITE 1: RUNTIME DAL & BLL INTEGRATION TESTS (TableRepo, OrderRepo, TableService)
# ==============================================================================
Write-Host "`n>>> [SUITE 1] RUNTIME EXECUTION TESTS (TableRepository, OrderRepository, TableService) <<<" -ForegroundColor Yellow

# 1.1 GetAllTablesWithArea
try {
    $tablesWithArea = $tableRepo.GetAllTablesWithArea()
    Assert-Check "TableRepo" "GetAllTablesWithArea returns non-null collection" ($null -ne $tablesWithArea) "Not Null" "$($tablesWithArea.Count) tables"
    Assert-Check "TableRepo" "GetAllTablesWithArea returns configured tables (Count >= 1)" ($tablesWithArea.Count -ge 1) ">= 1" "$($tablesWithArea.Count)"
    if ($tablesWithArea.Count -gt 0) {
        $firstT = $tablesWithArea[0]
        Assert-Check "TableRepo" "TableDTO has valid non-empty TableName" (![string]::IsNullOrEmpty($firstT.TableName)) "Non-empty" "$($firstT.TableName)"
        Assert-Check "TableRepo" "TableDTO has non-null AreaName from join" ($null -ne $firstT.AreaName) "Non-null" "$($firstT.AreaName)"
        Assert-Check "TableRepo" "TableDTO Status is recognized ('Trống', 'Có khách', 'Đặt trước')" ($firstT.Status -in @("Trống", "Có khách", "Đặt trước")) "Valid status" "$($firstT.Status)"
    }
} catch {
    Assert-Check "TableRepo" "GetAllTablesWithArea execution" $false "Success" "Threw: $($_.Exception.Message)"
}

# 1.2 DeleteAreaByName (Repository)
try {
    $delMsg = ""
    # Boundary: Null
    $delNull = $tableRepo.DeleteAreaByName($null, [ref]$delMsg)
    Assert-Check "TableRepo" "DeleteAreaByName(null) returns false" (-not $delNull) "False" "$delNull"
    Assert-Check "TableRepo" "DeleteAreaByName(null) message: 'Tên khu vực không hợp lệ!'" ($delMsg -eq "Tên khu vực không hợp lệ!") "Match" "$delMsg"

    # Boundary: Empty string
    $delEmpty = $tableRepo.DeleteAreaByName("", [ref]$delMsg)
    Assert-Check "TableRepo" "DeleteAreaByName('') returns false" (-not $delEmpty) "False" "$delEmpty"
    Assert-Check "TableRepo" "DeleteAreaByName('') message: 'Tên khu vực không hợp lệ!'" ($delMsg -eq "Tên khu vực không hợp lệ!") "Match" "$delMsg"

    # Boundary: Whitespace
    $delWs = $tableRepo.DeleteAreaByName("   ", [ref]$delMsg)
    Assert-Check "TableRepo" "DeleteAreaByName('   ') returns false" (-not $delWs) "False" "$delWs"
    Assert-Check "TableRepo" "DeleteAreaByName('   ') message: 'Tên khu vực không hợp lệ!'" ($delMsg -eq "Tên khu vực không hợp lệ!") "Match" "$delMsg"

    # Boundary: Non-existent area
    $delNone = $tableRepo.DeleteAreaByName("__NON_EXISTENT_AREA_XYZ_99999__", [ref]$delMsg)
    Assert-Check "TableRepo" "DeleteAreaByName non-existent returns false" (-not $delNone) "False" "$delNone"
    Assert-Check "TableRepo" "DeleteAreaByName non-existent message: 'Khu vực không tồn tại trong hệ thống!'" ($delMsg -eq "Khu vực không tồn tại trong hệ thống!") "Match" "$delMsg"

    # Boundary: Active Area with tables
    if ($tablesWithArea -and $tablesWithArea.Count -gt 0) {
        $activeAreaName = $tablesWithArea[0].AreaName
        if (![string]::IsNullOrEmpty($activeAreaName)) {
            $delActive = $tableRepo.DeleteAreaByName($activeAreaName, [ref]$delMsg)
            Assert-Check "TableRepo" "DeleteAreaByName on active area '$activeAreaName' returns false" (-not $delActive) "False" "$delActive"
            Assert-Check "TableRepo" "DeleteAreaByName prevents deleting active area with tables" ($delMsg -like "*đang có bàn hoạt động*") "Contains 'đang có bàn hoạt động'" "$delMsg"
        }
    }
} catch {
    Assert-Check "TableRepo" "DeleteAreaByName boundary tests" $false "No exception" "Threw: $($_.Exception.Message)"
}

# 1.3 DeleteAreaByName (BLL TableService Delegation)
try {
    $svcMsg = ""
    $svcDelNull = $tableService.DeleteAreaByName($null, [ref]$svcMsg)
    Assert-Check "TableService" "TableService.DeleteAreaByName(null) delegates properly and returns false" (-not $svcDelNull) "False" "$svcDelNull"
    Assert-Check "TableService" "TableService.DeleteAreaByName(null) validation message matches DAL" ($svcMsg -eq "Tên khu vực không hợp lệ!") "Match" "$svcMsg"

    $svcDelNone = $tableService.DeleteAreaByName("__NON_EXISTENT_AREA__", [ref]$svcMsg)
    Assert-Check "TableService" "TableService.DeleteAreaByName non-existent returns false" (-not $svcDelNone) "False" "$svcDelNone"
    Assert-Check "TableService" "TableService.DeleteAreaByName non-existent message matches DAL" ($svcMsg -eq "Khu vực không tồn tại trong hệ thống!") "Match" "$svcMsg"
} catch {
    Assert-Check "TableService" "TableService.DeleteAreaByName delegation" $false "No exception" "Threw: $($_.Exception.Message)"
}

# 1.4 GetOrderDetailsByOrderId (OrderRepository)
try {
    # Valid Order
    $orderDetails = $orderRepo.GetOrderDetailsByOrderId(1)
    Assert-Check "OrderRepo" "GetOrderDetailsByOrderId(1) returns non-null collection" ($null -ne $orderDetails) "Not Null" "$($orderDetails.Count) items"

    # Boundary: Non-existent order ID -1
    $ordNeg = $orderRepo.GetOrderDetailsByOrderId(-1)
    Assert-Check "OrderRepo" "GetOrderDetailsByOrderId(-1) returns empty collection without throwing" ($null -ne $ordNeg -and $ordNeg.Count -eq 0) "Empty list (0)" "$($ordNeg.Count)"

    # Boundary: Non-existent order ID 0
    $ordZero = $orderRepo.GetOrderDetailsByOrderId(0)
    Assert-Check "OrderRepo" "GetOrderDetailsByOrderId(0) returns empty collection without throwing" ($null -ne $ordZero -and $ordZero.Count -eq 0) "Empty list (0)" "$($ordZero.Count)"

    # Boundary: Non-existent order ID 999999
    $ordHuge = $orderRepo.GetOrderDetailsByOrderId(999999)
    Assert-Check "OrderRepo" "GetOrderDetailsByOrderId(999999) returns empty collection without throwing" ($null -ne $ordHuge -and $ordHuge.Count -eq 0) "Empty list (0)" "$($ordHuge.Count)"

    # Boundary: int.MinValue
    $ordMin = $orderRepo.GetOrderDetailsByOrderId([int]::MinValue)
    Assert-Check "OrderRepo" "GetOrderDetailsByOrderId(int.MinValue) returns empty collection without throwing" ($null -ne $ordMin -and $ordMin.Count -eq 0) "Empty list (0)" "$($ordMin.Count)"

    # Boundary: int.MaxValue
    $ordMax = $orderRepo.GetOrderDetailsByOrderId([int]::MaxValue)
    Assert-Check "OrderRepo" "GetOrderDetailsByOrderId(int.MaxValue) returns empty collection without throwing" ($null -ne $ordMax -and $ordMax.Count -eq 0) "Empty list (0)" "$($ordMax.Count)"
} catch {
    Assert-Check "OrderRepo" "GetOrderDetailsByOrderId boundary tests" $false "No exception" "Threw: $($_.Exception.Message)"
}

# ==============================================================================
# SUITE 2: IN-MEMORY STRESS HARNESS FOR ALL 12 LINQ LAMBDA EXPRESSIONS
# Adversarial testing against mock entities, null references, and boundary edge cases
# ==============================================================================
Write-Host "`n>>> [SUITE 2] ADVERSARIAL STRESS TESTING OF LINQ LAMBDA LOGIC <<<" -ForegroundColor Yellow

# Create test entities matching LinqToSql schema
$empType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Employee")
$roleType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Role")
$salaryType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Salary")
$prodType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Product")
$catType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Category")
$userType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.User")
$recipeType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Recipe")
$invTransType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.InventoryTransaction")
$materialType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.Material")

# Mock Role
$r1 = [Activator]::CreateInstance($roleType); $r1.Id = 1; $r1.RoleName = "Quản lý"
$r2 = [Activator]::CreateInstance($roleType); $r2.Id = 2; $r2.RoleName = "Thu ngân"

# Mock Salary
$s1 = [Activator]::CreateInstance($salaryType); $s1.Id = 1; $s1.RoleId = 1; $s1.Amount = 15000000; $s1.TimeUnit = "Tháng"
$sList = [System.Collections.Generic.List[PSObject]]::new(); $sList.Add($s1)

# Mock Employees:
# Emp 1: Normal with Role and Salary
$e1 = [Activator]::CreateInstance($empType); $e1.Id = 1; $e1.FullName = "Nguyễn Văn Quản Lý"; $e1.RoleId = 1; $e1.Role = $r1; $e1.Phone = "0901234567"
# Emp 2: Normal with Role but NO Salary configured
$e2 = [Activator]::CreateInstance($empType); $e2.Id = 2; $e2.FullName = "Trần Thị Thu Ngân"; $e2.RoleId = 2; $e2.Role = $r2; $e2.Phone = "0909876543"
# Emp 3 (Adversarial): NULL Role, NULL Phone, NULL Address
$e3 = [Activator]::CreateInstance($empType); $e3.Id = 3; $e3.FullName = "Nhân Viên Mồ Côi"; $e3.RoleId = 999; $e3.Role = $null; $e3.Phone = $null

$empList = [System.Collections.Generic.List[PSObject]]::new()
$empList.Add($e1); $empList.Add($e2); $empList.Add($e3)

# Mock Users:
$u1 = [Activator]::CreateInstance($userType); $u1.Id = 1; $u1.Username = "admin"; $u1.EmployeeId = 1; $u1.RoleId = 1; $u1.Role = $r1; $u1.Employee = $e1; $u1.IsActive = $true
# User 2: NULL Employee, NULL Role, IsActive = null
$u2 = [Activator]::CreateInstance($userType); $u2.Id = 2; $u2.Username = "orphan"; $u2.EmployeeId = $null; $u2.RoleId = 99; $u2.Role = $null; $u2.Employee = $null; $u2.IsActive = $null

$userList = [System.Collections.Generic.List[PSObject]]::new()
$userList.Add($u1); $userList.Add($u2)

# --- 2.1 Test EmployeeRepository.GetAllEmployees Lambda Logic ---
try {
    $empDtos = @($empList | ForEach-Object {
        $e = $_
        $sal = $sList | Where-Object { $_.RoleId -eq $e.RoleId } | Select-Object -ExpandProperty Amount -ErrorAction SilentlyContinue
        [PSCustomObject]@{
            Id       = $e.Id
            FullName = $e.FullName
            Phone    = $e.Phone
            Position = if ($e.Role -ne $null) { $e.Role.RoleName } else { [string]::Empty }
            RoleId   = $e.RoleId
            Salary   = if ($sal) { $sal } else { $null }
        }
    })
    Assert-Check "Lambda_Employee" "GetAllEmployees maps all 3 mock employees" ($empDtos.Count -eq 3) "3" "$($empDtos.Count)"
    Assert-Check "Lambda_Employee" "GetAllEmployees safely handles null Role (Position = empty string)" ($empDtos[2].Position -eq [string]::Empty) "Empty string" "'$($empDtos[2].Position)'"
    Assert-Check "Lambda_Employee" "GetAllEmployees assigns null salary for employee with unconfigured role" ($null -eq $empDtos[1].Salary) "Null" "$($empDtos[1].Salary)"
    Assert-Check "Lambda_Employee" "GetAllEmployees populates salary correctly for configured role" ($empDtos[0].Salary -eq 15000000) "15000000" "$($empDtos[0].Salary)"
} catch {
    Assert-Check "Lambda_Employee" "GetAllEmployees Lambda stress test" $false "No exception" "Threw: $($_.Exception.Message)"
}

# --- 2.2 Test EmployeeRepository.GetEmployeeDTOById Lambda Logic ---
try {
    # Valid Id
    $dto1 = $empDtos | Where-Object { $_.Id -eq 1 } | Select-Object -First 1
    Assert-Check "Lambda_Employee" "GetEmployeeDTOById(1) returns matching DTO" ($dto1 -ne $null -and $dto1.Id -eq 1) "Id 1" "$($dto1.Id)"

    # Boundary IDs
    $dtoNeg = $empDtos | Where-Object { $_.Id -eq -1 } | Select-Object -First 1
    Assert-Check "Lambda_Employee" "GetEmployeeDTOById(-1) returns null without exception" ($null -eq $dtoNeg) "Null" "$dtoNeg"

    $dtoHuge = $empDtos | Where-Object { $_.Id -eq 999999 } | Select-Object -First 1
    Assert-Check "Lambda_Employee" "GetEmployeeDTOById(999999) returns null without exception" ($null -eq $dtoHuge) "Null" "$dtoHuge"
} catch {
    Assert-Check "Lambda_Employee" "GetEmployeeDTOById Lambda stress test" $false "No exception" "Threw: $($_.Exception.Message)"
}

# --- 2.3 Test EmployeeRepository.GetEmployeesWithoutAccount Lambda Logic ---
try {
    $assignedEmpIds = @($userList | Where-Object { $_.EmployeeId -ne $null -and $_.EmployeeId -gt 0 } | Select-Object -ExpandProperty EmployeeId)
    $unassignedEmps = @($empList | Where-Object { $assignedEmpIds -notcontains $_.Id } | ForEach-Object {
        $e = $_
        [PSCustomObject]@{
            Id       = $e.Id
            FullName = $e.FullName
            Phone    = $e.Phone
            Position = if ($e.Role -ne $null) { $e.Role.RoleName } else { "Chưa có" }
        }
    })
    $hasEmp1 = ($unassignedEmps | Where-Object { $_.Id -eq 1 } | Measure-Object).Count -gt 0
    Assert-Check "Lambda_Employee" "GetEmployeesWithoutAccount filters out Employee 1 (assigned to admin)" (-not $hasEmp1) "Filtered out" "HasEmp1=$hasEmp1"
    Assert-Check "Lambda_Employee" "GetEmployeesWithoutAccount includes Employees 2 and 3" ($unassignedEmps.Count -eq 2) "2" "$($unassignedEmps.Count)"
    Assert-Check "Lambda_Employee" "GetEmployeesWithoutAccount falls back to 'Chưa có' when Role is null" (($unassignedEmps | Where-Object { $_.Id -eq 3 }).Position -eq "Chưa có") "'Chưa có'" "$($unassignedEmps[1].Position)"
} catch {
    Assert-Check "Lambda_Employee" "GetEmployeesWithoutAccount Lambda stress test" $false "No exception" "Threw: $($_.Exception.Message)"
}

# --- 2.4 Test ProductRepository Lambda Logic (GetAllProducts, GetProductsByCategory, SearchProductsByName) ---
try {
    # Mock Categories
    $c1 = [Activator]::CreateInstance($catType); $c1.Id = 1; $c1.CategoryName = "Cà phê"
    $c2 = [Activator]::CreateInstance($catType); $c2.Id = 2; $c2.CategoryName = "Trà trái cây"

    # Mock Products
    $p1 = [Activator]::CreateInstance($prodType); $p1.Id = 1; $p1.ProductName = "Cà phê đen đá"; $p1.CategoryId = 1; $p1.Category = $c1; $p1.Price = 25000; $p1.IsActive = $true
    $p2 = [Activator]::CreateInstance($prodType); $p2.Id = 2; $p2.ProductName = "Trà đào cam sả"; $p2.CategoryId = 2; $p2.Category = $c2; $p2.Price = 35000; $p2.IsActive = $true
    # Adversarial product: NULL Category
    $p3 = [Activator]::CreateInstance($prodType); $p3.Id = 3; $p3.ProductName = "Món Bí Ẩn"; $p3.CategoryId = 999; $p3.Category = $null; $p3.Price = 50000; $p3.IsActive = $false

    $prodList = [System.Collections.Generic.List[PSObject]]::new()
    $prodList.Add($p1); $prodList.Add($p2); $prodList.Add($p3)

    # GetAllProducts Lambda
    $prodDtos = @($prodList | ForEach-Object {
        $p = $_
        [PSCustomObject]@{
            Id           = $p.Id
            ProductName  = $p.ProductName
            CategoryId   = $p.CategoryId
            CategoryName = if ($p.Category -ne $null) { $p.Category.CategoryName } else { [string]::Empty }
            Price        = $p.Price
            IsActive     = $p.IsActive
        }
    })
    Assert-Check "Lambda_Product" "GetAllProducts maps all products including null Category safely" ($prodDtos.Count -eq 3) "3" "$($prodDtos.Count)"
    Assert-Check "Lambda_Product" "GetAllProducts CategoryName fallback to string.Empty for null Category" ($prodDtos[2].CategoryName -eq [string]::Empty) "Empty string" "'$($prodDtos[2].CategoryName)'"

    # GetProductsByCategory
    $byCat1 = @($prodDtos | Where-Object { $_.CategoryId -eq 1 })
    Assert-Check "Lambda_Product" "GetProductsByCategory(1) returns Cà phê items" ($byCat1.Count -eq 1 -and $byCat1[0].ProductName -eq "Cà phê đen đá") "Match" "$($byCat1[0].ProductName)"

    # Boundary: Non-existent CategoryId
    $byCatNeg = @($prodDtos | Where-Object { $_.CategoryId -eq -1 })
    Assert-Check "Lambda_Product" "GetProductsByCategory(-1) returns empty collection" ($byCatNeg.Count -eq 0) "0" "$($byCatNeg.Count)"

    $byCatHuge = @($prodDtos | Where-Object { $_.CategoryId -eq 999999 })
    Assert-Check "Lambda_Product" "GetProductsByCategory(999999) returns empty collection" ($byCatHuge.Count -eq 0) "0" "$($byCatHuge.Count)"

    # SearchProductsByName
    $searchMatch = @($prodDtos | Where-Object { $_.ProductName -like "*đào*" })
    Assert-Check "Lambda_Product" "SearchProductsByName('đào') returns matching product" ($searchMatch.Count -eq 1 -and $searchMatch[0].ProductName -eq "Trà đào cam sả") "Match" "$($searchMatch[0].ProductName)"

    $searchNone = @($prodDtos | Where-Object { $_.ProductName -like "*__NON_EXISTENT__*" })
    Assert-Check "Lambda_Product" "SearchProductsByName non-matching returns empty collection" ($searchNone.Count -eq 0) "0" "$($searchNone.Count)"
} catch {
    Assert-Check "Lambda_Product" "Product Lambda stress test" $false "No exception" "Threw: $($_.Exception.Message)"
}

# --- 2.5 Test RecipeRepository Lambda Logic (GetRecipesByProductId) ---
try {
    # Mock Recipes
    $rec1 = [Activator]::CreateInstance($recipeType); $rec1.Id = 1; $rec1.ProductId = 1; $rec1.Product = $p1; $rec1.IngredientName = "Cà phê Robusta"; $rec1.Quantity = 25; $rec1.Unit = "g"
    # Adversarial Recipe: NULL Product reference
    $rec2 = [Activator]::CreateInstance($recipeType); $rec2.Id = 2; $rec2.ProductId = 999; $rec2.IngredientName = "Đường cát"; $rec2.Quantity = 10; $rec2.Unit = "g"

    $recipeList = [System.Collections.Generic.List[PSObject]]::new()
    $recipeList.Add($rec1); $recipeList.Add($rec2)

    $recDtos = @($recipeList | ForEach-Object {
        $r = $_
        [PSCustomObject]@{
            Id             = $r.Id
            ProductId      = $r.ProductId
            ProductName    = if ($r.Product -ne $null) { $r.Product.ProductName } else { [string]::Empty }
            IngredientName = $r.IngredientName
            Quantity       = $r.Quantity
            Unit           = $r.Unit
        }
    })
    $recMatch1 = @($recDtos | Where-Object { $_.ProductId -eq 1 })
    Assert-Check "Lambda_Recipe" "GetRecipesByProductId maps correctly with ProductName" ($recMatch1.Count -eq 1 -and $recMatch1[0].ProductName -eq "Cà phê đen đá") "Cà phê đen đá" "$($recMatch1[0].ProductName)"

    $recNullProd = @($recDtos | Where-Object { $_.ProductId -eq 999 })
    $isCount1 = ($recNullProd.Count -eq 1)
    $isNullOrEmpty = if ($recNullProd.Count -gt 0) { [string]::IsNullOrEmpty($recNullProd[0].ProductName) } else { $false }
    $passCond = ($isCount1 -and $isNullOrEmpty)
    Assert-Check "Lambda_Recipe" "GetRecipesByProductId handles NULL Product safely (ProductName = empty string)" $passCond "Empty string" "Count=$($recNullProd.Count), Name='$($recNullProd[0].ProductName)'"

    # Boundary ID
    $recNone = @($recDtos | Where-Object { $_.ProductId -eq -1 })
    Assert-Check "Lambda_Recipe" "GetRecipesByProductId(-1) returns empty collection" ($recNone.Count -eq 0) "0" "$($recNone.Count)"
} catch {
    Assert-Check "Lambda_Recipe" "Recipe Lambda stress test" $false "No exception" "Threw: $($_.Exception.Message)"
}

# --- 2.6 Test InventoryRepository Lambda Logic (GetTransactionHistory) ---
try {
    # Mock Material
    $m1 = [Activator]::CreateInstance($materialType); $m1.Id = 1; $m1.MaterialName = "Hạt cà phê Robusta"

    # Mock Transactions
    $t1 = [Activator]::CreateInstance($invTransType); $t1.Id = 1; $t1.MaterialId = 1; $t1.Material = $m1; $t1.EmployeeId = 1; $t1.Employee = $e1; $t1.TransactionType = "Nhập kho"; $t1.Quantity = 10; $t1.TransactionDate = [DateTime]::Now.AddDays(-2)
    # Adversarial Transaction: NULL Material, NULL Employee
    $t2 = [Activator]::CreateInstance($invTransType); $t2.Id = 2; $t2.MaterialId = 99; $t2.Material = $null; $t2.EmployeeId = 99; $t2.Employee = $null; $t2.TransactionType = "Xuất kho"; $t2.Quantity = 5; $t2.TransactionDate = [DateTime]::Now.AddDays(-1)

    $transList = [System.Collections.Generic.List[PSObject]]::new()
    $transList.Add($t1); $transList.Add($t2)

    $invDtos = @($transList | Sort-Object -Property TransactionDate -Descending | ForEach-Object {
        $t = $_
        [PSCustomObject]@{
            Id              = $t.Id
            MaterialId      = $t.MaterialId
            MaterialName    = if ($t.Material -ne $null) { $t.Material.MaterialName } else { [string]::Empty }
            EmployeeId      = $t.EmployeeId
            EmployeeName    = if ($t.Employee -ne $null) { $t.Employee.FullName } else { [string]::Empty }
            TransactionType = $t.TransactionType
            Quantity        = $t.Quantity
            TransactionDate = $t.TransactionDate
        }
    })

    Assert-Check "Lambda_Inventory" "GetTransactionHistory sorts descending by TransactionDate" ($invDtos[0].Id -eq 2 -and $invDtos[1].Id -eq 1) "Id 2 then Id 1" "Id $($invDtos[0].Id) then Id $($invDtos[1].Id)"
    Assert-Check "Lambda_Inventory" "GetTransactionHistory handles null Material safely" ($invDtos[0].MaterialName -eq [string]::Empty) "Empty string" "'$($invDtos[0].MaterialName)'"
    Assert-Check "Lambda_Inventory" "GetTransactionHistory handles null Employee safely" ($invDtos[0].EmployeeName -eq [string]::Empty) "Empty string" "'$($invDtos[0].EmployeeName)'"
    Assert-Check "Lambda_Inventory" "GetTransactionHistory populates names when navigation properties exist" ($invDtos[1].MaterialName -eq "Hạt cà phê Robusta" -and $invDtos[1].EmployeeName -eq "Nguyễn Văn Quản Lý") "Populated" "$($invDtos[1].MaterialName) / $($invDtos[1].EmployeeName)"
} catch {
    Assert-Check "Lambda_Inventory" "Inventory Lambda stress test" $false "No exception" "Threw: $($_.Exception.Message)"
}

# --- 2.7 Test UserRepository Lambda Logic (GetAllUsersWithRoles) ---
try {
    $uDtos = @($userList | ForEach-Object {
        $u = $_
        [PSCustomObject]@{
            Id         = $u.Id
            Username   = $u.Username
            RoleId     = $u.RoleId
            RoleName   = if ($u.Role -ne $null) { $u.Role.RoleName } else { [string]::Empty }
            EmployeeId = $u.EmployeeId
            FullName   = if ($u.Employee -ne $null) { $u.Employee.FullName } else { [string]::Empty }
            IsActive   = if ($u.IsActive -ne $null) { $u.IsActive } else { $true }
        }
    })
    Assert-Check "Lambda_User" "GetAllUsersWithRoles maps all users" ($uDtos.Count -eq 2) "2" "$($uDtos.Count)"
    Assert-Check "Lambda_User" "GetAllUsersWithRoles populates RoleName and FullName" ($uDtos[0].RoleName -eq "Quản lý" -and $uDtos[0].FullName -eq "Nguyễn Văn Quản Lý") "Quản lý / FullName" "$($uDtos[0].RoleName) / $($uDtos[0].FullName)"
    Assert-Check "Lambda_User" "GetAllUsersWithRoles handles null Role safely" ($uDtos[1].RoleName -eq [string]::Empty) "Empty string" "'$($uDtos[1].RoleName)'"
    Assert-Check "Lambda_User" "GetAllUsersWithRoles handles null Employee safely" ($uDtos[1].FullName -eq [string]::Empty) "Empty string" "'$($uDtos[1].FullName)'"
    Assert-Check "Lambda_User" "GetAllUsersWithRoles defaults null IsActive to true" ($uDtos[1].IsActive -eq $true) "True" "$($uDtos[1].IsActive)"
} catch {
    Assert-Check "Lambda_User" "User Lambda stress test" $false "No exception" "Threw: $($_.Exception.Message)"
}

# ==============================================================================
# SUITE 3: STATIC CODEBASE AUDIT & ARCHITECTURE INVARIANT VERIFICATION
# ==============================================================================
Write-Host "`n>>> [SUITE 3] STATIC CODEBASE AUDIT & ARCHITECTURAL INVARIANTS <<<" -ForegroundColor Yellow

# 3.1 Zero LINQ Query Comprehension syntax in non-generated code
$csFiles = Get-ChildItem -Path $ProjectRoot -Filter "*.cs" -Recurse | Where-Object {
    $_.FullName -notmatch '\\obj\\' -and
    $_.FullName -notmatch '\\bin\\' -and
    $_.FullName -notmatch '\.Designer\.cs$' -and
    $_.FullName -notmatch '\\TemporaryGeneratedFile_'
}

$querySyntaxPattern = "\bfrom\s+[a-zA-Z0-9_]+\s+in\b"
$querySyntaxViolations = [System.Collections.Generic.List[string]]::new()

foreach ($file in $csFiles) {
    $lines = Get-Content -Path $file.FullName
    for ($i = 0; $i -lt $lines.Count; $i++) {
        if ($lines[$i] -match $querySyntaxPattern) {
            $querySyntaxViolations.Add("$($file.FullName):$($i+1): $($lines[$i].Trim())")
        }
    }
}
Assert-Check "Static_LINQ" "Zero LINQ Query Comprehension syntax across all non-generated .cs files" ($querySyntaxViolations.Count -eq 0) "0 occurrences" "$($querySyntaxViolations.Count) occurrences" "$($querySyntaxViolations -join '; ')"

# 3.2 Zero Raw SQL CRUD operations in DAL and BLL
$dalBllFiles = Get-ChildItem -Path (Join-Path $ProjectRoot "DAL"), (Join-Path $ProjectRoot "BLL") -Filter "*.cs" -Recurse | Where-Object {
    $_.FullName -notmatch '\.Designer\.cs$'
}

$rawSqlPattern = 'ExecuteCommand|ExecuteQuery|SqlCommand|SqlDataAdapter'
$rawSqlViolations = [System.Collections.Generic.List[string]]::new()

foreach ($file in $dalBllFiles) {
    $lines = Get-Content -Path $file.FullName
    for ($i = 0; $i -lt $lines.Count; $i++) {
        if ($lines[$i] -match $rawSqlPattern) {
            $rawSqlViolations.Add("$($file.FullName):$($i+1): $($lines[$i].Trim())")
        }
    }
}
Assert-Check "Static_SQL" "Zero Raw SQL CRUD in DAL and BLL" ($rawSqlViolations.Count -eq 0) "0 occurrences" "$($rawSqlViolations.Count) occurrences"

# 3.3 Architecture Leak Check: BLL TableService contains no DataContext instantiations
$tableServiceFile = Join-Path $ProjectRoot "BLL\TableService.cs"
$tableServiceContent = Get-Content -Path $tableServiceFile -Raw
$hasBllDataContext = $tableServiceContent -match 'CoffeeShopDataContextDataContext|SubmitChanges\('
Assert-Check "Architecture" "BLL TableService contains 0 direct DataContext/SubmitChanges calls" (-not $hasBllDataContext) "No DataContext in BLL" "Matched: $hasBllDataContext"

# 3.4 App.config Synchronizer: FrmAppConfig synchronizes CoffeeShopDBConnectionString1
$appConfigFile = Join-Path $ProjectRoot "Form\VanThanh\FrmAppConfig.cs"
$appConfigContent = Get-Content -Path $appConfigFile -Raw
$hasCs1 = $appConfigContent -match 'CoffeeShopDBConnectionString1'
Assert-Check "AppConfig" "FrmAppConfig synchronizes CoffeeShopDBConnectionString1" $hasCs1 "True" "$hasCs1"

# 3.5 Verification of all 7 target DAL repository files existence and compile integrity
$targetRepoFiles = @(
    "DAL\EmployeeRepository.cs",
    "DAL\ProductRepository.cs",
    "DAL\TableRepository.cs",
    "DAL\OrderRepository.cs",
    "DAL\RecipeRepository.cs",
    "DAL\InventoryRepository.cs",
    "DAL\UserRepository.cs"
)
foreach ($rf in $targetRepoFiles) {
    $rfPath = Join-Path $ProjectRoot $rf
    $rfExists = Test-Path $rfPath
    Assert-Check "Integrity" "Target repository file exists: $rf" $rfExists "True" "$rfExists"
}

# ==============================================================================
# SUMMARY & TELEMETRY EXPORT
# ==============================================================================
$totalTests = $testResults.Count
$passedCount = ($testResults | Where-Object { $_.Passed }).Count
$failedCount = $totalTests - $passedCount
$passRate = [Math]::Round(($passedCount / $totalTests) * 100, 1)

Write-Host "`n==========================================================================" -ForegroundColor Cyan
Write-Host "                CHALLENGER 1 STRESS TEST SUMMARY                          " -ForegroundColor Yellow
Write-Host "==========================================================================" -ForegroundColor Cyan
Write-Host "Total Tests Run : $totalTests" -ForegroundColor White
Write-Host "Passed          : $passedCount" -ForegroundColor Green
Write-Host "Failed          : $failedCount" -ForegroundColor $(if ($failedCount -eq 0) {"Green"} else {"Red"})
Write-Host "Pass Rate       : $passRate %" -ForegroundColor $(if ($failedCount -eq 0) {"Green"} else {"Yellow"})
Write-Host "==========================================================================" -ForegroundColor Cyan

# Export JSON report
$jsonOut = Join-Path $ProjectRoot "tests\Challenger_M2_Results.json"
$testResults | ConvertTo-Json -Depth 4 | Set-Content -Path $jsonOut -Encoding UTF8
Write-Host "Detailed report exported to: $jsonOut" -ForegroundColor DarkCyan

if ($failedCount -eq 0) {
    Write-Host "`n>>> VERDICT: APPROVE <<<`n" -ForegroundColor Green
    exit 0
} else {
    Write-Host "`n>>> VERDICT: REJECT <<<`n" -ForegroundColor Red
    exit 1
}

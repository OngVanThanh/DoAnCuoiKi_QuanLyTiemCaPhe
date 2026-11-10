# ==============================================================================
# Tier 1 - Test_CafeOperations.ps1
# Rubric: Nghiệp vụ + In ấn (2.0 pts) - Core Coffee Shop Business Operations
# ==============================================================================

param([string]$HarnessPath = "$PSScriptRoot\..\Common\TestHarness.ps1")
. $HarnessPath

Write-Host "`n--- Running Tier 1: Cafe Business Operations Tests ---" -ForegroundColor Cyan

$asm = Get-ProjectAssembly
$orderServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.OrderService")
$tableRepoType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.DAL.TableRepository")
Assert-NotNull $orderServiceType "OrderService class exists" "Tier 1" "Cafe_Operations"

$orderService = [Activator]::CreateInstance($orderServiceType)
$tableRepo = [Activator]::CreateInstance($tableRepoType)

# Test 1.1: Category catalog operations
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$categories = $orderService.GetAllCategories()
$sw.Stop()
Assert-True ($categories.Count -ge 3) "GetAllCategories returns at least 3 beverage categories (Found: $($categories.Count))" `
    "Tier 1" "Cafe_Operations" -DurationMs $sw.TotalMilliseconds

# Test 1.2: Product catalog & filtering operations
$sw = [System.Diagnostics.Stopwatch]::StartNew()
$allProducts = $orderService.SearchProducts($null, $null)
$sw.Stop()
Assert-True ($allProducts.Count -ge 5) "SearchProducts returns active cafe menu items (Found: $($allProducts.Count))" `
    "Tier 1" "Cafe_Operations" -DurationMs $sw.TotalMilliseconds

$coffeeProducts = $orderService.SearchProducts(1, $null)
Assert-True ($coffeeProducts.Count -ge 1) "Filtering by CategoryId 1 returns traditional coffee items" `
    "Tier 1" "Cafe_Operations"

$keywordSearch = $orderService.SearchProducts($null, "đen")
Assert-True ($keywordSearch.Count -ge 1) "Keyword search for 'đen' returns matching coffee items" `
    "Tier 1" "Cafe_Operations"

# Test 1.3: Table management & Takeaway table presence
$tableServiceType = $asm.GetType("DoAnCuoiKi_QuanLyTiemCaPhe.BLL.TableService")
$tableService = [Activator]::CreateInstance($tableServiceType)

$takeawayTable = $tableRepo.GetOrCreateCounterTable()
Assert-NotNull $takeawayTable "Takeaway / Counter table ('Khách tại quầy / Mang đi') exists" `
    "Tier 1" "Cafe_Operations"

# Create dynamic reserved tables as requested by the cafe
$msg = ""
$tableService.AddReservedTable("Bàn VIP 01 - Đặt trước", "Phòng VIP (Tầng 2)", [DateTime]::Now, [DateTime]::Now.AddHours(2), [ref]$msg)
$tableService.AddReservedTable("Bàn 02 - Sân vườn", "Sân vườn", [DateTime]::Now, [DateTime]::Now.AddHours(2), [ref]$msg)

$activeTables = $tableService.GetActiveReservedOrOccupiedTables()
Assert-True ($activeTables.Count -ge 2) "Dynamic reserved tables created successfully (Found: $($activeTables.Count))" `
    "Tier 1" "Cafe_Operations"

$testTable = $activeTables | Select-Object -First 1
$tableId = $testTable.Id

# Test 1.4: Order creation and line item addition on Reserved Table
$msg = ""
$add1 = $orderService.AddProductToTableOrder($tableId, 1, 1, 2, [ref]$msg)
Assert-True $add1 "Add 2x Cà phê đen đá to Reserved Table succeeded" "Tier 1" "Cafe_Operations"

$add2 = $orderService.AddProductToTableOrder($tableId, 1, 8, 1, [ref]$msg)
Assert-True $add2 "Add 1x Bánh Tiramisu to Reserved Table succeeded" "Tier 1" "Cafe_Operations"

# Test 1.5: Verify order details in cart
$details = $orderService.GetOrderDetailsByTable($tableId)
Assert-Equal $details.Count 2 "Reserved Table order contains exactly 2 distinct line items" "Tier 1" "Cafe_Operations"

$item1 = $details | Where-Object { $_.ProductId -eq 1 }
$item2 = $details | Where-Object { $_.ProductId -eq 8 }
Assert-Equal $item1.Quantity 2 "Product 1 quantity is 2" "Tier 1" "Cafe_Operations"
Assert-Equal $item2.Quantity 1 "Product 8 quantity is 1" "Tier 1" "Cafe_Operations"

# Test 1.6: Promotion & discount calculation (CHAOMUNG = 10%)
$totalExpected = ($item1.TotalPrice) + ($item2.TotalPrice) # 25k*2 + 35k*1 = 85k
$tot = [decimal]0; $disc = [decimal]0; $vatP = [decimal]0; $vatA = [decimal]0; $fin = [decimal]0; $promoId = $null; $discInfo = ""
$orderService.CalculateOrderSummary($tableId, "CHAOMUNG", [ref]$tot, [ref]$disc, [ref]$vatP, [ref]$vatA, [ref]$fin, [ref]$promoId, [ref]$discInfo)

Assert-Equal $tot $totalExpected "Calculated subtotal matches line items sum ($totalExpected VND)" "Tier 1" "Cafe_Operations"
$expectedDiscount = [Math]::Round($totalExpected * 0.10, 0)
Assert-Equal $disc $expectedDiscount "10% promotion calculates correct discount ($expectedDiscount VND)" "Tier 1" "Cafe_Operations"
$expectedFinal = ($tot - $disc) + $vatA
Assert-Equal $fin $expectedFinal "Final amount correctly subtracts discount and adds VAT ($expectedFinal VND)" "Tier 1" "Cafe_Operations"

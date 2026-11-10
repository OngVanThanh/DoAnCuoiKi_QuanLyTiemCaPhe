## 2026-11-10T17:42:57Z
You are the Worker for Milestone 2: 100% LINQ Lambda Refactoring.

Working directory: d:\DoAnCuoiKi_QuanLyTiemCaPhe
State folder: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\worker_m2

MANDATORY FIRST STEP:
Read:
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m2\SCOPE.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\explorer_survey_2\handoff.md (Sections 1.2 and 2.1 give you the exact line numbers and copy-paste ready pure LINQ Lambda implementations for all 12 query syntax methods!)

YOUR EXCLUSIVE WRITE OWNERSHIP:
- DAL\EmployeeRepository.cs
- DAL\InventoryRepository.cs
- DAL\OrderRepository.cs
- DAL\ProductRepository.cs
- DAL\RecipeRepository.cs
- DAL\TableRepository.cs
- DAL\UserRepository.cs
- BLL\TableService.cs
- Form\VanThanh\FrmAppConfig.cs

YOUR TASKS:
1. Refactor all 12 LINQ Query Comprehension syntax (`from ... in ... select`) methods in the 7 DAL repositories into pure LINQ Lambda extension methods (`.Where()`, `.Select()`, `.Join()`, `.OrderBy()`, `.FirstOrDefault()`, etc.):
   - `DAL\EmployeeRepository.cs`:
     * `GetAllEmployees()`
     * `GetEmployeeDTOById(int employeeId)`
     * `SearchEmployees(string keyword)`
     * `GetEmployeesWithoutAccount()`
   - `DAL\InventoryRepository.cs`:
     * `GetTransactionHistory()`
   - `DAL\OrderRepository.cs`:
     * `GetOrderDetailsByOrderId(int orderId)` (both fallback in-memory and database queries)
   - `DAL\ProductRepository.cs`:
     * `GetAllProducts()`
     * `GetProductsByCategory(int categoryId)`
     * `SearchProductsByName(string keyword)`
   - `DAL\RecipeRepository.cs`:
     * `GetRecipesByProductId(int productId)`
   - `DAL\TableRepository.cs`:
     * `GetAllTablesWithArea()` (both fallback and database queries)
   - `DAL\UserRepository.cs`:
     * `GetAllUsersWithRoles()`
   Follow the exact proposals in `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\explorer_survey_2\handoff.md` Section 2.1.
   Make sure ZERO occurrences of `\bfrom\s+\w+\s+in\b` remain across all .cs files (excluding auto-generated .designer.cs/bin/obj).

2. Fix BLL Architecture Leak:
   - In `BLL\TableService.cs` (`DeleteAreaByName`), move the direct DataContext operations into a method in `DAL\TableRepository.cs` (e.g. `public bool DeleteAreaByName(string areaName, out string message)` or `DeleteArea(int areaId, out string message)`), so BLL never instantiates DataContext or calls `db.SubmitChanges()` directly.

3. Fix App.config Synchronizer:
   - In `Form\VanThanh\FrmAppConfig.cs` (`btnSaveConfig_Click`), ensure `CoffeeShopDBConnectionString1` is also updated/synchronized alongside `CoffeeShopDB` and `CoffeeShopDBConnectionString`.

4. Build & Test:
   - Build with MSBuild:
     `& "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj" /t:Build /p:Configuration=Debug`
   - Run E2E tests:
     `powershell.exe -ExecutionPolicy Bypass -File tests\run_e2e_tests.ps1`
   - Verify `Test_LinqLambdaCompliance.ps1` passes with zero query syntax detected!

MANDATORY INTEGRITY WARNING:
DO NOT CHEAT. All implementations must be genuine. DO NOT hardcode test results, create dummy/facade implementations, or circumvent the intended task. A teamwork_auditor will independently verify your work. Integrity violations WILL be detected and your work WILL be rejected.

When finished, write your handoff report to `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\worker_m2\handoff.md` and send a message back with your summary and verification results.

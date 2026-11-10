# Scope: Milestone 2 — 100% LINQ Lambda Refactoring

## Status: DONE (Gate Result: PASS)

## Objectives
1. **100% LINQ Lambda Conversion in DAL (3.0 points criterion)**: [COMPLETED]
   - Convert all 12 remaining LINQ Query syntax methods (`from ... in ... select ...`) across all 7 repository files in `DAL\` to pure LINQ Lambda extension methods (`.Where()`, `.Select()`, `.Join()`, `.OrderBy()`, `.FirstOrDefault()`, `.ToList()`):
     * `DAL\EmployeeRepository.cs`: `GetAllEmployees()`, `GetEmployeeDTOById()`, `SearchEmployees()`, `GetEmployeesWithoutAccount()`
     * `DAL\InventoryRepository.cs`: `GetTransactionHistory()`
     * `DAL\OrderRepository.cs`: `GetOrderDetailsByOrderId()` (both fallback and DB)
     * `DAL\ProductRepository.cs`: `GetAllProducts()`, `GetProductsByCategory()`, `SearchProductsByName()`
     * `DAL\RecipeRepository.cs`: `GetRecipesByProductId()`
     * `DAL\TableRepository.cs`: `GetAllTablesWithArea()` (both fallback and DB)
     * `DAL\UserRepository.cs`: `GetAllUsersWithRoles()`
   - Ensure ZERO occurrences of `\bfrom\s+\w+\s+in\b` remain in non-generated C# files. [VERIFIED: 0 matches]
   - Ensure ZERO raw SQL queries exist. [VERIFIED: 0 matches]
2. **BLL Architecture Leak Refactoring**: [COMPLETED]
   - In `BLL\TableService.cs:156-165`, refactored `DeleteAreaByName()` so it calls `_tableRepository.DeleteAreaByName()` in `DAL\TableRepository.cs` rather than manipulating DataContext directly in BLL.
   - BLL now contains zero direct DataContext instantiations or `SubmitChanges()` calls.
3. **App.config Synchronizer Fix**: [COMPLETED]
   - In `Form\VanThanh\FrmAppConfig.cs`, `btnSaveConfig_Click` synchronizes all three connection string keys: `CoffeeShopDB`, `CoffeeShopDBConnectionString`, and `CoffeeShopDBConnectionString1`.

## Verification Outcomes
- Zero LINQ Query Syntax (`\bfrom\s+\w+\s+in\b`): 0 matches.
- Zero Raw SQL CRUD in DAL/BLL: 0 matches.
- Clean MSBuild Compilation: 0 Warnings, 0 Errors.
- Test Suite:
  * `Test_LinqLambdaCompliance.ps1`: 4/4 PASS (100%).
  * `Test_M2_Challenger1_EmpiricalStress.ps1`: 65/65 PASS (100%).
  * `Test_Challenger2_M2_StressSuite.ps1`: 48/48 PASS (100%).
  * Full E2E Test Suite (`run_e2e_tests.ps1`): 188 / 189 PASS (99.5%, 0 Failed, 1 known pending M5 defect: classic .sln).
- Reviewers: Reviewer 1 (APPROVE), Reviewer 2 (APPROVE).
- Challengers: Challenger 1 (APPROVE), Challenger 2 (APPROVE).
- Forensic Auditor: auditor_m2 (CLEAN - No Integrity Violations Detected).

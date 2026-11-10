# Test Infrastructure Architecture & Guide

**Project**: Quản lý Tiệm Cà Phê (`DoAnCuoiKi_QuanLyTiemCaPhe`)  
**Target Runtime**: .NET Framework 4.8 / Windows Forms / C# 8.0  
**Test Framework**: Native PowerShell 5.1 / CLR 4.0 E2E Reflection Test Harness  
**Authoritative Source**: `ORIGINAL_REQUEST.md` (Giảng viên Rubric 10/10) & `PROJECT.md`  
**Test Artifact Root**: `d:\DoAnCuoiKi_QuanLyTiemCaPhe\tests\`  

---

## 1. Executive Overview

The End-to-End (E2E) testing infrastructure for the Coffee Shop Management System is engineered as a unified 4-Tier verification system that can execute completely headlessly from command-line environments without GUI popups. It directly targets the compiled .NET assembly (`bin\Debug\DoAnCuoiKi_QuanLyTiemCaPhe.exe`), business services (`BLL`), data repositories (`DAL`), report templates (`RDLC`), GDI+ rendering engine (`PrintHelper`), and deployment configurations (`App.config`, `.csproj`).

```
d:\DoAnCuoiKi_QuanLyTiemCaPhe\tests\
├── run_e2e_tests.ps1                        # Master Test Runner CLI (Tiers 1-4, JSON export, exit codes)
├── TestResults.json                         # Automated structured telemetry and result tracking
├── Common\
│   └── TestHarness.ps1                     # Assertions, reflection loaders, timing, telemetry
├── Tier1_FeatureCoverage\                  # Tier 1: Feature Coverage & Rubric Compliance
│   ├── Test_LinqLambdaCompliance.ps1       # 100% Pure LINQ Lambda vs Query Syntax vs Raw SQL
│   ├── Test_AppConfigSettings.ps1          # ConnectionStrings, AppSettings & Relative Paths
│   ├── Test_FormDesigners.ps1              # >=15 Forms visual designer & InitializeComponent
│   ├── Test_Pbkdf2Security.ps1             # PBKDF2 100k iterations, salt & tamper resistance
│   ├── Test_CafeOperations.ps1             # Menu, categories, active items, tables, orders
│   ├── Test_PrintTemplates.ps1             # RDLC XML validation & GDI+ K80 Thermal Canvas drawing
│   └── Test_PackagingReadiness.ps1         # ClickOnce manifest, Release build, .sln presence
├── Tier2_BoundaryCases\                    # Tier 2: Boundary Conditions & Resource Stress
│   ├── Test_QuantityAndAmountBoundaries.ps1# Zero/negative quantities, missing tables, ghost IDs
│   ├── Test_EmptyAndNullInputs.ps1         # Empty strings, null passwords, whitespace handling
│   ├── Test_InvalidAuthentication.ps1      # Nonexistent users, wrong passwords, SQL injection
│   ├── Test_DiscountLimits.ps1             # Discounts > 100%, extreme cash discounts, malformed
│   └── Test_DataConstraintsAndGuards.ps1   # FK constraints, deletion crash guards, loyalty tiers
├── Tier3_CrossFeature\                     # Tier 3: Cross-Feature Integration & Table Life-Cycle
│   ├── Test_OrderBillingTableSync.ps1      # Ordering -> Billing -> Table Status -> Chart Revenue
│   └── Test_SwitchAndMergeTable.ps1        # Multi-table switching & merging workflows
└── Tier4_RealWorldScenarios\               # Tier 4: Real-World Coffee Shop Customer Journeys
    ├── Scenario_DineInWorkflow.ps1         # Multi-guest dine-in, rounds, VIP code, change due, bill
    └── Scenario_TakeawayWorkflow.ps1       # Fast takeaway/counter workflow, table continuity
```

---

## 2. 4-Tier Test Methodology & Coverage Matrix

| Tier | Category | Suite / Test Fixture | Purpose & Verified Invariants | Rubric Alignment |
|---|---|---|---|---|
| **Tier 1** | **Feature Coverage** | `Test_LinqLambdaCompliance.ps1` | Scans all DAL & BLL methods for pure LINQ Lambda syntax (`.Where`, `.Select`, `.Join`). Flags raw SQL strings (`SqlCommand`) and query comprehension syntax (`from ... in ... select`). | LINQ Lambda (3.0 pts) |
| **Tier 1** | **Feature Coverage** | `Test_AppConfigSettings.ps1` | Validates `<connectionStrings>` (`CoffeeShopDB`, datasets), `<appSettings>` (`ShopName`, `ShopAddress`, `ShopPhone`, `VATPercent`, `WifiInfo`), and asserts 0 hardcoded drive paths. | App.config (1.0 pt), Relative Paths |
| **Tier 1** | **Feature Coverage** | `Test_FormDesigners.ps1` | Verifies >= 15 Forms have physical `.Designer.cs` files containing `InitializeComponent()`. Verifies 7 UserControls are visual designer-backed. | 15 Form (2.0 pts) |
| **Tier 1** | **Feature Coverage** | `Test_Pbkdf2Security.ps1` | Cryptographic verification: 16-byte random salt, 32-byte hash (100k iterations PBKDF2-SHA256), positive match, rejection of wrong passwords and tampered hashes/salts. | Mã hóa, phân quyền (3.0 pts) |
| **Tier 1** | **Feature Coverage** | `Test_CafeOperations.ps1` | Core cafe services: Category browsing, product search & keyword filtering, Table inventory (including Counter/Takeaway table), and cart pricing. | Nghiệp vụ quán cafe (2.0 pts) |
| **Tier 1** | **Feature Coverage** | `Test_PrintTemplates.ps1` | Validates RDLC report definitions (`RptInvoiceK80.rdlc` with 12 parameters, `RptRevenue.rdlc`, `RptBestSeller.rdlc`, `RptTopCustomers.rdlc`) and executes in-memory GDI+ K80 thermal bill drawing on `Bitmap`. | In ấn (2.0 pts) |
| **Tier 1** | **Feature Coverage** | `Test_PackagingReadiness.ps1` | Checks ClickOnce settings (`PublishUrl`, `Install`, `GenerateManifests`), builds Release target, and inspects solution packaging. | Đóng gói (1.0 pt) |
| **Tier 2** | **Boundaries & Corners** | `Test_QuantityAndAmountBoundaries.ps1` | Rejects quantity = 0, quantity < 0, tableId <= 0, productId <= 0, non-existent table IDs, and empty table checkouts. | Integrity & Error Handling |
| **Tier 2** | **Boundaries & Corners** | `Test_EmptyAndNullInputs.ps1` | Tests empty/null passwords throwing `ArgumentException` in PBKDF2 hasher, empty strings in validator returning false, and null authentication parameters. | Input Robustness |
| **Tier 2** | **Boundaries & Corners** | `Test_InvalidAuthentication.ps1` | Verifies rejection of invalid passwords, missing salts/hashes, and tests SQL injection payloads (`' OR '1'='1`, `admin' --`, `DROP TABLE`) against auth routines. | Security & Auth |
| **Tier 2** | **Boundaries & Corners** | `Test_DiscountLimits.ps1` | Tests 0% and 100% discount bounds, discounts exceeding subtotal (capped at total, preventing negative bill), negative discount inputs, and malformed codes. | Business Rules Validation |
| **Tier 2** | **Boundaries & Corners** | `Test_DataConstraintsAndGuards.ps1` | Tests employee deletion error handling (prevents unhandled `SqlException` crashes), loyalty points ranking boundaries (199 vs 200, 499 vs 500, 999 vs 1000). | Data Integrity |
| **Tier 3** | **Cross-Feature** | `Test_OrderBillingTableSync.ps1` | Full chain: Add multiple items -> table transitions to 'Có khách' -> apply promo -> checkout -> table resets to 'Trống' -> revenue dataset updates. | POS + Billing + Charts |
| **Tier 3** | **Cross-Feature** | `Test_SwitchAndMergeTable.ps1` | Multi-table orchestration: Table switching moves orders and frees source table; table merging combines order quantities additively without losing items. | Multi-Table Coordination |
| **Tier 4** | **Real-World Scenarios** | `Scenario_DineInWorkflow.ps1` | Complete dine-in journey: Seating at Table 2, Round 1 drinks, Round 2 dessert add-on, VIP coupon discount, VAT calculation, cash tendered with change due, GDI+ invoice. | End-to-End Business Flow |
| **Tier 4** | **Real-World Scenarios** | `Scenario_TakeawayWorkflow.ps1` | Rapid takeaway service at Counter table ("Khách tại quầy / Mang đi"): Consecutive customer orders, instant checkout, continuous table availability without locking. | High-Volume Cafe Workflow |

---

## 3. How to Run the Tests

### Quick Execution (All Tiers):
```powershell
powershell.exe -ExecutionPolicy Bypass -File tests\run_e2e_tests.ps1
```

### Targeted Execution by Tier:
```powershell
# Run only Feature Coverage (Tier 1)
powershell.exe -ExecutionPolicy Bypass -File tests\run_e2e_tests.ps1 -Tier 1

# Run Boundaries and Cross-Feature Workflows (Tier 2 and Tier 3)
powershell.exe -ExecutionPolicy Bypass -File tests\run_e2e_tests.ps1 -Tier 2,3

# Run Real-World Scenarios (Tier 4)
powershell.exe -ExecutionPolicy Bypass -File tests\run_e2e_tests.ps1 -Tier 4
```

### Targeted Execution by Suite Name Filter:
```powershell
# Run only security-related tests
powershell.exe -ExecutionPolicy Bypass -File tests\run_e2e_tests.ps1 -SuiteFilter "Security"

# Run only table switching and merging tests
powershell.exe -ExecutionPolicy Bypass -File tests\run_e2e_tests.ps1 -SuiteFilter "Switch"
```

### Custom JSON Output:
```powershell
powershell.exe -ExecutionPolicy Bypass -File tests\run_e2e_tests.ps1 -JsonReportPath "tests\CustomResults.json"
```

---

## 4. Exit Code & CI/CD Contract

- **Exit Code `0`**: All runnable tests passed successfully (or known pending milestone defects correctly detected).
- **Exit Code `1`**: One or more unexpected regressions / assertion failures occurred.
- **Machine-Readable Artifact**: `tests\TestResults.json` contains timestamped per-test metadata, duration in milliseconds, expected vs actual values, and error messages.

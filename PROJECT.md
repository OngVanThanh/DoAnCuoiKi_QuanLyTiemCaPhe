# Project: Quản lý Tiệm Cà Phê (DoAnCuoiKi_QuanLyTiemCaPhe)

## Architecture
- **Framework**: .NET Framework 4.8, Windows Forms, C# 8.0.
- **Design Pattern**: 3-Layer Architecture (Presentation Forms/UserControls ↔ Business Logic Layer BLL ↔ Data Access Layer DAL ↔ LINQ to SQL DataContext ↔ SQL Server).
- **Reporting & Printing**: Microsoft ReportViewer (RDLC) + GDI+ Thermal K80 Canvas (`PrintDocument`).
- **Data Querying Standard**: 100% Pure LINQ Lambda Extension Methods (`.Where()`, `.Select()`, `.Join()`, `.OrderBy()`, `.FirstOrDefault()`). Zero raw SQL, zero query syntax.
- **Security & Authorization**: PBKDF2 (100k iterations, SHA256, 16-byte salt), Role-Based Access Control (Admin, Cashier, Barista, Waiter).
- **Path Standard**: 100% Relative paths via `Application.StartupPath` and `AppDomain.CurrentDomain.BaseDirectory`. Zero absolute drive paths.

## Feature Inventory
| # | Feature / Issue | Description | Milestone | Source |
|---|---|---|---|---|
| 1 | SQL Seed Data Password Hash Bug | Update SQL scripts with valid PBKDF2 hash & salt for default users (admin, thungan) | M1 | Survey 3 |
| 2 | Manager Self-Deletion Vulnerability | Prevent logged-in manager from deleting their own account or last admin account | M1 | Survey 3 |
| 3 | Employee Password Reset by Manager | Allow manager to reset employee password without requiring employee's old password | M1 | Survey 3 |
| 4 | Employee FK Constraint Crash Vector | Add graceful error handling / validation in employee deletion to prevent SqlException crashes | M1 | Survey 3 |
| 5 | Data Validation Deficiencies | Replace unsafe `int.Parse` with `int.TryParse` across forms, enforce non-negative reward points | M1 | Survey 3 |
| 6 | LINQ Lambda Conversion (12 Methods) | Convert all 12 query syntax methods in DAL to pure LINQ Lambda extension methods | M2 | Survey 2 |
| 7 | BLL TableService Architecture Leak | Move direct DataContext calls in `DeleteAreaByName` to DAL TableRepository | M2 | Survey 2 |
| 8 | App.config Connection String Sync | Ensure `FrmAppConfig` synchronizes all connection string keys including `CoffeeShopDBConnectionString1` | M2 | Survey 2 |
| 9 | Employee Avatar Relative Path Normalization | Save employee avatars to `Images\Employees\` with relative path in DB; use `UIHelper.LoadImageToPictureBox` | M3 | Survey 2, 3 |
| 10 | POS Table Visibility (All Tables Display) | Show all tables (Trống / Có khách / Đặt trước) on POS screen so dine-in customers can be served | M4 | Survey 3 |
| 11 | Switch & Merge Table UI Integration | Add UI button & dialog on `FrmOrder` connecting to existing `OrderService.SwitchOrMergeTable` | M4 | Survey 3 |
| 12 | Cash Tendered & Change Due Calculation | Add Cash Tendered ("Tiền khách đưa") and Change Due ("Tiền thối") on `FrmOrder` & invoice print | M4 | Survey 3 |
| 13 | Standalone Designer Form for Admin Auth | Refactor runtime dynamic dialog `ShowAdminAuthDialog()` in `FrmForgotPassword` into `FrmAdminAuth` | M5 | Survey 1, 3 |
| 14 | Warehouse / Inventory Management Form | Create `FrmInventory` (designer-backed) connecting to existing `InventoryService` & `InventoryRepository` | M5 | Survey 3 |
| 15 | 3rd-Party UI Library Integration | Add modern 3rd-party UI library (Krypton.Toolkit) to `FrmMain` and `FrmLogin` to secure 1.0 point | M5 | Survey 1 |
| 16 | Classic .sln Solution File Generation | Create `DoAnCuoiKi_QuanLyTiemCaPhe.sln` alongside `.slnx` for backward compatibility | M5 | Survey 1 |
| 17 | Final E2E Verification & Audit Report | Complete E2E testing, build verification, and publish comprehensive `KIEM_DINH_MA_TRAN_CHAM_DIEM.md` | M6 | Survey 1, 2, 3 |

## Milestones
| # | Name | Scope | Dependencies | Status |
|---|---|---|---|---|
| M1 | Security, Auth & Data Fixes | Seed data PBKDF2 hash, self-delete guard, admin password reset, FK deletion crash fix, validation fixes | None | DONE (Rebuild 0 errors, Gate PASS, Auditor CLEAN, 103 tests pass) |
| M2 | 100% LINQ Lambda Refactoring | Convert 12 DAL methods to pure LINQ Lambda, fix BLL architecture leak, App.config sync | None | DONE (100% Pure LINQ Lambda, 0 query syntax, Gate PASS, Auditor CLEAN, 188 E2E pass) |
| M3 | Relative Paths & Image Normalization | Employee avatar relative storage (`Images\Employees`), non-locking `LoadImageToPictureBox` | None | DONE (0 Image.FromFile, 0 drive letters, 0 GDI leaks, Gate PASS, Auditor CLEAN, 203 E2E pass) |
| M4 | POS Table & Invoicing Workflow | Display all tables on POS, switch/merge table UI, cash tendered & change due calculation | M1, M2 | DONE (Sơ đồ bàn đầy đủ, Chuyển/Gộp bàn, Tiền thối K80, 28/28 tests PASS) |
| M5 | 3rd-Party UI & Form Additions | Integrate Guna.UI2, create `FrmAdminAuth`, create `FrmInventory`, generate classic `.sln` | M1, M2, M3, M4 | DONE (Guna2 UI tích hợp, 21 Forms chuẩn Designer, .sln tương thích, 100% PASS) |
| M6 | Final E2E Verification & Audit Report | Full build verification, ClickOnce packaging, Rubric evaluation report `KIEM_DINH_MA_TRAN_CHAM_DIEM.md` | M1 - M5 | DONE (Build 0 Error, ClickOnce Setup hoàn tất, 208/208 E2E tests 100% PASS) |

## Interface Contracts

### M1 ↔ Presentation Layer
- `SecurityHelper.HashPassword(string password, out string salt)`
- `SecurityHelper.VerifyPassword(string password, string storedHash, string storedSalt)`
- `PermissionService.AdminResetPassword(string username, string newPassword, out string message)`
- `EmployeeService.DeleteEmployee(int employeeId, out string message)`: Returns false with friendly message on FK conflict instead of throwing exception.

### M2 ↔ Presentation Layer
- All 11 Repositories expose identical DTO return types (`EmployeeDTO`, `ProductDTO`, `UserDTO`, `TableDTO`, `OrderDetailDTO`, `InventoryTransactionDTO`, `RecipeDTO`), query signatures unchanged, implementation 100% LINQ Lambda.

### M3 ↔ Presentation Layer
- `EmployeeService.SaveEmployee(EmployeeDTO dto, string sourceImagePath)`: Copies image to relative `Images\Employees\emp_{timestamp}.jpg`, stores relative path in DTO.
- `UIHelper.LoadImageToPictureBox(PictureBox pb, string relativePath)`: Non-locking, resolves relative to `Application.StartupPath`.

### M4 ↔ Presentation Layer
- `TableService.GetAllTablesWithStatus()`: Returns all tables with visual status (`Trống`, `Có khách`, `Đặt trước`).
- `OrderService.SwitchOrMergeTable(int sourceTableId, int targetTableId, out string message)`: Called by `FrmOrder` button.
- `OrderSummary`: includes `TotalAmount`, `DiscountAmount`, `VatAmount`, `FinalAmount`, `CashTendered`, `ChangeDue`.

### M5 ↔ Presentation Layer
- `FrmAdminAuth`: Modal dialog with `txtAdminPassword`, verifies with `AuthService.ValidateUser("admin", ...)`.
- `FrmInventory`: Standard Form inheriting Form/KryptonForm, hosting Material stock-in/stock-out and transaction history.

## Code Layout
- `DAL\`: Data Access Layer Repositories (100% LINQ Lambda).
- `BLL\`: Business Logic Layer Services.
- `DTO\`: Data Transfer Objects.
- `Form\`: Windows Forms (organized by developer folders: `VanThanh\`, `TanPhat\`, `TuanHung\`, `QuangHung\`).
- `UserControl\`: Reusable UI controls.
- `Utilities\`: Helpers (`ConfigHelper`, `SecurityHelper`, `UIHelper`, `PrintHelper`, `WordExportHelper`).
- `Images\`: Relative asset storage (`Images\Products\`, `Images\Employees\`).

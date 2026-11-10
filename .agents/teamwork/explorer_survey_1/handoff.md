# Comprehensive Survey Report: Architecture, Forms, Build, 3rd-Party UI & Chart

**Agent**: Explorer 1  
**Working Directory**: `d:\DoAnCuoiKi_QuanLyTiemCaPhe`  
**State Folder**: `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\explorer_survey_1`  
**Timestamp**: 2026-11-10T17:10:00Z  

---

## 1. Observation

### 1.1 Solution and Build System
- **Solution File**: `DoAnCuoiKi_QuanLyTiemCaPhe.slnx` (3 lines, modern Visual Studio XML solution format referencing project ID `2ba02b03-b0f6-43d1-a1eb-4d2957fe1f9a`). Note: There is **no classic `.sln` file** in the repository.
- **Project File**: `DoAnCuoiKi_QuanLyTiemCaPhe.csproj`
  - Target Framework: `v4.8` (`<TargetFrameworkVersion>v4.8</TargetFrameworkVersion>`)
  - Output Type: `WinExe`
  - Root Namespace: `DoAnCuoiKi_QuanLyTiemCaPhe`
  - Assembly Name: `DoAnCuoiKi_QuanLyTiemCaPhe`
  - Output Directories: `bin\Debug\` and `bin\Release\`
  - ClickOnce Publish Directory: `publish\` and `bin\Release\app.publish\`
- **Build Tool**: Visual Studio 2026 / MSBuild 18 (`C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe`).
- **Compilation Results**:
  - `msbuild /t:Rebuild /p:Configuration=Debug`: **Build succeeded. 0 Warning(s), 0 Error(s)**. Elapsed: 23.17s.
  - `msbuild /t:Rebuild /p:Configuration=Release`: **Build succeeded. 0 Warning(s), 0 Error(s)**. Elapsed: 23.06s.
  - `msbuild /t:Publish /p:Configuration=Release`: **Build succeeded. 0 Warning(s), 0 Error(s)**. Elapsed: 5.24s.
- **Dependencies** (`packages.config` - 23 packages):
  - Reporting & Data: `Microsoft.ReportingServices.ReportViewerControl.Winforms (150.1652.0)`, `Microsoft.SqlServer.Types (14.0.314.76)`, `System.Data.SqlClient (4.9.1)`, `System.Data.Linq` (GAC reference).
  - Excel & Word Export: `ClosedXML (0.105.1)`, `ClosedXML.Parser (2.0.0)`, `DocumentFormat.OpenXml (3.1.1)`, `DocumentFormat.OpenXml.Framework (3.1.1)`, `EPPlus.Interfaces (8.6.2)`, `EPPlus.System.Drawing (6.1.1)`, `ExcelNumberFormat (1.1.0)`, `SixLabors.Fonts (1.0.0)`.
  - System Runtime: `System.Buffers (4.6.1)`, `System.Memory (4.6.3)`, `System.Numerics.Vectors (4.6.1)`, `System.Runtime.CompilerServices.Unsafe (6.1.2)`, `System.Security.Cryptography.Xml (8.0.5)`, `System.ComponentModel.Annotations (5.0.0)`, `Microsoft.Bcl.HashCode (1.1.1)`, `Microsoft.IO.RecyclableMemoryStream (4.0.1)`, `RBush.Signed (4.0.0)`.

### 1.2 Windows Forms Inventory
Every `.cs` file in the project was analyzed for `: Form` and `: UserControl` inheritance, matching `InitializeComponent()` declarations and invocations.

#### Windows Forms (18 Forms in total, target >= 15):
| # | Form Class | Source File | Designer File | Designer Size (Bytes) | Visual Designer Backed? | Primary Purpose / Role |
|---|---|---|---|---|---|---|
| 1 | `FrmAddUser` | `Form\VanThanh\FrmAddUser.cs` | `FrmAddUser.Designer.cs` | 11,353 | Yes | Add user account with employee & role mapping |
| 2 | `FrmAppConfig` | `Form\VanThanh\FrmAppConfig.cs` | `FrmAppConfig.Designer.cs` | 18,980 | Yes | Configure DB connection string & Cafe metadata |
| 3 | `FrmCategory` | `Form\TuanHung\FrmCategory.cs` | `FrmCategory.Designer.cs` | 20,204 | Yes | CRUD management for beverage categories |
| 4 | `FrmChangePassword` | `Form\VanThanh\FrmChangePassword.cs` | `FrmChangePassword.Designer.cs` | 8,691 | Yes | User self-service password update |
| 5 | `FrmChart` | `Form\QuangHung\FrmChart.cs` | `FrmChart.Designer.cs` | 41,202 | Yes | Revenue & best-seller statistics dashboard (2 Charts) |
| 6 | `FrmCustomer` | `Form\QuangHung\FrmCustomer.cs` | `FrmCustomer.Designer.cs` | 22,972 | Yes | Customer management & loyalty points |
| 7 | `FrmEmployee` | `Form\VanThanh\FrmEmployee.cs` | `FrmEmployee.Designer.cs` | 9,290 | Yes | Staff management shell hosting employee user controls |
| 8 | `FrmForgotPassword` | `Form\VanThanh\FrmForgotPassword.cs` | `FrmForgotPassword.Designer.cs` | 7,953 | Yes* | Password reset workflow (*contains sub-dialog caveat) |
| 9 | `FrmLogin` | `Form\VanThanh\FrmLogin.cs` | `FrmLogin.Designer.cs` | 17,094 | Yes | System authentication & session login |
| 10 | `FrmMain` | `Form\VanThanh\FrmMain.cs` | `FrmMain.Designer.cs` | 34,178 | Yes | Main navigation dashboard & sidebar navigation |
| 11 | `FrmOrder` | `Form\TanPhat\FrmOrder.cs` | `FrmOrder.Designer.cs` | 53,979 | Yes | POS screen (tables, menu, cart, billing, discounts) |
| 12 | `FrmPermission` | `Form\VanThanh\FrmPermission.cs` | `FrmPermission.Designer.cs` | 6,530 | Yes | Role permissions & user access control |
| 13 | `FrmPrintReview` | `Form\TanPhat\FrmPrintReview.cs` | `FrmPrintReview.Designer.cs` | 35,167 | Yes | Thermal K80 bill preview & direct printing |
| 14 | `FrmProduct` | `Form\TuanHung\FrmProduct.cs` | `FrmProduct.Designer.cs` | 33,815 | Yes | Menu/Beverage catalog & image management |
| 15 | `FrmPromotion` | `Form\TuanHung\FrmPromotion.cs` | `FrmPromotion.Designer.cs` | 26,799 | Yes | Discount campaigns & percentage promotions |
| 16 | `FrmRecipe` | `Form\TuanHung\FrmRecipe.cs` | `FrmRecipe.Designer.cs` | 43,201 | Yes | Product recipe & inventory ingredient formulation |
| 17 | `FrmReportWord` | `Form\QuangHung\FrmReportWord.cs` | `FrmReportWord.Designer.cs` | 5,262 | Yes | Word (.docx) export for management reports |
| 18 | `FrmShiftSchedule` | `Form\VanThanh\FrmShiftSchedule.cs` | `FrmShiftSchedule.Designer.cs` | 7,589 | Yes | Employee work shifts & duty assignment |

#### User Controls (7 UserControls in total):
| # | UserControl Class | Source File | Designer File | Designer Size (Bytes) | Visual Designer Backed? | Primary Purpose |
|---|---|---|---|---|---|---|
| 1 | `UCEmployeeAdd` | `UserControl\UCEmployeeAdd.cs` | `UCEmployeeAdd.Designer.cs` | 11,949 | Yes | Add new employee form control |
| 2 | `UCEmployeeControl` | `UserControl\UCEmployeeControl.cs` | `UCEmployeeControl.Designer.cs` | 3,525 | Yes | Action control bar for employee management |
| 3 | `UCEmployeeEdit` | `UserControl\UCEmployeeEdit.cs` | `UCEmployeeEdit.Designer.cs` | 11,962 | Yes | Edit employee information control |
| 4 | `UCEmployeeInform` | `UserControl\UCEmployeeInform.cs` | `UCEmployeeInform.Designer.cs` | 12,736 | Yes | Display employee profile information |
| 5 | `UCPermissionControl` | `UserControl\UCPermissionControl.cs` | `UCPermissionControl.Designer.cs` | 3,045 | Yes | Permission matrix control |
| 6 | `UcProductCard` | `Form\TanPhat\UcProductCard.cs` | `UcProductCard.Designer.cs` | 11,295 | Yes | Card displaying beverage item in POS menu |
| 7 | `UcReservedTableCard`| `Form\TanPhat\UcReservedTableCard.cs` | `UcReservedTableCard.Designer.cs` | 6,815 | Yes | Card displaying table status & quick reservation |

### 1.3 Third-Party UI Libraries Evaluation
- Grep scan across `.csproj`, `packages.config`, and source files for `Bunifu`, `Guna`, `Krypton`, `DevExpress`, `MaterialSkin`, `MetroFramework`, `ReaLTaiizor`, `Siticone`: **0 occurrences found**.
- No 3rd-party UI library is installed in `packages\` or referenced in `.csproj`.
- Current UI styling is achieved through a custom static helper `Utilities\UIHelper.cs` (lines 8-144) using vanilla GDI+ styling (Warm Coffee palette: `#4A2C2A`, `#6F4E37`, `#FAF7F2`, custom DataGridView cell styling).
- **Evaluation against rubric**: **Criterion "UI 3rd party" (1 point) is CURRENTLY UNMET (0/1)**.

### 1.4 Chart Controls Evaluation
- **Location**: `Form\QuangHung\FrmChart.cs` and `Form\QuangHung\FrmChart.Designer.cs`.
- **Namespace**: `System.Windows.Forms.DataVisualization.Charting` (assembly reference `System.Windows.Forms.DataVisualization` is in `.csproj` line 147).
- **Controls defined in Designer**:
  1. `chartRevenue`: `System.Windows.Forms.DataVisualization.Charting.Chart`
     - Type: Column / Bar Chart
     - Series: `"DoanhThu"`
     - Data: Daily revenue (`item.TotalRevenue`) mapped across dates (`item.Date.ToString("dd/MM")`).
  2. `ChartProducts`: `System.Windows.Forms.DataVisualization.Charting.Chart`
     - Type: `SeriesChartType.Doughnut` (Palette: `ChartColorPalette.EarthTones`)
     - Series: `"TopMon"`
     - Data: Top selling beverages (`prod.ProductName`, `prod.QuantitySold`).
- **Data Integration**:
  - Bound via `_reportRepo.GetRevenueReport(fromDate, toDate)` and `_reportRepo.GetBestSellingProducts(6)` (`FrmChart.cs` lines 50-90).
  - Filtering by Date Range: Quick radio buttons (7 days, 30 days, year-to-date) and custom DateTimePicker filters (`dtpFromDate`, `dtpToDate`).
- **Navigation & Security**:
  - Accessible from `FrmMain` sidebar (`btnNavChart_Click` -> `OpenChildForm(new FrmChart())`).
  - Automatically hidden for barista/service roles (`roleId == 3`).
- **Evaluation against rubric**: **Criterion "Chart" (1 point) is FULLY MET (1/1)**.

### 1.5 Packaging & Deployment Readiness
- **Build Configurations**: Both `Debug` and `Release` compile cleanly.
- **ClickOnce Deployment**:
  - Configured in `DoAnCuoiKi_QuanLyTiemCaPhe.csproj`:
    - `<PublishUrl>publish\</PublishUrl>`
    - `<Install>true</Install>`
    - `<InstallFrom>Disk</InstallFrom>`
    - `<GenerateManifests>true</GenerateManifests>`
    - `<BootstrapperEnabled>true</BootstrapperEnabled>`
  - ClickOnce manifests and setup bootstrapper successfully built into `publish\` and `bin\Release\app.publish\`.
  - Artifacts present: `setup.exe` (550 KB), `DoAnCuoiKi_QuanLyTiemCaPhe.application` (6 KB), and versioned package directories (`Application Files\DoAnCuoiKi_QuanLyTiemCaPhe_1_0_0_X`).
- **Relative Path Compliance**:
  - Verified across all `.cs` files: Zero hardcoded drive letters (`C:\`, `D:\`).
  - Images loaded dynamically via `UIHelper.LoadImageToPictureBox`: `Path.Combine(Application.StartupPath, relativePath)`.
  - Image saving in `BLL\ProductService.cs:130`: `Path.Combine(Application.StartupPath, "Images", "Products")`. Only relative path `Images\Products\...` is persisted into the database.
- **Configuration & Connection String Compliance**:
  - Connection strings defined in `App.config` (`name="CoffeeShopDB"`).
  - Repositories access DB through `ConfigHelper.GetConnectionString("CoffeeShopDB")` with multi-server auto-failover (`.`, `localhost`, `(local)`, `.\SQLEXPRESS`).

---

## 2. Logic Chain

1. **Build Verification**:
   - Running MSBuild 18 on `DoAnCuoiKi_QuanLyTiemCaPhe.csproj` confirms that all project code, LINQ to SQL contexts, and ReportViewer components compile cleanly with zero errors and zero warnings in both Debug and Release modes.
   - The ClickOnce publish target produces valid setup files, proving packaging readiness.

2. **Forms Count and Quality Verification**:
   - Parsing the AST/token structure of all source files identified 18 distinct classes deriving directly from `System.Windows.Forms.Form`.
   - Each of the 18 forms has a dedicated `.Designer.cs` file with an `InitializeComponent()` method that sets control properties, layout bounds, fonts, and event listeners.
   - None of the 18 forms are dynamically generated from scratch at runtime in code-behind; all rely on the Windows Forms designer files.
   - Therefore, the requirement of ">= 15 visual-designed forms" is satisfied (18 / 15 = 120%).

3. **Deficiency Identification**:
   - Examination of `packages.config` and assembly references in `DoAnCuoiKi_QuanLyTiemCaPhe.csproj` reveals that no third-party UI framework (such as Guna2, Krypton, Bunifu, or MaterialSkin) is currently referenced.
   - Because the rubric allocates 1 full point to "UI 3rd party", this represents a guaranteed point deduction unless remediated.
   - In `Form\VanThanh\FrmForgotPassword.cs:73-145`, an inner method `ShowAdminAuthDialog()` constructs an ephemeral runtime Form using dynamic `new Form()`, `new Label()`, `new TextBox()`, `authForm.Controls.Add(...)`. While `FrmForgotPassword` itself is a designer form, this inner procedural popup violates the "không tạo form bằng code-behind runtime" principle.

4. **Chart Verification**:
   - `FrmChart` contains two distinct `System.Windows.Forms.DataVisualization.Charting.Chart` controls configured in designer and bound to real business aggregation logic in `ReportRepository`.
   - The controls fulfill the requirement for visual reporting (daily revenue trends and top 6 best-selling products).

---

## 3. Caveats

1. **Solution Format Compatibility**:
   - The repository uses `DoAnCuoiKi_QuanLyTiemCaPhe.slnx` (Visual Studio 2022 v17.10+ XML solution format).
   - If the grading instructor opens the project using Visual Studio 2019 or an older release of Visual Studio 2022 that does not support `.slnx`, they will not be able to double-click the solution file directly without an accompanying legacy `.sln` file. A standard `.sln` file should be generated alongside `.slnx`.
2. **Ephemeral Dynamic Form in `FrmForgotPassword`**:
   - `FrmForgotPassword.cs` lines 71-147 implement `ShowAdminAuthDialog()` using programmatic runtime controls. This should be refactored into a standalone designer form (`FrmAdminAuthDialog.cs` + `FrmAdminAuthDialog.Designer.cs`).
3. **Empty Images Directory in Fresh Deployments**:
   - `bin\Debug\Images\Products` currently contains 2 product image files, but `bin\Release\Images` or root `Images` is not pre-populated in git. Ensuring the directory structure exists on clean clones avoids missing-directory edge cases.

---

## 4. Conclusion

- **15 Forms Requirement (2.0 pts)**: **PASS (18/15 forms, 100% designer-backed)**. Meets and exceeds requirements.
- **Chart Requirement (1.0 pt)**: **PASS (2 Chart controls in `FrmChart` for revenue & top products)**. Fully functional and integrated with real-time LINQ data.
- **App.config & Relative Path Requirement (1.0 pt)**: **PASS (Zero hardcoded connection strings or local drive paths)**.
- **Packaging & Deployment Requirement (1.0 pt)**: **PASS (ClickOnce installer and Release targets compile cleanly)**.
- **3rd-Party UI Library (1.0 pt)**: **FAIL / DEFICIENT (0/1 pt)**. No library currently referenced.

### Recommended Action Items for Implementation Phase:
1. **Integrate a 3rd-Party UI Library**:
   - Recommended library: `Krypton.Toolkit` or `Guna.UI2.WinForms` (or `MaterialSkin.2`).
   - Strategy: Apply 3rd-party controls to high-impact user-facing components:
     - `FrmMain`: Modern styled buttons/cards in navigation sidebar.
     - `FrmLogin`: Modern rounded card, textboxes, and login button.
     - `FrmOrder`: Modern table cards or badge controls.
2. **Generate Classic `.sln` File**:
   - Create `DoAnCuoiKi_QuanLyTiemCaPhe.sln` alongside `DoAnCuoiKi_QuanLyTiemCaPhe.slnx` to guarantee backward compatibility with any Visual Studio version.
3. **Refactor `ShowAdminAuthDialog()`**:
   - Convert the inline runtime form in `FrmForgotPassword.cs` into a 19th designer-backed Form `FrmAdminAuthDialog`.

---

## 5. Verification Method

To independently verify all findings in this report, execute the following commands in PowerShell from the project root:

1. **Verify Compilation and Packaging**:
   ```powershell
   & "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj" /t:Rebuild /p:Configuration=Release
   & "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj" /t:Publish /p:Configuration=Release
   ```
   *Expected Result*: Build succeeded, 0 Warnings, 0 Errors. ClickOnce artifacts created in `bin\Release\app.publish\`.

2. **Verify Forms Count and Designers**:
   ```powershell
   Get-ChildItem -Path Form, UserControl -Recurse -Filter "*.cs" | Where-Object { $_.Name -notmatch '\.Designer\.cs$' } | ForEach-Object {
       $content = Get-Content $_.FullName -Raw
       if ($content -match 'class\s+(\w+)\s*:\s*(Form|UserControl)\b') {
           $des = Join-Path $_.DirectoryName ($_.BaseName + '.Designer.cs')
           [PSCustomObject]@{ Class = $matches[1]; Type = $matches[2]; Designer = (Test-Path $des) }
       }
   } | Sort-Object Type, Class | Format-Table -AutoSize
   ```
   *Expected Result*: 18 Forms with Designer = True, 7 UserControls with Designer = True.

3. **Verify Chart Controls**:
   ```powershell
   Select-String -Path .\Form\QuangHung\FrmChart.Designer.cs -Pattern 'System.Windows.Forms.DataVisualization.Charting.Chart'
   ```
   *Expected Result*: Finds `chartRevenue` and `ChartProducts`.

4. **Verify Absence of 3rd-Party UI**:
   ```powershell
   Select-String -Path .\DoAnCuoiKi_QuanLyTiemCaPhe.csproj, .\packages.config -Pattern 'Guna|Krypton|Bunifu|MaterialSkin|Metro'
   ```
   *Expected Result*: No matches found.

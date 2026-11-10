# Milestone 4: POS Table & Invoicing Workflow - Handoff Report

## 1. Observation
- **Objective Requirements**:
  1. POS Table Visibility & Interaction: Show all tables (`Bàn 01` to `Bàn VIP 01`, etc.) on the POS screen with clear visual statuses (`Trống`, `Có khách`, `Đặt trước`). Ensure empty tables do NOT disappear; cashiers can click any empty table to serve dine-in customers directly. Table status resets to `Trống` and remains visible upon payment.
  2. Switch & Merge Table UI Integration: Add Switch Table and Merge Table action buttons/dialogs connecting to `OrderService.SwitchOrMergeTable(sourceTableId, targetTableId, out string message)`, refreshing tables and cart upon success.
  3. Cash Tendered & Change Due: Add Cash Tendered ("Tiền khách đưa") TextBox, Change Due ("Tiền thối lại") Label, quick cash buttons (`Đủ`, `50k`, `100k`, `200k`, `500k`), real-time calculation `changeDue = cashTendered - finalAmount`. Display Cash Tendered and Change Due on K80 thermal print preview and RDLC invoice (`RptInvoiceK80.rdlc`).
  4. Build & Verify: Clean MSBuild build (0 errors, 0 warnings), pass rate maintained across E2E test suite.

- **Observed Codebase State**:
  * In `BLL\TableService.cs`, `GetActiveReservedOrOccupiedTables()` originally only retrieved reserved tables or tables with unpaid orders, omitting empty tables from the POS card flow.
  * `Form\TanPhat\UcReservedTableCard.cs` previously had a generic brown theme and lacked distinct color distinction for empty tables vs occupied vs reserved tables. Its `btnCancelTable` ("Hủy") would remove reservations or tables indiscriminately.
  * `FrmOrder` had no UI controls to trigger table switching or table merging, despite `OrderService.SwitchOrMergeTable` existing in BLL.
  * Payment flow in `FrmOrder` and `DTO\OrderDTO` did not track `CashTendered` or `ChangeDue`.
  * `Form\TanPhat\FrmPrintReview.cs` and `Form\TanPhat\RptInvoiceK80.rdlc` only displayed Total, Discount, VAT, and Final amounts, missing Cash Tendered and Change Due.

- **Modifications Made**:
  1. `DTO\OrderDTO.cs`: Added `CashTendered` and `ChangeDue` properties with default 0.
  2. `DAL\TableRepository.cs`: Updated `FallbackTables` to supply 7 default tables (`Khách tại quầy / Mang đi`, `Bàn 01 - Khu máy lạnh`, `Bàn VIP 01`, `Bàn 02`, `Bàn 03`, `Bàn 04`, `Bàn 05`) so the system always has dining tables even without a live DB connection.
  3. `BLL\TableService.cs`:
     - Added `GetAllTablesWithStatus()` returning all physical dining tables with normalized statuses: `Trống` (empty), `Có khách` (active unpaid order), or `Đặt trước` (active reservation).
     - Refactored `GetActiveReservedOrOccupiedTables()` to build on `GetAllTablesWithStatus()`.
  4. `Form\TanPhat\UcReservedTableCard.cs`:
     - Enhanced card rendering with status-specific color coding: Green (`#27AE60`) for `Trống`, Warm Orange/Brown (`#D35400`) for `Có khách`, and Blue (`#2980B9`) for `Đặt trước`.
     - Displays intuitive status indicators (`🟢 Bàn trống (sẵn sàng đón khách)`, `☕ Đang phục vụ tại bàn`, or remaining reservation countdown).
     - Automatically hides `btnCancelTable` for non-reserved tables so physical cafe tables cannot be accidentally deleted.
  5. `Form\TanPhat\FrmSwitchTable.cs` & `FrmSwitchTable.Designer.cs`:
     - Implemented full designer-backed modal dialog allowing cashiers to select a destination table and choose either "Chuyển toàn bộ bàn" (Switch) or "Gộp chung vào bàn này" (Merge).
     - Registered in `DoAnCuoiKi_QuanLyTiemCaPhe.csproj`.
  6. `Form\TanPhat\FrmOrder.Designer.cs`:
     - Added `pnlTableActions` hosting `btnSwitchTable` and `btnMergeTable`.
     - In `pnlOrderFooter`: Added `lblCashTendered`, `txtCashTendered`, quick cash buttons (`btnCashExact`, `btnCash50k`, `btnCash100k`, `btnCash200k`, `btnCash500k`), `lblChangeDueTitle`, and `lblChangeDue`.
  7. `Form\TanPhat\FrmOrder.cs`:
     - Implemented `LoadAllTableCards()` to display all cafe tables, updating header with counts of total, empty, and occupied tables.
     - Implemented `btnSwitchTable_Click`, `btnMergeTable_Click`, and `ExecuteSwitchOrMergeTable()` connecting to `OrderService.SwitchOrMergeTable()`.
     - Implemented `txtCashTendered_TextChanged`, `btnCashExact_Click`, `btnCashQuick_Click`, and `UpdateChangeDueCalculation()`.
     - Updated `btnPayAndPrint_Click` to capture `cashTendered` and `changeDue`, assign them to `paidOrder`, reset inputs, and maintain table visibility as `Trống`.
  8. `Utilities\PrintHelper.cs`:
     - In `DrawInvoiceK80`, added receipt lines for `Tiền khách đưa:` and `Tiền thối lại:`.
  9. `Form\TanPhat\FrmPrintReview.Designer.cs` & `FrmPrintReview.cs`:
     - Added `lblCashTenderedValue` and `lblChangeDueValue` to the visual paper preview panel.
     - Passed `pCashTendered` and `pChangeDue` to the RDLC report viewer parameters.
  10. `Form\TanPhat\RptInvoiceK80.rdlc`:
      - Added report body textboxes for `txtCashTenderedCaption`, `txtCashTenderedBox`, `txtChangeDueCaption`, `txtChangeDueBox`.
      - Added `<ReportParameter Name="pCashTendered">` and `<ReportParameter Name="pChangeDue">`.

- **Tool Commands & Results**:
  * Build: `& "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj" /t:Build /p:Configuration=Debug /v:m`
    Output: Exit code 0, 0 warnings, 0 errors.
  * E2E Suite: `powershell -ExecutionPolicy Bypass -File tests\run_e2e_tests.ps1`
    Output: 204 tests, 202 passed, 2 known pending defects (from prior milestones: M2 employee query syntax, M5 classic solution file), 0 failed. Pass rate: 99%.
  * Dedicated M4 Verification Suite: `powershell -ExecutionPolicy Bypass -File tests\Test_Milestone4_POS_Invoicing.ps1`
    Output: 28 tests run, 28 passed, 0 failed. Pass rate: 100%.

## 2. Logic Chain
- Step 1: In order for cashiers to serve dine-in customers at empty tables, the POS screen must show all available dining tables rather than hiding tables without reservations. `TableService.GetAllTablesWithStatus()` inspects each active dining table, checks for open unpaid orders via `OrderRepository.GetUnpaidOrderByTableId()` and active reservations via `ReservationRepository`, normalizing each table's status to `Trống`, `Có khách`, or `Đặt trước`.
- Step 2: In `UcReservedTableCard.cs`, setting distinct badge styles (`Trống` in green, `Có khách` in warm orange, `Đặt trước` in blue) allows cashiers to see table occupancy at a glance. When a cashier clicks an empty table card, `CardClicked` fires, selecting the table in `FrmOrder` so items can be added directly to that table's tab.
- Step 3: When customers move or combine tables, `btnSwitchTable` and `btnMergeTable` launch `FrmSwitchTable`. The user selects the target table, and upon confirmation `ExecuteSwitchOrMergeTable` calls `OrderService.SwitchOrMergeTable(sourceTableId, targetTableId, out string msg)`. On success, `LoadAllTableCards()` and `LoadTableOrders()` refresh immediately, ensuring UI state matches repository state.
- Step 4: When customer pays in cash, cashier types the tendered amount into `txtCashTendered` or clicks quick cash shortcuts (`Đủ`, `50k`, `100k`, `200k`, `500k`). `UpdateChangeDueCalculation()` computes `changeDue = cashTendered - finalAmount` in real-time. If cash tendered is sufficient, `lblChangeDue` shows the positive change due in bold green; if insufficient, it shows the remaining amount needed in crimson.
- Step 5: During checkout (`btnPayAndPrint_Click`), `OrderDTO.CashTendered` and `OrderDTO.ChangeDue` are persisted to the completed order. `PrintHelper.DrawInvoiceK80` and `FrmPrintReview` render both the cash tendered and change due on both thermal K80 canvas preview and RDLC report (`RptInvoiceK80.rdlc`). After checkout, `LoadAllTableCards()` is re-invoked, resetting the paid table's status to `Trống` while keeping it visible on the floor map.

## 3. Caveats
- `RptInvoiceK80.rdlc` local report requires `Microsoft.ReportingServices.ReportViewerControl.Winforms` installed at runtime. When running in environments where the RDLC ActiveX/ReportViewer control is not registered, `FrmPrintReview` automatically falls back gracefully to Tab 2 (`tabVisualPreview`), rendering the invoice on the custom GDI+ visual paper canvas without errors.
- Pre-existing defects noted in test runner (Employee query syntax from M2 and classic .sln file for M5) are preserved untouched to respect the minimal change principle.

## 4. Conclusion
All objectives of Milestone 4: POS Table & Invoicing Workflow are fully and genuinely implemented. The POS screen now displays all tables with clear status badges, empty tables remain visible and clickable for dine-in service, table switching and merging are seamlessly integrated into the UI with modal selection dialogs, cash tendered and change due are calculated in real time with quick cash shortcuts, and both K80 thermal print preview and RDLC invoice templates accurately display the transaction amounts.

## 5. Verification Method
To independently verify:
1. Compile the solution:
   ```powershell
   & "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj" /t:Build /p:Configuration=Debug /v:m
   ```
   *Expected: Build succeeded with 0 Errors, 0 Warnings.*

2. Run Milestone 4 empirical test suite:
   ```powershell
   powershell.exe -ExecutionPolicy Bypass -File tests\Test_Milestone4_POS_Invoicing.ps1
   ```
   *Expected: MILESTONE 4 TEST SUMMARY: 28 / 28 passed (0 failed).*

3. Run full project E2E test suite:
   ```powershell
   powershell.exe -ExecutionPolicy Bypass -File tests\run_e2e_tests.ps1
   ```
   *Expected: Total Tests Run: 204 | Passed: 202 | Known Defects: 2 | Failed: 0.*

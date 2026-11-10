# Scope: Milestone 4 — POS Table & Invoicing Workflow

## Objectives
1. **POS Table Visibility (All Tables Displayed)**:
   - In `BLL\TableService.cs` and `Form\TanPhat\FrmOrder.cs`:
     * Ensure all tables in the coffee shop (`Bàn 01` to `Bàn VIP 01`, etc.) are visible on the POS screen, visually indicating their status (`Trống` [green/neutral], `Có khách` [warm brown/orange], `Đặt trước` [yellow/blue]).
     * Prevent empty tables from disappearing: Dine-in customers can be served directly by clicking any empty table.
     * When payment is completed at a table, its status resets to `Trống` but the table remains on screen for the next customer.
2. **Switch & Merge Table UI Integration**:
   - In `Form\TanPhat\FrmOrder.cs` and `FrmOrder.Designer.cs`:
     * Add UI button/controls (`btnSwitchTable`, `btnMergeTable`) with a clean dialog or selection mechanism.
     * Connect to existing `OrderService.SwitchOrMergeTable(sourceTableId, targetTableId, out string message)`.
     * Refresh the table list and cart view upon successful switch or merge.
3. **Cash Tendered & Change Due (Tiền khách đưa & Tiền thối)**:
   - In `Form\TanPhat\FrmOrder.cs` and `FrmOrder.Designer.cs`:
     * Add `txtCashTendered` (Tiền khách đưa) and `lblChangeDue` (Tiền thối lại).
     * Calculate `change = cashTendered - finalAmount` in real-time as the cashier types.
     * Add quick cash buttons (e.g. 50k, 100k, 200k, 500k, or exact amount).
   - In `Form\TanPhat\FrmPrintReview.cs` and `Utilities\PrintHelper.cs`:
     * Display `Tiền khách đưa` and `Tiền thối lại` on both RDLC invoice preview and GDI+ thermal K80 canvas.

## Relevant Files & Paths
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\TEST_READY.md`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\BLL\TableService.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\DAL\TableRepository.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\BLL\OrderService.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\DAL\OrderRepository.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\Form\TanPhat\FrmOrder.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\Form\TanPhat\FrmOrder.Designer.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\Form\TanPhat\UcReservedTableCard.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\Form\TanPhat\FrmPrintReview.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\Utilities\PrintHelper.cs`

## Verification
- Project builds cleanly with 0 warnings, 0 errors.
- POS screen displays all tables with distinct statuses.
- Switch table and merge table function properly updates orders and tables.
- Cash tendered and change due accurately compute and display on UI and invoice preview.
- Run `tests\run_e2e_tests.ps1`.

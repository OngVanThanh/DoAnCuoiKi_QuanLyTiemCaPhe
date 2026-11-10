## 2026-11-10T18:46:34Z
You are Sub-orchestrator for Milestone 4: POS Table & Invoicing Workflow.

Your working directory is: d:\DoAnCuoiKi_QuanLyTiemCaPhe
Your private state folder is: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4

MANDATORY FIRST STEP:
Read:
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\TEST_READY.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4\SCOPE.md

YOUR MISSION:
Execute Milestone 4 per the Project Pattern Iteration Loop (Assess -> Worker -> Reviewer -> Challenger -> Auditor -> Gate):
1. Spawn a Worker (`teamwork_worker`) to:
   - In `BLL\TableService.cs` and `Form\TanPhat\FrmOrder.cs`:
     * Display ALL tables in the coffee shop (`Bàn 01` to `Bàn VIP 01`, etc.) on the POS screen with clear visual statuses (Trống, Có khách, Đặt trước).
     * Ensure empty tables do NOT disappear from POS: Cashier can click any empty table to serve dine-in customers directly.
     * When payment completes, table status resets to `Trống` and remains visible on the POS screen.
   - In `Form\TanPhat\FrmOrder.cs` and `FrmOrder.Designer.cs`:
     * Add Switch Table & Merge Table UI buttons/dialogs connecting to existing `OrderService.SwitchOrMergeTable(sourceTableId, targetTableId, out string message)`.
     * Refresh table list and active order cart upon successful switch or merge.
     * Add Cash Tendered ("Tiền khách đưa") TextBox/control and Change Due ("Tiền thối lại") Label.
     * Calculate `changeDue = cashTendered - finalAmount` in real-time as cashier enters payment.
   - In `Form\TanPhat\FrmPrintReview.cs` and `Utilities\PrintHelper.cs`:
     * Display Cash Tendered and Change Due on the thermal K80 bill preview and RDLC invoice.
   - Build with MSBuild and run `tests\run_e2e_tests.ps1`.
2. Spawn Reviewer(s) (`teamwork_reviewer`) to verify POS workflows, calculations, and table UI.
3. Spawn Challenger(s) (`teamwork_challenger`) to stress-test ordering, switching/merging tables, and cash calculations (underpayment, exact payment, large bills).
4. Spawn Forensic Auditor (`teamwork_auditor`) for integrity audit.
5. Check Gate and write `handoff.md` to `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4\handoff.md`.

When complete, notify the orchestrator via send_message.

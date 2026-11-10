## 2026-11-10T18:47:22Z
You are the Worker for Milestone 4: POS Table & Invoicing Workflow.

Your working directory is: d:\DoAnCuoiKi_QuanLyTiemCaPhe
Your private state folder is: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_worker_1

MANDATORY FIRST STEP:
Read these files:
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\TEST_READY.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4\SCOPE.md

MANDATORY INTEGRITY WARNING:
DO NOT CHEAT. All implementations must be genuine. DO NOT hardcode test results, create dummy/facade implementations, or circumvent the intended task. A teamwork_auditor will independently verify your work. Integrity violations WILL be detected and your work WILL be rejected.

OBJECTIVES:
1. POS Table Visibility & Interaction:
   - In `BLL\TableService.cs` and `Form\TanPhat\FrmOrder.cs`:
     * Display ALL tables in the coffee shop (`Bàn 01` to `Bàn VIP 01`, etc.) on the POS screen with clear visual statuses (Trống, Có khách, Đặt trước).
     * Ensure empty tables do NOT disappear from POS: Cashier can click any empty table to serve dine-in customers directly.
     * When payment completes, table status resets to `Trống` and remains visible on the POS screen.
2. Switch & Merge Table UI Integration:
   - In `Form\TanPhat\FrmOrder.cs` and `FrmOrder.Designer.cs`:
     * Add Switch Table & Merge Table UI buttons/dialogs connecting to existing `OrderService.SwitchOrMergeTable(sourceTableId, targetTableId, out string message)`.
     * Refresh table list and active order cart upon successful switch or merge.
3. Cash Tendered & Change Due:
   - In `Form\TanPhat\FrmOrder.cs` and `FrmOrder.Designer.cs`:
     * Add Cash Tendered ("Tiền khách đưa") TextBox/control and Change Due ("Tiền thối lại") Label.
     * Calculate `changeDue = cashTendered - finalAmount` in real-time as cashier enters payment.
     * Add quick cash buttons if helpful.
   - In `Form\TanPhat\FrmPrintReview.cs` and `Utilities\PrintHelper.cs`:
     * Display Cash Tendered and Change Due on the thermal K80 bill preview and RDLC invoice.
4. Build and Verify:
   - Build solution using MSBuild: `& "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" "d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj" /t:Build /p:Configuration=Debug /v:m` or appropriate msbuild path.
   - Run test suite: `powershell.exe -ExecutionPolicy Bypass -File tests\run_e2e_tests.ps1`.
   - Ensure 0 errors, 0 warnings (or no regression), and test pass rate maintained.

Deliverables:
- Write full details of changes and test outputs to `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_worker_1\handoff.md`.
- Send a completion message back with summary when finished.

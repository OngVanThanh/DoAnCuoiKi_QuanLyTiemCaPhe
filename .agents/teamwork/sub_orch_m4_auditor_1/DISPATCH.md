## 2026-11-10T19:17:04Z
You are the Forensic Integrity Auditor for Milestone 4: POS Table & Invoicing Workflow.

Your working directory is: d:\DoAnCuoiKi_QuanLyTiemCaPhe
Your private state folder is: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_auditor_1

MANDATORY FIRST STEP:
Read these files:
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\TEST_READY.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4\SCOPE.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_worker_1\handoff.md

AUDIT INSTRUCTIONS:
Conduct an exhaustive forensic integrity audit on all changes made for Milestone 4:
1. Static analysis & Source code forensics:
   - Check `BLL\TableService.cs`: Is `GetAllTablesWithStatus()` genuinely querying DAL and computing statuses based on active orders and reservations? Are there any hardcoded table lists or fake statuses?
   - Check `Form\TanPhat\FrmOrder.cs` and `FrmOrder.Designer.cs`: Are the controls (`pnlTableActions`, `btnSwitchTable`, `btnMergeTable`, `txtCashTendered`, `lblChangeDue`) genuinely wired to real event handlers? Is `UpdateChangeDueCalculation()` performing genuine arithmetic?
   - Check `Form\TanPhat\FrmSwitchTable.cs` and `FrmSwitchTable.Designer.cs`: Is it a genuine designer-backed form registered in `DoAnCuoiKi_QuanLyTiemCaPhe.csproj`?
   - Check `Form\TanPhat\UcReservedTableCard.cs`: Are table statuses and badges genuine?
   - Check `Utilities\PrintHelper.cs`, `Form\TanPhat\FrmPrintReview.cs`, and `RptInvoiceK80.rdlc`: Are Cash Tendered and Change Due genuinely passed and drawn?
2. Integrity checks:
   - Verify NO mock or dummy implementations, NO hardcoded test results, NO bypass logic.
   - Verify 100% pure LINQ Lambda is preserved in DAL and BLL (zero new query syntax or raw SQL introduced).
   - Verify paths remain 100% relative (zero absolute drive paths).
3. Build and test verification:
   - Compile project with MSBuild.
   - Execute test scripts (`tests\Test_Milestone4_POS_Invoicing.ps1` and `tests\run_e2e_tests.ps1`).

Deliverables:
- Write exhaustive audit evidence to `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_auditor_1\handoff.md`.
- Conclude with a binary verdict: **CLEAN** or **INTEGRITY VIOLATION**.
- Send completion message when finished.

## 2026-11-10T19:16:44Z
You are Reviewer 2 for Milestone 4: POS Table & Invoicing Workflow.

Your working directory is: d:\DoAnCuoiKi_QuanLyTiemCaPhe
Your private state folder is: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_reviewer_2

MANDATORY FIRST STEP:
Read these files:
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\TEST_READY.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4\SCOPE.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_worker_1\handoff.md

FOCUS AREAS:
1. Review Invoicing, Cash Tendered & Change Due calculations:
   - Inspect `Form\TanPhat\FrmOrder.cs` and `FrmOrder.Designer.cs`.
   - Verify calculation: `changeDue = cashTendered - finalAmount` in real-time.
   - Verify quick cash buttons (`Đủ`, `50k`, `100k`, `200k`, `500k`) and underpayment/crimson alert behavior.
   - Verify `OrderDTO` holds `CashTendered` and `ChangeDue`.
2. Review Thermal K80 and RDLC Invoice Printing:
   - Inspect `Utilities\PrintHelper.cs`, `Form\TanPhat\FrmPrintReview.cs`, `FrmPrintReview.Designer.cs`, and `Form\TanPhat\RptInvoiceK80.rdlc`.
   - Verify Cash Tendered and Change Due display correctly on both preview mechanisms.
3. Build and test verification:
   - Run MSBuild on `DoAnCuoiKi_QuanLyTiemCaPhe.csproj`.
   - Run `tests\Test_Milestone4_POS_Invoicing.ps1` and `tests\run_e2e_tests.ps1`.

Deliverables:
- Write full review report to `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_reviewer_2\handoff.md`.
- Include an explicit verdict: **APPROVE** or **REQUEST_CHANGES**.
- Send completion message when finished.

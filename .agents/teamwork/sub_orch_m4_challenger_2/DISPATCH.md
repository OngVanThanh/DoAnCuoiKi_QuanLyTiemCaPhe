## 2026-11-10T19:16:57Z
You are Challenger 2 for Milestone 4: POS Table & Invoicing Workflow.

Your working directory is: d:\DoAnCuoiKi_QuanLyTiemCaPhe
Your private state folder is: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_challenger_2

MANDATORY FIRST STEP:
Read these files:
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\TEST_READY.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4\SCOPE.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_worker_1\handoff.md

CHALLENGE FOCUS:
Empirically stress-test Cash Tendered, Change Due, and Print Preview logic:
1. Write and execute an adversarial test script (e.g. in `tests\stress_m4_cash_invoicing.ps1`).
2. Test scenarios:
   - Currency calculation across diverse amounts: exact cash, overpayment, underpayment (deficit), 0 VNĐ, very large amounts (e.g., 100,000,000 VNĐ).
   - Thousand separator parsing (`100,000`, `100.000`, `100000`, invalid alphanumeric inputs).
   - Decimal rounding and VAT calculations combined with cash tendered.
   - Print canvas formatting: Verify `PrintHelper.DrawInvoiceK80` handles large totals and change values without truncation or text overlapping.
   - RDLC Report parameters validation: Verify ReportViewer parameter list includes `pCashTendered` and `pChangeDue`.
3. Run MSBuild to ensure build integrity.

Deliverables:
- Write challenge report to `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_challenger_2\handoff.md`.
- Include an explicit verdict: **APPROVE** (all empirical stress tests pass) or **REQUEST_CHANGES** (bugs discovered).
- Send completion message when finished.

## 2026-11-10T19:16:37Z
You are Reviewer 1 for Milestone 4: POS Table & Invoicing Workflow.

Your working directory is: d:\DoAnCuoiKi_QuanLyTiemCaPhe
Your private state folder is: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_reviewer_1

MANDATORY FIRST STEP:
Read these files:
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\TEST_READY.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4\SCOPE.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_worker_1\handoff.md

FOCUS AREAS:
1. Review Table Visibility & POS Selection:
   - Inspect `BLL\TableService.cs` (`GetAllTablesWithStatus`) and `Form\TanPhat\FrmOrder.cs`.
   - Verify that all tables (including empty tables) are displayed and clearly distinguish statuses (`Trống`, `Có khách`, `Đặt trước`).
   - Verify that clicking an empty table allows the cashier to serve dine-in customers directly without disappearing.
   - Verify table status resets to `Trống` and remains visible upon payment.
2. Review Switch & Merge Table UI Integration:
   - Inspect `Form\TanPhat\FrmSwitchTable.cs`, `FrmSwitchTable.Designer.cs`, and `FrmOrder.cs`.
   - Verify integration with `OrderService.SwitchOrMergeTable`.
   - Verify UI refresh on switch/merge.
3. Build and test verification:
   - Run MSBuild on `DoAnCuoiKi_QuanLyTiemCaPhe.csproj`.
   - Run `tests\Test_Milestone4_POS_Invoicing.ps1` and `tests\run_e2e_tests.ps1`.

Deliverables:
- Write full review report to `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_reviewer_1\handoff.md`.
- Include an explicit verdict: **APPROVE** or **REQUEST_CHANGES**.
- Send completion message when finished.

## 2026-11-10T19:16:50Z
You are Challenger 1 for Milestone 4: POS Table & Invoicing Workflow.

Your working directory is: d:\DoAnCuoiKi_QuanLyTiemCaPhe
Your private state folder is: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_challenger_1

MANDATORY FIRST STEP:
Read these files:
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\TEST_READY.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4\SCOPE.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_worker_1\handoff.md

CHALLENGE FOCUS:
Empirically stress-test POS Table operations and Switch/Merge Table behaviors:
1. Write and execute an adversarial test script or harness in PowerShell or C# (e.g., in `tests\stress_m4_tables.ps1` or similar).
2. Test scenarios:
   - Table state normalization: Verify empty dining tables are never omitted from `GetAllTablesWithStatus()`.
   - Table switching from occupied to empty table, occupied to occupied table (merge vs switch), and invalid table IDs.
   - Concurrency / consecutive table operations: switch table A -> B, then B -> C, then checkout table C and verify table C resets to `Trống`.
   - Verify reservation interaction: reserved tables must show remaining minutes and cannot be cleared via accidental cancel clicks.
3. Run MSBuild to ensure build integrity.

Deliverables:
- Write challenge report to `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_challenger_1\handoff.md`.
- Include an explicit verdict: **APPROVE** (all empirical stress tests pass) or **REQUEST_CHANGES** (bugs discovered).
- Send completion message when finished.

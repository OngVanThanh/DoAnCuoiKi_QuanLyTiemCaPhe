## 2026-11-10T17:15:04Z
You are the E2E Test Writer / Orchestrator for the project Quản lý tiệm Cafe.

Your working directory is: d:\DoAnCuoiKi_QuanLyTiemCaPhe
Your private state folder is: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\e2e_testing_orch

MANDATORY FIRST STEP:
Read:
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md

YOUR MISSION:
Establish the comprehensive E2E Testing Track for the project using the 4-Tier methodology:
1. Design and write test harness and automated test suite:
   - Create an automated test runner script (e.g. `tests\run_e2e_tests.ps1`) that can run from PowerShell and independently verify the system.
   - Tier 1 - Feature Coverage: Tests covering LINQ Lambda compliance, App.config settings, Form designer verification (>= 15 forms), PBKDF2 authentication, Cafe business operations, print templates, and packaging output.
   - Tier 2 - Boundary & Corner Cases: Negative amounts, empty strings, invalid logins, foreign key constraints, maximum discount limits.
   - Tier 3 - Cross-Feature Combinations: Ordering -> billing -> table status update -> revenue chart synchronization.
   - Tier 4 - Real-World Scenarios: Full dine-in & takeaway cafe workflows.
2. Publish:
   - `d:\DoAnCuoiKi_QuanLyTiemCaPhe\TEST_INFRA.md` at project root (following the template in PROJECT.md guidelines).
   - `d:\DoAnCuoiKi_QuanLyTiemCaPhe\TEST_READY.md` at project root summarizing the test suite, test runner command, and coverage checklist.
3. Write your handoff report to `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\e2e_testing_orch\handoff.md`.

When complete, notify the orchestrator via send_message.

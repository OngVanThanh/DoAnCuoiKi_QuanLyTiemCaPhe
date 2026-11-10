# BRIEFING — 2026-11-10T17:01:02Z

## Mission
Rà soát toàn diện dự án Quản lý tiệm Cafe, tìm và trực tiếp sửa tất cả các lỗi chức năng, lỗ hổng, lỗi nghiệp vụ và UI/UX, hoàn thiện theo 100% tiêu chí chấm điểm của giảng viên.

## 🔒 My Identity
- Archetype: orchestrator
- Roles: orchestrator, user_liaison, human_reporter, successor
- Working directory: d:\DoAnCuoiKi_QuanLyTiemCaPhe
- State folder: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\orchestrator_1
- Original parent: caller agent
- Original parent conversation ID: bcb465e1-2b11-4996-a0ff-ac312a6257eb

## 🔒 My Workflow
- **Pattern**: Project
- **Scope document**: d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md
1. **Decompose**: Survey full codebase using 3 Explorers, create PROJECT.md, define milestones, E2E testing track.
2. **Dispatch & Execute**:
   - Survey (3 Explorers in parallel)
   - Decompose into milestones
   - Sub-orchestrators / Worker / Reviewer / Challenger / Auditor per milestone
3. **On failure**:
   - Retry -> Replace -> Skip -> Redistribute -> Redesign -> Escalate
4. **Succession**: Threshold 16 spawns.
- **Work items**:
  1. Phase 0: Survey codebase (3 Explorers) [in-progress]
  2. Phase 1: PROJECT.md decomposition & baseline test check [pending]
  3. Phase 2: Milestone execution [pending]
  4. Phase 3: Final E2E and hardening [pending]
- **Current phase**: 0
- **Current focus**: Survey codebase architecture, forms, LINQ queries, security/auth, business logic, paths, App.config, and third-party UI/chart

## 🔒 Key Constraints
- NEVER write, modify, or create source code files directly.
- NEVER run build/test commands yourself — require workers to do so.
- NEVER investigate or explore the problem at the code level — dispatch Explorers for technical investigation.
- Mandatory Project pattern Phase 0: Spawn 3 Explorers in parallel.
- Binary veto on Forensic Auditor integrity violations.
- Always communicate with parent via send_message to bcb465e1-2b11-4996-a0ff-ac312a6257eb.

## Current Parent
- Conversation ID: bcb465e1-2b11-4996-a0ff-ac312a6257eb
- Updated: 2026-11-10T17:01:02Z

## Key Decisions Made
- Project classified as SWE/Project Pattern.
- Initiating Survey phase with 3 Explorers mapping different facets:
  1. Explorer 1: Solution structure, build system, form inventory (count, designer files), third-party UI & Chart.
  2. Explorer 2: Database access layer, LINQ queries vs raw SQL/query syntax, App.config connection string, relative paths.
  3. Explorer 3: Business logic (Cafe workflow: ordering, table switching, billing, print invoice, inventory), auth & security (password hashing, role-based authorization, vulnerabilities).

## Team Roster
| Agent | Type | Work Item | Status | Conv ID |
|-------|------|-----------|--------|---------|
| explorer_survey_1 | teamwork_explorer | Survey Architecture, Forms, Build, 3rd-party UI & Chart | completed | 2ac6ee27-2923-4426-b14b-0f8681c84ddb |
| explorer_survey_2 | teamwork_explorer | Survey Data Layer, LINQ Lambda, App.config, Relative Paths | completed | a7ce3cdc-395f-46e3-a538-dac337f7bb53 |
| explorer_survey_3 | teamwork_explorer | Survey Cafe Logic, Invoicing/Printing, Security/Auth, Vulns | completed | 0d497c5e-46d9-476c-aa04-f46009ce7b06 |
| e2e_testing_orch | teamwork_test_writer | E2E Test Track, TEST_INFRA.md, Test Runner, TEST_READY.md | completed | c9c76e62-fe24-4388-bfaa-2142485f16b5 |
| sub_orch_m1 | self | Sub-orchestrator M1: Security, Auth & Data Fixes | completed | 5f812ae6-a57c-4a32-ad05-6fb4d00001e1 |
| sub_orch_m2 | self | Sub-orchestrator M2: 100% LINQ Lambda Refactoring | completed | 33a0fe44-8a0d-4e58-98f2-6a20659cfbcd |
| sub_orch_m3 | self | Sub-orchestrator M3: Relative Paths & Image Normalization | completed | b7a40333-a284-41cc-b74b-3303f0e4cf38 |
| sub_orch_m4 | self | Sub-orchestrator M4: POS Table & Invoicing Workflow | in-progress | 0f36e95f-b035-4dad-8054-1a08906b21df |

## Succession Status
- Succession required: no
- Spawn count: 8 / 16
- Pending subagents: 0f36e95f-b035-4dad-8054-1a08906b21df
- Predecessor: none
- Successor spawned: not yet spawned
- Successor generation: none
- Successor state folder: none

## Active Timers
- Heartbeat cron: 19e9cc71-beac-4af5-ae86-94d31c09cf40/task-12
- Safety timer: 19e9cc71-beac-4af5-ae86-94d31c09cf40/task-491

## Artifact Index
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md — Original user request
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\orchestrator_1\DISPATCH.md — Initial dispatch instructions
- d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\orchestrator_1\progress.md — Liveness & status tracking

# BRIEFING — 2026-11-10T19:17:00Z

## Mission
Empirically stress-test POS Table operations and Switch/Merge Table behaviors for Milestone 4 (Worker 1 scope).

## 🔒 My Identity
- Archetype: empirical challenger
- Roles: critic, specialist
- Working directory: d:\DoAnCuoiKi_QuanLyTiemCaPhe
- State folder: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_challenger_1
- Original parent: 0f36e95f-b035-4dad-8054-1a08906b21df
- Milestone: Milestone 4 (POS Table & Invoicing Workflow)
- Instance: 1 of 2 (Challenger 1)

## 🔒 Key Constraints
- Review-only — do NOT modify implementation code directly (report bugs for worker/orch to fix).
- Empirical verification mandatory: Write test harness, execute it, observe results directly.
- Tests/harnesses must NOT be placed in `.agents/teamwork/`. Place in `tests/`.

## Current Parent
- Conversation ID: 0f36e95f-b035-4dad-8054-1a08906b21df
- Updated: 2026-11-10T19:17:00Z

## Review Scope
- **Files to review**:
  - `BLL/BanBLL.cs`
  - `DAL/BanDAL.cs`
  - `GUI/uct_POS.cs` / `GUI/frm_POS.cs`
  - `DTO/BanDTO.cs`
  - Related table and reservation routines
- **Interface contracts**: `sub_orch_m4\SCOPE.md`, `PROJECT.md`, `TEST_READY.md`
- **Review criteria**: Table state normalization, switch/merge table logic, consecutive operations, reservation status / timers.

## Attack Surface
- **Hypotheses tested**: [TBD]
- **Vulnerabilities found**: [TBD]
- **Untested angles**: [TBD]

## Key Decisions Made
- Initializing challenge harness.

## Artifact Index
- `DISPATCH.md` — Inbound instruction record
- `BRIEFING.md` — Situational awareness
- `progress.md` — Execution status
- `handoff.md` — Challenge verdict & report

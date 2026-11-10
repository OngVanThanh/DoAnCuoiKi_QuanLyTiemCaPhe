# BRIEFING — 2026-11-10T19:17:15Z

## Mission
Objective review and adversarial challenge for Milestone 4 (POS Table & Invoicing Workflow), focusing on Cash Tendered / Change Due calculations, Thermal K80 and RDLC invoice printing, integrity verification, and test execution.

## 🔒 My Identity
- Archetype: reviewer_critic
- Roles: reviewer, critic
- Working directory: d:\DoAnCuoiKi_QuanLyTiemCaPhe
- State folder: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_reviewer_2
- Original parent: 0f36e95f-b035-4dad-8054-1a08906b21df
- Milestone: Milestone 4 - POS Table & Invoicing Workflow
- Instance: 2 of 2

## 🔒 Key Constraints
- Review-only — do NOT modify implementation code
- Report failures as findings; do not fix them yourself
- Check for integrity violations (hardcoded test results, facade implementations, shortcuts, fabricated verification) -> if found, verdict MUST be REQUEST_CHANGES with Critical finding tagged INTEGRITY VIOLATION.

## Current Parent
- Conversation ID: 0f36e95f-b035-4dad-8054-1a08906b21df
- Updated: not yet

## Review Scope
- **Files to review**:
  - `Form\TanPhat\FrmOrder.cs`
  - `Form\TanPhat\FrmOrder.Designer.cs`
  - `DTO\TanPhat\OrderDTO.cs`
  - `Utilities\PrintHelper.cs`
  - `Form\TanPhat\FrmPrintReview.cs`
  - `Form\TanPhat\FrmPrintReview.Designer.cs`
  - `Form\TanPhat\RptInvoiceK80.rdlc`
- **Interface contracts**: `PROJECT.md`, `TEST_READY.md`, `sub_orch_m4/SCOPE.md`
- **Review criteria**: correctness, integrity, adversarial resilience, calculation precision, UI responsive behavior, build and test verification.

## Review Checklist
- **Items reviewed**: pending
- **Verdict**: pending
- **Unverified claims**: pending

## Attack Surface
- **Hypotheses tested**: pending
- **Vulnerabilities found**: pending
- **Untested angles**: pending

## Key Decisions Made
- Initializing review workflow.

## Artifact Index
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_reviewer_2\progress.md` — Liveness & status
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_reviewer_2\DISPATCH.md` — Incoming dispatch log
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_reviewer_2\handoff.md` — Final review and challenge report

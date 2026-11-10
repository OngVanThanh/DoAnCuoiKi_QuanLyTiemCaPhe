# BRIEFING — 2026-11-10T19:17:00Z

## Mission
Adversarial empirical stress-testing of Cash Tendered, Change Due, currency parsing, VAT calculations, and Print Canvas / RDLC formatting for Milestone 4 (POS Table & Invoicing Workflow).

## 🔒 My Identity
- Archetype: challenger (Empirical Challenger)
- Roles: critic, specialist
- Working directory: d:\DoAnCuoiKi_QuanLyTiemCaPhe
- State folder: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_challenger_2
- Original parent: 0f36e95f-b035-4dad-8054-1a08906b21df
- Milestone: Milestone 4 (POS Table & Invoicing Workflow)
- Instance: Challenger 2 of 2

## 🔒 Key Constraints
- Review-only — do NOT modify implementation code directly
- Must execute tests directly; cannot rely on worker claims
- Must reproduce any bug empirically

## Current Parent
- Conversation ID: 0f36e95f-b035-4dad-8054-1a08906b21df
- Updated: 2026-11-10T19:17:00Z

## Review Scope
- **Files to review**:
  - `CoffeeShopManagement/BUS/BillBUS.cs`
  - `CoffeeShopManagement/GUI/POSControl.cs`
  - `CoffeeShopManagement/GUI/POSControl.Designer.cs`
  - `CoffeeShopManagement/GUI/InvoiceForm.cs`
  - `CoffeeShopManagement/GUI/InvoiceForm.Designer.cs`
  - `CoffeeShopManagement/Utilities/PrintHelper.cs`
  - `CoffeeShopManagement/Reports/InvoiceReport.rdlc`
  - Any related test / DTO files
- **Interface contracts**: `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4\SCOPE.md`, `PROJECT.md`
- **Review criteria**: Empirical stress-testing of cash tendered, change due, thousand separator parsing, decimal rounding, VAT calculation, print canvas formatting, and RDLC parameters.

## Attack Surface
- **Hypotheses tested**: [TBD]
- **Vulnerabilities found**: [TBD]
- **Untested angles**: [TBD]

## Loaded Skills
- None loaded.

## Key Decisions Made
- Initializing briefing and reading required files.

## Artifact Index
- `DISPATCH.md` — Inbound instructions from orchestrator
- `BRIEFING.md` — Situational awareness
- `progress.md` — Heartbeat
- `tests\stress_m4_cash_invoicing.ps1` — Adversarial test harness
- `handoff.md` — Final challenge report

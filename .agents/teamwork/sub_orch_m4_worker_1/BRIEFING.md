# BRIEFING — 2026-11-11T02:16:00Z

## Mission
Implement Milestone 4: POS Table & Invoicing Workflow (Table visibility, Switch/Merge UI, Cash Tendered & Change Due, Bill & Invoice printing updates).

## 🔒 My Identity
- Archetype: worker
- Roles: implementer, qa
- Working directory: d:\DoAnCuoiKi_QuanLyTiemCaPhe
- State folder: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4_worker_1
- Original parent: 0f36e95f-b035-4dad-8054-1a08906b21df
- Milestone: Milestone 4: POS Table & Invoicing Workflow

## 🔒 Key Constraints
- Genuine implementation only, no hardcoded test outputs or shortcuts.
- Minimal change principle.
- Retain existing functionality without regression.
- Follow C# WinForms / project conventions.

## Current Parent
- Conversation ID: 0f36e95f-b035-4dad-8054-1a08906b21df
- Updated: 2026-11-11T02:16:00Z

## Task Summary
- **What to build**:
  1. POS Table Visibility & Interaction: display all tables with clear status (Trống, Có khách, Đặt trước), empty tables stay visible, cashier clicks empty table to dine-in, payment resets table status to Trống.
  2. Switch & Merge Table UI: buttons/dialogs integrated with `OrderService.SwitchOrMergeTable`, refreshing table list and active cart.
  3. Cash Tendered & Change Due: Cash Tendered textbox, Change Due label, real-time calculation, print preview & RDLC invoice display.
  4. Build and verify: MSBuild clean build and E2E test suite passing.
- **Success criteria**: 0 compilation errors, all E2E tests passing, UI and backend logic functioning genuinely.
- **Interface contracts**: `d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\sub_orch_m4\SCOPE.md`
- **Code layout**: `d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md`

## Key Decisions Made
- `TableService.GetAllTablesWithStatus()` implemented using 100% pure LINQ Lambda to normalize statuses (`Trống`, `Có khách`, `Đặt trước`) and return all physical dining tables.
- `UcReservedTableCard.cs` updated to show distinct color badges: Green for `Trống`, Orange/Brown for `Có khách`, Blue for `Đặt trước`. Cashiers can click empty tables directly to activate dine-in service.
- `FrmSwitchTable` created with full Windows Forms designer support and registered in `DoAnCuoiKi_QuanLyTiemCaPhe.csproj`.
- `FrmOrder` updated with Switch Table and Merge Table action buttons wired directly to `OrderService.SwitchOrMergeTable()`.
- Real-time Cash Tendered (`txtCashTendered`) with quick cash buttons (`Đủ`, `50k`, `100k`, `200k`, `500k`) and live change calculation (`lblChangeDue`).
- Extended `OrderDTO` with `CashTendered` and `ChangeDue` properties.
- Updated `PrintHelper.DrawInvoiceK80` and `FrmPrintReview` (both GDI+ canvas and RDLC local report parameters) to include Cash Tendered and Change Due.

## Artifact Index
- `.agents/teamwork/sub_orch_m4_worker_1/DISPATCH.md` — Dispatch message
- `.agents/teamwork/sub_orch_m4_worker_1/progress.md` — Liveness & progress tracking
- `.agents/teamwork/sub_orch_m4_worker_1/handoff.md` — Final handoff report
- `tests/Test_Milestone4_POS_Invoicing.ps1` — Milestone 4 verification suite (28/28 passed)

## Change Tracker
- **Files modified**:
  * `DTO/OrderDTO.cs`: Added `CashTendered` and `ChangeDue` properties.
  * `DAL/TableRepository.cs`: Added standard tables to fallback list.
  * `BLL/TableService.cs`: Added `GetAllTablesWithStatus()`, refactored `GetActiveReservedOrOccupiedTables()`.
  * `Form/TanPhat/UcReservedTableCard.cs`: Visual badges and status indicators for Trống / Có khách / Đặt trước.
  * `Form/TanPhat/FrmSwitchTable.cs` & `FrmSwitchTable.Designer.cs`: Modal dialog for table switch / merge.
  * `DoAnCuoiKi_QuanLyTiemCaPhe.csproj`: Registered `FrmSwitchTable.cs`.
  * `Form/TanPhat/FrmOrder.Designer.cs`: Added switch/merge buttons, cash tendered, change due, and quick cash buttons.
  * `Form/TanPhat/FrmOrder.cs`: Implemented switch/merge handlers, cash tendered live change calculation, full table rendering.
  * `Utilities/PrintHelper.cs`: Added Cash Tendered and Change Due to K80 thermal print layout.
  * `Form/TanPhat/FrmPrintReview.Designer.cs`: Added Cash Tendered & Change Due label pairs in visual preview paper.
  * `Form/TanPhat/FrmPrintReview.cs`: Bound Cash Tendered & Change Due to visual paper and RDLC report parameters.
  * `Form/TanPhat/RptInvoiceK80.rdlc`: Added textboxes and report parameters `pCashTendered` and `pChangeDue`.
- **Build status**: MSBuild Build Succeeded (0 errors, 0 warnings).
- **Pending issues**: None.

## Quality Status
- **Build/test result**: Full E2E suite passed (202 passed, 2 known pending, 0 failed); M4 empirical suite 28/28 passed (100%).
- **Lint status**: Clean (Pure LINQ Lambda compliance in all modified BLL/DAL code).
- **Tests added/modified**: `tests/Test_Milestone4_POS_Invoicing.ps1` (28 empirical test cases covering all 4 M4 objectives).

## Loaded Skills
None

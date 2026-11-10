# BRIEFING — 2026-11-10T17:12:00Z

## Mission
Comprehensive survey of Data Access Layer, LINQ Lambda Compliance, App.config, and Relative Paths across the solution.

## 🔒 My Identity
- Archetype: explorer
- Roles: investigation, survey, synthesis
- Working directory: d:\DoAnCuoiKi_QuanLyTiemCaPhe
- State folder: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\explorer_survey_2
- Original parent: 19e9cc71-beac-4af5-ae86-94d31c09cf40
- Milestone: codebase survey

## 🔒 Key Constraints
- Read-only investigation — do NOT implement
- Strictly verify 100% LINQ Lambda compliance (3 points grading requirement)
- Audit all database files, connection strings, App.config, and absolute paths

## Current Parent
- Conversation ID: 19e9cc71-beac-4af5-ae86-94d31c09cf40
- Updated: 2026-11-10T17:12:00Z

## Investigation State
- **Explored paths**:
  * `App.config`, `CoffeeShopDataContext.dbml`, `CoffeeShopDataContext.designer.cs`, `CoffeeShopDataContext.cs`
  * `DAL\`: all 11 repository files
  * `BLL\`: all 13 service files
  * `DTO\`: all 12 DTO files
  * `Form\`: all WinForms and UserControls
  * `Utilities\`: `ConfigHelper.cs`, `UIHelper.cs`, `PrintHelper.cs`, `WordExportHelper.cs`, `SecurityHelper.cs`, `UserSession.cs`
  * SQL scripts: `CoffeeShopDB.sql`, `CoffeeShopDB_Full.sql`, `DataBaseCF.sql`, `Migrate_Database_SyncAll.sql`, `Migrate_Mision_EmployeeShifts.sql`
- **Key findings**:
  1. ORM is LINQ to SQL (.dbml), cleanly wrapped in 11 DAL repositories.
  2. Zero raw SQL queries (no SqlCommand, SqlDataAdapter, etc.).
  3. Exactly 12 non-Lambda LINQ Query syntax occurrences across 7 DAL repositories violating the 3-point instructor requirement.
  4. One architectural leak: `TableService.DeleteAreaByName` performs direct DataContext CRUD in BLL layer.
  5. App.config connection string dynamically loaded via `ConfigHelper` with auto-probe fallback. `FrmAppConfig` updates config but misses `CoffeeShopDBConnectionString1`.
  6. Product images & RDLC reports follow relative paths correctly.
  7. Employee avatars violate relative path requirements: `UCEmployeeAdd`/`UCEmployeeEdit` store absolute file paths directly in database, causing load failures on external machines.
- **Unexplored areas**: None within DAL, LINQ Lambda, App.config, and Paths survey scope.

## Key Decisions Made
- Fully documented all 12 non-Lambda query occurrences with exact file paths, line numbers, and pure LINQ Lambda replacement code.
- Fully documented the employee avatar absolute path bug and exact refactoring plan.

## Artifact Index
- DISPATCH.md — incoming instructions log
- BRIEFING.md — persistent state index
- progress.md — liveness heartbeat
- handoff.md — final survey report

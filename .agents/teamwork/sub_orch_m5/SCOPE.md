# Scope: Milestone 5 — 3rd-Party UI & Form Additions

## Objectives
1. **Third-Party UI Library Integration (1.0 point criterion)**:
   - Install and reference a modern third-party UI library: `Krypton.Toolkit` (or Guna.UI2.WinForms) in `.csproj` and `packages.config`.
   - Integrate 3rd-party controls into high-visibility user-facing forms:
     * `Form\VanThanh\FrmMain.cs`: Modern styled Krypton navigation buttons/badges in the sidebar.
     * `Form\VanThanh\FrmLogin.cs`: Modern styled Krypton controls for login buttons and inputs.
   - Satisfies the 1.0 point criterion for "UI 3rd party".
2. **Refactor Ephemeral Dialog into Standalone Designer Form**:
   - Convert runtime code-behind dialog `ShowAdminAuthDialog()` in `Form\VanThanh\FrmForgotPassword.cs` into a dedicated designer-backed Form:
     * `Form\VanThanh\FrmAdminAuth.cs` + `FrmAdminAuth.Designer.cs`
     * Visual dialog with admin password prompt, error labels, and OK/Cancel buttons.
   - Eliminates code-behind form generation violation.
3. **Warehouse / Inventory Management Form (`FrmInventory`)**:
   - Create visual designer form: `Form\QuangHung\FrmInventory.cs` + `FrmInventory.Designer.cs`.
   - Host `InventoryService` and `InventoryRepository`:
     * DataGridView of raw materials (Tồn kho, Đơn vị tính, Định mức tối thiểu, Cảnh báo thiếu hụt).
     * Nhập kho (Import material) dialog / controls.
     * Xuất kho (Export material) dialog / controls with anti-negative stock validation.
     * Transaction history view.
   - Add navigation entry `btnNavInventory` on `FrmMain` sidebar (role-gated for Manager).
   - Raises designer form count to 20 forms (>= 15 requirement exceeded).
4. **Classic Solution File (`.sln`) Compatibility**:
   - Create `DoAnCuoiKi_QuanLyTiemCaPhe.sln` alongside `DoAnCuoiKi_QuanLyTiemCaPhe.slnx` referencing project GUID `2BA02B03-B0F6-43D1-A1EB-4D2957FE1F9A`.
   - Resolves the 1 pending defect in `Test_PackagingReadiness.ps1`, driving E2E test pass rate to 100%.

## Relevant Files & Paths
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\PROJECT.md`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\TEST_READY.md`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\DoAnCuoiKi_QuanLyTiemCaPhe.csproj`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\packages.config`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\Form\VanThanh\FrmMain.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\Form\VanThanh\FrmMain.Designer.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\Form\VanThanh\FrmLogin.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\Form\VanThanh\FrmLogin.Designer.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\Form\VanThanh\FrmForgotPassword.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\DAL\InventoryRepository.cs`
- `d:\DoAnCuoiKi_QuanLyTiemCaPhe\BLL\InventoryService.cs`

## Verification
- MSBuild rebuild: 0 errors, 0 warnings.
- Designer files exist for all 20 Forms and 7 UserControls.
- Third-party UI assembly reference verified in `.csproj` and used in runtime forms.
- Classic `.sln` verified by MSBuild and test suite.
- Master test suite `tests\run_e2e_tests.ps1`: 100% PASS (0 defects).

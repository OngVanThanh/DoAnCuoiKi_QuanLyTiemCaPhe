using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    /// <summary>
    /// Màn hình Quản lý Sơ đồ bàn (FrmTable) - Thiết kế kéo thả WinForms Designer 100%.
    /// Quản lý danh sách bàn ăn (Mã bàn, Tên bàn, Trạng thái) độc lập không phụ thuộc Area.
    /// </summary>
    public partial class FrmTable : Form
    {
        private readonly TableService _tableService = new TableService();
        private int _selectedTableId = -1;
        // _isLoading removed

        public FrmTable()
        {
            InitializeComponent();
        }

        // 1. Sự kiện khi Form được tải lên
        private void FrmTable_Load(object sender, EventArgs e)
        {
            try
            {
                //
                UIHelper.StyleDataGridView(dgvTable);

                LoadStatusesToComboBox();
                LoadTablesToGrid();
                ResetInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Đã xảy ra lỗi khi tải dữ liệu bàn:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                //
            }
        }

        // 2. Tải danh sách Trạng thái bàn lên ComboBox cboTableStatus
        private void LoadStatusesToComboBox()
        {
            cboTableStatus.Items.Clear();
            foreach (string status in _tableService.GetValidTableStatuses())
            {
                cboTableStatus.Items.Add(status);
            }
            if (cboTableStatus.Items.Count > 0)
            {
                cboTableStatus.SelectedIndex = 0;
            }
        }

        // 3. Tải danh sách bàn lên DataGridView dgvTable và cập nhật thống kê trạng thái
        private void LoadTablesToGrid()
        {
            List<TableDTO> tables = _tableService.GetAllTables();

            dgvTable.DataSource = null;
            dgvTable.DataSource = tables;
            dgvTable.ClearSelection();

            // Cập nhật thống kê nhanh trên thanh tiêu đề
            int total = tables.Count;
            int emptyCount = tables.Count(t => string.Equals(t.Status, "Trống", StringComparison.OrdinalIgnoreCase));
            int occupiedCount = tables.Count(t => string.Equals(t.Status, "Có khách", StringComparison.OrdinalIgnoreCase));
            lblSummaryStats.Text = $"Tổng: {total} bàn | Trống: {emptyCount} | Có khách: {occupiedCount}";
        }

        // 4. Đặt lại các ô nhập liệu về trạng thái mặc định
        private void ResetInputFields()
        {
            _selectedTableId = -1;
            txtTableId.Clear();
            txtTableName.Clear();

            if (cboTableStatus.Items.Count > 0)
            {
                cboTableStatus.SelectedIndex = 0;
            }

            dgvTable.ClearSelection();
            txtTableName.Focus();
        }

        // 5. Sự kiện khi click chọn một dòng trên bảng dgvTable
        private void dgvTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvTable.Rows[e.RowIndex];
            if (row.Cells["colTableId"].Value == null)
                return;

            if (!int.TryParse(row.Cells["colTableId"].Value.ToString(), out int tableId))
                return;

            _selectedTableId = tableId;
            txtTableId.Text = tableId.ToString();
            txtTableName.Text = row.Cells["colTableName"].Value?.ToString() ?? string.Empty;

            //

            string status = row.Cells["colStatus"].Value?.ToString() ?? "Trống";
            int statusIndex = cboTableStatus.FindStringExact(status);
            cboTableStatus.SelectedIndex = statusIndex >= 0 ? statusIndex : 0;

            //
        }

        // 6. Sự kiện nút Thêm bàn (btnAdd)
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_selectedTableId > 0 && string.IsNullOrWhiteSpace(txtTableName.Text))
            {
                ResetInputFields();
                return;
            }

            string tableName = txtTableName.Text.Trim();
            if (string.IsNullOrWhiteSpace(tableName))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên bàn cần thêm!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtTableName.Focus();
                return;
            }

            string status = cboTableStatus.SelectedItem?.ToString() ?? "Trống";

            try
            {
                bool success = _tableService.AddTable(tableName, status, out string message);
                MessageBox.Show(
                    message,
                    success ? "Thông báo" : "Cảnh báo",
                    MessageBoxButtons.OK,
                    success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (success)
                {
                    //
                    LoadTablesToGrid();
                    ResetInputFields();
                    //
                }
            }
            catch (Exception ex)
            {
                //
                MessageBox.Show(
                    "Lỗi khi thêm bàn:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // 7. Sự kiện nút Sửa bàn (btnEdit)
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedTableId <= 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn một bàn trong danh sách để cập nhật!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string tableName = txtTableName.Text.Trim();
            string status = cboTableStatus.SelectedItem?.ToString() ?? "Trống";

            try
            {
                bool success = _tableService.UpdateTable(_selectedTableId, tableName, status, out string message);
                MessageBox.Show(
                    message,
                    success ? "Thông báo" : "Cảnh báo",
                    MessageBoxButtons.OK,
                    success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (success)
                {
                    //
                    LoadTablesToGrid();
                    ResetInputFields();
                    //
                }
            }
            catch (Exception ex)
            {
                //
                MessageBox.Show(
                    "Lỗi khi cập nhật bàn:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // 8. Sự kiện nút Xóa bàn (btnDelete)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedTableId <= 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn bàn cần xóa trong danh sách!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa '{txtTableName.Text}' (Mã bàn: {_selectedTableId}) không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                bool success = _tableService.DeleteTable(_selectedTableId, out string message);
                MessageBox.Show(
                    message,
                    success ? "Thông báo" : "Cảnh báo",
                    MessageBoxButtons.OK,
                    success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (success)
                {
                    //
                    LoadTablesToGrid();
                    ResetInputFields();
                    //
                }
            }
            catch (Exception ex)
            {
                //
                MessageBox.Show(
                    "Lỗi khi xóa bàn:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // 9. Sự kiện nút Làm mới (btnSave)
        private void btnSave_Click(object sender, EventArgs e)
        {
            //
            LoadTablesToGrid();
            ResetInputFields();
            //
        }
    }
}

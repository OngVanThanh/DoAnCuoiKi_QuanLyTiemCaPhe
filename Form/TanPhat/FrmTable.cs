using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using System;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    /// <summary>
    /// Màn hình Quản lý Sơ đồ bàn & Khu vực (FrmTable).
    /// Cho phép xem danh sách bàn, lọc theo khu vực, thêm mới, cập nhật và xóa bàn.
    /// </summary>
    public partial class FrmTable : Form
    {
        private readonly TableService _tableService = new TableService();
        private int _selectedTableId = -1;
        private bool _isLoading = false;

        public FrmTable()
        {
            InitializeComponent();
        }

        // 1. Sự kiện khi Form được tải lên
        private void FrmTable_Load(object sender, EventArgs e)
        {
            try
            {
                _isLoading = true;
                LoadAreasToComboBox();
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
                _isLoading = false;
            }
        }

        // 2. Tải danh sách Khu vực lên ComboBox cboArea
        private void LoadAreasToComboBox()
        {
            cboArea.DataSource = null;
            cboArea.DisplayMember = "AreaName";
            cboArea.ValueMember = "Id";
            cboArea.DataSource = _tableService.GetAllAreas();
            if (cboArea.Items.Count > 0)
            {
                cboArea.SelectedIndex = 0;
            }
        }

        // 3. Tải danh sách Trạng thái bàn lên ComboBox cboTableStatus
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

        // 4. Tải danh sách bàn lên DataGridView dgvTable (hỗ trợ lọc theo khu vực nếu tích chọn)
        private void LoadTablesToGrid()
        {
            int filterAreaId = -1;
            if (chkFilterByArea.Checked && TryGetSelectedAreaId(out int selectedAreaId))
            {
                filterAreaId = selectedAreaId;
            }

            dgvTable.DataSource = null;
            dgvTable.DataSource = _tableService.GetTablesByArea(filterAreaId);

            if (dgvTable.Columns.Contains("Id"))
                dgvTable.Columns["Id"].HeaderText = "Mã bàn";

            if (dgvTable.Columns.Contains("TableName"))
                dgvTable.Columns["TableName"].HeaderText = "Tên bàn";

            if (dgvTable.Columns.Contains("AreaId"))
                dgvTable.Columns["AreaId"].Visible = false;

            if (dgvTable.Columns.Contains("AreaName"))
                dgvTable.Columns["AreaName"].HeaderText = "Khu vực";

            if (dgvTable.Columns.Contains("Status"))
                dgvTable.Columns["Status"].HeaderText = "Trạng thái";

            dgvTable.ReadOnly = true;
            dgvTable.AllowUserToAddRows = false;
            dgvTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTable.MultiSelect = false;
            dgvTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTable.ClearSelection();
        }

        // 5. Lấy AreaId đang được chọn trên ComboBox cboArea
        private bool TryGetSelectedAreaId(out int areaId)
        {
            areaId = -1;
            if (cboArea.SelectedValue == null)
                return false;

            return int.TryParse(cboArea.SelectedValue.ToString(), out areaId);
        }

        // 6. Đặt lại các ô nhập liệu về trạng thái mặc định (chuẩn bị thêm mới)
        private void ResetInputFields()
        {
            _selectedTableId = -1;
            txtTableId.Clear();
            txtTableName.Clear();

            if (cboTableStatus.Items.Count > 0)
            {
                cboTableStatus.SelectedIndex = 0; // Mặc định là "Trống"
            }

            dgvTable.ClearSelection();
            txtTableName.Focus();
        }

        // 7. Sự kiện khi thay đổi lựa chọn Khu vực trên cboArea
        private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading)
                return;

            if (chkFilterByArea.Checked)
            {
                LoadTablesToGrid();
            }
        }

        // 8. Sự kiện khi bật/tắt chế độ lọc bàn theo khu vực
        private void chkFilterByArea_CheckedChanged(object sender, EventArgs e)
        {
            if (_isLoading)
                return;

            LoadTablesToGrid();
        }

        // 9. Sự kiện khi click chọn một dòng trên bảng dgvTable
        private void dgvTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvTable.Rows[e.RowIndex];
            if (row.Cells["Id"].Value == null)
                return;

            if (!int.TryParse(row.Cells["Id"].Value.ToString(), out int tableId))
                return;

            _selectedTableId = tableId;
            txtTableId.Text = tableId.ToString();
            txtTableName.Text = row.Cells["TableName"].Value?.ToString() ?? string.Empty;

            _isLoading = true;

            if (row.Cells["AreaId"].Value != null &&
                int.TryParse(row.Cells["AreaId"].Value.ToString(), out int areaId))
            {
                cboArea.SelectedValue = areaId;
            }

            string status = row.Cells["Status"].Value?.ToString() ?? "Trống";
            int statusIndex = cboTableStatus.FindStringExact(status);
            cboTableStatus.SelectedIndex = statusIndex >= 0 ? statusIndex : 0;

            _isLoading = false;
        }

        // 10. Sự kiện nút Thêm bàn (btnAdd)
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Nếu đang chọn một bàn cũ trên lưới và chưa nhập tên bàn mới, chuyển sang chế độ nhập mới
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

            if (!TryGetSelectedAreaId(out int areaId))
            {
                MessageBox.Show(
                    "Vui lòng chọn khu vực cho bàn!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string status = cboTableStatus.SelectedItem?.ToString() ?? "Trống";

            try
            {
                bool success = _tableService.AddTable(tableName, areaId, status, out string message);
                MessageBox.Show(
                    message,
                    success ? "Thông báo" : "Cảnh báo",
                    MessageBoxButtons.OK,
                    success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (success)
                {
                    _isLoading = true;
                    LoadTablesToGrid();
                    ResetInputFields();
                    _isLoading = false;
                }
            }
            catch (Exception ex)
            {
                _isLoading = false;
                MessageBox.Show(
                    "Lỗi khi thêm bàn:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // 11. Sự kiện nút Sửa bàn (btnEdit)
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

            if (!TryGetSelectedAreaId(out int areaId))
            {
                MessageBox.Show(
                    "Vui lòng chọn khu vực hợp lệ!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string tableName = txtTableName.Text.Trim();
            string status = cboTableStatus.SelectedItem?.ToString() ?? "Trống";

            try
            {
                bool success = _tableService.UpdateTable(_selectedTableId, tableName, areaId, status, out string message);
                MessageBox.Show(
                    message,
                    success ? "Thông báo" : "Cảnh báo",
                    MessageBoxButtons.OK,
                    success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (success)
                {
                    _isLoading = true;
                    LoadTablesToGrid();
                    ResetInputFields();
                    _isLoading = false;
                }
            }
            catch (Exception ex)
            {
                _isLoading = false;
                MessageBox.Show(
                    "Lỗi khi cập nhật bàn:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // 12. Sự kiện nút Xóa bàn (btnDelete)
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
                    _isLoading = true;
                    LoadTablesToGrid();
                    ResetInputFields();
                    _isLoading = false;
                }
            }
            catch (Exception ex)
            {
                _isLoading = false;
                MessageBox.Show(
                    "Lỗi khi xóa bàn:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // 13. Sự kiện nút Lưu / Làm mới (btnSave)
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Nếu người dùng đã nhập thông tin bàn mới (chưa chọn bàn cũ) thì thực hiện lưu mới,
            // ngược lại sẽ làm mới các trường nhập liệu và tải lại danh sách bàn.
            if (_selectedTableId <= 0 && !string.IsNullOrWhiteSpace(txtTableName.Text))
            {
                btnAdd_Click(sender, e);
                return;
            }

            _isLoading = true;
            chkFilterByArea.Checked = false;
            LoadTablesToGrid();
            ResetInputFields();
            _isLoading = false;
        }
    }
}

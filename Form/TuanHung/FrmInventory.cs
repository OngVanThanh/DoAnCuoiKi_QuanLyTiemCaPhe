using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmInventory : Form
    {
        private readonly InventoryService _inventoryService = new InventoryService();
        private bool _isAdding = false;

        public FrmInventory()
        {
            InitializeComponent();
        }

        private void FrmInventory_Load(object sender, EventArgs e)
        {
            // 1. Chuẩn hóa cả 2 bảng DataGridView qua UIHelper
            UIHelper.StyleDataGridView(dgvInventory);
            UIHelper.StyleDataGridView(dgvHistory);

            // 2. Khóa ô nhập CRUD, mở Thêm/Sửa/Xóa
            UIHelper.SetFormState(grpMaterialInfo, false, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);

            // 3. Tải danh sách nguyên liệu và lịch sử giao dịch
            LoadInventoryList();
            LoadTransactionHistory();
        }

        // Tải danh sách tồn kho nguyên liệu
        private void LoadInventoryList()
        {
            var list = _inventoryService.GetAllMaterials();
            dgvInventory.DataSource = list;

            if (dgvInventory.Columns["Id"] != null) dgvInventory.Columns["Id"].HeaderText = "Mã";
            if (dgvInventory.Columns["MaterialName"] != null) dgvInventory.Columns["MaterialName"].HeaderText = "Tên nguyên liệu";
            if (dgvInventory.Columns["Unit"] != null) dgvInventory.Columns["Unit"].HeaderText = "ĐVT";
            if (dgvInventory.Columns["QuantityInStock"] != null)
            {
                dgvInventory.Columns["QuantityInStock"].HeaderText = "Tồn kho";
                dgvInventory.Columns["QuantityInStock"].DefaultCellStyle.Format = "N1";
            }
            if (dgvInventory.Columns["MinThreshold"] != null)
            {
                dgvInventory.Columns["MinThreshold"].HeaderText = "Định mức tối thiểu";
                dgvInventory.Columns["MinThreshold"].DefaultCellStyle.Format = "N1";
            }
            if (dgvInventory.Columns["IsLowStock"] != null)
            {
                dgvInventory.Columns["IsLowStock"].HeaderText = "Cảnh báo thiếu";
            }

            // Đổi màu cảnh báo đỏ cho các nguyên liệu sắp hết (dưới định mức)
            foreach (DataGridViewRow row in dgvInventory.Rows)
            {
                if (row.Cells["IsLowStock"].Value is bool isLow && isLow)
                {
                    row.DefaultCellStyle.ForeColor = Color.Red;
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                }
            }
        }

        // Tải lịch sử giao dịch kho
        private void LoadTransactionHistory()
        {
            var history = _inventoryService.GetTransactionHistory();
            dgvHistory.DataSource = history;

            if (dgvHistory.Columns["Id"] != null) dgvHistory.Columns["Id"].HeaderText = "Mã GD";
            if (dgvHistory.Columns["MaterialName"] != null) dgvHistory.Columns["MaterialName"].HeaderText = "Tên nguyên liệu";
            if (dgvHistory.Columns["EmployeeName"] != null) dgvHistory.Columns["EmployeeName"].HeaderText = "Người thực hiện";
            if (dgvHistory.Columns["TransactionType"] != null) dgvHistory.Columns["TransactionType"].HeaderText = "Loại giao dịch";
            if (dgvHistory.Columns["Quantity"] != null)
            {
                dgvHistory.Columns["Quantity"].HeaderText = "Số lượng";
                dgvHistory.Columns["Quantity"].DefaultCellStyle.Format = "N1";
            }
            if (dgvHistory.Columns["TransactionDate"] != null)
            {
                dgvHistory.Columns["TransactionDate"].HeaderText = "Ngày giờ giao dịch";
                dgvHistory.Columns["TransactionDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            }

            if (dgvHistory.Columns["MaterialId"] != null) dgvHistory.Columns["MaterialId"].Visible = false;
            if (dgvHistory.Columns["EmployeeId"] != null) dgvHistory.Columns["EmployeeId"].Visible = false;
        }

        // Khi click vào dòng nguyên liệu -> Hiển thị lên ô nhập
        private void dgvInventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvInventory.Rows.Count)
            {
                var row = dgvInventory.Rows[e.RowIndex];
                txtMaterialId.Text = row.Cells["Id"].Value?.ToString();
                txtMaterialName.Text = row.Cells["MaterialName"].Value?.ToString();
                txtUnit.Text = row.Cells["Unit"].Value?.ToString();
                txtQuantityInStock.Text = row.Cells["QuantityInStock"].Value?.ToString();
                txtMinThreshold.Text = row.Cells["MinThreshold"].Value?.ToString();
            }
        }

        // ==========================================================
        // NGHIỆP VỤ NHẬP KHO & XUẤT KHO (ĐIỂM NHẤN CỐT LÕI)
        // ==========================================================

        // Nút Nhập kho
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaterialId.Text))
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu cần nhập kho từ bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int materialId = int.Parse(txtMaterialId.Text);
            double qty = (double)nudQuantity.Value;
            int employeeId = UserSession.CurrentUser?.EmployeeId ?? 1; // Lấy ID nhân viên đang đăng nhập

            string errorMsg;
            bool success = _inventoryService.ImportMaterial(materialId, qty, employeeId, out errorMsg);

            if (success)
            {
                MessageBox.Show($"Nhập kho thành công +{qty} {txtUnit.Text} cho '{txtMaterialName.Text}'!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadInventoryList();
                LoadTransactionHistory();
            }
            else
            {
                MessageBox.Show(errorMsg, "Lỗi nhập kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nút Xuất kho (Kiểm tra chống âm kho & Cảnh báo sắp hết hàng)
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaterialId.Text))
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu cần xuất kho từ bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int materialId = int.Parse(txtMaterialId.Text);
            double qty = (double)nudQuantity.Value;
            int employeeId = UserSession.CurrentUser?.EmployeeId ?? 1;

            string errorMsg;
            bool isLowStock;
            bool success = _inventoryService.ExportMaterial(materialId, qty, employeeId, out errorMsg, out isLowStock);

            if (success)
            {
                string msg = $"Xuất kho thành công -{qty} {txtUnit.Text} cho '{txtMaterialName.Text}'!";
                if (isLowStock)
                {
                    msg += "\n\n⚠️ CẢNH BÁO: Số lượng tồn kho của nguyên liệu này hiện đã XUỐNG DƯỚI ĐỊNH MỨC AN TOÀN! Vui lòng nhập thêm.";
                    MessageBox.Show(msg, "Cảnh báo tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadInventoryList();
                LoadTransactionHistory();
            }
            else
            {
                MessageBox.Show(errorMsg, "Lỗi xuất kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nút làm mới lịch sử giao dịch ở Tab 2
        private void btnRefreshHistory_Click(object sender, EventArgs e)
        {
            LoadTransactionHistory();
            MessageBox.Show("Đã làm mới nhật ký giao dịch!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ==========================================================
        // CÁC THAO TÁC CRUD NGUYÊN LIỆU MỚI
        // ==========================================================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isAdding = true;

            txtMaterialId.Text = "";
            txtMaterialName.Text = "";
            txtUnit.Text = "Kg";
            txtQuantityInStock.Text = "0";
            txtMinThreshold.Text = "10";

            UIHelper.SetFormState(grpMaterialInfo, true, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            grpAction.Enabled = false; // Khi đang thêm mới thì tạm khóa cụm nhập/xuất
            txtMaterialName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaterialId.Text))
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu cần chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _isAdding = false;
            UIHelper.SetFormState(grpMaterialInfo, true, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            grpAction.Enabled = false;
            txtMaterialName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UIHelper.SetFormState(grpMaterialInfo, false, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            grpAction.Enabled = true;

            if (dgvInventory.CurrentRow != null)
            {
                dgvInventory_CellClick(null, new DataGridViewCellEventArgs(0, dgvInventory.CurrentRow.Index));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            double minThreshold;
            if (!double.TryParse(txtMinThreshold.Text.Trim(), out minThreshold))
            {
                MessageBox.Show("Định mức tối thiểu phải là số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new InventoryDTO
            {
                Id = _isAdding ? 0 : int.Parse(txtMaterialId.Text),
                MaterialName = txtMaterialName.Text,
                Unit = txtUnit.Text,
                QuantityInStock = _isAdding ? 0 : double.Parse(txtQuantityInStock.Text),
                MinThreshold = minThreshold
            };

            string errorMsg;
            bool success = _inventoryService.SaveMaterial(dto, out errorMsg);

            if (success)
            {
                MessageBox.Show(_isAdding ? "Thêm nguyên liệu mới thành công!" : "Cập nhật nguyên liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadInventoryList();
                UIHelper.SetFormState(grpMaterialInfo, false, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
                grpAction.Enabled = true;
            }
            else
            {
                MessageBox.Show(errorMsg, "Lỗi kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaterialId.Text))
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = int.Parse(txtMaterialId.Text);
            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa nguyên liệu '{txtMaterialName.Text}' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                string errorMsg;
                bool success = _inventoryService.DeleteMaterial(id, out errorMsg);
                if (success)
                {
                    MessageBox.Show("Xóa nguyên liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInventoryList();
                    txtMaterialId.Text = "";
                    txtMaterialName.Text = "";
                    txtUnit.Text = "";
                    txtQuantityInStock.Text = "";
                    txtMinThreshold.Text = "";
                }
                else
                {
                    MessageBox.Show(errorMsg, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
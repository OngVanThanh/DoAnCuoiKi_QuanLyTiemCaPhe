using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmCategory : Form
    {
        private readonly ProductService _productService = new ProductService();
        private bool _isAdding = false; // Đánh dấu đang ở trạng thái Thêm mới hay Sửa

        public FrmCategory()
        {
            InitializeComponent();
        }

        private void FrmCategory_Load(object sender, EventArgs e)
        {
            // 1. Áp dụng bảng màu và chuẩn hóa DataGridView qua UIHelper (Tiêu chí 1đ UI/UX)
            UIHelper.StyleDataGridView(dgvCategory);

            // 2. Mặc định ở trạng thái Xem (Khóa ô nhập, chỉ sáng nút Thêm/Sửa/Xóa)
            UIHelper.SetFormState(grpCategoryInfo, false, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);

            // 3. Tải danh sách nhóm món lên bảng
            LoadCategoryList();
        }

        // Tải danh sách từ BLL lên bảng
        private void LoadCategoryList()
        {
            var list = _productService.GetAllCategories();
            dgvCategory.DataSource = list;

            // Đặt tên tiêu đề cột tiếng Việt rõ ràng
            if (dgvCategory.Columns["Id"] != null) dgvCategory.Columns["Id"].HeaderText = "Mã nhóm";
            if (dgvCategory.Columns["CategoryName"] != null) dgvCategory.Columns["CategoryName"].HeaderText = "Tên nhóm món";
            if (dgvCategory.Columns["Description"] != null) dgvCategory.Columns["Description"].HeaderText = "Mô tả";
        }

        // Khi click vào 1 dòng trên DataGridView -> Hiển thị dữ liệu lên các ô TextBox
        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvCategory.Rows.Count)
            {
                var row = dgvCategory.Rows[e.RowIndex];
                txtCategoryId.Text = row.Cells["Id"].Value?.ToString();
                txtCategoryName.Text = row.Cells["CategoryName"].Value?.ToString();
                txtDescription.Text = row.Cells["Description"].Value?.ToString();
            }
        }

        // NÚT THÊM: Mở khóa ô nhập, sáng nút Lưu/Hủy
        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isAdding = true;

            // Xóa trắng ô nhập để chuẩn bị nhập mới
            txtCategoryId.Text = "";
            txtCategoryName.Text = "";
            txtDescription.Text = "";

            UIHelper.SetFormState(grpCategoryInfo, true, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            txtCategoryName.Focus();
        }

        // NÚT SỬA: Mở khóa ô nhập để sửa dòng đang chọn
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoryId.Text))
            {
                MessageBox.Show("Vui lòng chọn một nhóm món trong danh sách để chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _isAdding = false;
            UIHelper.SetFormState(grpCategoryInfo, true, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            txtCategoryName.Focus();
        }

        // NÚT HỦY: Trở về trạng thái Xem bình thường
        private void btnCancel_Click(object sender, EventArgs e)
        {
            UIHelper.SetFormState(grpCategoryInfo, false, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            // Nạp lại dòng đang chọn nếu có
            if (dgvCategory.CurrentRow != null)
            {
                dgvCategory_CellClick(null, new DataGridViewCellEventArgs(0, dgvCategory.CurrentRow.Index));
            }
        }

        // NÚT LƯU: Gọi Service kiểm tra hợp lệ và lưu xuống CSDL
        private void btnSave_Click(object sender, EventArgs e)
        {
            var dto = new CategoryDTO
            {
                Id = _isAdding ? 0 : int.Parse(txtCategoryId.Text),
                CategoryName = txtCategoryName.Text,
                Description = txtDescription.Text
            };

            string errorMsg;
            bool success = _productService.SaveCategory(dto, out errorMsg);

            if (success)
            {
                MessageBox.Show(_isAdding ? "Thêm nhóm món thành công!" : "Cập nhật nhóm món thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCategoryList();
                UIHelper.SetFormState(grpCategoryInfo, false, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            }
            else
            {
                MessageBox.Show(errorMsg, "Lỗi kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // NÚT XÓA: Gọi Service xóa nhóm món
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoryId.Text))
            {
                MessageBox.Show("Vui lòng chọn nhóm món cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = int.Parse(txtCategoryId.Text);

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhóm món '{txtCategoryName.Text}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                string errorMsg;
                bool success = _productService.DeleteCategory(id, out errorMsg);

                if (success)
                {
                    MessageBox.Show("Xóa nhóm món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoryList();
                    txtCategoryId.Text = "";
                    txtCategoryName.Text = "";
                    txtDescription.Text = "";
                }
                else
                {
                    MessageBox.Show(errorMsg, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void grpCategoryInfo_Enter(object sender, EventArgs e)
        {
        }

        private void lblCategoryName_Click(object sender, EventArgs e)
        {
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmCategory_Load_1(object sender, EventArgs e)
        {
            FrmCategory_Load(sender, e); // Gọi hàm nạp dữ liệu và làm đẹp bảng
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            btnCancel_Click(sender, e); // Gọi hàm hủy
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            btnSave_Click(sender, e); // Gọi hàm lưu
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
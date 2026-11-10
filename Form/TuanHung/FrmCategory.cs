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
        private bool _isAdding = false;

        public FrmCategory()
        {
            InitializeComponent();
        }

        private void FrmCategory_Load(object sender, EventArgs e)
        {
            dgvCategory.AutoGenerateColumns = false;
            UIHelper.SetFormState(grpCategoryInfo, false, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            LoadCategoryList();
        }

        private void LoadCategoryList()
        {
            dgvCategory.AutoGenerateColumns = false;
            dgvCategory.DataSource = null;
            dgvCategory.DataSource = _productService.GetAllCategories();
        }

        private void ClearCategoryInputs()
        {
            txtCategoryId.Clear();
            txtCategoryName.Clear();
            txtDescription.Clear();
        }

        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvCategory.Rows.Count)
            {
                DataGridViewRow row = dgvCategory.Rows[e.RowIndex];
                txtCategoryId.Text = row.Cells["colCatId"].Value?.ToString();
                txtCategoryName.Text = row.Cells["colCatName"].Value?.ToString();
                txtDescription.Text = row.Cells["colCatDesc"].Value?.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isAdding = true;
            ClearCategoryInputs();
            UIHelper.SetFormState(grpCategoryInfo, true, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            txtCategoryId.Enabled = false;
            txtCategoryName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoryId.Text))
            {
                MessageBox.Show("Vui lòng chọn một nhóm sản phẩm dưới bảng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _isAdding = false;
            UIHelper.SetFormState(grpCategoryInfo, true, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            txtCategoryId.Enabled = false;
            txtCategoryName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoryId.Text))
            {
                MessageBox.Show("Vui lòng chọn nhóm sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhóm sản phẩm này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!int.TryParse(txtCategoryId.Text, out int id))
                {
                    MessageBox.Show("Mã nhóm sản phẩm không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_productService.DeleteCategory(id, out string errorMsg))
                {
                    MessageBox.Show("Xóa nhóm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoryList();
                    ClearCategoryInputs();
                }
                else
                {
                    MessageBox.Show(errorMsg, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int catId = 0;
            if (!_isAdding && !string.IsNullOrEmpty(txtCategoryId.Text))
            {
                if (!int.TryParse(txtCategoryId.Text, out catId))
                {
                    MessageBox.Show("Mã nhóm sản phẩm không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            CategoryDTO cat = new CategoryDTO
            {
                Id = catId,
                CategoryName = txtCategoryName.Text.Trim(),
                Description = txtDescription.Text.Trim()
            };

            if (_productService.SaveCategory(cat, out string errorMsg))
            {
                MessageBox.Show(_isAdding ? "Thêm mới thành công!" : "Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCategoryList();
                UIHelper.SetFormState(grpCategoryInfo, false, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            }
            else
            {
                MessageBox.Show(errorMsg, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearCategoryInputs();
            UIHelper.SetFormState(grpCategoryInfo, false, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
        }
    }
}
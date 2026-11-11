using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Windows.Forms;
using System.Drawing;

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
            UIHelper.StyleDataGridView(dgvCategory);
            LoadCategoryList();
            SetMode(false);
        }

        private void LoadCategoryList()
        {
            dgvCategory.AutoGenerateColumns = false;
            dgvCategory.DataSource = null;
            dgvCategory.DataSource = _productService.GetAllCategories();
        }
        /// <summary>
        /// Đổi màu nút động: Active thì có màu rực rỡ, Inactive thì mất màu (xám nhạt phẳng).
        /// </summary>
        private void SetButtonActive(Button btn, bool isActive, Color activeColor)
        {
            btn.Enabled = isActive;
            if (isActive)
            {
                btn.BackColor = activeColor;
                btn.ForeColor = Color.White;
                btn.Cursor = Cursors.Hand;
            }
            else
            {
                btn.BackColor = Color.FromArgb(220, 220, 220); // Mất màu
                btn.ForeColor = Color.FromArgb(150, 150, 150); // Chữ mờ
                btn.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Điều khiển trạng thái CRUD: Ẩn/Hiện thông minh cụm nút và khóa/mở các ô nhập.
        /// </summary>
        private void SetMode(bool isEditing)
        {
            txtCategoryName.ReadOnly = !isEditing;
            txtCategoryName.BackColor = isEditing ? Color.White : Color.FromArgb(245, 245, 245);

            txtDescription.ReadOnly = !isEditing;
            txtDescription.BackColor = isEditing ? Color.White : Color.FromArgb(245, 245, 245);

            // Ẩn / Hiện cụm nút
            btnAdd.Visible = !isEditing;
            btnEdit.Visible = !isEditing;
            btnDelete.Visible = !isEditing;

            btnSave.Visible = isEditing;
            btnCancel.Visible = isEditing;

            // Khóa bảng khi đang thêm/sửa
            dgvCategory.Enabled = !isEditing;

            if (isEditing)
            {
                SetButtonActive(btnSave, true, Color.FromArgb(39, 174, 96));
                SetButtonActive(btnCancel, true, Color.FromArgb(127, 140, 141));
            }
            else
            {
                SetButtonActive(btnAdd, true, Color.FromArgb(39, 174, 96));
                bool hasSelection = !string.IsNullOrEmpty(txtCategoryId.Text);
                SetButtonActive(btnEdit, hasSelection, Color.FromArgb(211, 84, 0));
                SetButtonActive(btnDelete, hasSelection, Color.FromArgb(192, 57, 43));
            }
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

                SetButtonActive(btnEdit, true, Color.FromArgb(211, 84, 0));
                SetButtonActive(btnDelete, true, Color.FromArgb(192, 57, 43));
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isAdding = true;
            ClearCategoryInputs();
            SetMode(true);
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
            SetMode(true);
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
                    ClearCategoryInputs();
                    SetMode(false);
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
                SetMode(false);
            }
            else
            {
                MessageBox.Show(errorMsg, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearCategoryInputs();
            SetMode(false);
        }
    }
}
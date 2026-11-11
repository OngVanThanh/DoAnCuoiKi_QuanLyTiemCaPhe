using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmProduct : Form
    {
        private readonly ProductService _productService = new ProductService();
        private bool _isAdding = false;
        private string _selectedImagePath = "";

        public FrmProduct()
        {
            InitializeComponent();
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            dgvProduct.AutoGenerateColumns = false;
            UIHelper.StyleDataGridView(dgvProduct);

            // Định dạng hiển thị tiền tệ cho cột giá trên DataGridView
            if (dgvProduct.Columns["colPrice"] != null)
            {
                dgvProduct.Columns["colPrice"].DefaultCellStyle.Format = "#,##0 đ";
                dgvProduct.Columns["colPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            LoadCategoriesToComboBox();
            LoadProductList();

            // Khởi tạo trạng thái Xem ban đầu
            SetMode(false);
        }

        /// <summary>
        /// Điều khiển trạng thái CRUD: Ẩn/Hiện thông minh cụm nút và khóa/mở các ô nhập liệu.
        /// </summary>
        /// <param name="isEditing">true: Đang Thêm mới hoặc Sửa; false: Đang Xem bình thường</param>
        private void SetMode(bool isEditing)
        {
            // 1. Khóa / Mở khóa các ô nhập liệu
            txtProductName.ReadOnly = !isEditing;
            txtProductName.BackColor = isEditing ? Color.White : Color.FromArgb(245, 245, 245);

            cboCategory.Enabled = isEditing;

            txtPrice.ReadOnly = !isEditing;
            txtPrice.BackColor = isEditing ? Color.White : Color.FromArgb(245, 245, 245);

            txtUnit.ReadOnly = !isEditing;
            txtUnit.BackColor = isEditing ? Color.White : Color.FromArgb(245, 245, 245);

            btnBrowseImage.Enabled = isEditing;

            // 2. Ẩn / Hiện cụm nút theo chế độ
            btnAdd.Visible = !isEditing;
            btnEdit.Visible = !isEditing;
            btnDelete.Visible = !isEditing;

            btnSave.Visible = isEditing;
            btnCancel.Visible = isEditing;

            // 3. Khóa bảng khi đang chỉnh sửa
            dgvProduct.Enabled = !isEditing;

            if (isEditing)
            {
                // Khi Thêm / Sửa: Lưu (Xanh) và Hủy (Xám đậm) đều Active có màu
                SetButtonActive(btnSave, true, Color.FromArgb(39, 174, 96));
                SetButtonActive(btnCancel, true, Color.FromArgb(127, 140, 141));
            }
            else
            {
                // Khi Xem: Nút Thêm luôn Active (Xanh lá)
                SetButtonActive(btnAdd, true, Color.FromArgb(39, 174, 96));

                // Sửa và Xóa: CHỈ CÓ MÀU khi đã chọn một món hợp lệ, ngược lại MẤT MÀU
                bool hasSelection = !string.IsNullOrEmpty(txtProductId.Text);
                SetButtonActive(btnEdit, hasSelection, Color.FromArgb(211, 84, 0));
                SetButtonActive(btnDelete, hasSelection, Color.FromArgb(192, 57, 43));
            }
        }

        /// <summary>
        /// Đổi màu nút động theo trạng thái: Active thì có màu rực rỡ, Inactive thì mất màu (xám nhạt mờ).
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
                btn.BackColor = Color.FromArgb(220, 220, 220); // Nền xám nhạt mất màu
                btn.ForeColor = Color.FromArgb(150, 150, 150); // Chữ xám mờ
                btn.Cursor = Cursors.Default;
            }
        }

        private void LoadCategoriesToComboBox()
        {
            var categories = _productService.GetAllCategories();
            cboCategory.DataSource = categories;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "Id";

            var filterCategories = _productService.GetAllCategories();
            filterCategories.Insert(0, new CategoryDTO { Id = 0, CategoryName = "-- Tất cả nhóm món --" });
            cboCategoryFilter.DataSource = filterCategories;
            cboCategoryFilter.DisplayMember = "CategoryName";
            cboCategoryFilter.ValueMember = "Id";
            cboCategoryFilter.SelectedIndex = 0;
        }

        private void LoadProductList()
        {
            dgvProduct.AutoGenerateColumns = false;
            dgvProduct.DataSource = null;
            dgvProduct.DataSource = _productService.GetAllProducts();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvProduct.Rows.Count)
            {
                var row = dgvProduct.Rows[e.RowIndex];
                txtProductId.Text = row.Cells["colProdId"].Value?.ToString();
                txtProductName.Text = row.Cells["colProdName"].Value?.ToString();
                if (row.Cells["colCategoryId"].Value != null)
                {
                    cboCategory.SelectedValue = Convert.ToInt32(row.Cells["colCategoryId"].Value);
                }
                txtPrice.Text = row.Cells["colPrice"].Value?.ToString();
                txtUnit.Text = row.Cells["colUnit"].Value?.ToString();

                string relativePath = row.Cells["colImagePath"].Value?.ToString();
                UIHelper.LoadImageToPictureBox(picProductImage, relativePath);
                _selectedImagePath = "";

                SetButtonActive(btnEdit, true, Color.FromArgb(211, 84, 0));
                SetButtonActive(btnDelete, true, Color.FromArgb(192, 57, 43));
            }
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn hình ảnh món ăn";
                ofd.Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _selectedImagePath = ofd.FileName;
                    UIHelper.LoadImageToPictureBox(picProductImage, _selectedImagePath);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isAdding = true;
            _selectedImagePath = "";

            txtProductId.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            txtUnit.Text = "Ly";
            picProductImage.Image = null;

            SetMode(true);
            txtProductName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductId.Text))
            {
                MessageBox.Show("Vui lòng chọn món ăn cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _isAdding = false;
            _selectedImagePath = "";
            SetMode(true);
            txtProductName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetMode(false);

            if (dgvProduct.CurrentRow != null)
            {
                dgvProduct_CellClick(null, new DataGridViewCellEventArgs(0, dgvProduct.CurrentRow.Index));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price))
            {
                MessageBox.Show("Đơn giá không hợp lệ (phải là số)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int prodId = 0;
            if (!_isAdding && !string.IsNullOrEmpty(txtProductId.Text))
            {
                if (!int.TryParse(txtProductId.Text, out prodId))
                {
                    MessageBox.Show("Mã sản phẩm không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var dto = new ProductDTO
            {
                Id = prodId,
                ProductName = txtProductName.Text,
                CategoryId = Convert.ToInt32(cboCategory.SelectedValue),
                Price = price,
                Unit = txtUnit.Text,
                ImagePath = dgvProduct.CurrentRow != null && !_isAdding ? dgvProduct.CurrentRow.Cells["colImagePath"].Value?.ToString() : "",
                IsActive = true
            };

            if (_productService.SaveProduct(dto, _selectedImagePath, out string errorMsg))
            {
                MessageBox.Show(_isAdding ? "Thêm món mới thành công!" : "Cập nhật món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProductList();
                SetMode(false);
                btnBrowseImage.Enabled = false;
            }
            else
            {
                MessageBox.Show(errorMsg, "Lỗi kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductId.Text))
            {
                MessageBox.Show("Vui lòng chọn món cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!int.TryParse(txtProductId.Text, out int id))
            {
                MessageBox.Show("Mã sản phẩm không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa món '{txtProductName.Text}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                if (_productService.DeleteProduct(id, out string errorMsg))
                {
                    MessageBox.Show("Xóa món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProductList();
                    txtProductId.Clear();
                    txtProductName.Clear();
                    txtPrice.Clear();
                    picProductImage.Image = null;
                    txtProductId.Clear();
                    txtProductName.Clear();
                    txtPrice.Clear();
                    picProductImage.Image = null;
                    SetButtonActive(btnEdit, false, Color.FromArgb(211, 84, 0));
                    SetButtonActive(btnDelete, false, Color.FromArgb(192, 57, 43));
                }
                else
                {
                    MessageBox.Show(errorMsg, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cboCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategoryFilter.SelectedValue != null && int.TryParse(cboCategoryFilter.SelectedValue.ToString(), out int categoryId))
            {
                dgvProduct.AutoGenerateColumns = false;
                dgvProduct.DataSource = null;
                dgvProduct.DataSource = _productService.GetProductsByCategory(categoryId);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearchProduct.Text.Trim();
            dgvProduct.AutoGenerateColumns = false;
            dgvProduct.DataSource = null;
            dgvProduct.DataSource = _productService.SearchProducts(keyword);
        }
    }
}
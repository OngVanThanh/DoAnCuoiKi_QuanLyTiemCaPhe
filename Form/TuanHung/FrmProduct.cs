using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.IO;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmProduct : Form
    {
        private readonly ProductService _productService = new ProductService();
        private bool _isAdding = false;
        private string _selectedImagePath = ""; // Lưu đường dẫn file ảnh người dùng vừa chọn từ máy tính

        public FrmProduct()
        {
            InitializeComponent();
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            // 1. Chuẩn hóa DataGridView
            UIHelper.StyleDataGridView(dgvProduct);

            // 2. Mặc định ở trạng thái Xem
            UIHelper.SetFormState(grpProductInfo, false, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            btnBrowseImage.Enabled = false;

            // 3. Nạp danh sách Nhóm món vào ComboBox
            LoadCategoriesToComboBox();

            // 4. Nạp danh sách món ăn lên bảng
            LoadProductList();
        }

        // Nạp nhóm món vào cả 2 ComboBox (trong khung nhập và thanh lọc)
        private void LoadCategoriesToComboBox()
        {
            var categories = _productService.GetAllCategories();

            // ComboBox chọn khi thêm/sửa món
            cboCategory.DataSource = categories;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "Id";

            // ComboBox lọc trên thanh tìm kiếm
            var filterCategories = _productService.GetAllCategories();
            filterCategories.Insert(0, new CategoryDTO { Id = 0, CategoryName = "-- Tất cả nhóm món --" });
            cboCategoryFilter.DataSource = filterCategories;
            cboCategoryFilter.DisplayMember = "CategoryName";
            cboCategoryFilter.ValueMember = "Id";
            cboCategoryFilter.SelectedIndex = 0;
        }

        // Tải danh sách món ăn lên DataGridView
        private void LoadProductList()
        {
            var list = _productService.GetAllProducts();
            dgvProduct.DataSource = list;

            // Đặt tên tiêu đề cột
            if (dgvProduct.Columns["Id"] != null) dgvProduct.Columns["Id"].HeaderText = "Mã";
            if (dgvProduct.Columns["ProductName"] != null) dgvProduct.Columns["ProductName"].HeaderText = "Tên món";
            if (dgvProduct.Columns["CategoryName"] != null) dgvProduct.Columns["CategoryName"].HeaderText = "Loại món";
            if (dgvProduct.Columns["Price"] != null)
            {
                dgvProduct.Columns["Price"].HeaderText = "Giá bán";
                dgvProduct.Columns["Price"].DefaultCellStyle.Format = "N0"; // Định dạng tiền 25,000
            }
            if (dgvProduct.Columns["Unit"] != null) dgvProduct.Columns["Unit"].HeaderText = "ĐVT";

            // Ẩn các cột kỹ thuật không cần hiển thị
            if (dgvProduct.Columns["CategoryId"] != null) dgvProduct.Columns["CategoryId"].Visible = false;
            if (dgvProduct.Columns["ImagePath"] != null) dgvProduct.Columns["ImagePath"].Visible = false;
            if (dgvProduct.Columns["IsActive"] != null) dgvProduct.Columns["IsActive"].Visible = false;
        }

        // Khi click vào 1 dòng trên bảng -> Nạp thông tin và load ảnh món ăn (Đường dẫn tương đối)
        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvProduct.Rows.Count)
            {
                var row = dgvProduct.Rows[e.RowIndex];
                txtProductId.Text = row.Cells["Id"].Value?.ToString();
                txtProductName.Text = row.Cells["ProductName"].Value?.ToString();
                cboCategory.SelectedValue = Convert.ToInt32(row.Cells["CategoryId"].Value);
                txtPrice.Text = row.Cells["Price"].Value?.ToString();
                txtUnit.Text = row.Cells["Unit"].Value?.ToString();

                // Load ảnh món ăn bằng đường dẫn tương đối qua UIHelper
                string relativePath = row.Cells["ImagePath"].Value?.ToString();
                UIHelper.LoadImageToPictureBox(picProductImage, relativePath);
                _selectedImagePath = ""; // Reset đường dẫn tạm
            }
        }

        // Nút chọn ảnh từ máy tính
        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn hình ảnh món ăn";
                ofd.Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _selectedImagePath = ofd.FileName;
                    // Hiển thị ảnh xem trước lên PictureBox
                    using (var fs = new FileStream(_selectedImagePath, FileMode.Open, FileAccess.Read))
                    {
                        picProductImage.Image = System.Drawing.Image.FromStream(fs);
                    }
                }
            }
        }

        // Nút Thêm
        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isAdding = true;
            _selectedImagePath = "";

            txtProductId.Text = "";
            txtProductName.Text = "";
            txtPrice.Text = "";
            txtUnit.Text = "Ly";
            picProductImage.Image = null;

            UIHelper.SetFormState(grpProductInfo, true, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            btnBrowseImage.Enabled = true;
            txtProductName.Focus();
        }

        // Nút Sửa
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductId.Text))
            {
                MessageBox.Show("Vui lòng chọn món ăn cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _isAdding = false;
            _selectedImagePath = "";
            UIHelper.SetFormState(grpProductInfo, true, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            btnBrowseImage.Enabled = true;
            txtProductName.Focus();
        }

        // Nút Hủy
        private void btnCancel_Click(object sender, EventArgs e)
        {
            UIHelper.SetFormState(grpProductInfo, false, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            btnBrowseImage.Enabled = false;

            if (dgvProduct.CurrentRow != null)
            {
                dgvProduct_CellClick(null, new DataGridViewCellEventArgs(0, dgvProduct.CurrentRow.Index));
            }
        }

        // Nút Lưu: Gọi Service kiểm tra hợp lệ và copy ảnh tương đối
        private void btnSave_Click(object sender, EventArgs e)
        {
            decimal price;
            if (!decimal.TryParse(txtPrice.Text.Trim(), out price))
            {
                MessageBox.Show("Đơn giá không hợp lệ (phải là số)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new ProductDTO
            {
                Id = _isAdding ? 0 : int.Parse(txtProductId.Text),
                ProductName = txtProductName.Text,
                CategoryId = Convert.ToInt32(cboCategory.SelectedValue),
                Price = price,
                Unit = txtUnit.Text,
                // Nếu không chọn ảnh mới thì giữ nguyên đường dẫn cũ
                ImagePath = dgvProduct.CurrentRow != null && !_isAdding ? dgvProduct.CurrentRow.Cells["ImagePath"].Value?.ToString() : "",
                IsActive = true
            };

            string errorMsg;
            bool success = _productService.SaveProduct(dto, _selectedImagePath, out errorMsg);

            if (success)
            {
                MessageBox.Show(_isAdding ? "Thêm món mới thành công!" : "Cập nhật món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProductList();
                UIHelper.SetFormState(grpProductInfo, false, btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
                btnBrowseImage.Enabled = false;
            }
            else
            {
                MessageBox.Show(errorMsg, "Lỗi kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nút Xóa món
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductId.Text))
            {
                MessageBox.Show("Vui lòng chọn món cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = int.Parse(txtProductId.Text);
            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa món '{txtProductName.Text}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                string errorMsg;
                bool success = _productService.DeleteProduct(id, out errorMsg);
                if (success)
                {
                    MessageBox.Show("Xóa món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProductList();
                    txtProductId.Text = "";
                    txtProductName.Text = "";
                    txtPrice.Text = "";
                    picProductImage.Image = null;
                }
                else
                {
                    MessageBox.Show(errorMsg, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // Lọc theo ComboBox danh mục
        private void cboCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategoryFilter.SelectedValue != null && int.TryParse(cboCategoryFilter.SelectedValue.ToString(), out int categoryId))
            {
                dgvProduct.DataSource = _productService.GetProductsByCategory(categoryId);
            }
        }

        // Tìm kiếm theo tên món
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearchProduct.Text.Trim();
            dgvProduct.DataSource = _productService.SearchProducts(keyword);
        }
    }
}
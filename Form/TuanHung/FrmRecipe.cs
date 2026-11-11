using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmRecipe : Form
    {
        private readonly ProductService _productService = new ProductService();
        private readonly RecipeService _recipeService = new RecipeService();

        private int _selectedProductId = 0;
        private string _selectedProductName = string.Empty;
        private bool _isAddingIngredient = false;

        public FrmRecipe()
        {
            InitializeComponent();
        }
        private void SetButtonActive(Button btn, bool isActive, Color activeColor)
        {
            btn.Enabled = isActive;
            if (isActive)
            {
                btn.BackColor = activeColor;
                btn.ForeColor = Color.White;
            }
            else
            {
                btn.BackColor = Color.FromArgb(220, 220, 220);
                btn.ForeColor = Color.FromArgb(150, 150, 150);
            }
        }

        private void SetIngredientMode(bool isEditing)
        {
            txtIngredientName.Enabled = isEditing;
            txtQuantity.Enabled = isEditing;
            cboUnit.Enabled = isEditing;
            txtNote.Enabled = isEditing;

            btnAddIng.Visible = !isEditing;
            btnEditIng.Visible = !isEditing;
            btnDeleteIng.Visible = !isEditing;

            btnSaveIng.Visible = isEditing;
            btnCancelIng.Visible = isEditing;

            if (!isEditing)
            {
                SetButtonActive(btnAddIng, true, Color.FromArgb(39, 174, 96));
                bool hasSelection = !string.IsNullOrEmpty(txtRecipeId.Text);
                SetButtonActive(btnEditIng, hasSelection, Color.FromArgb(211, 84, 0));
                SetButtonActive(btnDeleteIng, hasSelection, Color.FromArgb(192, 57, 43));
            }
        }
        private void FrmRecipe_Load(object sender, EventArgs e)
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvRecipeIngredients.AutoGenerateColumns = false;
            SetIngredientMode(false);
            LoadCategoryFilter();
            LoadProductList();
        }

        private void LoadCategoryFilter()
        {
            var categories = _productService.GetAllCategories();
            categories.Insert(0, new CategoryDTO { Id = 0, CategoryName = "-- Tất cả nhóm món --" });
            cboCategoryFilter.DataSource = categories;
            cboCategoryFilter.DisplayMember = "CategoryName";
            cboCategoryFilter.ValueMember = "Id";
            cboCategoryFilter.SelectedIndex = 0;
        }

        private void LoadProductList()
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.DataSource = null;
            dgvProducts.DataSource = _productService.GetAllProducts();

            if (dgvProducts.Rows.Count > 0)
            {
                SelectProductFromRow(0);
            }
        }

        private void SelectProductFromRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvProducts.Rows.Count) return;

            var row = dgvProducts.Rows[rowIndex];
            if (int.TryParse(row.Cells["colProdId"].Value?.ToString(), out int prodId))
            {
                _selectedProductId = prodId;
                _selectedProductName = row.Cells["colProdName"].Value?.ToString() ?? "";
                lblSelectedProduct.Text = $"ĐANG XEM CÔNG THỨC: {_selectedProductName.ToUpper()} (Mã #{_selectedProductId})";

                LoadRecipeDetails(_selectedProductId);
            }
        }

        private void LoadRecipeDetails(int productId)
        {
            dgvRecipeIngredients.AutoGenerateColumns = false;
            dgvRecipeIngredients.DataSource = null;
            dgvRecipeIngredients.DataSource = _recipeService.GetRecipesByProduct(productId);

            txtPreparationSteps.Text = _recipeService.GetPreparationSteps(productId);
            ClearIngredientInputs();
            SetIngredientMode(false);
            if (dgvRecipeIngredients.Rows.Count > 0)
            {
                PopulateIngredientFromRow(0);
            }
        }

        private void PopulateIngredientFromRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvRecipeIngredients.Rows.Count) return;

            var row = dgvRecipeIngredients.Rows[rowIndex];
            txtRecipeId.Text = row.Cells["colRecId"].Value?.ToString();
            txtIngredientName.Text = row.Cells["colIngredientName"].Value?.ToString();
            txtQuantity.Text = row.Cells["colQuantity"].Value?.ToString();
            cboUnit.Text = row.Cells["colRecUnit"].Value?.ToString();
            txtNote.Text = row.Cells["colNote"].Value?.ToString();
            SetButtonActive(btnEditIng, true, Color.FromArgb(211, 84, 0));
            SetButtonActive(btnDeleteIng, true, Color.FromArgb(192, 57, 43));
        }

        private void ClearIngredientInputs()
        {
            txtRecipeId.Clear();
            txtIngredientName.Clear();
            txtQuantity.Clear();
            cboUnit.SelectedIndex = -1;
            cboUnit.Text = string.Empty;
            txtNote.Clear();
        }

        private void cboCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategoryFilter.SelectedValue is int catId)
            {
                dgvProducts.AutoGenerateColumns = false;
                dgvProducts.DataSource = null;
                dgvProducts.DataSource = _productService.GetProductsByCategory(catId);
                if (dgvProducts.Rows.Count > 0)
                    SelectProductFromRow(0);
            }
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.DataSource = null;
            dgvProducts.DataSource = _productService.SearchProducts(txtSearchProduct.Text);
            if (dgvProducts.Rows.Count > 0)
                SelectProductFromRow(0);
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectProductFromRow(e.RowIndex);
            }
        }

        private void dgvRecipeIngredients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PopulateIngredientFromRow(e.RowIndex);
            }
        }

        private void btnAddIng_Click(object sender, EventArgs e)
        {
            if (_selectedProductId <= 0)
            {
                MessageBox.Show("Vui lòng chọn một món đồ uống bên danh sách trái trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _isAddingIngredient = true;
            ClearIngredientInputs();
            cboUnit.SelectedIndex = 0; // Mặc định "ml"
            SetIngredientMode(true);
            txtIngredientName.Focus();
        }

        private void btnEditIng_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRecipeId.Text))
            {
                MessageBox.Show("Vui lòng chọn một thành phần nguyên liệu trong bảng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _isAddingIngredient = false;
            SetIngredientMode(true);
            txtIngredientName.Focus();
        }

        private void btnDeleteIng_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtRecipeId.Text, out int recipeId) || recipeId <= 0)
            {
                MessageBox.Show("Vui lòng chọn thành phần nguyên liệu cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Xóa nguyên liệu '{txtIngredientName.Text}' khỏi công thức món '{_selectedProductName}'?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_recipeService.DeleteRecipe(recipeId, out string errorMsg))
                {
                    LoadRecipeDetails(_selectedProductId);
                }
                else
                {
                    MessageBox.Show(errorMsg, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSaveIng_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtQuantity.Text.Trim(), out double quantity))
            {
                MessageBox.Show("Định lượng phải là số hợp lệ (ví dụ: 30, 45, 2.5)!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return;
            }

            var dto = new RecipeDTO
            {
                Id = _isAddingIngredient ? 0 : (int.TryParse(txtRecipeId.Text, out int id) ? id : 0),
                ProductId = _selectedProductId,
                IngredientName = txtIngredientName.Text.Trim(),
                Quantity = quantity,
                Unit = cboUnit.Text.Trim(),
                Note = txtNote.Text.Trim()
            };

            if (_recipeService.SaveRecipe(dto, out string errorMsg))
            {
                MessageBox.Show(_isAddingIngredient ? "Đã thêm nguyên liệu vào công thức!" : "Đã cập nhật định lượng nguyên liệu!",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRecipeDetails(_selectedProductId);
                SetIngredientMode(false);
            }
            else
            {
                MessageBox.Show(errorMsg, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelIng_Click(object sender, EventArgs e)
        {
            ClearIngredientInputs();
            SetIngredientMode(false);
            if (dgvRecipeIngredients.Rows.Count > 0)
            {
                PopulateIngredientFromRow(0);
            }
        }

        private void btnSaveSteps_Click(object sender, EventArgs e)
        {
            if (_recipeService.SavePreparationSteps(_selectedProductId, txtPreparationSteps.Text, out string errorMsg))
            {
                MessageBox.Show($"Đã lưu quy trình pha chế chuẩn cho món '{_selectedProductName}'!",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(errorMsg, "Lỗi lưu quy trình", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
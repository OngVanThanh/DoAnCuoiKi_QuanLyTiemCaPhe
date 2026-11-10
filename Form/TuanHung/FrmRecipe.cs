using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Windows.Forms;

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

        private void FrmRecipe_Load(object sender, EventArgs e)
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvRecipeIngredients.AutoGenerateColumns = false;
            UIHelper.SetFormState(pnlIngredientEditor, false, btnAddIng, btnEditIng, btnDeleteIng, btnSaveIng, btnCancelIng);
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
            UIHelper.SetFormState(pnlIngredientEditor, false, btnAddIng, btnEditIng, btnDeleteIng, btnSaveIng, btnCancelIng);

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
            UIHelper.SetFormState(pnlIngredientEditor, true, btnAddIng, btnEditIng, btnDeleteIng, btnSaveIng, btnCancelIng);
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
            UIHelper.SetFormState(pnlIngredientEditor, true, btnAddIng, btnEditIng, btnDeleteIng, btnSaveIng, btnCancelIng);
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
            }
            else
            {
                MessageBox.Show(errorMsg, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelIng_Click(object sender, EventArgs e)
        {
            ClearIngredientInputs();
            UIHelper.SetFormState(pnlIngredientEditor, false, btnAddIng, btnEditIng, btnDeleteIng, btnSaveIng, btnCancelIng);
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
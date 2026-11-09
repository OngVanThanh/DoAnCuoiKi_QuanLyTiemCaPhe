using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    /// <summary>
    /// Màn hình POS Bán hàng & Đặt món tại bàn (FrmOrder).
    /// Kết hợp sơ đồ bàn trực quan, danh mục thẻ món ăn/thức uống bằng UserControl (UcProductCard),
    /// bảng tra cứu nhanh, tính tiền khuyến mãi/VAT tự động, chuyển/gộp bàn và in hóa đơn K80.
    /// </summary>
    public partial class FrmOrder : Form
    {
        private readonly OrderService _orderService = new OrderService();
        private readonly TableService _tableService = new TableService();

        private int _selectedTableId = -1;
        private string _selectedTableName = string.Empty;
        private int _selectedProductId = -1;
        private bool _isLoading = false;
        private Dictionary<int, string> _categoryNameMap = new Dictionary<int, string>();

        public FrmOrder()
        {
            InitializeComponent();
        }

        // 1. Sự kiện khi Form khởi chạy
        private void FrmOrder_Load(object sender, EventArgs e)
        {
            try
            {
                _isLoading = true;

                // Đồng bộ giao diện bảng với UIHelper của nhóm
                UIHelper.StyleDataGridView(dgvProductList);
                UIHelper.StyleDataGridView(dgvOrderDetails);

                LoadCategoriesToComboBoxAndPills();
                LoadProductsToMenuAndGrid();
                LoadTableButtons();
                UpdateOrderSummaryDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Đã xảy ra lỗi khi khởi tạo màn hình POS bán hàng:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        // 2. Tải danh sách nhóm món vào ComboBox cboCategoryFilter và tạo các nút lọc nhanh (Pills) như Mộc Cafe
        private void LoadCategoriesToComboBoxAndPills()
        {
            var categories = _orderService.GetAllCategories();
            _categoryNameMap = categories.ToDictionary(c => c.Id, c => c.CategoryName);

            var filterItems = new List<Category>
            {
                new Category { Id = 0, CategoryName = "Tất cả nhóm món" }
            };
            filterItems.AddRange(categories);

            cboCategoryFilter.DataSource = null;
            cboCategoryFilter.DisplayMember = "CategoryName";
            cboCategoryFilter.ValueMember = "Id";
            cboCategoryFilter.DataSource = filterItems;
            cboCategoryFilter.SelectedIndex = 0;

            RenderCategoryFilterPills(filterItems, 0);
        }

        // 3. Tạo thanh nút chọn nhanh nhóm món (Tất cả, Cà phê, Trà & Trà sữa, Nước ép, Bánh ngọt)
        private void RenderCategoryFilterPills(List<Category> categories, int activeCategoryId)
        {
            flpCategoryPills.SuspendLayout();
            flpCategoryPills.Controls.Clear();

            foreach (var cat in categories)
            {
                bool isActive = (cat.Id == activeCategoryId);
                Button btnPill = new Button
                {
                    Text = cat.CategoryName,
                    AutoSize = true,
                    Height = 30,
                    Margin = new Padding(0, 2, 6, 2),
                    Padding = new Padding(8, 0, 8, 0),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    Tag = cat.Id,
                    Font = new Font("Segoe UI", 8.5F, isActive ? FontStyle.Bold : FontStyle.Regular),
                    BackColor = isActive ? UIHelper.PrimaryCoffee : Color.FromArgb(242, 237, 229),
                    ForeColor = isActive ? Color.White : UIHelper.PrimaryCoffee,
                    UseMnemonic = false
                };
                btnPill.FlatAppearance.BorderSize = 0;
                btnPill.Click += CategoryPill_Click;
                flpCategoryPills.Controls.Add(btnPill);
            }

            flpCategoryPills.ResumeLayout();
        }

        // 4. Sự kiện khi nhấn vào nút Pill danh mục món
        private void CategoryPill_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is int catId)
            {
                cboCategoryFilter.SelectedValue = catId;
            }
        }

        // 5. Tải danh sách món lên cả danh mục UserControl (flpProductMenu) và bảng dgvProductList
        private void LoadProductsToMenuAndGrid()
        {
            int selectedCatId = 0;
            if (cboCategoryFilter.SelectedValue != null)
            {
                int.TryParse(cboCategoryFilter.SelectedValue.ToString(), out selectedCatId);
            }

            int? categoryFilter = selectedCatId > 0 ? (int?)selectedCatId : null;
            string keyword = txtSearchProduct.Text.Trim();
            var products = _orderService.SearchProducts(categoryFilter, keyword);

            // 5a. Nạp dữ liệu vào bảng dgvProductList (các cột đã được thiết kế sẵn trong FrmOrder.Designer.cs)
            dgvProductList.DataSource = null;
            dgvProductList.DataSource = products;

            // 5b. Nạp danh sách thẻ UserControl UcProductCard vào flpProductMenu
            flpProductMenu.SuspendLayout();
            flpProductMenu.Controls.Clear();

            foreach (var prod in products)
            {
                string catName = _categoryNameMap.ContainsKey(prod.CategoryId)
                    ? _categoryNameMap[prod.CategoryId]
                    : "Thực đơn";

                UcProductCard card = new UcProductCard();
                card.BindData(prod, catName);
                card.SetSelected(prod.Id == _selectedProductId);
                card.ProductSelected += UcProductCard_ProductSelected;
                card.ProductAddClicked += UcProductCard_ProductAddClicked;

                flpProductMenu.Controls.Add(card);
            }

            flpProductMenu.ResumeLayout();
        }

        // 6. Sự kiện khi click chọn 1 thẻ món UcProductCard trên menu
        private void UcProductCard_ProductSelected(object sender, Product product)
        {
            if (product == null)
                return;

            _selectedProductId = product.Id;

            // Đánh dấu thẻ đang chọn
            foreach (Control ctrl in flpProductMenu.Controls)
            {
                if (ctrl is UcProductCard card && card.ProductData != null)
                {
                    card.SetSelected(card.ProductData.Id == _selectedProductId);
                }
            }

            // Đồng bộ dòng tương ứng trên dgvProductList
            foreach (DataGridViewRow row in dgvProductList.Rows)
            {
                if (row.Cells["colProdId"].Value != null &&
                    Convert.ToInt32(row.Cells["colProdId"].Value) == _selectedProductId)
                {
                    row.Selected = true;
                    dgvProductList.CurrentCell = row.Cells["colProdName"];
                    break;
                }
            }
        }

        // 7. Sự kiện khi nhấn nút "+ Thêm" ngay trên thẻ món UcProductCard
        private void UcProductCard_ProductAddClicked(object sender, Product product)
        {
            if (product == null)
                return;

            _selectedProductId = product.Id;
            AddProductByIdToCurrentTable(product.Id, (int)nudQuantity.Value);
        }

        // 8. Hiển thị danh sách bàn dưới dạng các thẻ Button trực quan trên flpTableList
        private void LoadTableButtons()
        {
            flpTableList.SuspendLayout();
            flpTableList.Controls.Clear();

            List<TableDTO> tables = _tableService.GetAllTables();

            foreach (TableDTO table in tables)
            {
                Button btnTable = new Button
                {
                    Width = 124,
                    Height = 78,
                    Margin = new Padding(5),
                    Tag = table,
                    Cursor = Cursors.Hand,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Text = $"{table.TableName}\n{table.AreaName}\n[{table.Status}]",
                    FlatStyle = FlatStyle.Flat
                };

                // Tô màu trực quan theo trạng thái bàn và bàn đang được chọn
                if (table.Id == _selectedTableId)
                {
                    btnTable.BackColor = UIHelper.PrimaryCoffee;
                    btnTable.ForeColor = Color.White;
                    btnTable.FlatAppearance.BorderColor = Color.Gold;
                    btnTable.FlatAppearance.BorderSize = 2;
                }
                else if (string.Equals(table.Status, "Có khách", StringComparison.OrdinalIgnoreCase))
                {
                    btnTable.BackColor = Color.FromArgb(230, 126, 34); // Cam đậm cho bàn đang có khách
                    btnTable.ForeColor = Color.White;
                    btnTable.FlatAppearance.BorderSize = 0;
                }
                else if (string.Equals(table.Status, "Đặt trước", StringComparison.OrdinalIgnoreCase))
                {
                    btnTable.BackColor = Color.FromArgb(52, 152, 219); // Xanh dương cho bàn đặt trước
                    btnTable.ForeColor = Color.White;
                    btnTable.FlatAppearance.BorderSize = 0;
                }
                else
                {
                    btnTable.BackColor = Color.FromArgb(212, 239, 223); // Xanh lá dịu cho bàn trống
                    btnTable.ForeColor = Color.FromArgb(20, 90, 50);
                    btnTable.FlatAppearance.BorderColor = Color.FromArgb(169, 223, 191);
                    btnTable.FlatAppearance.BorderSize = 1;
                }

                btnTable.Click += TableButton_Click;
                flpTableList.Controls.Add(btnTable);
            }

            flpTableList.ResumeLayout();
        }

        // 9. Sự kiện khi nhân viên click chọn một bàn trên sơ đồ
        private void TableButton_Click(object sender, EventArgs e)
        {
            Button clickedBtn = sender as Button;
            if (clickedBtn?.Tag is TableDTO table)
            {
                _selectedTableId = table.Id;
                _selectedTableName = table.TableName;

                lblSelectedTableBanner.Text = $"Đang chọn: {table.TableName} ({table.AreaName}) · {table.Status}";
                lblOrderHeaderTitle.Text = $"ĐƠN HÀNG · {table.TableName.ToUpper()}";

                LoadTableButtons();
                LoadOrderDetailsForSelectedTable();
            }
        }

        // 10. Tải chi tiết hóa đơn hiện tại của bàn đang chọn lên dgvOrderDetails
        private void LoadOrderDetailsForSelectedTable()
        {
            if (_selectedTableId <= 0)
            {
                dgvOrderDetails.DataSource = null;
                lblOrderStatusBadge.Text = "Chưa chọn bàn";
                UpdateOrderSummaryDisplay();
                return;
            }

            List<OrderDetailDTO> details = _orderService.GetOrderDetailsByTable(_selectedTableId);
            dgvOrderDetails.DataSource = null;
            dgvOrderDetails.DataSource = details;

            lblOrderStatusBadge.Text = details.Count > 0
                ? $"Đang phục vụ ({details.Count} món)"
                : "Bàn đang trống";

            UpdateOrderSummaryDisplay();
        }

        // 11. Cập nhật hiển thị Tổng tiền hàng và Tổng thanh toán (sau Giảm giá & VAT)
        private void UpdateOrderSummaryDisplay()
        {
            if (_selectedTableId <= 0)
            {
                lblTotalAmount.Text = "Tổng tiền: 0 đ";
                lblFinalAmount.Text = "Thanh toán: 0 đ";
                return;
            }

            _orderService.CalculateOrderSummary(
                _selectedTableId,
                txtDiscount.Text,
                out decimal totalAmount,
                out decimal discountAmount,
                out decimal vatPercent,
                out decimal vatAmount,
                out decimal finalAmount,
                out _,
                out string discountInfo);

            string discountText = discountAmount > 0
                ? $" | Giảm: -{discountAmount:N0} đ ({discountInfo})"
                : string.Empty;

            string vatText = vatAmount > 0
                ? $" | VAT ({vatPercent:0.##}%): +{vatAmount:N0} đ"
                : string.Empty;

            lblTotalAmount.Text = $"Tạm tính: {totalAmount:N0} đ{discountText}{vatText}";
            lblFinalAmount.Text = $"Thanh toán: {finalAmount:N0} đ";
        }

        // 12. Sự kiện lọc món theo danh mục trên ComboBox
        private void cboCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading)
                return;

            int selectedCatId = 0;
            if (cboCategoryFilter.SelectedValue != null)
            {
                int.TryParse(cboCategoryFilter.SelectedValue.ToString(), out selectedCatId);
            }

            // Cập nhật trạng thái sáng của các nút Pill danh mục
            foreach (Control ctrl in flpCategoryPills.Controls)
            {
                if (ctrl is Button btn && btn.Tag is int pillCatId)
                {
                    bool active = (pillCatId == selectedCatId);
                    btn.BackColor = active ? UIHelper.PrimaryCoffee : Color.FromArgb(242, 237, 229);
                    btn.ForeColor = active ? Color.White : UIHelper.PrimaryCoffee;
                    btn.Font = new Font("Segoe UI", 8.5F, active ? FontStyle.Bold : FontStyle.Regular);
                }
            }

            LoadProductsToMenuAndGrid();
        }

        // 13. Sự kiện tìm kiếm nhanh tên món
        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            if (_isLoading)
                return;

            LoadProductsToMenuAndGrid();
        }

        // 14. Sự kiện click chọn dòng trên bảng dgvProductList (đồng bộ với thẻ UcProductCard)
        private void dgvProductList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvProductList.Rows[e.RowIndex].Cells["colProdId"].Value == null)
                return;

            _selectedProductId = Convert.ToInt32(dgvProductList.Rows[e.RowIndex].Cells["colProdId"].Value);

            foreach (Control ctrl in flpProductMenu.Controls)
            {
                if (ctrl is UcProductCard card && card.ProductData != null)
                {
                    card.SetSelected(card.ProductData.Id == _selectedProductId);
                }
            }
        }

        // 15. Sự kiện double-click vào món trong bảng để thêm nhanh vào bàn
        private void dgvProductList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnAddProduct_Click(sender, EventArgs.Empty);
            }
        }

        // 16. Sự kiện nút "+ Thêm món" (btnAddProduct)
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            int productId = _selectedProductId;
            if (productId <= 0 && dgvProductList.CurrentRow != null && dgvProductList.CurrentRow.Cells["colProdId"].Value != null)
            {
                productId = Convert.ToInt32(dgvProductList.CurrentRow.Cells["colProdId"].Value);
            }

            AddProductByIdToCurrentTable(productId, (int)nudQuantity.Value);
        }

        // 17. Hàm dùng chung để thêm món vào bàn đang chọn (gọi từ nút "+ Thêm" trên UcProductCard hoặc nút btnAddProduct)
        private void AddProductByIdToCurrentTable(int productId, int quantity)
        {
            if (_selectedTableId <= 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn một bàn ở cột bên trái trước khi gọi món!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (productId <= 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn một món trên thẻ Menu hoặc bảng thực đơn!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int employeeId = UserSession.CurrentUser != null ? UserSession.CurrentUser.EmployeeId : 1;

            try
            {
                bool success = _orderService.AddProductToTableOrder(
                    _selectedTableId,
                    employeeId,
                    productId,
                    quantity,
                    out string message);

                if (!success)
                {
                    MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                nudQuantity.Value = 1;
                LoadTableButtons();
                LoadOrderDetailsForSelectedTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi thêm món vào hóa đơn:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // 18. Sự kiện nút "Xóa món" (btnRemoveProduct)
        private void btnRemoveProduct_Click(object sender, EventArgs e)
        {
            if (_selectedTableId <= 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn bàn cần thao tác!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (dgvOrderDetails.CurrentRow == null || dgvOrderDetails.CurrentRow.Cells["colDetailProductId"].Value == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn món cần xóa trong bảng chi tiết hóa đơn!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int productId = Convert.ToInt32(dgvOrderDetails.CurrentRow.Cells["colDetailProductId"].Value);
            string productName = dgvOrderDetails.CurrentRow.Cells["colDetailProductName"].Value?.ToString() ?? "món này";

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa '{productName}' khỏi hóa đơn của {_selectedTableName}?",
                "Xác nhận xóa món",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                bool success = _orderService.RemoveProductFromTableOrder(
                    _selectedTableId,
                    productId,
                    out string message);

                if (!success)
                {
                    MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                LoadTableButtons();
                LoadOrderDetailsForSelectedTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi xóa món:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // 19. Sự kiện khi nhập mã khuyến mãi hoặc số tiền giảm giá (txtDiscount)
        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (_isLoading)
                return;

            UpdateOrderSummaryDisplay();
        }

        // 20. Sự kiện nút "Chuyển / Gộp bàn" (btnSwitchTable)
        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            if (_selectedTableId <= 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn bàn nguồn đang có khách trước khi chuyển/gộp bàn!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var currentOrder = _orderService.GetUnpaidOrderByTable(_selectedTableId);
            if (currentOrder == null)
            {
                MessageBox.Show(
                    $"'{_selectedTableName}' hiện đang trống, không có hóa đơn để chuyển hoặc gộp!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var allTables = _tableService.GetAllTables();
            var targetCandidates = allTables.FindAll(t => t.Id != _selectedTableId);

            if (targetCandidates.Count == 0)
            {
                MessageBox.Show("Không có bàn đích nào khác để chuyển/gộp!", "Thông báo");
                return;
            }

            using (Form dialog = new Form())
            {
                dialog.Text = $"Chuyển / Gộp từ {_selectedTableName}";
                dialog.Size = new Size(390, 195);
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;
                dialog.BackColor = UIHelper.LightCream;

                Label lblPrompt = new Label
                {
                    Text = "Chọn bàn đích (Bàn trống -> Chuyển bàn | Bàn có khách -> Gộp bàn):",
                    Location = new Point(16, 18),
                    AutoSize = true
                };

                ComboBox cboTarget = new ComboBox
                {
                    Location = new Point(16, 48),
                    Width = 340,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    DisplayMember = "TableName",
                    ValueMember = "Id",
                    DataSource = targetCandidates
                };

                Button btnConfirm = new Button
                {
                    Text = "Thực hiện",
                    DialogResult = DialogResult.OK,
                    BackColor = UIHelper.SuccessColor,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Location = new Point(180, 96),
                    Size = new Size(85, 34)
                };

                Button btnCancel = new Button
                {
                    Text = "Hủy",
                    DialogResult = DialogResult.Cancel,
                    BackColor = UIHelper.GrayColor,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Location = new Point(271, 96),
                    Size = new Size(85, 34)
                };

                dialog.Controls.AddRange(new Control[] { lblPrompt, cboTarget, btnConfirm, btnCancel });
                dialog.AcceptButton = btnConfirm;
                dialog.CancelButton = btnCancel;

                if (dialog.ShowDialog(this) == DialogResult.OK && cboTarget.SelectedValue != null)
                {
                    int targetTableId = Convert.ToInt32(cboTarget.SelectedValue);
                    var targetDto = cboTarget.SelectedItem as TableDTO;

                    bool success = _orderService.SwitchOrMergeTable(
                        _selectedTableId,
                        targetTableId,
                        out string message);

                    MessageBox.Show(
                        message,
                        success ? "Thông báo" : "Cảnh báo",
                        MessageBoxButtons.OK,
                        success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                    if (success)
                    {
                        _selectedTableId = targetTableId;
                        _selectedTableName = targetDto != null ? targetDto.TableName : $"Bàn #{targetTableId}";
                        lblSelectedTableBanner.Text = $"Đang chọn: {_selectedTableName} · Có khách";
                        LoadTableButtons();
                        LoadOrderDetailsForSelectedTable();
                    }
                }
            }
        }

        // 21. Sự kiện nút "Thanh toán & In hóa đơn" (btnPayAndPrint)
        private void btnPayAndPrint_Click(object sender, EventArgs e)
        {
            if (_selectedTableId <= 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn bàn cần thanh toán!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var currentDetails = _orderService.GetOrderDetailsByTable(_selectedTableId);
            if (currentDetails == null || currentDetails.Count == 0)
            {
                MessageBox.Show(
                    $"'{_selectedTableName}' hiện chưa có món nào để thanh toán!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Xác nhận thanh toán hóa đơn cho '{_selectedTableName}'?\n({lblFinalAmount.Text})",
                "Xác nhận thanh toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                bool success = _orderService.CheckoutTableOrder(
                    _selectedTableId,
                    txtDiscount.Text,
                    out OrderDTO paidOrder,
                    out List<OrderDetailDTO> orderDetails,
                    out string message);

                if (!success)
                {
                    MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                txtDiscount.Clear();
                LoadTableButtons();
                LoadOrderDetailsForSelectedTable();

                // Mở màn hình Xem trước & In hóa đơn K80 (FrmPrintReview)
                using (FrmPrintReview frmPrint = new FrmPrintReview(paidOrder, orderDetails))
                {
                    frmPrint.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Đã xảy ra lỗi trong quá trình thanh toán:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void lblHeaderTitle_Click(object sender, EventArgs e)
        {

        }
    }
}

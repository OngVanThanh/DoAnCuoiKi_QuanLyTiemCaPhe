using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    /// <summary>
    /// Màn hình Bán hàng & Gọi món tại bàn (FrmOrder).
    /// Quản lý sơ đồ bàn trực quan, lọc/tìm kiếm món, thêm/xóa món trong hóa đơn,
    /// áp dụng khuyến mãi, chuyển/gộp bàn và thanh toán kèm in hóa đơn K80.
    /// </summary>
    public partial class FrmOrder : Form
    {
        private readonly OrderService _orderService = new OrderService();
        private readonly TableService _tableService = new TableService();

        private int _selectedTableId = -1;
        private string _selectedTableName = string.Empty;
        private bool _isLoading = false;

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
                LoadCategoriesToComboBox();
                LoadProductsToGrid();
                LoadTableButtons();
                UpdateOrderSummaryDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Đã xảy ra lỗi khi khởi tạo màn hình bán hàng:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        // 2. Tải danh sách nhóm món vào ComboBox cboCategoryFilter (có mục "Tất cả nhóm món")
        private void LoadCategoriesToComboBox()
        {
            var categories = _orderService.GetAllCategories();
            var filterItems = new List<Category>
            {
                new Category { Id = 0, CategoryName = "-- Tất cả nhóm món --" }
            };
            filterItems.AddRange(categories);

            cboCategoryFilter.DataSource = null;
            cboCategoryFilter.DisplayMember = "CategoryName";
            cboCategoryFilter.ValueMember = "Id";
            cboCategoryFilter.DataSource = filterItems;
            cboCategoryFilter.SelectedIndex = 0;
        }

        // 3. Tải danh sách món đang kinh doanh vào DataGridView dgvProductList
        private void LoadProductsToGrid()
        {
            int? categoryId = null;
            if (cboCategoryFilter.SelectedValue != null &&
                int.TryParse(cboCategoryFilter.SelectedValue.ToString(), out int selectedCatId) &&
                selectedCatId > 0)
            {
                categoryId = selectedCatId;
            }

            string keyword = txtSearchProduct.Text.Trim();
            var products = _orderService.SearchProducts(categoryId, keyword);

            dgvProductList.DataSource = null;
            dgvProductList.DataSource = products;

            if (dgvProductList.Columns.Contains("Id"))
                dgvProductList.Columns["Id"].HeaderText = "Mã";

            if (dgvProductList.Columns.Contains("ProductName"))
                dgvProductList.Columns["ProductName"].HeaderText = "Tên món";

            if (dgvProductList.Columns.Contains("Price"))
            {
                dgvProductList.Columns["Price"].HeaderText = "Đơn giá";
                dgvProductList.Columns["Price"].DefaultCellStyle.Format = "N0";
            }

            if (dgvProductList.Columns.Contains("Unit"))
                dgvProductList.Columns["Unit"].HeaderText = "ĐVT";

            // Ẩn các cột kỹ thuật của LINQ to SQL Entity
            string[] hiddenCols = { "CategoryId", "ImagePath", "IsActive", "Category", "OrderDetails" };
            foreach (string col in hiddenCols)
            {
                if (dgvProductList.Columns.Contains(col))
                    dgvProductList.Columns[col].Visible = false;
            }

            dgvProductList.ReadOnly = true;
            dgvProductList.AllowUserToAddRows = false;
            dgvProductList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductList.MultiSelect = false;
            dgvProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // 4. Hiển thị danh sách bàn dưới dạng các Button trực quan trên FlowLayoutPanel flpTableList
        private void LoadTableButtons()
        {
            flpTableList.SuspendLayout();
            flpTableList.Controls.Clear();

            List<TableDTO> tables = _tableService.GetAllTables();

            foreach (TableDTO table in tables)
            {
                Button btnTable = new Button
                {
                    Width = 132,
                    Height = 76,
                    Margin = new Padding(5),
                    Tag = table,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Text = $"{table.TableName}\n({table.Status})",
                    FlatStyle = FlatStyle.Flat
                };

                // Tô màu trực quan theo trạng thái bàn và bàn đang được chọn
                if (table.Id == _selectedTableId)
                {
                    btnTable.BackColor = Color.FromArgb(41, 128, 185);
                    btnTable.ForeColor = Color.White;
                    btnTable.FlatAppearance.BorderColor = Color.Navy;
                    btnTable.FlatAppearance.BorderSize = 2;
                }
                else if (string.Equals(table.Status, "Có khách", StringComparison.OrdinalIgnoreCase))
                {
                    btnTable.BackColor = Color.FromArgb(243, 156, 18); // Cam nổi bật cho bàn đang có khách
                    btnTable.ForeColor = Color.White;
                }
                else if (string.Equals(table.Status, "Đặt trước", StringComparison.OrdinalIgnoreCase))
                {
                    btnTable.BackColor = Color.FromArgb(174, 214, 241); // Xanh nhạt cho bàn đặt trước
                    btnTable.ForeColor = Color.Black;
                }
                else
                {
                    btnTable.BackColor = Color.FromArgb(212, 239, 223); // Xanh lá nhạt cho bàn trống
                    btnTable.ForeColor = Color.DarkGreen;
                }

                btnTable.Click += TableButton_Click;
                flpTableList.Controls.Add(btnTable);
            }

            flpTableList.ResumeLayout();
        }

        // 5. Sự kiện khi nhân viên click chọn một bàn trên sơ đồ
        private void TableButton_Click(object sender, EventArgs e)
        {
            Button clickedBtn = sender as Button;
            if (clickedBtn?.Tag is TableDTO table)
            {
                _selectedTableId = table.Id;
                _selectedTableName = table.TableName;
                this.Text = $"Hệ thống Bán hàng & Gọi món - Đang chọn: {_selectedTableName} ({table.AreaName})";

                LoadTableButtons();
                LoadOrderDetailsForSelectedTable();
            }
        }

        // 6. Tải chi tiết hóa đơn hiện tại của bàn đang chọn lên dgvOrderDetails
        private void LoadOrderDetailsForSelectedTable()
        {
            if (_selectedTableId <= 0)
            {
                dgvOrderDetails.DataSource = null;
                UpdateOrderSummaryDisplay();
                return;
            }

            List<OrderDetailDTO> details = _orderService.GetOrderDetailsByTable(_selectedTableId);
            dgvOrderDetails.DataSource = null;
            dgvOrderDetails.DataSource = details;

            if (dgvOrderDetails.Columns.Contains("Id"))
                dgvOrderDetails.Columns["Id"].Visible = false;

            if (dgvOrderDetails.Columns.Contains("OrderId"))
                dgvOrderDetails.Columns["OrderId"].Visible = false;

            if (dgvOrderDetails.Columns.Contains("ProductId"))
                dgvOrderDetails.Columns["ProductId"].Visible = false;

            if (dgvOrderDetails.Columns.Contains("ProductName"))
                dgvOrderDetails.Columns["ProductName"].HeaderText = "Tên món";

            if (dgvOrderDetails.Columns.Contains("Unit"))
                dgvOrderDetails.Columns["Unit"].HeaderText = "ĐVT";

            if (dgvOrderDetails.Columns.Contains("Quantity"))
                dgvOrderDetails.Columns["Quantity"].HeaderText = "SL";

            if (dgvOrderDetails.Columns.Contains("UnitPrice"))
            {
                dgvOrderDetails.Columns["UnitPrice"].HeaderText = "Đơn giá";
                dgvOrderDetails.Columns["UnitPrice"].DefaultCellStyle.Format = "N0";
            }

            if (dgvOrderDetails.Columns.Contains("TotalPrice"))
            {
                dgvOrderDetails.Columns["TotalPrice"].HeaderText = "Thành tiền";
                dgvOrderDetails.Columns["TotalPrice"].DefaultCellStyle.Format = "N0";
            }

            dgvOrderDetails.ReadOnly = true;
            dgvOrderDetails.AllowUserToAddRows = false;
            dgvOrderDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrderDetails.MultiSelect = false;
            dgvOrderDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            UpdateOrderSummaryDisplay();
        }

        // 7. Cập nhật hiển thị Tổng tiền hàng và Tổng thanh toán (sau Giảm giá & VAT)
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

            lblTotalAmount.Text = $"Tổng tiền: {totalAmount:N0} đ{discountText}{vatText}";
            lblFinalAmount.Text = $"Thanh toán: {finalAmount:N0} đ";
        }

        // 8. Sự kiện lọc món theo danh mục
        private void cboCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading)
                return;

            LoadProductsToGrid();
        }

        // 9. Sự kiện tìm kiếm nhanh tên món
        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            if (_isLoading)
                return;

            LoadProductsToGrid();
        }

        // 10. Sự kiện double-click vào món trong thực đơn để thêm nhanh vào bàn
        private void dgvProductList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnAddProduct_Click(sender, EventArgs.Empty);
            }
        }

        // 11. Sự kiện nút "Thêm món" (btnAddProduct)
        private void btnAddProduct_Click(object sender, EventArgs e)
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

            if (dgvProductList.CurrentRow == null || dgvProductList.CurrentRow.Cells["Id"].Value == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn một món trong danh sách thực đơn!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int productId = Convert.ToInt32(dgvProductList.CurrentRow.Cells["Id"].Value);
            int quantity = (int)nudQuantity.Value;
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

        // 12. Sự kiện nút "Xóa món" (btnRemoveProduct)
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

            if (dgvOrderDetails.CurrentRow == null || dgvOrderDetails.CurrentRow.Cells["ProductId"].Value == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn món cần xóa trong bảng chi tiết hóa đơn!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int productId = Convert.ToInt32(dgvOrderDetails.CurrentRow.Cells["ProductId"].Value);
            string productName = dgvOrderDetails.CurrentRow.Cells["ProductName"].Value?.ToString() ?? "món này";

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

        // 13. Sự kiện khi nhập mã khuyến mãi hoặc số tiền giảm giá (txtDiscount)
        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (_isLoading)
                return;

            UpdateOrderSummaryDisplay();
        }

        // 14. Sự kiện nút "Chuyển / Gộp bàn" (btnSwitchTable)
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

            // Hiển thị hộp thoại chọn bàn đích
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
                dialog.Size = new Size(380, 190);
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;

                Label lblPrompt = new Label
                {
                    Text = "Chọn bàn đích (Bàn trống -> Chuyển bàn | Bàn có khách -> Gộp bàn):",
                    Location = new Point(16, 18),
                    AutoSize = true
                };

                ComboBox cboTarget = new ComboBox
                {
                    Location = new Point(16, 48),
                    Width = 330,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    DisplayMember = "TableName",
                    ValueMember = "Id",
                    DataSource = targetCandidates
                };

                Button btnConfirm = new Button
                {
                    Text = "Thực hiện",
                    DialogResult = DialogResult.OK,
                    Location = new Point(170, 95),
                    Size = new Size(85, 32)
                };

                Button btnCancel = new Button
                {
                    Text = "Hủy",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(261, 95),
                    Size = new Size(85, 32)
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
                        LoadTableButtons();
                        LoadOrderDetailsForSelectedTable();
                    }
                }
            }
        }

        // 15. Sự kiện nút "Thanh toán & In hóa đơn" (btnPayAndPrint)
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

                // Làm mới lại giao diện bàn và hóa đơn sau khi thanh toán thành công
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
    }
}

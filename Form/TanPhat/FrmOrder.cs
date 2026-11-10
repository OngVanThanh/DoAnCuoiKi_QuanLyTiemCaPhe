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
    public partial class FrmOrder : Form
    {
        private readonly OrderService _orderService = new OrderService();
        private readonly TableService _tableService = new TableService();
        private readonly CustomerService _customerService = new CustomerService();
        private readonly PromotionService _promotionService = new PromotionService();

        private int _selectedTableId = 1;
        private string _selectedTableName = "Khách tại quầy / Mang đi";
        private int _selectedProductId = -1;
        private bool _isLoading = false;
        private bool _isSelectingSuggestion = false;

        private CustomerDTO _selectedCustomer = null;
        private List<CustomerDTO> _cachedCustomers = new List<CustomerDTO>();
        private List<PromotionDTO> _cachedPromotions = new List<PromotionDTO>();
        private Dictionary<int, string> _categoryNameMap = new Dictionary<int, string>();

        public FrmOrder()
        {
            InitializeComponent();
        }

        private void FrmOrder_Load(object sender, EventArgs e)
        {
            try
            {
                _isLoading = true;
                dgvOrderDetails.AutoGenerateColumns = false;

                dtpStartTime.Value = DateTime.Now;
                dtpEndTime.Value = DateTime.Now.AddHours(2);
                txtReserveArea.Text = "Tầng 1 - Máy lạnh";

                LoadAreaAutoComplete();
                LoadCustomerAndPromoCache();
                LoadCategoriesToComboBox();
                LoadProductsToMenu();

                _tableService.AutoCleanupExpiredReservations();
                SelectCounterMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo màn hình POS:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        // Gợi ý tên khu vực từng dùng nhưng vẫn cho phép gõ tự do bất kỳ khu vực nào của quán
        private void LoadAreaAutoComplete()
        {
            var areas = _tableService.GetAllAreaDTOs();
            var collection = new AutoCompleteStringCollection();
            foreach (var area in areas)
            {
                if (!string.IsNullOrWhiteSpace(area.AreaName))
                {
                    collection.Add(area.AreaName);
                }
            }
            txtReserveArea.AutoCompleteCustomSource = collection;
        }

        // Nạp danh sách khách hàng và mã khuyến mãi để đề xuất tức thời khi gõ từng ký tự
        private void LoadCustomerAndPromoCache()
        {
            try
            {
                _cachedCustomers = _customerService.GetAll() ?? new List<CustomerDTO>();
            }
            catch
            {
                _cachedCustomers = new List<CustomerDTO>();
            }

            try
            {
                _cachedPromotions = (_promotionService.GetAllPromotions() ?? new List<PromotionDTO>())
                    .Where(p => p.IsValidNow)
                    .ToList();
            }
            catch
            {
                _cachedPromotions = new List<PromotionDTO>();
            }
        }

        private void LoadCategoriesToComboBox()
        {
            var categories = _orderService.GetAllCategories();
            _categoryNameMap = categories.ToDictionary(c => c.Id, c => c.CategoryName);

            var filterItems = new List<Category>
            {
                new Category { Id = 0, CategoryName = "-- Tất cả nhóm món --" }
            };
            filterItems.AddRange(categories);

            cboCategoryFilter.DisplayMember = "CategoryName";
            cboCategoryFilter.ValueMember = "Id";
            cboCategoryFilter.DataSource = filterItems;
            cboCategoryFilter.SelectedIndex = 0;
        }

        private void LoadProductsToMenu()
        {
            int selectedCatId = 0;
            if (cboCategoryFilter.SelectedValue != null)
            {
                int.TryParse(cboCategoryFilter.SelectedValue.ToString(), out selectedCatId);
            }

            int? categoryFilter = selectedCatId > 0 ? (int?)selectedCatId : null;
            string keyword = txtSearchProduct.Text.Trim();
            var products = _orderService.SearchProducts(categoryFilter, keyword);

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

        private void LoadReservedTableCards()
        {
            var reservedTables = _tableService.GetActiveReservedOrOccupiedTables();
            lblReservedListHeader.Text = $"DANH SÁCH BÀN ĐẶT TRƯỚC ({reservedTables.Count})";

            flpTableList.SuspendLayout();
            flpTableList.Controls.Clear();

            foreach (var table in reservedTables)
            {
                UcReservedTableCard card = new UcReservedTableCard();
                card.BindData(table, table.Id == _selectedTableId);
                card.TableSelected += UcReservedTableCard_TableSelected;
                card.TableCancelClicked += UcReservedTableCard_TableCancelClicked;
                flpTableList.Controls.Add(card);
            }

            flpTableList.ResumeLayout();
        }

        private void SelectCounterMode()
        {
            _selectedTableId = 1;
            _selectedTableName = "Khách tại quầy / Mang đi";
            btnCounterOrder.BackColor = Color.FromArgb(39, 174, 96);

            lblSelectedTableBanner.Text = "Đang chọn: Khách tại quầy / Mang đi";
            lblOrderHeaderTitle.Text = "ĐƠN HÀNG · KHÁCH TẠI QUẦY";

            LoadReservedTableCards();
            LoadOrderDetailsForSelectedTable();
        }

        private void btnCounterOrder_Click(object sender, EventArgs e)
        {
            SelectCounterMode();
        }

        private void UcReservedTableCard_TableSelected(object sender, TableDTO table)
        {
            if (table == null) return;

            _selectedTableId = table.Id;
            _selectedTableName = table.TableName;
            btnCounterOrder.BackColor = Color.FromArgb(127, 140, 141);

            string timeNote = table.StartTime.HasValue && table.EndTime.HasValue
                ? $" ({table.StartTime:HH:mm}-{table.EndTime:HH:mm})"
                : "";

            lblSelectedTableBanner.Text = $"Đang chọn: {table.TableName}{timeNote}";
            lblOrderHeaderTitle.Text = $"ĐƠN HÀNG · {table.TableName.ToUpper()}";

            LoadReservedTableCards();
            LoadOrderDetailsForSelectedTable();
        }

        private void UcReservedTableCard_TableCancelClicked(object sender, TableDTO table)
        {
            if (table == null) return;

            if (MessageBox.Show($"Bạn có chắc muốn hủy bàn đặt trước '{table.TableName}'?",
                "Xác nhận hủy bàn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_tableService.DeleteTable(table.Id, out string msg))
                {
                    if (_selectedTableId == table.Id)
                    {
                        SelectCounterMode();
                    }
                    else
                    {
                        LoadReservedTableCards();
                    }
                }
                else
                {
                    MessageBox.Show(msg, "Không thể hủy bàn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnAddReservedTable_Click(object sender, EventArgs e)
        {
            if (_tableService.AddReservedTable(
                txtReserveTableName.Text,
                txtReserveArea.Text,
                dtpStartTime.Value,
                dtpEndTime.Value,
                out string message))
            {
                txtReserveTableName.Clear();
                dtpStartTime.Value = DateTime.Now;
                dtpEndTime.Value = DateTime.Now.AddHours(2);
                LoadAreaAutoComplete();
                LoadReservedTableCards();
            }
            else
            {
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tmrAutoExpire_Tick(object sender, EventArgs e)
        {
            int expiredCount = _tableService.AutoCleanupExpiredReservations();
            if (expiredCount > 0)
            {
                var stillExists = _tableService.GetTableById(_selectedTableId);
                if (_selectedTableId > 1 && (stillExists == null || stillExists.Status == "Trống"))
                {
                    SelectCounterMode();
                }
                else
                {
                    LoadReservedTableCards();
                }
            }
        }

        private void UcProductCard_ProductSelected(object sender, Product product)
        {
            if (product == null) return;
            _selectedProductId = product.Id;

            foreach (Control ctrl in flpProductMenu.Controls)
            {
                if (ctrl is UcProductCard card && card.ProductData != null)
                {
                    card.SetSelected(card.ProductData.Id == _selectedProductId);
                }
            }
        }

        private void UcProductCard_ProductAddClicked(object sender, Product product)
        {
            if (product == null) return;
            _selectedProductId = product.Id;
            AddProductByIdToCurrentTable(product.Id, (int)nudQuantity.Value);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            AddProductByIdToCurrentTable(_selectedProductId, (int)nudQuantity.Value);
        }

        private void AddProductByIdToCurrentTable(int productId, int quantity)
        {
            if (_selectedTableId <= 0)
            {
                SelectCounterMode();
            }

            if (productId <= 0)
            {
                MessageBox.Show("Vui lòng chọn một món trên thực đơn trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int employeeId = UserSession.CurrentUser != null ? UserSession.CurrentUser.EmployeeId : 1;

            if (_orderService.AddProductToTableOrder(_selectedTableId, employeeId, productId, quantity, out string message))
            {
                nudQuantity.Value = 1;
                LoadReservedTableCards();
                LoadOrderDetailsForSelectedTable();
            }
            else
            {
                MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadOrderDetailsForSelectedTable()
        {
            List<OrderDetailDTO> details = _orderService.GetOrderDetailsByTable(_selectedTableId);
            dgvOrderDetails.AutoGenerateColumns = false;
            dgvOrderDetails.DataSource = null;
            dgvOrderDetails.DataSource = details;

            lblOrderStatusBadge.Text = details.Count > 0
                ? $"Đang gọi ({details.Count} món)"
                : "Chưa có món trong đơn";

            UpdateOrderSummaryDisplay();
        }

        private void UpdateOrderSummaryDisplay()
        {
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

        private void cboCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;
            LoadProductsToMenu();
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;
            LoadProductsToMenu();
        }

        // =====================================================================
        // TÌM KIẾM & ĐỀ XUẤT TRỰC TIẾP KHÁCH HÀNG (NHẬP ĐẾN ĐÂU ĐỀ XUẤT ĐẾN ĐÓ)
        // =====================================================================
        private void txtCustomerPhone_TextChanged(object sender, EventArgs e)
        {
            if (_isLoading || _isSelectingSuggestion) return;

            string kw = txtCustomerPhone.Text.Trim();
            if (string.IsNullOrEmpty(kw))
            {
                _selectedCustomer = null;
                lstCustomerSuggest.Visible = false;
                lblCustomerInfo.ForeColor = Color.DimGray;
                lblCustomerInfo.Text = "Khách lẻ (Gõ SĐT hoặc tên khách để gợi ý)";
                return;
            }

            if (_cachedCustomers.Count == 0)
            {
                LoadCustomerAndPromoCache();
            }

            var matches = _cachedCustomers
                .Where(c => (c.Phone != null && c.Phone.Contains(kw)) ||
                            (c.CustomerName != null && c.CustomerName.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0))
                .Take(6)
                .ToList();

            var exact = matches.FirstOrDefault(c => string.Equals(c.Phone, kw, StringComparison.OrdinalIgnoreCase));
            if (exact != null)
            {
                _selectedCustomer = exact;
                lblCustomerInfo.ForeColor = Color.FromArgb(39, 174, 96);
                lblCustomerInfo.Text = $"✔ {exact.CustomerName} · Hạng {exact.MembershipTier} ({exact.RewardPoints} điểm)";
            }
            else if (matches.Count > 0)
            {
                _selectedCustomer = matches[0];
                lblCustomerInfo.ForeColor = Color.FromArgb(41, 128, 185);
                lblCustomerInfo.Text = $"Gợi ý: {matches[0].CustomerName} ({matches[0].Phone}) - Click để chọn";
            }
            else
            {
                _selectedCustomer = null;
                lblCustomerInfo.ForeColor = Color.FromArgb(192, 57, 43);
                lblCustomerInfo.Text = "✖ Không có khách hàng khớp từ khóa";
            }

            if (matches.Count > 0 && exact == null)
            {
                lstCustomerSuggest.Items.Clear();
                foreach (var c in matches)
                {
                    lstCustomerSuggest.Items.Add($"{c.Phone} — {c.CustomerName} ({c.MembershipTier} · {c.RewardPoints}đ)");
                }
                lstCustomerSuggest.Tag = matches;
                lstCustomerSuggest.BringToFront();
                lstCustomerSuggest.Visible = true;
            }
            else
            {
                lstCustomerSuggest.Visible = false;
            }
        }

        private void lstCustomerSuggest_Click(object sender, EventArgs e)
        {
            if (lstCustomerSuggest.SelectedIndex >= 0 && lstCustomerSuggest.Tag is List<CustomerDTO> matches)
            {
                var chosen = matches[lstCustomerSuggest.SelectedIndex];
                _isSelectingSuggestion = true;
                _selectedCustomer = chosen;
                txtCustomerPhone.Text = chosen.Phone;
                lblCustomerInfo.ForeColor = Color.FromArgb(39, 174, 96);
                lblCustomerInfo.Text = $"✔ {chosen.CustomerName} · Hạng {chosen.MembershipTier} ({chosen.RewardPoints} điểm)";
                lstCustomerSuggest.Visible = false;
                _isSelectingSuggestion = false;
            }
        }

        private void txtCustomerPhone_Leave(object sender, EventArgs e)
        {
            if (!lstCustomerSuggest.Focused)
            {
                lstCustomerSuggest.Visible = false;
            }
        }

        // =====================================================================
        // TÌM KIẾM & ĐỀ XUẤT TRỰC TIẾP MÃ GIẢM GIÁ (NHẬP ĐẾN ĐÂU ĐỀ XUẤT ĐẾN ĐÓ)
        // =====================================================================
        private void txtDiscount_Enter(object sender, EventArgs e)
        {
            ShowPromoSuggestions(txtDiscount.Text.Trim());
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;

            if (!_isSelectingSuggestion)
            {
                ShowPromoSuggestions(txtDiscount.Text.Trim());
            }

            UpdateOrderSummaryDisplay();
        }

        private void ShowPromoSuggestions(string keyword)
        {
            if (_cachedPromotions.Count == 0)
            {
                LoadCustomerAndPromoCache();
            }

            var matches = _cachedPromotions
                .Where(p => string.IsNullOrEmpty(keyword) ||
                            (p.PromoCode != null && p.PromoCode.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                .Take(5)
                .ToList();

            bool isExactMatch = matches.Any(p => string.Equals(p.PromoCode, keyword, StringComparison.OrdinalIgnoreCase));

            if (matches.Count > 0 && !isExactMatch)
            {
                lstPromoSuggest.Items.Clear();
                foreach (var p in matches)
                {
                    lstPromoSuggest.Items.Add($"{p.PromoCode} — Giảm {p.DiscountPercent}%");
                }
                lstPromoSuggest.Tag = matches;
                lstPromoSuggest.BringToFront();
                lstPromoSuggest.Visible = true;
            }
            else
            {
                lstPromoSuggest.Visible = false;
            }
        }

        private void lstPromoSuggest_Click(object sender, EventArgs e)
        {
            if (lstPromoSuggest.SelectedIndex >= 0 && lstPromoSuggest.Tag is List<PromotionDTO> matches)
            {
                var chosen = matches[lstPromoSuggest.SelectedIndex];
                _isSelectingSuggestion = true;
                txtDiscount.Text = chosen.PromoCode;
                lstPromoSuggest.Visible = false;
                _isSelectingSuggestion = false;
                UpdateOrderSummaryDisplay();
            }
        }

        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            if (!lstPromoSuggest.Focused)
            {
                lstPromoSuggest.Visible = false;
            }
        }

        private void btnRemoveProduct_Click(object sender, EventArgs e)
        {
            if (dgvOrderDetails.CurrentRow == null || dgvOrderDetails.CurrentRow.Cells["colDetailProductId"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn món cần xóa trong bảng hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int productId = Convert.ToInt32(dgvOrderDetails.CurrentRow.Cells["colDetailProductId"].Value);
            string productName = dgvOrderDetails.CurrentRow.Cells["colDetailProductName"].Value?.ToString() ?? "món này";

            if (MessageBox.Show($"Xóa '{productName}' khỏi đơn hàng hiện tại?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_orderService.RemoveProductFromTableOrder(_selectedTableId, productId, out string message))
                {
                    LoadReservedTableCards();
                    LoadOrderDetailsForSelectedTable();
                }
                else
                {
                    MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnPayAndPrint_Click(object sender, EventArgs e)
        {
            var currentDetails = _orderService.GetOrderDetailsByTable(_selectedTableId);
            if (currentDetails == null || currentDetails.Count == 0)
            {
                MessageBox.Show("Đơn hàng hiện tại chưa có món nào để thanh toán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Xác nhận thanh toán cho '{_selectedTableName}'?\n({lblFinalAmount.Text})",
                "Xác nhận thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

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

                if (_selectedCustomer != null && paidOrder != null && paidOrder.FinalAmount >= 10000)
                {
                    int addedPoints = (int)(paidOrder.FinalAmount / 10000m);
                    _selectedCustomer.RewardPoints += addedPoints;
                    _customerService.Update(_selectedCustomer, out _);
                    LoadCustomerAndPromoCache();
                }

                if (_selectedTableId > 1)
                {
                    _tableService.DeleteTable(_selectedTableId, out _);
                }

                txtDiscount.Clear();
                txtCustomerPhone.Clear();
                lstCustomerSuggest.Visible = false;
                lstPromoSuggest.Visible = false;
                _selectedCustomer = null;
                lblCustomerInfo.Text = "Khách lẻ (Gõ SĐT hoặc tên khách để gợi ý)";

                SelectCounterMode();

                using (FrmPrintReview frmPrint = new FrmPrintReview(paidOrder, orderDetails))
                {
                    frmPrint.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thanh toán:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
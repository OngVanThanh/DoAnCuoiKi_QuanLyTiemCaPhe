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

                lblHeaderTitle.Text = $"{ConfigHelper.GetShopName().ToUpper()} · POS BÁN HÀNG";
                dtpStartTime.Value = DateTime.Now;
                dtpEndTime.Value = DateTime.Now.AddHours(2);
                cboReserveArea.Text = "Tầng 1 - Máy lạnh";

                LoadAreaComboBox();
                LoadCustomerAndPromoCache();
                LoadPromotionComboBox();
                LoadCategoriesToComboBox();
                LoadProductsToMenu();

                // Đăng ký sự kiện co giãn tự động cho lưới thẻ món (Responsive Grid 3-4 cột)
                flpProductMenu.Resize += (s, ev) => UpdateProductCardSizes();

                _tableService.AutoCleanupExpiredReservations();
                SelectCounterMode();
            }
            catch (Exception ex)
            {
                // Tự động phục hồi sang chế độ an toàn để thu ngân không bị gián đoạn bán hàng
                try
                {
                    SelectCounterMode();
                }
                catch { }
                Console.WriteLine("Cảnh báo nạp POS: " + ex.Message);
            }
            finally
            {
                _isLoading = false;
            }
        }

        // Nạp danh sách khu vực vào ComboBox (cho phép chọn hoặc gõ thêm khu vực mới linh hoạt)
        private void LoadAreaComboBox()
        {
            string currentText = cboReserveArea.Text.Trim();
            var areas = _tableService.GetAllAreaDTOs();
            cboReserveArea.Items.Clear();
            foreach (var area in areas)
            {
                if (!string.IsNullOrWhiteSpace(area.AreaName))
                {
                    cboReserveArea.Items.Add(area.AreaName);
                }
            }
            if (!string.IsNullOrEmpty(currentText) && cboReserveArea.Items.Contains(currentText))
            {
                cboReserveArea.Text = currentText;
            }
            else if (cboReserveArea.Items.Count > 0)
            {
                cboReserveArea.SelectedIndex = 0;
            }
            else
            {
                cboReserveArea.Text = string.Empty;
            }
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

            // Tính toán kích thước thẻ món thích ứng (Responsive Grid: 3 hoặc 4 cột tùy chiều rộng màn hình)
            int availableWidth = flpProductMenu.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 16;
            if (availableWidth < 300) availableWidth = 500;
            int cols = availableWidth >= 680 ? 4 : (availableWidth >= 460 ? 3 : 2);
            int cardMargin = 4;
            int cardWidth = (availableWidth - (cols * cardMargin * 2)) / cols;
            if (cardWidth < 135) cardWidth = 135;
            int cardHeight = cardWidth + 128; // Thumbnail vuông + 128px cho thông tin & nút bấm không bao giờ bị cắt xén
            Size targetSize = new Size(cardWidth, cardHeight);

            foreach (var prod in products)
            {
                string catName = _categoryNameMap.ContainsKey(prod.CategoryId)
                    ? _categoryNameMap[prod.CategoryId]
                    : "Thực đơn";

                UcProductCard card = new UcProductCard();
                card.Margin = new Padding(cardMargin);
                card.Size = targetSize;
                card.BindData(prod, catName);
                card.SetSelected(prod.Id == _selectedProductId);
                card.ProductSelected += UcProductCard_ProductSelected;
                card.ProductAddClicked += UcProductCard_ProductAddClicked;

                flpProductMenu.Controls.Add(card);
            }

            flpProductMenu.ResumeLayout();
        }

        // Tự động co giãn kích thước các thẻ món khi người dùng kéo giãn hoặc phóng to cửa sổ
        private void UpdateProductCardSizes()
        {
            if (flpProductMenu.Controls.Count == 0) return;

            int availableWidth = flpProductMenu.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 16;
            if (availableWidth < 300) return;
            int cols = availableWidth >= 680 ? 4 : (availableWidth >= 460 ? 3 : 2);
            int cardMargin = 4;
            int cardWidth = (availableWidth - (cols * cardMargin * 2)) / cols;
            if (cardWidth < 135) cardWidth = 135;
            int cardHeight = cardWidth + 128; // Thumbnail vuông + 128px cho thông tin & nút bấm không bao giờ bị cắt xén
            Size targetSize = new Size(cardWidth, cardHeight);

            flpProductMenu.SuspendLayout();
            foreach (Control ctrl in flpProductMenu.Controls)
            {
                if (ctrl is UcProductCard card)
                {
                    card.Margin = new Padding(cardMargin);
                    card.Size = targetSize;
                }
            }
            flpProductMenu.ResumeLayout();
        }

        private void btnManageAreas_Click(object sender, EventArgs e)
        {
            ShowAreaYoutubeDropdown();
        }

        private ToolStripDropDown _areaDropDown;

        // Hiển thị danh sách gợi ý khu vực có nút [✕] xóa từng dòng giống YouTube
        private void ShowAreaYoutubeDropdown()
        {
            if (_areaDropDown != null)
            {
                _areaDropDown.Close();
                _areaDropDown.Dispose();
                _areaDropDown = null;
            }

            var areas = _tableService.GetAllAreaDTOs();
            _areaDropDown = new ToolStripDropDown();
            _areaDropDown.AutoClose = true;
            _areaDropDown.Margin = Padding.Empty;
            _areaDropDown.Padding = Padding.Empty;
            _areaDropDown.BackColor = Color.White;

            int popupWidth = Math.Max(260, cboReserveArea.Width + (btnManageAreas != null ? btnManageAreas.Width : 32) + 10);
            Panel pnlOuter = new Panel();
            pnlOuter.BackColor = Color.White;
            pnlOuter.BorderStyle = BorderStyle.FixedSingle;
            pnlOuter.Width = popupWidth;

            // Header nhỏ
            Label lblHeader = new Label();
            lblHeader.Text = "  📍 KHU VỰC QUÁN (Bấm ✕ để xóa)";
            lblHeader.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(120, 100, 90);
            lblHeader.BackColor = Color.FromArgb(248, 245, 240);
            lblHeader.Dock = DockStyle.Top;
            lblHeader.Height = 26;
            lblHeader.TextAlign = ContentAlignment.MiddleLeft;
            pnlOuter.Controls.Add(lblHeader);

            FlowLayoutPanel flpRows = new FlowLayoutPanel();
            flpRows.FlowDirection = FlowDirection.TopDown;
            flpRows.WrapContents = false;
            flpRows.AutoScroll = true;
            flpRows.Dock = DockStyle.Fill;
            flpRows.Padding = new Padding(2);
            flpRows.BackColor = Color.White;

            int contentHeight = 28;

            if (areas == null || areas.Count == 0)
            {
                Label lblEmpty = new Label();
                lblEmpty.Text = "Chưa có khu vực nào";
                lblEmpty.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
                lblEmpty.ForeColor = Color.Gray;
                lblEmpty.Size = new Size(popupWidth - 14, 30);
                lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
                flpRows.Controls.Add(lblEmpty);
                contentHeight += 32;
            }
            else
            {
                foreach (var area in areas)
                {
                    string areaName = area.AreaName;
                    Panel row = new Panel();
                    row.Size = new Size(popupWidth - 14, 30);
                    row.Margin = new Padding(0, 1, 0, 1);
                    row.BackColor = Color.White;
                    row.Cursor = Cursors.Hand;

                    Label lblPin = new Label();
                    lblPin.Text = "📍";
                    lblPin.Font = new Font("Segoe UI", 8F);
                    lblPin.ForeColor = Color.FromArgb(140, 100, 80);
                    lblPin.Size = new Size(20, 24);
                    lblPin.Location = new Point(2, 4);
                    lblPin.Cursor = Cursors.Hand;

                    Label lblName = new Label();
                    lblName.Text = areaName;
                    lblName.Font = new Font("Segoe UI", 9F);
                    lblName.ForeColor = Color.FromArgb(40, 40, 40);
                    lblName.Location = new Point(24, 4);
                    lblName.Size = new Size(row.Width - 60, 24);
                    lblName.Cursor = Cursors.Hand;

                    Label btnDel = new Label();
                    btnDel.Text = "✕";
                    btnDel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                    btnDel.ForeColor = Color.FromArgb(160, 160, 160);
                    btnDel.TextAlign = ContentAlignment.MiddleCenter;
                    btnDel.Location = new Point(row.Width - 28, 3);
                    btnDel.Size = new Size(24, 24);
                    btnDel.Cursor = Cursors.Hand;

                    // Hover hiệu ứng giống YouTube
                    Action highlightRow = () =>
                    {
                        row.BackColor = Color.FromArgb(248, 244, 238);
                    };
                    Action unhighlightRow = () =>
                    {
                        row.BackColor = Color.White;
                    };

                    row.MouseEnter += (s, ev) => highlightRow();
                    row.MouseLeave += (s, ev) => unhighlightRow();
                    lblPin.MouseEnter += (s, ev) => highlightRow();
                    lblName.MouseEnter += (s, ev) => highlightRow();

                    btnDel.MouseEnter += (s, ev) =>
                    {
                        btnDel.ForeColor = Color.FromArgb(192, 57, 43);
                        btnDel.BackColor = Color.FromArgb(253, 237, 236);
                    };
                    btnDel.MouseLeave += (s, ev) =>
                    {
                        btnDel.ForeColor = Color.FromArgb(160, 160, 160);
                        btnDel.BackColor = Color.Transparent;
                    };

                    // Chọn khu vực khi click vào dòng
                    EventHandler selectArea = (s, ev) =>
                    {
                        cboReserveArea.Text = areaName;
                        _areaDropDown.Close();
                    };
                    row.Click += selectArea;
                    lblPin.Click += selectArea;
                    lblName.Click += selectArea;

                    // Xóa khu vực khi click nút [✕]
                    btnDel.Click += (s, ev) =>
                    {
                        var confirm = MessageBox.Show(
                            $"Bạn có chắc chắn muốn xóa khu vực '{areaName}' khỏi danh sách?",
                            "Xác nhận xóa khu vực",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (confirm == DialogResult.Yes)
                        {
                            if (_tableService.DeleteAreaByName(areaName, out string msg))
                            {
                                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (cboReserveArea.Text == areaName)
                                {
                                    cboReserveArea.Text = string.Empty;
                                }
                                LoadAreaComboBox();
                                _areaDropDown.Close();
                                BeginInvoke((MethodInvoker)delegate { ShowAreaYoutubeDropdown(); });
                            }
                            else
                            {
                                MessageBox.Show(msg, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    };

                    row.Controls.Add(lblPin);
                    row.Controls.Add(lblName);
                    row.Controls.Add(btnDel);
                    flpRows.Controls.Add(row);

                    contentHeight += 32;
                }
            }

            // Gợi ý tự thêm ở chân dropdown
            Label lblFooter = new Label();
            lblFooter.Text = "💡 Gõ tên mới vào ô để tự thêm khu vực";
            lblFooter.Font = new Font("Segoe UI", 7.5F, FontStyle.Italic);
            lblFooter.ForeColor = Color.FromArgb(140, 140, 140);
            lblFooter.Dock = DockStyle.Bottom;
            lblFooter.Height = 22;
            lblFooter.TextAlign = ContentAlignment.MiddleCenter;
            pnlOuter.Controls.Add(lblFooter);
            contentHeight += 24;

            pnlOuter.Controls.Add(flpRows);
            flpRows.BringToFront();

            int finalHeight = Math.Min(260, contentHeight);
            pnlOuter.Height = finalHeight;

            ToolStripControlHost host = new ToolStripControlHost(pnlOuter);
            host.Margin = Padding.Empty;
            host.Padding = Padding.Empty;
            host.AutoSize = false;
            host.Size = pnlOuter.Size;

            _areaDropDown.Items.Clear();
            _areaDropDown.Items.Add(host);
            _areaDropDown.Show(cboReserveArea, 0, cboReserveArea.Height);
        }

        private void LoadReservedTableCards()
        {
            LoadAllTableCards();
        }

        private void LoadAllTableCards()
        {
            _tableService.AutoCleanupExpiredReservations();
            var activeReservedTables = _tableService.GetActiveReservedOrOccupiedTables() ?? new List<TableDTO>();

            lblReservedListHeader.Text = $"DANH SÁCH BÀN ĐẶT TRƯỚC ({activeReservedTables.Count})";

            flpTableList.SuspendLayout();
            flpTableList.Controls.Clear();

            if (activeReservedTables.Count == 0)
            {
                flpTableList.Controls.Add(lblEmptyReservedNotice);
            }
            else
            {
                foreach (var table in activeReservedTables)
                {
                    UcReservedTableCard card = new UcReservedTableCard();
                    card.BindData(table, table.Id == _selectedTableId);
                    card.TableSelected += UcReservedTableCard_TableSelected;
                    card.TableCancelClicked += UcReservedTableCard_TableCancelClicked;
                    flpTableList.Controls.Add(card);
                }
            }

            flpTableList.ResumeLayout();
        }

        private void SelectCounterMode()
        {
            var counter = _tableService.GetOrCreateCounterTable();
            _selectedTableId = counter.Id;
            _selectedTableName = counter.TableName;
            btnCounterOrder.BackColor = Color.FromArgb(39, 174, 96);

            lblSelectedTableBanner.Text = "Đang chọn: " + counter.TableName;
            lblOrderHeaderTitle.Text = "ĐƠN HÀNG · KHÁCH TẠI QUẦY";

            LoadAllTableCards();
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

            string statusText = string.IsNullOrEmpty(table.Status) ? "Trống" : table.Status;
            string timeNote = table.StartTime.HasValue && table.EndTime.HasValue
                ? $" ({table.StartTime:HH:mm}-{table.EndTime:HH:mm})"
                : "";

            string displayName = !string.IsNullOrWhiteSpace(table.TableName) && !table.TableName.StartsWith("KH: ", StringComparison.OrdinalIgnoreCase)
                ? $"KH: {table.TableName}"
                : table.TableName;

            lblSelectedTableBanner.Text = $"Đang chọn: {displayName} · Khu vực: {table.AreaName} [{statusText}]{timeNote}";
            lblOrderHeaderTitle.Text = $"ĐƠN HÀNG · {displayName.ToUpper()}";

            LoadAllTableCards();
            LoadOrderDetailsForSelectedTable();
        }

        private void UcReservedTableCard_TableCancelClicked(object sender, TableDTO table)
        {
            if (table == null) return;

            string actionText = string.Equals(table.Status, "Đã thanh toán", StringComparison.OrdinalIgnoreCase)
                ? "đóng / dọn bàn"
                : "hủy bàn đặt trước";

            string displayName = !string.IsNullOrWhiteSpace(table.TableName) && !table.TableName.StartsWith("KH: ", StringComparison.OrdinalIgnoreCase)
                ? $"KH: {table.TableName}"
                : table.TableName;

            if (MessageBox.Show($"Bạn có chắc muốn {actionText} '{displayName}' tại '{table.AreaName}'?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_tableService.DeleteTable(table.Id, out string msg))
                {
                    if (_selectedTableId == table.Id)
                    {
                        SelectCounterMode();
                    }
                    else
                    {
                        LoadAllTableCards();
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
            string customerName = txtReserveCustomerName.Text.Trim();
            string areaName = cboReserveArea.Text.Trim();

            if (_tableService.AddReservedTable(
                customerName,
                areaName,
                dtpStartTime.Value,
                dtpEndTime.Value,
                out string message))
            {
                txtReserveCustomerName.Clear();
                dtpStartTime.Value = DateTime.Now;
                dtpEndTime.Value = DateTime.Now.AddHours(2);
                LoadAreaComboBox();
                LoadAllTableCards();

                var newlyAdded = _tableService.GetActiveReservedOrOccupiedTables()
                    .FirstOrDefault(t => string.Equals(t.TableName?.Trim(), customerName, StringComparison.OrdinalIgnoreCase) &&
                                         string.Equals(t.AreaName?.Trim(), areaName, StringComparison.OrdinalIgnoreCase));
                if (newlyAdded != null)
                {
                    UcReservedTableCard_TableSelected(this, newlyAdded);
                }
            }
            else
            {
                MessageBox.Show(message, "Thông báo đặt bàn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tmrAutoExpire_Tick(object sender, EventArgs e)
        {
            int expiredCount = _tableService.AutoCleanupExpiredReservations();
            if (expiredCount > 0)
            {
                var stillExists = _tableService.GetTableById(_selectedTableId);
                if (_selectedTableId > 1 && stillExists == null)
                {
                    SelectCounterMode();
                }
                else
                {
                    LoadAllTableCards();
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
            var promoItem = cboPromotion.SelectedItem as PromotionItem;
            decimal discountPercent = promoItem != null ? promoItem.DiscountPercent : 0m;
            decimal fixedDiscount = promoItem != null ? promoItem.FixedDiscountAmount : 0m;
            int? promoId = promoItem != null ? promoItem.PromotionId : null;
            string discountDesc = promoItem != null ? (promoItem.PromoCode ?? promoItem.DisplayText) : string.Empty;

            _orderService.CalculateOrderSummaryWithPromo(
                _selectedTableId,
                discountPercent,
                fixedDiscount,
                promoId,
                discountDesc,
                out decimal totalAmount,
                out decimal discountAmount,
                out decimal vatPercent,
                out decimal vatAmount,
                out decimal finalAmount,
                out string discountInfo);

            string discountText = discountAmount > 0
                ? $" | Giảm: -{discountAmount:N0} đ ({discountInfo})"
                : string.Empty;

            string vatText = vatAmount > 0
                ? $" | VAT ({vatPercent:0.##}%): +{vatAmount:N0} đ"
                : string.Empty;

            lblTotalAmount.Text = $"Tạm tính: {totalAmount:N0} đ{discountText}{vatText}";
            lblFinalAmount.Text = $"Thanh toán: {finalAmount:N0} đ";

            UpdateChangeDueCalculation();
        }

        private void txtCashTendered_TextChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;
            UpdateChangeDueCalculation();
        }

        private void btnCashExact_Click(object sender, EventArgs e)
        {
            var promoItem = cboPromotion.SelectedItem as PromotionItem;
            decimal discountPercent = promoItem != null ? promoItem.DiscountPercent : 0m;
            decimal fixedDiscount = promoItem != null ? promoItem.FixedDiscountAmount : 0m;
            int? promoId = promoItem != null ? promoItem.PromotionId : null;
            string discountDesc = promoItem != null ? (promoItem.PromoCode ?? promoItem.DisplayText) : string.Empty;

            _orderService.CalculateOrderSummaryWithPromo(
                _selectedTableId,
                discountPercent,
                fixedDiscount,
                promoId,
                discountDesc,
                out _,
                out _,
                out _,
                out _,
                out decimal finalAmount,
                out _);

            txtCashTendered.Text = finalAmount > 0 ? $"{finalAmount:N0}" : "0";
            txtCashTendered.SelectionStart = txtCashTendered.Text.Length;
        }

        private void btnCashQuick_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                string text = btn.Text.ToLower().Replace("k", "000").Replace(",", "").Trim();
                if (decimal.TryParse(text, out decimal val))
                {
                    txtCashTendered.Text = $"{val:N0}";
                    txtCashTendered.SelectionStart = txtCashTendered.Text.Length;
                }
            }
        }

        private void UpdateChangeDueCalculation()
        {
            var promoItem = cboPromotion.SelectedItem as PromotionItem;
            decimal discountPercent = promoItem != null ? promoItem.DiscountPercent : 0m;
            decimal fixedDiscount = promoItem != null ? promoItem.FixedDiscountAmount : 0m;
            int? promoId = promoItem != null ? promoItem.PromotionId : null;
            string discountDesc = promoItem != null ? (promoItem.PromoCode ?? promoItem.DisplayText) : string.Empty;

            _orderService.CalculateOrderSummaryWithPromo(
                _selectedTableId,
                discountPercent,
                fixedDiscount,
                promoId,
                discountDesc,
                out _,
                out _,
                out _,
                out _,
                out decimal finalAmount,
                out _);

            string raw = txtCashTendered.Text.Trim().Replace(",", "").Replace(".", "").Replace("đ", "").Trim();
            if (decimal.TryParse(raw, out decimal cashTendered) && cashTendered > 0)
            {
                decimal changeDue = cashTendered - finalAmount;
                if (changeDue >= 0)
                {
                    lblChangeDue.ForeColor = Color.FromArgb(39, 174, 96);
                    lblChangeDue.Text = $"{changeDue:N0} đ";
                }
                else
                {
                    lblChangeDue.ForeColor = Color.FromArgb(231, 76, 60);
                    lblChangeDue.Text = $"Thiếu {Math.Abs(changeDue):N0} đ";
                }
            }
            else
            {
                lblChangeDue.ForeColor = Color.DimGray;
                lblChangeDue.Text = "0 đ";
            }
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
                LoadPromotionComboBox();
                UpdateOrderSummaryDisplay();
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
                LoadPromotionComboBox();
                UpdateOrderSummaryDisplay();
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
                lblCustomerInfo.ForeColor = Color.FromArgb(211, 84, 0);
                lblCustomerInfo.Text = "✖ Chưa là thành viên (Bấm '➕ Thêm' để đăng ký)";
                LoadPromotionComboBox();
                UpdateOrderSummaryDisplay();
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
                LoadPromotionComboBox();
                UpdateOrderSummaryDisplay();
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
        // SỰ KIỆN NÚT THÊM KHÁCH HÀNG MỚI (TRỰC TIẾP TRÊN CONTROL btnAddCustomer)
        // =====================================================================
        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            string phoneHint = txtCustomerPhone.Text.Trim();
            using (var frm = new FrmQuickAddCustomer(phoneHint))
            {
                if (frm.ShowDialog(this) == DialogResult.OK && frm.CreatedCustomer != null)
                {
                    LoadCustomerAndPromoCache();
                    var created = _cachedCustomers.FirstOrDefault(c => c.Phone == frm.CreatedCustomer.Phone) ?? frm.CreatedCustomer;
                    _selectedCustomer = created;

                    _isSelectingSuggestion = true;
                    txtCustomerPhone.Text = created.Phone;
                    _isSelectingSuggestion = false;

                    lblCustomerInfo.ForeColor = Color.FromArgb(39, 174, 96);
                    lblCustomerInfo.Text = $"✔ {created.CustomerName} · Hạng {created.MembershipTier} ({created.RewardPoints} điểm)";
                    lstCustomerSuggest.Visible = false;

                    LoadPromotionComboBox();
                    UpdateOrderSummaryDisplay();
                }
            }
        }

        // =====================================================================
        // SỰ KIỆN ĐỔI KHUYẾN MÃI / QUY ĐỔI ĐIỂM (TRỰC TIẾP TRÊN cboPromotion)
        // =====================================================================
        private void cboPromotion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;
            UpdateOrderSummaryDisplay();
        }

        // =====================================================================
        // NẠP DANH SÁCH PROMOTION VÀ QUY ĐỔI ĐIỂM VÀO COMBOBOX
        // (Ủy quyền xử lý nghiệp vụ cho PromotionService)
        // =====================================================================
        private void LoadPromotionComboBox()
        {
            _promotionService.LoadPromotionComboBox(cboPromotion, _selectedCustomer, _cachedPromotions);
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
                var promoItem = cboPromotion.SelectedItem as PromotionItem;
                decimal discountPercent = promoItem != null ? promoItem.DiscountPercent : 0m;
                decimal fixedDiscount = promoItem != null ? promoItem.FixedDiscountAmount : 0m;
                int? promoId = promoItem != null ? promoItem.PromotionId : null;
                string discountDesc = promoItem != null ? promoItem.DisplayText : string.Empty;
                int pointsToRedeem = promoItem != null ? promoItem.PointsToRedeem : 0;
                int? customerDbId = _selectedCustomer != null ? (int?)_selectedCustomer.CustomerId : null;

                bool success = _orderService.CheckoutTableOrder(
                    _selectedTableId,
                    discountPercent,
                    fixedDiscount,
                    promoId,
                    discountDesc,
                    customerDbId,
                    out OrderDTO paidOrder,
                    out List<OrderDetailDTO> orderDetails,
                    out string message);

                if (!success)
                {
                    MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // CỘNG ĐIỂM THEO TỔNG TIỀN THANH TOÁN (100.000 VNĐ = 100 điểm => 1.000 VNĐ = 1 điểm)
                // VÀ TRỪ ĐIỂM NẾU CÓ DÙNG KHUYẾN MÃI ĐỔI ĐIỂM
                if (_selectedCustomer != null && paidOrder != null)
                {
                    int earnedPoints = (int)Math.Floor(paidOrder.FinalAmount / 1000m);
                    if (_customerService.AddRewardPoints(
                        _selectedCustomer.Phone,
                        earnedPoints,
                        pointsToRedeem,
                        out int updatedPoints,
                        out string updatedTier,
                        out string _))
                    {
                        _selectedCustomer.RewardPoints = updatedPoints;
                        _selectedCustomer.MembershipTier = updatedTier;
                        LoadCustomerAndPromoCache();
                    }
                }

                // Gán tiền khách đưa và tiền thối lại vào đối tượng hóa đơn để in ấn
                if (paidOrder != null)
                {
                    string rawCash = txtCashTendered.Text.Trim().Replace(",", "").Replace(".", "").Replace("đ", "").Trim();
                    if (!decimal.TryParse(rawCash, out decimal cashTendered) || cashTendered <= 0)
                    {
                        cashTendered = paidOrder.FinalAmount;
                    }
                    paidOrder.CashTendered = cashTendered;
                    paidOrder.ChangeDue = Math.Max(0m, cashTendered - paidOrder.FinalAmount);
                    if (_selectedCustomer != null)
                    {
                        paidOrder.CustomerName = _selectedCustomer.CustomerName;
                    }
                }

                // Reset trạng thái sau thanh toán
                txtCustomerPhone.Clear();
                txtCashTendered.Clear();
                lblChangeDue.ForeColor = Color.DimGray;
                lblChangeDue.Text = "0 đ";
                lstCustomerSuggest.Visible = false;
                _selectedCustomer = null;
                lblCustomerInfo.Text = "Khách lẻ (Gõ SĐT hoặc tên khách để gợi ý)";
                LoadPromotionComboBox();

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
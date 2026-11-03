using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    /// <summary>
    /// Màn hình Xem trước và In hóa đơn thanh toán khổ K80 (FrmPrintReview).
    /// Thiết kế chuẩn K80 trực quan ngay trên màn hình thiết kế Designer và hỗ trợ in thực tế.
    /// </summary>
    public partial class FrmPrintReview : Form
    {
        private readonly OrderDTO _order;
        private readonly List<OrderDetailDTO> _orderDetails;

        // 1. Constructor mặc định (dùng khi mở xem thử nghiệm độc lập)
        public FrmPrintReview() : this(null, null)
        {
        }

        // 2. Constructor nhận dữ liệu Hóa đơn và Chi tiết món từ màn hình FrmOrder
        public FrmPrintReview(OrderDTO order, List<OrderDetailDTO> orderDetails)
        {
            InitializeComponent();

            if (order != null)
            {
                _order = order;
                _orderDetails = orderDetails ?? new List<OrderDetailDTO>();
            }
            else
            {
                // Dữ liệu mẫu hiển thị ngay cả khi mở thử nghiệm
                _order = new OrderDTO
                {
                    Id = 1024,
                    TableName = "Bàn 02 (Khu máy lạnh)",
                    EmployeeName = UserSession.CurrentUser != null ? UserSession.CurrentUser.Username : "Tấn Phát (Thu ngân)",
                    OrderDate = DateTime.Now,
                    TotalAmount = 134000m,
                    DiscountAmount = 13400m,
                    FinalAmount = 120600m
                };
                _orderDetails = new List<OrderDetailDTO>
                {
                    new OrderDetailDTO { ProductId = 3, ProductName = "Bạc xỉu", Unit = "Ly", Quantity = 2, UnitPrice = 32000m, TotalPrice = 64000m },
                    new OrderDetailDTO { ProductId = 4, ProductName = "Trà đào cam sả", Unit = "Ly", Quantity = 1, UnitPrice = 40000m, TotalPrice = 40000m },
                    new OrderDetailDTO { ProductId = 8, ProductName = "Bánh Croissant bơ tỏi", Unit = "Cái", Quantity = 1, UnitPrice = 30000m, TotalPrice = 30000m }
                };
            }
        }

        // 3. Sự kiện khi Form tải lên: Nạp danh sách máy in, căn giữa tờ hóa đơn và hiển thị chi tiết đơn hàng
        private void FrmPrintReview_Load(object sender, EventArgs e)
        {
            try
            {
                LoadInstalledPrinters();
                ConfigureInvoiceDocument();
                CenterReceiptPaper();
                BindReceiptDataToVisualPaper();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể khởi tạo bản xem trước hóa đơn:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // 4. Căn giữa tờ giấy hóa đơn K80 trên vùng nền xám
        private void CenterReceiptPaper()
        {
            if (pnlPaper != null && pnlReceiptContainer != null)
            {
                pnlPaper.Left = Math.Max(20, (pnlReceiptContainer.ClientSize.Width - pnlPaper.Width) / 2);
            }
        }

        private void pnlReceiptContainer_Resize(object sender, EventArgs e)
        {
            CenterReceiptPaper();
        }

        // 5. Hiển thị dữ liệu hóa đơn thật lên tờ giấy in trực quan
        private void BindReceiptDataToVisualPaper()
        {
            string shopName = ConfigHelper.GetAppSetting("ShopName") ?? "THE COFFEE HOUSE";
            string shopAddress = ConfigHelper.GetAppSetting("ShopAddress") ?? "123 Nguyễn Huệ, Q.1, TP.HCM";
            string shopPhone = ConfigHelper.GetAppSetting("ShopPhone") ?? "0909 123 456";

            lblShopName.Text = shopName.ToUpper();
            lblShopAddress.Text = "Đ/c: " + shopAddress;
            lblShopPhone.Text = "SĐT: " + shopPhone;

            if (_order != null)
            {
                lblOrderNo.Text = $"Số HĐ: #{_order.Id:D5}";
                lblTableName.Text = $"Bàn: {_order.TableName}";
                lblOrderDate.Text = $"Ngày: {_order.OrderDate:dd/MM/yyyy HH:mm}";
                lblCashier.Text = $"TN: {_order.EmployeeName}";

                lblTotalValue.Text = $"{_order.TotalAmount:N0} đ";
                lblDiscountValue.Text = $"-{_order.DiscountAmount:N0} đ";
                lblFinalValue.Text = $"{_order.FinalAmount:N0} đ";

                if (!string.IsNullOrWhiteSpace(_order.PromoCode))
                {
                    lblDiscountCaption.Text = $"Giảm giá ({_order.PromoCode}):";
                }
            }

            // Tạo danh sách món dạng cột
            if (_orderDetails != null && _orderDetails.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in _orderDetails)
                {
                    string name = item.ProductName;
                    if (name.Length > 20)
                        name = name.Substring(0, 18) + "..";

                    sb.AppendLine(string.Format("{0,-20} {1,2}  {2,9:N0}  {3,10:N0}",
                        name, item.Quantity, item.UnitPrice, item.TotalPrice));
                }
                lblItemDetails.Text = sb.ToString().TrimEnd();
                lblItemDetails.Height = Math.Max(60, _orderDetails.Count * 22);
                pnlPaper.Height = 410 + lblItemDetails.Height;
            }
        }

        // 6. Tải danh sách các máy in hiện có trên hệ thống vào ComboBox cboPrinters
        private void LoadInstalledPrinters()
        {
            cboPrinters.Items.Clear();

            foreach (string printerName in PrinterSettings.InstalledPrinters)
            {
                cboPrinters.Items.Add(printerName);
            }

            if (cboPrinters.Items.Count == 0)
            {
                cboPrinters.Items.Add("Microsoft Print to PDF");
            }

            // Ưu tiên chọn Microsoft Print to PDF
            int pdfIdx = cboPrinters.FindString("Microsoft Print to PDF");
            if (pdfIdx >= 0)
            {
                cboPrinters.SelectedIndex = pdfIdx;
                return;
            }

            string defaultPrinter = new PrinterSettings().PrinterName;
            int defaultIdx = !string.IsNullOrEmpty(defaultPrinter)
                ? cboPrinters.FindStringExact(defaultPrinter)
                : -1;

            cboPrinters.SelectedIndex = defaultIdx >= 0 ? defaultIdx : 0;
        }

        // 7. Cấu hình khổ giấy in K80 cho PrintDocument
        private void ConfigureInvoiceDocument()
        {
            int itemCount = _orderDetails != null ? _orderDetails.Count : 0;
            int dynamicHeight = Math.Max(460, 330 + (itemCount * 30));

            PaperSize k80PaperSize = new PaperSize("K80_Receipt", 315, dynamicHeight);
            printDocumentInvoice.DefaultPageSettings.PaperSize = k80PaperSize;
            printDocumentInvoice.DefaultPageSettings.Margins = new Margins(5, 5, 5, 5);

            if (cboPrinters.SelectedItem != null)
            {
                printDocumentInvoice.PrinterSettings.PrinterName = cboPrinters.SelectedItem.ToString();
            }
        }

        // 8. Sự kiện vẽ nội dung hóa đơn lên trang in thật (PrintPage)
        private void printDocumentInvoice_PrintPage(object sender, PrintPageEventArgs e)
        {
            PrintHelper.DrawInvoiceK80(
                e.Graphics,
                _order,
                _orderDetails,
                e.PageBounds.Width);

            e.HasMorePages = false;
        }

        // 9. Sự kiện khi nhấn nút "Thực hiện in" (btnPrint)
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (cboPrinters.SelectedItem == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn máy in trước khi thực hiện in!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string selectedPrinter = cboPrinters.SelectedItem.ToString();

            try
            {
                printDocumentInvoice.PrinterSettings.PrinterName = selectedPrinter;
                printDocumentInvoice.Print();

                MessageBox.Show(
                    "Đã gửi lệnh in hóa đơn thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể thực hiện in hóa đơn:\n" + ex.Message,
                    "Lỗi in ấn",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}

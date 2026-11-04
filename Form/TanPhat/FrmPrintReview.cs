using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    /// <summary>
    /// Màn hình Xem trước và In hóa đơn thanh toán khổ K80 sử dụng RDLC Report (FrmPrintReview).
    /// Tích hợp Microsoft.Reporting.WinForms.ReportViewer kết nối với RptInvoiceK80.rdlc.
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
                // Dữ liệu mẫu chuẩn hóa đơn quán cà phê Việt Nam khi mở xem thử nghiệm
                _order = new OrderDTO
                {
                    Id = 1024,
                    TableName = "Bàn 02 (Khu máy lạnh)",
                    EmployeeName = UserSession.CurrentUser != null ? UserSession.CurrentUser.Username : "Tấn Phát",
                    OrderDate = DateTime.Now,
                    TotalAmount = 134000m,
                    DiscountAmount = 13400m,
                    VATPercent = 8m,
                    VATAmount = 9648m,
                    FinalAmount = 130248m,
                    PromoCode = "CHAOMUNG"
                };
                _orderDetails = new List<OrderDetailDTO>
                {
                    new OrderDetailDTO { Id = 1, OrderId = 1024, ProductId = 3, ProductName = "Bạc xỉu đá", Unit = "Ly", Quantity = 2, UnitPrice = 32000m, TotalPrice = 64000m },
                    new OrderDetailDTO { Id = 2, OrderId = 1024, ProductId = 4, ProductName = "Trà đào cam sả", Unit = "Ly", Quantity = 1, UnitPrice = 40000m, TotalPrice = 40000m },
                    new OrderDetailDTO { Id = 3, OrderId = 1024, ProductId = 8, ProductName = "Bánh Croissant bơ tỏi", Unit = "Cái", Quantity = 1, UnitPrice = 30000m, TotalPrice = 30000m }
                };
            }
        }

        // 3. Sự kiện khi Form tải lên: Nạp danh sách máy in và hiển thị báo cáo RDLC RptInvoiceK80.rdlc
        private void FrmPrintReview_Load(object sender, EventArgs e)
        {
            try
            {
                LoadInstalledPrinters();
                ConfigureInvoiceDocument();
                LoadRdlcReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể khởi tạo báo cáo hóa đơn RDLC:\n" + ex.Message,
                    "Lỗi báo cáo RDLC",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // 4. Nạp dữ liệu OrderDTO & OrderDetailDTO vào báo cáo RDLC (RptInvoiceK80.rdlc) trên ReportViewer
        private void LoadRdlcReport()
        {
            string shopName = ConfigHelper.GetAppSetting("ShopName") ?? "THE COFFEE HOUSE";
            string shopAddress = ConfigHelper.GetAppSetting("ShopAddress") ?? "86-88 Cao Thắng, Quận 3, TP.HCM";
            string shopPhone = ConfigHelper.GetAppSetting("ShopPhone") ?? "1800 6936 - 0909 123 456";

            reportViewerInvoice.ProcessingMode = ProcessingMode.Local;
            reportViewerInvoice.LocalReport.ReportEmbeddedResource = "DoAnCuoiKi_QuanLyTiemCaPhe.Form.TanPhat.RptInvoiceK80.rdlc";

            // Hỗ trợ nạp từ đường dẫn file nếu chạy từ thư mục bin
            string localRdlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Form", "TanPhat", "RptInvoiceK80.rdlc");
            if (File.Exists(localRdlcPath))
            {
                reportViewerInvoice.LocalReport.ReportPath = localRdlcPath;
            }

            // Gán nguồn dữ liệu danh sách món (DataSetInvoiceDetails)
            orderDetailDTOBindingSource.DataSource = _orderDetails;
            reportViewerInvoice.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSetInvoiceDetails", _orderDetails);
            reportViewerInvoice.LocalReport.DataSources.Add(rds);

            // Tính toán hiển thị giảm giá và thuế VAT
            string discountDisplay = _order.DiscountAmount > 0
                ? (!string.IsNullOrWhiteSpace(_order.PromoCode)
                    ? $"-{_order.DiscountAmount:N0} đ ({_order.PromoCode})"
                    : $"-{_order.DiscountAmount:N0} đ")
                : "0 đ";

            decimal vatAmount = _order.VATAmount;
            if (vatAmount <= 0 && _order.FinalAmount > (_order.TotalAmount - _order.DiscountAmount))
            {
                vatAmount = _order.FinalAmount - (_order.TotalAmount - _order.DiscountAmount);
            }

            string vatDisplay = vatAmount > 0 ? $"+{vatAmount:N0} đ" : "0 đ";
            DateTime orderDateValue = _order.OrderDate ?? DateTime.Now;

            // Truyền các tham số chuẩn hóa đơn K80 vào RDLC
            ReportParameter[] parameters = new ReportParameter[]
            {
                new ReportParameter("pShopName", shopName.ToUpper()),
                new ReportParameter("pShopAddress", "Đ/c: " + shopAddress),
                new ReportParameter("pShopPhone", "Hotline: " + shopPhone),
                new ReportParameter("pOrderId", $"#HD{_order.Id:D5}"),
                new ReportParameter("pTableName", _order.TableName ?? "Mang về"),
                new ReportParameter("pOrderDate", orderDateValue.ToString("dd/MM/yyyy HH:mm")),
                new ReportParameter("pCashier", _order.EmployeeName ?? "Thu ngân"),
                new ReportParameter("pTotalAmount", $"{_order.TotalAmount:N0} đ"),
                new ReportParameter("pDiscountAmount", discountDisplay),
                new ReportParameter("pVatAmount", vatDisplay),
                new ReportParameter("pFinalAmount", $"{_order.FinalAmount:N0} đ"),
                new ReportParameter("pWifiInfo", "Wifi: TheCoffeeHouse_Free  |  Pass: 12345678")
            };

            reportViewerInvoice.LocalReport.SetParameters(parameters);
            reportViewerInvoice.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewerInvoice.ZoomMode = ZoomMode.Percent;
            reportViewerInvoice.ZoomPercent = 100;
            reportViewerInvoice.RefreshReport();
        }

        // 5. Tải danh sách các máy in hiện có trên hệ thống vào ComboBox cboPrinters
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

            // Ưu tiên chọn Microsoft Print to PDF để dễ dàng xuất PDF khi kiểm thử
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

        // 6. Cấu hình khổ giấy in K80 cho PrintDocument
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

        // 7. Sự kiện vẽ nội dung hóa đơn lên trang in thật (PrintPage)
        private void printDocumentInvoice_PrintPage(object sender, PrintPageEventArgs e)
        {
            PrintHelper.DrawInvoiceK80(
                e.Graphics,
                _order,
                _orderDetails,
                e.PageBounds.Width);

            e.HasMorePages = false;
        }

        // 8. Sự kiện khi nhấn nút "Thực hiện in" (btnPrint)
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
                    "Đã gửi lệnh in hóa đơn K80 thành công!",
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
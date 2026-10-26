using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    /// <summary>
    /// Màn hình Xem trước và In hóa đơn thanh toán khổ K80 (FrmPrintReview / FrmPrintPreview).
    /// Sử dụng PrintDocument kết hợp PrintHelper để hiển thị bản xem trước và xuất ra máy in.
    /// </summary>
    public partial class FrmPrintReview : Form
    {
        private readonly OrderDTO _order;
        private readonly List<OrderDetailDTO> _orderDetails;

        // 1. Constructor mặc định (dùng khi mở thử nghiệm giao diện)
        public FrmPrintReview() : this(null, null)
        {
        }

        // 2. Constructor nhận dữ liệu Hóa đơn và Chi tiết món từ màn hình FrmOrder
        public FrmPrintReview(OrderDTO order, List<OrderDetailDTO> orderDetails)
        {
            InitializeComponent();
            _order = order ?? new OrderDTO
            {
                Id = 1,
                TableName = "Bàn 01",
                EmployeeName = UserSession.CurrentUser != null ? UserSession.CurrentUser.Username : "Thu ngân",
                OrderDate = DateTime.Now,
                TotalAmount = 0m,
                DiscountAmount = 0m,
                FinalAmount = 0m
            };
            _orderDetails = orderDetails ?? new List<OrderDetailDTO>();
        }

        // 3. Sự kiện khi Form tải lên: Nạp danh sách máy in và hiển thị bản xem trước hóa đơn
        private void FrmPrintReview_Load(object sender, EventArgs e)
        {
            try
            {
                LoadInstalledPrinters();
                ConfigureInvoiceDocument();
                printPreviewControl.Document = printDocumentInvoice;
                printPreviewControl.InvalidatePreview();
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

        // 4. Tải danh sách các máy in hiện có trên hệ thống vào ComboBox cboPrinters
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

            // Ưu tiên chọn máy in mặc định của hệ thống hoặc Microsoft Print to PDF
            string defaultPrinter = new PrinterSettings().PrinterName;
            int defaultIdx = !string.IsNullOrEmpty(defaultPrinter)
                ? cboPrinters.FindStringExact(defaultPrinter)
                : -1;

            if (defaultIdx >= 0)
            {
                cboPrinters.SelectedIndex = defaultIdx;
            }
            else
            {
                int pdfIdx = cboPrinters.FindString("Microsoft Print to PDF");
                cboPrinters.SelectedIndex = pdfIdx >= 0 ? pdfIdx : 0;
            }
        }

        // 5. Cấu hình khổ giấy in K80 cho PrintDocument
        private void ConfigureInvoiceDocument()
        {
            int itemCount = _orderDetails != null ? _orderDetails.Count : 0;
            int dynamicHeight = Math.Max(520, 340 + (itemCount * 28));

            PaperSize k80PaperSize = new PaperSize("K80_Receipt", 315, dynamicHeight);
            printDocumentInvoice.DefaultPageSettings.PaperSize = k80PaperSize;
            printDocumentInvoice.DefaultPageSettings.Margins = new Margins(5, 5, 5, 5);
        }

        // 6. Sự kiện vẽ nội dung hóa đơn lên trang in (PrintPage)
        private void printDocumentInvoice_PrintPage(object sender, PrintPageEventArgs e)
        {
            PrintHelper.DrawInvoiceK80(
                e.Graphics,
                _order,
                _orderDetails,
                e.PageBounds.Width);

            e.HasMorePages = false;
        }

        // 7. Sự kiện khi nhấn nút "Thực hiện in" (btnPrint)
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

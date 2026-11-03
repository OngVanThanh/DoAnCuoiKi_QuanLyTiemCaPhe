using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private PictureBox _pbReceiptCanvas;

        // 1. Constructor mặc định (dùng khi mở thử nghiệm giao diện độc lập)
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
                // Dữ liệu mẫu để xem thử thiết kế khi mở trực tiếp FrmPrintReview()
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

        // 3. Sự kiện khi Form tải lên: Nạp danh sách máy in và hiển thị bản xem trước hóa đơn K80
        private void FrmPrintReview_Load(object sender, EventArgs e)
        {
            try
            {
                LoadInstalledPrinters();
                ConfigureInvoiceDocument();
                RenderDirectPreviewSheet();
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

            // Ưu tiên chọn Microsoft Print to PDF để tránh lỗi spooler của OneNote Protected
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

        // 5. Cấu hình khổ giấy in K80 cho PrintDocument
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

        // 6. Hiển thị tờ hóa đơn K80 sắc nét trực tiếp trên vùng printPreviewControl
        private void RenderDirectPreviewSheet()
        {
            int itemCount = _orderDetails != null ? _orderDetails.Count : 0;
            int paperWidth = 320;
            int paperHeight = Math.Max(450, 330 + (itemCount * 32));

            Bitmap receiptBmp = new Bitmap(paperWidth + 24, paperHeight + 24);
            using (Graphics g = Graphics.FromImage(receiptBmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.Clear(Color.FromArgb(82, 86, 89));

                // Đổ bóng tờ giấy nhiệt K80
                using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(90, 0, 0, 0)))
                {
                    g.FillRectangle(shadowBrush, 14, 14, paperWidth, paperHeight);
                }

                // Nền giấy trắng K80
                g.FillRectangle(Brushes.White, 8, 8, paperWidth, paperHeight);
                using (Pen borderPen = new Pen(Color.FromArgb(210, 210, 210), 1f))
                {
                    g.DrawRectangle(borderPen, 8, 8, paperWidth, paperHeight);
                }

                // Dịch tọa độ vào trong tờ giấy và gọi PrintHelper vẽ nội dung hóa đơn
                g.TranslateTransform(8, 8);
                PrintHelper.DrawInvoiceK80(g, _order, _orderDetails, paperWidth);
                g.ResetTransform();
            }

            if (_pbReceiptCanvas == null)
            {
                _pbReceiptCanvas = new PictureBox
                {
                    BackColor = Color.FromArgb(82, 86, 89),
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.CenterImage
                };
                printPreviewControl.Controls.Add(_pbReceiptCanvas);
            }

            if (_pbReceiptCanvas.Image != null)
            {
                _pbReceiptCanvas.Image.Dispose();
            }
            _pbReceiptCanvas.Image = receiptBmp;
            _pbReceiptCanvas.BringToFront();
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

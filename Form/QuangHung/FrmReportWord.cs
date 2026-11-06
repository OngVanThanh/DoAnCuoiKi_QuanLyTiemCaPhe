using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmReportWord : Form
    {
        private readonly ReportRepository _reportRepo = new ReportRepository();

        public FrmReportWord()
        {
            InitializeComponent();
        }

        private void FrmReportWord_Load(object sender, EventArgs e)
        {
            // 1. Nạp danh sách 3 loại báo cáo vào ComboBox
            cboReportType.Items.Clear();
            cboReportType.Items.Add("Báo cáo Doanh thu");
            cboReportType.Items.Add("Top Món bán chạy nhất");
            cboReportType.Items.Add("Top Khách hàng thân thiết");
            cboReportType.SelectedIndex = 0;

            // 2. Mặc định khoảng ngày: 30 ngày gần nhất đến hôm nay
            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Today;

            // 3. Tải báo cáo đầu tiên lên màn hình
            LoadSelectedReport();
        }

        // Khi người dùng đổi lựa chọn trên ComboBox
        private void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Bật bộ lọc ngày nếu là Báo cáo Doanh thu, 2 báo cáo còn lại thì khóa
            bool isRevenue = (cboReportType.SelectedIndex == 0);
            dtpFromDate.Enabled = isRevenue;
            dtpToDate.Enabled = isRevenue;

            // Tự động tải lại báo cáo tương ứng
            LoadSelectedReport();
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            if (cboReportType.SelectedIndex == 0)
            {
                LoadSelectedReport();
            }
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            if (cboReportType.SelectedIndex == 0)
            {
                LoadSelectedReport();
            }
        }

        // =========================================================================
        // 1. HÀM TẢI VÀ HIỂN THỊ BÁO CÁO LÊN REPORTVIEWER
        // =========================================================================
        private void LoadSelectedReport()
        {
            try
            {
                reportViewer1.LocalReport.DataSources.Clear();
                string rdlcPath = "";

                switch (cboReportType.SelectedIndex)
                {
                    case 0: // Báo cáo Doanh thu
                        rdlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Form\QuangHung\RptRevenue.rdlc");
                        var revenueData = _reportRepo.GetRevenueReport(dtpFromDate.Value, dtpToDate.Value);

                        reportViewer1.LocalReport.ReportPath = rdlcPath;
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetReport", revenueData));
                        break;

                    case 1: // Top Món bán chạy
                        rdlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Form\QuangHung\RptBestSeller.rdlc");
                        var productData = _reportRepo.GetBestSellingProducts(10);

                        reportViewer1.LocalReport.ReportPath = rdlcPath;
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetReport", productData));
                        break;

                    case 2: // Top Khách hàng thân thiết
                        rdlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Form\QuangHung\RptTopCustomers.rdlc");
                        var customerData = _reportRepo.GetTopCustomersByPoints(10);

                        reportViewer1.LocalReport.ReportPath = rdlcPath;
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetReport", customerData));
                        break;
                }

                // Cấu hình chế độ hiển thị trang in
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị báo cáo: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================================================
        // 2. SỰ KIỆN NÚT XUẤT RA MICROSOFT WORD (btnExportWord_Click)
        // =========================================================================
        private void btnExportWord_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem báo cáo đã được nạp hay chưa
                if (string.IsNullOrEmpty(reportViewer1.LocalReport.ReportPath))
                {
                    MessageBox.Show("Vui lòng tải dữ liệu báo cáo trước khi xuất file!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Đặt tên file gợi ý theo loại báo cáo và ngày giờ hiện tại
                string reportName = "";
                switch (cboReportType.SelectedIndex)
                {
                    case 0:
                        reportName = $"BaoCaoDoanhThu_{dtpFromDate.Value:yyyyMMdd}_{dtpToDate.Value:yyyyMMdd}";
                        break;
                    case 1:
                        reportName = $"TopMonBanChay_{DateTime.Now:yyyyMMdd_HHmm}";
                        break;
                    case 2:
                        reportName = $"TopKhachHangVIP_{DateTime.Now:yyyyMMdd_HHmm}";
                        break;
                    default:
                        reportName = "BaoCao";
                        break;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Word Document (*.docx)|*.docx|Word 97-2003 (*.doc)|*.doc";
                    sfd.FileName = reportName;
                    sfd.Title = "Lưu Báo Cáo Sang Microsoft Word";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        // Chọn định dạng phù hợp dựa theo phần mở rộng người dùng chọn
                        string format = sfd.FileName.EndsWith(".doc", StringComparison.OrdinalIgnoreCase) ? "WORD" : "WORDOPENXML";

                        Warning[] warnings;
                        string[] streamIds;
                        string mimeType = string.Empty;
                        string encoding = string.Empty;
                        string extension = string.Empty;

                        // Kết xuất trực tiếp nội dung RDLC sang mảng byte của Word
                        byte[] bytes = reportViewer1.LocalReport.Render(
                            format,
                            null,
                            out mimeType,
                            out encoding,
                            out extension,
                            out streamIds,
                            out warnings
                        );

                        // Ghi dữ liệu ra file
                        File.WriteAllBytes(sfd.FileName, bytes);

                        var confirmOpen = MessageBox.Show(
                            "Xuất báo cáo sang Word thành công!\nBạn có muốn mở file vừa xuất không?",
                            "Thành công",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information
                        );

                        // Mở ngay file Word nếu người dùng chọn Yes
                        if (confirmOpen == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file Word: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
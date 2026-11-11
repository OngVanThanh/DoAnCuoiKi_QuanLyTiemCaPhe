using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Configuration;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmReportWord : Form
    {
        private readonly ReportRepository _reportRepo = new ReportRepository();
        private readonly ShiftRepository _shiftRepo = new ShiftRepository();

        public FrmReportWord()
        {
            InitializeComponent();
        }

        private void FrmReportWord_Load(object sender, EventArgs e)
        {
            // 1. Nạp danh sách đúng 3 loại báo cáo bạn yêu cầu vào ComboBox
            cboReportType.Items.Clear();
            cboReportType.Items.Add("Báo cáo Doanh thu");
            cboReportType.Items.Add("Ca làm của nhân viên trong tuần sau");
            cboReportType.Items.Add("Danh sách khách hàng mua hàng nhiều nhất");
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
            // Bật bộ lọc ngày nếu là Báo cáo Doanh thu (Index 0), 2 báo cáo còn lại thì khóa lại
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

                // 1. Đọc thông tin quán trực tiếp từ thẻ <appSettings> của App.config
                string storeName = ConfigurationManager.AppSettings["ShopName"] ?? "MỘC CAFE POS";
                string storeAddress = ConfigurationManager.AppSettings["ShopAddress"] ?? "123 Nguyễn Huệ, Q.1, TP.HCM";

                // Danh sách tham số cơ bản (Tên quán, Địa chỉ quán)
                List<ReportParameter> shopParams = new List<ReportParameter>
                {
                    new ReportParameter("StoreName", storeName),
                    new ReportParameter("StoreAddress", storeAddress)
                };

                switch (cboReportType.SelectedIndex)
                {
                    case 0: // 1. Báo cáo Doanh thu
                        rdlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Form\QuangHung\RptRevenue.rdlc");
                        if (!File.Exists(rdlcPath))
                            rdlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RptRevenue.rdlc");

                        var revenueData = _reportRepo.GetRevenueReport(dtpFromDate.Value, dtpToDate.Value);

                        reportViewer1.LocalReport.ReportPath = rdlcPath;
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetReport", revenueData));
                        reportViewer1.LocalReport.SetParameters(shopParams);
                        break;

                    case 1: // 2. Ca làm của nhân viên trong tuần sau
                        rdlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Form\QuangHung\RptShiftSchedule.rdlc");
                        if (!File.Exists(rdlcPath))
                            rdlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RptShiftSchedule.rdlc");

                        // Lấy dữ liệu lịch tuần sau từ ShiftRepository
                        var shiftData = _shiftRepo.GetNextWeekScheduleForReport();

                        // Tự động tính khoảng ngày Thứ 2 đến Chủ nhật của tuần sau
                        DateTime today = DateTime.Today;
                        int daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
                        if (daysUntilNextMonday == 0) daysUntilNextMonday = 7;

                        DateTime nextMonday = today.AddDays(daysUntilNextMonday).Date;
                        DateTime nextSunday = nextMonday.AddDays(6).Date;
                        string weekRange = $"Áp dụng: Từ ngày {nextMonday:dd/MM/yyyy} đến ngày {nextSunday:dd/MM/yyyy}";

                        List<ReportParameter> shiftParams = new List<ReportParameter>
                        {
                            new ReportParameter("StoreName", storeName),
                            new ReportParameter("StoreAddress", storeAddress),
                            new ReportParameter("WeekRange", weekRange)
                        };

                        reportViewer1.LocalReport.ReportPath = rdlcPath;
                        reportViewer1.LocalReport.DataSources.Clear();
                        // Chú ý: Tên DataSet trên file RptShiftSchedule.rdlc là "DataSetSchedule"
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetSchedule", shiftData));
                        reportViewer1.LocalReport.SetParameters(shiftParams);
                        break;

                    case 2: // 3. Danh sách khách hàng mua hàng nhiều nhất
                        rdlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Form\QuangHung\RptTopCustomers.rdlc");
                        if (!File.Exists(rdlcPath))
                            rdlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RptTopCustomers.rdlc");

                        var customerData = _reportRepo.GetTopCustomersByPoints(10);

                        reportViewer1.LocalReport.ReportPath = rdlcPath;
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetReport", customerData));
                        reportViewer1.LocalReport.SetParameters(shopParams);
                        break;
                }

                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (ex.InnerException != null)
                {
                    msg += "\nChi tiết lỗi: " + ex.InnerException.Message;
                }
                MessageBox.Show("Lỗi hiển thị báo cáo: " + msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================================================
        // 2. SỰ KIỆN NÚT XUẤT RA MICROSOFT WORD (btnExportWord_Click)
        // =========================================================================
        private void btnExportWord_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra xem báo cáo đã được nạp hay chưa
                if (string.IsNullOrEmpty(reportViewer1.LocalReport.ReportPath))
                {
                    MessageBox.Show("Vui lòng tải dữ liệu báo cáo trước khi xuất file!",
                                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tự động đặt tên file Word gợi ý theo từng loại báo cáo
                string defaultFileName = $"BaoCao_{DateTime.Now:yyyyMMdd_HHmmss}";
                switch (cboReportType.SelectedIndex)
                {
                    case 0:
                        defaultFileName = $"BaoCao_DoanhThu_{dtpFromDate.Value:yyyyMMdd}_{dtpToDate.Value:yyyyMMdd}";
                        break;
                    case 1:
                        DateTime today = DateTime.Today;
                        int daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
                        if (daysUntilNextMonday == 0) daysUntilNextMonday = 7;
                        DateTime nextMonday = today.AddDays(daysUntilNextMonday).Date;
                        DateTime nextSunday = nextMonday.AddDays(6).Date;
                        defaultFileName = $"LichLamViec_TuanSau_{nextMonday:yyyyMMdd}_{nextSunday:yyyyMMdd}";
                        break;
                    case 2:
                        defaultFileName = $"Top_KhachHang_MuaNhieuNhat_{DateTime.Now:yyyyMMdd}";
                        break;
                }

                // 2. Mở hộp thoại chọn nơi lưu file Word
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Word Document (*.docx)|*.docx";
                    sfd.FileName = defaultFileName;
                    sfd.Title = "Lưu Báo Cáo Sang Microsoft Word";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        // 3. GỌI LỚP WORDEXPORTHELPER ĐỂ RENDER CHUẨN DOCX
                        string errorMessage;
                        bool isSuccess = WordExportHelper.ExportReportToWord(reportViewer1.LocalReport, sfd.FileName, out errorMessage);

                        if (isSuccess)
                        {
                            var confirm = MessageBox.Show(
                                "Xuất báo cáo sang Word thành công!\nBạn có muốn mở file vừa xuất không?",
                                "Thành công",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information
                            );

                            if (confirm == DialogResult.Yes)
                            {
                                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                            }
                        }
                        else
                        {
                            MessageBox.Show("Lỗi xuất file Word: " + errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
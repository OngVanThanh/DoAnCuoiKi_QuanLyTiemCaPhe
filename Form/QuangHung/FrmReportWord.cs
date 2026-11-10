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

                // 1. ĐỌC THÔNG TIN QUÁN TRỰC TIẾP TỪ THẺ <appSettings> CỦA App.config
                string storeName = ConfigurationManager.AppSettings["ShopName"] ?? "Tên Quán Cà Phê";
                string storeAddress = ConfigurationManager.AppSettings["ShopAddress"] ?? "Địa chỉ quán";

                // Khởi tạo danh sách tham số thông tin quán
                List<ReportParameter> shopParams = new List<ReportParameter>
        {
            new ReportParameter("StoreName", storeName),
            new ReportParameter("StoreAddress", storeAddress)
        };

                switch (cboReportType.SelectedIndex)
                {
                    case 0: // Báo cáo Doanh thu
                        rdlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Form\QuangHung\RptRevenue.rdlc");
                        var revenueData = _reportRepo.GetRevenueReport(dtpFromDate.Value, dtpToDate.Value);

                        reportViewer1.LocalReport.ReportPath = rdlcPath;
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetReport", revenueData));

                        // CHỈ TRUYỀN CÁC PARAMETER CÓ THẬT TRONG FILE RDLC (Ví dụ StoreName, StoreAddress)
                        List<ReportParameter> reportParams = new List<ReportParameter>
    {
        new ReportParameter("StoreName", ConfigurationManager.AppSettings["ShopName"] ?? "Tên Quán"),
        new ReportParameter("StoreAddress", ConfigurationManager.AppSettings["ShopAddress"] ?? "Địa chỉ quán")
    };

                        reportViewer1.LocalReport.SetParameters(reportParams);
                        break;

                    case 1: // Top Món bán chạy nhất
                        rdlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Form\QuangHung\RptBestSeller.rdlc");
                        var productData = _reportRepo.GetBestSellingProducts(10);

                        reportViewer1.LocalReport.ReportPath = rdlcPath;
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetReport", productData));

                        // Gán tham số Tên và Địa chỉ quán vào RptBestSeller.rdlc
                        reportViewer1.LocalReport.SetParameters(shopParams);
                        break;

                    case 2: // Top Khách hàng thân thiết
                        rdlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Form\QuangHung\RptTopCustomers.rdlc");
                        var customerData = _reportRepo.GetTopCustomersByPoints(10);

                        reportViewer1.LocalReport.ReportPath = rdlcPath;
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetReport", customerData));

                        // Gán tham số Tên và Địa chỉ quán vào RptTopCustomers.rdlc
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

                // 2. Mở hộp thoại chọn nơi lưu file Word
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Word Document (*.docx)|*.docx";
                    sfd.FileName = $"BaoCao_{DateTime.Now:yyyyMMdd_HHmmss}";
                    sfd.Title = "Lưu Báo Cáo Sang Microsoft Word";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        // 3. GỌI LỚP WORDEXPORTHELPER TẠI ĐÂY
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
using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmChart : Form
    {
        private readonly ReportRepository _reportRepo = new ReportRepository();
        private bool _isUpdatingDates = false;

        public FrmChart()
        {
            InitializeComponent();
        }

        private void FrmChart_Load(object sender, EventArgs e)
        {
            dgvRevenueStats.AutoGenerateColumns = false;
            ApplyQuickDateRange(7);
            LoadDashboardStatistics();
        }

        private void ApplyQuickDateRange(int daysBack)
        {
            _isUpdatingDates = true;
            dtpToDate.Value = DateTime.Today;
            dtpFromDate.Value = DateTime.Today.AddDays(-daysBack);
            _isUpdatingDates = false;
        }

        // Chỉ đổ dữ liệu biến thiên từ Database vào các điều khiển tĩnh đã thiết kế sẵn trên Designer
        private void LoadDashboardStatistics()
        {
            try
            {
                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date;

                if (fromDate > toDate)
                {
                    MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Lấy dữ liệu doanh thu theo khoảng ngày và Top món bán chạy
                List<RevenueStatisticDTO> revenueList = _reportRepo.GetRevenueReport(fromDate, toDate);
                List<TopProductDTO> topProducts = _reportRepo.GetBestSellingProducts(6);

                // 2. Đổ dữ liệu vào Bảng tổng hợp (các cột đã có sẵn trên Designer)
                dgvRevenueStats.AutoGenerateColumns = false;
                dgvRevenueStats.DataSource = null;
                dgvRevenueStats.DataSource = revenueList;

                // 3. Cập nhật 4 thẻ KPI tổng quan
                decimal totalRevenue = revenueList.Sum(x => x.TotalRevenue);
                int totalOrders = revenueList.Sum(x => x.TotalOrders);
                decimal totalDiscount = revenueList.Sum(x => x.TotalDiscount);

                lblKpiRevenueValue.Text = $"{totalRevenue:N0} đ";
                lblKpiOrdersValue.Text = $"{totalOrders:N0} đơn";
                lblKpiDiscountValue.Text = $"{totalDiscount:N0} đ";

                var top1 = topProducts.FirstOrDefault();
                lblKpiBestValue.Text = top1 != null
                    ? $"{top1.ProductName} ({top1.QuantitySold} ly)"
                    : "Chưa có dữ liệu";

                // 4. Đổ dữ liệu vào Biểu đồ cột Doanh thu (chartRevenue)
                var revSeries = chartRevenue.Series["DoanhThu"];
                revSeries.Points.Clear();

                foreach (var item in revenueList)
                {
                    revSeries.Points.AddXY(item.Date.ToString("dd/MM"), item.TotalRevenue);
                }

                // 5. Đổ dữ liệu vào Biểu đồ tròn Top món bán chạy (ChartProducts)
                var prodSeries = ChartProducts.Series["TopMon"];
                prodSeries.Points.Clear();

                foreach (var prod in topProducts)
                {
                    int ptIndex = prodSeries.Points.AddXY(prod.ProductName, prod.QuantitySold);
                    prodSeries.Points[ptIndex].LegendText = $"{prod.ProductName} ({prod.QuantitySold})";
                    prodSeries.Points[ptIndex].Label = $"{prod.QuantitySold}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu thống kê:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFilterChart_Click(object sender, EventArgs e)
        {
            LoadDashboardStatistics();
        }

        private void rdoWeek_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoWeek.Checked && !_isUpdatingDates)
            {
                ApplyQuickDateRange(7);
                LoadDashboardStatistics();
            }
        }

        private void rdoMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoMonth.Checked && !_isUpdatingDates)
            {
                ApplyQuickDateRange(30);
                LoadDashboardStatistics();
            }
        }

        private void rdoYear_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoYear.Checked && !_isUpdatingDates)
            {
                _isUpdatingDates = true;
                dtpFromDate.Value = new DateTime(DateTime.Today.Year, 1, 1);
                dtpToDate.Value = DateTime.Today;
                _isUpdatingDates = false;
                LoadDashboardStatistics();
            }
        }
    }
}
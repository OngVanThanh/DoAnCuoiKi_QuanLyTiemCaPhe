using System;
using System.Collections.Generic;
using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.BLL
{
    public class ReportService
    {
        private readonly ReportRepository _repo = new ReportRepository();

        public List<RevenueStatisticDTO> GetRevenue(DateTime fromDate, DateTime toDate)
        {
            return _repo.GetRevenueReport(fromDate, toDate);
        }

        public List<TopProductDTO> GetTopSelling(int top = 10)
        {
            return _repo.GetBestSellingProducts(top);
        }

        public List<CustomerDTO> GetTopCustomers(int top = 10)
        {
            return _repo.GetTopCustomersByPoints(top);
        }
    }
}
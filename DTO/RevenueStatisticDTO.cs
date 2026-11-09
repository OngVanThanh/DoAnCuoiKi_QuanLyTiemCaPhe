using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    public class RevenueStatisticDTO
    {
        // 1. Các trường thông tin cho báo cáo doanh thu
        public DateTime Date { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalRevenue { get; set; }

        // 2. Constructor không tham số (Bắt buộc để RDLC nhận diện Object DataSource)
        public RevenueStatisticDTO()
        {
            Date = DateTime.Today;
            TotalOrders = 0;
            TotalDiscount = 0m;
            TotalRevenue = 0m;
        }

        // 3. Constructor đầy đủ tham số
        public RevenueStatisticDTO(DateTime date, int totalOrders, decimal totalDiscount, decimal totalRevenue)
        {
            Date = date;
            TotalOrders = totalOrders;
            TotalDiscount = totalDiscount;
            TotalRevenue = totalRevenue;
        }

        public override string ToString()
        {
            return $"{Date:dd/MM/yyyy}: {TotalOrders} đơn - Doanh thu: {TotalRevenue:N0} đ";
        }
    }
}
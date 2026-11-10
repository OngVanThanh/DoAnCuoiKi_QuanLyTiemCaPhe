using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    public class RevenueStatisticDTO
    {
        public DateTime Date { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalAmount { get; set; }     // Thêm thuộc tính này (Tiền gốc trước giảm)
        public decimal TotalDiscount { get; set; }   // Tiền giảm giá
        public decimal TotalRevenue { get; set; }    // Tiền thực thu sau giảm (FinalAmount)

        public RevenueStatisticDTO()
        {
            Date = DateTime.Today;
            TotalOrders = 0;
            TotalAmount = 0m;
            TotalDiscount = 0m;
            TotalRevenue = 0m;
        }

        public RevenueStatisticDTO(DateTime date, int totalOrders, decimal totalAmount, decimal totalDiscount, decimal totalRevenue)
        {
            Date = date;
            TotalOrders = totalOrders;
            TotalAmount = totalAmount;
            TotalDiscount = totalDiscount;
            TotalRevenue = totalRevenue;
        }
    }
}
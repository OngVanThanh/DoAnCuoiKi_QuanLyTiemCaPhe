using System;
using System.Collections.Generic;
using System.Linq;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    public class ReportRepository
    {
        private CoffeeShopDataContextDataContext GetContext()
        {
            return new CoffeeShopDataContextDataContext(ConfigHelper.GetConnectionString("CoffeeShopDB"));
        }
        // 1. Thống kê doanh thu theo khoảng thời gian
        public List<RevenueStatisticDTO> GetRevenueReport(DateTime fromDate, DateTime toDate)
        {
            using (var db = GetContext())
            {
                var endDate = toDate.Date.AddDays(1).AddSeconds(-1);

                return db.Orders
                    .Where(o => o.OrderDate >= fromDate.Date
                             && o.OrderDate <= endDate
                             && o.Status == "Đã thanh toán")
                    .GroupBy(o => o.OrderDate.Value.Date)
                    .Select(g => new RevenueStatisticDTO
                    {
                        Date = g.Key,
                        TotalOrders = g.Count(),
                        TotalAmount = g.Sum(x => x.TotalAmount),            // Tiền gốc trước giảm
                        TotalDiscount = g.Sum(x => x.DiscountAmount ?? 0m), // Chiết khấu
                        TotalRevenue = g.Sum(x => x.FinalAmount)            // Thực thu
                    })
                    .OrderBy(x => x.Date)
                    .ToList();
            }
        }

        // 2. Thống kê Top món bán chạy nhất (Best Sellers)
        public List<TopProductDTO> GetBestSellingProducts(int topCount = 10)
        {
            using (var db = GetContext())
            {
                return db.OrderDetails
                    .Where(d => d.Order.Status == "Đã thanh toán")
                    .GroupBy(d => new
                    {
                        d.ProductId,
                        d.Product.ProductName,
                        d.Product.Category.CategoryName
                    })
                    .Select(g => new TopProductDTO
                    {
                        ProductId = g.Key.ProductId,
                        ProductName = g.Key.ProductName,
                        CategoryName = g.Key.CategoryName,
                        QuantitySold = g.Sum(x => x.Quantity),
                        TotalAmount = g.Sum(x => x.Quantity * x.UnitPrice)
                    })
                    .OrderByDescending(x => x.QuantitySold)
                    .Take(topCount)
                    .ToList();
            }
        }

        // 3. Top khách hàng có điểm tích lũy cao nhất
        public List<CustomerDTO> GetTopCustomersByPoints(int topCount = 10)
        {
            using (var db = GetContext())
            {
                return db.Customers
                    .OrderByDescending(c => c.RewardPoints)
                    .Take(topCount)
                    .Select(c => new CustomerDTO
                    {
                        CustomerName = c.CustomerName,
                        Phone = c.Phone,
                        RewardPoints = c.RewardPoints ?? 0,
                        MembershipTier = c.MembershipTier,
                        RegistrationDate = (c.RegistrationDate ?? DateTime.Now).Date
                    })
                    .ToList();
            }
        }
    }
}
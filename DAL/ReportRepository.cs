using System;
using System.Collections.Generic;
using System.Linq;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    public class ReportRepository
    {
        // 1. Thống kê doanh thu theo khoảng thời gian
        public List<RevenueStatisticDTO> GetRevenueReport(DateTime fromDate, DateTime toDate)
        {
            using (var db = new CoffeeShopDataContextDataContext())
            {
                var query = from o in db.Orders
                            where o.OrderDate >= fromDate.Date
                               && o.OrderDate <= toDate.Date.AddDays(1).AddSeconds(-1)
                               && o.Status == "Đã thanh toán"
                            group o by o.OrderDate.Value.Date into g
                            select new RevenueStatisticDTO
                            {
                                Date = g.Key,
                                TotalOrders = g.Count(),
                                TotalAmount = g.Sum(x => x.TotalAmount),            // Tiền gốc trước giảm
                                TotalDiscount = g.Sum(x => x.DiscountAmount ?? 0m), // Chiết khấu
                                TotalRevenue = g.Sum(x => x.FinalAmount)      // Thực thu
                            };

                return query.OrderBy(x => x.Date).ToList();
            }
        }

        // 2. Thống kê Top món bán chạy nhất (Best Sellers)
        public List<TopProductDTO> GetBestSellingProducts(int topCount = 10)
        {
            using (var db = new CoffeeShopDataContextDataContext())
            {
                var query = from d in db.OrderDetails
                            where d.Order.Status == "Đã thanh toán"
                            group d by new { d.ProductId, d.Product.ProductName, d.Product.Category.CategoryName } into g
                            select new TopProductDTO
                            {
                                ProductId = g.Key.ProductId,
                                ProductName = g.Key.ProductName,
                                CategoryName = g.Key.CategoryName,
                                QuantitySold = g.Sum(x => x.Quantity),
                                TotalAmount = g.Sum(x => (x.Quantity) * (x.UnitPrice))
                            };

                return query.OrderByDescending(x => x.QuantitySold).Take(topCount).ToList();
            }
        }

        // 3. Top khách hàng có điểm tích lũy cao nhất
        public List<CustomerDTO> GetTopCustomersByPoints(int topCount = 10)
        {
            using (var db = new CoffeeShopDataContextDataContext())
            {
                var query = from c in db.Customers
                            orderby c.RewardPoints descending
                            select new CustomerDTO
                            {
                                
                                CustomerName = c.CustomerName,
                                Phone = c.Phone,
                                RewardPoints = c.RewardPoints ?? 0,
                                MembershipTier = c.MembershipTier,
                                RegistrationDate = (c.RegistrationDate ?? DateTime.Now).Date
                            };

                return query.Take(topCount).ToList();
            }
        }
    }
}
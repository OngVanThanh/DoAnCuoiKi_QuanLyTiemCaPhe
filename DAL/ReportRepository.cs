using DoAnCuoiKi_QuanLyTiemCaPhe.DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

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

        // 2. Lấy danh sách ca làm việc của tuần sau để in báo cáo
        public List<ShiftReportDTO> GetNextWeekScheduleForReport()
        {
            using (var db = GetContext())
            {
                // 1. Tự động tính khoảng thời gian từ Thứ Hai đến Chủ Nhật của tuần sau
                DateTime today = DateTime.Today;
                int daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
                if (daysUntilNextMonday == 0) daysUntilNextMonday = 7;

                DateTime nextMonday = today.AddDays(daysUntilNextMonday).Date;
                DateTime nextSunday = nextMonday.AddDays(6).Date;

                // 2. Truy vấn LINQ Lambda thuần túy (Pure LINQ Lambda)
                return db.EmployeeShifts
                    .Where(s => s.WorkDate >= nextMonday && s.WorkDate <= nextSunday)
                    .OrderBy(s => s.WorkDate)
                    .ThenBy(s => s.ShiftId)
                    .ThenBy(s => s.MisionId)
                    .Select(s => new ShiftReportDTO
                    {
                        WorkDate = s.WorkDate,
                        ShiftId = s.ShiftId ?? 0,
                        MissionName = s.Mision != null ? s.Mision.MissisonName : string.Empty,
                        EmployeeName = s.Employee != null ? s.Employee.FullName : "Chưa phân công",
                        Status = s.Status
                    })
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
        // 4. Thống kê Top món bán chạy nhất (Best Sellers)
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
    }
}
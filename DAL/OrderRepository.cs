using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    /// <summary>
    /// Lớp truy xuất dữ liệu (DAL) phục vụ nghiệp vụ Gọi món (Order), Chi tiết hóa đơn (OrderDetails),
    /// Thực đơn (Products/Categories), Khuyến mãi (Promotions) và Chuyển/Gộp bàn.
    /// </summary>
    internal class OrderRepository
    {
        // 1. Khởi tạo DataContext kết nối cơ sở dữ liệu
        public CoffeeShopDataContextDataContext GetContext()
        {
            return new CoffeeShopDataContextDataContext(ConfigHelper.GetConnectionString("CoffeeShopDB"));
        }

        // 2. Lấy danh sách toàn bộ nhóm món (Categories)
        public List<Category> GetAllCategories()
        {
            using (var db = GetContext())
            {
                return db.Categories.OrderBy(c => c.Id).ToList();
            }
        }

        // 3. Lấy danh sách món đang kinh doanh (lọc theo nhóm món và từ khóa tìm kiếm nhanh)
        public List<Product> GetActiveProducts(int? categoryId = null, string keyword = null)
        {
            using (var db = GetContext())
            {
                var query = db.Products.Where(p => p.IsActive == true);

                if (categoryId.HasValue && categoryId.Value > 0)
                {
                    query = query.Where(p => p.CategoryId == categoryId.Value);
                }

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    string kw = keyword.Trim().ToLower();
                    query = query.Where(p => p.ProductName.ToLower().Contains(kw));
                }

                return query.OrderBy(p => p.CategoryId).ThenBy(p => p.ProductName).ToList();
            }
        }

        // 4. Lấy thông tin món theo Id
        public Product GetProductById(int productId)
        {
            using (var db = GetContext())
            {
                return db.Products.FirstOrDefault(p => p.Id == productId);
            }
        }

        // 5. Tìm chương trình khuyến mãi đang hoạt động theo mã code
        public Promotion GetActivePromotionByCode(string promoCode)
        {
            if (string.IsNullOrWhiteSpace(promoCode))
                return null;

            string code = promoCode.Trim().ToUpper();
            DateTime now = DateTime.Now;

            using (var db = GetContext())
            {
                return db.Promotions.FirstOrDefault(p =>
                    p.PromoCode.ToUpper() == code &&
                    p.IsActive == true &&
                    p.StartDate <= now &&
                    p.EndDate >= now);
            }
        }

        // 6. Lấy đơn hàng chưa thanh toán của một bàn
        public Order GetUnpaidOrderByTableId(int tableId)
        {
            using (var db = GetContext())
            {
                return db.Orders
                    .OrderByDescending(o => o.Id)
                    .FirstOrDefault(o => o.TableId == tableId && o.Status == "Chưa thanh toán");
            }
        }

        // 7. Lấy đơn hàng theo Id
        public Order GetOrderById(int orderId)
        {
            using (var db = GetContext())
            {
                return db.Orders.FirstOrDefault(o => o.Id == orderId);
            }
        }

        // 8. Lấy thông tin OrderDTO đầy đủ (kèm tên bàn, tên nhân viên, khách hàng, khuyến mãi) để hiển thị và in hóa đơn
        public OrderDTO GetOrderDTOById(int orderId)
        {
            using (var db = GetContext())
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);
                if (order == null)
                    return null;

                var table = db.Tables.FirstOrDefault(t => t.Id == order.TableId);
                var employee = db.Employees.FirstOrDefault(e => e.Id == order.EmployeeId);
                var customer = order.CustomerId.HasValue
                    ? db.Customers.FirstOrDefault(c => c.Id == order.CustomerId.Value)
                    : null;
                var promo = order.PromotionId.HasValue
                    ? db.Promotions.FirstOrDefault(p => p.Id == order.PromotionId.Value)
                    : null;

                return new OrderDTO
                {
                    Id = order.Id,
                    TableId = order.TableId,
                    TableName = table != null ? table.TableName : $"Bàn #{order.TableId}",
                    EmployeeId = order.EmployeeId,
                    EmployeeName = employee != null ? employee.FullName : "Nhân viên thu ngân",
                    CustomerId = order.CustomerId,
                    CustomerName = customer != null ? customer.CustomerName : "Khách lẻ",
                    OrderDate = order.OrderDate ?? DateTime.Now,
                    TotalAmount = order.TotalAmount,
                    DiscountAmount = order.DiscountAmount ?? 0m,
                    FinalAmount = order.FinalAmount,
                    Status = order.Status,
                    PromotionId = order.PromotionId,
                    PromoCode = promo != null ? promo.PromoCode : string.Empty
                };
            }
        }

        // 9. Tạo mới đơn hàng cho bàn và cập nhật trạng thái bàn thành "Có khách"
        public int CreateOrder(int tableId, int employeeId, int? customerId = null)
        {
            using (var db = GetContext())
            {
                // Đảm bảo EmployeeId tồn tại trong bảng Employees
                int validEmployeeId = employeeId;
                if (!db.Employees.Any(e => e.Id == validEmployeeId))
                {
                    var firstEmp = db.Employees.FirstOrDefault();
                    if (firstEmp != null)
                        validEmployeeId = firstEmp.Id;
                }

                var newOrder = new Order
                {
                    TableId = tableId,
                    EmployeeId = validEmployeeId,
                    CustomerId = customerId,
                    OrderDate = DateTime.Now,
                    TotalAmount = 0m,
                    DiscountAmount = 0m,
                    FinalAmount = 0m,
                    Status = "Chưa thanh toán",
                    PromotionId = null
                };

                db.Orders.InsertOnSubmit(newOrder);

                var table = db.Tables.FirstOrDefault(t => t.Id == tableId);
                if (table != null)
                {
                    table.Status = "Có khách";
                }

                db.SubmitChanges();
                return newOrder.Id;
            }
        }

        // 10. Lấy danh sách chi tiết các món trong đơn hàng (OrderDetailDTO)
        public List<OrderDetailDTO> GetOrderDetailsByOrderId(int orderId)
        {
            using (var db = GetContext())
            {
                var query =
                    from detail in db.OrderDetails
                    join product in db.Products
                        on detail.ProductId equals product.Id
                    where detail.OrderId == orderId
                    orderby detail.Id
                    select new OrderDetailDTO
                    {
                        Id = detail.Id,
                        OrderId = detail.OrderId,
                        ProductId = detail.ProductId,
                        ProductName = product.ProductName,
                        Unit = product.Unit,
                        Quantity = detail.Quantity,
                        UnitPrice = detail.UnitPrice,
                        TotalPrice = detail.TotalPrice
                    };

                return query.ToList();
            }
        }

        // 11. Thêm món hoặc cộng dồn số lượng món vào đơn hàng, đồng thời tính lại tổng tiền đơn hàng
        public bool AddOrUpdateOrderDetail(int orderId, int productId, int quantityToAdd)
        {
            if (quantityToAdd <= 0)
                return false;

            using (var db = GetContext())
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);
                var product = db.Products.FirstOrDefault(p => p.Id == productId);

                if (order == null || product == null)
                    return false;

                var existingDetail = db.OrderDetails.FirstOrDefault(d =>
                    d.OrderId == orderId &&
                    d.ProductId == productId);

                if (existingDetail != null)
                {
                    existingDetail.Quantity += quantityToAdd;
                    existingDetail.UnitPrice = product.Price;
                    existingDetail.TotalPrice = existingDetail.Quantity * existingDetail.UnitPrice;
                }
                else
                {
                    var newDetail = new OrderDetail
                    {
                        OrderId = orderId,
                        ProductId = productId,
                        Quantity = quantityToAdd,
                        UnitPrice = product.Price,
                        TotalPrice = quantityToAdd * product.Price
                    };
                    db.OrderDetails.InsertOnSubmit(newDetail);
                }

                db.SubmitChanges();

                // Cập nhật lại tổng tiền hàng của hóa đơn
                RecalculateOrderTotalInternal(db, orderId);
                return true;
            }
        }

        // 12. Xóa món khỏi đơn hàng (nếu đơn hàng không còn món nào thì xóa đơn nháp và trả bàn về "Trống")
        public bool RemoveOrderDetail(int orderId, int productId)
        {
            using (var db = GetContext())
            {
                var detail = db.OrderDetails.FirstOrDefault(d =>
                    d.OrderId == orderId &&
                    d.ProductId == productId);

                if (detail == null)
                    return false;

                db.OrderDetails.DeleteOnSubmit(detail);
                db.SubmitChanges();

                bool hasRemainingItems = db.OrderDetails.Any(d => d.OrderId == orderId);
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);

                if (!hasRemainingItems && order != null && order.Status == "Chưa thanh toán")
                {
                    int tableId = order.TableId;
                    db.Orders.DeleteOnSubmit(order);

                    var table = db.Tables.FirstOrDefault(t => t.Id == tableId);
                    if (table != null)
                    {
                        table.Status = "Trống";
                    }

                    db.SubmitChanges();
                }
                else
                {
                    RecalculateOrderTotalInternal(db, orderId);
                }

                return true;
            }
        }

        // 13. Hàm nội bộ tính lại TotalAmount và FinalAmount của đơn hàng từ các dòng OrderDetails
        private void RecalculateOrderTotalInternal(CoffeeShopDataContextDataContext db, int orderId)
        {
            var order = db.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
                return;

            decimal total = db.OrderDetails
                .Where(d => d.OrderId == orderId)
                .Select(d => (decimal?)d.TotalPrice)
                .Sum() ?? 0m;

            order.TotalAmount = total;
            decimal discount = order.DiscountAmount ?? 0m;
            if (discount > total)
                discount = total;

            order.DiscountAmount = discount;
            order.FinalAmount = total - discount;
            db.SubmitChanges();
        }

        // 14. Cập nhật thông tin giảm giá và tổng tiền cho đơn hàng
        public bool UpdateOrderAmounts(int orderId, decimal totalAmount, decimal discountAmount, decimal finalAmount, int? promotionId)
        {
            using (var db = GetContext())
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);
                if (order == null)
                    return false;

                order.TotalAmount = totalAmount;
                order.DiscountAmount = discountAmount;
                order.FinalAmount = finalAmount;
                order.PromotionId = promotionId;

                db.SubmitChanges();
                return true;
            }
        }

        // 15. Hoàn tất thanh toán hóa đơn và giải phóng bàn về trạng thái "Trống"
        public bool CompleteOrderPayment(int orderId, decimal totalAmount, decimal discountAmount, decimal finalAmount, int? promotionId)
        {
            using (var db = GetContext())
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);
                if (order == null)
                    return false;

                order.TotalAmount = totalAmount;
                order.DiscountAmount = discountAmount;
                order.FinalAmount = finalAmount;
                order.PromotionId = promotionId;
                order.OrderDate = DateTime.Now;
                order.Status = "Đã thanh toán";

                var table = db.Tables.FirstOrDefault(t => t.Id == order.TableId);
                if (table != null)
                {
                    table.Status = "Trống";
                }

                db.SubmitChanges();
                return true;
            }
        }

        // 16. Chuyển bàn hoặc Gộp bàn
        public bool SwitchOrMergeTable(int sourceTableId, int targetTableId)
        {
            if (sourceTableId <= 0 || targetTableId <= 0 || sourceTableId == targetTableId)
                return false;

            using (var db = GetContext())
            {
                var sourceTable = db.Tables.FirstOrDefault(t => t.Id == sourceTableId);
                var targetTable = db.Tables.FirstOrDefault(t => t.Id == targetTableId);

                if (sourceTable == null || targetTable == null)
                    return false;

                var sourceOrder = db.Orders
                    .OrderByDescending(o => o.Id)
                    .FirstOrDefault(o => o.TableId == sourceTableId && o.Status == "Chưa thanh toán");

                if (sourceOrder == null)
                    return false;

                var targetOrder = db.Orders
                    .OrderByDescending(o => o.Id)
                    .FirstOrDefault(o => o.TableId == targetTableId && o.Status == "Chưa thanh toán");

                if (targetOrder == null)
                {
                    // Trường hợp 1: Bàn đích đang trống -> Chuyển hóa đơn sang bàn đích
                    sourceOrder.TableId = targetTableId;
                    sourceTable.Status = "Trống";
                    targetTable.Status = "Có khách";
                    db.SubmitChanges();
                }
                else
                {
                    // Trường hợp 2: Bàn đích đang có khách -> Gộp các món từ bàn nguồn sang bàn đích
                    var sourceDetails = db.OrderDetails.Where(d => d.OrderId == sourceOrder.Id).ToList();
                    var targetDetails = db.OrderDetails.Where(d => d.OrderId == targetOrder.Id).ToList();

                    foreach (var srcItem in sourceDetails)
                    {
                        var existingTargetItem = targetDetails.FirstOrDefault(d => d.ProductId == srcItem.ProductId);
                        if (existingTargetItem != null)
                        {
                            existingTargetItem.Quantity += srcItem.Quantity;
                            existingTargetItem.TotalPrice = existingTargetItem.Quantity * existingTargetItem.UnitPrice;
                            db.OrderDetails.DeleteOnSubmit(srcItem);
                        }
                        else
                        {
                            srcItem.OrderId = targetOrder.Id;
                        }
                    }

                    db.SubmitChanges();

                    // Xóa đơn hàng rỗng ở bàn nguồn và trả trạng thái bàn nguồn về "Trống"
                    db.Orders.DeleteOnSubmit(sourceOrder);
                    sourceTable.Status = "Trống";
                    targetTable.Status = "Có khách";
                    db.SubmitChanges();

                    // Tính lại tổng tiền cho hóa đơn bàn đích sau khi gộp
                    RecalculateOrderTotalInternal(db, targetOrder.Id);
                }

                return true;
            }
        }
    }
}

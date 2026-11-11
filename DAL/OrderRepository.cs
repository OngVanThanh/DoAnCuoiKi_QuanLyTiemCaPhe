using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    /// <summary>
    /// Lớp truy xuất dữ liệu (DAL) phục vụ nghiệp vụ Gọi món (Order), Chi tiết hóa đơn (OrderDetails),
    /// Thực đơn (Products/Categories), Khuyến mãi (Promotions) và Chuyển/Gộp bàn.
    /// Sử dụng TransactionScope trong LINQ to SQL để đảm bảo tính toàn vẹn giao dịch (Commit / Rollback),
    /// đồng thời hỗ trợ dữ liệu mẫu từ CoffeeShopDB.sql khi chạy offline.
    /// </summary>
    internal class OrderRepository
    {
        private readonly TableRepository _tableRepo = new TableRepository();

        // Dữ liệu mẫu đồng bộ với CoffeeShopDB.sql phục vụ chạy kiểm thử khi chưa bật SQL Server
        private static readonly List<Category> _fallbackCategories = new List<Category>
        {
            new Category { Id = 1, CategoryName = "Cà phê truyền thống", Description = "Cà phê pha phin, pha máy" },
            new Category { Id = 2, CategoryName = "Trà & Trà sữa", Description = "Các loại trà trái cây và trà sữa" },
            new Category { Id = 3, CategoryName = "Nước ép & Sinh tố", Description = "Nước ép nguyên chất tươi mát" },
            new Category { Id = 4, CategoryName = "Bánh ngọt", Description = "Bánh ăn kèm cà phê" }
        };

        private static readonly List<Product> _fallbackProducts = new List<Product>
        {
            new Product { Id = 1, ProductName = "Cà phê đen đá", CategoryId = 1, Price = 25000m, Unit = "Ly", ImagePath = "", IsActive = true },
            new Product { Id = 2, ProductName = "Cà phê sữa đá", CategoryId = 1, Price = 29000m, Unit = "Ly", ImagePath = "", IsActive = true },
            new Product { Id = 3, ProductName = "Bạc xỉu", CategoryId = 1, Price = 32000m, Unit = "Ly", ImagePath = "", IsActive = true },
            new Product { Id = 4, ProductName = "Trà đào cam sả", CategoryId = 2, Price = 40000m, Unit = "Ly", ImagePath = "", IsActive = true },
            new Product { Id = 5, ProductName = "Trà sữa trân châu hoàng gia", CategoryId = 2, Price = 45000m, Unit = "Ly", ImagePath = "", IsActive = true },
            new Product { Id = 6, ProductName = "Nước ép cam tươi", CategoryId = 3, Price = 38000m, Unit = "Ly", ImagePath = "", IsActive = true },
            new Product { Id = 7, ProductName = "Sinh tố bơ sáp", CategoryId = 3, Price = 45000m, Unit = "Ly", ImagePath = "", IsActive = true },
            new Product { Id = 8, ProductName = "Bánh Tiramisu", CategoryId = 4, Price = 35000m, Unit = "Cái", ImagePath = "", IsActive = true },
            new Product { Id = 9, ProductName = "Bánh Croissant bơ tỏi", CategoryId = 4, Price = 30000m, Unit = "Cái", ImagePath = "", IsActive = true }
        };

        private static readonly List<Promotion> _fallbackPromotions = new List<Promotion>
        {
            new Promotion { Id = 1, PromoCode = "CHAOMUNG", DiscountPercent = 10, StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2027, 12, 31), IsActive = true },
            new Promotion { Id = 2, PromoCode = "VIPCOFFEE", DiscountPercent = 15, StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2027, 12, 31), IsActive = true }
        };

        internal static readonly List<Order> FallbackOrders = new List<Order>();
        internal static readonly List<OrderDetail> FallbackOrderDetails = new List<OrderDetail>();

        // 1. Khởi tạo DataContext kết nối cơ sở dữ liệu
        public CoffeeShopDataContextDataContext GetContext()
        {
            return new CoffeeShopDataContextDataContext(ConfigHelper.GetConnectionString("CoffeeShopDB"));
        }

        // 2. Lấy danh sách toàn bộ nhóm món (Categories)
        public List<Category> GetAllCategories()
        {
            if (!_tableRepo.IsDatabaseOnline())
                return _fallbackCategories.OrderBy(c => c.Id).ToList();

            using (var db = GetContext())
            {
                return db.Categories.OrderBy(c => c.Id).ToList();
            }
        }

        // 3. Lấy danh sách món đang kinh doanh (lọc theo nhóm món và từ khóa tìm kiếm nhanh)
        public List<Product> GetActiveProducts(int? categoryId = null, string keyword = null)
        {
            if (!_tableRepo.IsDatabaseOnline())
            {
                IEnumerable<Product> q = _fallbackProducts.Where(p => p.IsActive == true);
                if (categoryId.HasValue && categoryId.Value > 0)
                    q = q.Where(p => p.CategoryId == categoryId.Value);

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    string kw = keyword.Trim().ToLower();
                    q = q.Where(p => p.ProductName.ToLower().Contains(kw));
                }
                return q.OrderBy(p => p.CategoryId).ThenBy(p => p.ProductName).ToList();
            }

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
            if (!_tableRepo.IsDatabaseOnline())
                return _fallbackProducts.FirstOrDefault(p => p.Id == productId);

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

            if (!_tableRepo.IsDatabaseOnline())
            {
                return _fallbackPromotions.FirstOrDefault(p =>
                    p.PromoCode.ToUpper() == code &&
                    p.IsActive == true &&
                    p.StartDate <= now &&
                    p.EndDate >= now);
            }

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
            if (!_tableRepo.IsDatabaseOnline())
            {
                return FallbackOrders
                    .OrderByDescending(o => o.Id)
                    .FirstOrDefault(o => o.TableId == tableId && o.Status == "Chưa thanh toán");
            }

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
            if (!_tableRepo.IsDatabaseOnline())
                return FallbackOrders.FirstOrDefault(o => o.Id == orderId);

            using (var db = GetContext())
            {
                return db.Orders.FirstOrDefault(o => o.Id == orderId);
            }
        }

        // 8. Lấy thông tin OrderDTO đầy đủ để hiển thị và in hóa đơn
        public OrderDTO GetOrderDTOById(int orderId)
        {
            if (!_tableRepo.IsDatabaseOnline())
            {
                var order = FallbackOrders.FirstOrDefault(o => o.Id == orderId);
                if (order == null)
                    return null;

                var table = TableRepository.FallbackTables.FirstOrDefault(t => t.Id == order.TableId);
                var promo = order.PromotionId.HasValue
                    ? _fallbackPromotions.FirstOrDefault(p => p.Id == order.PromotionId.Value)
                    : null;

                return new OrderDTO
                {
                    Id = order.Id,
                    TableId = order.TableId,
                    TableName = table != null ? table.TableName : $"Bàn #{order.TableId}",
                    EmployeeId = order.EmployeeId,
                    EmployeeName = UserSession.CurrentUser != null ? UserSession.CurrentUser.Username : "Trần Thị Thu Ngân",
                    CustomerId = order.CustomerId,
                    CustomerName = "Khách lẻ",
                    OrderDate = order.OrderDate ?? DateTime.Now,
                    TotalAmount = order.TotalAmount,
                    DiscountAmount = order.DiscountAmount ?? 0m,
                    FinalAmount = order.FinalAmount,
                    Status = order.Status,
                    PromotionId = order.PromotionId,
                    PromoCode = promo != null ? promo.PromoCode : string.Empty
                };
            }

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

        // 9. Tạo mới đơn hàng cho bàn và cập nhật trạng thái bàn thành "Có khách" (bọc TransactionScope toàn vẹn)
        public int CreateOrder(int tableId, int employeeId, int? customerId = null)
        {
            if (!_tableRepo.IsDatabaseOnline())
            {
                int nextId = FallbackOrders.Count > 0 ? FallbackOrders.Max(o => o.Id) + 1 : 1;
                var newOrder = new Order
                {
                    Id = nextId,
                    TableId = tableId,
                    EmployeeId = employeeId > 0 ? employeeId : 1,
                    CustomerId = customerId,
                    OrderDate = DateTime.Now,
                    TotalAmount = 0m,
                    DiscountAmount = 0m,
                    FinalAmount = 0m,
                    Status = "Chưa thanh toán",
                    PromotionId = null
                };
                FallbackOrders.Add(newOrder);

                var table = TableRepository.FallbackTables.FirstOrDefault(t => t.Id == tableId);
                if (table != null)
                    table.Status = "Có khách";

                return newOrder.Id;
            }

            using (var scope = new TransactionScope())
            {
                using (var db = GetContext())
                {
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
                    scope.Complete();
                    return newOrder.Id;
                }
            }
        }

        // 10. Lấy danh sách chi tiết các món trong đơn hàng (OrderDetailDTO)
        public List<OrderDetailDTO> GetOrderDetailsByOrderId(int orderId)
        {
            if (!_tableRepo.IsDatabaseOnline())
            {
                return FallbackOrderDetails
                    .Where(detail => detail.OrderId == orderId)
                    .Join(_fallbackProducts,
                          detail => detail.ProductId,
                          product => product.Id,
                          (detail, product) => new OrderDetailDTO
                          {
                              Id = detail.Id,
                              OrderId = detail.OrderId,
                              ProductId = detail.ProductId,
                              ProductName = product.ProductName,
                              Unit = product.Unit,
                              Quantity = detail.Quantity,
                              UnitPrice = detail.UnitPrice,
                              TotalPrice = detail.TotalPrice
                          })
                    .OrderBy(d => d.Id)
                    .ToList();
            }

            using (var db = GetContext())
            {
                return db.OrderDetails
                    .Where(d => d.OrderId == orderId)
                    .OrderBy(d => d.Id)
                    .Select(d => new OrderDetailDTO
                    {
                        Id = d.Id,
                        OrderId = d.OrderId,
                        ProductId = d.ProductId,
                        ProductName = d.Product != null ? d.Product.ProductName : string.Empty,
                        Unit = d.Product != null ? d.Product.Unit : string.Empty,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice,
                        TotalPrice = d.TotalPrice
                    })
                    .ToList();
            }
        }

        // 11. Thêm món hoặc cộng dồn số lượng món vào đơn hàng, đồng thời tính lại tổng tiền đơn hàng (bọc TransactionScope)
        public bool AddOrUpdateOrderDetail(int orderId, int productId, int quantityToAdd)
        {
            if (quantityToAdd <= 0)
                return false;

            if (!_tableRepo.IsDatabaseOnline())
            {
                var order = FallbackOrders.FirstOrDefault(o => o.Id == orderId);
                var product = _fallbackProducts.FirstOrDefault(p => p.Id == productId);
                if (order == null || product == null)
                    return false;

                var existing = FallbackOrderDetails.FirstOrDefault(d => d.OrderId == orderId && d.ProductId == productId);
                if (existing != null)
                {
                    existing.Quantity += quantityToAdd;
                    existing.UnitPrice = product.Price;
                    existing.TotalPrice = existing.Quantity * existing.UnitPrice;
                }
                else
                {
                    int nextId = FallbackOrderDetails.Count > 0 ? FallbackOrderDetails.Max(d => d.Id) + 1 : 1;
                    FallbackOrderDetails.Add(new OrderDetail
                    {
                        Id = nextId,
                        OrderId = orderId,
                        ProductId = productId,
                        Quantity = quantityToAdd,
                        UnitPrice = product.Price,
                        TotalPrice = quantityToAdd * product.Price
                    });
                }

                decimal total = FallbackOrderDetails.Where(d => d.OrderId == orderId).Sum(d => d.TotalPrice);
                order.TotalAmount = total;
                order.FinalAmount = Math.Max(0m, total - (order.DiscountAmount ?? 0m));
                return true;
            }

            using (var scope = new TransactionScope())
            {
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
                    RecalculateOrderTotalInternal(db, orderId);
                    scope.Complete();
                    return true;
                }
            }
        }

        // 12. Xóa món khỏi đơn hàng (bọc TransactionScope đảm bảo đồng bộ Hóa đơn & Bàn nếu xóa hết món)
        public bool RemoveOrderDetail(int orderId, int productId)
        {
            if (!_tableRepo.IsDatabaseOnline())
            {
                var detail = FallbackOrderDetails.FirstOrDefault(d => d.OrderId == orderId && d.ProductId == productId);
                if (detail == null)
                    return false;

                FallbackOrderDetails.Remove(detail);
                var order = FallbackOrders.FirstOrDefault(o => o.Id == orderId);
                bool hasRemaining = FallbackOrderDetails.Any(d => d.OrderId == orderId);

                if (!hasRemaining && order != null && order.Status == "Chưa thanh toán")
                {
                    int tableId = order.TableId;
                    FallbackOrders.Remove(order);
                    var table = TableRepository.FallbackTables.FirstOrDefault(t => t.Id == tableId);
                    if (table != null)
                        table.Status = "Trống";
                }
                else if (order != null)
                {
                    order.TotalAmount = FallbackOrderDetails.Where(d => d.OrderId == orderId).Sum(d => d.TotalPrice);
                    order.FinalAmount = Math.Max(0m, order.TotalAmount - (order.DiscountAmount ?? 0m));
                }
                return true;
            }

            using (var scope = new TransactionScope())
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

                    scope.Complete();
                    return true;
                }
            }
        }

        // 13. Hàm nội bộ tính lại TotalAmount và FinalAmount của đơn hàng
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

        // 14. Hoàn tất thanh toán hóa đơn và giải phóng bàn về trạng thái "Trống" (bọc TransactionScope)
        public bool CompleteOrderPayment(int orderId, decimal totalAmount, decimal discountAmount, decimal finalAmount, int? promotionId, int? customerId = null)
        {
            if (!_tableRepo.IsDatabaseOnline())
            {
                var order = FallbackOrders.FirstOrDefault(o => o.Id == orderId);
                if (order == null)
                    return false;

                order.TotalAmount = totalAmount;
                order.DiscountAmount = discountAmount;
                order.FinalAmount = finalAmount;
                order.PromotionId = promotionId;
                if (customerId.HasValue && customerId.Value > 0)
                {
                    order.CustomerId = customerId.Value;
                }
                order.OrderDate = DateTime.Now;
                order.Status = "Đã thanh toán";

                var table = TableRepository.FallbackTables.FirstOrDefault(t => t.Id == order.TableId);
                if (table != null)
                {
                    if (table.Id > 1 && table.EndTime.HasValue && table.EndTime.Value > DateTime.Now)
                    {
                        table.Status = "Đã thanh toán";
                    }
                    else
                    {
                        table.Status = "Trống";
                    }
                }

                return true;
            }

            using (var scope = new TransactionScope())
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
                    if (customerId.HasValue && customerId.Value > 0)
                    {
                        order.CustomerId = customerId.Value;
                    }
                    order.OrderDate = DateTime.Now;
                    order.Status = "Đã thanh toán";

                    var table = db.Tables.FirstOrDefault(t => t.Id == order.TableId);
                    if (table != null)
                    {
                        if (table.Id > 1 && table.EndTime.HasValue && table.EndTime.Value > DateTime.Now)
                        {
                            table.Status = "Đã thanh toán";
                        }
                        else
                        {
                            table.Status = "Trống";
                        }
                    }

                    db.SubmitChanges();
                    scope.Complete();
                    return true;
                }
            }
        }

        // 15. Chuyển bàn hoặc Gộp bàn (bọc TransactionScope đảm bảo không mất món khi gộp bàn)
        public bool SwitchOrMergeTable(int sourceTableId, int targetTableId)
        {
            if (sourceTableId <= 0 || targetTableId <= 0 || sourceTableId == targetTableId)
                return false;

            if (!_tableRepo.IsDatabaseOnline())
            {
                var sourceTable = TableRepository.FallbackTables.FirstOrDefault(t => t.Id == sourceTableId);
                var targetTable = TableRepository.FallbackTables.FirstOrDefault(t => t.Id == targetTableId);
                if (sourceTable == null || targetTable == null)
                    return false;

                var sourceOrder = FallbackOrders.OrderByDescending(o => o.Id)
                    .FirstOrDefault(o => o.TableId == sourceTableId && o.Status == "Chưa thanh toán");
                if (sourceOrder == null)
                    return false;

                var targetOrder = FallbackOrders.OrderByDescending(o => o.Id)
                    .FirstOrDefault(o => o.TableId == targetTableId && o.Status == "Chưa thanh toán");

                if (targetOrder == null)
                {
                    sourceOrder.TableId = targetTableId;
                    sourceTable.Status = "Trống";
                    targetTable.Status = "Có khách";
                }
                else
                {
                    var srcDetails = FallbackOrderDetails.Where(d => d.OrderId == sourceOrder.Id).ToList();
                    foreach (var srcItem in srcDetails)
                    {
                        var targetItem = FallbackOrderDetails.FirstOrDefault(d =>
                            d.OrderId == targetOrder.Id && d.ProductId == srcItem.ProductId);
                        if (targetItem != null)
                        {
                            targetItem.Quantity += srcItem.Quantity;
                            targetItem.TotalPrice = targetItem.Quantity * targetItem.UnitPrice;
                            FallbackOrderDetails.Remove(srcItem);
                        }
                        else
                        {
                            srcItem.OrderId = targetOrder.Id;
                        }
                    }
                    FallbackOrders.Remove(sourceOrder);
                    sourceTable.Status = "Trống";
                    targetTable.Status = "Có khách";
                    targetOrder.TotalAmount = FallbackOrderDetails.Where(d => d.OrderId == targetOrder.Id).Sum(d => d.TotalPrice);
                    targetOrder.FinalAmount = Math.Max(0m, targetOrder.TotalAmount - (targetOrder.DiscountAmount ?? 0m));
                }
                return true;
            }

            using (var scope = new TransactionScope())
            {
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
                        sourceOrder.TableId = targetTableId;
                        sourceTable.Status = "Trống";
                        targetTable.Status = "Có khách";
                        db.SubmitChanges();
                    }
                    else
                    {
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

                        db.Orders.DeleteOnSubmit(sourceOrder);
                        sourceTable.Status = "Trống";
                        targetTable.Status = "Có khách";
                        db.SubmitChanges();

                        RecalculateOrderTotalInternal(db, targetOrder.Id);
                    }

                    scope.Complete();
                    return true;
                }
            }
        }
    }
}

using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    /// <summary>
    /// Lớp truy xuất dữ liệu (DAL) cho Bàn (Tables) và Khu vực (Areas).
    /// Hỗ trợ kết nối SQL Server qua LINQ to SQL và tự động nạp dữ liệu mẫu (Seed Data từ CoffeeShopDB.sql)
    /// khi máy trạm chưa khởi động dịch vụ SQL Server để phục vụ kiểm thử giao diện & nghiệp vụ.
    /// </summary>
    internal class TableRepository
    {
        private static bool? _isSqlAvailable;

        // Dữ liệu mẫu dự phòng (đồng bộ 100% với CoffeeShopDB.sql) khi chưa bật SQL Server
        private static readonly List<Area> _fallbackAreas = new List<Area>
        {
            new Area { Id = 1, AreaName = "Khu máy lạnh (Tầng 1)", Description = "Không gian yên tĩnh, làm việc" },
            new Area { Id = 2, AreaName = "Sân vườn", Description = "Không gian ngoài trời thoáng đãng" },
            new Area { Id = 3, AreaName = "Phòng VIP (Tầng 2)", Description = "Phòng họp nhóm riêng tư" }
        };

        internal static readonly List<Table> FallbackTables = new List<Table>
        {
            new Table { Id = 1, TableName = "Bàn 01", AreaId = 1, Status = "Trống" },
            new Table { Id = 2, TableName = "Bàn 02", AreaId = 1, Status = "Trống" },
            new Table { Id = 3, TableName = "Bàn 03", AreaId = 1, Status = "Trống" },
            new Table { Id = 4, TableName = "Bàn 04", AreaId = 2, Status = "Trống" },
            new Table { Id = 5, TableName = "Bàn 05", AreaId = 2, Status = "Trống" },
            new Table { Id = 6, TableName = "Bàn VIP 01", AreaId = 3, Status = "Trống" }
        };

        // 1. Khởi tạo DataContext kết nối cơ sở dữ liệu
        public CoffeeShopDataContextDataContext GetContext()
        {
            return new CoffeeShopDataContextDataContext(ConfigHelper.GetConnectionString("CoffeeShopDB"));
        }

        // Kiểm tra nhanh kết nối SQL Server (Timeout 2 giây để không làm treo giao diện)
        public bool IsDatabaseOnline()
        {
            if (_isSqlAvailable.HasValue)
                return _isSqlAvailable.Value;

            try
            {
                string rawConn = ConfigHelper.GetConnectionString("CoffeeShopDB");
                var builder = new SqlConnectionStringBuilder(rawConn)
                {
                    ConnectTimeout = 2
                };
                using (var conn = new SqlConnection(builder.ConnectionString))
                {
                    conn.Open();
                    _isSqlAvailable = true;
                }
            }
            catch
            {
                _isSqlAvailable = false;
            }

            return _isSqlAvailable.Value;
        }

        // 2. Lấy toàn bộ danh sách Khu vực (Areas)
        public List<Area> GetAllAreas()
        {
            if (!IsDatabaseOnline())
                return _fallbackAreas.OrderBy(a => a.Id).ToList();

            using (var db = GetContext())
            {
                return db.Areas.OrderBy(a => a.Id).ToList();
            }
        }

        // 3. Lấy danh sách Khu vực dưới dạng AreaDTO
        public List<AreaDTO> GetAllAreaDTOs()
        {
            return GetAllAreas()
                .Select(a => new AreaDTO
                {
                    Id = a.Id,
                    AreaName = a.AreaName,
                    Description = a.Description
                })
                .ToList();
        }

        // 4. Lấy thông tin Khu vực theo Id
        public Area GetAreaById(int areaId)
        {
            if (!IsDatabaseOnline())
                return _fallbackAreas.FirstOrDefault(a => a.Id == areaId);

            using (var db = GetContext())
            {
                return db.Areas.FirstOrDefault(a => a.Id == areaId);
            }
        }

        // 5. Kiểm tra Khu vực có tồn tại không
        public bool AreaExists(int areaId)
        {
            if (!IsDatabaseOnline())
                return _fallbackAreas.Any(a => a.Id == areaId);

            using (var db = GetContext())
            {
                return db.Areas.Any(a => a.Id == areaId);
            }
        }

        // 6. Lấy toàn bộ danh sách Bàn (Entity)
        public List<Table> GetAllTables()
        {
            if (!IsDatabaseOnline())
                return FallbackTables.OrderBy(t => t.Id).ToList();

            using (var db = GetContext())
            {
                return db.Tables.OrderBy(t => t.Id).ToList();
            }
        }

        // 7. Lấy danh sách Bàn kèm tên Khu vực để hiển thị lên giao diện
        public List<TableDTO> GetAllTablesWithArea()
        {
            if (!IsDatabaseOnline())
            {
                return (from table in FallbackTables
                        join area in _fallbackAreas on table.AreaId equals area.Id
                        orderby table.AreaId, table.Id
                        select new TableDTO
                        {
                            Id = table.Id,
                            TableName = table.TableName,
                            AreaId = table.AreaId,
                            AreaName = area.AreaName,
                            Status = table.Status
                        }).ToList();
            }

            using (var db = GetContext())
            {
                var query =
                    from table in db.Tables
                    join area in db.Areas
                        on table.AreaId equals area.Id
                    orderby table.AreaId, table.Id
                    select new TableDTO
                    {
                        Id = table.Id,
                        TableName = table.TableName,
                        AreaId = table.AreaId,
                        AreaName = area.AreaName,
                        Status = table.Status
                    };

                return query.ToList();
            }
        }

        // 8. Lọc danh sách Bàn theo Khu vực (areaId <= 0 sẽ lấy tất cả)
        public List<TableDTO> GetTablesByAreaId(int areaId)
        {
            if (areaId <= 0)
            {
                return GetAllTablesWithArea();
            }

            return GetAllTablesWithArea()
                .Where(t => t.AreaId == areaId)
                .OrderBy(t => t.Id)
                .ToList();
        }

        // 9. Lấy thông tin Bàn theo Id
        public Table GetTableById(int tableId)
        {
            if (!IsDatabaseOnline())
                return FallbackTables.FirstOrDefault(t => t.Id == tableId);

            using (var db = GetContext())
            {
                return db.Tables.FirstOrDefault(t => t.Id == tableId);
            }
        }

        // 10. Kiểm tra tên bàn đã tồn tại chưa (hỗ trợ loại trừ bàn hiện tại khi cập nhật)
        public bool IsTableNameDuplicate(string tableName, int excludeTableId = -1)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                return false;

            string normalized = tableName.Trim().ToLower();

            if (!IsDatabaseOnline())
            {
                return FallbackTables.Any(t =>
                    t.Id != excludeTableId &&
                    t.TableName.ToLower() == normalized);
            }

            using (var db = GetContext())
            {
                return db.Tables.Any(t =>
                    t.Id != excludeTableId &&
                    t.TableName.ToLower() == normalized);
            }
        }

        // 11. Thêm bàn mới vào cơ sở dữ liệu
        public bool InsertTable(Table table)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table));
            }

            if (!IsDatabaseOnline())
            {
                int nextId = FallbackTables.Count > 0 ? FallbackTables.Max(t => t.Id) + 1 : 1;
                table.Id = nextId;
                FallbackTables.Add(table);
                return true;
            }

            using (var db = GetContext())
            {
                db.Tables.InsertOnSubmit(table);
                db.SubmitChanges();
                return true;
            }
        }

        // 12. Cập nhật thông tin bàn
        public bool UpdateTable(Table table)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table));
            }

            if (!IsDatabaseOnline())
            {
                var existing = FallbackTables.FirstOrDefault(t => t.Id == table.Id);
                if (existing == null)
                    return false;

                existing.TableName = table.TableName;
                existing.AreaId = table.AreaId;
                existing.Status = table.Status;
                return true;
            }

            using (var db = GetContext())
            {
                var existing = db.Tables.FirstOrDefault(t => t.Id == table.Id);
                if (existing != null)
                {
                    existing.TableName = table.TableName;
                    existing.AreaId = table.AreaId;
                    existing.Status = table.Status;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        // 13. Cập nhật trạng thái bàn ("Trống", "Có khách", "Đặt trước")
        public bool UpdateTableStatus(int tableId, string status)
        {
            if (!IsDatabaseOnline())
            {
                var existing = FallbackTables.FirstOrDefault(t => t.Id == tableId);
                if (existing == null)
                    return false;

                existing.Status = status;
                return true;
            }

            using (var db = GetContext())
            {
                var existing = db.Tables.FirstOrDefault(t => t.Id == tableId);
                if (existing == null)
                    return false;

                existing.Status = status;
                db.SubmitChanges();
                return true;
            }
        }

        // 14. Kiểm tra bàn có đang có hóa đơn chưa thanh toán hay không
        public bool HasUnpaidOrders(int tableId)
        {
            if (!IsDatabaseOnline())
            {
                return OrderRepository.FallbackOrders.Any(o =>
                    o.TableId == tableId &&
                    o.Status == "Chưa thanh toán");
            }

            using (var db = GetContext())
            {
                return db.Orders.Any(o =>
                    o.TableId == tableId &&
                    o.Status == "Chưa thanh toán");
            }
        }

        // 15. Kiểm tra bàn đã có lịch sử hóa đơn liên kết trong bảng Orders chưa
        public bool HasAnyOrders(int tableId)
        {
            if (!IsDatabaseOnline())
            {
                return OrderRepository.FallbackOrders.Any(o => o.TableId == tableId);
            }

            using (var db = GetContext())
            {
                return db.Orders.Any(o => o.TableId == tableId);
            }
        }

        // 16. Xóa bàn theo Id
        public bool DeleteTable(int tableId)
        {
            if (!IsDatabaseOnline())
            {
                var existing = FallbackTables.FirstOrDefault(t => t.Id == tableId);
                if (existing == null)
                    return false;

                FallbackTables.Remove(existing);
                return true;
            }

            using (var db = GetContext())
            {
                var table = db.Tables.FirstOrDefault(t => t.Id == tableId);
                if (table != null)
                {
                    db.Tables.DeleteOnSubmit(table);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }
    }
}

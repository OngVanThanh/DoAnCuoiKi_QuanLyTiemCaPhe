using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    /// <summary>
    /// Lớp truy xuất dữ liệu (DAL) cho Bàn (Tables) và Khu vực (Areas).
    /// </summary>
    internal class TableRepository
    {
        // 1. Khởi tạo DataContext kết nối cơ sở dữ liệu
        public CoffeeShopDataContextDataContext GetContext()
        {
            return new CoffeeShopDataContextDataContext(ConfigHelper.GetConnectionString("CoffeeShopDB"));
        }

        // 2. Lấy toàn bộ danh sách Khu vực (Areas)
        public List<Area> GetAllAreas()
        {
            using (var db = GetContext())
            {
                return db.Areas.OrderBy(a => a.Id).ToList();
            }
        }

        // 3. Lấy danh sách Khu vực dưới dạng AreaDTO
        public List<AreaDTO> GetAllAreaDTOs()
        {
            using (var db = GetContext())
            {
                return db.Areas
                    .OrderBy(a => a.Id)
                    .Select(a => new AreaDTO
                    {
                        Id = a.Id,
                        AreaName = a.AreaName,
                        Description = a.Description
                    })
                    .ToList();
            }
        }

        // 4. Lấy thông tin Khu vực theo Id
        public Area GetAreaById(int areaId)
        {
            using (var db = GetContext())
            {
                return db.Areas.FirstOrDefault(a => a.Id == areaId);
            }
        }

        // 5. Kiểm tra Khu vực có tồn tại không
        public bool AreaExists(int areaId)
        {
            using (var db = GetContext())
            {
                return db.Areas.Any(a => a.Id == areaId);
            }
        }

        // 6. Lấy toàn bộ danh sách Bàn (Entity)
        public List<Table> GetAllTables()
        {
            using (var db = GetContext())
            {
                return db.Tables.OrderBy(t => t.Id).ToList();
            }
        }

        // 7. Lấy danh sách Bàn kèm tên Khu vực để hiển thị lên giao diện
        public List<TableDTO> GetAllTablesWithArea()
        {
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

            using (var db = GetContext())
            {
                var query =
                    from table in db.Tables
                    join area in db.Areas
                        on table.AreaId equals area.Id
                    where table.AreaId == areaId
                    orderby table.Id
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

        // 9. Lấy thông tin Bàn theo Id
        public Table GetTableById(int tableId)
        {
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
            using (var db = GetContext())
            {
                return db.Orders.Any(o => o.TableId == tableId);
            }
        }

        // 16. Xóa bàn theo Id
        public bool DeleteTable(int tableId)
        {
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

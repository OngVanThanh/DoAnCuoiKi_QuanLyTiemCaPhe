using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    internal class TableRepository
    {
        private static bool? _isSqlAvailable;

        private static readonly List<Area> _fallbackAreas = new List<Area>
        {
            new Area { Id = 1, AreaName = "Khu máy lạnh (Tầng 1)", Description = "Không gian yên tĩnh, làm việc" },
            new Area { Id = 2, AreaName = "Sân vườn", Description = "Không gian ngoài trời thoáng đãng" },
            new Area { Id = 3, AreaName = "Phòng VIP (Tầng 2)", Description = "Phòng họp nhóm riêng tư" }
        };

        internal static readonly List<Table> FallbackTables = new List<Table>
        {
            new Table { Id = 1, TableName = "Khách tại quầy / Mang đi", AreaId = 1, Status = "Trống" }
        };

        public CoffeeShopDataContextDataContext GetContext()
        {
            return new CoffeeShopDataContextDataContext(ConfigHelper.GetConnectionString("CoffeeShopDB"));
        }

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

        public List<Area> GetAllAreas()
        {
            if (!IsDatabaseOnline())
                return _fallbackAreas.OrderBy(a => a.Id).ToList();

            try
            {
                using (var db = GetContext())
                {
                    return db.Areas.OrderBy(a => a.Id).ToList();
                }
            }
            catch
            {
                return _fallbackAreas.OrderBy(a => a.Id).ToList();
            }
        }

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

        // Tự động tìm hoặc tạo mới Khu vực theo tên người dùng nhập tự do trên TextBox
        public int GetOrCreateAreaIdByName(string areaName)
        {
            string cleanName = string.IsNullOrWhiteSpace(areaName) ? "Khu vực chung" : areaName.Trim();

            if (!IsDatabaseOnline())
            {
                var existingFallback = _fallbackAreas.FirstOrDefault(a =>
                    string.Equals(a.AreaName, cleanName, StringComparison.OrdinalIgnoreCase));
                if (existingFallback != null)
                    return existingFallback.Id;

                int nextId = _fallbackAreas.Count > 0 ? _fallbackAreas.Max(a => a.Id) + 1 : 1;
                _fallbackAreas.Add(new Area { Id = nextId, AreaName = cleanName, Description = "Khu vực đặt bàn" });
                return nextId;
            }

            try
            {
                using (var db = GetContext())
                {
                    var existing = db.Areas.FirstOrDefault(a => a.AreaName.ToLower() == cleanName.ToLower());
                    if (existing != null)
                        return existing.Id;

                    var newArea = new Area
                    {
                        AreaName = cleanName,
                        Description = "Khu vực tạo từ màn hình đặt bàn"
                    };
                    db.Areas.InsertOnSubmit(newArea);
                    db.SubmitChanges();
                    return newArea.Id;
                }
            }
            catch
            {
                var existingFallback = _fallbackAreas.FirstOrDefault(a =>
                    string.Equals(a.AreaName, cleanName, StringComparison.OrdinalIgnoreCase));
                if (existingFallback != null)
                    return existingFallback.Id;

                int nextId = _fallbackAreas.Count > 0 ? _fallbackAreas.Max(a => a.Id) + 1 : 1;
                _fallbackAreas.Add(new Area { Id = nextId, AreaName = cleanName, Description = "Khu vực đặt bàn" });
                return nextId;
            }
        }

        public Area GetAreaById(int areaId)
        {
            if (!IsDatabaseOnline())
                return _fallbackAreas.FirstOrDefault(a => a.Id == areaId);

            try
            {
                using (var db = GetContext())
                {
                    return db.Areas.FirstOrDefault(a => a.Id == areaId);
                }
            }
            catch
            {
                return _fallbackAreas.FirstOrDefault(a => a.Id == areaId);
            }
        }

        public bool AreaExists(int areaId)
        {
            if (!IsDatabaseOnline())
                return _fallbackAreas.Any(a => a.Id == areaId);

            try
            {
                using (var db = GetContext())
                {
                    return db.Areas.Any(a => a.Id == areaId);
                }
            }
            catch
            {
                return _fallbackAreas.Any(a => a.Id == areaId);
            }
        }

        public bool DeleteAreaByName(string areaName, out string message)
        {
            if (string.IsNullOrWhiteSpace(areaName))
            {
                message = "Tên khu vực không hợp lệ!";
                return false;
            }

            string cleanName = areaName.Trim();

            if (!IsDatabaseOnline())
            {
                var area = _fallbackAreas.FirstOrDefault(a => string.Equals(a.AreaName, cleanName, StringComparison.OrdinalIgnoreCase));
                if (area == null)
                {
                    message = "Khu vực không tồn tại trong hệ thống!";
                    return false;
                }

                bool hasTables = FallbackTables.Any(t => t.AreaId == area.Id);
                if (hasTables)
                {
                    message = $"Khu vực '{cleanName}' đang có bàn hoạt động, không thể xóa!";
                    return false;
                }

                _fallbackAreas.Remove(area);
                message = $"Đã xóa khu vực '{cleanName}' khỏi danh sách gợi ý!";
                return true;
            }

            using (var db = GetContext())
            {
                var area = db.Areas.FirstOrDefault(a => a.AreaName == cleanName);
                if (area == null)
                {
                    message = "Khu vực không tồn tại trong hệ thống!";
                    return false;
                }

                // Kiểm tra xem khu vực này có bàn đang hoạt động không
                bool hasTables = db.Tables.Any(t => t.AreaId == area.Id);
                if (hasTables)
                {
                    message = $"Khu vực '{cleanName}' đang có bàn hoạt động, không thể xóa!";
                    return false;
                }

                db.Areas.DeleteOnSubmit(area);
                db.SubmitChanges();
                message = $"Đã xóa khu vực '{cleanName}' khỏi danh sách gợi ý!";
                return true;
            }
        }

        public List<Table> GetAllTables()
        {
            if (!IsDatabaseOnline())
                return FallbackTables.OrderBy(t => t.Id).ToList();

            using (var db = GetContext())
            {
                return db.Tables.OrderBy(t => t.Id).ToList();
            }
        }

        public List<TableDTO> GetAllTablesWithArea()
        {
            if (!IsDatabaseOnline())
            {
                return FallbackTables
                    .Join(_fallbackAreas,
                          table => table.AreaId,
                          area => area.Id,
                          (table, area) => new TableDTO
                          {
                              Id = table.Id,
                              TableName = table.TableName,
                              AreaId = table.AreaId,
                              AreaName = area.AreaName,
                              Status = table.Status,
                              StartTime = table.StartTime,
                              EndTime = table.EndTime,
                              CustomerId = table.CustomerId,
                              CustomerName = !string.IsNullOrEmpty(table.CustomerId) ? "Khách mẫu" : null,
                              CustomerPhone = table.CustomerId
                          })
                    .OrderBy(t => t.Id)
                    .ToList();
            }

            try
            {
                using (var db = GetContext())
                {
                    var tables = db.Tables
                        .OrderBy(t => t.Id)
                        .Select(t => new
                        {
                            t.Id,
                            t.TableName,
                            t.AreaId,
                            AreaName = t.Area != null ? t.Area.AreaName : string.Empty,
                            t.Status,
                            t.StartTime,
                            t.EndTime,
                            t.CustomerId
                        })
                        .ToList();

                    var customerList = db.Customers.ToList();

                    return tables.Select(t =>
                    {
                        var cust = !string.IsNullOrEmpty(t.CustomerId)
                            ? customerList.FirstOrDefault(c => c.Phone == t.CustomerId || c.Id.ToString() == t.CustomerId)
                            : null;

                        return new TableDTO
                        {
                            Id = t.Id,
                            TableName = t.TableName,
                            AreaId = t.AreaId,
                            AreaName = t.AreaName,
                            Status = t.Status,
                            StartTime = t.StartTime,
                            EndTime = t.EndTime,
                            CustomerId = t.CustomerId,
                            CustomerName = cust != null ? cust.CustomerName : (!string.IsNullOrEmpty(t.CustomerId) ? t.CustomerId : null),
                            CustomerPhone = cust?.Phone
                        };
                    }).ToList();
                }
            }
            catch
            {
                return FallbackTables
                    .Join(_fallbackAreas,
                          table => table.AreaId,
                          area => area.Id,
                          (table, area) => new TableDTO
                          {
                              Id = table.Id,
                              TableName = table.TableName,
                              AreaId = table.AreaId,
                              AreaName = area.AreaName,
                              Status = table.Status,
                              StartTime = table.StartTime,
                              EndTime = table.EndTime,
                              CustomerId = table.CustomerId,
                              CustomerName = !string.IsNullOrEmpty(table.CustomerId) ? "Khách mẫu" : null,
                              CustomerPhone = table.CustomerId
                          })
                    .OrderBy(t => t.Id)
                    .ToList();
            }
        }

        public List<TableDTO> GetTablesByAreaId(int areaId)
        {
            if (areaId <= 0)
                return GetAllTablesWithArea();

            return GetAllTablesWithArea()
                .Where(t => t.AreaId == areaId)
                .OrderBy(t => t.Id)
                .ToList();
        }

        public Table GetTableById(int tableId)
        {
            if (!IsDatabaseOnline())
                return FallbackTables.FirstOrDefault(t => t.Id == tableId);

            using (var db = GetContext())
            {
                return db.Tables.FirstOrDefault(t => t.Id == tableId);
            }
        }

        public TableDTO GetOrCreateCounterTable()
        {
            if (!IsDatabaseOnline())
            {
                return new TableDTO
                {
                    Id = 1,
                    TableName = "Khách tại quầy / Mang đi",
                    AreaId = 1,
                    AreaName = "Khu máy lạnh (Tầng 1)",
                    Status = "Trống"
                };
            }

            using (var db = GetContext())
            {
                var counter = db.Tables.FirstOrDefault(t =>
                    t.TableName == "Khách tại quầy / Mang đi" ||
                    t.TableName == "Khách tại quầy" ||
                    t.TableName == "Mang đi");

                if (counter == null)
                {
                    int defaultAreaId = db.Areas.Select(a => a.Id).FirstOrDefault();
                    if (defaultAreaId <= 0)
                    {
                        var newArea = new Area { AreaName = "Khu phục vụ", Description = "Khu vực quầy thu ngân" };
                        db.Areas.InsertOnSubmit(newArea);
                        db.SubmitChanges();
                        defaultAreaId = newArea.Id;
                    }

                    counter = new Table
                    {
                        TableName = "Khách tại quầy / Mang đi",
                        AreaId = defaultAreaId,
                        Status = "Trống"
                    };
                    db.Tables.InsertOnSubmit(counter);
                    db.SubmitChanges();
                }

                string areaName = db.Areas.Where(a => a.Id == counter.AreaId).Select(a => a.AreaName).FirstOrDefault() ?? "";
                return new TableDTO
                {
                    Id = counter.Id,
                    TableName = counter.TableName,
                    AreaId = counter.AreaId,
                    AreaName = areaName,
                    Status = counter.Status
                };
            }
        }

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

        public bool InsertTable(Table table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

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

        public bool UpdateTable(Table table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            if (!IsDatabaseOnline())
            {
                var existing = FallbackTables.FirstOrDefault(t => t.Id == table.Id);
                if (existing == null)
                    return false;

                existing.TableName = table.TableName;
                existing.AreaId = table.AreaId;
                existing.Status = table.Status;
                existing.StartTime = table.StartTime;
                existing.EndTime = table.EndTime;
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
                    existing.StartTime = table.StartTime;
                    existing.EndTime = table.EndTime;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

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

        public bool DeleteTable(int tableId)
        {
            if (tableId <= 1) return false;

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
                    if (db.Orders.Any(o => o.TableId == tableId))
                    {
                        table.Status = "Trống";
                        table.StartTime = null;
                        table.EndTime = null;
                    }
                    else
                    {
                        db.Tables.DeleteOnSubmit(table);
                    }
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        public int DeleteExpiredReservations()
        {
            DateTime now = DateTime.Now;
            int removedCount = 0;

            if (!IsDatabaseOnline())
            {
                var expired = FallbackTables
                    .Where(t => t.Id > 1 && t.EndTime.HasValue && t.EndTime.Value < now && !HasUnpaidOrders(t.Id))
                    .ToList();

                foreach (var t in expired)
                {
                    FallbackTables.Remove(t);
                    removedCount++;
                }
                return removedCount;
            }

            using (var db = GetContext())
            {
                var expiredTables = db.Tables
                    .Where(t => t.Id > 1 && t.EndTime != null && t.EndTime < now)
                    .ToList();

                foreach (var table in expiredTables)
                {
                    bool hasUnpaid = db.Orders.Any(o => o.TableId == table.Id && o.Status == "Chưa thanh toán");
                    if (hasUnpaid)
                        continue;

                    bool hasHistory = db.Orders.Any(o => o.TableId == table.Id);
                    if (hasHistory)
                    {
                        table.Status = "Trống";
                        table.StartTime = null;
                        table.EndTime = null;
                    }
                    else
                    {
                        db.Tables.DeleteOnSubmit(table);
                    }
                    removedCount++;
                }

                if (removedCount > 0)
                {
                    db.SubmitChanges();
                }
            }

            return removedCount;
        }
    }
}
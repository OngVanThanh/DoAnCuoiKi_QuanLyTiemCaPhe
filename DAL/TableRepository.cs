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
            new Table { Id = 1, TableName = "Khách tại quầy / Mang đi", AreaId = 1, Status = "Trống" },
            new Table { Id = 2, TableName = "Bàn 01 - Khu máy lạnh", AreaId = 1, Status = "Đặt trước", StartTime = DateTime.Now.AddMinutes(-15), EndTime = DateTime.Now.AddHours(2) },
            new Table { Id = 3, TableName = "Bàn VIP 01", AreaId = 3, Status = "Đặt trước", StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(3) }
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

            using (var db = GetContext())
            {
                return db.Areas.OrderBy(a => a.Id).ToList();
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

        public Area GetAreaById(int areaId)
        {
            if (!IsDatabaseOnline())
                return _fallbackAreas.FirstOrDefault(a => a.Id == areaId);

            using (var db = GetContext())
            {
                return db.Areas.FirstOrDefault(a => a.Id == areaId);
            }
        }

        public bool AreaExists(int areaId)
        {
            if (!IsDatabaseOnline())
                return _fallbackAreas.Any(a => a.Id == areaId);

            using (var db = GetContext())
            {
                return db.Areas.Any(a => a.Id == areaId);
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
                return (from table in FallbackTables
                        join area in _fallbackAreas on table.AreaId equals area.Id
                        orderby table.Id
                        select new TableDTO
                        {
                            Id = table.Id,
                            TableName = table.TableName,
                            AreaId = table.AreaId,
                            AreaName = area.AreaName,
                            Status = table.Status,
                            StartTime = table.StartTime,
                            EndTime = table.EndTime
                        }).ToList();
            }

            using (var db = GetContext())
            {
                var query =
                    from table in db.Tables
                    join area in db.Areas on table.AreaId equals area.Id
                    orderby table.Id
                    select new TableDTO
                    {
                        Id = table.Id,
                        TableName = table.TableName,
                        AreaId = table.AreaId,
                        AreaName = area.AreaName,
                        Status = table.Status,
                        StartTime = table.StartTime,
                        EndTime = table.EndTime
                    };

                return query.ToList();
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
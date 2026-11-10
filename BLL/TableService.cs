using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.BLL
{
    internal class TableService
    {
        private readonly TableRepository _tableRepository = new TableRepository();

        public List<Area> GetAllAreas()
        {
            return _tableRepository.GetAllAreas();
        }

        public List<AreaDTO> GetAllAreaDTOs()
        {
            return _tableRepository.GetAllAreaDTOs();
        }

        public List<TableDTO> GetAllTables()
        {
            return _tableRepository.GetAllTablesWithArea();
        }

        public TableDTO GetOrCreateCounterTable()
        {
            return _tableRepository.GetOrCreateCounterTable();
        }

        public List<TableDTO> GetAllTablesWithStatus()
        {
            var counter = GetOrCreateCounterTable();
            var tables = _tableRepository.GetAllTablesWithArea()
                .Where(t => t.Id != counter.Id &&
                            !t.TableName.Contains("quầy") &&
                            !t.TableName.Contains("Mang đi"))
                .OrderBy(t => t.AreaId)
                .ThenBy(t => t.Id)
                .ToList();

            foreach (var table in tables)
            {
                if (string.IsNullOrWhiteSpace(table.Status))
                {
                    table.Status = "Trống";
                }
            }

            return tables;
        }

        public List<TableDTO> GetActiveReservedOrOccupiedTables()
        {
            var counter = GetOrCreateCounterTable();
            DateTime now = DateTime.Now;
            return _tableRepository.GetAllTablesWithArea()
                .Where(t => t.Id != counter.Id &&
                            !t.TableName.Contains("quầy") &&
                            !t.TableName.Contains("Mang đi") &&
                            (string.Equals(t.Status, "Đặt trước", StringComparison.OrdinalIgnoreCase) ||
                             string.Equals(t.Status, "Có khách", StringComparison.OrdinalIgnoreCase) ||
                             string.Equals(t.Status, "Đã thanh toán", StringComparison.OrdinalIgnoreCase)) &&
                            (!t.EndTime.HasValue || t.EndTime.Value > now || string.Equals(t.Status, "Có khách", StringComparison.OrdinalIgnoreCase)))
                .OrderBy(t => t.StartTime ?? DateTime.MaxValue)
                .ThenBy(t => t.Id)
                .ToList();
        }

        public List<TableDTO> GetTablesByArea(int areaId)
        {
            return _tableRepository.GetTablesByAreaId(areaId);
        }

        public Table GetTableById(int tableId)
        {
            if (tableId <= 0)
                return null;

            return _tableRepository.GetTableById(tableId);
        }

        public List<string> GetValidTableStatuses()
        {
            return new List<string> { "Trống", "Có khách", "Đặt trước" };
        }

        // Thêm bàn đặt trước 
        public bool AddReservedTable(string tableName, string areaName, DateTime startTime, DateTime endTime, out string message)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                message = "Vui lòng nhập tên khách hàng đặt bàn!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(areaName))
            {
                message = "Vui lòng chọn hoặc nhập tên khu vực (VD: Khu máy lạnh, Sân vườn, Tầng 1...)!";
                return false;
            }

            if (endTime <= startTime)
            {
                message = "Thời gian kết thúc phải lớn hơn thời gian bắt đầu!";
                return false;
            }

            if (endTime <= DateTime.Now)
            {
                message = "Thời gian kết thúc phải lớn hơn thời điểm hiện tại!";
                return false;
            }

            int areaId = _tableRepository.GetOrCreateAreaIdByName(areaName.Trim());

            // --- NGHIỆP VỤ KIỂM TRA TRÙNG LỊCH BÀN THEO KHU VỰC ---
            // Kiểm tra trong cùng khu vực (areaId) xem có lịch đặt nào giao nhau trong khung giờ [startTime, endTime] không:
            // Điều kiện giao nhau giữa 2 khoảng thời gian [S1, E1] và [S2, E2]: S_moi < E_cu VÀ E_moi > S_cu
            var conflictingTable = this.GetActiveReservedOrOccupiedTables()
                .FirstOrDefault(t => t.AreaId == areaId &&
                                     t.StartTime.HasValue && t.EndTime.HasValue &&
                                     startTime < t.EndTime.Value && endTime > t.StartTime.Value);

            if (conflictingTable != null)
            {
                string bookedCust = !string.IsNullOrWhiteSpace(conflictingTable.TableName) ? conflictingTable.TableName.Trim() : "khách khác";
                if (bookedCust.StartsWith("KH: ", StringComparison.OrdinalIgnoreCase))
                {
                    bookedCust = bookedCust.Substring(4).Trim();
                }

                message = $"Khu vực '{areaName.Trim()}' đã có lịch đặt trước của khách '{bookedCust}' " +
                          $"trong khung giờ {conflictingTable.StartTime.Value:HH:mm dd/MM} - {conflictingTable.EndTime.Value:HH:mm dd/MM}!\n" +
                          $"Vui lòng chọn khu vực khác hoặc điều chỉnh lại khung giờ đặt để tránh trùng khu vực.";
                return false;
            }

            var newTable = new Table
            {
                TableName = tableName.Trim(),
                AreaId = areaId,
                Status = "Đặt trước",
                StartTime = startTime,
                EndTime = endTime,
                CustomerId = tableName.Trim()
            };

            bool result = _tableRepository.InsertTable(newTable);
            message = result ? "Đã tạo bàn đặt trước thành công!" : "Không thể tạo bàn đặt trước.";
            return result;
        }

        public int AutoCleanupExpiredReservations()
        {
            try
            {
                return _tableRepository.DeleteExpiredReservations();
            }
            catch
            {
                return 0;
            }
        }

        public bool DeleteTable(int tableId, out string message)
        {
            if (tableId <= 1)
            {
                message = "Không thể xóa chế độ Khách tại quầy mặc định!";
                return false;
            }

            if (_tableRepository.HasUnpaidOrders(tableId))
            {
                message = "Bàn này đang có hóa đơn chưa thanh toán, vui lòng thanh toán hoặc xóa món trước khi hủy bàn!";
                return false;
            }

            bool result = _tableRepository.DeleteTable(tableId);
            message = result ? "Đã hủy/xóa bàn đặt trước!" : "Không tìm thấy bàn cần xóa.";
            return result;
        }
        public bool DeleteAreaByName(string areaName, out string message)
        {
            if (string.IsNullOrWhiteSpace(areaName))
            {
                message = "Tên khu vực không hợp lệ!";
                return false;
            }

            return _tableRepository.DeleteAreaByName(areaName, out message);
        }
    }
}
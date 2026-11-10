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

        public List<TableDTO> GetActiveReservedOrOccupiedTables()
        {
            return _tableRepository.GetAllTablesWithArea()
                .Where(t => t.Id > 1 &&
                           (string.Equals(t.Status, "Đặt trước", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(t.Status, "Có khách", StringComparison.OrdinalIgnoreCase) ||
                            t.StartTime.HasValue))
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

        // Thêm bàn đặt trước với Khu vực nhập tự do bằng TextBox (areaName)
        public bool AddReservedTable(string tableName, string areaName, DateTime startTime, DateTime endTime, out string message)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                message = "Vui lòng nhập tên bàn hoặc tên khách đặt (VD: Bàn 02 - Chị Lan)!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(areaName))
            {
                message = "Vui lòng nhập tên khu vực (VD: Tầng 1, Ban công, Sân vườn...)!";
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

            var newTable = new Table
            {
                TableName = tableName.Trim(),
                AreaId = areaId,
                Status = "Đặt trước",
                StartTime = startTime,
                EndTime = endTime
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
    }
}
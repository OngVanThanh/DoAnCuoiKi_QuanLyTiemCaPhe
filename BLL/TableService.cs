using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Collections.Generic;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.BLL
{
    /// <summary>
    /// Lớp xử lý nghiệp vụ (BLL) cho Quản lý Bàn và Khu vực.
    /// Thực hiện kiểm tra ràng buộc đầu vào trước khi gọi xuống tầng DAL.
    /// </summary>
    internal class TableService
    {
        private readonly TableRepository _tableRepository = new TableRepository();

        // 1. Lấy danh sách toàn bộ khu vực
        public List<Area> GetAllAreas()
        {
            return _tableRepository.GetAllAreas();
        }

        // 2. Lấy danh sách khu vực dạng DTO
        public List<AreaDTO> GetAllAreaDTOs()
        {
            return _tableRepository.GetAllAreaDTOs();
        }

        // 3. Lấy toàn bộ danh sách bàn kèm tên khu vực
        public List<TableDTO> GetAllTables()
        {
            return _tableRepository.GetAllTablesWithArea();
        }

        // 4. Lọc danh sách bàn theo khu vực (areaId <= 0 trả về tất cả bàn)
        public List<TableDTO> GetTablesByArea(int areaId)
        {
            return _tableRepository.GetTablesByAreaId(areaId);
        }

        // 5. Lấy thông tin bàn theo Id
        public Table GetTableById(int tableId)
        {
            if (tableId <= 0)
                return null;

            return _tableRepository.GetTableById(tableId);
        }

        // 6. Danh sách trạng thái bàn hợp lệ trong hệ thống
        public List<string> GetValidTableStatuses()
        {
            return new List<string>
            {
                "Trống",
                "Có khách",
                "Đặt trước"
            };
        }

        // 7. Thêm bàn mới (có kiểm tra ràng buộc dữ liệu đầu vào)
        public bool AddTable(string tableName, int areaId, string status, out string message)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                message = "Tên bàn không được để trống!";
                return false;
            }

            tableName = tableName.Trim();

            if (areaId <= 0 || !_tableRepository.AreaExists(areaId))
            {
                message = "Vui lòng chọn khu vực hợp lệ cho bàn!";
                return false;
            }

            if (_tableRepository.IsTableNameDuplicate(tableName))
            {
                message = $"Tên bàn '{tableName}' đã tồn tại trong hệ thống!";
                return false;
            }

            string normalizedStatus = string.IsNullOrWhiteSpace(status) ? "Trống" : status.Trim();

            var newTable = new Table
            {
                TableName = tableName,
                AreaId = areaId,
                Status = normalizedStatus
            };

            bool result = _tableRepository.InsertTable(newTable);
            message = result ? "Thêm bàn mới thành công!" : "Không thể thêm bàn mới.";
            return result;
        }

        // 8. Cập nhật thông tin bàn
        public bool UpdateTable(int tableId, string tableName, int areaId, string status, out string message)
        {
            if (tableId <= 0)
            {
                message = "Vui lòng chọn bàn cần cập nhật!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(tableName))
            {
                message = "Tên bàn không được để trống!";
                return false;
            }

            tableName = tableName.Trim();

            if (areaId <= 0 || !_tableRepository.AreaExists(areaId))
            {
                message = "Vui lòng chọn khu vực hợp lệ!";
                return false;
            }

            if (_tableRepository.IsTableNameDuplicate(tableName, tableId))
            {
                message = $"Tên bàn '{tableName}' đã trùng với một bàn khác!";
                return false;
            }

            string normalizedStatus = string.IsNullOrWhiteSpace(status) ? "Trống" : status.Trim();

            // Không cho phép đổi trạng thái sang "Trống" nếu bàn đang có hóa đơn chưa thanh toán
            if (normalizedStatus == "Trống" && _tableRepository.HasUnpaidOrders(tableId))
            {
                message = "Bàn hiện đang có hóa đơn chưa thanh toán, không thể chuyển trạng thái về 'Trống'!";
                return false;
            }

            var tableToUpdate = new Table
            {
                Id = tableId,
                TableName = tableName,
                AreaId = areaId,
                Status = normalizedStatus
            };

            bool result = _tableRepository.UpdateTable(tableToUpdate);
            message = result ? "Cập nhật thông tin bàn thành công!" : "Không tìm thấy bàn cần cập nhật.";
            return result;
        }

        // 9. Xóa bàn (kiểm tra ràng buộc khóa ngoại với hóa đơn trước khi xóa)
        public bool DeleteTable(int tableId, out string message)
        {
            if (tableId <= 0)
            {
                message = "Vui lòng chọn bàn cần xóa!";
                return false;
            }

            if (_tableRepository.HasUnpaidOrders(tableId))
            {
                message = "Không thể xóa bàn đang phục vụ khách (có hóa đơn chưa thanh toán)!";
                return false;
            }

            if (_tableRepository.HasAnyOrders(tableId))
            {
                message = "Không thể xóa bàn đã phát sinh lịch sử hóa đơn để đảm bảo toàn vẹn dữ liệu thống kê!";
                return false;
            }

            bool result = _tableRepository.DeleteTable(tableId);
            message = result ? "Xóa bàn thành công!" : "Không tìm thấy bàn hoặc không thể xóa.";
            return result;
        }
    }
}

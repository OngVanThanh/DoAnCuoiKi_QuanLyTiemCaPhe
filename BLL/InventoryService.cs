using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System.Collections.Generic;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.BLL
{
    public class InventoryService
    {
        private readonly InventoryRepository _inventoryRepo = new InventoryRepository();

        public List<InventoryDTO> GetAllMaterials()
        {
            return _inventoryRepo.GetAllMaterials();
        }

        public List<InventoryTransactionDTO> GetTransactionHistory()
        {
            return _inventoryRepo.GetTransactionHistory();
        }

        // Quản lý danh mục nguyên vật liệu
        public bool SaveMaterial(InventoryDTO dto, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (dto == null)
            {
                errorMsg = "Dữ liệu nguyên liệu không hợp lệ!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.MaterialName))
            {
                errorMsg = "Tên nguyên liệu không được để trống!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.Unit))
            {
                errorMsg = "Đơn vị tính không được để trống!";
                return false;
            }

            if (dto.MinThreshold < 0)
            {
                errorMsg = "Định mức tối thiểu không được âm!";
                return false;
            }

            var material = new Material
            {
                Id = dto.Id,
                MaterialName = dto.MaterialName.Trim(),
                Unit = dto.Unit.Trim(),
                QuantityInStock = dto.QuantityInStock,
                MinThreshold = dto.MinThreshold
            };

            if (dto.Id == 0)
            {
                return _inventoryRepo.InsertMaterial(material);
            }
            else
            {
                return _inventoryRepo.UpdateMaterial(material);
            }
        }

        public bool DeleteMaterial(int materialId, out string errorMsg)
        {
            errorMsg = string.Empty;
            return _inventoryRepo.DeleteMaterial(materialId);
        }

        // NGHIỆP VỤ NHẬP KHO: Kiểm tra số lượng nhập > 0
        public bool ImportMaterial(int materialId, double quantity, int employeeId, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (materialId <= 0)
            {
                errorMsg = "Vui lòng chọn nguyên liệu cần nhập kho!";
                return false;
            }

            if (quantity <= 0)
            {
                errorMsg = "Số lượng nhập kho phải lớn hơn 0!";
                return false;
            }

            bool success = _inventoryRepo.ImportStock(materialId, quantity, employeeId);
            if (!success)
            {
                errorMsg = "Không thể thực hiện nhập kho, vui lòng thử lại!";
            }
            return success;
        }

        // NGHIỆP VỤ XUẤT KHO: Kiểm tra chống âm kho & cảnh báo định mức
        public bool ExportMaterial(int materialId, double quantity, int employeeId, out string errorMsg, out bool isLowStock)
        {
            errorMsg = string.Empty;
            isLowStock = false;

            if (materialId <= 0)
            {
                errorMsg = "Vui lòng chọn nguyên liệu cần xuất kho!";
                return false;
            }

            if (quantity <= 0)
            {
                errorMsg = "Số lượng xuất kho phải lớn hơn 0!";
                return false;
            }

            bool success = _inventoryRepo.ExportStock(materialId, quantity, employeeId);
            if (!success)
            {
                errorMsg = "Số lượng trong kho không đủ để xuất (Chống âm kho)!";
                return false;
            }

            // Kiểm tra xem sau khi xuất thì nguyên liệu có bị dưới định mức tối thiểu không
            var materials = _inventoryRepo.GetAllMaterials();
            var mat = materials.Find(m => m.Id == materialId);
            if (mat != null && mat.IsLowStock)
            {
                isLowStock = true;
            }

            return true;
        }
    }
}
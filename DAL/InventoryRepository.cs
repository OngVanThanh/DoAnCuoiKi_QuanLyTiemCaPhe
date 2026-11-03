using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    public class InventoryRepository
    {
        private CoffeeShopDataContextDataContext GetContext()
        {
            return new CoffeeShopDataContextDataContext(ConfigHelper.GetConnectionString("CoffeeShopDB"));
        }

        // 1. Lấy toàn bộ nguyên vật liệu kèm trạng thái định mức
        public List<InventoryDTO> GetAllMaterials()
        {
            using (var db = GetContext())
            {
                return db.Materials
                    .Select(m => new InventoryDTO
                    {
                        Id = m.Id,
                        MaterialName = m.MaterialName,
                        Unit = m.Unit,
                        QuantityInStock = m.QuantityInStock,
                        MinThreshold = m.MinThreshold
                    })
                    .ToList();
            }
        }

        // 2. Thêm nguyên liệu mới vào danh mục kho
        public bool InsertMaterial(Material material)
        {
            if (material == null) return false;
            using (var db = GetContext())
            {
                db.Materials.InsertOnSubmit(material);
                db.SubmitChanges();
                return true;
            }
        }

        // 3. Sửa thông tin nguyên liệu
        public bool UpdateMaterial(Material material)
        {
            if (material == null) return false;
            using (var db = GetContext())
            {
                var existing = db.Materials.FirstOrDefault(m => m.Id == material.Id);
                if (existing == null) return false;

                existing.MaterialName = material.MaterialName;
                existing.Unit = material.Unit;
                existing.MinThreshold = material.MinThreshold;

                db.SubmitChanges();
                return true;
            }
        }

        // 4. Xóa nguyên liệu
        public bool DeleteMaterial(int materialId)
        {
            using (var db = GetContext())
            {
                var existing = db.Materials.FirstOrDefault(m => m.Id == materialId);
                if (existing != null)
                {
                    db.Materials.DeleteOnSubmit(existing);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        // 5. THỰC HIỆN GIAO DỊCH NHẬP KHO
        // (Tăng số lượng tồn kho + Chèn 1 dòng vào bảng lịch sử InventoryTransactions)
        public bool ImportStock(int materialId, double quantity, int employeeId)
        {
            using (var db = GetContext())
            {
                var material = db.Materials.FirstOrDefault(m => m.Id == materialId);
                if (material == null) return false;

                // Tăng tồn kho
                material.QuantityInStock += quantity;

                // Ghi nhật ký giao dịch
                var transaction = new InventoryTransaction
                {
                    MaterialId = materialId,
                    EmployeeId = employeeId,
                    TransactionType = "Nhập kho",
                    Quantity = quantity,
                    TransactionDate = DateTime.Now
                };
                db.InventoryTransactions.InsertOnSubmit(transaction);

                db.SubmitChanges();
                return true;
            }
        }

        // 6. THỰC HIỆN GIAO DỊCH XUẤT KHO
        // (Kiểm tra chống âm kho + Giảm tồn kho + Ghi nhật ký giao dịch)
        public bool ExportStock(int materialId, double quantity, int employeeId)
        {
            using (var db = GetContext())
            {
                var material = db.Materials.FirstOrDefault(m => m.Id == materialId);
                if (material == null || material.QuantityInStock < quantity)
                {
                    return false; // Không đủ hàng để xuất
                }

                // Giảm tồn kho
                material.QuantityInStock -= quantity;

                // Ghi nhật ký xuất kho
                var transaction = new InventoryTransaction
                {
                    MaterialId = materialId,
                    EmployeeId = employeeId,
                    TransactionType = "Xuất kho",
                    Quantity = quantity,
                    TransactionDate = DateTime.Now
                };
                db.InventoryTransactions.InsertOnSubmit(transaction);

                db.SubmitChanges();
                return true;
            }
        }

        // 7. Lấy lịch sử giao dịch nhập xuất kho (để hiển thị báo cáo hoặc kiểm tra)
        public List<InventoryTransactionDTO> GetTransactionHistory()
        {
            using (var db = GetContext())
            {
                var query = from t in db.InventoryTransactions
                            join m in db.Materials on t.MaterialId equals m.Id
                            join e in db.Employees on t.EmployeeId equals e.Id
                            orderby t.TransactionDate descending
                            select new InventoryTransactionDTO
                            {
                                Id = t.Id,
                                MaterialId = t.MaterialId,
                                MaterialName = m.MaterialName,
                                EmployeeId = t.EmployeeId,
                                EmployeeName = e.FullName,
                                TransactionType = t.TransactionType,
                                Quantity = t.Quantity,
                                TransactionDate = t.TransactionDate
                            };
                return query.ToList();
            }
        }
    }
}
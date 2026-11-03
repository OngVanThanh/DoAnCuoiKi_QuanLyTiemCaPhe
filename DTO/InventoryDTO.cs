using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    public class InventoryDTO
    {
        public int Id { get; set; }
        public string MaterialName { get; set; }
        public string Unit { get; set; }
        public double QuantityInStock { get; set; }
        public double MinThreshold { get; set; }

        // Thuộc tính tiện ích: Cảnh báo khi tồn kho <= định mức tối thiểu
        public bool IsLowStock => QuantityInStock <= MinThreshold;
    }
}
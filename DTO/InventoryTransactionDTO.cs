using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    public class InventoryTransactionDTO
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string TransactionType { get; set; } // "Nhập kho" hoặc "Xuất kho"
        public double Quantity { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
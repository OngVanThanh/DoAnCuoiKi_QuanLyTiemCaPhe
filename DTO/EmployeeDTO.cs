using System;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? HireDate { get; set; }
    }
}
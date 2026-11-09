using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.DAL
{
    public class ShiftRepository
    {
        public CoffeeShopDataContextDataContext GetContext()
        {
            return new CoffeeShopDataContextDataContext(ConfigHelper.GetConnectionString("CoffeeShopDB"));
        }

        // Đăng ký ca làm việc
        public bool RegisterShift(int employeeId, int shiftId, DateTime workDate)
        {
            try
            {
                using (var db = GetContext())
                {
                    // 1. Kiểm tra trùng lặp ca làm
                    var existing = db.EmployeeShifts
                        .FirstOrDefault(s => s.EmployeeId == employeeId && s.ShiftId == shiftId && s.WorkDate == workDate.Date);

                    if (existing != null)
                    {
                        return false; // Đã tồn tại ca này rồi
                    }

                    // 2. Tự sinh mã Id thủ công định dạng: yyMMdd + "C" + shiftId (Ví dụ: 260811C1)
                    string generatedId = workDate.ToString("yyMMdd") + "C" + shiftId;

                    // 3. Tạo thực thể mới với đầy đủ thông tin
                    var newShift = new EmployeeShift
                    {
                        Id = generatedId,         // Truyền tường minh khóa chính kiểu string vào đây
                        EmployeeId = employeeId,
                        ShiftId = shiftId,
                        WorkDate = workDate.Date,
                        Status = "Đã đăng ký"
                    };

                    // 4. Đẩy thay đổi xuống Database
                    db.EmployeeShifts.InsertOnSubmit(newShift);
                    db.SubmitChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi DAL RegisterShift: " + ex.Message, "Chi tiết lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }

        // Lấy danh sách ca làm quy đổi sang ShiftRTO
        public List<EmployeeShift> GetShiftsByDateRangeRaw(DateTime startDate, DateTime endDate)
        {
            using (var db = GetContext())
            {
                return db.EmployeeShifts
                    .Where(s => s.WorkDate >= startDate.Date && s.WorkDate <= endDate.Date)
                    .ToList();
            }
        }
    }
}
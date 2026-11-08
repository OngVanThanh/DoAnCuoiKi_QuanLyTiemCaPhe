using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    internal class ShiftRepository
    {
        public CoffeeShopDataContextDataContext GetContext()
        {
            return new CoffeeShopDataContextDataContext(ConfigHelper.GetConnectionString("CoffeeShopDB"));
        }
        public bool RegisterShift(int employeeId, int shiftId, DateTime workDate)
        {
            try
            {
                using (var db = GetContext())
                {
                    var existing = db.EmployeeShifts
                        .FirstOrDefault(s => s.EmployeeId == employeeId && s.ShiftId == shiftId && s.WorkDate == workDate.Date);

                    if (existing != null)
                    {
                        return false; // Đã đăng ký ca này trước đó rồi
                    }

                    var newShift = new EmployeeShift
                    {
                        EmployeeId = employeeId,
                        ShiftId = shiftId,
                        WorkDate = workDate.Date,
                        Status = "Đã đăng ký"
                    };

                    db.EmployeeShifts.InsertOnSubmit(newShift);
                    db.SubmitChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Bổ sung: Lấy danh sách ca làm theo khoảng thời gian quy đổi sang ShiftRTO
        public List<ShiftRTO> GetShiftsByDateRange(DateTime startDate, DateTime endDate)
        {
            using (var db = GetContext())
            {
                return db.EmployeeShifts
                    .Where(s => s.WorkDate >= startDate.Date && s.WorkDate <= endDate.Date)
                    .Select(s => new ShiftRTO
                    {
                        Id = s.Id,
                        EmployeeId = s.EmployeeId,
                        FullName = s.Employee.FullName, // Lấy trực tiếp tên từ quan hệ bảng
                        ShiftId = s.ShiftId,
                        WorkDate = s.WorkDate
                    })
                    .ToList();
            }
        }

    }
}

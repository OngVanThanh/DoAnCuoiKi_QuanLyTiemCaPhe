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
        public bool UpdateShiftAssignment(int? employeeId, int shiftId, DateTime workDate)
        {
            try
            {
                using (var db = GetContext())
                {
                    // Tìm bản ghi đã tồn tại chắc chắn trong DB
                    var existing = db.EmployeeShifts
                        .FirstOrDefault(s => s.ShiftId == shiftId && s.WorkDate == workDate.Date);

                    if (existing != null)
                    {
                        if (employeeId.HasValue)
                        {
                            // Cập nhật nhân viên mới cho ca này
                            existing.EmployeeId = employeeId.Value;
                            existing.Status = "Đã đăng ký";
                        }
                        else
                        {
                            // Nếu người dùng bỏ trống ô trên giao diện -> Đặt lại EmployeeId về null
                            existing.EmployeeId = null;
                            existing.Status = "Chưa đăng ký";
                        }
                        db.SubmitChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi DAL UpdateShiftAssignment: " + ex.Message, "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }
        public void InitializeWeekShiftsIfNeeded(DateTime startDate, DateTime endDate)
        {
            using (var db = GetContext())
            {
                for (int i = 0; i < 7; i++)
                {
                    DateTime targetDate = startDate.AddDays(i).Date;
                    for (int shiftId = 1; shiftId <= 3; shiftId++)
                    {
                        // Kiểm tra xem ca này trong ngày đó đã có trong DB chưa
                        bool exists = db.EmployeeShifts.Any(s => s.ShiftId == shiftId && s.WorkDate == targetDate);

                        if (!exists)
                        {
                            // Nếu chưa có -> Tạo mới bản ghi trống (EmployeeId = null)
                            string generatedId = targetDate.ToString("yyMMdd") + "C" + shiftId;
                            var newShift = new EmployeeShift
                            {
                                Id = generatedId,
                                EmployeeId = null, // Chưa có ai nhận ca
                                ShiftId = shiftId,
                                WorkDate = targetDate,
                                Status = "Chưa đăng ký"
                            };
                            db.EmployeeShifts.InsertOnSubmit(newShift);
                        }
                    }
                }
                db.SubmitChanges();
            }
        }
    }
}
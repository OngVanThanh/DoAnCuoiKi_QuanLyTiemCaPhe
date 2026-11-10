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

        // Lấy danh sách thô các ca làm việc theo khoảng thời gian
        public List<EmployeeShift> GetShiftsByDateRangeRaw(DateTime startDate, DateTime endDate)
        {
            using (var db = GetContext())
            {
                return db.EmployeeShifts
                    .Where(s => s.WorkDate >= startDate.Date && s.WorkDate <= endDate.Date)
                    .ToList();
            }
        }

        // Khởi tạo lịch tuần mới phân tách theo từng nhiệm vụ
        public void InitializeWeekShiftsIfNeeded(DateTime startDate, DateTime endDate)
        {
            using (var db = GetContext())
            {
                var allMissions = db.Misions.ToList();
                if (allMissions == null || allMissions.Count == 0) return;

                DateTime start = startDate.Date;
                for (int i = 0; i < 7; i++)
                {
                    DateTime targetDate = start.AddDays(i);

                    for (int shiftId = 1; shiftId <= 3; shiftId++)
                    {
                        foreach (var mission in allMissions)
                        {
                            string missionAbbr = GetMissionAbbreviation(mission.MissisonName);

                            // Kiểm tra sự tồn tại theo Ngày, Ca và Mã nhiệm vụ cụ thể
                            bool exists = db.EmployeeShifts.Any(s =>
                                s.ShiftId == shiftId &&
                                s.WorkDate == targetDate &&
                                s.MisionId == mission.Id);

                            if (!exists)
                            {
                                // Sinh mã Id chuẩn: yyMMdd + "C" + shiftId + MissionAbbr (Ví dụ: 261116C1PV)
                                string generatedId = targetDate.ToString("yyMMdd") + "C" + shiftId + missionAbbr;

                                var newShift = new EmployeeShift
                                {
                                    Id = generatedId,
                                    EmployeeId = null,
                                    ShiftId = shiftId,
                                    WorkDate = targetDate,
                                    MisionId = mission.Id,
                                    Status = "Chưa đăng ký"
                                };

                                db.EmployeeShifts.InsertOnSubmit(newShift);
                            }
                        }
                    }
                }
                db.SubmitChanges();
            }
        }

        // Cập nhật phân công ca làm việc gắn liền với Nhiệm vụ cụ thể
        public bool UpdateShiftAssignmentWithMission(int? employeeId, int shiftId, DateTime workDate, int missionId)
        {
            try
            {
                using (var db = GetContext())
                {
                    var existing = db.EmployeeShifts
                        .FirstOrDefault(s => s.ShiftId == shiftId && s.WorkDate == workDate.Date && s.MisionId == missionId);

                    if (existing != null)
                    {
                        existing.EmployeeId = employeeId;
                        existing.Status = employeeId.HasValue ? "Đã đăng ký" : "Chưa đăng ký";
                        db.SubmitChanges();
                    }
                    else
                    {
                        var missionObj = db.Misions.FirstOrDefault(m => m.Id == missionId);
                        string missionAbbr = missionObj != null ? GetMissionAbbreviation(missionObj.MissisonName) : "";
                        string generatedId = workDate.ToString("yyMMdd") + "C" + shiftId + missionAbbr;

                        var newShift = new EmployeeShift
                        {
                            Id = generatedId,
                            EmployeeId = employeeId,
                            ShiftId = shiftId,
                            WorkDate = workDate.Date,
                            MisionId = missionId,
                            Status = employeeId.HasValue ? "Đã đăng ký" : "Chưa đăng ký"
                        };
                        db.EmployeeShifts.InsertOnSubmit(newShift);
                        db.SubmitChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi DAL UpdateShiftAssignmentWithMission: " + ex.Message, "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }

        // Lấy danh sách nhiệm vụ từ bảng Mision
        public List<Mision> GetAllMisions()
        {
            try
            {
                using (var db = GetContext())
                {
                    return db.Misions.ToList();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi DAL GetAllMisions: " + ex.Message, "Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return new List<Mision>();
            }
        }

        // Hàm phụ trợ viết tắt tên nhiệm vụ (Ví dụ: "Thu ngân" -> "TN")
        private string GetMissionAbbreviation(string missionName)
        {
            if (string.IsNullOrWhiteSpace(missionName)) return string.Empty;
            var words = missionName.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string initials = string.Empty;
            foreach (var word in words)
            {
                initials += char.ToUpper(word[0]);
            }
            return initials;
        }
    }
}
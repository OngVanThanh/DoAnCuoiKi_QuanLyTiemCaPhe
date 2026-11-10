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

        // Lấy danh sách ca làm trong ngày, hỗ trợ lọc theo nhiệm vụ và tìm kiếm từ khóa
        public List<DailyShiftDTO> GetDailyShifts(DateTime date, int? missionId = null, string keyword = null)
        {
            List<DailyShiftDTO> list = new List<DailyShiftDTO>();
            try
            {
                using (var db = GetContext())
                {
                    DateTime targetDate = date.Date;
                    var shifts = db.EmployeeShifts
                                   .Where(s => s.WorkDate == targetDate)
                                   .ToList();

                    var missions = db.Misions.OrderBy(m => m.Id).ToList();
                    var employees = db.Employees.ToDictionary(e => e.Id, e => e.FullName.Trim());
                    var shiftDefs = db.Shifts.OrderBy(s => s.Id).ToList();

                    Dictionary<int, string> shiftNames = new Dictionary<int, string>
                    {
                        { 1, "Ca 1 (06:00 - 12:00)" },
                        { 2, "Ca 2 (11:30 - 17:30)" },
                        { 3, "Ca 3 (16:00 - 22:00)" }
                    };

                    foreach (var sDef in shiftDefs)
                    {
                        shiftNames[sDef.Id] = $"{sDef.ShiftName} ({sDef.StartTime:hh\\:mm} - {sDef.EndTime:hh\\:mm})";
                    }

                    for (int sId = 1; sId <= 3; sId++)
                    {
                        string sName = shiftNames.ContainsKey(sId) ? shiftNames[sId] : $"Ca {sId}";

                        foreach (var m in missions)
                        {
                            var match = shifts.FirstOrDefault(s => s.ShiftId == sId && s.MisionId == m.Id);
                            string empName = "— Chưa phân công —";
                            string status = "Chưa phân công";
                            int? empId = null;

                            if (match != null && match.EmployeeId.HasValue)
                            {
                                empId = match.EmployeeId.Value;
                                if (employees.ContainsKey(empId.Value))
                                {
                                    empName = employees[empId.Value];
                                    status = "Đã phân công";
                                }
                            }

                            list.Add(new DailyShiftDTO
                            {
                                ShiftId = sId,
                                ShiftName = sName,
                                MissionId = m.Id,
                                MissionName = m.MissisonName,
                                EmployeeId = empId,
                                EmployeeName = empName,
                                Status = status,
                                WorkDate = targetDate
                            });
                        }
                    }
                }
            }
            catch
            {
                // Fallback nếu tạm thời chưa kết nối được CSDL
            }

            // Lọc theo nhiệm vụ nếu có
            if (missionId.HasValue && missionId.Value > 0)
            {
                list = list.Where(x => x.MissionId == missionId.Value).ToList();
            }

            // Lọc theo từ khóa tìm kiếm nếu có
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string kw = keyword.Trim();
                list = list.Where(x =>
                    (!string.IsNullOrEmpty(x.EmployeeName) && x.EmployeeName.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrEmpty(x.MissionName) && x.MissionName.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrEmpty(x.ShiftName) && x.ShiftName.IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0)
                ).ToList();
            }

            return list;
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
        // Lấy danh sách lịch làm việc tuần sau phục vụ in báo cáo RDLC / xuất Word
        public List<ShiftDTO> GetNextWeekScheduleForReport()
        {
            List<ShiftDTO> result = new List<ShiftDTO>();

            // 1. Xác định Thứ Hai và Chủ Nhật của tuần sau
            DateTime today = DateTime.Today;
            int daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            if (daysUntilNextMonday == 0) daysUntilNextMonday = 7;

            DateTime startDate = today.AddDays(daysUntilNextMonday).Date;
            DateTime endDate = startDate.AddDays(6).Date;

            using (var db = GetContext())
            {
                var shifts = db.EmployeeShifts
                               .Where(s => s.WorkDate >= startDate && s.WorkDate <= endDate)
                               .ToList();
                var missions = db.Misions.OrderBy(m => m.Id).ToList();
                var employees = db.Employees.ToDictionary(e => e.Id, e => e.FullName.Trim());

                string GetEmpName(DateTime date, int sId, int mId)
                {
                    var match = shifts.FirstOrDefault(s => s.WorkDate == date && s.ShiftId == sId && s.MisionId == mId);
                    if (match != null && match.EmployeeId.HasValue && employees.ContainsKey(match.EmployeeId.Value))
                    {
                        return employees[match.EmployeeId.Value];
                    }
                    return "-"; // Chưa phân công
                }

                // 2. Duyệt 7 ngày từ Thứ Hai đến Chủ Nhật
                for (int i = 0; i < 7; i++)
                {
                    DateTime targetDate = startDate.AddDays(i);
                    string dateStr = targetDate.ToString("dd/MM/yyyy") + " (" + targetDate.ToString("ddd") + ")";

                    foreach (var mission in missions)
                    {
                        result.Add(new ShiftDTO
                        {
                            DateString = dateStr,
                            MissionName = mission.MissisonName,
                            Shift1 = GetEmpName(targetDate, 1, mission.Id),
                            Shift2 = GetEmpName(targetDate, 2, mission.Id),
                            Shift3 = GetEmpName(targetDate, 3, mission.Id)
                        });
                    }
                }
            }
            return result;
        }
    }
}
using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmShiftSchedule : Form
    {
        private ShiftRepository _shiftRepo = new ShiftRepository();
        private EmployeeRepository _empRepo = new EmployeeRepository();
        private List<string> employeeNames = new List<string>();

        public FrmShiftSchedule()
        {
            InitializeComponent();
        }

        private void FrmShiftSchedule_Load(object sender, EventArgs e)
        {
            LoadEmployeeData();
            PopulateComboBoxSources();
            LoadWeekScheduleTemplateWithExistingData();
        }

        private void LoadEmployeeData()
        {
            employeeNames.Clear();
            var employees = _empRepo.GetAllEmployees();
            if (employees != null)
            {
                // Thêm tùy chọn dòng trống để hủy phân công nếu muốn
                employeeNames.Add("");
                foreach (var emp in employees)
                {
                    if (!string.IsNullOrWhiteSpace(emp.FullName))
                    {
                        employeeNames.Add(emp.FullName.Trim());
                    }
                }
            }
        }

        private void PopulateComboBoxSources()
        {
            // Đổ danh sách nhân viên vào các cột ComboBox Ca 1, Ca 2, Ca 3 trên DataGridView
            if (dgvSchedule.Columns["Shift1"] is DataGridViewComboBoxColumn colCa1)
                colCa1.DataSource = new List<string>(employeeNames);

            if (dgvSchedule.Columns["Shift2"] is DataGridViewComboBoxColumn colCa2)
                colCa2.DataSource = new List<string>(employeeNames);

            if (dgvSchedule.Columns["Shift3"] is DataGridViewComboBoxColumn colCa3)
                colCa3.DataSource = new List<string>(employeeNames);
        }

        // Tạo template lịch tuần mới kết hợp Ngày và Nhiệm vụ, đồng thời load dữ liệu từ DB
        private void LoadWeekScheduleTemplateWithExistingData()
        {
            dgvSchedule.Rows.Clear();

            // Xác định khoảng thời gian Thứ 2 đến Chủ Nhật của tuần sau
            DateTime today = DateTime.Today;
            int daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            if (daysUntilNextMonday == 0) daysUntilNextMonday = 7;

            DateTime startDate = today.AddDays(daysUntilNextMonday).Date;
            DateTime endDate = startDate.AddDays(6).Date;

            // Lấy danh sách ca và danh sách nhiệm vụ
            var existingShifts = _shiftRepo.GetShiftsByDateRangeRaw(startDate, endDate);
            var allMissions = _shiftRepo.GetAllMisions();

            if (allMissions == null || allMissions.Count == 0) return;

            for (int i = 0; i < 7; i++)
            {
                DateTime targetDate = startDate.AddDays(i);
                string dateStr = targetDate.ToString("dd/MM/yyyy") + " (" + targetDate.ToString("ddd") + ")";

                foreach (var mission in allMissions)
                {
                    // Lấy tên nhân viên theo Ngày, Mã ca và Mã nhiệm vụ
                    string empShift1 = GetEmployeeNameByMission(existingShifts, targetDate, 1, mission.Id);
                    string empShift2 = GetEmployeeNameByMission(existingShifts, targetDate, 2, mission.Id);
                    string empShift3 = GetEmployeeNameByMission(existingShifts, targetDate, 3, mission.Id);

                    // Thêm dòng lên DataGridView (Cột 0: Ngày, Cột 1: Nhiệm vụ, Cột 2-4: Ca 1, Ca 2, Ca 3)
                    dgvSchedule.Rows.Add(dateStr, mission.MissisonName, empShift1, empShift2, empShift3);
                }
            }
        }

        // Hàm phụ trợ tìm tên nhân viên theo ngày, ca và nhiệm vụ
        private string GetEmployeeNameByMission(List<EmployeeShift> shifts, DateTime date, int shiftId, int missionId)
        {
            var match = shifts.FirstOrDefault(s => s.WorkDate.HasValue &&
                                                   s.WorkDate.Value.Date == date.Date &&
                                                   s.ShiftId == shiftId &&
                                                   s.MisionId == missionId);
            if (match != null && match.EmployeeId.HasValue)
            {
                var emp = _empRepo.GetAllEmployees().FirstOrDefault(e => e.Id == match.EmployeeId.Value);
                return emp != null ? emp.FullName.Trim() : "";
            }
            return "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Gom nhóm ca làm việc theo Ngày để kiểm tra ràng buộc toàn cục trong ngày cho từng nhân viên
                // Key: Ngày (string dạng dd/MM/yyyy), Value: Dictionary<Tên nhân viên, List<Mã ca>>
                Dictionary<string, Dictionary<string, List<int>>> dailyEmployeeShifts = new Dictionary<string, Dictionary<string, List<int>>>();

                // Danh sách tạm lưu lại thông tin để xử lý lưu vào DB sau khi validate thành công
                var updateQueue = new List<Tuple<DateTime, int, int, string>>(); // Date, ShiftId, MissionId, EmpName
                var allMissions = _shiftRepo.GetAllMisions();

                foreach (DataGridViewRow row in dgvSchedule.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells[0].Value == null || row.Cells[1].Value == null) continue;

                    string dateCellText = row.Cells[0].Value.ToString();
                    string datePart = dateCellText.Split(' ')[0];

                    if (!DateTime.TryParseExact(datePart, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime workDate))
                    {
                        continue;
                    }

                    string missionName = row.Cells[1].Value.ToString().Trim();
                    var missionObj = allMissions.FirstOrDefault(m => m.MissisonName.Trim().Equals(missionName, StringComparison.OrdinalIgnoreCase));
                    if (missionObj == null) continue;

                    string name1 = row.Cells["Shift1"].Value?.ToString().Trim();
                    string name2 = row.Cells["Shift2"].Value?.ToString().Trim();
                    string name3 = row.Cells["Shift3"].Value?.ToString().Trim();

                    // Đưa vào hàng đợi cập nhật
                    updateQueue.Add(new Tuple<DateTime, int, int, string>(workDate, 1, missionObj.Id, name1));
                    updateQueue.Add(new Tuple<DateTime, int, int, string>(workDate, 2, missionObj.Id, name2));
                    updateQueue.Add(new Tuple<DateTime, int, int, string>(workDate, 3, missionObj.Id, name3));

                    // Gom nhóm để kiểm tra ràng buộc quy tắc ca làm trong ngày
                    if (!dailyEmployeeShifts.ContainsKey(datePart))
                    {
                        dailyEmployeeShifts[datePart] = new Dictionary<string, List<int>>();
                    }

                    void AddToDailyMap(string empName, int sId)
                    {
                        if (!string.IsNullOrEmpty(empName))
                        {
                            if (!dailyEmployeeShifts[datePart].ContainsKey(empName))
                            {
                                dailyEmployeeShifts[datePart][empName] = new List<int>();
                            }
                            dailyEmployeeShifts[datePart][empName].Add(sId);
                        }
                    }

                    AddToDailyMap(name1, 1);
                    AddToDailyMap(name2, 2);
                    AddToDailyMap(name3, 3);
                }

                // Kiểm tra ràng buộc: Tối đa 2 ca/ngày, phải cách nhau đúng 1 ca (ví dụ Ca 1 và Ca 3) tính trên toàn bộ nhiệm vụ trong ngày
                foreach (var dayPair in dailyEmployeeShifts)
                {
                    string dateStr = dayPair.Key;
                    foreach (var empPair in dayPair.Value)
                    {
                        string empName = empPair.Key;
                        List<int> shifts = empPair.Value;

                        if (shifts.Count > 2)
                        {
                            MessageBox.Show($"Nhân viên '{empName}' vào ngày {dateStr} đăng ký tổng cộng {shifts.Count} ca (Vượt quá tối đa 2 ca/ngày)!", "Cảnh báo ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (shifts.Count == 2)
                        {
                            shifts.Sort();
                            // Khoảng cách ca hợp lệ phải là 2 (tức là Ca 1 và Ca 3, hoặc cách nhau 1 ca ở giữa)
                            if (shifts[1] - shifts[0] != 2)
                            {
                                MessageBox.Show($"Nhân viên '{empName}' vào ngày {dateStr} đăng ký 2 ca liền kề không hợp lệ (Các ca làm việc phải cách nhau đúng 1 ca)!", "Cảnh báo ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }

                // Tiến hành lưu dữ liệu xuống Database thông qua Repository
                foreach (var item in updateQueue)
                {
                    DateTime wDate = item.Item1;
                    int sId = item.Item2;
                    int mId = item.Item3;
                    string empName = item.Item4;

                    int? employeeId = null;
                    if (!string.IsNullOrEmpty(empName))
                    {
                        var emp = _empRepo.GetEmployeeByName(empName);
                        if (emp != null)
                        {
                            employeeId = emp.Id;
                        }
                    }

                    _shiftRepo.UpdateShiftAssignmentWithMission(employeeId, sId, wDate, mId);
                }

                MessageBox.Show("Cập nhật lịch phân công làm việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
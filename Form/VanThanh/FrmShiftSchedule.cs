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
        private List<EmployeeScheduleItem> _employeeItems = new List<EmployeeScheduleItem>();
        private Dictionary<int, EmployeeDTO> _employeeMap = new Dictionary<int, EmployeeDTO>();

        public FrmShiftSchedule()
        {
            InitializeComponent();
        }

        private void FrmShiftSchedule_Load(object sender, EventArgs e)
        {
            dgvSchedule.DataError += (s, ev) => { ev.Cancel = true; };
            LoadEmployeeData();
            PopulateComboBoxSources();
            LoadWeekScheduleTemplateWithExistingData();
        }

        private void LoadEmployeeData()
        {
            _employeeItems.Clear();
            _employeeMap.Clear();
            var employees = _empRepo.GetAllEmployees();

            _employeeItems.Add(new EmployeeScheduleItem { Id = 0, DisplayText = "" });
            if (employees != null)
            {
                foreach (var emp in employees)
                {
                    _employeeMap[emp.Id] = emp;
                    _employeeItems.Add(new EmployeeScheduleItem
                    {
                        Id = emp.Id,
                        DisplayText = $"{emp.FullName} (#{emp.Id})"
                    });
                }
            }
        }

        private void PopulateComboBoxSources()
        {
            // Đổ danh sách nhân viên vào các cột ComboBox Ca 1, Ca 2, Ca 3 trên DataGridView theo ID
            void SetupColumn(string colName)
            {
                if (dgvSchedule.Columns[colName] is DataGridViewComboBoxColumn col)
                {
                    col.DataSource = new List<EmployeeScheduleItem>(_employeeItems);
                    col.DisplayMember = "DisplayText";
                    col.ValueMember = "Id";
                }
            }

            SetupColumn("Shift1");
            SetupColumn("Shift2");
            SetupColumn("Shift3");
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
                    // Lấy mã nhân viên (Id) theo Ngày, Mã ca và Mã nhiệm vụ
                    int empShift1 = GetEmployeeIdByMission(existingShifts, targetDate, 1, mission.Id);
                    int empShift2 = GetEmployeeIdByMission(existingShifts, targetDate, 2, mission.Id);
                    int empShift3 = GetEmployeeIdByMission(existingShifts, targetDate, 3, mission.Id);

                    // Thêm dòng lên DataGridView (Cột 0: Ngày, Cột 1: Nhiệm vụ, Cột 2-4: Ca 1, Ca 2, Ca 3)
                    dgvSchedule.Rows.Add(dateStr, mission.MissisonName, empShift1, empShift2, empShift3);
                }
            }
        }

        // Hàm phụ trợ tìm Id nhân viên theo ngày, ca và nhiệm vụ
        private int GetEmployeeIdByMission(List<EmployeeShift> shifts, DateTime date, int shiftId, int missionId)
        {
            var match = shifts.FirstOrDefault(s => s.WorkDate.HasValue &&
                                                   s.WorkDate.Value.Date == date.Date &&
                                                   s.ShiftId == shiftId &&
                                                   s.MisionId == missionId);
            if (match != null && match.EmployeeId.HasValue && _employeeMap.ContainsKey(match.EmployeeId.Value))
            {
                return match.EmployeeId.Value;
            }
            return 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Gom nhóm ca làm việc theo Ngày để kiểm tra ràng buộc toàn cục trong ngày cho từng nhân viên theo ID
                // Key: Ngày (string dạng dd/MM/yyyy), Value: Dictionary<EmployeeId, List<Mã ca>>
                Dictionary<string, Dictionary<int, List<int>>> dailyEmployeeShifts = new Dictionary<string, Dictionary<int, List<int>>>();

                // Danh sách tạm lưu lại thông tin để xử lý lưu vào DB sau khi validate thành công
                var updateQueue = new List<Tuple<DateTime, int, int, int>>(); // Date, ShiftId, MissionId, EmpId
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

                    int ParseCellEmpId(object val)
                    {
                        if (val == null) return 0;
                        if (int.TryParse(val.ToString(), out int id)) return id;
                        return 0;
                    }

                    int empId1 = ParseCellEmpId(row.Cells["Shift1"].Value);
                    int empId2 = ParseCellEmpId(row.Cells["Shift2"].Value);
                    int empId3 = ParseCellEmpId(row.Cells["Shift3"].Value);

                    // Đưa vào hàng đợi cập nhật
                    updateQueue.Add(new Tuple<DateTime, int, int, int>(workDate, 1, missionObj.Id, empId1));
                    updateQueue.Add(new Tuple<DateTime, int, int, int>(workDate, 2, missionObj.Id, empId2));
                    updateQueue.Add(new Tuple<DateTime, int, int, int>(workDate, 3, missionObj.Id, empId3));

                    // Gom nhóm để kiểm tra ràng buộc quy tắc ca làm trong ngày
                    if (!dailyEmployeeShifts.ContainsKey(datePart))
                    {
                        dailyEmployeeShifts[datePart] = new Dictionary<int, List<int>>();
                    }

                    void AddToDailyMap(int eId, int sId)
                    {
                        if (eId > 0)
                        {
                            if (!dailyEmployeeShifts[datePart].ContainsKey(eId))
                            {
                                dailyEmployeeShifts[datePart][eId] = new List<int>();
                            }
                            dailyEmployeeShifts[datePart][eId].Add(sId);
                        }
                    }

                    AddToDailyMap(empId1, 1);
                    AddToDailyMap(empId2, 2);
                    AddToDailyMap(empId3, 3);
                }

                // Kiểm tra ràng buộc: Tối đa 2 ca/ngày, phải cách nhau đúng 1 ca (ví dụ Ca 1 và Ca 3) tính trên toàn bộ nhiệm vụ trong ngày
                foreach (var dayPair in dailyEmployeeShifts)
                {
                    string dateStr = dayPair.Key;
                    foreach (var empPair in dayPair.Value)
                    {
                        int eId = empPair.Key;
                        List<int> shifts = empPair.Value;
                        string empDisplayName = _employeeMap.ContainsKey(eId)
                            ? $"{_employeeMap[eId].FullName} (ID: {eId})"
                            : $"ID: {eId}";

                        if (shifts.Count > 2)
                        {
                            MessageBox.Show($"Nhân viên '{empDisplayName}' vào ngày {dateStr} đăng ký tổng cộng {shifts.Count} ca (Vượt quá tối đa 2 ca/ngày)!", "Cảnh báo ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (shifts.Count == 2)
                        {
                            shifts.Sort();
                            // Khoảng cách ca hợp lệ phải là 2 (tức là Ca 1 và Ca 3, hoặc cách nhau 1 ca ở giữa)
                            if (shifts[1] - shifts[0] != 2)
                            {
                                MessageBox.Show($"Nhân viên '{empDisplayName}' vào ngày {dateStr} đăng ký 2 ca liền kề không hợp lệ (Các ca làm việc phải cách nhau đúng 1 ca)!", "Cảnh báo ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    int eId = item.Item4;

                    int? employeeId = eId > 0 ? (int?)eId : null;
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

    public class EmployeeScheduleItem
    {
        public int Id { get; set; }
        public string DisplayText { get; set; }

        public override string ToString()
        {
            return DisplayText;
        }
    }
}
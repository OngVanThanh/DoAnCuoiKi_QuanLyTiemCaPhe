using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
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
        private  ShiftRepository _shiftRepo = new ShiftRepository();
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
            // Đổ danh sách nhân viên vào cột ComboBox Ca 1, Ca 2, Ca 3
            if (dgvSchedule.Columns["Shift1"] is DataGridViewComboBoxColumn colCa1)
                colCa1.DataSource = new List<string>(employeeNames);

            if (dgvSchedule.Columns["Shift2"] is DataGridViewComboBoxColumn colCa2)
                colCa2.DataSource = new List<string>(employeeNames);

            if (dgvSchedule.Columns["Shift3"] is DataGridViewComboBoxColumn colCa3)
                colCa3.DataSource = new List<string>(employeeNames);
        }
        // Tạo 7 ngày (21 ca) và load dữ liệu đã lưu từ Database lên DataGridView
        private void LoadWeekScheduleTemplateWithExistingData()
        {
            dgvSchedule.Rows.Clear();
            DateTime startDate = DateTime.Now.AddDays(1).Date;
            DateTime endDate = startDate.AddDays(6).Date;

            // Lấy danh sách ca đã đăng ký sẵn trong tuần từ Database
            var existingShifts = _shiftRepo.GetShiftsByDateRangeRaw(startDate, endDate);

            for (int i = 0; i < 7; i++)
            {
                DateTime targetDate = startDate.AddDays(i);
                string dateStr = targetDate.ToString("dd/MM/yyyy") + " (" + targetDate.ToString("ddd") + ")";

                // Tìm xem ngày này có nhân viên nào đã làm Ca 1, Ca 2, Ca 3 chưa
                string empShift1 = GetEmployeeNameById(existingShifts, targetDate, 1);
                string empShift2 = GetEmployeeNameById(existingShifts, targetDate, 2);
                string empShift3 = GetEmployeeNameById(existingShifts, targetDate, 3);

                // Thêm dòng với ngày và các ca đã có dữ liệu (hoặc trống nếu chưa đăng ký)
                dgvSchedule.Rows.Add(dateStr, empShift1, empShift2, empShift3);
            }
        }

        // Hàm phụ trợ tìm tên nhân viên theo ngày và mã ca từ danh sách trong DB (Đã xử lý an toàn DateTime?)
        private string GetEmployeeNameById(List<EmployeeShift> shifts, DateTime date, int shiftId)
        {
            var match = shifts.FirstOrDefault(s => s.WorkDate.HasValue && s.WorkDate.Value.Date == date.Date && s.ShiftId == shiftId);
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
                foreach (DataGridViewRow row in dgvSchedule.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells[0].Value == null) continue;

                    string dateCellText = row.Cells[0].Value.ToString();
                    string datePart = dateCellText.Split(' ')[0];

                    if (!DateTime.TryParseExact(datePart, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime workDate))
                    {
                        continue;
                    }

                    string name1 = row.Cells["Shift1"].Value?.ToString().Trim();
                    string name2 = row.Cells["Shift2"].Value?.ToString().Trim();
                    string name3 = row.Cells["Shift3"].Value?.ToString().Trim();

                    // Kiểm tra ràng buộc tối đa 2 ca/ngày, cách nhau 1 ca (Ca 1 và Ca 3)
                    Dictionary<string, List<int>> empShiftsMap = new Dictionary<string, List<int>>();
                    void AddMapping(string name, int shiftId)
                    {
                        if (!string.IsNullOrEmpty(name))
                        {
                            if (!empShiftsMap.ContainsKey(name)) empShiftsMap[name] = new List<int>();
                            empShiftsMap[name].Add(shiftId);
                        }
                    }
                    AddMapping(name1, 1);
                    AddMapping(name2, 2);
                    AddMapping(name3, 3);

                    foreach (var pair in empShiftsMap)
                    {
                        string empName = pair.Key;
                        List<int> shifts = pair.Value;

                        if (shifts.Count > 2)
                        {
                            MessageBox.Show($"Nhân viên '{empName}' ngày {datePart} đăng ký quá 2 ca!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (shifts.Count == 2)
                        {
                            shifts.Sort();
                            if (shifts[1] - shifts[0] != 2)
                            {
                                MessageBox.Show($"Nhân viên '{empName}' ngày {datePart} đăng ký 2 ca liền kề không hợp lệ (Phải cách nhau 1 ca)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    // Thực hiện cập nhật dữ liệu cho từng ca trong ngày
                    UpdateCellDatabase(workDate, 1, name1);
                    UpdateCellDatabase(workDate, 2, name2);
                    UpdateCellDatabase(workDate, 3, name3);
                }

                MessageBox.Show("Cập nhật lịch làm việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCellDatabase(DateTime workDate, int shiftId, string employeeName)
        {
            int? employeeId = null;
            if (!string.IsNullOrEmpty(employeeName))
            {
                var emp = _empRepo.GetEmployeeByName(employeeName);
                if (emp != null)
                {
                    employeeId = emp.Id;
                }
            }

            _shiftRepo.UpdateShiftAssignment(employeeId, shiftId, workDate);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

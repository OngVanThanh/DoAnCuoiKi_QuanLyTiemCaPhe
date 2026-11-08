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
        private void LoadWeekScheduleTemplate()
        {
            dgvSchedule.Rows.Clear();
            DateTime startDate = DateTime.Now.AddDays(1);

            for (int i = 0; i < 7; i++)
            {
                DateTime targetDate = startDate.AddDays(i);
                string dateStr = targetDate.ToString("dd/MM/yyyy") + " (" + targetDate.ToString("ddd") + ")";

                // Thêm dòng với cột Ngày (index 0 hoặc tên cột "Date"), các cột Ca ban đầu để trống
                dgvSchedule.Rows.Add(dateStr, "", "", "");
            }
        }
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
        // Hàm phụ trợ tìm tên nhân viên theo ngày và mã ca từ danh sách trong DB
        private string GetEmployeeNameById(List<EmployeeShift> shifts, DateTime date, int shiftId)
        {
            // Thêm kiểm tra s.WorkDate.HasValue và s.WorkDate.Value.Date
            var match = shifts.FirstOrDefault(s => s.WorkDate.HasValue && s.WorkDate.Value.Date == date.Date && s.ShiftId == shiftId);
            if (match != null)
            {
                var emp = _empRepo.GetAllEmployees().FirstOrDefault(e => e.Id == match.EmployeeId);
                return emp != null ? emp.FullName.Trim() : "";
            }
            return "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int successCount = 0;

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

                    // Lấy giá trị từ 3 cột ca làm
                    Dictionary<int, List<string>> shiftEmployees = new Dictionary<int, List<string>>
                    {
                        { 1, GetSelectedName(row.Cells["Shift1"].Value) },
                        { 2, GetSelectedName(row.Cells["Shift2"].Value) },
                        { 3, GetSelectedName(row.Cells["Shift3"].Value) }
                    };

                    var empShiftMapping = new Dictionary<string, List<int>>();
                    foreach (var shift in shiftEmployees)
                    {
                        int shiftId = shift.Key;
                        foreach (var empName in shift.Value)
                        {
                            if (!empShiftMapping.ContainsKey(empName))
                            {
                                empShiftMapping[empName] = new List<int>();
                            }
                            empShiftMapping[empName].Add(shiftId);
                        }
                    }

                    // Kiểm tra ràng buộc nghiệp vụ
                    foreach (var pair in empShiftMapping)
                    {
                        string empName = pair.Key;
                        List<int> registeredShifts = pair.Value;

                        var employee = _empRepo.GetEmployeeByName(empName);
                        if (employee == null)
                        {
                            MessageBox.Show($"Không tìm thấy nhân viên '{empName}' trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Quy tắc 1: Tối đa 2 ca/ngày
                        if (registeredShifts.Count > 2)
                        {
                            MessageBox.Show($"Nhân viên '{empName}' ngày {datePart} đăng ký quá 2 ca (tối đa 2 ca/ngày)!", "Vi phạm ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Quy tắc 2: Đăng ký 2 ca thì phải cách nhau 1 ca (Ca 1 và Ca 3)
                        if (registeredShifts.Count == 2)
                        {
                            registeredShifts.Sort();
                            if (registeredShifts[1] - registeredShifts[0] != 2)
                            {
                                MessageBox.Show($"Nhân viên '{empName}' ngày {datePart} đăng ký 2 ca liền kề không hợp lệ (Phải cách nhau 1 ca, chỉ được chọn Ca 1 và Ca 3)!", "Vi phạm ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // Lưu xuống Database (Hàm RegisterShift đã có kiểm tra trùng lặp nên nếu đã có rồi sẽ bỏ qua, không gây lỗi)
                        foreach (var shiftId in registeredShifts)
                        {
                            bool ok = _shiftRepo.RegisterShift(employee.Id, shiftId, workDate);
                            if (ok) successCount++;
                        }
                    }
                }

                MessageBox.Show($"Đã cập nhật thành công {successCount} lượt đăng ký ca làm vào Database!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<string> GetSelectedName(object cellValue)
        {
            List<string> list = new List<string>();
            if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
            {
                list.Add(cellValue.ToString().Trim());
            }
            return list;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.BLL
{
    public class ShiftService
    {
        private ShiftRepository _shiftRepo = new ShiftRepository();
        private EmployeeRepository _empRepo = new EmployeeRepository();

        // 1. Phương thức mở Hộp thoại đăng ký ca làm việc (FrmShiftSchedule)
        public void OpenShiftScheduleDialog()
        {
            using (FrmShiftSchedule frm = new FrmShiftSchedule())
            {
                frm.ShowDialog();
            }
        }

        // 2. Lấy danh sách họ tên tất cả nhân viên phục vụ nạp vào ComboBox của DataGridView
        public List<string> GetEmployeeNamesForSchedule()
        {
            List<string> employeeNames = new List<string>();
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

            return employeeNames;
        }

        // 3. Kiểm tra ràng buộc ca làm việc cho 1 nhân viên trong 1 ngày
        public bool ValidateShiftRules(string empName, List<int> registeredShifts, string dateStr, out string errorMessage)
        {
            errorMessage = "";

            // Quy tắc 1: Tối đa 2 ca/ngày
            if (registeredShifts.Count > 2)
            {
                errorMessage = $"Nhân viên '{empName}' ngày {dateStr} đăng ký quá 2 ca (tối đa 2 ca/ngày)!";
                return false;
            }

            // Quy tắc 2: Nếu đăng ký 2 ca thì phải cách nhau 1 ca (Bắt buộc là cặp Ca 1 và Ca 3)
            if (registeredShifts.Count == 2)
            {
                registeredShifts.Sort();
                if (registeredShifts[1] - registeredShifts[0] != 2)
                {
                    errorMessage = $"Nhân viên '{empName}' ngày {dateStr} đăng ký 2 ca liền kề không hợp lệ (Phải cách nhau 1 ca, chỉ được chọn Ca 1 và Ca 3)!";
                    return false;
                }
            }

            return true;
        }

        // 4. Lưu lượt đăng ký ca xuống Database thông qua ShiftRepository
    }
}
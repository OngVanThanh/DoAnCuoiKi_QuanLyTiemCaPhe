using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.BLL
{
    public class ShiftService
    {
        private ShiftRepository _shiftRepo = new ShiftRepository();
        private EmployeeRepository _empRepo = new EmployeeRepository();

        // 1. Hàm tạo hoặc cập nhật file Excel mẫu tích hợp Dropdown danh sách nhân viên từ Server
        public string GenerateExcelTemplate(string savePath)
        {
            // Bước A: Lấy danh sách tên nhân viên từ CSDL thông qua EmployeeRepository (sử dụng LINQ)
            List<string> employeeNames = new List<string>();
            var employees = _empRepo.GetAllEmployees(); // Gọi LINQ từ DAL
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

            // Kiểm tra xem file đã tồn tại hay chưa để xử lý linh hoạt
            if (File.Exists(savePath))
            {
                // Nếu file đã có: Mở lên và cập nhật lại sheet chứa danh sách nhân viên cho đồng bộ với Server
                using (var workbook = new XLWorkbook(savePath))
                {
                    var wsEmployees = workbook.Worksheets.FirstOrDefault(w => w.Name == "EmployeesList");
                    if (wsEmployees != null)
                    {
                        wsEmployees.Clear(); // Xóa dữ liệu cũ
                    }
                    else
                    {
                        wsEmployees = workbook.Worksheets.Add("EmployeesList");
                    }
                    wsEmployees.Hide(); // Ẩn sheet danh sách nhân viên

                    // Nạp danh sách tên nhân viên mới nhất vào cột A của sheet ẩn
                    for (int i = 0; i < employeeNames.Count; i++)
                    {
                        wsEmployees.Cell(i + 1, 1).Value = employeeNames[i];
                    }

                    workbook.Save();
                }
            }
            else
            {
                // Nếu chưa có file: Tạo mới hoàn toàn file Excel mẫu
                using (var workbook = new XLWorkbook())
                {
                    // Tạo sheet phụ ẩn để lưu danh sách nhân viên làm nguồn cho Dropdown
                    var wsEmployees = workbook.Worksheets.Add("EmployeesList");
                    wsEmployees.Hide();

                    for (int i = 0; i < employeeNames.Count; i++)
                    {
                        wsEmployees.Cell(i + 1, 1).Value = employeeNames[i];
                    }

                    // Tạo sheet chính hiển thị lịch làm việc tuần
                    var worksheet = workbook.Worksheets.Add("LichLamViecTuan");

                    // Tiêu đề cột
                    worksheet.Cell(1, 1).Value = "Date (dd/MM/yyyy)";
                    worksheet.Cell(1, 2).Value = "Ca 1 (Sáng)";
                    worksheet.Cell(1, 3).Value = "Ca 2 (Chiều)";
                    worksheet.Cell(1, 4).Value = "Ca 3 (Tối)";

                    // Định dạng tiêu đề đậm, màu nền xanh nhẹ
                    var headerRange = worksheet.Range("A1:D1");
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightSkyBlue;

                    // Xác định vùng dữ liệu nguồn cho Dropdown (Ví dụ: EmployeesList!$A$1:$A$10)
                    int totalEmployees = employeeNames.Count;
                    string sourceRange = totalEmployees > 0 ? $"=EmployeesList!$A$1:$A${totalEmployees}" : "\"\"";

                    // Sinh sẵn 7 ngày tiếp theo tính từ ngày mai
                    DateTime startDate = DateTime.Now.AddDays(1);
                    for (int i = 0; i < 7; i++)
                    {
                        int currentRow = i + 2;
                        DateTime targetDate = startDate.AddDays(i);

                        // Ghi ngày tháng và thứ trong tuần
                        worksheet.Cell(currentRow, 1).Value = targetDate.ToString("dd/MM/yyyy") + " (" + targetDate.ToString("ddd") + ")";

                        // Gán Data Validation (Dropdown List) cho 3 cột Ca làm việc
                        if (totalEmployees > 0)
                        {
                            worksheet.Cell(currentRow, 2).SetDataValidation().List(sourceRange);
                            worksheet.Cell(currentRow, 3).SetDataValidation().List(sourceRange);
                            worksheet.Cell(currentRow, 4).SetDataValidation().List(sourceRange);
                        }
                    }

                    worksheet.Columns().AdjustToContents();

                    // Đảm bảo thư mục chứa file tồn tại trước khi lưu
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                    workbook.SaveAs(savePath);
                }
            }

            return savePath;
        }

        // 2. Hàm đọc file Excel sau khi quản lý đã điền dữ liệu và lưu vào Database
        public bool ImportShiftFromExcel(string filePath, out string message)
        {
            message = "";
            int successCount = 0;

            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet("LichLamViecTuan");
                    var rows = worksheet.RowsUsed().Skip(1); // Bỏ qua dòng tiêu đề

                    foreach (var row in rows)
                    {
                        var dateCellText = row.Cell(1).GetString().Trim();
                        if (string.IsNullOrEmpty(dateCellText)) continue;

                        string datePart = dateCellText.Split(' ')[0];
                        if (!DateTime.TryParseExact(datePart, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime workDate))
                        {
                            continue;
                        }

                        // Đọc tên nhân viên được chọn từ Dropdown của 3 cột Ca
                        Dictionary<int, List<string>> shiftEmployees = new Dictionary<int, List<string>>
                        {
                            { 1, ParseNames(row.Cell(2).GetString()) },
                            { 2, ParseNames(row.Cell(3).GetString()) },
                            { 3, ParseNames(row.Cell(4).GetString()) }
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

                        // Kiểm tra ràng buộc và lưu xuống CSDL
                        foreach (var pair in empShiftMapping)
                        {
                            string empName = pair.Key;
                            List<int> registeredShifts = pair.Value;

                            // Gọi phương thức từ EmployeeRepository để tìm nhân viên
                            var employee = _empRepo.GetEmployeeByName(empName);
                            if (employee == null)
                            {
                                message = $"Không tìm thấy nhân viên '{empName}' trong hệ thống!";
                                return false;
                            }

                            // Ràng buộc 1: Tối đa 2 ca/ngày
                            if (registeredShifts.Count > 2)
                            {
                                message = $"Nhân viên '{empName}' ngày {datePart} đăng ký quá 2 ca (tối đa 2 ca/ngày)!";
                                return false;
                            }

                            // Ràng buộc 2: Đăng ký 2 ca thì phải cách nhau 1 ca (Cặp Ca 1 và Ca 3)
                            if (registeredShifts.Count == 2)
                            {
                                registeredShifts.Sort();
                                if (registeredShifts[1] - registeredShifts[0] != 2)
                                {
                                    message = $"Nhân viên '{empName}' ngày {datePart} đăng ký 2 ca liền kề không hợp lệ (Phải cách nhau 1 ca, chỉ được chọn Ca 1 và Ca 3)!";
                                    return false;
                                }
                            }

                            // Lưu từng ca xuống Database thông qua ShiftRepository
                            foreach (var shiftId in registeredShifts)
                            {
                                bool ok = _shiftRepo.RegisterShift(employee.Id, shiftId, workDate);
                                if (ok) successCount++;
                            }
                        }
                    }
                }

                message = $"Đồng bộ thành công {successCount} lượt đăng ký ca làm lên Database!";
                return true;
            }
            catch (Exception ex)
            {
                message = "Lỗi xử lý file Excel: " + ex.Message;
                return false;
            }
        }

        private List<string> ParseNames(string cellValue)
        {
            List<string> names = new List<string>();
            if (string.IsNullOrWhiteSpace(cellValue)) return names;

            string[] parts = cellValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var p in parts)
            {
                string cleanName = p.Trim();
                if (!string.IsNullOrEmpty(cleanName))
                {
                    names.Add(cleanName);
                }
            }
            return names;
        }
    }
}
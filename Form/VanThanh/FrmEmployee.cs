using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmEmployee : Form
    {
        private ToolStripDropDown _actionDropDown;
        private UCEmployeeControl _ucEmployeeControl;
        private UCEmployeeInform _ucEmployeeInform;
        private UCEmployeeEdit _ucEmployeeEdit;
        private UCEmployeeAdd _ucEmployeeAdd;
        private int _selectedRowIndex = -1; //
        private bool _isLoading = false;
        private EmployeeService _employeeService = new EmployeeService();
        private ShiftService _shiftService = new ShiftService();
        private ShiftRepository _shiftRepo = new ShiftRepository();

        public FrmEmployee()
        {
            InitializeComponent();
        }

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            try
            {
                _isLoading = true;
                LoadRoleFilter();
                LoadMissionFilter();
                LoadEmployeeData();
                LoadDailyShifts();
                InitActionPopup();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi khởi tạo dữ liệu:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        // 1. Nạp danh sách vai trò vào ComboBox cboFilter (Phục vụ tìm kiếm 1.a)
        private void LoadRoleFilter()
        {
            // Nếu bạn vẫn cần dòng "Tất cả vai trò" ở đầu để lọc danh sách:
            var filterList = new List<RoleDTO>
            {
                 new RoleDTO(0, "Tất cả vai trò")
            };

            // Nếu bạn KHÔNG CẦN dòng "Tất cả vai trò" mà chỉ muốn load đúng 100% từ DB, hãy dùng:
            // var filterList = new List<RoleDTO>();

            var roles = _employeeService.GetAllRoles();
            if (roles != null && roles.Count > 0)
            {
                filterList.AddRange(roles);
            }

            cboFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilter.DisplayMember = "RoleName";
            cboFilter.ValueMember = "Id";
            cboFilter.DataSource = filterList;

            if (cboFilter.Items.Count > 0)
            {
                cboFilter.SelectedIndex = 0;
            }
        }

        // 2. Nạp danh sách nhiệm vụ vào ComboBox cboShiftMission (Phục vụ lọc ca làm 1.b)
        private void LoadMissionFilter()
        {
            var filterList = new List<RoleDTO>
            {
                new RoleDTO(0, "Tất cả nhiệm vụ")
            };

            try
            {
                var missions = _shiftService.GetAllMisions();
                if (missions != null && missions.Count > 0)
                {
                    foreach (var m in missions)
                    {
                        filterList.Add(new RoleDTO(m.Id, m.MissisonName));
                    }
                }
            }
            catch
            {
                filterList.Add(new RoleDTO(1, "Phục vụ"));
                filterList.Add(new RoleDTO(2, "Pha chế"));
                filterList.Add(new RoleDTO(3, "Thu ngân"));
            }

            cboShiftMission.DropDownStyle = ComboBoxStyle.DropDownList;
            cboShiftMission.DisplayMember = "RoleName";
            cboShiftMission.ValueMember = "Id";
            cboShiftMission.DataSource = filterList;
            cboShiftMission.SelectedIndex = 0;
        }

        // 3. Đăng ký các sự kiện tìm kiếm và lọc cho cả 2 vùng

        // 4. Hàm thực thi tìm kiếm nhân viên theo Tên / SĐT / ID kết hợp lọc theo vai trò
        private void FilterEmployees()
        {
            if (_isLoading) return;

            string keyword = txtSearch.Text.Trim();
            int selectedRoleId = 0;
            if (cboFilter.SelectedValue is int rId)
            {
                selectedRoleId = rId;
            }
            else if (cboFilter.SelectedItem is RoleDTO rDto)
            {
                selectedRoleId = rDto.Id;
            }

            List<EmployeeDTO> list = null;
            try
            {
                list = _employeeService.GetAllEmployees();
            }
            catch
            {
                list = new List<EmployeeDTO>();
            }

            if (list == null) list = new List<EmployeeDTO>();

            var filtered = list.Where(emp =>
            {
                // Lọc theo vai trò nếu được chọn cụ thể (RoleId > 0)
                if (selectedRoleId > 0 && emp.RoleId != selectedRoleId)
                    return false;

                // Tìm kiếm theo Họ tên, Số điện thoại hoặc ID
                if (!string.IsNullOrEmpty(keyword))
                {
                    bool matchId = emp.Id.ToString().Equals(keyword, StringComparison.OrdinalIgnoreCase)
                                || emp.Id.ToString().Contains(keyword);
                    bool matchName = !string.IsNullOrEmpty(emp.FullName)
                                  && emp.FullName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
                    bool matchPhone = !string.IsNullOrEmpty(emp.Phone)
                                   && emp.Phone.Contains(keyword);

                    return matchId || matchName || matchPhone;
                }

                return true;
            }).ToList();

            dgvEmployee.AutoGenerateColumns = false;
            dgvEmployee.DataSource = filtered;
        }

        // 5. Hàm tải và tìm kiếm ca làm trong ngày, lọc theo nhiệm vụ
        private void LoadDailyShifts()
        {
            if (_isLoading) return;

            DateTime targetDate = dtpShiftDate.Value.Date;
            int selectedMissionId = 0;
            if (cboShiftMission.SelectedValue is int mId)
            {
                selectedMissionId = mId;
            }
            else if (cboShiftMission.SelectedItem is RoleDTO mDto)
            {
                selectedMissionId = mDto.Id;
            }

            string shiftKeyword = txtSearchShift.Text.Trim();

            List<DailyShiftDTO> shifts = null;
            try
            {
                shifts = _shiftService.GetDailyShifts(targetDate, selectedMissionId, shiftKeyword);
            }
            catch
            {
                shifts = new List<DailyShiftDTO>();
            }

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = shifts;
        }

        private void LoadEmployeeData()
        {
            FilterEmployees();
        }
        private void InitActionPopup()
        {
            _ucEmployeeControl = new UCEmployeeControl();
            DataGridViewButtonColumn btnActionCol = dgvEmployee.Columns["Control"] as DataGridViewButtonColumn;
            if (btnActionCol != null)
            {
                btnActionCol.Text = "⚙️";
                btnActionCol.UseColumnTextForButtonValue = true;
            }
            // Gắn sự kiện khi bấm nút bên trong User Control
            _ucEmployeeControl.DetailClicked += (s, e) => {
                _actionDropDown.Close();
                ExecuteDetailAction(_selectedRowIndex);
            };

            _ucEmployeeControl.DeleteClicked += (s, e) => {
                _actionDropDown.Close();
                ExecuteDeleteAction(_selectedRowIndex);
            };

            // Đưa UserControl vào ToolStripDropDown để làm popup
            _actionDropDown = new ToolStripDropDown();
            ToolStripControlHost host = new ToolStripControlHost(_ucEmployeeControl);
            host.Margin = Padding.Empty;
            host.Padding = Padding.Empty;
            _actionDropDown.Items.Add(host);
        }
        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvEmployee.Columns[e.ColumnIndex].Name == "Control")
            {
                // Nếu popup đang mở VÀ người dùng bấm lại chính đúng dòng đó -> Ẩn đi
                if (_actionDropDown.Visible && _selectedRowIndex == e.RowIndex)
                {
                    _actionDropDown.Close();
                    _selectedRowIndex = -1; // Reset lại vị trí
                    return;
                }

                // Nếu bấm sang dòng khác hoặc popup đang đóng -> Mở lên bình thường
                _selectedRowIndex = e.RowIndex;

                // Lấy tọa độ của ô hiện tại trên màn hình
                Rectangle cellRect = dgvEmployee.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                Point point = new Point(cellRect.Right, cellRect.Top);

                if (_actionDropDown != null)
                {
                    _actionDropDown.Show(dgvEmployee, point);
                }
            }
        }
        private void dgvEmployee_Leave(object sender, EventArgs e)
        {
            dgvEmployee.ClearSelection();
        }
        private void ExecuteDetailAction(int rowIndex)
        {
            if (rowIndex < 0) return;

            // Lấy trực tiếp đối tượng EmployeeDTO của hàng đang chọn dựa trên DataSource
            DataGridViewRow row = dgvEmployee.Rows[rowIndex];
            EmployeeDTO selectedEmployee = row.DataBoundItem as EmployeeDTO;

            if (selectedEmployee != null)
            {
                if (pnlEmployeeDetail == null)
                {
                    MessageBox.Show("Vui lòng kiểm tra lại panel pnlEmployeeDetail trên Form!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                pnlEmployeeDetail.Controls.Clear();

                // 3. Khởi tạo User Control chi tiết
                _ucEmployeeInform = new UCEmployeeInform();
                _ucEmployeeInform.Dock = DockStyle.Fill;
                _ucEmployeeInform.BindData(selectedEmployee);

                // ==========================================
                // THÊM ĐOẠN LẮNG NGHE SỰ KIỆN SỬA THÔNG TIN NÀY:
                // ==========================================
                _ucEmployeeInform.EditClicked += (sender, empToEdit) => {
                    pnlEmployeeDetail.Controls.Clear();

                    // Khởi tạo UC Sửa thông tin
                    _ucEmployeeEdit = new UCEmployeeEdit();
                    _ucEmployeeEdit.Dock = DockStyle.Fill;

                    // Đẩy dữ liệu nhân viên hiện tại sang UC Sửa
                    _ucEmployeeEdit.SetEmployeeData(empToEdit);

                    // Khi sửa và lưu thành công
                    _ucEmployeeEdit.SaveCompleted += (s, ev) => {
                        pnlEmployeeDetail.Controls.Clear();
                        pnlEmployeeDetail.Visible = false;
                        LoadEmployeeData(); // Load lại DataGridView chính
                        MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    };

                    // Khi bấm nút Hủy ở form Sửa
                    _ucEmployeeEdit.CancelClicked += (s, ev) => {
                        pnlEmployeeDetail.Controls.Clear();
                        pnlEmployeeDetail.Visible = false;
                    };

                    pnlEmployeeDetail.Controls.Add(_ucEmployeeEdit);
                    _ucEmployeeEdit.BringToFront();
                };
                // ==========================================

                // Xử lý sự kiện đóng màn hình chi tiết
                _ucEmployeeInform.CloseClicked += (s, e) =>
                {
                    pnlEmployeeDetail.Controls.Clear();
                    pnlEmployeeDetail.Visible = false;
                };

                pnlEmployeeDetail.Controls.Add(_ucEmployeeInform);
                _ucEmployeeInform.BringToFront();
                pnlEmployeeDetail.BringToFront();
                pnlEmployeeDetail.Visible = true;
            }
        }
        private void ExecuteDeleteAction(int rowIndex)
        {
            if (rowIndex < 0) return;

            // Lấy đối tượng EmployeeDTO của dòng đang chọn
            DataGridViewRow row = dgvEmployee.Rows[rowIndex];
            EmployeeDTO selectedEmployee = row.DataBoundItem as EmployeeDTO;

            if (selectedEmployee != null)
            {
                // Hiển thị hộp thoại xác nhận trước khi xóa
                DialogResult confirm = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa nhân viên: {selectedEmployee.FullName} (ID: {selectedEmployee.Id})?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        bool success = _employeeService.DeleteEmployee(selectedEmployee.Id, out string message);

                        if (success)
                        {
                            MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Nếu panel chi tiết đang hiển thị đúng nhân viên vừa bị xóa thì ẩn/dọn đi
                            if (pnlEmployeeDetail != null)
                            {
                                pnlEmployeeDetail.Controls.Clear();
                                pnlEmployeeDetail.Visible = false;
                            }

                            // Tải lại dữ liệu cho DataGridView
                            LoadEmployeeData();
                        }
                        else
                        {
                            MessageBox.Show(message ?? "Xóa nhân viên thất bại! Vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa nhân viên:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (pnlEmployeeDetail == null)
            {
                MessageBox.Show("Vui lòng kiểm tra lại panel pnlEmployeeDetail trên Form!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Dọn dẹp sạch các control cũ đang có trên panel
            pnlEmployeeDetail.Controls.Clear();

            // 2. Khởi tạo User Control thêm mới nhân viên
            _ucEmployeeAdd = new UCEmployeeAdd();
            _ucEmployeeAdd.Dock = DockStyle.Fill; // Trải đầy khung Panel

            // 3. Lắng nghe sự kiện khi thêm thành công từ UC
            _ucEmployeeAdd.SaveCompleted += (s, ev) => {
                pnlEmployeeDetail.Controls.Clear();
                pnlEmployeeDetail.Visible = false;  // Ẩn panel đi sau khi thêm xong
                LoadEmployeeData();                 // Tải lại DataGridView để hiện nhân viên mới
                MessageBox.Show("Thêm nhân viên mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            // 4. Lắng nghe sự kiện khi bấm nút Hủy / Thoát từ UC
            _ucEmployeeAdd.CancelClicked += (s, ev) => {
                pnlEmployeeDetail.Controls.Clear();
                pnlEmployeeDetail.Visible = false;
            };

            // 5. Thêm UC vào Panel và đưa lên lớp trên cùng
            pnlEmployeeDetail.Controls.Add(_ucEmployeeAdd);
            _ucEmployeeAdd.BringToFront();
            pnlEmployeeDetail.Visible = true;
        }

        private void btnRegisterShift_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime today = DateTime.Today;

                // Tính số ngày để nhảy tới Thứ Hai tuần sau
                int daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
                if (daysUntilNextMonday == 0) daysUntilNextMonday = 7; // Nếu hôm nay là Thứ Hai, lấy Thứ Hai tuần sau

                DateTime nextMonday = today.AddDays(daysUntilNextMonday).Date;
                DateTime nextSunday = nextMonday.AddDays(6).Date; // Chủ Nhật của tuần sau

                // 1. Tự động khởi tạo lịch tuần tới (63 bản ghi phân tách theo nhiệm vụ) vào Database nếu chưa có sẵn
                _shiftRepo.InitializeWeekShiftsIfNeeded(nextMonday, nextSunday);

                // 2. Mở form quản lý lịch làm việc
                FrmShiftSchedule frm = new FrmShiftSchedule();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khởi tạo lịch: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện tìm kiếm nhân viên
        private void btnSearch_Click(object sender, EventArgs e)
        {
            FilterEmployees(); // Gọi trực tiếp hàm xử lý
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterEmployees();
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterEmployees();
        }

        // Sự kiện tìm kiếm ca làm việc
        private void btnSearchShift_Click(object sender, EventArgs e)
        {
            LoadDailyShifts();
        }

        private void cboShiftMission_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDailyShifts();
        }

        private void dtpShiftDate_ValueChanged(object sender, EventArgs e)
        {
            LoadDailyShifts();
        }
        private void txtSearchShift_TextChanged(object sender, EventArgs e)
        {
            LoadDailyShifts(); // Gọi trực tiếp hàm tải ca làm
        }
    }
}

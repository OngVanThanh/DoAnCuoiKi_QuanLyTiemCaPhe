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
        private int _selectedRowIndex = -1; //
        private EmployeeService _employeeService = new EmployeeService();
        private ShiftService _shiftService = new ShiftService();
        public FrmEmployee()
        {
            InitializeComponent();
        }
        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            LoadEmployeeData();
            InitActionPopup();
        }
        private void LoadEmployeeData()
        {
            dgvEmployee.AutoGenerateColumns = false;
            dgvEmployee.DataSource = _employeeService.GetAllEmployees();
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
                // 1. Kiểm tra xem panel chứa đã được tạo trên form chưa
                if (pnlEmployeeDetail == null)
                {
                    MessageBox.Show("Vui lòng kéo một Panel tên 'panelDetailContainer' lên Form thiết kế trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Xóa các control cũ bên trong panel (nếu trước đó đã xem nhân viên khác)
                pnlEmployeeDetail.Controls.Clear();

                // 3. Khởi tạo User Control chi tiết
                _ucEmployeeInform = new UCEmployeeInform();
                _ucEmployeeInform.Dock = DockStyle.Fill; // Trải đầy panel

                // 4. Đổ dữ liệu ứng với dòng được chọn vào User Control
                _ucEmployeeInform.BindData(selectedEmployee);

                // 5. Add User Control vào Panel và đưa lên lớp trên cùng hiển thị
                pnlEmployeeDetail.Controls.Add(_ucEmployeeInform);
                _ucEmployeeInform.BringToFront(); // Đưa User Control lên vị trí hiển thị trên cùng (Top)

                // (Tùy chọn) Đảm bảo Panel cũng được hiện lên nếu trước đó bạn để ẩn
                pnlEmployeeDetail.BringToFront();
                pnlEmployeeDetail.Visible = true;
                _ucEmployeeInform.CloseClicked += (s, e) =>
                {
                    pnlEmployeeDetail.Controls.Clear(); // Xóa UC khỏi Panel
                    pnlEmployeeDetail.Visible = false;  // Ẩn panel đi (hoặc giữ nguyên tùy ý bạn)
                };
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
                    bool success = _employeeService.DeleteEmployee(selectedEmployee.Id);

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
                        MessageBox.Show("Xóa nhân viên thất bại! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            _ucEmployeeEdit = new UCEmployeeEdit();
            _ucEmployeeEdit.Dock = DockStyle.Fill; // Trải đầy khung Panel

            // 3. Lắng nghe sự kiện khi thêm thành công từ UC
            _ucEmployeeEdit.SaveCompleted += (s, ev) => {
                pnlEmployeeDetail.Controls.Clear();
                pnlEmployeeDetail.Visible = false;  // Ẩn panel đi sau khi thêm xong
                LoadEmployeeData();                 // Tải lại DataGridView để hiện nhân viên mới
                MessageBox.Show("Thêm nhân viên mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            // 4. Lắng nghe sự kiện khi bấm nút Hủy / Thoát từ UC
            _ucEmployeeEdit.CancelClicked += (s, ev) => {
                pnlEmployeeDetail.Controls.Clear();
                pnlEmployeeDetail.Visible = false;
            };

            // 5. Thêm UC vào Panel và đưa lên lớp trên cùng
            pnlEmployeeDetail.Controls.Add(_ucEmployeeEdit);
            _ucEmployeeEdit.BringToFront();
            pnlEmployeeDetail.Visible = true;
        }

        private void btnRegisterShift_Click(object sender, EventArgs e)
        {
            // Khởi tạo form đăng ký ca làm
            FrmShiftSchedule frm = new FrmShiftSchedule();

            // Mở form dưới dạng hộp thoại (người dùng phải đóng form này mới thao tác tiếp được form chính)
            frm.ShowDialog();
        }
    }
}

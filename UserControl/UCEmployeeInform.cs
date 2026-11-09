using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class UCEmployeeInform : UserControl
    {
        public event EventHandler CloseClicked;

        // Khai báo thêm sự kiện để báo cho form cha biết khi người dùng bấm nút Sửa
        public event EventHandler<EmployeeDTO> EditClicked;

        private EmployeeDTO _currentEmployee; // Biến lưu trữ thông tin nhân viên đang hiển thị

        public UCEmployeeInform()
        {
            InitializeComponent();
        }

        public void BindData(EmployeeDTO emp)
        {
            if (emp == null) return;

            // Lưu lại đối tượng nhân viên hiện tại để phục vụ việc chuyển qua form sửa
            _currentEmployee = emp;

            // Hiển thị Avatar từ đường dẫn chuỗi (ImgPath kiểu string)
            try
            {
                if (!string.IsNullOrEmpty(emp.Image) && System.IO.File.Exists(emp.Image))
                {
                    ptbAvatar.Image = Image.FromFile(emp.Image);
                }
                else
                {
                    ptbAvatar.Image = null;
                }
                ptbAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception)
            {
                ptbAvatar.Image = null;
            }

            lblIdDetail.Text = emp.Id.ToString();
            lblFullNameDetail.Text = emp.FullName;
            lblPhoneDetail.Text = emp.Phone;
            lblBirthDateDetail.Text = emp.BirthDate.HasValue ? emp.BirthDate.Value.ToString("dd/MM/yyyy") : "";
            lblAddressDetail.Text = emp.Address;
            lblPositionDetail.Text = emp.Position;

            // Lấy thông tin lương và đơn vị tính từ bảng Salaries
            EmployeeRepository empRepo = new EmployeeRepository();
            var salaryInfo = empRepo.GetSalaryInfoByRoleId(emp.RoleId);

            lblSalaryDetail.Text = $"{salaryInfo.Amount:N0} VNĐ / {salaryInfo.TimeUnit}";
            lblHireDateDetail.Text = emp.HireDate.HasValue ? emp.HireDate.Value.ToString("dd/MM/yyyy") : "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            CloseClicked?.Invoke(this, EventArgs.Empty);
        }

        // Sự kiện khi người dùng nhấn nút Sửa trên giao diện chi tiết
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_currentEmployee != null)
            {
                // Kích hoạt sự kiện EditClicked và truyền đối tượng nhân viên hiện tại sang cho Form cha xử lý
                EditClicked?.Invoke(this, _currentEmployee);
            }
        }
    }
}
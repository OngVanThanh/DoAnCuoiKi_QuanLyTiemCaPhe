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
    public partial class UCEmployeeInform : UserControl
    {
        public event EventHandler CloseClicked;
        public UCEmployeeInform()
        {
            InitializeComponent();
        }
        public void BindData(EmployeeDTO emp)
        {
            if (emp == null) return;
            // Hiển thị Avatar từ đường dẫn chuỗi (ImgPath kiểu string)
            try
            {
                // Kiểm tra xem đường dẫn có dữ liệu và file thực sự tồn tại trên máy không
                if (!string.IsNullOrEmpty(emp.Image) && System.IO.File.Exists(emp.Image))
                {
                    ptbAvatar.Image = Image.FromFile(emp.Image);
                }
                else
                {
                    ptbAvatar.Image = null; // Không có ảnh hoặc không tìm thấy file
                }
                ptbAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception)
            {
                ptbAvatar.Image = null; // Phòng trường hợp file bị lỗi định dạng hoặc đang bị khóa
            }
            lblIdDetail.Text = emp.Id.ToString();
            lblFullNameDetail.Text = emp.FullName;
            lblPhoneDetail.Text = emp.Phone;
            lblBirthDateDetail.Text = emp.BirthDate.HasValue ? emp.BirthDate.Value.ToString("dd/MM/yyyy") : "";
            lblAddressDetail.Text = emp.Address;
            lblPositionDetail.Text = emp.Position;
            lblSalaryDetail.Text = $"{emp.Salary:N0} VNĐ";
            lblHireDateDetail.Text = emp.HireDate.HasValue ? emp.HireDate.Value.ToString("dd/MM/yyyy") : "";

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            CloseClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}

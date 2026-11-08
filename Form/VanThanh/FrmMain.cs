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
    public partial class FrmMain : Form
    {
        private User _currentUser;
        private Role _currentRole;
        private Timer _systemTimer;
        public FrmMain(User user)
        {
            InitializeComponent();
            _currentUser = user;
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {

            // 1. Hiển thị thông tin người dùng và quyền lên StatusStrip
            if (_currentUser != null)
            {
                lblCurrentUser.Text = $"Người dùng: {_currentUser.Username}";
                var userRepository = new UserRepository();
                _currentRole = userRepository.GetRoleById(_currentUser.RoleId);

                lblCurrentRole.Text = _currentRole != null ? $"Vai trò: {_currentRole.RoleName}" : "Vai trò: Chưa xác định";
                ApplyPermisisonsToMenu();
            }
            lblSystemTime.Text = $"Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
            // 2. Khởi tạo đồng hồ hệ thống để hiển thị thời gian thực trên StatusStrip
            _systemTimer = new Timer();
            _systemTimer.Interval = 1000; // Cập nhật mỗi giây
            _systemTimer.Tick += (s, args) =>
            {
                lblSystemTime.Text = $"Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";

            };
            _systemTimer.Start();
            this.BeginInvoke((MethodInvoker)delegate {
                FrmOrder frmOrder = new FrmOrder();
                OpenChildForm(frmOrder);
            });
        }
        private void OpenChildForm(Form childForm)
        {
            pnlContent.Controls.Clear();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(childForm);
            childForm.Show();
        }
        private  void ApplyPermisisonsToMenu()
        {
            int roleId = _currentUser.RoleId;
            if (roleId == 1) // Admin
            {
                menuSystem.Visible = true;
                menuCategory.Visible = true;
                menuBusiness.Visible = true;
                menuReport.Visible = true;
                menuPermission.Visible = true;
            }
            else if (roleId == 2)
            {
                menuItemAppConfig.Visible = false;
                menuItemCategory.Visible = false;
                menuItemProduct.Visible = false;
                menuItemEmployee.Visible = false;
                menuItemInventory.Visible = false;
                menuPermission.Visible = false;
            }
            else if (roleId == 3) // Staff
            {
                menuItemAppConfig.Visible = false;
                menuCategory.Visible = false;
                menuItemPromotion.Visible = false;
                menuReport.Visible = false;
                menuPermission.Visible = false;
            }
        }
        private void menuItemAppConfig_Click(object sender, EventArgs e)
        {
            FrmAppConfig frm = new FrmAppConfig();
            frm.ShowDialog();
        }

        private void menuItemLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.ShowDialog();
            this.Close();
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItemProduct_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmProduct());
        }

        private void menuItemCategory_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmCategory());
        }

        private void menuItemEmployee_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmEmployee());
        }

        private void menuItemTable_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmTable());
        }

        private void menuItemCustomer_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmCustomer());
        }

        private void menuItemOrder_Click(object sender, EventArgs e)
        {
            FrmOrder frm = new FrmOrder();
            frm.ShowDialog();
        }

        private void menuItemInventory_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmInventory());
        }

        private void menuItemPromotion_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmPromotion());
        }

        private void menuItemReprotWord_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmReportWord());
        }

        private void menuItemChart_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmChart());
        }

        private void menuItemPermission_Click(object sender, EventArgs e)
        {
            FrmPermission frm = new FrmPermission();
            frm.ShowDialog();
        }
    }
}

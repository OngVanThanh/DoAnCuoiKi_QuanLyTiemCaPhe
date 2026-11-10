using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmMain : Form
    {
        private readonly User _currentUser;
        private Role _currentRole;

        public FrmMain() : this(null)
        {
        }

        public FrmMain(User user)
        {
            InitializeComponent();
            _currentUser = user;
            if (user != null)
            {
                UserSession.Login(user);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (_currentUser != null)
            {
                lblCurrentUser.Text = $"Người dùng: {_currentUser.Username}";
                var userRepository = new UserRepository();
                _currentRole = userRepository.GetRoleById(_currentUser.RoleId);
                lblCurrentRole.Text = _currentRole != null ? $"Vai trò: {_currentRole.RoleName}" : "Vai trò: Nhân viên";
                ApplyPermissionsToSidebar();
            }

            lblSystemTime.Text = $"Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";

            // Mở sẵn màn hình POS Bán hàng & Đặt bàn khi vừa đăng nhập
            this.BeginInvoke((MethodInvoker)delegate
            {
                SetActiveSidebarButton(btnNavOrder);
                OpenChildForm(new FrmOrder());
            });
        }

        private void tmrClock_Tick(object sender, EventArgs e)
        {
            lblSystemTime.Text = $"Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
        }

        private void SetActiveSidebarButton(Button activeBtn)
        {
            foreach (Control ctrl in pnlNavButtons.Controls)
            {
                if (ctrl is Button btn)
                {
                    if (btn == activeBtn)
                    {
                        btn.BackColor = Color.FromArgb(111, 78, 55);
                        btn.ForeColor = Color.White;
                        btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    }
                    else
                    {
                        btn.BackColor = Color.FromArgb(48, 29, 26);
                        btn.ForeColor = Color.FromArgb(240, 232, 225);
                        btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
                    }
                }
            }
        }

        private void OpenChildForm(Form childForm)
        {
            foreach (Control ctrl in pnlContent.Controls)
            {
                if (ctrl is Form oldForm)
                {
                    oldForm.Close();
                    oldForm.Dispose();
                }
            }

            pnlContent.Controls.Clear();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(childForm);
            childForm.Show();
        }

        private void ApplyPermissionsToSidebar()
        {
            int roleId = _currentUser != null ? _currentUser.RoleId : 1;

            if (roleId == 1) // Quản lý / Admin: toàn quyền
            {
                return;
            }
            else if (roleId == 2) // Thu ngân: Bán hàng, Công thức, Khách hàng, Khuyến mãi, Thống kê & Báo cáo
            {
                btnNavProduct.Visible = false;
                btnNavCategory.Visible = false;
                lblGroupAdmin.Visible = false;
                btnNavEmployee.Visible = false;
                btnNavPermission.Visible = false;
                btnNavAppConfig.Visible = false;
            }
            else // Pha chế / Phục vụ: POS Bán hàng, Công thức Barista, Khách hàng
            {
                lblGroupCatalog.Visible = false;
                btnNavProduct.Visible = false;
                btnNavCategory.Visible = false;
                btnNavPromotion.Visible = false;
                lblGroupReport.Visible = false;
                btnNavChart.Visible = false;
                btnNavReport.Visible = false;
                lblGroupAdmin.Visible = false;
                btnNavEmployee.Visible = false;
                btnNavPermission.Visible = false;
                btnNavAppConfig.Visible = false;
            }
        }

        private void btnNavOrder_Click(object sender, EventArgs e)
        {
            SetActiveSidebarButton(btnNavOrder);
            OpenChildForm(new FrmOrder());
        }

        private void btnNavRecipe_Click(object sender, EventArgs e)
        {
            SetActiveSidebarButton(btnNavRecipe);
            OpenChildForm(new FrmRecipe());
        }

        private void btnNavCustomer_Click(object sender, EventArgs e)
        {
            SetActiveSidebarButton(btnNavCustomer);
            OpenChildForm(new FrmCustomer());
        }

        private void btnNavProduct_Click(object sender, EventArgs e)
        {
            SetActiveSidebarButton(btnNavProduct);
            OpenChildForm(new FrmProduct());
        }

        private void btnNavCategory_Click(object sender, EventArgs e)
        {
            SetActiveSidebarButton(btnNavCategory);
            OpenChildForm(new FrmCategory());
        }

        private void btnNavPromotion_Click(object sender, EventArgs e)
        {
            SetActiveSidebarButton(btnNavPromotion);
            OpenChildForm(new FrmPromotion());
        }

        private void btnNavChart_Click(object sender, EventArgs e)
        {
            SetActiveSidebarButton(btnNavChart);
            OpenChildForm(new FrmChart());
        }

        private void btnNavReport_Click(object sender, EventArgs e)
        {
            SetActiveSidebarButton(btnNavReport);
            OpenChildForm(new FrmReportWord());
        }

        private void btnNavEmployee_Click(object sender, EventArgs e)
        {
            SetActiveSidebarButton(btnNavEmployee);
            OpenChildForm(new FrmEmployee());
        }

        private void btnNavPermission_Click(object sender, EventArgs e)
        {
            SetActiveSidebarButton(btnNavPermission);
            OpenChildForm(new FrmPermission());
        }

        private void btnNavAppConfig_Click(object sender, EventArgs e)
        {
            SetActiveSidebarButton(btnNavAppConfig);
            using (FrmAppConfig frm = new FrmAppConfig())
            {
                frm.ShowDialog(this);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            UserSession.Logout();
            this.Hide();
            using (FrmLogin frmLogin = new FrmLogin())
            {
                frmLogin.ShowDialog();
            }
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
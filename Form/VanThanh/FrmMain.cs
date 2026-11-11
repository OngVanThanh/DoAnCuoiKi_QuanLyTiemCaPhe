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

        public void LoadBrandIdentity()
        {
            string shopName = ConfigHelper.GetShopName();
            string shopSlogan = ConfigHelper.GetShopSlogan();

            this.Text = $"{shopSlogan} — {shopName}";
            lblBrandName.Text = $"☕ {shopName.ToUpper()}";
            lblBrandSub.Text = shopSlogan;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadBrandIdentity();

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
            if (_currentUser == null) return;

            // Kiểm tra dựa trên tên vai trò (_currentRole đã được lấy ở FrmMain_Load)
            string roleName = _currentRole != null ? _currentRole.RoleName.ToLower() : "";

            // Nếu là Quản lý hoặc Admin thì giữ nguyên toàn quyền
            if (roleName.Contains("Quản lý") || roleName.Contains("admin") || _currentUser.RoleId == 1)
            {
                return;
            }
            else // Nếu là nhân viên, thu ngân, phục vụ,... ẩn các tính năng quản trị hệ thống
            {
                if (btnNavProduct != null) btnNavProduct.Visible = false;
                if (btnNavCategory != null) btnNavCategory.Visible = false;
                if (lblGroupAdmin != null) lblGroupAdmin.Visible = false;
                if (btnNavEmployee != null) btnNavEmployee.Visible = false;
                if (btnNavPermission != null) btnNavPermission.Visible = false;
                if (btnNavAppConfig != null) btnNavAppConfig.Visible = false;
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
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadBrandIdentity();
                }
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

        private void menuItemChangePassWord_Click(object sender, EventArgs e)
        {
            FrmChangePassword frm = new FrmChangePassword(_currentUser.Username);
            frm.ShowDialog();
        }
    }
}
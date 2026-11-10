using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmAppConfig : Form
    {
        private AppConfigService _configService = new AppConfigService();

        public FrmAppConfig()
        {
            InitializeComponent();
        }

        private void FrmAppConfig_Load(object sender, EventArgs e)
        {
            LoadCurrentConfig();
        }

        // 1. Tải các cấu hình hiện tại từ App.config lên giao diện
        private void LoadCurrentConfig()
        {
            try
            {
                // Lấy chuỗi kết nối hiện tại (Giả sử tên connection string trong App.config là "CoffeeShopDB")
                string connString = _configService.GetConnectionString("CoffeeShopDB");

                if (!string.IsNullOrEmpty(connString))
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connString);
                    txtServer.Text = builder.DataSource;
                    txtDatabase.Text = builder.InitialCatalog;
                    chkIntegratedSecurity.Checked = builder.IntegratedSecurity;

                    if (!builder.IntegratedSecurity)
                    {
                        txtUsername.Text = builder.UserID;
                        txtPassword.Text = builder.Password;
                    }
                }

                // Tải thông tin quán từ appSettings
                txtShopName.Text = _configService.GetSetting("ShopName");
                txtShopAddress.Text = _configService.GetSetting("ShopAddress");
                txtShopPhone.Text = _configService.GetSetting("ShopPhone");
                txtVAT.Text = _configService.GetSetting("VATRate");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải cấu hình: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. Xử lý bật/tắt ô nhập tài khoản SQL khi chọn Windows Authentication
        private void chkIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
        {
            bool isWindowsAuth = chkIntegratedSecurity.Checked;
            txtUsername.Enabled = !isWindowsAuth;
            txtPassword.Enabled = !isWindowsAuth;

            if (isWindowsAuth)
            {
                txtUsername.Clear();
                txtPassword.Clear();
            }
        }

        // 3. Kiểm tra kết nối CSDL (btnTestConnection)
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            string connectionString = BuildConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    MessageBox.Show("Kết nối cơ sở dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối thất bại! Vui lòng kiểm tra lại thông tin.\nChi tiết lỗi: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 4. Lưu cấu hình vào file App.config (btnSaveConfig)
        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo chuỗi kết nối mới từ giao diện
                string newConnString = BuildConnectionString();

                // Lưu thông tin vào App.config (Bạn cần cấu hình chuỗi kết nối trong ConnectionStrings hoặc AppSettings tùy theo cách dùng của dự án)
                // Ở đây ta lưu thông tin quán vào AppSettings
                _configService.UpdateAppSetting("ShopName", txtShopName.Text.Trim());
                _configService.UpdateAppSetting("ShopAddress", txtShopAddress.Text.Trim());
                _configService.UpdateAppSetting("ShopPhone", txtShopPhone.Text.Trim());
                _configService.UpdateAppSetting("VATRate", txtVAT.Text.Trim());

                MessageBox.Show("Lưu cấu hình hệ thống thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lưu cấu hình thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm hỗ trợ tự động dựng chuỗi kết nối từ các TextBox
        private string BuildConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = txtServer.Text.Trim();
            builder.InitialCatalog = txtDatabase.Text.Trim();
            builder.IntegratedSecurity = chkIntegratedSecurity.Checked;

            if (!chkIntegratedSecurity.Checked)
            {
                builder.UserID = txtUsername.Text.Trim();
                builder.Password = txtPassword.Text;
            }

            return builder.ConnectionString;
        }

        // Nút Hủy / Thoát form
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
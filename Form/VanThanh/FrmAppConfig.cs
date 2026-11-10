using DoAnCuoiKi_QuanLyTiemCaPhe.BLL;
using DoAnCuoiKi_QuanLyTiemCaPhe.DAL;
using DoAnCuoiKi_QuanLyTiemCaPhe.Utilities;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    public partial class FrmAppConfig : Form
    {
        private readonly AppConfigService _configService = new AppConfigService();

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
                // Tab 1: Bộ nhận diện thương hiệu
                txtShopName.Text = ConfigHelper.GetShopName();
                txtShopSlogan.Text = ConfigHelper.GetShopSlogan();
                txtShopAddress.Text = ConfigHelper.GetShopAddress();
                txtShopPhone.Text = ConfigHelper.GetShopPhone();
                txtVAT.Text = ConfigHelper.GetVATRate().ToString("0.##");
                txtWifiInfo.Text = ConfigHelper.GetWifiInfo();

                // Tab 2: Chuỗi kết nối CSDL
                string connString = _configService.GetConnectionString("CoffeeShopDB");
                if (!string.IsNullOrEmpty(connString))
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connString);
                    txtServer.Text = builder.DataSource;
                    txtDatabase.Text = builder.InitialCatalog;
                    chkIntegratedSecurity.Checked = builder.IntegratedSecurity;
                    chkTrustServerCertificate.Checked = builder.TrustServerCertificate;

                    if (!builder.IntegratedSecurity)
                    {
                        txtUsername.Text = builder.UserID;
                        txtPassword.Text = builder.Password;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải cấu hình: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. Lưu bộ nhận diện thương hiệu (Độc lập, TUYỆT ĐỐI không chạm vào connectionStrings)
        private void btnSaveBrand_Click(object sender, EventArgs e)
        {
            try
            {
                string shopName = txtShopName.Text.Trim();
                if (string.IsNullOrWhiteSpace(shopName))
                {
                    MessageBox.Show("Vui lòng nhập tên thương hiệu / quán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtShopName.Focus();
                    return;
                }

                _configService.UpdateAppSetting("ShopName", shopName);
                _configService.UpdateAppSetting("ShopSlogan", txtShopSlogan.Text.Trim());
                _configService.UpdateAppSetting("ShopAddress", txtShopAddress.Text.Trim());
                _configService.UpdateAppSetting("ShopPhone", txtShopPhone.Text.Trim());
                _configService.UpdateAppSetting("VATPercent", txtVAT.Text.Trim());
                _configService.UpdateAppSetting("VATRate", txtVAT.Text.Trim());
                _configService.UpdateAppSetting("WifiInfo", txtWifiInfo.Text.Trim());

                MessageBox.Show("Lưu Bộ nhận diện thương hiệu thành công!\nHệ thống sẽ tự động cập nhật ngay trên giao diện.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lưu nhận diện thương hiệu thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. Xử lý bật/tắt ô nhập tài khoản SQL khi chọn Windows Authentication
        private void chkIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
        {
            bool isWindowsAuth = chkIntegratedSecurity.Checked;
            txtUsername.Enabled = !isWindowsAuth;
            txtPassword.Enabled = !isWindowsAuth;
            lblUsernameSql.Enabled = !isWindowsAuth;
            lblPasswordSql.Enabled = !isWindowsAuth;

            if (isWindowsAuth)
            {
                txtUsername.Clear();
                txtPassword.Clear();
            }
        }

        // 4. Thử kết nối CSDL (btnTestConnection)
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            string connectionString = BuildConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    MessageBox.Show("Kết nối máy chủ cơ sở dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối thất bại! Vui lòng kiểm tra lại thông tin máy chủ hoặc quyền truy cập.\nChi tiết lỗi: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 5. Khởi tạo / Đồng bộ bảng CSDL tự động nếu thiếu bảng Areas hoặc Tables
        private void btnSyncSchema_Click(object sender, EventArgs e)
        {
            if (DatabaseMigrationHelper.EnsureDatabaseSchema(out string message))
            {
                MessageBox.Show(message, "Đồng bộ thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(message, "Đồng bộ thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // 6. Lưu cấu hình CSDL (Có hộp thoại xác nhận rõ ràng để bảo vệ)
        private void btnSaveDatabase_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thay đổi chuỗi kết nối CSDL?\n(Ứng dụng sẽ chuyển hướng truy vấn sang máy chủ/CSDL này)", "Xác nhận đổi CSDL", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                string newConnString = BuildConnectionString();

                _configService.UpdateConnectionString("CoffeeShopDB", newConnString);
                _configService.UpdateConnectionString("DoAnCuoiKi_QuanLyTiemCaPhe.Properties.Settings.CoffeeShopDBConnectionString", newConnString);
                _configService.UpdateConnectionString("DoAnCuoiKi_QuanLyTiemCaPhe.Properties.Settings.CoffeeShopDBConnectionString1", newConnString);

                MessageBox.Show("Lưu cấu hình kết nối CSDL thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lưu cấu hình CSDL thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tự động dựng chuỗi kết nối đảm bảo luôn có TrustServerCertificate
        private string BuildConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = string.IsNullOrWhiteSpace(txtServer.Text) ? "." : txtServer.Text.Trim();
            builder.InitialCatalog = string.IsNullOrWhiteSpace(txtDatabase.Text) ? "CoffeeShopDB" : txtDatabase.Text.Trim();
            builder.IntegratedSecurity = chkIntegratedSecurity.Checked;
            builder.TrustServerCertificate = chkTrustServerCertificate.Checked;
            builder.ConnectTimeout = 3;

            if (!chkIntegratedSecurity.Checked)
            {
                builder.UserID = txtUsername.Text.Trim();
                builder.Password = txtPassword.Text;
            }

            return builder.ConnectionString;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

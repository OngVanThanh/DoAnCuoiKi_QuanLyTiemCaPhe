namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmAppConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabAppConfig = new System.Windows.Forms.TabControl();
            this.tabBrand = new System.Windows.Forms.TabPage();
            this.grpShopInfo = new System.Windows.Forms.GroupBox();
            this.btnSaveBrand = new System.Windows.Forms.Button();
            this.txtWifiInfo = new System.Windows.Forms.TextBox();
            this.lblWifi = new System.Windows.Forms.Label();
            this.txtVAT = new System.Windows.Forms.TextBox();
            this.lblVAT = new System.Windows.Forms.Label();
            this.txtShopPhone = new System.Windows.Forms.TextBox();
            this.lblSdt = new System.Windows.Forms.Label();
            this.txtShopAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtShopSlogan = new System.Windows.Forms.TextBox();
            this.lblShopSlogan = new System.Windows.Forms.Label();
            this.txtShopName = new System.Windows.Forms.TextBox();
            this.lblShopname = new System.Windows.Forms.Label();
            this.tabDatabase = new System.Windows.Forms.TabPage();
            this.grpDatabase = new System.Windows.Forms.GroupBox();
            this.btnSaveDatabase = new System.Windows.Forms.Button();
            this.btnSyncSchema = new System.Windows.Forms.Button();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.chkTrustServerCertificate = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPasswordSql = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsernameSql = new System.Windows.Forms.Label();
            this.chkIntegratedSecurity = new System.Windows.Forms.CheckBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.tabAppConfig.SuspendLayout();
            this.tabBrand.SuspendLayout();
            this.grpShopInfo.SuspendLayout();
            this.tabDatabase.SuspendLayout();
            this.grpDatabase.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabAppConfig
            // 
            this.tabAppConfig.Controls.Add(this.tabBrand);
            this.tabAppConfig.Controls.Add(this.tabDatabase);
            this.tabAppConfig.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.tabAppConfig.Location = new System.Drawing.Point(16, 16);
            this.tabAppConfig.Name = "tabAppConfig";
            this.tabAppConfig.SelectedIndex = 0;
            this.tabAppConfig.Size = new System.Drawing.Size(600, 420);
            this.tabAppConfig.TabIndex = 0;
            // 
            // tabBrand
            // 
            this.tabBrand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(248)))), ((int)(((byte)(245)))));
            this.tabBrand.Controls.Add(this.grpShopInfo);
            this.tabBrand.Location = new System.Drawing.Point(4, 30);
            this.tabBrand.Name = "tabBrand";
            this.tabBrand.Padding = new System.Windows.Forms.Padding(12);
            this.tabBrand.Size = new System.Drawing.Size(592, 386);
            this.tabBrand.TabIndex = 0;
            this.tabBrand.Text = "☕  Bộ nhận diện thương hiệu";
            // 
            // grpShopInfo
            // 
            this.grpShopInfo.BackColor = System.Drawing.Color.White;
            this.grpShopInfo.Controls.Add(this.btnSaveBrand);
            this.grpShopInfo.Controls.Add(this.txtWifiInfo);
            this.grpShopInfo.Controls.Add(this.lblWifi);
            this.grpShopInfo.Controls.Add(this.txtVAT);
            this.grpShopInfo.Controls.Add(this.lblVAT);
            this.grpShopInfo.Controls.Add(this.txtShopPhone);
            this.grpShopInfo.Controls.Add(this.lblSdt);
            this.grpShopInfo.Controls.Add(this.txtShopAddress);
            this.grpShopInfo.Controls.Add(this.lblAddress);
            this.grpShopInfo.Controls.Add(this.txtShopSlogan);
            this.grpShopInfo.Controls.Add(this.lblShopSlogan);
            this.grpShopInfo.Controls.Add(this.txtShopName);
            this.grpShopInfo.Controls.Add(this.lblShopname);
            this.grpShopInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpShopInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpShopInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(52)))), ((int)(((byte)(46)))));
            this.grpShopInfo.Location = new System.Drawing.Point(12, 12);
            this.grpShopInfo.Name = "grpShopInfo";
            this.grpShopInfo.Size = new System.Drawing.Size(568, 362);
            this.grpShopInfo.TabIndex = 0;
            this.grpShopInfo.TabStop = false;
            this.grpShopInfo.Text = "Thông tin Thương hiệu & Bộ nhận diện cửa hàng";
            // 
            // btnSaveBrand
            // 
            this.btnSaveBrand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(52)))), ((int)(((byte)(46)))));
            this.btnSaveBrand.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveBrand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveBrand.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSaveBrand.ForeColor = System.Drawing.Color.White;
            this.btnSaveBrand.Location = new System.Drawing.Point(165, 245);
            this.btnSaveBrand.Name = "btnSaveBrand";
            this.btnSaveBrand.Size = new System.Drawing.Size(380, 42);
            this.btnSaveBrand.TabIndex = 12;
            this.btnSaveBrand.Text = "💾  Lưu Bộ Nhận Diện Thương Hiệu";
            this.btnSaveBrand.UseVisualStyleBackColor = false;
            this.btnSaveBrand.Click += new System.EventHandler(this.btnSaveBrand_Click);
            // 
            // txtWifiInfo
            // 
            this.txtWifiInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtWifiInfo.Location = new System.Drawing.Point(165, 193);
            this.txtWifiInfo.Name = "txtWifiInfo";
            this.txtWifiInfo.Size = new System.Drawing.Size(380, 29);
            this.txtWifiInfo.TabIndex = 11;
            // 
            // lblWifi
            // 
            this.lblWifi.AutoSize = true;
            this.lblWifi.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblWifi.Location = new System.Drawing.Point(20, 196);
            this.lblWifi.Name = "lblWifi";
            this.lblWifi.Size = new System.Drawing.Size(111, 21);
            this.lblWifi.TabIndex = 10;
            this.lblWifi.Text = "Thông tin Wifi:";
            // 
            // txtVAT
            // 
            this.txtVAT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtVAT.Location = new System.Drawing.Point(445, 153);
            this.txtVAT.Name = "txtVAT";
            this.txtVAT.Size = new System.Drawing.Size(100, 29);
            this.txtVAT.TabIndex = 9;
            // 
            // lblVAT
            // 
            this.lblVAT.AutoSize = true;
            this.lblVAT.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblVAT.Location = new System.Drawing.Point(340, 156);
            this.lblVAT.Name = "lblVAT";
            this.lblVAT.Size = new System.Drawing.Size(94, 21);
            this.lblVAT.TabIndex = 8;
            this.lblVAT.Text = "Thuế VAT %:";
            // 
            // txtShopPhone
            // 
            this.txtShopPhone.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtShopPhone.Location = new System.Drawing.Point(165, 153);
            this.txtShopPhone.Name = "txtShopPhone";
            this.txtShopPhone.Size = new System.Drawing.Size(155, 29);
            this.txtShopPhone.TabIndex = 7;
            // 
            // lblSdt
            // 
            this.lblSdt.AutoSize = true;
            this.lblSdt.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSdt.Location = new System.Drawing.Point(20, 156);
            this.lblSdt.Name = "lblSdt";
            this.lblSdt.Size = new System.Drawing.Size(104, 21);
            this.lblSdt.TabIndex = 6;
            this.lblSdt.Text = "Số điện thoại:";
            // 
            // txtShopAddress
            // 
            this.txtShopAddress.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtShopAddress.Location = new System.Drawing.Point(165, 113);
            this.txtShopAddress.Name = "txtShopAddress";
            this.txtShopAddress.Size = new System.Drawing.Size(380, 29);
            this.txtShopAddress.TabIndex = 5;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblAddress.Location = new System.Drawing.Point(20, 116);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(60, 21);
            this.lblAddress.TabIndex = 4;
            this.lblAddress.Text = "Địa chỉ:";
            // 
            // txtShopSlogan
            // 
            this.txtShopSlogan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtShopSlogan.Location = new System.Drawing.Point(165, 73);
            this.txtShopSlogan.Name = "txtShopSlogan";
            this.txtShopSlogan.Size = new System.Drawing.Size(380, 29);
            this.txtShopSlogan.TabIndex = 3;
            // 
            // lblShopSlogan
            // 
            this.lblShopSlogan.AutoSize = true;
            this.lblShopSlogan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblShopSlogan.Location = new System.Drawing.Point(20, 76);
            this.lblShopSlogan.Name = "lblShopSlogan";
            this.lblShopSlogan.Size = new System.Drawing.Size(115, 21);
            this.lblShopSlogan.TabIndex = 2;
            this.lblShopSlogan.Text = "Khẩu hiệu/Phụ:";
            // 
            // txtShopName
            // 
            this.txtShopName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtShopName.Location = new System.Drawing.Point(165, 33);
            this.txtShopName.Name = "txtShopName";
            this.txtShopName.Size = new System.Drawing.Size(380, 29);
            this.txtShopName.TabIndex = 1;
            // 
            // lblShopname
            // 
            this.lblShopname.AutoSize = true;
            this.lblShopname.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblShopname.Location = new System.Drawing.Point(20, 36);
            this.lblShopname.Name = "lblShopname";
            this.lblShopname.Size = new System.Drawing.Size(130, 21);
            this.lblShopname.TabIndex = 0;
            this.lblShopname.Text = "Tên quán / Brand:";
            // 
            // tabDatabase
            // 
            this.tabDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(248)))), ((int)(((byte)(245)))));
            this.tabDatabase.Controls.Add(this.grpDatabase);
            this.tabDatabase.Location = new System.Drawing.Point(4, 30);
            this.tabDatabase.Name = "tabDatabase";
            this.tabDatabase.Padding = new System.Windows.Forms.Padding(12);
            this.tabDatabase.Size = new System.Drawing.Size(592, 386);
            this.tabDatabase.TabIndex = 1;
            this.tabDatabase.Text = "🗄️  Cấu hình Cơ sở dữ liệu";
            // 
            // grpDatabase
            // 
            this.grpDatabase.BackColor = System.Drawing.Color.White;
            this.grpDatabase.Controls.Add(this.btnSaveDatabase);
            this.grpDatabase.Controls.Add(this.btnSyncSchema);
            this.grpDatabase.Controls.Add(this.btnTestConnection);
            this.grpDatabase.Controls.Add(this.chkTrustServerCertificate);
            this.grpDatabase.Controls.Add(this.txtPassword);
            this.grpDatabase.Controls.Add(this.lblPasswordSql);
            this.grpDatabase.Controls.Add(this.txtUsername);
            this.grpDatabase.Controls.Add(this.lblUsernameSql);
            this.grpDatabase.Controls.Add(this.chkIntegratedSecurity);
            this.grpDatabase.Controls.Add(this.txtDatabase);
            this.grpDatabase.Controls.Add(this.lblDatabase);
            this.grpDatabase.Controls.Add(this.txtServer);
            this.grpDatabase.Controls.Add(this.lblServer);
            this.grpDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDatabase.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(52)))), ((int)(((byte)(46)))));
            this.grpDatabase.Location = new System.Drawing.Point(12, 12);
            this.grpDatabase.Name = "grpDatabase";
            this.grpDatabase.Size = new System.Drawing.Size(568, 362);
            this.grpDatabase.TabIndex = 0;
            this.grpDatabase.TabStop = false;
            this.grpDatabase.Text = "Kết nối Máy chủ SQL Server (Độc lập với nhận diện)";
            // 
            // btnSaveDatabase
            // 
            this.btnSaveDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnSaveDatabase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveDatabase.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSaveDatabase.ForeColor = System.Drawing.Color.White;
            this.btnSaveDatabase.Location = new System.Drawing.Point(155, 260);
            this.btnSaveDatabase.Name = "btnSaveDatabase";
            this.btnSaveDatabase.Size = new System.Drawing.Size(390, 42);
            this.btnSaveDatabase.TabIndex = 12;
            this.btnSaveDatabase.Text = "💾  Lưu Cấu Hình Kết Nối CSDL";
            this.btnSaveDatabase.UseVisualStyleBackColor = false;
            this.btnSaveDatabase.Click += new System.EventHandler(this.btnSaveDatabase_Click);
            // 
            // btnSyncSchema
            // 
            this.btnSyncSchema.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnSyncSchema.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSyncSchema.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSyncSchema.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSyncSchema.ForeColor = System.Drawing.Color.White;
            this.btnSyncSchema.Location = new System.Drawing.Point(350, 212);
            this.btnSyncSchema.Name = "btnSyncSchema";
            this.btnSyncSchema.Size = new System.Drawing.Size(195, 36);
            this.btnSyncSchema.TabIndex = 11;
            this.btnSyncSchema.Text = "⚡ Khởi tạo bảng CSDL";
            this.btnSyncSchema.UseVisualStyleBackColor = false;
            this.btnSyncSchema.Click += new System.EventHandler(this.btnSyncSchema_Click);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(110)))), ((int)(((byte)(99)))));
            this.btnTestConnection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestConnection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTestConnection.ForeColor = System.Drawing.Color.White;
            this.btnTestConnection.Location = new System.Drawing.Point(155, 212);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(185, 36);
            this.btnTestConnection.TabIndex = 10;
            this.btnTestConnection.Text = "🔍 Thử kết nối CSDL";
            this.btnTestConnection.UseVisualStyleBackColor = false;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // chkTrustServerCertificate
            // 
            this.chkTrustServerCertificate.AutoSize = true;
            this.chkTrustServerCertificate.Checked = true;
            this.chkTrustServerCertificate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrustServerCertificate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkTrustServerCertificate.Location = new System.Drawing.Point(155, 175);
            this.chkTrustServerCertificate.Name = "chkTrustServerCertificate";
            this.chkTrustServerCertificate.Size = new System.Drawing.Size(358, 24);
            this.chkTrustServerCertificate.TabIndex = 9;
            this.chkTrustServerCertificate.Text = "Tin cậy chứng chỉ máy chủ (TrustServerCertificate)";
            this.chkTrustServerCertificate.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtPassword.Location = new System.Drawing.Point(395, 137);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(150, 29);
            this.txtPassword.TabIndex = 8;
            // 
            // lblPasswordSql
            // 
            this.lblPasswordSql.AutoSize = true;
            this.lblPasswordSql.Enabled = false;
            this.lblPasswordSql.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPasswordSql.Location = new System.Drawing.Point(310, 140);
            this.lblPasswordSql.Name = "lblPasswordSql";
            this.lblPasswordSql.Size = new System.Drawing.Size(79, 21);
            this.lblPasswordSql.TabIndex = 7;
            this.lblPasswordSql.Text = "Password:";
            // 
            // txtUsername
            // 
            this.txtUsername.Enabled = false;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtUsername.Location = new System.Drawing.Point(155, 137);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(140, 29);
            this.txtUsername.TabIndex = 6;
            // 
            // lblUsernameSql
            // 
            this.lblUsernameSql.AutoSize = true;
            this.lblUsernameSql.Enabled = false;
            this.lblUsernameSql.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblUsernameSql.Location = new System.Drawing.Point(20, 140);
            this.lblUsernameSql.Name = "lblUsernameSql";
            this.lblUsernameSql.Size = new System.Drawing.Size(84, 21);
            this.lblUsernameSql.TabIndex = 5;
            this.lblUsernameSql.Text = "Username:";
            // 
            // chkIntegratedSecurity
            // 
            this.chkIntegratedSecurity.AutoSize = true;
            this.chkIntegratedSecurity.Checked = true;
            this.chkIntegratedSecurity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIntegratedSecurity.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.chkIntegratedSecurity.Location = new System.Drawing.Point(155, 105);
            this.chkIntegratedSecurity.Name = "chkIntegratedSecurity";
            this.chkIntegratedSecurity.Size = new System.Drawing.Size(263, 25);
            this.chkIntegratedSecurity.TabIndex = 4;
            this.chkIntegratedSecurity.Text = "Sử dụng Windows Authentication";
            this.chkIntegratedSecurity.UseVisualStyleBackColor = true;
            this.chkIntegratedSecurity.CheckedChanged += new System.EventHandler(this.chkIntegratedSecurity_CheckedChanged);
            // 
            // txtDatabase
            // 
            this.txtDatabase.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDatabase.Location = new System.Drawing.Point(155, 67);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(390, 29);
            this.txtDatabase.TabIndex = 3;
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDatabase.Location = new System.Drawing.Point(20, 70);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(123, 21);
            this.lblDatabase.TabIndex = 2;
            this.lblDatabase.Text = "Database Name:";
            // 
            // txtServer
            // 
            this.txtServer.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtServer.Location = new System.Drawing.Point(155, 29);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(390, 29);
            this.txtServer.TabIndex = 1;
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblServer.Location = new System.Drawing.Point(20, 32);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(104, 21);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server Name:";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Gray;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(495, 448);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 36);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Đóng";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmAppConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(248)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(635, 498);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tabAppConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAppConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình Hệ thống & Bộ nhận diện thương hiệu";
            this.Load += new System.EventHandler(this.FrmAppConfig_Load);
            this.tabAppConfig.ResumeLayout(false);
            this.tabBrand.ResumeLayout(false);
            this.grpShopInfo.ResumeLayout(false);
            this.grpShopInfo.PerformLayout();
            this.tabDatabase.ResumeLayout(false);
            this.grpDatabase.ResumeLayout(false);
            this.grpDatabase.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabAppConfig;
        private System.Windows.Forms.TabPage tabBrand;
        private System.Windows.Forms.TabPage tabDatabase;
        private System.Windows.Forms.GroupBox grpShopInfo;
        private System.Windows.Forms.TextBox txtShopName;
        private System.Windows.Forms.Label lblShopname;
        private System.Windows.Forms.TextBox txtShopSlogan;
        private System.Windows.Forms.Label lblShopSlogan;
        private System.Windows.Forms.TextBox txtShopPhone;
        private System.Windows.Forms.Label lblSdt;
        private System.Windows.Forms.TextBox txtShopAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtVAT;
        private System.Windows.Forms.Label lblVAT;
        private System.Windows.Forms.TextBox txtWifiInfo;
        private System.Windows.Forms.Label lblWifi;
        private System.Windows.Forms.Button btnSaveBrand;
        private System.Windows.Forms.GroupBox grpDatabase;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.CheckBox chkIntegratedSecurity;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblUsernameSql;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPasswordSql;
        private System.Windows.Forms.CheckBox chkTrustServerCertificate;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Button btnSyncSchema;
        private System.Windows.Forms.Button btnSaveDatabase;
        private System.Windows.Forms.Button btnExit;
    }
}

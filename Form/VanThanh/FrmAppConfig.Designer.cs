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
            this.grpDatabase = new System.Windows.Forms.GroupBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPasswordSql = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsernameSql = new System.Windows.Forms.Label();
            this.chkIntegratedSecurity = new System.Windows.Forms.CheckBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.grpShopInfo = new System.Windows.Forms.GroupBox();
            this.txtVAT = new System.Windows.Forms.TextBox();
            this.txtShopPhone = new System.Windows.Forms.TextBox();
            this.lblVAT = new System.Windows.Forms.Label();
            this.lblSdt = new System.Windows.Forms.Label();
            this.txtShopAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtShopName = new System.Windows.Forms.TextBox();
            this.lblShopname = new System.Windows.Forms.Label();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.grpDatabase.SuspendLayout();
            this.grpShopInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDatabase
            // 
            this.grpDatabase.Controls.Add(this.btnTestConnection);
            this.grpDatabase.Controls.Add(this.txtPassword);
            this.grpDatabase.Controls.Add(this.lblPasswordSql);
            this.grpDatabase.Controls.Add(this.txtUsername);
            this.grpDatabase.Controls.Add(this.lblUsernameSql);
            this.grpDatabase.Controls.Add(this.chkIntegratedSecurity);
            this.grpDatabase.Controls.Add(this.txtDatabase);
            this.grpDatabase.Controls.Add(this.lblDatabase);
            this.grpDatabase.Controls.Add(this.txtServer);
            this.grpDatabase.Controls.Add(this.lblServer);
            this.grpDatabase.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(52)))), ((int)(((byte)(46)))));
            this.grpDatabase.Location = new System.Drawing.Point(20, 20);
            this.grpDatabase.Name = "grpDatabase";
            this.grpDatabase.Size = new System.Drawing.Size(525, 265);
            this.grpDatabase.TabIndex = 0;
            this.grpDatabase.TabStop = false;
            this.grpDatabase.Text = "Cấu hình Cơ sở dữ liệu (SQL Server)";
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(110)))), ((int)(((byte)(99)))));
            this.btnTestConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestConnection.ForeColor = System.Drawing.Color.White;
            this.btnTestConnection.Location = new System.Drawing.Point(130, 170);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(180, 35);
            this.btnTestConnection.TabIndex = 9;
            this.btnTestConnection.Text = "🔍 Thử kết nối CSDL";
            this.btnTestConnection.UseVisualStyleBackColor = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(405, 124);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(120, 30);
            this.txtPassword.TabIndex = 8;
            // 
            // lblPasswordSql
            // 
            this.lblPasswordSql.AutoSize = true;
            this.lblPasswordSql.Enabled = false;
            this.lblPasswordSql.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPasswordSql.Location = new System.Drawing.Point(323, 127);
            this.lblPasswordSql.Name = "lblPasswordSql";
            this.lblPasswordSql.Size = new System.Drawing.Size(84, 23);
            this.lblPasswordSql.TabIndex = 7;
            this.lblPasswordSql.Text = "Password:";
            // 
            // txtUsername
            // 
            this.txtUsername.Enabled = false;
            this.txtUsername.Location = new System.Drawing.Point(165, 127);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(160, 30);
            this.txtUsername.TabIndex = 6;
            // 
            // lblUsernameSql
            // 
            this.lblUsernameSql.AutoSize = true;
            this.lblUsernameSql.Enabled = false;
            this.lblUsernameSql.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsernameSql.Location = new System.Drawing.Point(20, 130);
            this.lblUsernameSql.Name = "lblUsernameSql";
            this.lblUsernameSql.Size = new System.Drawing.Size(91, 23);
            this.lblUsernameSql.TabIndex = 5;
            this.lblUsernameSql.Text = "Username:";
            // 
            // chkIntegratedSecurity
            // 
            this.chkIntegratedSecurity.AutoSize = true;
            this.chkIntegratedSecurity.Checked = true;
            this.chkIntegratedSecurity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIntegratedSecurity.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIntegratedSecurity.Location = new System.Drawing.Point(130, 97);
            this.chkIntegratedSecurity.Name = "chkIntegratedSecurity";
            this.chkIntegratedSecurity.Size = new System.Drawing.Size(277, 27);
            this.chkIntegratedSecurity.TabIndex = 4;
            this.chkIntegratedSecurity.Text = "Sử dụng Windows Authentication";
            this.chkIntegratedSecurity.UseVisualStyleBackColor = true;
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(165, 65);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(360, 30);
            this.txtDatabase.TabIndex = 3;
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabase.Location = new System.Drawing.Point(20, 65);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(136, 23);
            this.lblDatabase.TabIndex = 2;
            this.lblDatabase.Text = "Database Name:";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(165, 30);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(360, 30);
            this.txtServer.TabIndex = 1;
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServer.Location = new System.Drawing.Point(20, 30);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(112, 23);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server Name:";
            // 
            // grpShopInfo
            // 
            this.grpShopInfo.Controls.Add(this.txtVAT);
            this.grpShopInfo.Controls.Add(this.txtShopPhone);
            this.grpShopInfo.Controls.Add(this.lblVAT);
            this.grpShopInfo.Controls.Add(this.lblSdt);
            this.grpShopInfo.Controls.Add(this.txtShopAddress);
            this.grpShopInfo.Controls.Add(this.lblAddress);
            this.grpShopInfo.Controls.Add(this.txtShopName);
            this.grpShopInfo.Controls.Add(this.lblShopname);
            this.grpShopInfo.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpShopInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(52)))), ((int)(((byte)(46)))));
            this.grpShopInfo.Location = new System.Drawing.Point(20, 295);
            this.grpShopInfo.Name = "grpShopInfo";
            this.grpShopInfo.Size = new System.Drawing.Size(525, 185);
            this.grpShopInfo.TabIndex = 1;
            this.grpShopInfo.TabStop = false;
            this.grpShopInfo.Text = "Thông tin Quán Cà phê (Dùng khi in hóa đơn)";
            // 
            // txtVAT
            // 
            this.txtVAT.Location = new System.Drawing.Point(425, 97);
            this.txtVAT.Name = "txtVAT";
            this.txtVAT.Size = new System.Drawing.Size(100, 30);
            this.txtVAT.TabIndex = 17;
            // 
            // txtShopPhone
            // 
            this.txtShopPhone.Location = new System.Drawing.Point(165, 97);
            this.txtShopPhone.Name = "txtShopPhone";
            this.txtShopPhone.Size = new System.Drawing.Size(145, 30);
            this.txtShopPhone.TabIndex = 15;
            // 
            // lblVAT
            // 
            this.lblVAT.AutoSize = true;
            this.lblVAT.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVAT.Location = new System.Drawing.Point(316, 100);
            this.lblVAT.Name = "lblVAT";
            this.lblVAT.Size = new System.Drawing.Size(115, 23);
            this.lblVAT.TabIndex = 16;
            this.lblVAT.Text = "Thuế VAT (%):";
            // 
            // lblSdt
            // 
            this.lblSdt.AutoSize = true;
            this.lblSdt.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSdt.Location = new System.Drawing.Point(20, 100);
            this.lblSdt.Name = "lblSdt";
            this.lblSdt.Size = new System.Drawing.Size(115, 23);
            this.lblSdt.TabIndex = 14;
            this.lblSdt.Text = "Số điện thoại:";
            // 
            // txtShopAddress
            // 
            this.txtShopAddress.Location = new System.Drawing.Point(165, 62);
            this.txtShopAddress.Name = "txtShopAddress";
            this.txtShopAddress.Size = new System.Drawing.Size(360, 30);
            this.txtShopAddress.TabIndex = 13;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(20, 65);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(66, 23);
            this.lblAddress.TabIndex = 12;
            this.lblAddress.Text = "Địa chỉ:";
            // 
            // txtShopName
            // 
            this.txtShopName.Location = new System.Drawing.Point(165, 30);
            this.txtShopName.Name = "txtShopName";
            this.txtShopName.Size = new System.Drawing.Size(360, 30);
            this.txtShopName.TabIndex = 11;
            // 
            // lblShopname
            // 
            this.lblShopname.AutoSize = true;
            this.lblShopname.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShopname.Location = new System.Drawing.Point(20, 30);
            this.lblShopname.Name = "lblShopname";
            this.lblShopname.Size = new System.Drawing.Size(84, 23);
            this.lblShopname.TabIndex = 10;
            this.lblShopname.Text = "Tên quán:";
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(52)))), ((int)(((byte)(46)))));
            this.btnSaveConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveConfig.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveConfig.ForeColor = System.Drawing.Color.White;
            this.btnSaveConfig.Location = new System.Drawing.Point(221, 495);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(182, 40);
            this.btnSaveConfig.TabIndex = 18;
            this.btnSaveConfig.Text = "💾 Lưu Cấu Hình";
            this.btnSaveConfig.UseVisualStyleBackColor = false;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(375, 497);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(0, 0);
            this.button2.TabIndex = 19;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Gray;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(423, 495);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(122, 40);
            this.btnExit.TabIndex = 20;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmAppConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(248)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(562, 573);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSaveConfig);
            this.Controls.Add(this.grpShopInfo);
            this.Controls.Add(this.grpDatabase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAppConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình Hệ thống & Kết nối Cơ sở dữ liệu";
            this.grpDatabase.ResumeLayout(false);
            this.grpDatabase.PerformLayout();
            this.grpShopInfo.ResumeLayout(false);
            this.grpShopInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDatabase;
        private System.Windows.Forms.CheckBox chkIntegratedSecurity;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPasswordSql;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblUsernameSql;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.GroupBox grpShopInfo;
        private System.Windows.Forms.TextBox txtShopName;
        private System.Windows.Forms.Label lblShopname;
        private System.Windows.Forms.TextBox txtShopPhone;
        private System.Windows.Forms.Label lblSdt;
        private System.Windows.Forms.TextBox txtShopAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtVAT;
        private System.Windows.Forms.Label lblVAT;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnExit;
    }
}
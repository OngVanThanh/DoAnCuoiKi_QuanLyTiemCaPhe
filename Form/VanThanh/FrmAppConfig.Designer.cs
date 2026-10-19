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
            this.lblServer = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.chkIntegratedSecurity = new System.Windows.Forms.CheckBox();
            this.lblUsernameSql = new System.Windows.Forms.Label();
            this.txtUsernameSql = new System.Windows.Forms.TextBox();
            this.txtPasswordSql = new System.Windows.Forms.TextBox();
            this.lblPasswordSql = new System.Windows.Forms.Label();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.grpShopInfo = new System.Windows.Forms.GroupBox();
            this.txtShopname = new System.Windows.Forms.TextBox();
            this.lblShopname = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblSdt = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.lblVAT = new System.Windows.Forms.Label();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.grpDatabase.SuspendLayout();
            this.grpShopInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDatabase
            // 
            this.grpDatabase.Controls.Add(this.btnTestConnection);
            this.grpDatabase.Controls.Add(this.txtPasswordSql);
            this.grpDatabase.Controls.Add(this.lblPasswordSql);
            this.grpDatabase.Controls.Add(this.txtUsernameSql);
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
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(165, 30);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(360, 30);
            this.txtServer.TabIndex = 1;
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
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(165, 65);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(360, 30);
            this.txtDatabase.TabIndex = 3;
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
            // txtUsernameSql
            // 
            this.txtUsernameSql.Enabled = false;
            this.txtUsernameSql.Location = new System.Drawing.Point(165, 127);
            this.txtUsernameSql.Name = "txtUsernameSql";
            this.txtUsernameSql.Size = new System.Drawing.Size(160, 30);
            this.txtUsernameSql.TabIndex = 6;
            // 
            // txtPasswordSql
            // 
            this.txtPasswordSql.Enabled = false;
            this.txtPasswordSql.Location = new System.Drawing.Point(405, 124);
            this.txtPasswordSql.Name = "txtPasswordSql";
            this.txtPasswordSql.PasswordChar = '*';
            this.txtPasswordSql.Size = new System.Drawing.Size(120, 30);
            this.txtPasswordSql.TabIndex = 8;
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
            // grpShopInfo
            // 
            this.grpShopInfo.Controls.Add(this.textBox3);
            this.grpShopInfo.Controls.Add(this.textBox2);
            this.grpShopInfo.Controls.Add(this.lblVAT);
            this.grpShopInfo.Controls.Add(this.lblSdt);
            this.grpShopInfo.Controls.Add(this.textBox1);
            this.grpShopInfo.Controls.Add(this.lblAddress);
            this.grpShopInfo.Controls.Add(this.txtShopname);
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
            // txtShopname
            // 
            this.txtShopname.Location = new System.Drawing.Point(165, 27);
            this.txtShopname.Name = "txtShopname";
            this.txtShopname.Size = new System.Drawing.Size(360, 30);
            this.txtShopname.TabIndex = 11;
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
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(165, 62);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(360, 30);
            this.textBox1.TabIndex = 13;
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
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(165, 97);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(145, 30);
            this.textBox2.TabIndex = 15;
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
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(425, 97);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 30);
            this.textBox3.TabIndex = 17;
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
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(423, 495);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 40);
            this.button1.TabIndex = 20;
            this.button1.Text = "Thoát";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // FrmAppConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(248)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(562, 573);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.TextBox txtPasswordSql;
        private System.Windows.Forms.Label lblPasswordSql;
        private System.Windows.Forms.TextBox txtUsernameSql;
        private System.Windows.Forms.Label lblUsernameSql;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.GroupBox grpShopInfo;
        private System.Windows.Forms.TextBox txtShopname;
        private System.Windows.Forms.Label lblShopname;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblSdt;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label lblVAT;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}
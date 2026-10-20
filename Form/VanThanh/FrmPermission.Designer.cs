namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmPermission
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
            this.grpUsers = new System.Windows.Forms.GroupBox();
            this.btnCreateUser = new System.Windows.Forms.Button();
            this.cboRoles = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.grpPermissions = new System.Windows.Forms.GroupBox();
            this.btnSavePermission = new System.Windows.Forms.Button();
            this.clbPermissions = new System.Windows.Forms.CheckedListBox();
            this.lblPermissions = new System.Windows.Forms.Label();
            this.grpUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.grpPermissions.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpUsers
            // 
            this.grpUsers.Controls.Add(this.btnCreateUser);
            this.grpUsers.Controls.Add(this.cboRoles);
            this.grpUsers.Controls.Add(this.label1);
            this.grpUsers.Controls.Add(this.txtPassword);
            this.grpUsers.Controls.Add(this.lblPassword);
            this.grpUsers.Controls.Add(this.lblUserName);
            this.grpUsers.Controls.Add(this.txtUsername);
            this.grpUsers.Controls.Add(this.dgvUsers);
            this.grpUsers.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(52)))), ((int)(((byte)(46)))));
            this.grpUsers.Location = new System.Drawing.Point(20, 20);
            this.grpUsers.Name = "grpUsers";
            this.grpUsers.Size = new System.Drawing.Size(490, 520);
            this.grpUsers.TabIndex = 0;
            this.grpUsers.TabStop = false;
            this.grpUsers.Text = "Danh sách Tài khoản & Tạo mới";
            // 
            // btnCreateUser
            // 
            this.btnCreateUser.AutoSize = true;
            this.btnCreateUser.Location = new System.Drawing.Point(130, 390);
            this.btnCreateUser.Name = "btnCreateUser";
            this.btnCreateUser.Size = new System.Drawing.Size(160, 40);
            this.btnCreateUser.TabIndex = 7;
            this.btnCreateUser.Text = "➕ Tạo tài khoản";
            this.btnCreateUser.UseVisualStyleBackColor = true;
            // 
            // cboRoles
            // 
            this.cboRoles.FormattingEnabled = true;
            this.cboRoles.Items.AddRange(new object[] {
            "Admin",
            "Thu ngân",
            "Pha chế"});
            this.cboRoles.Location = new System.Drawing.Point(149, 347);
            this.cboRoles.Name = "cboRoles";
            this.cboRoles.Size = new System.Drawing.Size(335, 31);
            this.cboRoles.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 350);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "Vai trò/Chức vụ:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(149, 307);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(335, 30);
            this.txtPassword.TabIndex = 4;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(3, 310);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(91, 23);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Mật khẩu:";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(3, 270);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(133, 23);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "Tên đăng nhập:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(149, 267);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(335, 30);
            this.txtUsername.TabIndex = 1;
            // 
            // dgvUsers
            // 
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Location = new System.Drawing.Point(15, 30);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.RowHeadersWidth = 51;
            this.dgvUsers.RowTemplate.Height = 24;
            this.dgvUsers.Size = new System.Drawing.Size(450, 220);
            this.dgvUsers.TabIndex = 0;
            // 
            // grpPermissions
            // 
            this.grpPermissions.Controls.Add(this.btnSavePermission);
            this.grpPermissions.Controls.Add(this.clbPermissions);
            this.grpPermissions.Controls.Add(this.lblPermissions);
            this.grpPermissions.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPermissions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(52)))), ((int)(((byte)(46)))));
            this.grpPermissions.Location = new System.Drawing.Point(515, 20);
            this.grpPermissions.Name = "grpPermissions";
            this.grpPermissions.Size = new System.Drawing.Size(405, 520);
            this.grpPermissions.TabIndex = 1;
            this.grpPermissions.TabStop = false;
            this.grpPermissions.Text = "Phân quyền Chức năng / Form";
            // 
            // btnSavePermission
            // 
            this.btnSavePermission.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(110)))), ((int)(((byte)(99)))));
            this.btnSavePermission.ForeColor = System.Drawing.Color.White;
            this.btnSavePermission.Location = new System.Drawing.Point(180, 445);
            this.btnSavePermission.Name = "btnSavePermission";
            this.btnSavePermission.Size = new System.Drawing.Size(210, 40);
            this.btnSavePermission.TabIndex = 2;
            this.btnSavePermission.Text = "💾 Lưu phân quyền";
            this.btnSavePermission.UseVisualStyleBackColor = false;
            // 
            // clbPermissions
            // 
            this.clbPermissions.FormattingEnabled = true;
            this.clbPermissions.Items.AddRange(new object[] {
            "Quản lý Bán hàng",
            "Quản lý Kho",
            "Quản lý Nhân viên",
            "Báo cáo Doanh thu",
            "Cấu hình hệ thống"});
            this.clbPermissions.Location = new System.Drawing.Point(15, 60);
            this.clbPermissions.Name = "clbPermissions";
            this.clbPermissions.Size = new System.Drawing.Size(375, 354);
            this.clbPermissions.TabIndex = 1;
            // 
            // lblPermissions
            // 
            this.lblPermissions.AutoSize = true;
            this.lblPermissions.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPermissions.Location = new System.Drawing.Point(15, 30);
            this.lblPermissions.Name = "lblPermissions";
            this.lblPermissions.Size = new System.Drawing.Size(340, 23);
            this.lblPermissions.TabIndex = 0;
            this.lblPermissions.Text = "Chọn các Form/Quyền được phép truy cập:";
            // 
            // FrmPermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(248)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(932, 553);
            this.Controls.Add(this.grpPermissions);
            this.Controls.Add(this.grpUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPermission";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Tài khoản & Phân quyền hệ thống";
            this.grpUsers.ResumeLayout(false);
            this.grpUsers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.grpPermissions.ResumeLayout(false);
            this.grpPermissions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpUsers;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.ComboBox cboRoles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnCreateUser;
        private System.Windows.Forms.GroupBox grpPermissions;
        private System.Windows.Forms.Button btnSavePermission;
        private System.Windows.Forms.CheckedListBox clbPermissions;
        private System.Windows.Forms.Label lblPermissions;
    }
}
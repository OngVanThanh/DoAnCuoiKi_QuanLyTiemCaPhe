namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmForgotPassword
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
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblId = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnExitNewPassword = new System.Windows.Forms.Button();
            this.btnSaveNewPassword = new System.Windows.Forms.Button();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtCheckPassword = new System.Windows.Forms.TextBox();
            this.lblCheckPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(196, 151);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(335, 22);
            this.txtNewPassword.TabIndex = 2;
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Location = new System.Drawing.Point(35, 154);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(86, 16);
            this.lblNewPassword.TabIndex = 34;
            this.lblNewPassword.Text = "Mật khẩu mới";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(196, 81);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(335, 22);
            this.txtId.TabIndex = 0;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(35, 84);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(86, 16);
            this.lblId.TabIndex = 32;
            this.lblId.Text = "Mã nhân viên";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(230, 47);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(126, 16);
            this.lblTitle.TabIndex = 31;
            this.lblTitle.Text = "Đặt lại mật khẩu mới";
            // 
            // btnExitNewPassword
            // 
            this.btnExitNewPassword.Location = new System.Drawing.Point(318, 245);
            this.btnExitNewPassword.Name = "btnExitNewPassword";
            this.btnExitNewPassword.Size = new System.Drawing.Size(75, 38);
            this.btnExitNewPassword.TabIndex = 30;
            this.btnExitNewPassword.Text = "Thoát";
            this.btnExitNewPassword.UseVisualStyleBackColor = true;
            this.btnExitNewPassword.Click += new System.EventHandler(this.btnExitNewPassword_Click);
            // 
            // btnSaveNewPassword
            // 
            this.btnSaveNewPassword.Location = new System.Drawing.Point(223, 245);
            this.btnSaveNewPassword.Name = "btnSaveNewPassword";
            this.btnSaveNewPassword.Size = new System.Drawing.Size(75, 38);
            this.btnSaveNewPassword.TabIndex = 29;
            this.btnSaveNewPassword.Text = "Lưu";
            this.btnSaveNewPassword.UseVisualStyleBackColor = true;
            this.btnSaveNewPassword.Click += new System.EventHandler(this.btnSaveNewPassword_Click);
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(196, 112);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(335, 22);
            this.txtPhone.TabIndex = 1;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(35, 115);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(35, 16);
            this.lblPhone.TabIndex = 27;
            this.lblPhone.Text = "SDT";
            // 
            // txtCheckPassword
            // 
            this.txtCheckPassword.Location = new System.Drawing.Point(196, 189);
            this.txtCheckPassword.Name = "txtCheckPassword";
            this.txtCheckPassword.PasswordChar = '*';
            this.txtCheckPassword.Size = new System.Drawing.Size(335, 22);
            this.txtCheckPassword.TabIndex = 3;
            // 
            // lblCheckPassword
            // 
            this.lblCheckPassword.AutoSize = true;
            this.lblCheckPassword.Location = new System.Drawing.Point(35, 192);
            this.lblCheckPassword.Name = "lblCheckPassword";
            this.lblCheckPassword.Size = new System.Drawing.Size(114, 16);
            this.lblCheckPassword.TabIndex = 36;
            this.lblCheckPassword.Text = "Nhập lại mật khẩu";
            // 
            // FrmForgotPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 310);
            this.Controls.Add(this.txtCheckPassword);
            this.Controls.Add(this.lblCheckPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.lblNewPassword);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnExitNewPassword);
            this.Controls.Add(this.btnSaveNewPassword);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblPhone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "FrmForgotPassword";
            this.Text = "FrmForgotPassword";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnExitNewPassword;
        private System.Windows.Forms.Button btnSaveNewPassword;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtCheckPassword;
        private System.Windows.Forms.Label lblCheckPassword;
    }
}
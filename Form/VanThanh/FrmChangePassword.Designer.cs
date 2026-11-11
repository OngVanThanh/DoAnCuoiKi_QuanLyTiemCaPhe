namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmChangePassword
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
            this.lblPasswordChangeTitle = new System.Windows.Forms.Label();
            this.btnExitNewPassword = new System.Windows.Forms.Button();
            this.btnSaveNewPassword = new System.Windows.Forms.Button();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.txtCheckPassword = new System.Windows.Forms.TextBox();
            this.lblCheckPassword = new System.Windows.Forms.Label();
            this.btnTogglePasswordChange = new System.Windows.Forms.Button();
            this.btnTogglePassword = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPasswordChangeTitle
            // 
            this.lblPasswordChangeTitle.AutoSize = true;
            this.lblPasswordChangeTitle.Location = new System.Drawing.Point(208, 23);
            this.lblPasswordChangeTitle.Name = "lblPasswordChangeTitle";
            this.lblPasswordChangeTitle.Size = new System.Drawing.Size(84, 16);
            this.lblPasswordChangeTitle.TabIndex = 22;
            this.lblPasswordChangeTitle.Text = "Đổi mật khẩu";
            // 
            // btnExitNewPassword
            // 
            this.btnExitNewPassword.Location = new System.Drawing.Point(427, 174);
            this.btnExitNewPassword.Name = "btnExitNewPassword";
            this.btnExitNewPassword.Size = new System.Drawing.Size(75, 38);
            this.btnExitNewPassword.TabIndex = 21;
            this.btnExitNewPassword.Text = "Thoát";
            this.btnExitNewPassword.UseVisualStyleBackColor = true;
            this.btnExitNewPassword.Click += new System.EventHandler(this.btnExitNewPassword_Click);
            // 
            // btnSaveNewPassword
            // 
            this.btnSaveNewPassword.Location = new System.Drawing.Point(340, 174);
            this.btnSaveNewPassword.Name = "btnSaveNewPassword";
            this.btnSaveNewPassword.Size = new System.Drawing.Size(75, 38);
            this.btnSaveNewPassword.TabIndex = 20;
            this.btnSaveNewPassword.Text = "Lưu";
            this.btnSaveNewPassword.UseVisualStyleBackColor = true;
            this.btnSaveNewPassword.Click += new System.EventHandler(this.btnSaveNewPassword_Click);
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(174, 88);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(335, 22);
            this.txtNewPassword.TabIndex = 1;
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Location = new System.Drawing.Point(13, 91);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(86, 16);
            this.lblNewPassword.TabIndex = 18;
            this.lblNewPassword.Text = "Mật khẩu mới";
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Location = new System.Drawing.Point(174, 57);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PasswordChar = '*';
            this.txtOldPassword.Size = new System.Drawing.Size(335, 22);
            this.txtOldPassword.TabIndex = 0;
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Location = new System.Drawing.Point(13, 60);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(78, 16);
            this.lblOldPassword.TabIndex = 23;
            this.lblOldPassword.Text = "Mật khẩu cũ";
            // 
            // txtCheckPassword
            // 
            this.txtCheckPassword.Location = new System.Drawing.Point(174, 127);
            this.txtCheckPassword.Name = "txtCheckPassword";
            this.txtCheckPassword.PasswordChar = '*';
            this.txtCheckPassword.Size = new System.Drawing.Size(335, 22);
            this.txtCheckPassword.TabIndex = 2;
            // 
            // lblCheckPassword
            // 
            this.lblCheckPassword.AutoSize = true;
            this.lblCheckPassword.Location = new System.Drawing.Point(13, 130);
            this.lblCheckPassword.Name = "lblCheckPassword";
            this.lblCheckPassword.Size = new System.Drawing.Size(86, 16);
            this.lblCheckPassword.TabIndex = 25;
            this.lblCheckPassword.Text = "Mật khẩu mới";
            // 
            // btnTogglePasswordChange
            // 
            this.btnTogglePasswordChange.Location = new System.Drawing.Point(476, 127);
            this.btnTogglePasswordChange.Name = "btnTogglePasswordChange";
            this.btnTogglePasswordChange.Size = new System.Drawing.Size(33, 23);
            this.btnTogglePasswordChange.TabIndex = 33;
            this.btnTogglePasswordChange.UseVisualStyleBackColor = true;
            // 
            // btnTogglePassword
            // 
            this.btnTogglePassword.Location = new System.Drawing.Point(476, 88);
            this.btnTogglePassword.Name = "btnTogglePassword";
            this.btnTogglePassword.Size = new System.Drawing.Size(33, 23);
            this.btnTogglePassword.TabIndex = 32;
            this.btnTogglePassword.UseVisualStyleBackColor = true;
            this.btnTogglePassword.Click += new System.EventHandler(this.btnTogglePassword_Click);
            // 
            // FrmChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 235);
            this.Controls.Add(this.btnTogglePasswordChange);
            this.Controls.Add(this.btnTogglePassword);
            this.Controls.Add(this.txtCheckPassword);
            this.Controls.Add(this.lblCheckPassword);
            this.Controls.Add(this.txtOldPassword);
            this.Controls.Add(this.lblOldPassword);
            this.Controls.Add(this.lblPasswordChangeTitle);
            this.Controls.Add(this.btnExitNewPassword);
            this.Controls.Add(this.btnSaveNewPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.lblNewPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmChangePassword";
            this.Text = "FrmChangePassword";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPasswordChangeTitle;
        private System.Windows.Forms.Button btnExitNewPassword;
        private System.Windows.Forms.Button btnSaveNewPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.TextBox txtOldPassword;
        private System.Windows.Forms.Label lblOldPassword;
        private System.Windows.Forms.TextBox txtCheckPassword;
        private System.Windows.Forms.Label lblCheckPassword;
        private System.Windows.Forms.Button btnTogglePasswordChange;
        private System.Windows.Forms.Button btnTogglePassword;
    }
}
namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmAdminAuth
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
            this.lblNotice = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtAdminUser = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtAdminPass = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNotice
            // 
            this.lblNotice.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotice.Location = new System.Drawing.Point(20, 15);
            this.lblNotice.Name = "lblNotice";
            this.lblNotice.Size = new System.Drawing.Size(360, 30);
            this.lblNotice.TabIndex = 0;
            this.lblNotice.Text = "Vui lòng đăng nhập tài khoản quản lý để tiếp tục:";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(20, 60);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(61, 15);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "Tài khoản:";
            // 
            // txtAdminUser
            // 
            this.txtAdminUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdminUser.Location = new System.Drawing.Point(120, 57);
            this.txtAdminUser.Name = "txtAdminUser";
            this.txtAdminUser.Size = new System.Drawing.Size(250, 23);
            this.txtAdminUser.TabIndex = 2;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPass.Location = new System.Drawing.Point(20, 100);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(60, 15);
            this.lblPass.TabIndex = 3;
            this.lblPass.Text = "Mật khẩu:";
            // 
            // txtAdminPass
            // 
            this.txtAdminPass.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdminPass.Location = new System.Drawing.Point(120, 97);
            this.txtAdminPass.Name = "txtAdminPass";
            this.txtAdminPass.Size = new System.Drawing.Size(250, 23);
            this.txtAdminPass.TabIndex = 4;
            this.txtAdminPass.UseSystemPasswordChar = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(78)))), ((int)(((byte)(55)))));
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(150, 145);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(100, 32);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "Xác nhận";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Gray;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(270, 145);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 32);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FrmAdminAuth
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(404, 201);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtAdminPass);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.txtAdminUser);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblNotice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAdminAuth";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xác nhận quyền Quản lý";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNotice;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtAdminUser;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtAdminPass;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnExit;
    }
}

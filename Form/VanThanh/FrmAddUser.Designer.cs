namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmAddUser
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
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cboFullNames = new System.Windows.Forms.ComboBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtCheckPassWord = new System.Windows.Forms.TextBox();
            this.lblCheckPassword = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRole = new System.Windows.Forms.TextBox();
            this.btnTogglePassword = new System.Windows.Forms.Button();
            this.btnTogglePasswordChange = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(472, 295);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 38);
            this.btnExit.TabIndex = 26;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(385, 295);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 38);
            this.btnSave.TabIndex = 25;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cboFullNames
            // 
            this.cboFullNames.FormattingEnabled = true;
            this.cboFullNames.Items.AddRange(new object[] {
            "Admin",
            "Thu ngân",
            "Pha chế"});
            this.cboFullNames.Location = new System.Drawing.Point(212, 135);
            this.cboFullNames.Name = "cboFullNames";
            this.cboFullNames.Size = new System.Drawing.Size(335, 24);
            this.cboFullNames.TabIndex = 1;
            this.cboFullNames.SelectedIndexChanged += new System.EventHandler(this.cboFullNames_SelectedIndexChanged);
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(51, 138);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(67, 16);
            this.lblFullName.TabIndex = 23;
            this.lblFullName.Text = "Họ và tên:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(211, 180);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(335, 22);
            this.txtPassword.TabIndex = 2;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(50, 183);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(64, 16);
            this.lblPassword.TabIndex = 19;
            this.lblPassword.Text = "Mật khẩu:";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(50, 93);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(101, 16);
            this.lblUserName.TabIndex = 18;
            this.lblUserName.Text = "Tên đăng nhập:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(211, 90);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(335, 22);
            this.txtUsername.TabIndex = 0;
            // 
            // txtCheckPassWord
            // 
            this.txtCheckPassWord.Location = new System.Drawing.Point(211, 225);
            this.txtCheckPassWord.Name = "txtCheckPassWord";
            this.txtCheckPassWord.PasswordChar = '*';
            this.txtCheckPassWord.Size = new System.Drawing.Size(335, 22);
            this.txtCheckPassWord.TabIndex = 3;
            // 
            // lblCheckPassword
            // 
            this.lblCheckPassword.AutoSize = true;
            this.lblCheckPassword.Location = new System.Drawing.Point(50, 228);
            this.lblCheckPassword.Name = "lblCheckPassword";
            this.lblCheckPassword.Size = new System.Drawing.Size(117, 16);
            this.lblCheckPassword.TabIndex = 27;
            this.lblCheckPassword.Text = "Nhập lại mật khẩu:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(209, 41);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(147, 16);
            this.lblTitle.TabIndex = 29;
            this.lblTitle.Text = "THÊM TÀI KHOẢN MỚI";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 264);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 16);
            this.label1.TabIndex = 21;
            this.label1.Text = "Vai trò/Chức vụ:";
            // 
            // txtRole
            // 
            this.txtRole.Location = new System.Drawing.Point(211, 258);
            this.txtRole.Name = "txtRole";
            this.txtRole.Size = new System.Drawing.Size(335, 22);
            this.txtRole.TabIndex = 4;
            // 
            // btnTogglePassword
            // 
            this.btnTogglePassword.Location = new System.Drawing.Point(513, 180);
            this.btnTogglePassword.Name = "btnTogglePassword";
            this.btnTogglePassword.Size = new System.Drawing.Size(33, 23);
            this.btnTogglePassword.TabIndex = 30;
            this.btnTogglePassword.UseVisualStyleBackColor = true;
            this.btnTogglePassword.Click += new System.EventHandler(this.btnTogglePassword_Click);
            // 
            // btnTogglePasswordChange
            // 
            this.btnTogglePasswordChange.Location = new System.Drawing.Point(513, 225);
            this.btnTogglePasswordChange.Name = "btnTogglePasswordChange";
            this.btnTogglePasswordChange.Size = new System.Drawing.Size(33, 23);
            this.btnTogglePasswordChange.TabIndex = 31;
            this.btnTogglePasswordChange.UseVisualStyleBackColor = true;
            this.btnTogglePasswordChange.Click += new System.EventHandler(this.btnTogglePasswordChange_Click);
            // 
            // FrmAddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(606, 361);
            this.Controls.Add(this.btnTogglePasswordChange);
            this.Controls.Add(this.btnTogglePassword);
            this.Controls.Add(this.txtRole);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtCheckPassWord);
            this.Controls.Add(this.lblCheckPassword);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cboFullNames);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.txtUsername);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAddUser";
            this.Load += new System.EventHandler(this.FrmAddUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cboFullNames;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtCheckPassWord;
        private System.Windows.Forms.Label lblCheckPassword;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRole;
        private CoffeeShopDBDataSet coffeeShopDBDataSet1;
        private System.Windows.Forms.Button btnTogglePassword;
        private System.Windows.Forms.Button btnTogglePasswordChange;
    }
}
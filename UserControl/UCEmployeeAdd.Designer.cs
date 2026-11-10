namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class UCEmployeeAdd
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnEditAvatar = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.ptbAvatar = new System.Windows.Forms.PictureBox();
            this.dtpBirthDate = new System.Windows.Forms.DateTimePicker();
            this.dtpHireDate = new System.Windows.Forms.DateTimePicker();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.cbPosition = new System.Windows.Forms.ComboBox();
            this.txtSalary = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblHireDate = new System.Windows.Forms.Label();
            this.lblSalary = new System.Windows.Forms.Label();
            this.lblBirthDate = new System.Windows.Forms.Label();
            this.cboPosition = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(69, 19);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(149, 16);
            this.lblTitle.TabIndex = 90;
            this.lblTitle.Text = "THÊM NHÂN VIÊN MỚI";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(279, 437);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEditAvatar
            // 
            this.btnEditAvatar.Location = new System.Drawing.Point(260, 171);
            this.btnEditAvatar.Name = "btnEditAvatar";
            this.btnEditAvatar.Size = new System.Drawing.Size(75, 23);
            this.btnEditAvatar.TabIndex = 89;
            this.btnEditAvatar.Text = "Thêm ảnh";
            this.btnEditAvatar.UseVisualStyleBackColor = true;
            this.btnEditAvatar.Click += new System.EventHandler(this.btnAddAvatar_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(198, 437);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ptbAvatar
            // 
            this.ptbAvatar.Location = new System.Drawing.Point(36, 62);
            this.ptbAvatar.Name = "ptbAvatar";
            this.ptbAvatar.Size = new System.Drawing.Size(218, 132);
            this.ptbAvatar.TabIndex = 86;
            this.ptbAvatar.TabStop = false;
            this.ptbAvatar.Click += new System.EventHandler(this.ptbAvatar_Click);
            // 
            // dtpBirthDate
            // 
            this.dtpBirthDate.Location = new System.Drawing.Point(111, 294);
            this.dtpBirthDate.Name = "dtpBirthDate";
            this.dtpBirthDate.Size = new System.Drawing.Size(200, 22);
            this.dtpBirthDate.TabIndex = 3;
            // 
            // dtpHireDate
            // 
            this.dtpHireDate.Location = new System.Drawing.Point(128, 380);
            this.dtpHireDate.Name = "dtpHireDate";
            this.dtpHireDate.Size = new System.Drawing.Size(200, 22);
            this.dtpHireDate.TabIndex = 6;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(128, 266);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(100, 22);
            this.txtAddress.TabIndex = 2;
            // 
            // cbPosition
            // 
            this.cbPosition.FormattingEnabled = true;
            this.cbPosition.Location = new System.Drawing.Point(128, 322);
            this.cbPosition.Name = "cbPosition";
            this.cbPosition.Size = new System.Drawing.Size(121, 24);
            this.cbPosition.TabIndex = 4;
            this.cbPosition.SelectedIndexChanged += new System.EventHandler(this.cbPosition_SelectedIndexChanged);
            // 
            // txtSalary
            // 
            this.txtSalary.Location = new System.Drawing.Point(144, 352);
            this.txtSalary.Name = "txtSalary";
            this.txtSalary.Size = new System.Drawing.Size(100, 22);
            this.txtSalary.TabIndex = 5;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(127, 238);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(100, 22);
            this.txtPhone.TabIndex = 1;
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(128, 213);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(100, 22);
            this.txtFullName.TabIndex = 0;
            // 
            // lblHireDate
            // 
            this.lblHireDate.Location = new System.Drawing.Point(25, 385);
            this.lblHireDate.Name = "lblHireDate";
            this.lblHireDate.Size = new System.Drawing.Size(97, 16);
            this.lblHireDate.TabIndex = 77;
            this.lblHireDate.Text = "Ngày vào làm";
            // 
            // lblSalary
            // 
            this.lblSalary.Location = new System.Drawing.Point(23, 355);
            this.lblSalary.Name = "lblSalary";
            this.lblSalary.Size = new System.Drawing.Size(127, 16);
            this.lblSalary.TabIndex = 76;
            this.lblSalary.Text = "Mức lương cơ bản";
            // 
            // lblBirthDate
            // 
            this.lblBirthDate.Location = new System.Drawing.Point(26, 299);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(80, 16);
            this.lblBirthDate.TabIndex = 75;
            this.lblBirthDate.Text = "Ngày sinh";
            // 
            // cboPosition
            // 
            this.cboPosition.Location = new System.Drawing.Point(33, 325);
            this.cboPosition.Name = "cboPosition";
            this.cboPosition.Size = new System.Drawing.Size(81, 16);
            this.cboPosition.TabIndex = 74;
            this.cboPosition.Text = "Chức vụ";
            // 
            // lblAddress
            // 
            this.lblAddress.Location = new System.Drawing.Point(33, 269);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(81, 16);
            this.lblAddress.TabIndex = 73;
            this.lblAddress.Text = "Địa chỉ";
            // 
            // lblPhone
            // 
            this.lblPhone.Location = new System.Drawing.Point(23, 242);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(98, 16);
            this.lblPhone.TabIndex = 78;
            this.lblPhone.Text = "Số điện thoại";
            // 
            // lblFullName
            // 
            this.lblFullName.Location = new System.Drawing.Point(26, 213);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(70, 16);
            this.lblFullName.TabIndex = 72;
            this.lblFullName.Text = "Họ và tên";
            // 
            // UCEmployeeAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnEditAvatar);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.ptbAvatar);
            this.Controls.Add(this.dtpBirthDate);
            this.Controls.Add(this.dtpHireDate);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.cbPosition);
            this.Controls.Add(this.txtSalary);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.lblHireDate);
            this.Controls.Add(this.lblSalary);
            this.Controls.Add(this.lblBirthDate);
            this.Controls.Add(this.cboPosition);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblFullName);
            this.Name = "UCEmployeeAdd";
            this.Size = new System.Drawing.Size(376, 479);
            ((System.ComponentModel.ISupportInitialize)(this.ptbAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnEditAvatar;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox ptbAvatar;
        private System.Windows.Forms.DateTimePicker dtpBirthDate;
        private System.Windows.Forms.DateTimePicker dtpHireDate;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.ComboBox cbPosition;
        private System.Windows.Forms.TextBox txtSalary;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblHireDate;
        private System.Windows.Forms.Label lblSalary;
        private System.Windows.Forms.Label lblBirthDate;
        private System.Windows.Forms.Label cboPosition;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblFullName;
    }
}

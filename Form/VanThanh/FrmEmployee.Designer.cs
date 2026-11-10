namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmEmployee
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
            this.dgvEmployee = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Control = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlEmployeeDetail = new System.Windows.Forms.Panel();
            this.btnRegisterShift = new System.Windows.Forms.Button();
            this.cboFilter = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColShiftTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMission = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEmployee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblShiftTitle = new System.Windows.Forms.Label();
            this.lblSearchEmp = new System.Windows.Forms.Label();
            this.dtpShiftDate = new System.Windows.Forms.DateTimePicker();
            this.cboShiftMission = new System.Windows.Forms.ComboBox();
            this.txtSearchShift = new System.Windows.Forms.TextBox();
            this.btnSearchShift = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEmployee
            // 
            this.dgvEmployee.AllowUserToAddRows = false;
            this.dgvEmployee.AllowUserToDeleteRows = false;
            this.dgvEmployee.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployee.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.FullName,
            this.Position,
            this.SDT,
            this.Control});
            this.dgvEmployee.Location = new System.Drawing.Point(53, 75);
            this.dgvEmployee.MultiSelect = false;
            this.dgvEmployee.Name = "dgvEmployee";
            this.dgvEmployee.ReadOnly = true;
            this.dgvEmployee.RowHeadersWidth = 51;
            this.dgvEmployee.RowTemplate.Height = 24;
            this.dgvEmployee.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvEmployee.Size = new System.Drawing.Size(730, 435);
            this.dgvEmployee.TabIndex = 0;
            this.dgvEmployee.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployee_CellClick);
            this.dgvEmployee.Leave += new System.EventHandler(this.dgvEmployee_Leave);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // FullName
            // 
            this.FullName.DataPropertyName = "FullName";
            this.FullName.HeaderText = "Họ và tên";
            this.FullName.MinimumWidth = 6;
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            // 
            // Position
            // 
            this.Position.DataPropertyName = "Position";
            this.Position.HeaderText = "Chức vụ";
            this.Position.MinimumWidth = 6;
            this.Position.Name = "Position";
            this.Position.ReadOnly = true;
            // 
            // SDT
            // 
            this.SDT.DataPropertyName = "Phone";
            this.SDT.HeaderText = "Số điện thoại";
            this.SDT.MinimumWidth = 6;
            this.SDT.Name = "SDT";
            this.SDT.ReadOnly = true;
            // 
            // Control
            // 
            this.Control.HeaderText = "Thao tác";
            this.Control.MinimumWidth = 6;
            this.Control.Name = "Control";
            this.Control.ReadOnly = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(648, 516);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(135, 30);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Thêm nhân viên";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(245, 25);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(260, 22);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(690, 23);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(93, 27);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlEmployeeDetail
            // 
            this.pnlEmployeeDetail.Location = new System.Drawing.Point(830, 75);
            this.pnlEmployeeDetail.Name = "pnlEmployeeDetail";
            this.pnlEmployeeDetail.Size = new System.Drawing.Size(465, 670);
            this.pnlEmployeeDetail.TabIndex = 5;
            // 
            // btnRegisterShift
            // 
            this.btnRegisterShift.Location = new System.Drawing.Point(57, 602);
            this.btnRegisterShift.Name = "btnRegisterShift";
            this.btnRegisterShift.Size = new System.Drawing.Size(173, 30);
            this.btnRegisterShift.TabIndex = 6;
            this.btnRegisterShift.Text = "Nhập ca làm tuần mới";
            this.btnRegisterShift.UseVisualStyleBackColor = true;
            this.btnRegisterShift.Click += new System.EventHandler(this.btnRegisterShift_Click);
            // 
            // cboFilter
            // 
            this.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Location = new System.Drawing.Point(520, 24);
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(155, 24);
            this.cboFilter.TabIndex = 23;
            this.cboFilter.SelectedIndexChanged += new System.EventHandler(this.cboFilter_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColShiftTime,
            this.ColMission,
            this.ColEmployee,
            this.ColStatus});
            this.dataGridView1.Location = new System.Drawing.Point(54, 644);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(729, 235);
            this.dataGridView1.TabIndex = 24;
            // 
            // ColShiftTime
            // 
            this.ColShiftTime.DataPropertyName = "ShiftName";
            this.ColShiftTime.FillWeight = 110F;
            this.ColShiftTime.HeaderText = "Ca làm";
            this.ColShiftTime.MinimumWidth = 6;
            this.ColShiftTime.Name = "ColShiftTime";
            this.ColShiftTime.ReadOnly = true;
            // 
            // ColMission
            // 
            this.ColMission.DataPropertyName = "MissionName";
            this.ColMission.FillWeight = 85F;
            this.ColMission.HeaderText = "Nhiệm vụ";
            this.ColMission.MinimumWidth = 6;
            this.ColMission.Name = "ColMission";
            this.ColMission.ReadOnly = true;
            // 
            // ColEmployee
            // 
            this.ColEmployee.DataPropertyName = "EmployeeName";
            this.ColEmployee.FillWeight = 120F;
            this.ColEmployee.HeaderText = "Nhân viên trực";
            this.ColEmployee.MinimumWidth = 6;
            this.ColEmployee.Name = "ColEmployee";
            this.ColEmployee.ReadOnly = true;
            // 
            // ColStatus
            // 
            this.ColStatus.DataPropertyName = "Status";
            this.ColStatus.FillWeight = 85F;
            this.ColStatus.HeaderText = "Trạng thái";
            this.ColStatus.MinimumWidth = 6;
            this.ColStatus.Name = "ColStatus";
            this.ColStatus.ReadOnly = true;
            // 
            // lblShiftTitle
            // 
            this.lblShiftTitle.AutoSize = true;
            this.lblShiftTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShiftTitle.Location = new System.Drawing.Point(316, 578);
            this.lblShiftTitle.Name = "lblShiftTitle";
            this.lblShiftTitle.Size = new System.Drawing.Size(135, 21);
            this.lblShiftTitle.TabIndex = 25;
            this.lblShiftTitle.Text = "📅 Ca làm ngày:";
            // 
            // lblSearchEmp
            // 
            this.lblSearchEmp.AutoSize = true;
            this.lblSearchEmp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchEmp.Location = new System.Drawing.Point(50, 27);
            this.lblSearchEmp.Name = "lblSearchEmp";
            this.lblSearchEmp.Size = new System.Drawing.Size(180, 20);
            this.lblSearchEmp.TabIndex = 26;
            this.lblSearchEmp.Text = "Tìm kiếm (Tên / SĐT / ID):";
            // 
            // dtpShiftDate
            // 
            this.dtpShiftDate.CustomFormat = "dd/MM/yyyy";
            this.dtpShiftDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpShiftDate.Location = new System.Drawing.Point(458, 576);
            this.dtpShiftDate.Name = "dtpShiftDate";
            this.dtpShiftDate.Size = new System.Drawing.Size(130, 22);
            this.dtpShiftDate.TabIndex = 27;
            this.dtpShiftDate.ValueChanged += new System.EventHandler(this.dtpShiftDate_ValueChanged);
            // 
            // cboShiftMission
            // 
            this.cboShiftMission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboShiftMission.FormattingEnabled = true;
            this.cboShiftMission.Location = new System.Drawing.Point(603, 575);
            this.cboShiftMission.Name = "cboShiftMission";
            this.cboShiftMission.Size = new System.Drawing.Size(180, 24);
            this.cboShiftMission.TabIndex = 28;
            this.cboShiftMission.SelectedIndexChanged += new System.EventHandler(this.cboShiftMission_SelectedIndexChanged);
            // 
            // txtSearchShift
            // 
            this.txtSearchShift.Location = new System.Drawing.Point(318, 610);
            this.txtSearchShift.Name = "txtSearchShift";
            this.txtSearchShift.Size = new System.Drawing.Size(370, 22);
            this.txtSearchShift.TabIndex = 29;
            this.txtSearchShift.TextChanged += new System.EventHandler(this.txtSearchShift_TextChanged);
            // 
            // btnSearchShift
            // 
            this.btnSearchShift.Location = new System.Drawing.Point(698, 607);
            this.btnSearchShift.Name = "btnSearchShift";
            this.btnSearchShift.Size = new System.Drawing.Size(85, 28);
            this.btnSearchShift.TabIndex = 30;
            this.btnSearchShift.Text = "Tìm kiếm";
            this.btnSearchShift.UseVisualStyleBackColor = true;
            this.btnSearchShift.Click += new System.EventHandler(this.btnSearchShift_Click);
            // 
            // FrmEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1348, 900);
            this.Controls.Add(this.btnSearchShift);
            this.Controls.Add(this.txtSearchShift);
            this.Controls.Add(this.cboShiftMission);
            this.Controls.Add(this.dtpShiftDate);
            this.Controls.Add(this.lblSearchEmp);
            this.Controls.Add(this.lblShiftTitle);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cboFilter);
            this.Controls.Add(this.btnRegisterShift);
            this.Controls.Add(this.pnlEmployeeDetail);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvEmployee);
            this.Name = "FrmEmployee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Nhân sự & Chấm công";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmEmployee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEmployee;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDT;
        private System.Windows.Forms.DataGridViewButtonColumn Control;
        private System.Windows.Forms.Panel pnlEmployeeDetail;
        private System.Windows.Forms.Button btnRegisterShift;
        private System.Windows.Forms.ComboBox cboFilter;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblShiftTitle;
        private System.Windows.Forms.Label lblSearchEmp;
        private System.Windows.Forms.DateTimePicker dtpShiftDate;
        private System.Windows.Forms.ComboBox cboShiftMission;
        private System.Windows.Forms.TextBox txtSearchShift;
        private System.Windows.Forms.Button btnSearchShift;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColShiftTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMission;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColEmployee;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColStatus;
    }
}

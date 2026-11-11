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
            System.Windows.Forms.DataGridViewCellStyle dgvHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvAltStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle shiftHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle shiftAltStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle shiftCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTopBanner = new System.Windows.Forms.Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.tabEmployeeMain = new System.Windows.Forms.TabControl();
            this.tabStaffList = new System.Windows.Forms.TabPage();
            this.pnlStaffContainer = new System.Windows.Forms.Panel();
            this.dgvEmployee = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Control = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlEmployeeDetail = new System.Windows.Forms.Panel();
            this.pnlStaffTop = new System.Windows.Forms.Panel();
            this.lblSearchEmp = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cboFilter = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tabShiftSchedule = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColShiftTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMission = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEmployee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlShiftTop = new System.Windows.Forms.Panel();
            this.lblShiftTitle = new System.Windows.Forms.Label();
            this.dtpShiftDate = new System.Windows.Forms.DateTimePicker();
            this.cboShiftMission = new System.Windows.Forms.ComboBox();
            this.txtSearchShift = new System.Windows.Forms.TextBox();
            this.btnSearchShift = new System.Windows.Forms.Button();
            this.btnRegisterShift = new System.Windows.Forms.Button();
            this.pnlTopBanner.SuspendLayout();
            this.tabEmployeeMain.SuspendLayout();
            this.tabStaffList.SuspendLayout();
            this.pnlStaffContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).BeginInit();
            this.pnlStaffTop.SuspendLayout();
            this.tabShiftSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnlShiftTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopBanner
            // 
            this.pnlTopBanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.pnlTopBanner.Controls.Add(this.lblFormTitle);
            this.pnlTopBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBanner.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBanner.Name = "pnlTopBanner";
            this.pnlTopBanner.Size = new System.Drawing.Size(1260, 48);
            this.pnlTopBanner.TabIndex = 0;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(0, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(1260, 48);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "HỆ THỐNG QUẢN LÝ NHÂN SỰ & CA LÀM VIỆC";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabEmployeeMain
            // 
            this.tabEmployeeMain.Controls.Add(this.tabStaffList);
            this.tabEmployeeMain.Controls.Add(this.tabShiftSchedule);
            this.tabEmployeeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabEmployeeMain.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tabEmployeeMain.ItemSize = new System.Drawing.Size(260, 38);
            this.tabEmployeeMain.Location = new System.Drawing.Point(0, 48);
            this.tabEmployeeMain.Name = "tabEmployeeMain";
            this.tabEmployeeMain.Padding = new System.Drawing.Point(16, 6);
            this.tabEmployeeMain.SelectedIndex = 0;
            this.tabEmployeeMain.Size = new System.Drawing.Size(1260, 752);
            this.tabEmployeeMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabEmployeeMain.TabIndex = 1;
            // 
            // tabStaffList
            // 
            this.tabStaffList.BackColor = System.Drawing.Color.White;
            this.tabStaffList.Controls.Add(this.pnlStaffContainer);
            this.tabStaffList.Controls.Add(this.pnlStaffTop);
            this.tabStaffList.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tabStaffList.Location = new System.Drawing.Point(4, 42);
            this.tabStaffList.Name = "tabStaffList";
            this.tabStaffList.Padding = new System.Windows.Forms.Padding(8);
            this.tabStaffList.Size = new System.Drawing.Size(1252, 706);
            this.tabStaffList.TabIndex = 0;
            this.tabStaffList.Text = "📋 Danh sách & Hồ sơ nhân viên";
            // 
            // pnlStaffContainer
            // 
            this.pnlStaffContainer.Controls.Add(this.dgvEmployee);
            this.pnlStaffContainer.Controls.Add(this.pnlEmployeeDetail);
            this.pnlStaffContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStaffContainer.Location = new System.Drawing.Point(8, 64);
            this.pnlStaffContainer.Name = "pnlStaffContainer";
            this.pnlStaffContainer.Size = new System.Drawing.Size(1236, 634);
            this.pnlStaffContainer.TabIndex = 1;
            // 
            // dgvEmployee
            // 
            this.dgvEmployee.AllowUserToAddRows = false;
            this.dgvEmployee.AllowUserToDeleteRows = false;
            this.dgvEmployee.AllowUserToResizeRows = false;
            dgvAltStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.dgvEmployee.AlternatingRowsDefaultCellStyle = dgvAltStyle;
            this.dgvEmployee.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmployee.BackgroundColor = System.Drawing.Color.White;
            this.dgvEmployee.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEmployee.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvEmployee.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dgvHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvHeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            dgvHeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dgvHeaderStyle.ForeColor = System.Drawing.Color.White;
            dgvHeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            dgvHeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvHeaderStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmployee.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
            this.dgvEmployee.ColumnHeadersHeight = 40;
            this.dgvEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvEmployee.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.FullName,
            this.Position,
            this.SDT,
            this.Control});
            dgvCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvCellStyle.BackColor = System.Drawing.Color.White;
            dgvCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dgvCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(224)))), ((int)(((byte)(210)))));
            dgvCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            dgvCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEmployee.DefaultCellStyle = dgvCellStyle;
            this.dgvEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmployee.EnableHeadersVisualStyles = false;
            this.dgvEmployee.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(230)))), ((int)(((byte)(225)))));
            this.dgvEmployee.Location = new System.Drawing.Point(0, 0);
            this.dgvEmployee.MultiSelect = false;
            this.dgvEmployee.Name = "dgvEmployee";
            this.dgvEmployee.ReadOnly = true;
            this.dgvEmployee.RowHeadersVisible = false;
            this.dgvEmployee.RowHeadersWidth = 51;
            this.dgvEmployee.RowTemplate.Height = 36;
            this.dgvEmployee.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployee.Size = new System.Drawing.Size(776, 634);
            this.dgvEmployee.TabIndex = 0;
            this.dgvEmployee.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployee_CellClick);
            this.dgvEmployee.Leave += new System.EventHandler(this.dgvEmployee_Leave);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.FillWeight = 12F;
            this.Id.HeaderText = "Mã NV";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // FullName
            // 
            this.FullName.DataPropertyName = "FullName";
            this.FullName.FillWeight = 38F;
            this.FullName.HeaderText = "Họ và tên";
            this.FullName.MinimumWidth = 6;
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            // 
            // Position
            // 
            this.Position.DataPropertyName = "Position";
            this.Position.FillWeight = 24F;
            this.Position.HeaderText = "Chức vụ";
            this.Position.MinimumWidth = 6;
            this.Position.Name = "Position";
            this.Position.ReadOnly = true;
            // 
            // SDT
            // 
            this.SDT.DataPropertyName = "Phone";
            this.SDT.FillWeight = 20F;
            this.SDT.HeaderText = "Số điện thoại";
            this.SDT.MinimumWidth = 6;
            this.SDT.Name = "SDT";
            this.SDT.ReadOnly = true;
            // 
            // Control
            // 
            this.Control.FillWeight = 12F;
            this.Control.HeaderText = "Thao tác";
            this.Control.MinimumWidth = 6;
            this.Control.Name = "Control";
            this.Control.ReadOnly = true;
            this.Control.Text = "⚙️";
            this.Control.UseColumnTextForButtonValue = true;
            // 
            // pnlEmployeeDetail
            // 
            this.pnlEmployeeDetail.BackColor = System.Drawing.Color.White;
            this.pnlEmployeeDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEmployeeDetail.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlEmployeeDetail.Location = new System.Drawing.Point(776, 0);
            this.pnlEmployeeDetail.Name = "pnlEmployeeDetail";
            this.pnlEmployeeDetail.Size = new System.Drawing.Size(460, 634);
            this.pnlEmployeeDetail.TabIndex = 1;
            this.pnlEmployeeDetail.Visible = false;
            // 
            // pnlStaffTop
            // 
            this.pnlStaffTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(248)))), ((int)(((byte)(245)))));
            this.pnlStaffTop.Controls.Add(this.lblSearchEmp);
            this.pnlStaffTop.Controls.Add(this.txtSearch);
            this.pnlStaffTop.Controls.Add(this.cboFilter);
            this.pnlStaffTop.Controls.Add(this.btnSearch);
            this.pnlStaffTop.Controls.Add(this.btnAdd);
            this.pnlStaffTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStaffTop.Location = new System.Drawing.Point(8, 8);
            this.pnlStaffTop.Name = "pnlStaffTop";
            this.pnlStaffTop.Size = new System.Drawing.Size(1236, 56);
            this.pnlStaffTop.TabIndex = 0;
            // 
            // lblSearchEmp
            // 
            this.lblSearchEmp.AutoSize = true;
            this.lblSearchEmp.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSearchEmp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblSearchEmp.Location = new System.Drawing.Point(12, 18);
            this.lblSearchEmp.Name = "lblSearchEmp";
            this.lblSearchEmp.Size = new System.Drawing.Size(89, 21);
            this.lblSearchEmp.TabIndex = 0;
            this.lblSearchEmp.Text = "🔍 Tìm kiếm:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearch.Location = new System.Drawing.Point(107, 14);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(250, 29);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // cboFilter
            // 
            this.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Location = new System.Drawing.Point(367, 14);
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(185, 29);
            this.cboFilter.TabIndex = 2;
            this.cboFilter.SelectedIndexChanged += new System.EventHandler(this.cboFilter_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(78)))), ((int)(((byte)(55)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(562, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 35);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(672, 11);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(160, 35);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "+ Thêm nhân viên";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tabShiftSchedule
            // 
            this.tabShiftSchedule.BackColor = System.Drawing.Color.White;
            this.tabShiftSchedule.Controls.Add(this.dataGridView1);
            this.tabShiftSchedule.Controls.Add(this.pnlShiftTop);
            this.tabShiftSchedule.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tabShiftSchedule.Location = new System.Drawing.Point(4, 42);
            this.tabShiftSchedule.Name = "tabShiftSchedule";
            this.tabShiftSchedule.Padding = new System.Windows.Forms.Padding(8);
            this.tabShiftSchedule.Size = new System.Drawing.Size(1252, 706);
            this.tabShiftSchedule.TabIndex = 1;
            this.tabShiftSchedule.Text = "📅 Lịch trực ca làm việc trong ngày";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            shiftAltStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = shiftAltStyle;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            shiftHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            shiftHeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            shiftHeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            shiftHeaderStyle.ForeColor = System.Drawing.Color.White;
            shiftHeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            shiftHeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            shiftHeaderStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = shiftHeaderStyle;
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColShiftTime,
            this.ColMission,
            this.ColEmployee,
            this.ColStatus});
            shiftCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            shiftCellStyle.BackColor = System.Drawing.Color.White;
            shiftCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            shiftCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            shiftCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(224)))), ((int)(((byte)(210)))));
            shiftCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            shiftCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = shiftCellStyle;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(230)))), ((int)(((byte)(225)))));
            this.dataGridView1.Location = new System.Drawing.Point(8, 64);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 36;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1236, 634);
            this.dataGridView1.TabIndex = 1;
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
            // pnlShiftTop
            // 
            this.pnlShiftTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(248)))), ((int)(((byte)(245)))));
            this.pnlShiftTop.Controls.Add(this.lblShiftTitle);
            this.pnlShiftTop.Controls.Add(this.dtpShiftDate);
            this.pnlShiftTop.Controls.Add(this.cboShiftMission);
            this.pnlShiftTop.Controls.Add(this.txtSearchShift);
            this.pnlShiftTop.Controls.Add(this.btnSearchShift);
            this.pnlShiftTop.Controls.Add(this.btnRegisterShift);
            this.pnlShiftTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlShiftTop.Location = new System.Drawing.Point(8, 8);
            this.pnlShiftTop.Name = "pnlShiftTop";
            this.pnlShiftTop.Size = new System.Drawing.Size(1236, 56);
            this.pnlShiftTop.TabIndex = 0;
            // 
            // lblShiftTitle
            // 
            this.lblShiftTitle.AutoSize = true;
            this.lblShiftTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblShiftTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblShiftTitle.Location = new System.Drawing.Point(12, 18);
            this.lblShiftTitle.Name = "lblShiftTitle";
            this.lblShiftTitle.Size = new System.Drawing.Size(123, 21);
            this.lblShiftTitle.TabIndex = 0;
            this.lblShiftTitle.Text = "📅 Ca làm ngày:";
            // 
            // dtpShiftDate
            // 
            this.dtpShiftDate.CustomFormat = "dd/MM/yyyy";
            this.dtpShiftDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpShiftDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpShiftDate.Location = new System.Drawing.Point(145, 14);
            this.dtpShiftDate.Name = "dtpShiftDate";
            this.dtpShiftDate.Size = new System.Drawing.Size(130, 29);
            this.dtpShiftDate.TabIndex = 1;
            this.dtpShiftDate.ValueChanged += new System.EventHandler(this.dtpShiftDate_ValueChanged);
            // 
            // cboShiftMission
            // 
            this.cboShiftMission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboShiftMission.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboShiftMission.FormattingEnabled = true;
            this.cboShiftMission.Location = new System.Drawing.Point(285, 14);
            this.cboShiftMission.Name = "cboShiftMission";
            this.cboShiftMission.Size = new System.Drawing.Size(180, 29);
            this.cboShiftMission.TabIndex = 2;
            this.cboShiftMission.SelectedIndexChanged += new System.EventHandler(this.cboShiftMission_SelectedIndexChanged);
            // 
            // txtSearchShift
            // 
            this.txtSearchShift.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearchShift.Location = new System.Drawing.Point(475, 14);
            this.txtSearchShift.Name = "txtSearchShift";
            this.txtSearchShift.Size = new System.Drawing.Size(220, 29);
            this.txtSearchShift.TabIndex = 3;
            this.txtSearchShift.TextChanged += new System.EventHandler(this.txtSearchShift_TextChanged);
            // 
            // btnSearchShift
            // 
            this.btnSearchShift.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(78)))), ((int)(((byte)(55)))));
            this.btnSearchShift.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchShift.FlatAppearance.BorderSize = 0;
            this.btnSearchShift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchShift.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSearchShift.ForeColor = System.Drawing.Color.White;
            this.btnSearchShift.Location = new System.Drawing.Point(705, 11);
            this.btnSearchShift.Name = "btnSearchShift";
            this.btnSearchShift.Size = new System.Drawing.Size(95, 35);
            this.btnSearchShift.TabIndex = 4;
            this.btnSearchShift.Text = "Tìm kiếm";
            this.btnSearchShift.UseVisualStyleBackColor = false;
            this.btnSearchShift.Click += new System.EventHandler(this.btnSearchShift_Click);
            // 
            // btnRegisterShift
            // 
            this.btnRegisterShift.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnRegisterShift.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegisterShift.FlatAppearance.BorderSize = 0;
            this.btnRegisterShift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegisterShift.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRegisterShift.ForeColor = System.Drawing.Color.White;
            this.btnRegisterShift.Location = new System.Drawing.Point(810, 11);
            this.btnRegisterShift.Name = "btnRegisterShift";
            this.btnRegisterShift.Size = new System.Drawing.Size(210, 35);
            this.btnRegisterShift.TabIndex = 5;
            this.btnRegisterShift.Text = "📅 Nhập ca làm tuần mới";
            this.btnRegisterShift.UseVisualStyleBackColor = false;
            this.btnRegisterShift.Click += new System.EventHandler(this.btnRegisterShift_Click);
            // 
            // FrmEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(248)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1260, 800);
            this.Controls.Add(this.tabEmployeeMain);
            this.Controls.Add(this.pnlTopBanner);
            this.Name = "FrmEmployee";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Nhân sự & Chấm công";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmEmployee_Load);
            this.pnlTopBanner.ResumeLayout(false);
            this.tabEmployeeMain.ResumeLayout(false);
            this.tabStaffList.ResumeLayout(false);
            this.pnlStaffContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployee)).EndInit();
            this.pnlStaffTop.ResumeLayout(false);
            this.pnlStaffTop.PerformLayout();
            this.tabShiftSchedule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pnlShiftTop.ResumeLayout(false);
            this.pnlShiftTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopBanner;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.TabControl tabEmployeeMain;
        private System.Windows.Forms.TabPage tabStaffList;
        private System.Windows.Forms.Panel pnlStaffTop;
        private System.Windows.Forms.Panel pnlStaffContainer;
        private System.Windows.Forms.TabPage tabShiftSchedule;
        private System.Windows.Forms.Panel pnlShiftTop;

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

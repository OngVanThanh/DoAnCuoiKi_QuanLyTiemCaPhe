namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmOrder
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.lblSelectedTableBanner = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.pnlLeftTables = new System.Windows.Forms.Panel();
            this.flpTableList = new System.Windows.Forms.FlowLayoutPanel();
            this.lblEmptyReservedNotice = new System.Windows.Forms.Label();
            this.lblReservedListHeader = new System.Windows.Forms.Label();
            this.grpQuickReserve = new System.Windows.Forms.GroupBox();
            this.btnAddReservedTable = new System.Windows.Forms.Button();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.cboReserveArea = new System.Windows.Forms.ComboBox();
            this.lblReserveArea = new System.Windows.Forms.Label();
            this.txtReserveCustomerName = new System.Windows.Forms.TextBox();
            this.lblReserveName = new System.Windows.Forms.Label();
            this.btnCounterOrder = new System.Windows.Forms.Button();
            this.pnlRightOrder = new System.Windows.Forms.Panel();
            this.dgvOrderDetails = new System.Windows.Forms.DataGridView();
            this.colDetailProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailTotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlOrderFooter = new System.Windows.Forms.Panel();
            this.lblChangeDue = new System.Windows.Forms.Label();
            this.lblChangeDueTitle = new System.Windows.Forms.Label();
            this.btnCash500k = new System.Windows.Forms.Button();
            this.btnCash200k = new System.Windows.Forms.Button();
            this.btnCash100k = new System.Windows.Forms.Button();
            this.btnCash50k = new System.Windows.Forms.Button();
            this.btnCashExact = new System.Windows.Forms.Button();
            this.txtCashTendered = new System.Windows.Forms.TextBox();
            this.lblCashTendered = new System.Windows.Forms.Label();
            this.lstCustomerSuggest = new System.Windows.Forms.ListBox();
            this.lstPromoSuggest = new System.Windows.Forms.ListBox();
            this.btnPayAndPrint = new System.Windows.Forms.Button();
            this.pnlSummaryBox = new System.Windows.Forms.Panel();
            this.lblFinalAmount = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.btnRemoveProduct = new System.Windows.Forms.Button();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblCustomerInfo = new System.Windows.Forms.Label();
            this.txtCustomerPhone = new System.Windows.Forms.TextBox();
            this.lblCustomerPhone = new System.Windows.Forms.Label();
            this.pnlOrderHeader = new System.Windows.Forms.Panel();
            this.lblOrderStatusBadge = new System.Windows.Forms.Label();
            this.lblOrderHeaderTitle = new System.Windows.Forms.Label();
            this.pnlCenterMenu = new System.Windows.Forms.Panel();
            this.flpProductMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlMenuFilter = new System.Windows.Forms.Panel();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.cboCategoryFilter = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.tmrAutoExpire = new System.Windows.Forms.Timer(this.components);
            this.pnlTopBar.SuspendLayout();
            this.pnlLeftTables.SuspendLayout();
            this.grpQuickReserve.SuspendLayout();
            this.pnlRightOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).BeginInit();
            this.pnlOrderFooter.SuspendLayout();
            this.pnlSummaryBox.SuspendLayout();
            this.pnlOrderHeader.SuspendLayout();
            this.pnlCenterMenu.SuspendLayout();
            this.pnlMenuFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.pnlTopBar.Controls.Add(this.lblSelectedTableBanner);
            this.pnlTopBar.Controls.Add(this.lblHeaderTitle);
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Padding = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.pnlTopBar.Size = new System.Drawing.Size(1280, 50);
            this.pnlTopBar.TabIndex = 0;
            // 
            // lblSelectedTableBanner
            // 
            this.lblSelectedTableBanner.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblSelectedTableBanner.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSelectedTableBanner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(130)))));
            this.lblSelectedTableBanner.Location = new System.Drawing.Point(804, 0);
            this.lblSelectedTableBanner.Name = "lblSelectedTableBanner";
            this.lblSelectedTableBanner.Size = new System.Drawing.Size(460, 50);
            this.lblSelectedTableBanner.TabIndex = 1;
            this.lblSelectedTableBanner.Text = "Chế độ: Khách tại quầy / Mang đi";
            this.lblSelectedTableBanner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(16, 0);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(387, 30);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "POS BÁN HÀNG & ĐẶT BÀN TRƯỚC";
            this.lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHeaderTitle.UseMnemonic = false;
            // 
            // pnlLeftTables
            // 
            this.pnlLeftTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.pnlLeftTables.Controls.Add(this.flpTableList);
            this.pnlLeftTables.Controls.Add(this.lblReservedListHeader);
            this.pnlLeftTables.Controls.Add(this.grpQuickReserve);
            this.pnlLeftTables.Controls.Add(this.btnCounterOrder);
            this.pnlLeftTables.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeftTables.Location = new System.Drawing.Point(0, 50);
            this.pnlLeftTables.Name = "pnlLeftTables";
            this.pnlLeftTables.Padding = new System.Windows.Forms.Padding(10);
            this.pnlLeftTables.Size = new System.Drawing.Size(305, 670);
            this.pnlLeftTables.TabIndex = 1;
            // 
            // flpTableList
            // 
            this.flpTableList.AutoScroll = true;
            this.flpTableList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.flpTableList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTableList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTableList.Location = new System.Drawing.Point(10, 298);
            this.flpTableList.Name = "flpTableList";
            this.flpTableList.Padding = new System.Windows.Forms.Padding(2);
            this.flpTableList.Size = new System.Drawing.Size(285, 362);
            this.flpTableList.TabIndex = 3;
            this.flpTableList.WrapContents = false;
            // 
            // lblEmptyReservedNotice
            // 
            this.lblEmptyReservedNotice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblEmptyReservedNotice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblEmptyReservedNotice.Location = new System.Drawing.Point(5, 10);
            this.lblEmptyReservedNotice.Name = "lblEmptyReservedNotice";
            this.lblEmptyReservedNotice.Size = new System.Drawing.Size(265, 80);
            this.lblEmptyReservedNotice.TabIndex = 0;
            this.lblEmptyReservedNotice.Text = "Chưa có bàn đặt trước nào.\r\nNhập thông tin bên trên và bấm \"+ Thêm bàn đặt trước\" khi có yêu cầu.";
            this.lblEmptyReservedNotice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReservedListHeader
            // 
            this.lblReservedListHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReservedListHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblReservedListHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblReservedListHeader.Location = new System.Drawing.Point(10, 266);
            this.lblReservedListHeader.Name = "lblReservedListHeader";
            this.lblReservedListHeader.Padding = new System.Windows.Forms.Padding(0, 8, 0, 4);
            this.lblReservedListHeader.Size = new System.Drawing.Size(285, 32);
            this.lblReservedListHeader.TabIndex = 2;
            this.lblReservedListHeader.Text = "DANH SÁCH BÀN ĐẶT TRƯỚC (0)";
            // 
            // grpQuickReserve
            // 
            this.grpQuickReserve.BackColor = System.Drawing.Color.White;
            this.grpQuickReserve.Controls.Add(this.btnAddReservedTable);
            this.grpQuickReserve.Controls.Add(this.dtpEndTime);
            this.grpQuickReserve.Controls.Add(this.lblEndTime);
            this.grpQuickReserve.Controls.Add(this.dtpStartTime);
            this.grpQuickReserve.Controls.Add(this.lblStartTime);
            this.grpQuickReserve.Controls.Add(this.cboReserveArea);
            this.grpQuickReserve.Controls.Add(this.lblReserveArea);
            this.grpQuickReserve.Controls.Add(this.txtReserveCustomerName);
            this.grpQuickReserve.Controls.Add(this.lblReserveName);
            this.grpQuickReserve.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpQuickReserve.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpQuickReserve.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.grpQuickReserve.Location = new System.Drawing.Point(10, 54);
            this.grpQuickReserve.Name = "grpQuickReserve";
            this.grpQuickReserve.Size = new System.Drawing.Size(285, 212);
            this.grpQuickReserve.TabIndex = 1;
            this.grpQuickReserve.TabStop = false;
            this.grpQuickReserve.Text = "Tạo bàn đặt trước (Tự hủy khi hết giờ)";
            // 
            // btnAddReservedTable
            // 
            this.btnAddReservedTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(78)))), ((int)(((byte)(55)))));
            this.btnAddReservedTable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddReservedTable.FlatAppearance.BorderSize = 0;
            this.btnAddReservedTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddReservedTable.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddReservedTable.ForeColor = System.Drawing.Color.White;
            this.btnAddReservedTable.Location = new System.Drawing.Point(10, 164);
            this.btnAddReservedTable.Name = "btnAddReservedTable";
            this.btnAddReservedTable.Size = new System.Drawing.Size(264, 36);
            this.btnAddReservedTable.TabIndex = 8;
            this.btnAddReservedTable.Text = "+ Thêm bàn đặt trước";
            this.btnAddReservedTable.UseVisualStyleBackColor = false;
            this.btnAddReservedTable.Click += new System.EventHandler(this.btnAddReservedTable_Click);
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CustomFormat = "HH:mm dd/MM/yyyy";
            this.dtpEndTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(112, 126);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.ShowUpDown = true;
            this.dtpEndTime.Size = new System.Drawing.Size(162, 27);
            this.dtpEndTime.TabIndex = 7;
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblEndTime.Location = new System.Drawing.Point(8, 129);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(65, 20);
            this.lblEndTime.TabIndex = 6;
            this.lblEndTime.Text = "Đến giờ:";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CustomFormat = "HH:mm dd/MM/yyyy";
            this.dtpStartTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(112, 92);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.ShowUpDown = true;
            this.dtpStartTime.Size = new System.Drawing.Size(162, 27);
            this.dtpStartTime.TabIndex = 5;
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStartTime.Location = new System.Drawing.Point(8, 95);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(73, 20);
            this.lblStartTime.TabIndex = 4;
            this.lblStartTime.Text = "Từ khung:";
            // 
            // cboReserveArea
            // 
            this.cboReserveArea.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboReserveArea.FormattingEnabled = true;
            this.cboReserveArea.Location = new System.Drawing.Point(112, 58);
            this.cboReserveArea.Name = "cboReserveArea";
            this.cboReserveArea.Size = new System.Drawing.Size(162, 28);
            this.cboReserveArea.TabIndex = 3;
            // 
            // lblReserveArea
            // 
            this.lblReserveArea.AutoSize = true;
            this.lblReserveArea.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblReserveArea.Location = new System.Drawing.Point(8, 61);
            this.lblReserveArea.Name = "lblReserveArea";
            this.lblReserveArea.Size = new System.Drawing.Size(64, 20);
            this.lblReserveArea.TabIndex = 2;
            this.lblReserveArea.Text = "Khu vực:";
            // 
            // txtReserveCustomerName
            // 
            this.txtReserveCustomerName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtReserveCustomerName.Location = new System.Drawing.Point(112, 24);
            this.txtReserveCustomerName.Name = "txtReserveCustomerName";
            this.txtReserveCustomerName.Size = new System.Drawing.Size(162, 27);
            this.txtReserveCustomerName.TabIndex = 1;
            // 
            // lblReserveName
            // 
            this.lblReserveName.AutoSize = true;
            this.lblReserveName.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblReserveName.Location = new System.Drawing.Point(8, 27);
            this.lblReserveName.Name = "lblReserveName";
            this.lblReserveName.Size = new System.Drawing.Size(114, 20);
            this.lblReserveName.TabIndex = 0;
            this.lblReserveName.Text = "Tên khách hàng:";
            // 
            // btnCounterOrder
            // 
            this.btnCounterOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnCounterOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCounterOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCounterOrder.FlatAppearance.BorderSize = 0;
            this.btnCounterOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCounterOrder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCounterOrder.ForeColor = System.Drawing.Color.White;
            this.btnCounterOrder.Location = new System.Drawing.Point(10, 10);
            this.btnCounterOrder.Name = "btnCounterOrder";
            this.btnCounterOrder.Size = new System.Drawing.Size(285, 44);
            this.btnCounterOrder.TabIndex = 0;
            this.btnCounterOrder.Text = "⚡ KHÁCH TẠI QUẦY / MANG ĐI";
            this.btnCounterOrder.UseVisualStyleBackColor = false;
            this.btnCounterOrder.Click += new System.EventHandler(this.btnCounterOrder_Click);
            // 
            // pnlRightOrder
            // 
            this.pnlRightOrder.BackColor = System.Drawing.Color.White;
            this.pnlRightOrder.Controls.Add(this.dgvOrderDetails);
            this.pnlRightOrder.Controls.Add(this.pnlOrderFooter);
            this.pnlRightOrder.Controls.Add(this.pnlOrderHeader);
            this.pnlRightOrder.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRightOrder.Location = new System.Drawing.Point(865, 50);
            this.pnlRightOrder.Name = "pnlRightOrder";
            this.pnlRightOrder.Padding = new System.Windows.Forms.Padding(10);
            this.pnlRightOrder.Size = new System.Drawing.Size(415, 670);
            this.pnlRightOrder.TabIndex = 2;
            // 
            // dgvOrderDetails
            // 
            this.dgvOrderDetails.AllowUserToAddRows = false;
            this.dgvOrderDetails.AllowUserToDeleteRows = false;
            this.dgvOrderDetails.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(249)))), ((int)(((byte)(245)))));
            this.dgvOrderDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOrderDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrderDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrderDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvOrderDetails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.dgvOrderDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvOrderDetails.ColumnHeadersHeight = 36;
            this.dgvOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvOrderDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDetailProductId,
            this.colDetailProductName,
            this.colDetailUnit,
            this.colDetailQuantity,
            this.colDetailUnitPrice,
            this.colDetailTotalPrice});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(224)))), ((int)(((byte)(210)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOrderDetails.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderDetails.EnableHeadersVisualStyles = false;
            this.dgvOrderDetails.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(230)))), ((int)(((byte)(225)))));
            this.dgvOrderDetails.Location = new System.Drawing.Point(10, 66);
            this.dgvOrderDetails.MultiSelect = false;
            this.dgvOrderDetails.Name = "dgvOrderDetails";
            this.dgvOrderDetails.ReadOnly = true;
            this.dgvOrderDetails.RowHeadersVisible = false;
            this.dgvOrderDetails.RowHeadersWidth = 51;
            this.dgvOrderDetails.RowTemplate.Height = 32;
            this.dgvOrderDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderDetails.Size = new System.Drawing.Size(395, 364);
            this.dgvOrderDetails.TabIndex = 1;
            // 
            // colDetailProductId
            // 
            this.colDetailProductId.DataPropertyName = "ProductId";
            this.colDetailProductId.HeaderText = "Mã món";
            this.colDetailProductId.MinimumWidth = 6;
            this.colDetailProductId.Name = "colDetailProductId";
            this.colDetailProductId.ReadOnly = true;
            this.colDetailProductId.Visible = false;
            // 
            // colDetailProductName
            // 
            this.colDetailProductName.DataPropertyName = "ProductName";
            this.colDetailProductName.FillWeight = 38F;
            this.colDetailProductName.HeaderText = "Tên món";
            this.colDetailProductName.MinimumWidth = 6;
            this.colDetailProductName.Name = "colDetailProductName";
            this.colDetailProductName.ReadOnly = true;
            // 
            // colDetailUnit
            // 
            this.colDetailUnit.DataPropertyName = "Unit";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colDetailUnit.DefaultCellStyle = dataGridViewCellStyle3;
            this.colDetailUnit.FillWeight = 13F;
            this.colDetailUnit.HeaderText = "ĐVT";
            this.colDetailUnit.MinimumWidth = 6;
            this.colDetailUnit.Name = "colDetailUnit";
            this.colDetailUnit.ReadOnly = true;
            // 
            // colDetailQuantity
            // 
            this.colDetailQuantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colDetailQuantity.DefaultCellStyle = dataGridViewCellStyle4;
            this.colDetailQuantity.FillWeight = 12F;
            this.colDetailQuantity.HeaderText = "SL";
            this.colDetailQuantity.MinimumWidth = 6;
            this.colDetailQuantity.Name = "colDetailQuantity";
            this.colDetailQuantity.ReadOnly = true;
            // 
            // colDetailUnitPrice
            // 
            this.colDetailUnitPrice.DataPropertyName = "UnitPrice";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            this.colDetailUnitPrice.DefaultCellStyle = dataGridViewCellStyle5;
            this.colDetailUnitPrice.FillWeight = 18F;
            this.colDetailUnitPrice.HeaderText = "Đơn giá";
            this.colDetailUnitPrice.MinimumWidth = 6;
            this.colDetailUnitPrice.Name = "colDetailUnitPrice";
            this.colDetailUnitPrice.ReadOnly = true;
            // 
            // colDetailTotalPrice
            // 
            this.colDetailTotalPrice.DataPropertyName = "TotalPrice";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            this.colDetailTotalPrice.DefaultCellStyle = dataGridViewCellStyle6;
            this.colDetailTotalPrice.FillWeight = 19F;
            this.colDetailTotalPrice.HeaderText = "Thành tiền";
            this.colDetailTotalPrice.MinimumWidth = 6;
            this.colDetailTotalPrice.Name = "colDetailTotalPrice";
            this.colDetailTotalPrice.ReadOnly = true;
            // 
            // pnlOrderFooter
            // 
            this.pnlOrderFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.pnlOrderFooter.Controls.Add(this.lblChangeDue);
            this.pnlOrderFooter.Controls.Add(this.lblChangeDueTitle);
            this.pnlOrderFooter.Controls.Add(this.btnCash500k);
            this.pnlOrderFooter.Controls.Add(this.btnCash200k);
            this.pnlOrderFooter.Controls.Add(this.btnCash100k);
            this.pnlOrderFooter.Controls.Add(this.btnCash50k);
            this.pnlOrderFooter.Controls.Add(this.btnCashExact);
            this.pnlOrderFooter.Controls.Add(this.txtCashTendered);
            this.pnlOrderFooter.Controls.Add(this.lblCashTendered);
            this.pnlOrderFooter.Controls.Add(this.lstCustomerSuggest);
            this.pnlOrderFooter.Controls.Add(this.lstPromoSuggest);
            this.pnlOrderFooter.Controls.Add(this.btnPayAndPrint);
            this.pnlOrderFooter.Controls.Add(this.pnlSummaryBox);
            this.pnlOrderFooter.Controls.Add(this.btnRemoveProduct);
            this.pnlOrderFooter.Controls.Add(this.txtDiscount);
            this.pnlOrderFooter.Controls.Add(this.lblDiscount);
            this.pnlOrderFooter.Controls.Add(this.lblCustomerInfo);
            this.pnlOrderFooter.Controls.Add(this.txtCustomerPhone);
            this.pnlOrderFooter.Controls.Add(this.lblCustomerPhone);
            this.pnlOrderFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOrderFooter.Location = new System.Drawing.Point(10, 360);
            this.pnlOrderFooter.Name = "pnlOrderFooter";
            this.pnlOrderFooter.Padding = new System.Windows.Forms.Padding(8);
            this.pnlOrderFooter.Size = new System.Drawing.Size(395, 300);
            this.pnlOrderFooter.TabIndex = 2;
            // 
            // lstCustomerSuggest
            // 
            this.lstCustomerSuggest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(253)))), ((int)(((byte)(248)))));
            this.lstCustomerSuggest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstCustomerSuggest.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lstCustomerSuggest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lstCustomerSuggest.FormattingEnabled = true;
            this.lstCustomerSuggest.ItemHeight = 19;
            this.lstCustomerSuggest.Location = new System.Drawing.Point(80, 35);
            this.lstCustomerSuggest.Name = "lstCustomerSuggest";
            this.lstCustomerSuggest.Size = new System.Drawing.Size(306, 78);
            this.lstCustomerSuggest.TabIndex = 9;
            this.lstCustomerSuggest.Visible = false;
            this.lstCustomerSuggest.Click += new System.EventHandler(this.lstCustomerSuggest_Click);
            // 
            // lstPromoSuggest
            // 
            this.lstPromoSuggest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(253)))), ((int)(((byte)(248)))));
            this.lstPromoSuggest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstPromoSuggest.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lstPromoSuggest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lstPromoSuggest.FormattingEnabled = true;
            this.lstPromoSuggest.ItemHeight = 19;
            this.lstPromoSuggest.Location = new System.Drawing.Point(80, 87);
            this.lstPromoSuggest.Name = "lstPromoSuggest";
            this.lstPromoSuggest.Size = new System.Drawing.Size(210, 59);
            this.lstPromoSuggest.TabIndex = 10;
            this.lstPromoSuggest.Visible = false;
            this.lstPromoSuggest.Click += new System.EventHandler(this.lstPromoSuggest_Click);
            // 
            // 
            // pnlSummaryBox
            // 
            this.pnlSummaryBox.BackColor = System.Drawing.Color.White;
            this.pnlSummaryBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSummaryBox.Controls.Add(this.lblFinalAmount);
            this.pnlSummaryBox.Controls.Add(this.lblTotalAmount);
            this.pnlSummaryBox.Location = new System.Drawing.Point(8, 93);
            this.pnlSummaryBox.Name = "pnlSummaryBox";
            this.pnlSummaryBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.pnlSummaryBox.Size = new System.Drawing.Size(378, 60);
            this.pnlSummaryBox.TabIndex = 7;
            // 
            // lblCashTendered
            // 
            this.lblCashTendered.AutoSize = true;
            this.lblCashTendered.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblCashTendered.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblCashTendered.Location = new System.Drawing.Point(8, 161);
            this.lblCashTendered.Name = "lblCashTendered";
            this.lblCashTendered.Size = new System.Drawing.Size(117, 20);
            this.lblCashTendered.TabIndex = 11;
            this.lblCashTendered.Text = "Tiền khách đưa:";
            // 
            // txtCashTendered
            // 
            this.txtCashTendered.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtCashTendered.Location = new System.Drawing.Point(125, 157);
            this.txtCashTendered.Name = "txtCashTendered";
            this.txtCashTendered.Size = new System.Drawing.Size(155, 29);
            this.txtCashTendered.TabIndex = 12;
            this.txtCashTendered.TextChanged += new System.EventHandler(this.txtCashTendered_TextChanged);
            // 
            // btnCashExact
            // 
            this.btnCashExact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnCashExact.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCashExact.FlatAppearance.BorderSize = 0;
            this.btnCashExact.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCashExact.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnCashExact.ForeColor = System.Drawing.Color.White;
            this.btnCashExact.Location = new System.Drawing.Point(286, 157);
            this.btnCashExact.Name = "btnCashExact";
            this.btnCashExact.Size = new System.Drawing.Size(46, 29);
            this.btnCashExact.TabIndex = 13;
            this.btnCashExact.Text = "Đủ";
            this.btnCashExact.UseVisualStyleBackColor = false;
            this.btnCashExact.Click += new System.EventHandler(this.btnCashExact_Click);
            // 
            // btnCash50k
            // 
            this.btnCash50k.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.btnCash50k.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCash50k.FlatAppearance.BorderSize = 0;
            this.btnCash50k.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCash50k.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnCash50k.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnCash50k.Location = new System.Drawing.Point(336, 157);
            this.btnCash50k.Name = "btnCash50k";
            this.btnCash50k.Size = new System.Drawing.Size(50, 29);
            this.btnCash50k.TabIndex = 14;
            this.btnCash50k.Text = "50k";
            this.btnCash50k.UseVisualStyleBackColor = false;
            this.btnCash50k.Click += new System.EventHandler(this.btnCashQuick_Click);
            // 
            // btnCash100k
            // 
            this.btnCash100k.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.btnCash100k.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCash100k.FlatAppearance.BorderSize = 0;
            this.btnCash100k.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCash100k.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnCash100k.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnCash100k.Location = new System.Drawing.Point(125, 190);
            this.btnCash100k.Name = "btnCash100k";
            this.btnCash100k.Size = new System.Drawing.Size(60, 26);
            this.btnCash100k.TabIndex = 15;
            this.btnCash100k.Text = "100k";
            this.btnCash100k.UseVisualStyleBackColor = false;
            this.btnCash100k.Click += new System.EventHandler(this.btnCashQuick_Click);
            // 
            // btnCash200k
            // 
            this.btnCash200k.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.btnCash200k.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCash200k.FlatAppearance.BorderSize = 0;
            this.btnCash200k.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCash200k.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnCash200k.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnCash200k.Location = new System.Drawing.Point(191, 190);
            this.btnCash200k.Name = "btnCash200k";
            this.btnCash200k.Size = new System.Drawing.Size(60, 26);
            this.btnCash200k.TabIndex = 16;
            this.btnCash200k.Text = "200k";
            this.btnCash200k.UseVisualStyleBackColor = false;
            this.btnCash200k.Click += new System.EventHandler(this.btnCashQuick_Click);
            // 
            // btnCash500k
            // 
            this.btnCash500k.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.btnCash500k.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCash500k.FlatAppearance.BorderSize = 0;
            this.btnCash500k.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCash500k.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnCash500k.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnCash500k.Location = new System.Drawing.Point(257, 190);
            this.btnCash500k.Name = "btnCash500k";
            this.btnCash500k.Size = new System.Drawing.Size(60, 26);
            this.btnCash500k.TabIndex = 17;
            this.btnCash500k.Text = "500k";
            this.btnCash500k.UseVisualStyleBackColor = false;
            this.btnCash500k.Click += new System.EventHandler(this.btnCashQuick_Click);
            // 
            // lblChangeDueTitle
            // 
            this.lblChangeDueTitle.AutoSize = true;
            this.lblChangeDueTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblChangeDueTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblChangeDueTitle.Location = new System.Drawing.Point(8, 222);
            this.lblChangeDueTitle.Name = "lblChangeDueTitle";
            this.lblChangeDueTitle.Size = new System.Drawing.Size(95, 20);
            this.lblChangeDueTitle.TabIndex = 18;
            this.lblChangeDueTitle.Text = "Tiền thối lại:";
            // 
            // lblChangeDue
            // 
            this.lblChangeDue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblChangeDue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblChangeDue.Location = new System.Drawing.Point(125, 219);
            this.lblChangeDue.Name = "lblChangeDue";
            this.lblChangeDue.Size = new System.Drawing.Size(261, 25);
            this.lblChangeDue.TabIndex = 19;
            this.lblChangeDue.Text = "0 đ";
            this.lblChangeDue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPayAndPrint
            // 
            this.btnPayAndPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnPayAndPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPayAndPrint.FlatAppearance.BorderSize = 0;
            this.btnPayAndPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayAndPrint.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnPayAndPrint.ForeColor = System.Drawing.Color.White;
            this.btnPayAndPrint.Location = new System.Drawing.Point(8, 248);
            this.btnPayAndPrint.Name = "btnPayAndPrint";
            this.btnPayAndPrint.Size = new System.Drawing.Size(378, 44);
            this.btnPayAndPrint.TabIndex = 8;
            this.btnPayAndPrint.Text = "THANH TOÁN & IN HÓA ĐƠN (K80)";
            this.btnPayAndPrint.UseMnemonic = false;
            this.btnPayAndPrint.UseVisualStyleBackColor = false;
            this.btnPayAndPrint.Click += new System.EventHandler(this.btnPayAndPrint_Click);
            // 
            // lblFinalAmount
            // 
            this.lblFinalAmount.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFinalAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFinalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.lblFinalAmount.Location = new System.Drawing.Point(8, 30);
            this.lblFinalAmount.Name = "lblFinalAmount";
            this.lblFinalAmount.Size = new System.Drawing.Size(360, 32);
            this.lblFinalAmount.TabIndex = 1;
            this.lblFinalAmount.Text = "Thanh toán: 0 đ";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotalAmount.ForeColor = System.Drawing.Color.DimGray;
            this.lblTotalAmount.Location = new System.Drawing.Point(8, 4);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(360, 24);
            this.lblTotalAmount.TabIndex = 0;
            this.lblTotalAmount.Text = "Tạm tính: 0 đ";
            // 
            // btnRemoveProduct
            // 
            this.btnRemoveProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnRemoveProduct.FlatAppearance.BorderSize = 0;
            this.btnRemoveProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveProduct.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnRemoveProduct.ForeColor = System.Drawing.Color.White;
            this.btnRemoveProduct.Location = new System.Drawing.Point(296, 59);
            this.btnRemoveProduct.Name = "btnRemoveProduct";
            this.btnRemoveProduct.Size = new System.Drawing.Size(90, 29);
            this.btnRemoveProduct.TabIndex = 6;
            this.btnRemoveProduct.Text = "Xóa món";
            this.btnRemoveProduct.UseVisualStyleBackColor = false;
            this.btnRemoveProduct.Click += new System.EventHandler(this.btnRemoveProduct_Click);
            // 
            // txtDiscount
            // 
            this.txtDiscount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiscount.Location = new System.Drawing.Point(80, 60);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(210, 27);
            this.txtDiscount.TabIndex = 5;
            this.txtDiscount.TextChanged += new System.EventHandler(this.txtDiscount_TextChanged);
            this.txtDiscount.Enter += new System.EventHandler(this.txtDiscount_Enter);
            this.txtDiscount.Leave += new System.EventHandler(this.txtDiscount_Leave);
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblDiscount.Location = new System.Drawing.Point(8, 64);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(74, 20);
            this.lblDiscount.TabIndex = 4;
            this.lblDiscount.Text = "Mã giảm:";
            // 
            // lblCustomerInfo
            // 
            this.lblCustomerInfo.AutoEllipsis = true;
            this.lblCustomerInfo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.lblCustomerInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblCustomerInfo.Location = new System.Drawing.Point(80, 37);
            this.lblCustomerInfo.Name = "lblCustomerInfo";
            this.lblCustomerInfo.Size = new System.Drawing.Size(306, 18);
            this.lblCustomerInfo.TabIndex = 3;
            this.lblCustomerInfo.Text = "Khách lẻ (Gõ SĐT hoặc tên khách để gợi ý)";
            // 
            // txtCustomerPhone
            // 
            this.txtCustomerPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCustomerPhone.Location = new System.Drawing.Point(80, 8);
            this.txtCustomerPhone.Name = "txtCustomerPhone";
            this.txtCustomerPhone.Size = new System.Drawing.Size(306, 27);
            this.txtCustomerPhone.TabIndex = 1;
            this.txtCustomerPhone.TextChanged += new System.EventHandler(this.txtCustomerPhone_TextChanged);
            this.txtCustomerPhone.Leave += new System.EventHandler(this.txtCustomerPhone_Leave);
            // 
            // lblCustomerPhone
            // 
            this.lblCustomerPhone.AutoSize = true;
            this.lblCustomerPhone.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblCustomerPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblCustomerPhone.Location = new System.Drawing.Point(8, 12);
            this.lblCustomerPhone.Name = "lblCustomerPhone";
            this.lblCustomerPhone.Size = new System.Drawing.Size(73, 20);
            this.lblCustomerPhone.TabIndex = 0;
            this.lblCustomerPhone.Text = "SĐT/Tên:";
            // 
            // pnlOrderHeader
            // 
            this.pnlOrderHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(238)))), ((int)(((byte)(230)))));
            this.pnlOrderHeader.Controls.Add(this.lblOrderStatusBadge);
            this.pnlOrderHeader.Controls.Add(this.lblOrderHeaderTitle);
            this.pnlOrderHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOrderHeader.Location = new System.Drawing.Point(10, 10);
            this.pnlOrderHeader.Name = "pnlOrderHeader";
            this.pnlOrderHeader.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.pnlOrderHeader.Size = new System.Drawing.Size(395, 56);
            this.pnlOrderHeader.TabIndex = 0;
            // 
            // lblOrderStatusBadge
            // 
            this.lblOrderStatusBadge.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblOrderStatusBadge.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblOrderStatusBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(78)))), ((int)(((byte)(55)))));
            this.lblOrderStatusBadge.Location = new System.Drawing.Point(10, 30);
            this.lblOrderStatusBadge.Name = "lblOrderStatusBadge";
            this.lblOrderStatusBadge.Size = new System.Drawing.Size(375, 20);
            this.lblOrderStatusBadge.TabIndex = 1;
            this.lblOrderStatusBadge.Text = "Sẵn sàng gọi món";
            // 
            // lblOrderHeaderTitle
            // 
            this.lblOrderHeaderTitle.AutoEllipsis = true;
            this.lblOrderHeaderTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOrderHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblOrderHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblOrderHeaderTitle.Location = new System.Drawing.Point(10, 6);
            this.lblOrderHeaderTitle.Name = "lblOrderHeaderTitle";
            this.lblOrderHeaderTitle.Size = new System.Drawing.Size(375, 24);
            this.lblOrderHeaderTitle.TabIndex = 0;
            this.lblOrderHeaderTitle.Text = "ĐƠN HÀNG · KHÁCH TẠI QUẦY";
            // 
            // pnlCenterMenu
            // 
            this.pnlCenterMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(250)))), ((int)(((byte)(247)))));
            this.pnlCenterMenu.Controls.Add(this.flpProductMenu);
            this.pnlCenterMenu.Controls.Add(this.pnlMenuFilter);
            this.pnlCenterMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenterMenu.Location = new System.Drawing.Point(305, 50);
            this.pnlCenterMenu.Name = "pnlCenterMenu";
            this.pnlCenterMenu.Padding = new System.Windows.Forms.Padding(8);
            this.pnlCenterMenu.Size = new System.Drawing.Size(560, 670);
            this.pnlCenterMenu.TabIndex = 3;
            // 
            // flpProductMenu
            // 
            this.flpProductMenu.AutoScroll = true;
            this.flpProductMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(250)))), ((int)(((byte)(247)))));
            this.flpProductMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpProductMenu.Location = new System.Drawing.Point(8, 90);
            this.flpProductMenu.Name = "flpProductMenu";
            this.flpProductMenu.Padding = new System.Windows.Forms.Padding(4);
            this.flpProductMenu.Size = new System.Drawing.Size(544, 572);
            this.flpProductMenu.TabIndex = 1;
            // 
            // pnlMenuFilter
            // 
            this.pnlMenuFilter.BackColor = System.Drawing.Color.White;
            this.pnlMenuFilter.Controls.Add(this.btnAddProduct);
            this.pnlMenuFilter.Controls.Add(this.nudQuantity);
            this.pnlMenuFilter.Controls.Add(this.lblQuantity);
            this.pnlMenuFilter.Controls.Add(this.txtSearchProduct);
            this.pnlMenuFilter.Controls.Add(this.lblSearch);
            this.pnlMenuFilter.Controls.Add(this.cboCategoryFilter);
            this.pnlMenuFilter.Controls.Add(this.lblCategory);
            this.pnlMenuFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMenuFilter.Location = new System.Drawing.Point(8, 8);
            this.pnlMenuFilter.Name = "pnlMenuFilter";
            this.pnlMenuFilter.Size = new System.Drawing.Size(544, 82);
            this.pnlMenuFilter.TabIndex = 0;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.btnAddProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddProduct.FlatAppearance.BorderSize = 0;
            this.btnAddProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddProduct.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddProduct.ForeColor = System.Drawing.Color.White;
            this.btnAddProduct.Location = new System.Drawing.Point(184, 45);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(160, 31);
            this.btnAddProduct.TabIndex = 6;
            this.btnAddProduct.Text = "+ Thêm món đang chọn";
            this.btnAddProduct.UseVisualStyleBackColor = false;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // nudQuantity
            // 
            this.nudQuantity.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.nudQuantity.Location = new System.Drawing.Point(92, 46);
            this.nudQuantity.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(80, 29);
            this.nudQuantity.TabIndex = 5;
            this.nudQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblQuantity.Location = new System.Drawing.Point(10, 50);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(75, 20);
            this.lblQuantity.TabIndex = 4;
            this.lblQuantity.Text = "Số lượng:";
            // 
            // txtSearchProduct
            // 
            this.txtSearchProduct.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearchProduct.Location = new System.Drawing.Point(362, 10);
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.Size = new System.Drawing.Size(170, 29);
            this.txtSearchProduct.TabIndex = 3;
            this.txtSearchProduct.TextChanged += new System.EventHandler(this.txtSearchProduct_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblSearch.Location = new System.Drawing.Point(288, 14);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(76, 20);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Tìm món:";
            // 
            // cboCategoryFilter
            // 
            this.cboCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategoryFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboCategoryFilter.FormattingEnabled = true;
            this.cboCategoryFilter.Location = new System.Drawing.Point(92, 10);
            this.cboCategoryFilter.Name = "cboCategoryFilter";
            this.cboCategoryFilter.Size = new System.Drawing.Size(185, 29);
            this.cboCategoryFilter.TabIndex = 1;
            this.cboCategoryFilter.SelectedIndexChanged += new System.EventHandler(this.cboCategoryFilter_SelectedIndexChanged);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblCategory.Location = new System.Drawing.Point(10, 14);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(84, 20);
            this.lblCategory.TabIndex = 0;
            this.lblCategory.Text = "Danh mục:";
            // 
            // tmrAutoExpire
            // 
            this.tmrAutoExpire.Enabled = true;
            this.tmrAutoExpire.Interval = 15000;
            this.tmrAutoExpire.Tick += new System.EventHandler(this.tmrAutoExpire_Tick);
            // 
            // FrmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.pnlCenterMenu);
            this.Controls.Add(this.pnlRightOrder);
            this.Controls.Add(this.pnlLeftTables);
            this.Controls.Add(this.pnlTopBar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS Bán hàng & Quản lý bàn đặt trước";
            this.Load += new System.EventHandler(this.FrmOrder_Load);
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            this.pnlLeftTables.ResumeLayout(false);
            this.grpQuickReserve.ResumeLayout(false);
            this.grpQuickReserve.PerformLayout();
            this.pnlRightOrder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).EndInit();
            this.pnlOrderFooter.ResumeLayout(false);
            this.pnlOrderFooter.PerformLayout();
            this.pnlSummaryBox.ResumeLayout(false);
            this.pnlOrderHeader.ResumeLayout(false);
            this.pnlCenterMenu.ResumeLayout(false);
            this.pnlMenuFilter.ResumeLayout(false);
            this.pnlMenuFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Label lblSelectedTableBanner;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Panel pnlLeftTables;
        private System.Windows.Forms.FlowLayoutPanel flpTableList;
        private System.Windows.Forms.Label lblReservedListHeader;
        private System.Windows.Forms.GroupBox grpQuickReserve;
        private System.Windows.Forms.Button btnAddReservedTable;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.ComboBox cboReserveArea;
        private System.Windows.Forms.Label lblReserveArea;
        private System.Windows.Forms.TextBox txtReserveCustomerName;
        private System.Windows.Forms.Label lblReserveName;
        private System.Windows.Forms.Button btnCounterOrder;
        private System.Windows.Forms.Panel pnlRightOrder;
        private System.Windows.Forms.DataGridView dgvOrderDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailTotalPrice;
        private System.Windows.Forms.Panel pnlOrderFooter;
        private System.Windows.Forms.ListBox lstCustomerSuggest;
        private System.Windows.Forms.ListBox lstPromoSuggest;
        private System.Windows.Forms.Button btnPayAndPrint;
        private System.Windows.Forms.Panel pnlSummaryBox;
        private System.Windows.Forms.Label lblFinalAmount;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Button btnRemoveProduct;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblCustomerInfo;
        private System.Windows.Forms.TextBox txtCustomerPhone;
        private System.Windows.Forms.Label lblCustomerPhone;
        private System.Windows.Forms.Label lblCashTendered;
        private System.Windows.Forms.TextBox txtCashTendered;
        private System.Windows.Forms.Button btnCashExact;
        private System.Windows.Forms.Button btnCash50k;
        private System.Windows.Forms.Button btnCash100k;
        private System.Windows.Forms.Button btnCash200k;
        private System.Windows.Forms.Button btnCash500k;
        private System.Windows.Forms.Label lblChangeDueTitle;
        private System.Windows.Forms.Label lblChangeDue;
        private System.Windows.Forms.Panel pnlOrderHeader;
        private System.Windows.Forms.Label lblOrderStatusBadge;
        private System.Windows.Forms.Label lblOrderHeaderTitle;
        private System.Windows.Forms.Panel pnlCenterMenu;
        private System.Windows.Forms.FlowLayoutPanel flpProductMenu;
        private System.Windows.Forms.Panel pnlMenuFilter;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ComboBox cboCategoryFilter;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Timer tmrAutoExpire;
        private System.Windows.Forms.Label lblEmptyReservedNotice;
    }
}
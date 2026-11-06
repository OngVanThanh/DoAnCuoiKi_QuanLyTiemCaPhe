namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStylePrice = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyleUnitPrice = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyleTotalPrice = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSelectedTableBanner = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.pnlTables = new System.Windows.Forms.Panel();
            this.flpTableList = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTableHeader = new System.Windows.Forms.Panel();
            this.lblTableLegend = new System.Windows.Forms.Label();
            this.lblTableSectionTitle = new System.Windows.Forms.Label();
            this.btnSwitchTable = new System.Windows.Forms.Button();
            this.pnlOrder = new System.Windows.Forms.Panel();
            this.dgvOrderDetails = new System.Windows.Forms.DataGridView();
            this.colDetailId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailOrderId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetailTotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlOrderActions = new System.Windows.Forms.Panel();
            this.lblQtyCaption = new System.Windows.Forms.Label();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnRemoveProduct = new System.Windows.Forms.Button();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.pnlPaymentSummary = new System.Windows.Forms.Panel();
            this.lblPromoHint = new System.Windows.Forms.Label();
            this.btnPayAndPrint = new System.Windows.Forms.Button();
            this.lblFinalAmount = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.pnlOrderHeader = new System.Windows.Forms.Panel();
            this.lblOrderStatusBadge = new System.Windows.Forms.Label();
            this.lblOrderHeaderTitle = new System.Windows.Forms.Label();
            this.pnlProducts = new System.Windows.Forms.Panel();
            this.flpProductMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.dgvProductList = new System.Windows.Forms.DataGridView();
            this.colProdId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProdName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProdPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProdUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMenuFilter = new System.Windows.Forms.Panel();
            this.flpCategoryPills = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCategoryFilterCaption = new System.Windows.Forms.Label();
            this.lblMenuTitle = new System.Windows.Forms.Label();
            this.cboCategoryFilter = new System.Windows.Forms.ComboBox();
            this.lblSearchProduct = new System.Windows.Forms.Label();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.pnlHeader.SuspendLayout();
            this.pnlTables.SuspendLayout();
            this.pnlTableHeader.SuspendLayout();
            this.pnlOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).BeginInit();
            this.pnlOrderActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.pnlPaymentSummary.SuspendLayout();
            this.pnlOrderHeader.SuspendLayout();
            this.pnlProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductList)).BeginInit();
            this.pnlMenuFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.pnlHeader.Controls.Add(this.lblSelectedTableBanner);
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1627, 64);
            this.pnlHeader.TabIndex = 3;
            // 
            // lblSelectedTableBanner
            // 
            this.lblSelectedTableBanner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectedTableBanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(78)))), ((int)(((byte)(55)))));
            this.lblSelectedTableBanner.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedTableBanner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.lblSelectedTableBanner.Location = new System.Drawing.Point(1107, 12);
            this.lblSelectedTableBanner.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedTableBanner.Name = "lblSelectedTableBanner";
            this.lblSelectedTableBanner.Padding = new System.Windows.Forms.Padding(13, 6, 13, 6);
            this.lblSelectedTableBanner.Size = new System.Drawing.Size(499, 39);
            this.lblSelectedTableBanner.TabIndex = 1;
            this.lblSelectedTableBanner.Text = "Bàn đang chọn: Chưa chọn bàn";
            this.lblSelectedTableBanner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSelectedTableBanner.UseMnemonic = false;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(21, 14);
            this.lblHeaderTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(479, 30);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "POS BÁN HÀNG · ĐẶT MÓN & THANH TOÁN";
            this.lblHeaderTitle.UseMnemonic = false;
            this.lblHeaderTitle.Click += new System.EventHandler(this.lblHeaderTitle_Click);
            // 
            // pnlTables
            // 
            this.pnlTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(239)))), ((int)(((byte)(230)))));
            this.pnlTables.Controls.Add(this.flpTableList);
            this.pnlTables.Controls.Add(this.pnlTableHeader);
            this.pnlTables.Controls.Add(this.btnSwitchTable);
            this.pnlTables.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTables.Location = new System.Drawing.Point(0, 64);
            this.pnlTables.Margin = new System.Windows.Forms.Padding(4);
            this.pnlTables.Name = "pnlTables";
            this.pnlTables.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.pnlTables.Size = new System.Drawing.Size(393, 773);
            this.pnlTables.TabIndex = 0;
            // 
            // flpTableList
            // 
            this.flpTableList.AutoScroll = true;
            this.flpTableList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.flpTableList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTableList.Location = new System.Drawing.Point(11, 76);
            this.flpTableList.Margin = new System.Windows.Forms.Padding(4);
            this.flpTableList.Name = "flpTableList";
            this.flpTableList.Padding = new System.Windows.Forms.Padding(5);
            this.flpTableList.Size = new System.Drawing.Size(371, 633);
            this.flpTableList.TabIndex = 0;
            // 
            // pnlTableHeader
            // 
            this.pnlTableHeader.BackColor = System.Drawing.Color.White;
            this.pnlTableHeader.Controls.Add(this.lblTableLegend);
            this.pnlTableHeader.Controls.Add(this.lblTableSectionTitle);
            this.pnlTableHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTableHeader.Location = new System.Drawing.Point(11, 10);
            this.pnlTableHeader.Margin = new System.Windows.Forms.Padding(4);
            this.pnlTableHeader.Name = "pnlTableHeader";
            this.pnlTableHeader.Size = new System.Drawing.Size(371, 66);
            this.pnlTableHeader.TabIndex = 2;
            // 
            // lblTableLegend
            // 
            this.lblTableLegend.AutoSize = true;
            this.lblTableLegend.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTableLegend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblTableLegend.Location = new System.Drawing.Point(13, 37);
            this.lblTableLegend.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTableLegend.Name = "lblTableLegend";
            this.lblTableLegend.Size = new System.Drawing.Size(293, 19);
            this.lblTableLegend.TabIndex = 1;
            this.lblTableLegend.Text = "■ Xanh: Trống  ■ Cam: Có khách  ■ Lam: Đặt";
            // 
            // lblTableSectionTitle
            // 
            this.lblTableSectionTitle.AutoSize = true;
            this.lblTableSectionTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTableSectionTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblTableSectionTitle.Location = new System.Drawing.Point(13, 7);
            this.lblTableSectionTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTableSectionTitle.Name = "lblTableSectionTitle";
            this.lblTableSectionTitle.Size = new System.Drawing.Size(192, 23);
            this.lblTableSectionTitle.TabIndex = 0;
            this.lblTableSectionTitle.Text = "SƠ ĐỒ BÀN TẠI QUÁN";
            // 
            // btnSwitchTable
            // 
            this.btnSwitchTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(78)))), ((int)(((byte)(55)))));
            this.btnSwitchTable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSwitchTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSwitchTable.FlatAppearance.BorderSize = 0;
            this.btnSwitchTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitchTable.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSwitchTable.ForeColor = System.Drawing.Color.White;
            this.btnSwitchTable.Location = new System.Drawing.Point(11, 709);
            this.btnSwitchTable.Margin = new System.Windows.Forms.Padding(4);
            this.btnSwitchTable.Name = "btnSwitchTable";
            this.btnSwitchTable.Size = new System.Drawing.Size(371, 54);
            this.btnSwitchTable.TabIndex = 1;
            this.btnSwitchTable.Text = "⇄  Chuyển bàn / Gộp bàn";
            this.btnSwitchTable.UseVisualStyleBackColor = false;
            this.btnSwitchTable.Click += new System.EventHandler(this.btnSwitchTable_Click);
            // 
            // pnlOrder
            // 
            this.pnlOrder.BackColor = System.Drawing.Color.White;
            this.pnlOrder.Controls.Add(this.dgvOrderDetails);
            this.pnlOrder.Controls.Add(this.pnlOrderActions);
            this.pnlOrder.Controls.Add(this.pnlPaymentSummary);
            this.pnlOrder.Controls.Add(this.pnlOrderHeader);
            this.pnlOrder.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlOrder.Location = new System.Drawing.Point(1054, 64);
            this.pnlOrder.Margin = new System.Windows.Forms.Padding(4);
            this.pnlOrder.Name = "pnlOrder";
            this.pnlOrder.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlOrder.Size = new System.Drawing.Size(573, 773);
            this.pnlOrder.TabIndex = 1;
            // 
            // dgvOrderDetails
            // 
            this.dgvOrderDetails.AllowUserToAddRows = false;
            this.dgvOrderDetails.AllowUserToDeleteRows = false;
            this.dgvOrderDetails.AutoGenerateColumns = false;
            this.dgvOrderDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDetailId,
            this.colDetailOrderId,
            this.colDetailProductId,
            this.colDetailProductName,
            this.colDetailUnit,
            this.colDetailQuantity,
            this.colDetailUnitPrice,
            this.colDetailTotalPrice});
            this.dgvOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderDetails.Location = new System.Drawing.Point(13, 71);
            this.dgvOrderDetails.Margin = new System.Windows.Forms.Padding(4);
            this.dgvOrderDetails.Name = "dgvOrderDetails";
            this.dgvOrderDetails.ReadOnly = true;
            this.dgvOrderDetails.RowHeadersVisible = false;
            this.dgvOrderDetails.RowHeadersWidth = 51;
            this.dgvOrderDetails.RowTemplate.Height = 28;
            this.dgvOrderDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderDetails.Size = new System.Drawing.Size(547, 363);
            this.dgvOrderDetails.TabIndex = 1;
            // 
            // colDetailId
            // 
            this.colDetailId.DataPropertyName = "Id";
            this.colDetailId.HeaderText = "ID";
            this.colDetailId.Name = "colDetailId";
            this.colDetailId.ReadOnly = true;
            this.colDetailId.Visible = false;
            // 
            // colDetailOrderId
            // 
            this.colDetailOrderId.DataPropertyName = "OrderId";
            this.colDetailOrderId.HeaderText = "Mã HĐ";
            this.colDetailOrderId.Name = "colDetailOrderId";
            this.colDetailOrderId.ReadOnly = true;
            this.colDetailOrderId.Visible = false;
            // 
            // colDetailProductId
            // 
            this.colDetailProductId.DataPropertyName = "ProductId";
            this.colDetailProductId.HeaderText = "Mã món";
            this.colDetailProductId.Name = "colDetailProductId";
            this.colDetailProductId.ReadOnly = true;
            this.colDetailProductId.Visible = false;
            // 
            // colDetailProductName
            // 
            this.colDetailProductName.DataPropertyName = "ProductName";
            this.colDetailProductName.FillWeight = 140F;
            this.colDetailProductName.HeaderText = "Tên món";
            this.colDetailProductName.Name = "colDetailProductName";
            this.colDetailProductName.ReadOnly = true;
            // 
            // colDetailUnit
            // 
            this.colDetailUnit.DataPropertyName = "Unit";
            this.colDetailUnit.FillWeight = 55F;
            this.colDetailUnit.HeaderText = "ĐVT";
            this.colDetailUnit.Name = "colDetailUnit";
            this.colDetailUnit.ReadOnly = true;
            // 
            // colDetailQuantity
            // 
            this.colDetailQuantity.DataPropertyName = "Quantity";
            this.colDetailQuantity.FillWeight = 45F;
            this.colDetailQuantity.HeaderText = "SL";
            this.colDetailQuantity.Name = "colDetailQuantity";
            this.colDetailQuantity.ReadOnly = true;
            // 
            // colDetailUnitPrice
            // 
            this.colDetailUnitPrice.DataPropertyName = "UnitPrice";
            dataGridViewCellStyleUnitPrice.Format = "N0";
            this.colDetailUnitPrice.DefaultCellStyle = dataGridViewCellStyleUnitPrice;
            this.colDetailUnitPrice.FillWeight = 80F;
            this.colDetailUnitPrice.HeaderText = "Đơn giá";
            this.colDetailUnitPrice.Name = "colDetailUnitPrice";
            this.colDetailUnitPrice.ReadOnly = true;
            // 
            // colDetailTotalPrice
            // 
            this.colDetailTotalPrice.DataPropertyName = "TotalPrice";
            dataGridViewCellStyleTotalPrice.Format = "N0";
            this.colDetailTotalPrice.DefaultCellStyle = dataGridViewCellStyleTotalPrice;
            this.colDetailTotalPrice.FillWeight = 90F;
            this.colDetailTotalPrice.HeaderText = "Thành tiền";
            this.colDetailTotalPrice.Name = "colDetailTotalPrice";
            this.colDetailTotalPrice.ReadOnly = true;
            // 
            // pnlOrderActions
            // 
            this.pnlOrderActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.pnlOrderActions.Controls.Add(this.lblQtyCaption);
            this.pnlOrderActions.Controls.Add(this.btnAddProduct);
            this.pnlOrderActions.Controls.Add(this.btnRemoveProduct);
            this.pnlOrderActions.Controls.Add(this.nudQuantity);
            this.pnlOrderActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOrderActions.Location = new System.Drawing.Point(13, 434);
            this.pnlOrderActions.Margin = new System.Windows.Forms.Padding(4);
            this.pnlOrderActions.Name = "pnlOrderActions";
            this.pnlOrderActions.Size = new System.Drawing.Size(547, 64);
            this.pnlOrderActions.TabIndex = 2;
            // 
            // lblQtyCaption
            // 
            this.lblQtyCaption.AutoSize = true;
            this.lblQtyCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtyCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblQtyCaption.Location = new System.Drawing.Point(331, 20);
            this.lblQtyCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQtyCaption.Name = "lblQtyCaption";
            this.lblQtyCaption.Size = new System.Drawing.Size(75, 20);
            this.lblQtyCaption.TabIndex = 4;
            this.lblQtyCaption.Text = "Số lượng:";
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnAddProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddProduct.FlatAppearance.BorderSize = 0;
            this.btnAddProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddProduct.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddProduct.ForeColor = System.Drawing.Color.White;
            this.btnAddProduct.Location = new System.Drawing.Point(11, 11);
            this.btnAddProduct.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(149, 42);
            this.btnAddProduct.TabIndex = 2;
            this.btnAddProduct.Text = "+ Thêm món";
            this.btnAddProduct.UseVisualStyleBackColor = false;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnRemoveProduct
            // 
            this.btnRemoveProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnRemoveProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemoveProduct.FlatAppearance.BorderSize = 0;
            this.btnRemoveProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveProduct.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveProduct.ForeColor = System.Drawing.Color.White;
            this.btnRemoveProduct.Location = new System.Drawing.Point(171, 11);
            this.btnRemoveProduct.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoveProduct.Name = "btnRemoveProduct";
            this.btnRemoveProduct.Size = new System.Drawing.Size(144, 42);
            this.btnRemoveProduct.TabIndex = 3;
            this.btnRemoveProduct.Text = "✕ Xóa món";
            this.btnRemoveProduct.UseVisualStyleBackColor = false;
            this.btnRemoveProduct.Click += new System.EventHandler(this.btnRemoveProduct_Click);
            // 
            // nudQuantity
            // 
            this.nudQuantity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudQuantity.Location = new System.Drawing.Point(429, 14);
            this.nudQuantity.Margin = new System.Windows.Forms.Padding(4);
            this.nudQuantity.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(107, 30);
            this.nudQuantity.TabIndex = 1;
            this.nudQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // pnlPaymentSummary
            // 
            this.pnlPaymentSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(239)))), ((int)(((byte)(230)))));
            this.pnlPaymentSummary.Controls.Add(this.lblPromoHint);
            this.pnlPaymentSummary.Controls.Add(this.btnPayAndPrint);
            this.pnlPaymentSummary.Controls.Add(this.lblFinalAmount);
            this.pnlPaymentSummary.Controls.Add(this.lblTotalAmount);
            this.pnlPaymentSummary.Controls.Add(this.lblDiscount);
            this.pnlPaymentSummary.Controls.Add(this.txtDiscount);
            this.pnlPaymentSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPaymentSummary.Location = new System.Drawing.Point(13, 498);
            this.pnlPaymentSummary.Margin = new System.Windows.Forms.Padding(4);
            this.pnlPaymentSummary.Name = "pnlPaymentSummary";
            this.pnlPaymentSummary.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlPaymentSummary.Size = new System.Drawing.Size(547, 263);
            this.pnlPaymentSummary.TabIndex = 3;
            // 
            // lblPromoHint
            // 
            this.lblPromoHint.AutoSize = true;
            this.lblPromoHint.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPromoHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(78)))), ((int)(((byte)(55)))));
            this.lblPromoHint.Location = new System.Drawing.Point(13, 54);
            this.lblPromoHint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPromoHint.Name = "lblPromoHint";
            this.lblPromoHint.Size = new System.Drawing.Size(345, 19);
            this.lblPromoHint.TabIndex = 9;
            this.lblPromoHint.Text = "Gợi ý mã KM: CHAOMUNG (-10%), VIPCOFFEE (-15%)";
            // 
            // btnPayAndPrint
            // 
            this.btnPayAndPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(111)))), ((int)(((byte)(67)))));
            this.btnPayAndPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPayAndPrint.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPayAndPrint.FlatAppearance.BorderSize = 0;
            this.btnPayAndPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayAndPrint.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayAndPrint.ForeColor = System.Drawing.Color.White;
            this.btnPayAndPrint.Location = new System.Drawing.Point(13, 187);
            this.btnPayAndPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPayAndPrint.Name = "btnPayAndPrint";
            this.btnPayAndPrint.Size = new System.Drawing.Size(521, 64);
            this.btnPayAndPrint.TabIndex = 8;
            this.btnPayAndPrint.Text = "✔  THANH TOÁN & IN HÓA ĐƠN (K80)";
            this.btnPayAndPrint.UseMnemonic = false;
            this.btnPayAndPrint.UseVisualStyleBackColor = false;
            this.btnPayAndPrint.Click += new System.EventHandler(this.btnPayAndPrint_Click);
            // 
            // lblFinalAmount
            // 
            this.lblFinalAmount.AutoSize = true;
            this.lblFinalAmount.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(111)))), ((int)(((byte)(67)))));
            this.lblFinalAmount.Location = new System.Drawing.Point(13, 130);
            this.lblFinalAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFinalAmount.Name = "lblFinalAmount";
            this.lblFinalAmount.Size = new System.Drawing.Size(175, 30);
            this.lblFinalAmount.TabIndex = 7;
            this.lblFinalAmount.Text = "Thanh toán: 0 đ";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(13, 91);
            this.lblTotalAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(104, 21);
            this.lblTotalAmount.TabIndex = 6;
            this.lblTotalAmount.Text = "Tổng tiền: 0 đ";
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblDiscount.Location = new System.Drawing.Point(13, 17);
            this.lblDiscount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(162, 20);
            this.lblDiscount.TabIndex = 5;
            this.lblDiscount.Text = "Khuyến mãi (Mã / %):";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscount.Location = new System.Drawing.Point(233, 12);
            this.txtDiscount.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(295, 29);
            this.txtDiscount.TabIndex = 4;
            this.txtDiscount.TextChanged += new System.EventHandler(this.txtDiscount_TextChanged);
            // 
            // pnlOrderHeader
            // 
            this.pnlOrderHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(239)))), ((int)(((byte)(230)))));
            this.pnlOrderHeader.Controls.Add(this.lblOrderStatusBadge);
            this.pnlOrderHeader.Controls.Add(this.lblOrderHeaderTitle);
            this.pnlOrderHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOrderHeader.Location = new System.Drawing.Point(13, 12);
            this.pnlOrderHeader.Margin = new System.Windows.Forms.Padding(4);
            this.pnlOrderHeader.Name = "pnlOrderHeader";
            this.pnlOrderHeader.Size = new System.Drawing.Size(547, 59);
            this.pnlOrderHeader.TabIndex = 0;
            // 
            // lblOrderStatusBadge
            // 
            this.lblOrderStatusBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrderStatusBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(208)))));
            this.lblOrderStatusBadge.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderStatusBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(74)))), ((int)(((byte)(0)))));
            this.lblOrderStatusBadge.Location = new System.Drawing.Point(363, 14);
            this.lblOrderStatusBadge.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOrderStatusBadge.Name = "lblOrderStatusBadge";
            this.lblOrderStatusBadge.Size = new System.Drawing.Size(171, 32);
            this.lblOrderStatusBadge.TabIndex = 1;
            this.lblOrderStatusBadge.Text = "Chưa chọn bàn";
            this.lblOrderStatusBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrderHeaderTitle
            // 
            this.lblOrderHeaderTitle.AutoSize = true;
            this.lblOrderHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblOrderHeaderTitle.Location = new System.Drawing.Point(13, 14);
            this.lblOrderHeaderTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOrderHeaderTitle.Name = "lblOrderHeaderTitle";
            this.lblOrderHeaderTitle.Size = new System.Drawing.Size(199, 25);
            this.lblOrderHeaderTitle.TabIndex = 0;
            this.lblOrderHeaderTitle.Text = "CHI TIẾT ĐƠN HÀNG";
            // 
            // pnlProducts
            // 
            this.pnlProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.pnlProducts.Controls.Add(this.flpProductMenu);
            this.pnlProducts.Controls.Add(this.dgvProductList);
            this.pnlProducts.Controls.Add(this.pnlMenuFilter);
            this.pnlProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProducts.Location = new System.Drawing.Point(393, 64);
            this.pnlProducts.Margin = new System.Windows.Forms.Padding(4);
            this.pnlProducts.Name = "pnlProducts";
            this.pnlProducts.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.pnlProducts.Size = new System.Drawing.Size(661, 773);
            this.pnlProducts.TabIndex = 2;
            // 
            // flpProductMenu
            // 
            this.flpProductMenu.AutoScroll = true;
            this.flpProductMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.flpProductMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpProductMenu.Location = new System.Drawing.Point(11, 150);
            this.flpProductMenu.Margin = new System.Windows.Forms.Padding(4);
            this.flpProductMenu.Name = "flpProductMenu";
            this.flpProductMenu.Padding = new System.Windows.Forms.Padding(5);
            this.flpProductMenu.Size = new System.Drawing.Size(639, 443);
            this.flpProductMenu.TabIndex = 4;
            // 
            // dgvProductList
            // 
            this.dgvProductList.AllowUserToAddRows = false;
            this.dgvProductList.AllowUserToDeleteRows = false;
            this.dgvProductList.AutoGenerateColumns = false;
            this.dgvProductList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProdId,
            this.colProdName,
            this.colProdPrice,
            this.colProdUnit});
            this.dgvProductList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvProductList.Location = new System.Drawing.Point(11, 593);
            this.dgvProductList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvProductList.Name = "dgvProductList";
            this.dgvProductList.ReadOnly = true;
            this.dgvProductList.RowHeadersVisible = false;
            this.dgvProductList.RowHeadersWidth = 51;
            this.dgvProductList.RowTemplate.Height = 26;
            this.dgvProductList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductList.Size = new System.Drawing.Size(639, 170);
            this.dgvProductList.TabIndex = 3;
            this.dgvProductList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductList_CellClick);
            this.dgvProductList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductList_CellDoubleClick);
            // 
            // colProdId
            // 
            this.colProdId.DataPropertyName = "Id";
            this.colProdId.FillWeight = 50F;
            this.colProdId.HeaderText = "Mã";
            this.colProdId.Name = "colProdId";
            this.colProdId.ReadOnly = true;
            // 
            // colProdName
            // 
            this.colProdName.DataPropertyName = "ProductName";
            this.colProdName.FillWeight = 150F;
            this.colProdName.HeaderText = "Tên món";
            this.colProdName.Name = "colProdName";
            this.colProdName.ReadOnly = true;
            // 
            // colProdPrice
            // 
            this.colProdPrice.DataPropertyName = "Price";
            dataGridViewCellStylePrice.Format = "N0";
            this.colProdPrice.DefaultCellStyle = dataGridViewCellStylePrice;
            this.colProdPrice.FillWeight = 90F;
            this.colProdPrice.HeaderText = "Đơn giá";
            this.colProdPrice.Name = "colProdPrice";
            this.colProdPrice.ReadOnly = true;
            // 
            // colProdUnit
            // 
            this.colProdUnit.DataPropertyName = "Unit";
            this.colProdUnit.FillWeight = 60F;
            this.colProdUnit.HeaderText = "ĐVT";
            this.colProdUnit.Name = "colProdUnit";
            this.colProdUnit.ReadOnly = true;
            // 
            // pnlMenuFilter
            // 
            this.pnlMenuFilter.BackColor = System.Drawing.Color.White;
            this.pnlMenuFilter.Controls.Add(this.flpCategoryPills);
            this.pnlMenuFilter.Controls.Add(this.lblCategoryFilterCaption);
            this.pnlMenuFilter.Controls.Add(this.lblMenuTitle);
            this.pnlMenuFilter.Controls.Add(this.cboCategoryFilter);
            this.pnlMenuFilter.Controls.Add(this.lblSearchProduct);
            this.pnlMenuFilter.Controls.Add(this.txtSearchProduct);
            this.pnlMenuFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMenuFilter.Location = new System.Drawing.Point(11, 10);
            this.pnlMenuFilter.Margin = new System.Windows.Forms.Padding(4);
            this.pnlMenuFilter.Name = "pnlMenuFilter";
            this.pnlMenuFilter.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlMenuFilter.Size = new System.Drawing.Size(639, 140);
            this.pnlMenuFilter.TabIndex = 0;
            // 
            // flpCategoryPills
            // 
            this.flpCategoryPills.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpCategoryPills.Location = new System.Drawing.Point(13, 89);
            this.flpCategoryPills.Margin = new System.Windows.Forms.Padding(4);
            this.flpCategoryPills.Name = "flpCategoryPills";
            this.flpCategoryPills.Size = new System.Drawing.Size(609, 44);
            this.flpCategoryPills.TabIndex = 6;
            this.flpCategoryPills.WrapContents = false;
            // 
            // lblCategoryFilterCaption
            // 
            this.lblCategoryFilterCaption.AutoSize = true;
            this.lblCategoryFilterCaption.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryFilterCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(78)))), ((int)(((byte)(55)))));
            this.lblCategoryFilterCaption.Location = new System.Drawing.Point(13, 52);
            this.lblCategoryFilterCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategoryFilterCaption.Name = "lblCategoryFilterCaption";
            this.lblCategoryFilterCaption.Size = new System.Drawing.Size(84, 20);
            this.lblCategoryFilterCaption.TabIndex = 5;
            this.lblCategoryFilterCaption.Text = "Danh mục:";
            // 
            // lblMenuTitle
            // 
            this.lblMenuTitle.AutoSize = true;
            this.lblMenuTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenuTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblMenuTitle.Location = new System.Drawing.Point(13, 10);
            this.lblMenuTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMenuTitle.Name = "lblMenuTitle";
            this.lblMenuTitle.Size = new System.Drawing.Size(315, 25);
            this.lblMenuTitle.TabIndex = 4;
            this.lblMenuTitle.Text = "THỰC ĐƠN MÓN ĂN & ĐỒ UỐNG";
            this.lblMenuTitle.UseMnemonic = false;
            // 
            // cboCategoryFilter
            // 
            this.cboCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategoryFilter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCategoryFilter.FormattingEnabled = true;
            this.cboCategoryFilter.Location = new System.Drawing.Point(112, 47);
            this.cboCategoryFilter.Margin = new System.Windows.Forms.Padding(4);
            this.cboCategoryFilter.Name = "cboCategoryFilter";
            this.cboCategoryFilter.Size = new System.Drawing.Size(205, 28);
            this.cboCategoryFilter.TabIndex = 0;
            this.cboCategoryFilter.SelectedIndexChanged += new System.EventHandler(this.cboCategoryFilter_SelectedIndexChanged);
            // 
            // lblSearchProduct
            // 
            this.lblSearchProduct.AutoSize = true;
            this.lblSearchProduct.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(78)))), ((int)(((byte)(55)))));
            this.lblSearchProduct.Location = new System.Drawing.Point(333, 52);
            this.lblSearchProduct.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchProduct.Name = "lblSearchProduct";
            this.lblSearchProduct.Size = new System.Drawing.Size(88, 20);
            this.lblSearchProduct.TabIndex = 2;
            this.lblSearchProduct.Text = "Tìm nhanh:";
            // 
            // txtSearchProduct
            // 
            this.txtSearchProduct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchProduct.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchProduct.Location = new System.Drawing.Point(433, 47);
            this.txtSearchProduct.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.Size = new System.Drawing.Size(188, 27);
            this.txtSearchProduct.TabIndex = 1;
            this.txtSearchProduct.TextChanged += new System.EventHandler(this.txtSearchProduct_TextChanged);
            // 
            // FrmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1627, 837);
            this.Controls.Add(this.pnlProducts);
            this.Controls.Add(this.pnlOrder);
            this.Controls.Add(this.pnlTables);
            this.Controls.Add(this.pnlHeader);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Bán hàng & Đặt món (POS)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmOrder_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlTables.ResumeLayout(false);
            this.pnlTableHeader.ResumeLayout(false);
            this.pnlTableHeader.PerformLayout();
            this.pnlOrder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).EndInit();
            this.pnlOrderActions.ResumeLayout(false);
            this.pnlOrderActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.pnlPaymentSummary.ResumeLayout(false);
            this.pnlPaymentSummary.PerformLayout();
            this.pnlOrderHeader.ResumeLayout(false);
            this.pnlOrderHeader.PerformLayout();
            this.pnlProducts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductList)).EndInit();
            this.pnlMenuFilter.ResumeLayout(false);
            this.pnlMenuFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblSelectedTableBanner;
        private System.Windows.Forms.Panel pnlTables;
        private System.Windows.Forms.Panel pnlTableHeader;
        private System.Windows.Forms.Label lblTableSectionTitle;
        private System.Windows.Forms.Label lblTableLegend;
        private System.Windows.Forms.FlowLayoutPanel flpTableList;
        private System.Windows.Forms.Button btnSwitchTable;
        private System.Windows.Forms.Panel pnlOrder;
        private System.Windows.Forms.Panel pnlOrderHeader;
        private System.Windows.Forms.Label lblOrderHeaderTitle;
        private System.Windows.Forms.Label lblOrderStatusBadge;
        private System.Windows.Forms.DataGridView dgvOrderDetails;
        private System.Windows.Forms.Panel pnlOrderActions;
        private System.Windows.Forms.Label lblQtyCaption;
        private System.Windows.Forms.Button btnRemoveProduct;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Panel pnlPaymentSummary;
        private System.Windows.Forms.Label lblPromoHint;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Button btnPayAndPrint;
        private System.Windows.Forms.Label lblFinalAmount;
        private System.Windows.Forms.Panel pnlProducts;
        private System.Windows.Forms.Panel pnlMenuFilter;
        private System.Windows.Forms.Label lblMenuTitle;
        private System.Windows.Forms.Label lblCategoryFilterCaption;
        private System.Windows.Forms.FlowLayoutPanel flpCategoryPills;
        private System.Windows.Forms.FlowLayoutPanel flpProductMenu;
        private System.Windows.Forms.DataGridView dgvProductList;
        private System.Windows.Forms.Label lblSearchProduct;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private System.Windows.Forms.ComboBox cboCategoryFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailOrderId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailTotalPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProdId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProdName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProdPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProdUnit;
    }
}

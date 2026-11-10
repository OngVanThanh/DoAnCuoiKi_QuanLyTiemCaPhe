namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmChart
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartAreaRev = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legendRev = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series seriesRev = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title titleRev = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartAreaProd = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legendProd = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series seriesProd = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title titleProd = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataGridViewCellStyle statAltStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle statHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle statCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dateCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle orderQtyCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle discountCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle revenueCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnFilterChart = new System.Windows.Forms.Button();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.rdoYear = new System.Windows.Forms.RadioButton();
            this.rdoMonth = new System.Windows.Forms.RadioButton();
            this.rdoWeek = new System.Windows.Forms.RadioButton();
            this.lblQuickRange = new System.Windows.Forms.Label();
            this.pnlKpiRow = new System.Windows.Forms.TableLayoutPanel();
            this.pnlKpiRevenue = new System.Windows.Forms.Panel();
            this.lblKpiRevenueValue = new System.Windows.Forms.Label();
            this.lblKpiRevenueTitle = new System.Windows.Forms.Label();
            this.pnlKpiOrders = new System.Windows.Forms.Panel();
            this.lblKpiOrdersValue = new System.Windows.Forms.Label();
            this.lblKpiOrdersTitle = new System.Windows.Forms.Label();
            this.pnlKpiDiscount = new System.Windows.Forms.Panel();
            this.lblKpiDiscountValue = new System.Windows.Forms.Label();
            this.lblKpiDiscountTitle = new System.Windows.Forms.Label();
            this.pnlKpiBestSeller = new System.Windows.Forms.Panel();
            this.lblKpiBestValue = new System.Windows.Forms.Label();
            this.lblKpiBestTitle = new System.Windows.Forms.Label();
            this.pnlChartsRow = new System.Windows.Forms.TableLayoutPanel();
            this.chartRevenue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ChartProducts = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.grpRevenueTable = new System.Windows.Forms.GroupBox();
            this.dgvRevenueStats = new System.Windows.Forms.DataGridView();
            this.colStatDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatOrders = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatRevenue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlHeader.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlKpiRow.SuspendLayout();
            this.pnlKpiRevenue.SuspendLayout();
            this.pnlKpiOrders.SuspendLayout();
            this.pnlKpiDiscount.SuspendLayout();
            this.pnlKpiBestSeller.SuspendLayout();
            this.pnlChartsRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartProducts)).BeginInit();
            this.grpRevenueTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenueStats)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(74, 44, 42);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1180, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1180, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "DASHBOARD THỐNG KÊ DOANH THU & HIỆU SUẤT KINH DOANH";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.UseMnemonic = false;
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(248, 244, 238);
            this.pnlFilter.Controls.Add(this.btnFilterChart);
            this.pnlFilter.Controls.Add(this.dtpToDate);
            this.pnlFilter.Controls.Add(this.lblToDate);
            this.pnlFilter.Controls.Add(this.dtpFromDate);
            this.pnlFilter.Controls.Add(this.lblFromDate);
            this.pnlFilter.Controls.Add(this.rdoYear);
            this.pnlFilter.Controls.Add(this.rdoMonth);
            this.pnlFilter.Controls.Add(this.rdoWeek);
            this.pnlFilter.Controls.Add(this.lblQuickRange);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 50);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1180, 52);
            this.pnlFilter.TabIndex = 1;
            // 
            // lblQuickRange
            // 
            this.lblQuickRange.AutoSize = true;
            this.lblQuickRange.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblQuickRange.ForeColor = System.Drawing.Color.FromArgb(74, 44, 42);
            this.lblQuickRange.Location = new System.Drawing.Point(16, 15);
            this.lblQuickRange.Name = "lblQuickRange";
            this.lblQuickRange.Size = new System.Drawing.Size(96, 21);
            this.lblQuickRange.TabIndex = 0;
            this.lblQuickRange.Text = "Kỳ thống kê:";
            // 
            // rdoWeek
            // 
            this.rdoWeek.AutoSize = true;
            this.rdoWeek.Checked = true;
            this.rdoWeek.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.rdoWeek.Location = new System.Drawing.Point(122, 14);
            this.rdoWeek.Name = "rdoWeek";
            this.rdoWeek.Size = new System.Drawing.Size(102, 25);
            this.rdoWeek.TabIndex = 1;
            this.rdoWeek.TabStop = true;
            this.rdoWeek.Text = "7 ngày qua";
            this.rdoWeek.UseVisualStyleBackColor = true;
            this.rdoWeek.CheckedChanged += new System.EventHandler(this.rdoWeek_CheckedChanged);
            // 
            // rdoMonth
            // 
            this.rdoMonth.AutoSize = true;
            this.rdoMonth.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.rdoMonth.Location = new System.Drawing.Point(234, 14);
            this.rdoMonth.Name = "rdoMonth";
            this.rdoMonth.Size = new System.Drawing.Size(104, 25);
            this.rdoMonth.TabIndex = 2;
            this.rdoMonth.Text = "30 ngày qua";
            this.rdoMonth.UseVisualStyleBackColor = true;
            this.rdoMonth.CheckedChanged += new System.EventHandler(this.rdoMonth_CheckedChanged);
            // 
            // rdoYear
            // 
            this.rdoYear.AutoSize = true;
            this.rdoYear.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.rdoYear.Location = new System.Drawing.Point(348, 14);
            this.rdoYear.Name = "rdoYear";
            this.rdoYear.Size = new System.Drawing.Size(89, 25);
            this.rdoYear.TabIndex = 3;
            this.rdoYear.Text = "Năm nay";
            this.rdoYear.UseVisualStyleBackColor = true;
            this.rdoYear.CheckedChanged += new System.EventHandler(this.rdoYear_CheckedChanged);
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblFromDate.ForeColor = System.Drawing.Color.FromArgb(74, 44, 42);
            this.lblFromDate.Location = new System.Drawing.Point(460, 15);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(67, 21);
            this.lblFromDate.TabIndex = 4;
            this.lblFromDate.Text = "Từ ngày:";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(533, 11);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(135, 29);
            this.dtpFromDate.TabIndex = 5;
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblToDate.ForeColor = System.Drawing.Color.FromArgb(74, 44, 42);
            this.lblToDate.Location = new System.Drawing.Point(682, 15);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(77, 21);
            this.lblToDate.TabIndex = 6;
            this.lblToDate.Text = "Đến ngày:";
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(765, 11);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(135, 29);
            this.dtpToDate.TabIndex = 7;
            // 
            // btnFilterChart
            // 
            this.btnFilterChart.BackColor = System.Drawing.Color.FromArgb(74, 44, 42);
            this.btnFilterChart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFilterChart.FlatAppearance.BorderSize = 0;
            this.btnFilterChart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilterChart.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnFilterChart.ForeColor = System.Drawing.Color.White;
            this.btnFilterChart.Location = new System.Drawing.Point(918, 9);
            this.btnFilterChart.Name = "btnFilterChart";
            this.btnFilterChart.Size = new System.Drawing.Size(150, 33);
            this.btnFilterChart.TabIndex = 8;
            this.btnFilterChart.Text = "Lọc dữ liệu";
            this.btnFilterChart.UseVisualStyleBackColor = false;
            this.btnFilterChart.Click += new System.EventHandler(this.btnFilterChart_Click);
            // 
            // pnlKpiRow
            // 
            this.pnlKpiRow.BackColor = System.Drawing.Color.White;
            this.pnlKpiRow.ColumnCount = 4;
            this.pnlKpiRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpiRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpiRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpiRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKpiRow.Controls.Add(this.pnlKpiRevenue, 0, 0);
            this.pnlKpiRow.Controls.Add(this.pnlKpiOrders, 1, 0);
            this.pnlKpiRow.Controls.Add(this.pnlKpiDiscount, 2, 0);
            this.pnlKpiRow.Controls.Add(this.pnlKpiBestSeller, 3, 0);
            this.pnlKpiRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKpiRow.Location = new System.Drawing.Point(0, 102);
            this.pnlKpiRow.Name = "pnlKpiRow";
            this.pnlKpiRow.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.pnlKpiRow.RowCount = 1;
            this.pnlKpiRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlKpiRow.Size = new System.Drawing.Size(1180, 92);
            this.pnlKpiRow.TabIndex = 2;
            // 
            // pnlKpiRevenue
            // 
            this.pnlKpiRevenue.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.pnlKpiRevenue.Controls.Add(this.lblKpiRevenueValue);
            this.pnlKpiRevenue.Controls.Add(this.lblKpiRevenueTitle);
            this.pnlKpiRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiRevenue.Location = new System.Drawing.Point(16, 12);
            this.pnlKpiRevenue.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.pnlKpiRevenue.Name = "pnlKpiRevenue";
            this.pnlKpiRevenue.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlKpiRevenue.Size = new System.Drawing.Size(278, 68);
            this.pnlKpiRevenue.TabIndex = 0;
            // 
            // lblKpiRevenueTitle
            // 
            this.lblKpiRevenueTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKpiRevenueTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblKpiRevenueTitle.ForeColor = System.Drawing.Color.FromArgb(230, 255, 240);
            this.lblKpiRevenueTitle.Location = new System.Drawing.Point(12, 8);
            this.lblKpiRevenueTitle.Name = "lblKpiRevenueTitle";
            this.lblKpiRevenueTitle.Size = new System.Drawing.Size(254, 20);
            this.lblKpiRevenueTitle.TabIndex = 0;
            this.lblKpiRevenueTitle.Text = "TỔNG DOANH THU THỰC THU";
            // 
            // lblKpiRevenueValue
            // 
            this.lblKpiRevenueValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKpiRevenueValue.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblKpiRevenueValue.ForeColor = System.Drawing.Color.White;
            this.lblKpiRevenueValue.Location = new System.Drawing.Point(12, 28);
            this.lblKpiRevenueValue.Name = "lblKpiRevenueValue";
            this.lblKpiRevenueValue.Size = new System.Drawing.Size(254, 32);
            this.lblKpiRevenueValue.TabIndex = 1;
            this.lblKpiRevenueValue.Text = "0 đ";
            this.lblKpiRevenueValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlKpiOrders
            // 
            this.pnlKpiOrders.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.pnlKpiOrders.Controls.Add(this.lblKpiOrdersValue);
            this.pnlKpiOrders.Controls.Add(this.lblKpiOrdersTitle);
            this.pnlKpiOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiOrders.Location = new System.Drawing.Point(306, 12);
            this.pnlKpiOrders.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.pnlKpiOrders.Name = "pnlKpiOrders";
            this.pnlKpiOrders.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlKpiOrders.Size = new System.Drawing.Size(278, 68);
            this.pnlKpiOrders.TabIndex = 1;
            // 
            // lblKpiOrdersTitle
            // 
            this.lblKpiOrdersTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKpiOrdersTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblKpiOrdersTitle.ForeColor = System.Drawing.Color.FromArgb(225, 245, 255);
            this.lblKpiOrdersTitle.Location = new System.Drawing.Point(12, 8);
            this.lblKpiOrdersTitle.Name = "lblKpiOrdersTitle";
            this.lblKpiOrdersTitle.Size = new System.Drawing.Size(254, 20);
            this.lblKpiOrdersTitle.TabIndex = 0;
            this.lblKpiOrdersTitle.Text = "TỔNG SỐ ĐƠN ĐÃ THANH TOÁN";
            // 
            // lblKpiOrdersValue
            // 
            this.lblKpiOrdersValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKpiOrdersValue.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblKpiOrdersValue.ForeColor = System.Drawing.Color.White;
            this.lblKpiOrdersValue.Location = new System.Drawing.Point(12, 28);
            this.lblKpiOrdersValue.Name = "lblKpiOrdersValue";
            this.lblKpiOrdersValue.Size = new System.Drawing.Size(254, 32);
            this.lblKpiOrdersValue.TabIndex = 1;
            this.lblKpiOrdersValue.Text = "0 đơn";
            this.lblKpiOrdersValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlKpiDiscount
            // 
            this.pnlKpiDiscount.BackColor = System.Drawing.Color.FromArgb(211, 84, 0);
            this.pnlKpiDiscount.Controls.Add(this.lblKpiDiscountValue);
            this.pnlKpiDiscount.Controls.Add(this.lblKpiDiscountTitle);
            this.pnlKpiDiscount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiDiscount.Location = new System.Drawing.Point(596, 12);
            this.pnlKpiDiscount.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.pnlKpiDiscount.Name = "pnlKpiDiscount";
            this.pnlKpiDiscount.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlKpiDiscount.Size = new System.Drawing.Size(278, 68);
            this.pnlKpiDiscount.TabIndex = 2;
            // 
            // lblKpiDiscountTitle
            // 
            this.lblKpiDiscountTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKpiDiscountTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblKpiDiscountTitle.ForeColor = System.Drawing.Color.FromArgb(255, 238, 225);
            this.lblKpiDiscountTitle.Location = new System.Drawing.Point(12, 8);
            this.lblKpiDiscountTitle.Name = "lblKpiDiscountTitle";
            this.lblKpiDiscountTitle.Size = new System.Drawing.Size(254, 20);
            this.lblKpiDiscountTitle.TabIndex = 0;
            this.lblKpiDiscountTitle.Text = "TỔNG TIỀN KHUYẾN MÃI GIẢM";
            // 
            // lblKpiDiscountValue
            // 
            this.lblKpiDiscountValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKpiDiscountValue.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblKpiDiscountValue.ForeColor = System.Drawing.Color.White;
            this.lblKpiDiscountValue.Location = new System.Drawing.Point(12, 28);
            this.lblKpiDiscountValue.Name = "lblKpiDiscountValue";
            this.lblKpiDiscountValue.Size = new System.Drawing.Size(254, 32);
            this.lblKpiDiscountValue.TabIndex = 1;
            this.lblKpiDiscountValue.Text = "0 đ";
            this.lblKpiDiscountValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlKpiBestSeller
            // 
            this.pnlKpiBestSeller.BackColor = System.Drawing.Color.FromArgb(111, 78, 55);
            this.pnlKpiBestSeller.Controls.Add(this.lblKpiBestValue);
            this.pnlKpiBestSeller.Controls.Add(this.lblKpiBestTitle);
            this.pnlKpiBestSeller.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKpiBestSeller.Location = new System.Drawing.Point(886, 12);
            this.pnlKpiBestSeller.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.pnlKpiBestSeller.Name = "pnlKpiBestSeller";
            this.pnlKpiBestSeller.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlKpiBestSeller.Size = new System.Drawing.Size(278, 68);
            this.pnlKpiBestSeller.TabIndex = 3;
            // 
            // lblKpiBestTitle
            // 
            this.lblKpiBestTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKpiBestTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblKpiBestTitle.ForeColor = System.Drawing.Color.FromArgb(245, 235, 225);
            this.lblKpiBestTitle.Location = new System.Drawing.Point(12, 8);
            this.lblKpiBestTitle.Name = "lblKpiBestTitle";
            this.lblKpiBestTitle.Size = new System.Drawing.Size(254, 20);
            this.lblKpiBestTitle.TabIndex = 0;
            this.lblKpiBestTitle.Text = "MÓN BÁN CHẠY NHẤT (TOP 1)";
            // 
            // lblKpiBestValue
            // 
            this.lblKpiBestValue.AutoEllipsis = true;
            this.lblKpiBestValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKpiBestValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblKpiBestValue.ForeColor = System.Drawing.Color.FromArgb(255, 224, 130);
            this.lblKpiBestValue.Location = new System.Drawing.Point(12, 28);
            this.lblKpiBestValue.Name = "lblKpiBestValue";
            this.lblKpiBestValue.Size = new System.Drawing.Size(254, 32);
            this.lblKpiBestValue.TabIndex = 1;
            this.lblKpiBestValue.Text = "---";
            this.lblKpiBestValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlChartsRow
            // 
            this.pnlChartsRow.BackColor = System.Drawing.Color.FromArgb(250, 247, 242);
            this.pnlChartsRow.ColumnCount = 2;
            this.pnlChartsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.pnlChartsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.pnlChartsRow.Controls.Add(this.chartRevenue, 0, 0);
            this.pnlChartsRow.Controls.Add(this.ChartProducts, 1, 0);
            this.pnlChartsRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChartsRow.Location = new System.Drawing.Point(0, 194);
            this.pnlChartsRow.Name = "pnlChartsRow";
            this.pnlChartsRow.Padding = new System.Windows.Forms.Padding(12, 4, 12, 8);
            this.pnlChartsRow.RowCount = 1;
            this.pnlChartsRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlChartsRow.Size = new System.Drawing.Size(1180, 280);
            this.pnlChartsRow.TabIndex = 3;
            // 
            // chartRevenue
            // 
            this.chartRevenue.BackColor = System.Drawing.Color.White;
            this.chartRevenue.BorderlineColor = System.Drawing.Color.FromArgb(230, 224, 216);
            this.chartRevenue.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartAreaRev.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(240, 240, 240);
            chartAreaRev.AxisY.LabelStyle.Format = "N0";
            chartAreaRev.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(235, 235, 235);
            chartAreaRev.Name = "ChartArea1";
            this.chartRevenue.ChartAreas.Add(chartAreaRev);
            this.chartRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            legendRev.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legendRev.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
            legendRev.Name = "Legend1";
            this.chartRevenue.Legends.Add(legendRev);
            this.chartRevenue.Location = new System.Drawing.Point(16, 8);
            this.chartRevenue.Margin = new System.Windows.Forms.Padding(4);
            this.chartRevenue.Name = "chartRevenue";
            seriesRev.ChartArea = "ChartArea1";
            seriesRev.Color = System.Drawing.Color.FromArgb(111, 78, 55);
            seriesRev.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            seriesRev.IsValueShownAsLabel = true;
            seriesRev.LabelFormat = "N0";
            seriesRev.Legend = "Legend1";
            seriesRev.LegendText = "Doanh thu thực thu (VNĐ)";
            seriesRev.Name = "DoanhThu";
            this.chartRevenue.Series.Add(seriesRev);
            this.chartRevenue.Size = new System.Drawing.Size(662, 264);
            this.chartRevenue.TabIndex = 0;
            titleRev.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            titleRev.ForeColor = System.Drawing.Color.FromArgb(74, 44, 42);
            titleRev.Name = "Title1";
            titleRev.Text = "BIỂU ĐỒ DOANH THU THEO NGÀY (VNĐ)";
            this.chartRevenue.Titles.Add(titleRev);
            // 
            // ChartProducts
            // 
            this.ChartProducts.BackColor = System.Drawing.Color.White;
            this.ChartProducts.BorderlineColor = System.Drawing.Color.FromArgb(230, 224, 216);
            this.ChartProducts.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartAreaProd.Name = "ChartArea1";
            this.ChartProducts.ChartAreas.Add(chartAreaProd);
            this.ChartProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            legendProd.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Right;
            legendProd.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
            legendProd.Name = "Legend1";
            this.ChartProducts.Legends.Add(legendProd);
            this.ChartProducts.Location = new System.Drawing.Point(686, 8);
            this.ChartProducts.Margin = new System.Windows.Forms.Padding(4);
            this.ChartProducts.Name = "ChartProducts";
            this.ChartProducts.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            seriesProd.ChartArea = "ChartArea1";
            seriesProd.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            seriesProd.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            seriesProd.IsValueShownAsLabel = true;
            seriesProd.Legend = "Legend1";
            seriesProd.Name = "TopMon";
            this.ChartProducts.Series.Add(seriesProd);
            this.ChartProducts.Size = new System.Drawing.Size(478, 264);
            this.ChartProducts.TabIndex = 1;
            titleProd.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            titleProd.ForeColor = System.Drawing.Color.FromArgb(74, 44, 42);
            titleProd.Name = "Title1";
            titleProd.Text = "TỶ TRỌNG TOP MÓN BÁN CHẠY (SỐ LY)";
            this.ChartProducts.Titles.Add(titleProd);
            // 
            // grpRevenueTable
            // 
            this.grpRevenueTable.BackColor = System.Drawing.Color.White;
            this.grpRevenueTable.Controls.Add(this.dgvRevenueStats);
            this.grpRevenueTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRevenueTable.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpRevenueTable.ForeColor = System.Drawing.Color.FromArgb(74, 44, 42);
            this.grpRevenueTable.Location = new System.Drawing.Point(0, 474);
            this.grpRevenueTable.Name = "grpRevenueTable";
            this.grpRevenueTable.Padding = new System.Windows.Forms.Padding(14, 6, 14, 12);
            this.grpRevenueTable.Size = new System.Drawing.Size(1180, 226);
            this.grpRevenueTable.TabIndex = 4;
            this.grpRevenueTable.TabStop = false;
            this.grpRevenueTable.Text = "Bảng tổng hợp chi tiết doanh thu theo ngày";
            // 
            // dgvRevenueStats
            // 
            this.dgvRevenueStats.AllowUserToAddRows = false;
            this.dgvRevenueStats.AllowUserToDeleteRows = false;
            this.dgvRevenueStats.AllowUserToResizeRows = false;
            statAltStyle.BackColor = System.Drawing.Color.FromArgb(252, 249, 245);
            this.dgvRevenueStats.AlternatingRowsDefaultCellStyle = statAltStyle;
            this.dgvRevenueStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRevenueStats.BackgroundColor = System.Drawing.Color.White;
            this.dgvRevenueStats.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRevenueStats.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvRevenueStats.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            statHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            statHeaderStyle.BackColor = System.Drawing.Color.FromArgb(74, 44, 42);
            statHeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            statHeaderStyle.ForeColor = System.Drawing.Color.White;
            statHeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(74, 44, 42);
            statHeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvRevenueStats.ColumnHeadersDefaultCellStyle = statHeaderStyle;
            this.dgvRevenueStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRevenueStats.ColumnHeadersHeight = 36;
            this.dgvRevenueStats.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStatDate,
            this.colStatOrders,
            this.colStatDiscount,
            this.colStatRevenue});
            statCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            statCellStyle.BackColor = System.Drawing.Color.White;
            statCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            statCellStyle.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            statCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(238, 224, 210);
            statCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(74, 44, 42);
            this.dgvRevenueStats.DefaultCellStyle = statCellStyle;
            this.dgvRevenueStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRevenueStats.EnableHeadersVisualStyles = false;
            this.dgvRevenueStats.GridColor = System.Drawing.Color.FromArgb(235, 230, 225);
            this.dgvRevenueStats.Location = new System.Drawing.Point(14, 26);
            this.dgvRevenueStats.MultiSelect = false;
            this.dgvRevenueStats.Name = "dgvRevenueStats";
            this.dgvRevenueStats.ReadOnly = true;
            this.dgvRevenueStats.RowHeadersVisible = false;
            this.dgvRevenueStats.RowTemplate.Height = 32;
            this.dgvRevenueStats.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRevenueStats.Size = new System.Drawing.Size(1152, 188);
            this.dgvRevenueStats.TabIndex = 0;
            // 
            // colStatDate
            // 
            this.colStatDate.DataPropertyName = "Date";
            dateCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dateCellStyle.Format = "dd/MM/yyyy";
            this.colStatDate.DefaultCellStyle = dateCellStyle;
            this.colStatDate.FillWeight = 22F;
            this.colStatDate.HeaderText = "Ngày bán hàng";
            this.colStatDate.MinimumWidth = 6;
            this.colStatDate.Name = "colStatDate";
            this.colStatDate.ReadOnly = true;
            // 
            // colStatOrders
            // 
            this.colStatOrders.DataPropertyName = "TotalOrders";
            orderQtyCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colStatOrders.DefaultCellStyle = orderQtyCellStyle;
            this.colStatOrders.FillWeight = 22F;
            this.colStatOrders.HeaderText = "Số hóa đơn đã thanh toán";
            this.colStatOrders.MinimumWidth = 6;
            this.colStatOrders.Name = "colStatOrders";
            this.colStatOrders.ReadOnly = true;
            // 
            // colStatDiscount
            // 
            this.colStatDiscount.DataPropertyName = "TotalDiscount";
            discountCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            discountCellStyle.Format = "N0";
            this.colStatDiscount.DefaultCellStyle = discountCellStyle;
            this.colStatDiscount.FillWeight = 26F;
            this.colStatDiscount.HeaderText = "Tổng khuyến mãi giảm (VNĐ)";
            this.colStatDiscount.MinimumWidth = 6;
            this.colStatDiscount.Name = "colStatDiscount";
            this.colStatDiscount.ReadOnly = true;
            // 
            // colStatRevenue
            // 
            this.colStatRevenue.DataPropertyName = "TotalRevenue";
            revenueCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            revenueCellStyle.Format = "N0";
            this.colStatRevenue.DefaultCellStyle = revenueCellStyle;
            this.colStatRevenue.FillWeight = 30F;
            this.colStatRevenue.HeaderText = "Doanh thu thực thu (VNĐ)";
            this.colStatRevenue.MinimumWidth = 6;
            this.colStatRevenue.Name = "colStatRevenue";
            this.colStatRevenue.ReadOnly = true;
            // 
            // FrmChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1180, 700);
            this.Controls.Add(this.grpRevenueTable);
            this.Controls.Add(this.pnlChartsRow);
            this.Controls.Add(this.pnlKpiRow);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.Name = "FrmChart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê Doanh thu & Biểu đồ";
            this.Load += new System.EventHandler(this.FrmChart_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlKpiRow.ResumeLayout(false);
            this.pnlKpiRevenue.ResumeLayout(false);
            this.pnlKpiOrders.ResumeLayout(false);
            this.pnlKpiDiscount.ResumeLayout(false);
            this.pnlKpiBestSeller.ResumeLayout(false);
            this.pnlChartsRow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartProducts)).EndInit();
            this.grpRevenueTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenueStats)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Button btnFilterChart;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.RadioButton rdoYear;
        private System.Windows.Forms.RadioButton rdoMonth;
        private System.Windows.Forms.RadioButton rdoWeek;
        private System.Windows.Forms.Label lblQuickRange;
        private System.Windows.Forms.TableLayoutPanel pnlKpiRow;
        private System.Windows.Forms.Panel pnlKpiRevenue;
        private System.Windows.Forms.Label lblKpiRevenueValue;
        private System.Windows.Forms.Label lblKpiRevenueTitle;
        private System.Windows.Forms.Panel pnlKpiOrders;
        private System.Windows.Forms.Label lblKpiOrdersValue;
        private System.Windows.Forms.Label lblKpiOrdersTitle;
        private System.Windows.Forms.Panel pnlKpiDiscount;
        private System.Windows.Forms.Label lblKpiDiscountValue;
        private System.Windows.Forms.Label lblKpiDiscountTitle;
        private System.Windows.Forms.Panel pnlKpiBestSeller;
        private System.Windows.Forms.Label lblKpiBestValue;
        private System.Windows.Forms.Label lblKpiBestTitle;
        private System.Windows.Forms.TableLayoutPanel pnlChartsRow;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenue;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartProducts;
        private System.Windows.Forms.GroupBox grpRevenueTable;
        private System.Windows.Forms.DataGridView dgvRevenueStats;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatOrders;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatRevenue;
    }
}
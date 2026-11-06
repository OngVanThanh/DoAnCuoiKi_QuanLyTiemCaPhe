namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmPrintReview
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.orderDetailDTOBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblPrinter = new System.Windows.Forms.Label();
            this.cboPrinters = new System.Windows.Forms.ComboBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.tabControlInvoice = new System.Windows.Forms.TabControl();
            this.tabVisualPreview = new System.Windows.Forms.TabPage();
            this.pnlReceiptContainer = new System.Windows.Forms.Panel();
            this.pnlPaper = new System.Windows.Forms.Panel();
            this.lblThanks = new System.Windows.Forms.Label();
            this.lblDash5 = new System.Windows.Forms.Label();
            this.pnlTotals = new System.Windows.Forms.Panel();
            this.lblFinalValue = new System.Windows.Forms.Label();
            this.lblFinalCaption = new System.Windows.Forms.Label();
            this.lblDiscountValue = new System.Windows.Forms.Label();
            this.lblDiscountCaption = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblTotalCaption = new System.Windows.Forms.Label();
            this.lblDash4 = new System.Windows.Forms.Label();
            this.lblItemDetails = new System.Windows.Forms.Label();
            this.lblDash3 = new System.Windows.Forms.Label();
            this.lblColHeaders = new System.Windows.Forms.Label();
            this.lblDash2 = new System.Windows.Forms.Label();
            this.pnlOrderMeta = new System.Windows.Forms.Panel();
            this.lblCashier = new System.Windows.Forms.Label();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.lblTableName = new System.Windows.Forms.Label();
            this.lblOrderNo = new System.Windows.Forms.Label();
            this.lblInvoiceTitle = new System.Windows.Forms.Label();
            this.lblDash1 = new System.Windows.Forms.Label();
            this.lblShopPhone = new System.Windows.Forms.Label();
            this.lblShopAddress = new System.Windows.Forms.Label();
            this.lblShopName = new System.Windows.Forms.Label();
            this.tabRdlcReport = new System.Windows.Forms.TabPage();
            this.reportViewerInvoice = new Microsoft.Reporting.WinForms.ReportViewer();
            this.printDocumentInvoice = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.orderDetailDTOBindingSource)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.tabControlInvoice.SuspendLayout();
            this.tabVisualPreview.SuspendLayout();
            this.pnlReceiptContainer.SuspendLayout();
            this.pnlPaper.SuspendLayout();
            this.pnlTotals.SuspendLayout();
            this.pnlOrderMeta.SuspendLayout();
            this.tabRdlcReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // orderDetailDTOBindingSource
            // 
            this.orderDetailDTOBindingSource.DataSource = typeof(DoAnCuoiKi_QuanLyTiemCaPhe.DTO.OrderDetailDTO);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(860, 48);
            this.pnlHeader.TabIndex = 2;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(16, 10);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(465, 28);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "XEM TRƯỚC & IN HÓA ĐƠN NHIỆT (KHỔ K80)";
            this.lblHeaderTitle.UseMnemonic = false;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(247)))), ((int)(((byte)(242)))));
            this.pnlBottom.Controls.Add(this.lblPrinter);
            this.pnlBottom.Controls.Add(this.btnPrint);
            this.pnlBottom.Controls.Add(this.cboPrinters);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 600);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(860, 60);
            this.pnlBottom.TabIndex = 1;
            // 
            // lblPrinter
            // 
            this.lblPrinter.AutoSize = true;
            this.lblPrinter.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrinter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblPrinter.Location = new System.Drawing.Point(20, 20);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(102, 21);
            this.lblPrinter.TabIndex = 2;
            this.lblPrinter.Text = "Chọn máy in:";
            // 
            // cboPrinters
            // 
            this.cboPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrinters.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPrinters.FormattingEnabled = true;
            this.cboPrinters.Location = new System.Drawing.Point(128, 16);
            this.cboPrinters.Name = "cboPrinters";
            this.cboPrinters.Size = new System.Drawing.Size(390, 29);
            this.cboPrinters.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(111)))), ((int)(((byte)(67)))));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(540, 11);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(300, 38);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "✔  Thực hiện in hóa đơn";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // tabControlInvoice
            // 
            this.tabControlInvoice.Controls.Add(this.tabVisualPreview);
            this.tabControlInvoice.Controls.Add(this.tabRdlcReport);
            this.tabControlInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlInvoice.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlInvoice.Location = new System.Drawing.Point(0, 48);
            this.tabControlInvoice.Name = "tabControlInvoice";
            this.tabControlInvoice.SelectedIndex = 0;
            this.tabControlInvoice.Size = new System.Drawing.Size(860, 552);
            this.tabControlInvoice.TabIndex = 0;
            // 
            // tabVisualPreview
            // 
            this.tabVisualPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(86)))), ((int)(((byte)(89)))));
            this.tabVisualPreview.Controls.Add(this.pnlReceiptContainer);
            this.tabVisualPreview.Location = new System.Drawing.Point(4, 30);
            this.tabVisualPreview.Name = "tabVisualPreview";
            this.tabVisualPreview.Padding = new System.Windows.Forms.Padding(3);
            this.tabVisualPreview.Size = new System.Drawing.Size(852, 518);
            this.tabVisualPreview.TabIndex = 0;
            this.tabVisualPreview.Text = "📄 Xem trước Phiếu K80 Trực quan";
            // 
            // pnlReceiptContainer
            // 
            this.pnlReceiptContainer.AutoScroll = true;
            this.pnlReceiptContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(86)))), ((int)(((byte)(89)))));
            this.pnlReceiptContainer.Controls.Add(this.pnlPaper);
            this.pnlReceiptContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlReceiptContainer.Location = new System.Drawing.Point(3, 3);
            this.pnlReceiptContainer.Name = "pnlReceiptContainer";
            this.pnlReceiptContainer.Padding = new System.Windows.Forms.Padding(0, 20, 0, 20);
            this.pnlReceiptContainer.Size = new System.Drawing.Size(846, 512);
            this.pnlReceiptContainer.TabIndex = 0;
            this.pnlReceiptContainer.Resize += new System.EventHandler(this.pnlReceiptContainer_Resize);
            // 
            // pnlPaper
            // 
            this.pnlPaper.BackColor = System.Drawing.Color.White;
            this.pnlPaper.Controls.Add(this.lblThanks);
            this.pnlPaper.Controls.Add(this.lblDash5);
            this.pnlPaper.Controls.Add(this.pnlTotals);
            this.pnlPaper.Controls.Add(this.lblDash4);
            this.pnlPaper.Controls.Add(this.lblItemDetails);
            this.pnlPaper.Controls.Add(this.lblDash3);
            this.pnlPaper.Controls.Add(this.lblColHeaders);
            this.pnlPaper.Controls.Add(this.lblDash2);
            this.pnlPaper.Controls.Add(this.pnlOrderMeta);
            this.pnlPaper.Controls.Add(this.lblInvoiceTitle);
            this.pnlPaper.Controls.Add(this.lblDash1);
            this.pnlPaper.Controls.Add(this.lblShopPhone);
            this.pnlPaper.Controls.Add(this.lblShopAddress);
            this.pnlPaper.Controls.Add(this.lblShopName);
            this.pnlPaper.Location = new System.Drawing.Point(253, 15);
            this.pnlPaper.Name = "pnlPaper";
            this.pnlPaper.Padding = new System.Windows.Forms.Padding(12, 14, 12, 14);
            this.pnlPaper.Size = new System.Drawing.Size(340, 480);
            this.pnlPaper.TabIndex = 0;
            // 
            // lblThanks
            // 
            this.lblThanks.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblThanks.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThanks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lblThanks.Location = new System.Drawing.Point(12, 432);
            this.lblThanks.Name = "lblThanks";
            this.lblThanks.Size = new System.Drawing.Size(316, 36);
            this.lblThanks.TabIndex = 13;
            this.lblThanks.Text = "Cảm ơn Quý khách & Hẹn gặp lại!\r\n--- Wifi: TheCoffeeHouse / Pass: 12345678 ---";
            this.lblThanks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDash5
            // 
            this.lblDash5.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDash5.Font = new System.Drawing.Font("Courier New", 8F);
            this.lblDash5.Location = new System.Drawing.Point(12, 418);
            this.lblDash5.Name = "lblDash5";
            this.lblDash5.Size = new System.Drawing.Size(316, 14);
            this.lblDash5.TabIndex = 12;
            this.lblDash5.Text = "------------------------------------------------";
            this.lblDash5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTotals
            // 
            this.pnlTotals.Controls.Add(this.lblFinalValue);
            this.pnlTotals.Controls.Add(this.lblFinalCaption);
            this.pnlTotals.Controls.Add(this.lblDiscountValue);
            this.pnlTotals.Controls.Add(this.lblDiscountCaption);
            this.pnlTotals.Controls.Add(this.lblTotalValue);
            this.pnlTotals.Controls.Add(this.lblTotalCaption);
            this.pnlTotals.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTotals.Location = new System.Drawing.Point(12, 342);
            this.pnlTotals.Name = "pnlTotals";
            this.pnlTotals.Size = new System.Drawing.Size(316, 76);
            this.pnlTotals.TabIndex = 11;
            // 
            // lblFinalValue
            // 
            this.lblFinalValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFinalValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(28)))), ((int)(((byte)(26)))));
            this.lblFinalValue.Location = new System.Drawing.Point(160, 48);
            this.lblFinalValue.Name = "lblFinalValue";
            this.lblFinalValue.Size = new System.Drawing.Size(150, 22);
            this.lblFinalValue.TabIndex = 5;
            this.lblFinalValue.Text = "130,248 đ";
            this.lblFinalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinalCaption
            // 
            this.lblFinalCaption.AutoSize = true;
            this.lblFinalCaption.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinalCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(28)))), ((int)(((byte)(26)))));
            this.lblFinalCaption.Location = new System.Drawing.Point(4, 48);
            this.lblFinalCaption.Name = "lblFinalCaption";
            this.lblFinalCaption.Size = new System.Drawing.Size(175, 23);
            this.lblFinalCaption.TabIndex = 4;
            this.lblFinalCaption.Text = "TỔNG THANH TOÁN:";
            // 
            // lblDiscountValue
            // 
            this.lblDiscountValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDiscountValue.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblDiscountValue.Location = new System.Drawing.Point(170, 25);
            this.lblDiscountValue.Name = "lblDiscountValue";
            this.lblDiscountValue.Size = new System.Drawing.Size(140, 18);
            this.lblDiscountValue.TabIndex = 3;
            this.lblDiscountValue.Text = "-13,400 đ";
            this.lblDiscountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiscountCaption
            // 
            this.lblDiscountCaption.AutoSize = true;
            this.lblDiscountCaption.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblDiscountCaption.Location = new System.Drawing.Point(4, 25);
            this.lblDiscountCaption.Name = "lblDiscountCaption";
            this.lblDiscountCaption.Size = new System.Drawing.Size(152, 19);
            this.lblDiscountCaption.TabIndex = 2;
            this.lblDiscountCaption.Text = "Giảm giá (CHAOMUNG):";
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTotalValue.Location = new System.Drawing.Point(170, 4);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(140, 18);
            this.lblTotalValue.TabIndex = 1;
            this.lblTotalValue.Text = "134,000 đ";
            this.lblTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalCaption
            // 
            this.lblTotalCaption.AutoSize = true;
            this.lblTotalCaption.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTotalCaption.Location = new System.Drawing.Point(4, 4);
            this.lblTotalCaption.Name = "lblTotalCaption";
            this.lblTotalCaption.Size = new System.Drawing.Size(107, 19);
            this.lblTotalCaption.TabIndex = 0;
            this.lblTotalCaption.Text = "Tổng tiền hàng:";
            // 
            // lblDash4
            // 
            this.lblDash4.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDash4.Font = new System.Drawing.Font("Courier New", 8F);
            this.lblDash4.Location = new System.Drawing.Point(12, 328);
            this.lblDash4.Name = "lblDash4";
            this.lblDash4.Size = new System.Drawing.Size(316, 14);
            this.lblDash4.TabIndex = 10;
            this.lblDash4.Text = "------------------------------------------------";
            this.lblDash4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblItemDetails
            // 
            this.lblItemDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblItemDetails.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblItemDetails.Location = new System.Drawing.Point(12, 258);
            this.lblItemDetails.Name = "lblItemDetails";
            this.lblItemDetails.Size = new System.Drawing.Size(316, 70);
            this.lblItemDetails.TabIndex = 9;
            this.lblItemDetails.Text = "Bạc xỉu đá                     2      32,000      64,000\r\nTrà đào cam sả           1      40,000      40,000\r\nBánh Croissant bơ tỏi        1      30,000      30,000";
            // 
            // lblDash3
            // 
            this.lblDash3.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDash3.Font = new System.Drawing.Font("Courier New", 8F);
            this.lblDash3.Location = new System.Drawing.Point(12, 244);
            this.lblDash3.Name = "lblDash3";
            this.lblDash3.Size = new System.Drawing.Size(316, 14);
            this.lblDash3.TabIndex = 8;
            this.lblDash3.Text = "------------------------------------------------";
            this.lblDash3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblColHeaders
            // 
            this.lblColHeaders.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblColHeaders.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblColHeaders.Location = new System.Drawing.Point(12, 224);
            this.lblColHeaders.Name = "lblColHeaders";
            this.lblColHeaders.Size = new System.Drawing.Size(316, 20);
            this.lblColHeaders.TabIndex = 7;
            this.lblColHeaders.Text = "Tên món                     SL      Đ.Giá      T.Tiền";
            // 
            // lblDash2
            // 
            this.lblDash2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDash2.Font = new System.Drawing.Font("Courier New", 8F);
            this.lblDash2.Location = new System.Drawing.Point(12, 210);
            this.lblDash2.Name = "lblDash2";
            this.lblDash2.Size = new System.Drawing.Size(316, 14);
            this.lblDash2.TabIndex = 6;
            this.lblDash2.Text = "------------------------------------------------";
            this.lblDash2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlOrderMeta
            // 
            this.pnlOrderMeta.Controls.Add(this.lblCashier);
            this.pnlOrderMeta.Controls.Add(this.lblOrderDate);
            this.pnlOrderMeta.Controls.Add(this.lblTableName);
            this.pnlOrderMeta.Controls.Add(this.lblOrderNo);
            this.pnlOrderMeta.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOrderMeta.Location = new System.Drawing.Point(12, 160);
            this.pnlOrderMeta.Name = "pnlOrderMeta";
            this.pnlOrderMeta.Size = new System.Drawing.Size(316, 50);
            this.pnlOrderMeta.TabIndex = 5;
            // 
            // lblCashier
            // 
            this.lblCashier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCashier.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCashier.Location = new System.Drawing.Point(130, 26);
            this.lblCashier.Name = "lblCashier";
            this.lblCashier.Size = new System.Drawing.Size(180, 20);
            this.lblCashier.TabIndex = 3;
            this.lblCashier.Text = "TN: Tấn Phát";
            this.lblCashier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrderDate.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblOrderDate.Location = new System.Drawing.Point(130, 4);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(180, 20);
            this.lblOrderDate.TabIndex = 2;
            this.lblOrderDate.Text = "Ngày: 04/11/2026 14:30";
            this.lblOrderDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTableName.Location = new System.Drawing.Point(4, 26);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(84, 19);
            this.lblTableName.TabIndex = 1;
            this.lblTableName.Text = "Bàn: Bàn 02";
            // 
            // lblOrderNo
            // 
            this.lblOrderNo.AutoSize = true;
            this.lblOrderNo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblOrderNo.Location = new System.Drawing.Point(4, 4);
            this.lblOrderNo.Name = "lblOrderNo";
            this.lblOrderNo.Size = new System.Drawing.Size(117, 19);
            this.lblOrderNo.TabIndex = 0;
            this.lblOrderNo.Text = "Số HĐ: #HD01024";
            // 
            // lblInvoiceTitle
            // 
            this.lblInvoiceTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInvoiceTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoiceTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(28)))), ((int)(((byte)(26)))));
            this.lblInvoiceTitle.Location = new System.Drawing.Point(12, 130);
            this.lblInvoiceTitle.Name = "lblInvoiceTitle";
            this.lblInvoiceTitle.Size = new System.Drawing.Size(316, 30);
            this.lblInvoiceTitle.TabIndex = 4;
            this.lblInvoiceTitle.Text = "PHIẾU THANH TOÁN (RECEIPT)";
            this.lblInvoiceTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDash1
            // 
            this.lblDash1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDash1.Font = new System.Drawing.Font("Courier New", 8F);
            this.lblDash1.Location = new System.Drawing.Point(12, 116);
            this.lblDash1.Name = "lblDash1";
            this.lblDash1.Size = new System.Drawing.Size(316, 14);
            this.lblDash1.TabIndex = 3;
            this.lblDash1.Text = "------------------------------------------------";
            this.lblDash1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShopPhone
            // 
            this.lblShopPhone.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblShopPhone.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblShopPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblShopPhone.Location = new System.Drawing.Point(12, 94);
            this.lblShopPhone.Name = "lblShopPhone";
            this.lblShopPhone.Size = new System.Drawing.Size(316, 22);
            this.lblShopPhone.TabIndex = 2;
            this.lblShopPhone.Text = "Hotline: 1800 6936 - 0909 123 456";
            this.lblShopPhone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShopAddress
            // 
            this.lblShopAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblShopAddress.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblShopAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblShopAddress.Location = new System.Drawing.Point(12, 44);
            this.lblShopAddress.Name = "lblShopAddress";
            this.lblShopAddress.Size = new System.Drawing.Size(316, 50);
            this.lblShopAddress.TabIndex = 1;
            this.lblShopAddress.Text = "Đ/c: 86-88 Cao Thắng, Quận 3, TP.HCM";
            this.lblShopAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShopName
            // 
            this.lblShopName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblShopName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShopName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(28)))), ((int)(((byte)(26)))));
            this.lblShopName.Location = new System.Drawing.Point(12, 14);
            this.lblShopName.Name = "lblShopName";
            this.lblShopName.Size = new System.Drawing.Size(316, 30);
            this.lblShopName.TabIndex = 0;
            this.lblShopName.Text = "THE COFFEE HOUSE";
            this.lblShopName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabRdlcReport
            // 
            this.tabRdlcReport.Controls.Add(this.reportViewerInvoice);
            this.tabRdlcReport.Location = new System.Drawing.Point(4, 30);
            this.tabRdlcReport.Name = "tabRdlcReport";
            this.tabRdlcReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabRdlcReport.Size = new System.Drawing.Size(852, 518);
            this.tabRdlcReport.TabIndex = 1;
            this.tabRdlcReport.Text = "📊 Báo cáo RDLC (ReportViewer)";
            this.tabRdlcReport.UseVisualStyleBackColor = true;
            // 
            // reportViewerInvoice
            // 
            this.reportViewerInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSetInvoiceDetails";
            reportDataSource1.Value = this.orderDetailDTOBindingSource;
            this.reportViewerInvoice.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewerInvoice.LocalReport.ReportEmbeddedResource = "DoAnCuoiKi_QuanLyTiemCaPhe.Form.TanPhat.RptInvoiceK80.rdlc";
            this.reportViewerInvoice.Location = new System.Drawing.Point(3, 3);
            this.reportViewerInvoice.Name = "reportViewerInvoice";
            this.reportViewerInvoice.ServerReport.BearerToken = null;
            this.reportViewerInvoice.Size = new System.Drawing.Size(846, 512);
            this.reportViewerInvoice.TabIndex = 0;
            this.reportViewerInvoice.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewerInvoice.ZoomPercent = 100;
            // 
            // printDocumentInvoice
            // 
            this.printDocumentInvoice.DocumentName = "HoaDonThanhToan_K80";
            this.printDocumentInvoice.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocumentInvoice_PrintPage);
            // 
            // FrmPrintReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(237)))), ((int)(((byte)(228)))));
            this.ClientSize = new System.Drawing.Size(860, 660);
            this.Controls.Add(this.tabControlInvoice);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmPrintReview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xem trước & In hóa đơn K80 (Canvas + RDLC) - Tấn Phát";
            this.Load += new System.EventHandler(this.FrmPrintReview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.orderDetailDTOBindingSource)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.tabControlInvoice.ResumeLayout(false);
            this.tabVisualPreview.ResumeLayout(false);
            this.pnlReceiptContainer.ResumeLayout(false);
            this.pnlPaper.ResumeLayout(false);
            this.pnlTotals.ResumeLayout(false);
            this.pnlTotals.PerformLayout();
            this.pnlOrderMeta.ResumeLayout(false);
            this.pnlOrderMeta.PerformLayout();
            this.tabRdlcReport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource orderDetailDTOBindingSource;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblPrinter;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ComboBox cboPrinters;
        private System.Windows.Forms.TabControl tabControlInvoice;
        private System.Windows.Forms.TabPage tabVisualPreview;
        private System.Windows.Forms.TabPage tabRdlcReport;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewerInvoice;
        private System.Drawing.Printing.PrintDocument printDocumentInvoice;
        private System.Windows.Forms.Panel pnlReceiptContainer;
        private System.Windows.Forms.Panel pnlPaper;
        private System.Windows.Forms.Label lblShopName;
        private System.Windows.Forms.Label lblShopAddress;
        private System.Windows.Forms.Label lblShopPhone;
        private System.Windows.Forms.Label lblDash1;
        private System.Windows.Forms.Label lblInvoiceTitle;
        private System.Windows.Forms.Panel pnlOrderMeta;
        private System.Windows.Forms.Label lblOrderNo;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.Label lblCashier;
        private System.Windows.Forms.Label lblDash2;
        private System.Windows.Forms.Label lblColHeaders;
        private System.Windows.Forms.Label lblDash3;
        private System.Windows.Forms.Label lblItemDetails;
        private System.Windows.Forms.Label lblDash4;
        private System.Windows.Forms.Panel pnlTotals;
        private System.Windows.Forms.Label lblTotalCaption;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label lblDiscountCaption;
        private System.Windows.Forms.Label lblDiscountValue;
        private System.Windows.Forms.Label lblFinalCaption;
        private System.Windows.Forms.Label lblFinalValue;
        private System.Windows.Forms.Label lblDash5;
        private System.Windows.Forms.Label lblThanks;
    }
}

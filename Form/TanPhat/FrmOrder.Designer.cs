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
            this.pnlTables = new System.Windows.Forms.Panel();
            this.flpTableList = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSwitchTable = new System.Windows.Forms.Button();
            this.pnlOrder = new System.Windows.Forms.Panel();
            this.btnPayAndPrint = new System.Windows.Forms.Button();
            this.lblFinalAmount = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.btnRemoveProduct = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.dgvOrderDetails = new System.Windows.Forms.DataGridView();
            this.pnlProducts = new System.Windows.Forms.Panel();
            this.dgvProductList = new System.Windows.Forms.DataGridView();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.lblSearchProduct = new System.Windows.Forms.Label();
            this.cboCategoryFilter = new System.Windows.Forms.ComboBox();
            this.pnlTables.SuspendLayout();
            this.pnlOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).BeginInit();
            this.pnlProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTables
            // 
            this.pnlTables.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTables.Controls.Add(this.flpTableList);
            this.pnlTables.Controls.Add(this.btnSwitchTable);
            this.pnlTables.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTables.Location = new System.Drawing.Point(0, 0);
            this.pnlTables.Name = "pnlTables";
            this.pnlTables.Padding = new System.Windows.Forms.Padding(6);
            this.pnlTables.Size = new System.Drawing.Size(310, 560);
            this.pnlTables.TabIndex = 0;
            // 
            // flpTableList
            // 
            this.flpTableList.AutoScroll = true;
            this.flpTableList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTableList.Location = new System.Drawing.Point(6, 6);
            this.flpTableList.Name = "flpTableList";
            this.flpTableList.Size = new System.Drawing.Size(298, 504);
            this.flpTableList.TabIndex = 0;
            // 
            // btnSwitchTable
            // 
            this.btnSwitchTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSwitchTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSwitchTable.Location = new System.Drawing.Point(6, 510);
            this.btnSwitchTable.Name = "btnSwitchTable";
            this.btnSwitchTable.Size = new System.Drawing.Size(298, 44);
            this.btnSwitchTable.TabIndex = 1;
            this.btnSwitchTable.Text = "Chuyển / Gộp bàn";
            this.btnSwitchTable.UseVisualStyleBackColor = true;
            this.btnSwitchTable.Click += new System.EventHandler(this.btnSwitchTable_Click);
            // 
            // pnlOrder
            // 
            this.pnlOrder.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlOrder.Controls.Add(this.btnPayAndPrint);
            this.pnlOrder.Controls.Add(this.lblFinalAmount);
            this.pnlOrder.Controls.Add(this.lblTotalAmount);
            this.pnlOrder.Controls.Add(this.lblDiscount);
            this.pnlOrder.Controls.Add(this.txtDiscount);
            this.pnlOrder.Controls.Add(this.btnRemoveProduct);
            this.pnlOrder.Controls.Add(this.btnAddProduct);
            this.pnlOrder.Controls.Add(this.nudQuantity);
            this.pnlOrder.Controls.Add(this.dgvOrderDetails);
            this.pnlOrder.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlOrder.Location = new System.Drawing.Point(680, 0);
            this.pnlOrder.Name = "pnlOrder";
            this.pnlOrder.Padding = new System.Windows.Forms.Padding(8);
            this.pnlOrder.Size = new System.Drawing.Size(440, 560);
            this.pnlOrder.TabIndex = 1;
            // 
            // dgvOrderDetails
            // 
            this.dgvOrderDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderDetails.Location = new System.Drawing.Point(8, 8);
            this.dgvOrderDetails.Name = "dgvOrderDetails";
            this.dgvOrderDetails.RowHeadersWidth = 51;
            this.dgvOrderDetails.RowTemplate.Height = 24;
            this.dgvOrderDetails.Size = new System.Drawing.Size(424, 300);
            this.dgvOrderDetails.TabIndex = 0;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddProduct.Location = new System.Drawing.Point(12, 322);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(115, 34);
            this.btnAddProduct.TabIndex = 2;
            this.btnAddProduct.Text = "Thêm món";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnRemoveProduct
            // 
            this.btnRemoveProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveProduct.Location = new System.Drawing.Point(138, 322);
            this.btnRemoveProduct.Name = "btnRemoveProduct";
            this.btnRemoveProduct.Size = new System.Drawing.Size(115, 34);
            this.btnRemoveProduct.TabIndex = 3;
            this.btnRemoveProduct.Text = "Xóa món";
            this.btnRemoveProduct.UseVisualStyleBackColor = true;
            this.btnRemoveProduct.Click += new System.EventHandler(this.btnRemoveProduct_Click);
            // 
            // nudQuantity
            // 
            this.nudQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudQuantity.Location = new System.Drawing.Point(300, 326);
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
            this.nudQuantity.Size = new System.Drawing.Size(128, 26);
            this.nudQuantity.TabIndex = 1;
            this.nudQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblDiscount
            // 
            this.lblDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Location = new System.Drawing.Point(12, 376);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(172, 16);
            this.lblDiscount.TabIndex = 5;
            this.lblDiscount.Text = "Khuyến mãi (Mã / % / VNĐ):";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiscount.Location = new System.Drawing.Point(195, 373);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(233, 22);
            this.txtDiscount.TabIndex = 4;
            this.txtDiscount.TextChanged += new System.EventHandler(this.txtDiscount_TextChanged);
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(12, 415);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(110, 20);
            this.lblTotalAmount.TabIndex = 6;
            this.lblTotalAmount.Text = "Tổng tiền: 0 đ";
            // 
            // lblFinalAmount
            // 
            this.lblFinalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFinalAmount.AutoSize = true;
            this.lblFinalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFinalAmount.ForeColor = System.Drawing.Color.Firebrick;
            this.lblFinalAmount.Location = new System.Drawing.Point(12, 448);
            this.lblFinalAmount.Name = "lblFinalAmount";
            this.lblFinalAmount.Size = new System.Drawing.Size(145, 24);
            this.lblFinalAmount.TabIndex = 7;
            this.lblFinalAmount.Text = "Thanh toán: 0 đ";
            // 
            // btnPayAndPrint
            // 
            this.btnPayAndPrint.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPayAndPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayAndPrint.Location = new System.Drawing.Point(8, 502);
            this.btnPayAndPrint.Name = "btnPayAndPrint";
            this.btnPayAndPrint.Size = new System.Drawing.Size(424, 50);
            this.btnPayAndPrint.TabIndex = 8;
            this.btnPayAndPrint.Text = "Thanh toán & In hóa đơn";
            this.btnPayAndPrint.UseVisualStyleBackColor = true;
            this.btnPayAndPrint.Click += new System.EventHandler(this.btnPayAndPrint_Click);
            // 
            // pnlProducts
            // 
            this.pnlProducts.Controls.Add(this.dgvProductList);
            this.pnlProducts.Controls.Add(this.txtSearchProduct);
            this.pnlProducts.Controls.Add(this.lblSearchProduct);
            this.pnlProducts.Controls.Add(this.cboCategoryFilter);
            this.pnlProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProducts.Location = new System.Drawing.Point(310, 0);
            this.pnlProducts.Name = "pnlProducts";
            this.pnlProducts.Padding = new System.Windows.Forms.Padding(6);
            this.pnlProducts.Size = new System.Drawing.Size(370, 560);
            this.pnlProducts.TabIndex = 2;
            // 
            // cboCategoryFilter
            // 
            this.cboCategoryFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategoryFilter.FormattingEnabled = true;
            this.cboCategoryFilter.Location = new System.Drawing.Point(6, 6);
            this.cboCategoryFilter.Name = "cboCategoryFilter";
            this.cboCategoryFilter.Size = new System.Drawing.Size(358, 24);
            this.cboCategoryFilter.TabIndex = 0;
            this.cboCategoryFilter.SelectedIndexChanged += new System.EventHandler(this.cboCategoryFilter_SelectedIndexChanged);
            // 
            // lblSearchProduct
            // 
            this.lblSearchProduct.AutoSize = true;
            this.lblSearchProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSearchProduct.Location = new System.Drawing.Point(6, 30);
            this.lblSearchProduct.Name = "lblSearchProduct";
            this.lblSearchProduct.Padding = new System.Windows.Forms.Padding(0, 6, 0, 2);
            this.lblSearchProduct.Size = new System.Drawing.Size(102, 24);
            this.lblSearchProduct.TabIndex = 2;
            this.lblSearchProduct.Text = "Tìm nhanh món:";
            // 
            // txtSearchProduct
            // 
            this.txtSearchProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSearchProduct.Location = new System.Drawing.Point(6, 54);
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.Size = new System.Drawing.Size(358, 22);
            this.txtSearchProduct.TabIndex = 1;
            this.txtSearchProduct.TextChanged += new System.EventHandler(this.txtSearchProduct_TextChanged);
            // 
            // dgvProductList
            // 
            this.dgvProductList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductList.Location = new System.Drawing.Point(6, 76);
            this.dgvProductList.Name = "dgvProductList";
            this.dgvProductList.RowHeadersWidth = 51;
            this.dgvProductList.RowTemplate.Height = 24;
            this.dgvProductList.Size = new System.Drawing.Size(358, 478);
            this.dgvProductList.TabIndex = 3;
            this.dgvProductList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductList_CellDoubleClick);
            // 
            // FrmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 560);
            this.Controls.Add(this.pnlProducts);
            this.Controls.Add(this.pnlOrder);
            this.Controls.Add(this.pnlTables);
            this.Name = "FrmOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Bán hàng & Gọi món tại bàn";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmOrder_Load);
            this.pnlTables.ResumeLayout(false);
            this.pnlOrder.ResumeLayout(false);
            this.pnlOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).EndInit();
            this.pnlProducts.ResumeLayout(false);
            this.pnlProducts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTables;
        private System.Windows.Forms.Panel pnlOrder;
        private System.Windows.Forms.Panel pnlProducts;
        private System.Windows.Forms.FlowLayoutPanel flpTableList;
        private System.Windows.Forms.Button btnSwitchTable;
        private System.Windows.Forms.DataGridView dgvProductList;
        private System.Windows.Forms.Label lblSearchProduct;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private System.Windows.Forms.ComboBox cboCategoryFilter;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Button btnRemoveProduct;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.DataGridView dgvOrderDetails;
        private System.Windows.Forms.Button btnPayAndPrint;
        private System.Windows.Forms.Label lblFinalAmount;
    }
}

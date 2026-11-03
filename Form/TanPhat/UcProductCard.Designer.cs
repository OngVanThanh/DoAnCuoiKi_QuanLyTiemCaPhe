namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class UcProductCard
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
            this.pnlCard = new System.Windows.Forms.Panel();
            this.pnlImageArea = new System.Windows.Forms.Panel();
            this.lblCategoryBadge = new System.Windows.Forms.Label();
            this.lblUnitBadge = new System.Windows.Forms.Label();
            this.picProduct = new System.Windows.Forms.PictureBox();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblProductDesc = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.btnQuickAdd = new System.Windows.Forms.Button();
            this.pnlCard.SuspendLayout();
            this.pnlImageArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.btnQuickAdd);
            this.pnlCard.Controls.Add(this.lblPrice);
            this.pnlCard.Controls.Add(this.lblProductDesc);
            this.pnlCard.Controls.Add(this.lblProductName);
            this.pnlCard.Controls.Add(this.pnlImageArea);
            this.pnlCard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard.Location = new System.Drawing.Point(0, 0);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(146, 192);
            this.pnlCard.TabIndex = 0;
            this.pnlCard.Click += new System.EventHandler(this.Card_Click);
            // 
            // pnlImageArea
            // 
            this.pnlImageArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(241)))), ((int)(((byte)(233)))));
            this.pnlImageArea.Controls.Add(this.lblUnitBadge);
            this.pnlImageArea.Controls.Add(this.lblCategoryBadge);
            this.pnlImageArea.Controls.Add(this.picProduct);
            this.pnlImageArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlImageArea.Location = new System.Drawing.Point(0, 0);
            this.pnlImageArea.Name = "pnlImageArea";
            this.pnlImageArea.Size = new System.Drawing.Size(144, 92);
            this.pnlImageArea.TabIndex = 0;
            this.pnlImageArea.Click += new System.EventHandler(this.Card_Click);
            // 
            // lblCategoryBadge
            // 
            this.lblCategoryBadge.AutoSize = true;
            this.lblCategoryBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblCategoryBadge.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryBadge.ForeColor = System.Drawing.Color.White;
            this.lblCategoryBadge.Location = new System.Drawing.Point(6, 6);
            this.lblCategoryBadge.Name = "lblCategoryBadge";
            this.lblCategoryBadge.Padding = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.lblCategoryBadge.Size = new System.Drawing.Size(55, 19);
            this.lblCategoryBadge.TabIndex = 1;
            this.lblCategoryBadge.Text = "Cà phê";
            this.lblCategoryBadge.UseMnemonic = false;
            this.lblCategoryBadge.Click += new System.EventHandler(this.Card_Click);
            // 
            // lblUnitBadge
            // 
            this.lblUnitBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUnitBadge.AutoSize = true;
            this.lblUnitBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(222)))), ((int)(((byte)(210)))));
            this.lblUnitBadge.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnitBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(44)))), ((int)(((byte)(42)))));
            this.lblUnitBadge.Location = new System.Drawing.Point(114, 6);
            this.lblUnitBadge.Name = "lblUnitBadge";
            this.lblUnitBadge.Padding = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.lblUnitBadge.Size = new System.Drawing.Size(24, 19);
            this.lblUnitBadge.TabIndex = 2;
            this.lblUnitBadge.Text = "Ly";
            this.lblUnitBadge.UseMnemonic = false;
            this.lblUnitBadge.Click += new System.EventHandler(this.Card_Click);
            // 
            // picProduct
            // 
            this.picProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picProduct.Location = new System.Drawing.Point(0, 0);
            this.picProduct.Name = "picProduct";
            this.picProduct.Size = new System.Drawing.Size(144, 92);
            this.picProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProduct.TabIndex = 0;
            this.picProduct.TabStop = false;
            this.picProduct.Click += new System.EventHandler(this.Card_Click);
            // 
            // lblProductName
            // 
            this.lblProductName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(28)))), ((int)(((byte)(26)))));
            this.lblProductName.Location = new System.Drawing.Point(6, 96);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(132, 36);
            this.lblProductName.TabIndex = 1;
            this.lblProductName.Text = "Cà phê sữa đá";
            this.lblProductName.UseMnemonic = false;
            this.lblProductName.Click += new System.EventHandler(this.Card_Click);
            // 
            // lblProductDesc
            // 
            this.lblProductDesc.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblProductDesc.Location = new System.Drawing.Point(6, 133);
            this.lblProductDesc.Name = "lblProductDesc";
            this.lblProductDesc.Size = new System.Drawing.Size(132, 16);
            this.lblProductDesc.TabIndex = 2;
            this.lblProductDesc.Text = "Đậm vị · Tại quán";
            this.lblProductDesc.UseMnemonic = false;
            this.lblProductDesc.Click += new System.EventHandler(this.Card_Click);
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(111)))), ((int)(((byte)(67)))));
            this.lblPrice.Location = new System.Drawing.Point(6, 158);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(68, 21);
            this.lblPrice.TabIndex = 3;
            this.lblPrice.Text = "29,000 đ";
            this.lblPrice.Click += new System.EventHandler(this.Card_Click);
            // 
            // btnQuickAdd
            // 
            this.btnQuickAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(111)))), ((int)(((byte)(67)))));
            this.btnQuickAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuickAdd.FlatAppearance.BorderSize = 0;
            this.btnQuickAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickAdd.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuickAdd.ForeColor = System.Drawing.Color.White;
            this.btnQuickAdd.Location = new System.Drawing.Point(80, 154);
            this.btnQuickAdd.Name = "btnQuickAdd";
            this.btnQuickAdd.Size = new System.Drawing.Size(58, 28);
            this.btnQuickAdd.TabIndex = 4;
            this.btnQuickAdd.Text = "+ Thêm";
            this.btnQuickAdd.UseVisualStyleBackColor = false;
            this.btnQuickAdd.Click += new System.EventHandler(this.btnQuickAdd_Click);
            // 
            // UcProductCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnlCard);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "UcProductCard";
            this.Size = new System.Drawing.Size(146, 192);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.pnlImageArea.ResumeLayout(false);
            this.pnlImageArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Panel pnlImageArea;
        private System.Windows.Forms.Label lblCategoryBadge;
        private System.Windows.Forms.Label lblUnitBadge;
        private System.Windows.Forms.PictureBox picProduct;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblProductDesc;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Button btnQuickAdd;
    }
}

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class UcReservedTableCard
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlCard = new System.Windows.Forms.Panel();
            this.btnCancelTable = new System.Windows.Forms.Button();
            this.lblTimeRange = new System.Windows.Forms.Label();
            this.lblAreaName = new System.Windows.Forms.Label();
            this.lblStatusBadge = new System.Windows.Forms.Label();
            this.lblTableName = new System.Windows.Forms.Label();
            this.pnlCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCard.Controls.Add(this.btnCancelTable);
            this.pnlCard.Controls.Add(this.lblTimeRange);
            this.pnlCard.Controls.Add(this.lblAreaName);
            this.pnlCard.Controls.Add(this.lblStatusBadge);
            this.pnlCard.Controls.Add(this.lblTableName);
            this.pnlCard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCard.Location = new System.Drawing.Point(0, 0);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Padding = new System.Windows.Forms.Padding(8);
            this.pnlCard.Size = new System.Drawing.Size(268, 78);
            this.pnlCard.TabIndex = 0;
            this.pnlCard.Click += new System.EventHandler(this.Card_Click);
            // 
            // lblTableName
            // 
            this.lblTableName.AutoEllipsis = true;
            this.lblTableName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTableName.ForeColor = System.Drawing.Color.FromArgb(74, 44, 42);
            this.lblTableName.Location = new System.Drawing.Point(8, 6);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(172, 22);
            this.lblTableName.TabIndex = 0;
            this.lblTableName.Text = "Bàn 01 - Khách đặt";
            this.lblTableName.Click += new System.EventHandler(this.Card_Click);
            // 
            // lblStatusBadge
            // 
            this.lblStatusBadge.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblStatusBadge.ForeColor = System.Drawing.Color.White;
            this.lblStatusBadge.Location = new System.Drawing.Point(184, 6);
            this.lblStatusBadge.Name = "lblStatusBadge";
            this.lblStatusBadge.Size = new System.Drawing.Size(74, 20);
            this.lblStatusBadge.TabIndex = 1;
            this.lblStatusBadge.Text = "Đặt trước";
            this.lblStatusBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatusBadge.Click += new System.EventHandler(this.Card_Click);
            // 
            // lblAreaName
            // 
            this.lblAreaName.AutoEllipsis = true;
            this.lblAreaName.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular);
            this.lblAreaName.ForeColor = System.Drawing.Color.DimGray;
            this.lblAreaName.Location = new System.Drawing.Point(8, 29);
            this.lblAreaName.Name = "lblAreaName";
            this.lblAreaName.Size = new System.Drawing.Size(190, 19);
            this.lblAreaName.TabIndex = 2;
            this.lblAreaName.Text = "Khu vực: Tầng 1";
            this.lblAreaName.Click += new System.EventHandler(this.Card_Click);
            // 
            // lblTimeRange
            // 
            this.lblTimeRange.AutoEllipsis = true;
            this.lblTimeRange.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic);
            this.lblTimeRange.ForeColor = System.Drawing.Color.FromArgb(180, 70, 20);
            this.lblTimeRange.Location = new System.Drawing.Point(8, 49);
            this.lblTimeRange.Name = "lblTimeRange";
            this.lblTimeRange.Size = new System.Drawing.Size(195, 20);
            this.lblTimeRange.TabIndex = 3;
            this.lblTimeRange.Text = "⏰ 14:00 - 16:00 (Còn 120p)";
            this.lblTimeRange.Click += new System.EventHandler(this.Card_Click);
            // 
            // btnCancelTable
            // 
            this.btnCancelTable.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnCancelTable.FlatAppearance.BorderSize = 0;
            this.btnCancelTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelTable.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold);
            this.btnCancelTable.ForeColor = System.Drawing.Color.White;
            this.btnCancelTable.Location = new System.Drawing.Point(206, 44);
            this.btnCancelTable.Name = "btnCancelTable";
            this.btnCancelTable.Size = new System.Drawing.Size(52, 25);
            this.btnCancelTable.TabIndex = 4;
            this.btnCancelTable.Text = "Hủy";
            this.btnCancelTable.UseVisualStyleBackColor = false;
            this.btnCancelTable.Click += new System.EventHandler(this.btnCancelTable_Click);
            // 
            // UcReservedTableCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCard);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.Name = "UcReservedTableCard";
            this.Size = new System.Drawing.Size(268, 78);
            this.pnlCard.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.Label lblStatusBadge;
        private System.Windows.Forms.Label lblAreaName;
        private System.Windows.Forms.Label lblTimeRange;
        private System.Windows.Forms.Button btnCancelTable;
    }
}
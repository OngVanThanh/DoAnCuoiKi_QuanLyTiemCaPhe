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
            this.printPreviewControl = new System.Windows.Forms.PrintPreviewControl();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblPrinter = new System.Windows.Forms.Label();
            this.cboPrinters = new System.Windows.Forms.ComboBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.printDocumentInvoice = new System.Drawing.Printing.PrintDocument();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // printPreviewControl
            // 
            this.printPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printPreviewControl.Location = new System.Drawing.Point(0, 0);
            this.printPreviewControl.Name = "printPreviewControl";
            this.printPreviewControl.Size = new System.Drawing.Size(800, 520);
            this.printPreviewControl.TabIndex = 0;
            this.printPreviewControl.Zoom = 1D;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlBottom.Controls.Add(this.lblPrinter);
            this.pnlBottom.Controls.Add(this.btnPrint);
            this.pnlBottom.Controls.Add(this.cboPrinters);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 520);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(800, 60);
            this.pnlBottom.TabIndex = 1;
            // 
            // lblPrinter
            // 
            this.lblPrinter.AutoSize = true;
            this.lblPrinter.Location = new System.Drawing.Point(24, 21);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(92, 16);
            this.lblPrinter.TabIndex = 2;
            this.lblPrinter.Text = "Chọn máy in:";
            // 
            // cboPrinters
            // 
            this.cboPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrinters.FormattingEnabled = true;
            this.cboPrinters.Location = new System.Drawing.Point(122, 17);
            this.cboPrinters.Name = "cboPrinters";
            this.cboPrinters.Size = new System.Drawing.Size(340, 24);
            this.cboPrinters.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(490, 13);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(220, 34);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Thực hiện in";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
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
            this.ClientSize = new System.Drawing.Size(800, 580);
            this.Controls.Add(this.printPreviewControl);
            this.Controls.Add(this.pnlBottom);
            this.Name = "FrmPrintReview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xem trước & In hóa đơn (K80)";
            this.Load += new System.EventHandler(this.FrmPrintReview_Load);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PrintPreviewControl printPreviewControl;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblPrinter;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ComboBox cboPrinters;
        private System.Drawing.Printing.PrintDocument printDocumentInvoice;
    }
}

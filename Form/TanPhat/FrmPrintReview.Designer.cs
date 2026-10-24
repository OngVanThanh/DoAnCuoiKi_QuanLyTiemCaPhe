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
            this.printPreviewControl.Size = new System.Drawing.Size(800, 450);
            this.printPreviewControl.TabIndex = 0;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnPrint);
            this.pnlBottom.Controls.Add(this.cboPrinters);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 400);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(800, 50);
            this.pnlBottom.TabIndex = 1;
            // 
            // cboPrinters
            // 
            this.cboPrinters.FormattingEnabled = true;
            this.cboPrinters.Location = new System.Drawing.Point(69, 14);
            this.cboPrinters.Name = "cboPrinters";
            this.cboPrinters.Size = new System.Drawing.Size(121, 24);
            this.cboPrinters.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(377, 14);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(213, 23);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Thực hiện in";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // FrmPrintReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.printPreviewControl);
            this.Name = "FrmPrintReview";
            this.Text = "FrmPrintReview";
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PrintPreviewControl printPreviewControl;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ComboBox cboPrinters;
        private System.Drawing.Printing.PrintDocument printDocumentInvoice;
    }
}
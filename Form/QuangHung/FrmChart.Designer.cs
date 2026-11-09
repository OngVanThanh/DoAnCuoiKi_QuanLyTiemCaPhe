namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    partial class FrmChart
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartRevenue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnFilterChart = new System.Windows.Forms.Button();
            this.ChartProducts = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.rdoYear = new System.Windows.Forms.RadioButton();
            this.rdoMonth = new System.Windows.Forms.RadioButton();
            this.rdoWeek = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // chartRevenue
            // 
            chartArea5.Name = "ChartArea1";
            this.chartRevenue.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chartRevenue.Legends.Add(legend5);
            this.chartRevenue.Location = new System.Drawing.Point(29, 83);
            this.chartRevenue.Name = "chartRevenue";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chartRevenue.Series.Add(series5);
            this.chartRevenue.Size = new System.Drawing.Size(300, 300);
            this.chartRevenue.TabIndex = 2;
            this.chartRevenue.Text = "chart1";
            // 
            // btnFilterChart
            // 
            this.btnFilterChart.Location = new System.Drawing.Point(254, 26);
            this.btnFilterChart.Name = "btnFilterChart";
            this.btnFilterChart.Size = new System.Drawing.Size(75, 23);
            this.btnFilterChart.TabIndex = 3;
            this.btnFilterChart.Text = "Filter";
            this.btnFilterChart.UseVisualStyleBackColor = true;
            this.btnFilterChart.Click += new System.EventHandler(this.btnFilterChart_Click);
            // 
            // ChartProducts
            // 
            chartArea6.Name = "ChartArea1";
            this.ChartProducts.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.ChartProducts.Legends.Add(legend6);
            this.ChartProducts.Location = new System.Drawing.Point(351, 83);
            this.ChartProducts.Name = "ChartProducts";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.ChartProducts.Series.Add(series6);
            this.ChartProducts.Size = new System.Drawing.Size(300, 300);
            this.ChartProducts.TabIndex = 4;
            this.ChartProducts.Text = "chart2";
            // 
            // rdoYear
            // 
            this.rdoYear.AutoSize = true;
            this.rdoYear.Location = new System.Drawing.Point(29, 29);
            this.rdoYear.Name = "rdoYear";
            this.rdoYear.Size = new System.Drawing.Size(47, 17);
            this.rdoYear.TabIndex = 5;
            this.rdoYear.TabStop = true;
            this.rdoYear.Text = "Year";
            this.rdoYear.UseVisualStyleBackColor = true;
            this.rdoYear.CheckedChanged += new System.EventHandler(this.rdoYear_CheckedChanged);
            // 
            // rdoMonth
            // 
            this.rdoMonth.AutoSize = true;
            this.rdoMonth.Location = new System.Drawing.Point(99, 29);
            this.rdoMonth.Name = "rdoMonth";
            this.rdoMonth.Size = new System.Drawing.Size(55, 17);
            this.rdoMonth.TabIndex = 6;
            this.rdoMonth.TabStop = true;
            this.rdoMonth.Text = "Month";
            this.rdoMonth.UseVisualStyleBackColor = true;
            this.rdoMonth.CheckedChanged += new System.EventHandler(this.rdoMonth_CheckedChanged);
            // 
            // rdoWeek
            // 
            this.rdoWeek.AutoSize = true;
            this.rdoWeek.Location = new System.Drawing.Point(178, 29);
            this.rdoWeek.Name = "rdoWeek";
            this.rdoWeek.Size = new System.Drawing.Size(54, 17);
            this.rdoWeek.TabIndex = 7;
            this.rdoWeek.TabStop = true;
            this.rdoWeek.Text = "Week";
            this.rdoWeek.UseVisualStyleBackColor = true;
            this.rdoWeek.CheckedChanged += new System.EventHandler(this.rdoWeek_CheckedChanged);
            // 
            // FrmChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rdoWeek);
            this.Controls.Add(this.rdoMonth);
            this.Controls.Add(this.rdoYear);
            this.Controls.Add(this.ChartProducts);
            this.Controls.Add(this.btnFilterChart);
            this.Controls.Add(this.chartRevenue);
            this.Name = "FrmChart";
            this.Text = "FrmChart";
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenue;
        private System.Windows.Forms.Button btnFilterChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartProducts;
        private System.Windows.Forms.RadioButton rdoYear;
        private System.Windows.Forms.RadioButton rdoMonth;
        private System.Windows.Forms.RadioButton rdoWeek;
    }
}
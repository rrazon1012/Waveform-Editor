namespace WindowsFormsApp2
{
    partial class Frequency
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
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.Location = new System.Drawing.Point(151, 12);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(599, 363);
            this.formsPlot1.TabIndex = 4;
            this.formsPlot1.Load += new System.EventHandler(this.FormsPlot1_Load);
            double[] xs = new double[] { 1, 2, 3, 4, 5 };
            double[] ys = new double[] { 1, 4, 9, 16, 25 };
            formsPlot1.plt.PlotScatter(xs, ys);
            formsPlot1.Render();
            // 
            // Frequency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 387);
            this.Controls.Add(this.formsPlot1);
            this.Name = "Frequency";
            this.Text = "Frequency";
            this.Load += new System.EventHandler(this.Frequency_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private ScottPlot.FormsPlot formsPlot1;
    }
}
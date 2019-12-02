using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp2
{
    public partial class Time : Form
    {
        int selStart, selEnd;
        int ymax;
        double[] samples;
        double[] freq;
        Boolean filter;
        DFT dft;
        public Time(double[] sample)
        {
            samples = sample;
            InitializeComponent();
        }

        private void Time_Load(object sender, EventArgs e) {
            dft = new DFT();
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            freq = dft.paralleldft(samples);
            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            chart1.Series.Add("Amplitude");
            chart1.Series["Amplitude"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["Amplitude"].ChartArea = "ChartArea1";
            chart1.Series["Amplitude"].Color = Color.Red;
            chart1.Series["Amplitude"].Points.DataBindY(freq);
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = false;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = Enabled;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = Enabled;
            chart1.SelectionRangeChanging += chart1_SelectionRangeChanging;
            chart1.SelectionRangeChanged += chart1_SelectionRangeChanged;
        }

        private void chart1_SelectionRangeChanged(object sender, CursorEventArgs e) {
            Console.WriteLine(selStart);
            Console.WriteLine(selEnd);
            Console.WriteLine(samples.Length / 2);
            int selStartcop = samples.Length - selStart,selEndcop = samples.Length - selEnd;
            Console.WriteLine(selStartcop);
            Console.WriteLine(selEndcop);
            Console.WriteLine(samples.Length);
            for (int i = 0; i < freq.Length;i++)
            {
                if (i < selEnd && i > selStart || i < selStartcop && i > selEndcop)
                {
                   
                } else {
                    freq[i] = 0;
                }
            }
            chart1.Series["Amplitude"].Points.DataBindY(freq);
            samples = dft.idft(freq);
        }

        private void chart1_SelectionRangeChanging(object sender, CursorEventArgs e)
        {
            selStart = (int) e.NewSelectionStart;
            selEnd = (int) e.NewSelectionEnd;
        }

        public double[] getSample() {
            return samples;
        }
    }
}

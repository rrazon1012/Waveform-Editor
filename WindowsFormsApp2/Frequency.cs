using System;
using System.Numerics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp2
{

    public partial class Frequency : Form
    {

        byte[] buffer;
        double[] samples;
        double ymax;
        Point mdown = Point.Empty;
        bool select;
        bool paste;
        int selStart;
        int selEnd;

        int ChunkId;
        int filesize;
        int rifftype;
        int fmtID;
        int fmtsize;
        int fmtcode;
        int channels;
        int samplerate;
        int fmtAvgBPS;
        int fmtblockalign;
        int bitdepth;
        int fmtextrasize;
        int DataID;
        int DataSize;


        List<DataPoint> selectedPoints = null;
        double[] copyArray;
        public Frequency()
        {
            try
            {
                List<double> samplez = new List<double>();
                select = false;
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Select .Wav file";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    buffer = File.ReadAllBytes(ofd.FileName);
                }
                System.IO.MemoryStream memstream = new System.IO.MemoryStream(buffer);
                System.IO.BinaryReader binreader = new System.IO.BinaryReader(memstream);

                ChunkId = binreader.ReadInt32();
                filesize = binreader.ReadInt32();
                rifftype = binreader.ReadInt32();
                fmtID = binreader.ReadInt32();
                fmtsize = binreader.ReadInt32();
                fmtcode = binreader.ReadInt16();
                channels = binreader.ReadInt16();
                samplerate = binreader.ReadInt32();
                fmtAvgBPS = binreader.ReadInt32();
                fmtblockalign = binreader.ReadInt16();
                bitdepth = binreader.ReadInt16();

                if (fmtsize == 18)
                {
                    fmtextrasize = binreader.ReadInt16();
                    binreader.ReadBytes(fmtextrasize);
                }

                DataID = binreader.ReadInt32();
                DataSize = binreader.ReadInt32();

                for (int i = 0; i < (DataSize - 1) / 2; i++)
                {
                    samplez.Add(Convert.ToDouble(binreader.ReadInt16()));
                }
                samples = samplez.ToArray();
                //samples = sampleArray;
                select = false;
                InitializeComponent();
            }
            catch (System.OutOfMemoryException e)
            {
                Console.WriteLine(e);
            }
        }

        private void Frequency_Load(object sender, EventArgs e)
        {
            chart1.Series.Add("wave");
            chart1.Series["wave"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series["wave"].ChartArea = "ChartArea1";
            chart1.Series["wave"].Color = Color.Red;
            chart1.Series["wave"].Points.DataBindY(samples);
            ymax = chart1.ChartAreas[0].AxisY.Maximum;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.MouseWheel += chart1_MouseWheel;
            chart1.MouseDown += chart1_MouseDown;
            chart1.MouseMove += chart1_MouseMove;
            chart1.MouseUp += chart1_MouseUp;
        }

        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            var chart = (Chart)sender;
            var xAxis = chart.ChartAreas[0].AxisX;
            var yAxis = chart.ChartAreas[0].AxisY;

            try
            {
                if (e.Delta < 0) // Scrolled down.
                {
                    xAxis.ScaleView.ZoomReset();
                    yAxis.ScaleView.ZoomReset();
                }
                else if (e.Delta > 0) // Scrolled up.
                {
                    var xMin = xAxis.ScaleView.ViewMinimum;
                    var xMax = xAxis.ScaleView.ViewMaximum;
                    var yMin = yAxis.ScaleView.ViewMinimum;
                    var yMax = yAxis.ScaleView.ViewMaximum;

                    var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                    xAxis.ScaleView.Zoom(posXStart, posXFinish);
                    yAxis.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }
        }


        private void FormsPlot1_Load(object sender, EventArgs e)
        {

            /*for (int n = 1; n < 10000; n++)
            {
                x1s[n] = n;
                y1s[n] = rnd.Next(-50, 50);
            }
            //double[] xs = {0,1,2,3,4};
            //double[] ys = new double[] { 1, 4, 9, 16, 25 };
            formsPlot1.plt.PlotScatter(x1s, y1s);
            formsPlot1.Render(); */


        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            if (select == true)
            {
                mdown = e.Location;
                selectedPoints = new List<DataPoint>();
                selStart = (int)chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
            }
            if (paste == true && copyArray != null)
            {
                int pasteX = (int)chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                DataSize += copyArray.Length;
                //size of the new samples array
                int newsize = samples.Length + copyArray.Length;
                double[] tempArray = new double[newsize];

                //size of the right side of click
                int shiftsize = samples.Length - pasteX;
                double[] shiftArray = new double[shiftsize];

                for (int i = pasteX, j = 0; i < samples.Length; i++, j++) //put everything after paste x in a temporary array
                {
                    shiftArray[j] = samples[i];
                }

                for (int i = 0; i < pasteX; i++)
                {
                    tempArray[i] = samples[i];
                }
                for (int i = pasteX, j = 0; i < pasteX + copyArray.Length; i++, j++)
                {
                    tempArray[i] = copyArray[j];
                }
                //Console.WriteLine(pasteX + copyArray.Length);
                //Console.WriteLine(newsize);
                for (int i = pasteX + copyArray.Length, j = pasteX; i < newsize; i++, j++)
                {
                    tempArray[i] = samples[j];
                }

                samples = tempArray;
                chart1.Series["wave"].Points.DataBindY(samples);
                paste = false;
            }
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (select == true)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    chart1.Refresh();
                    using (Graphics g = chart1.CreateGraphics())
                        g.DrawRectangle(Pens.Red, GetRectangle(mdown, e.Location));
                }
            }
        }

        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            if (select == true)
            {
                Axis ax = chart1.ChartAreas[0].AxisX;
                Axis ay = chart1.ChartAreas[0].AxisY;
                Rectangle rect = GetRectangle(mdown, e.Location);
                foreach (DataPoint dp in chart1.Series["wave"].Points)
                {
                    int x = (int)ax.ValueToPixelPosition(dp.XValue);
                    int y = (int)ay.ValueToPixelPosition(dp.YValues[0]);
                    if (rect.Contains(new Point(x, y))) selectedPoints.Add(dp);
                }

                foreach (DataPoint dp in chart1.Series["wave"].Points)
                {
                    int n = (int)dp.XValue;
                    //Console.WriteLine(n);
                    //chart1.Series["wave"].Points[n].Color = Color.Red;
                    dp.Color = selectedPoints.Contains(dp) ? Color.Black : Color.Red;
                }
                selEnd = (int)chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                Console.WriteLine("selStart" + selStart);
                Console.WriteLine("SelEnd " + selEnd);
                select = false;
            }
        }

        public Rectangle GetRectangle(Point p1, Point p2)
        {
            return new Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
        }

        //Select
        private void Button1_Click(object sender, EventArgs e)
        {
            if (!select)
            {
                Console.WriteLine("Selecting");
                select = true;
            }
            else
            {
                Console.WriteLine("Stopped Selecting");
                select = false;
            }
        }

        //Cut
        private void Button2_Click(object sender, EventArgs e)
        {
            if (selectedPoints != null)
            {

                //foreach (DataPoint p in selectedPoints) {
                //    chart1.Series["wave"].Points.Remove(p);
                //}
                int delsize = selEnd - selStart;
                DataSize -= delsize;
                int newsize = samples.Length - delsize;
                //for (int i = 0; i < samples.Length; i++)
                //{
                //    if (i > selStart && i < selEnd)
                //    {
                //        samples[i] = double.NaN;
                //    }
                //}

                double[] tempSamples = new double[samples.Length];
                int tempIndex = 0, currIndex = 0;
                for (int i = 0; i < selStart; i++)
                {
                    tempSamples[i] = samples[i];
                }
                for (int i = selStart,j = selEnd; j < tempSamples.Length;j++,i++)
                {
                    tempSamples[i] = samples[j];
                }
                
                //while (currIndex < samples.Length)
                //{
                //    if (!(double.IsNaN(samples[currIndex])))
                //    {
                //        tempSamples[tempIndex] = samples[currIndex];
                //        tempIndex++;
                //        currIndex++;
                //    }
                //    else
                //    {
                //        currIndex++;
                //    }

                //}
                samples = tempSamples;

                chart1.Series["wave"].Points.DataBindY(samples);
                selectedPoints = null;
            }
        }

        //copy
        private void Button3_Click(object sender, EventArgs e)
        {
            if (selectedPoints != null)
            {

                int copysize = selEnd - selStart;
                copyArray = new double[copysize];
                for (int i = selStart, j = 0; i < selEnd; i++, j++)
                {
                    copyArray[j] = samples[i];
                    //Console.WriteLine(samples[i]);
                }
                Console.WriteLine("selected samples copied");
                selectedPoints = null;
            }
        }
        //paste
        private void Button4_Click(object sender, EventArgs e)
        {
            if (!paste)
            {
                paste = true;
            }
            else
            {
                paste = false;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save Wav file";
            sfd.Filter = "Wav File | *.wav";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                List<double> samplez = samples.ToList();
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(Convert.ToInt32(ChunkId));
                bw.Write(Convert.ToInt32(filesize));
                bw.Write(Convert.ToInt32(rifftype));
                bw.Write(Convert.ToInt32(fmtID));
                bw.Write(Convert.ToInt32(fmtsize));
                bw.Write(Convert.ToInt16(fmtcode));
                bw.Write(Convert.ToInt16(channels));
                bw.Write(Convert.ToInt32(samplerate));
                bw.Write(Convert.ToInt32(fmtAvgBPS));
                bw.Write(Convert.ToInt16(fmtblockalign));
                bw.Write(Convert.ToInt16(bitdepth));
                if (fmtsize == 18)
                {
                    bw.Write(Convert.ToInt16(fmtextrasize));
                }
                bw.Write(Convert.ToInt32(DataID));
                bw.Write(Convert.ToInt32(DataSize));

                //samples = new double[samplestotalcount];
                for (int i = 0; i < (DataSize - 1) / 2; i++)
                {
                    bw.Write(Convert.ToInt16(samplez[i]));
                }
                bw.Close();
                fs.Close();
            }
        }

        public double[] getSamples() {
            return samples;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {

        [DllImport("winmm.dll")]
        private static extern long mciSendString(
            string command, StringBuilder retstring, int ReturnLength, IntPtr callback);        
        double[] samples;
        Frequency frequency;
        Time time;
        public Form1()
        {
            InitializeComponent();
            mciSendString("open new Type waveaudio alias recsound", null, 0, IntPtr.Zero);
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // this.close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void OpenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //List<double> samplez = new List<double>();
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Title = "Select .Wav file";
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    buffer = File.ReadAllBytes(ofd.FileName);
            //}
            //System.IO.MemoryStream memstream = new System.IO.MemoryStream(buffer);
            //System.IO.BinaryReader binreader = new System.IO.BinaryReader(memstream);

            //ChunkId = binreader.ReadInt32();
            //filesize = binreader.ReadInt32();
            //rifftype = binreader.ReadInt32();
            //fmtID = binreader.ReadInt32();
            //fmtsize = binreader.ReadInt32();
            //fmtcode = binreader.ReadInt16();
            //channels = binreader.ReadInt16();
            //samplerate = binreader.ReadInt32();
            //fmtAvgBPS = binreader.ReadInt32();
            //fmtblockalign = binreader.ReadInt16();
            //bitdepth = binreader.ReadInt16();

            //if (fmtsize == 18)
            //{
            //    fmtextrasize = binreader.ReadInt16();
            //    binreader.ReadBytes(fmtextrasize);
            //}

            //DataID = binreader.ReadInt32();
            //DataSize = binreader.ReadInt32();

            //for (int i = 0; i < (DataSize - 1) / 2; i++)
            //{
            //    samplez.Add(Convert.ToDouble(binreader.ReadInt16()));
            //}
            //samples = samplez.ToArray();


            //Frequency frequency = new Frequency();
            //frequency.Show();

            //openFrequency(frequency);
            //Thread a = new Thread(openTime);
            //Thread b = new Thread(openFrequency);
            //a.Start();
            //b.Start();
            openTime();
        }
        String output = "c:\\recorded\\Recorded.wav";
        private void openTime() {
            frequency = new Frequency();
            frequency.Show();
            samples = frequency.getSamples();
        }


        private void applyFilter() {
            samples = time.getSample();
        }
        private void openTime(String path)
        {
            Frequency frequency = new Frequency(path);
            frequency.Show();
            samples = frequency.getSamples();
        }
        private void openFrequency() {
            Time time = new Time(samples);
            time.Show();
        }

        private void SaveToolStripMenuItem1_Click(object sender, EventArgs e)
        {            
            //Frequency frequency = new Frequency(record.sample, record.DataSize);
            //frequency.Show();
            //samples = frequency.getSamples();

            //int rc = WaitForSingleObject(hEvent, 10000); 

            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Title = "Save Wav file";
            //sfd.Filter = "Wav File | *.wav";
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    List<double> samplez = samples.ToList();
            //    FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write);
            //    BinaryWriter bw = new BinaryWriter(fs);
            //    bw.Write(Convert.ToInt32(ChunkId));
            //    bw.Write(Convert.ToInt32(filesize));
            //    bw.Write(Convert.ToInt32(rifftype));
            //    bw.Write(Convert.ToInt32(fmtID));
            //    bw.Write(Convert.ToInt32(fmtsize));
            //    bw.Write(Convert.ToInt16(fmtcode));
            //    bw.Write(Convert.ToInt16(channels));
            //    bw.Write(Convert.ToInt32(samplerate));
            //    bw.Write(Convert.ToInt32(fmtAvgBPS));
            //    bw.Write(Convert.ToInt16(fmtblockalign));
            //    bw.Write(Convert.ToInt16(bitdepth));
            //    if (fmtsize == 18)
            //    {
            //        bw.Write(Convert.ToInt16(fmtextrasize));
            //    }
            //    bw.Write(Convert.ToInt32(DataID));
            //    bw.Write(Convert.ToInt32(DataSize));

            //    //samples = new double[samplestotalcount];
            //    for (int i = 0; i < (DataSize - 1) / 2; i++)
            //    {
            //        bw.Write(Convert.ToInt16(samplez[i]));
            //    }
            //    bw.Close();
            //    fs.Close();
            //}
        }        
        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mciSendString("record recsound", null, 0, IntPtr.Zero);
            DrawString("Recording...");
        }

        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawString("Stopped");
            mciSendString("save recsound " + output, null, 0, IntPtr.Zero);
            mciSendString("close recsound ", null, 0, IntPtr.Zero);
        }

        public void DrawString(String msg)
        {
            System.Drawing.Graphics formGraphics = this.CreateGraphics();
            formGraphics.Clear(Color.White);
            string drawString = msg;
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 16);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            float x = 150.0F;
            float y = 50.0F;
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            formGraphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            drawFont.Dispose();
            drawBrush.Dispose();
            formGraphics.Dispose();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            applyFilter();
            frequency.setSample(samples);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            time = new Time(samples);
            time.Show();
        }

        private void DisplayFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mciSendString("close recsound ", null, 0, IntPtr.Zero);
            openTime(output);
        }

        private void PlaybackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawString("Playing...");
            mciSendString("play recsound notify", null, 0, IntPtr.Zero);
            mciSendString("close recsound ", null, 0, IntPtr.Zero);
        }
    }
}

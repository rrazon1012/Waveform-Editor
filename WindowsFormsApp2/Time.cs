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

namespace WindowsFormsApp2
{
    public partial class Time : Form
    {
        public Time()
        {
            InitializeComponent();
            //double[] samples = {0, 0.707, 1, 0.707, 0, -0.707,-1,-0.707};
            double[] samples = {
                5, 1.1755705, -3.618034, -1.902113, 1.381966,
                0, -1.381966, 1.902113, 3.618034, -1.1755705,
                -5, -1.1755705, 3.618034, 1.902113, -1.381966,
                0, 1.381966, -1.902113, -3.618034, 1.1755705
            };
            double[] samples1 =
            {
                -0.8660254040, .3214394654, .9914448614, .6540312923e-1,
                -.9659258263, -.4422886902, .7933533404, .7518398075, -.5000000000,
                -.9469301295, .1305261922, .9978589232, .2588190451, -.8968727415,
                -.6087614290, .6593458152, .8660254040, -.3214394654, -.9914448614,
                -.6540312923e-1, .9659258263, .4422886902, -.7933533404, -.7518398075,
                .5000000000, .9469301295, -.1305261922, -.9978589232, -.2588190451,
                .8968727415, .6087614290, -.6593458152
            };

            //samples for number 3
            double[] samples2 = {
                1.2071068, -1.1345821, -0.78647806, 0.022538933, -0.82556507,
                0.20710678, 1.9658359, 0.34946204, -1.367536, -0.31855773,
                -0.20710678, -0.82171314, 0.99553499, 1.804552, -0.51269614,
                -1.2071068, -0.0095407399, -0.55851896, -0.45955496, 1.658189
            };

            double[] samples3 = {
                2, 4.209064, -1.677599, 1.2399199, 1.1453218,
                -4.4142136, -0.13875728, -1.0483257, -4.4712035, 1.51907
            };

            double[] real = {
                0, 0, 0, 48, 0, 0, 0, 32, 0, 0, 0, 0, 0, 80, 0, 0, 0,
                0, 0, 80, 0, 0, 0, 0, 0, 32, 0, 0, 0,48, 0, 0
            };

            double[] imag = {
                0, .24e-8, -.46e-7, .40e-7, .37e-7, .81e-7, .51e-7,
                -32.00000000, .1e-8, .43e-7, -.35e-7, .31e-7, .52e-7,
                -.389e-6, -.68e-7, .667e-7, 0, .3756e-6, -.27e-7, -.529e-6,
                .45e-7, .166e-6, .111e-6, .193e-6, -.1e-8, 31.99999988,
                278e-6, .192e-6, .117e-6, -.376e-6, -.64e-7, .2787e-6
            };
           


            double[] freq = DFT.dft(samples);
            

            for (int i = 0; i < samples.Length; i++)
            {
                chart1.Series["Amplitude"].Points.AddXY(i,freq[i]);
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
        }


        private void Time_Load(object sender, EventArgs e)
        {
            
        }

        private void HScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }
    }
}

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

namespace WindowsFormsApp2
{
    public partial class Frequency : Form
    {
        public Frequency()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen pen = new Pen(Brushes.Black, 2.0F);

            float x1 = 0;
            float y1 = 0;

            float y2 = 0;

            float yEx = 300;
            float eF = 50;


            for (float x = 0; x < Width; x += 0.005F)
            {
                y2 = (float)Complex.Pow(Math.Sin(x), x).Real;

                g.DrawLine(pen, x1 * eF, y1 * eF + yEx, x * eF, y2 * eF + yEx);
                x1 = x;
                y1 = y2;
            }
        }

        private void Frequency_Load(object sender, EventArgs e)
        {

        }
    }
}

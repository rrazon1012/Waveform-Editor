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
        Color BG = Color.Honeydew;
        Color BG2 = Color.DimGray;
        public Frequency()
        {
            InitializeComponent();         
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (Graphics g = panel1.CreateGraphics())
            {
                Pen pen = new Pen(Brushes.White, 2.0F);
                float x1 = 0;
                float y1 = 0;

                float y2 = 0;

                float initY = panel1.Height/2;
                float eF = 20;

                for (float x = 0; x < Width; x += 0.005F)
                {
                    y2 = 2*(float)Complex.Pow(
                        Math.Sin(2*x), x).Real; // Sine wave
                    g.DrawLine(pen, x1 * eF, y1 * eF + initY, x * eF, y2 * eF + initY);
                    x1 = x;
                    y1 = y2;
                }
            }
        }

        private void Frequency_Load(object sender, EventArgs e)
        {

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = BG2;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            using (Graphics g = panel1.CreateGraphics())
            {
                g.Clear(BG2);
            }

        }
    }
}

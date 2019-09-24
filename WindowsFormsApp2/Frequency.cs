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
        int toTest = 1;
        int offsetX = 0;
        int size = 1;
        bool remember = false;
        bool hasLine = false;
        Color BG = Color.Honeydew;
        Color BG2 = Color.DimGray;
        public Frequency()
        {
            InitializeComponent();
            this.MouseWheel += Zoom;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            hasLine = true;
            draw();
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
                toTest = remember ? toTest : 1;
            }
            hasLine = false;
        }
        private void Zoom(object sender, MouseEventArgs e)
        {
            Boolean changed = true;
            if (hasLine)
            {
                if (e.Delta > 0)
                {
                    toTest += 1;
                }
                else
                {
                    if (toTest > 1)
                    {
                        toTest -= 1;
                    }
                    else
                    {
                        changed = false;
                    }
                }
                if (changed == true)
                {
                    draw();
                }
            }            
        }
        private void draw()
        {
            using (Graphics g = panel1.CreateGraphics())
            {
                g.Clear(BG2);
                Pen pen = new Pen(Brushes.White, 2.0F);
                float x1 = 0;
                float y1 = 0;

                float y2 = 0;
                float initY = panel1.Height / 2;
                float eF = 30;

                for (float x = 0; x < Width; x += 0.01F)
                {
                    y2 = toTest * (float)Math.Sin((x+offsetX)/toTest); // Sine wave
                    g.DrawLine(pen, x1 * eF, y1 * eF + initY, x * eF, y2 * eF + initY);
                    x1 = x;
                    y1 = y2;
                }
            }        
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            remember = !remember;
        }

        private void HScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            
            if (e.Type == ScrollEventType.SmallDecrement)
            {
                offsetX -= size;
            }
            else if (e.Type == ScrollEventType.SmallIncrement)
            {
                  offsetX += size;
            }
            else
            {
                offsetX = e.NewValue;
            }
            draw();
        }
    }
}

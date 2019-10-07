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
        int width = 640;
        int height = 480;
        Bitmap b = new Bitmap(640,480);
        int[] points;
        int numpoints = 5000;
        public Frequency()
        {
            InitializeComponent();
            this.MouseWheel += Zoom;
            points = new int[50000];
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
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            using (Graphics g = myPanel1.CreateGraphics())
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
            SolidBrush blueBrush = new SolidBrush(Color.Blue);
            using (Graphics g = myPanel1.CreateGraphics()) {
                g.FillRectangle(blueBrush, 0,0, 2, 2);
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

        private void MyPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

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
        double size = 1;
        int[] samples = new int[1000];
        Color BG = Color.Honeydew;
        public Frequency()
        {
            InitializeComponent();
            Random rnd = new Random();
            for(int i = 0; i < samples.Length; i++)
            {
                samples[i] = rnd.Next(40);
            }
        }

        private void Frequency_Load(object sender, EventArgs e)
        {
            
        }

        private void FormsPlot1_Load(object sender, EventArgs e)
        {

        }        
    }
}

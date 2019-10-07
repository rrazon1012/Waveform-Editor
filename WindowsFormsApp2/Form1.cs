using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

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
            Time time = new Time();
                time.Show();
            //Frequency frequency = new Frequency();
                //frequency.Show();
        }
    }

    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
    }
}

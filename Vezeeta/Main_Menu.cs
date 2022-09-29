using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vezeeta
{
    public partial class Main_Menu : Form
    {
        public Main_Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Disconnected_Form form1 = new Disconnected_Form();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Connected_Form form2 = new Connected_Form();
            form2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Crystal_Report1 form3 = new Crystal_Report1();
            form3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Crystal_Report2 form4 = new Crystal_Report2();
            form4.Show();
        }
    }
}

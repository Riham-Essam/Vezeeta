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
    public partial class Crystal_Report2 : Form
    {
        CrystalReport2 CR2;
        public Crystal_Report2()
        {
            InitializeComponent();
        }

        private void Crystal_Report2_Load(object sender, EventArgs e)
        {
            CR2 = new CrystalReport2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = CR2;
        }
    }
}

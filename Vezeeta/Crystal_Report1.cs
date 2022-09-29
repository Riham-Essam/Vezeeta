using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Shared;
namespace Vezeeta
{
    public partial class Crystal_Report1 : Form
    {
        CrystalReport1 CR1;
        public Crystal_Report1()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            
        }

        private void Crystal_Report1_Load(object sender, EventArgs e)
        {
            CR1 = new CrystalReport1();
            foreach (ParameterDiscreteValue k in CR1.ParameterFields[0].DefaultValues)
                comboBox1.Items.Add(k.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CR1.SetParameterValue(0, comboBox1.Text);
            crystalReportViewer1.ReportSource = CR1;
        }
    }
}

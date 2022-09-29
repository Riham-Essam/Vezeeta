using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Vezeeta
{
    public partial class Disconnected_Form : Form
    {
        OracleDataAdapter myAdapter;
        OracleCommandBuilder myBuilder;
        DataSet myDataSet;
        public Disconnected_Form()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source = orcl; User Id = scott; Password = tiger;";

            string commandString = "select * from DRUGS where NAME = :name";
            myAdapter = new OracleDataAdapter(commandString, connectionString);

            myAdapter.SelectCommand.Parameters.Add("name", textBox1.Text);

            myDataSet = new DataSet();

            myAdapter.Fill(myDataSet);
            dataGridView1.DataSource = myDataSet.Tables[0];
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
             myBuilder = new OracleCommandBuilder(myAdapter);
             myAdapter.Update(myDataSet.Tables[0]);
            MessageBox.Show("Saved");

        }
    }
}

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
    public partial class Connected_Form : Form
    {
        string ordb = "Data source=orcl; User Id=scott; Password=tiger;";
        OracleConnection conn;
        public Connected_Form()
        {
            InitializeComponent();
        }

        private void Connected_Form_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();

            //insert specializes from db into combobox
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"select specialize
                                from doctor";
            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            dr.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //show data of selected choice from combobox
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"select name, area, fees, id, age
                                from doctor
                                where specialize=:specialize";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("specialize", comboBox1.SelectedItem.ToString());

            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox1.Text = dr[0].ToString();
                textBox2.Text = dr[1].ToString();
                textBox3.Text = dr[2].ToString();
                textBox4.Text = dr[3].ToString();
                textBox16.Text = dr[4].ToString();
            }
            dr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //insert appointments from db into combobox
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "AvailableAppointments";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("id", textBox5.Text);
            cmd.Parameters.Add("appointments", OracleDbType.RefCursor, ParameterDirection.Output);

            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0]);
            }
            dr.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //show data of selected choice from combobox
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"select start_time, end_time
                                from appointment
                                where reservation_date=:RD";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("RD", comboBox2.SelectedItem.ToString());

            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox6.Text = dr[0].ToString();
                textBox7.Text = dr[1].ToString();
            }
            dr.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //select new patient id
            int maxid, newid;
            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "NewPatientID";
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Output);

            cmd1.ExecuteNonQuery();
            try
            {
                maxid = Convert.ToInt32(cmd1.Parameters["id"].Value.ToString());
                newid = maxid + 1;
            }
            catch
            {
                newid = 1;
            }

            //select new patient number
            int maxnum, newnum;
            OracleCommand cmd4 = new OracleCommand();
            cmd4.Connection = conn;
            cmd4.CommandText = "NewPatientNumber";
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.Parameters.Add("num", OracleDbType.Int32, ParameterDirection.Output);

            cmd4.ExecuteNonQuery();
            try
            {
                maxnum = Convert.ToInt32(cmd4.Parameters["num"].Value.ToString());
                newnum = maxnum + 1;
            }
            catch
            {
                newnum = 1;
            }

            //update availability of selected time
            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "UpdateAvailability";
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("time", textBox8.Text);
            cmd2.ExecuteNonQuery();

            //insert new patient
            OracleCommand cmd3 = new OracleCommand();
            cmd3.Connection = conn;
            cmd3.CommandText = @"insert into patient(PATIENTNAME,PATIENTID,PATIENTNUMBER,GENDER,EMAIL,PASSWORD,PATIENTAGE)
                                 values(:PN,:PI,:PNum,:PG,:PE,:PP,:PA)";

            cmd3.Parameters.Add("PN", textBox9.Text);
            cmd3.Parameters.Add("PI", newid);
            cmd3.Parameters.Add("PNum", newnum);
            cmd3.Parameters.Add("PG", textBox11.Text);
            cmd3.Parameters.Add("PE", textBox12.Text);
            cmd3.Parameters.Add("PP", textBox13.Text);
            cmd3.Parameters.Add("PA", textBox10.Text);

            int r = cmd3.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Reserved Successfully :D");
            }
        }
    }
}

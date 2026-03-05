using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string conStr = @"Data Source=HOME;Initial Catalog=DBUsers;Integrated Security=True;";
        SqlConnection con;
        DataSet ds = new DataSet();
        SqlDataAdapter adptr;
        DataTable dtUsers;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(conStr);
            adptr = new SqlDataAdapter("SELECT * FROM TBUsers", con);
            adptr.Fill(ds, "Users"); //from SQL to C#
            dtUsers = ds.Tables["Users"];

            dataGridView1.DataSource = dtUsers;
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {
                if (dtUsers.Rows[i].RowState != DataRowState.Deleted && dtUsers.Rows[i]["Id"].ToString() == txtId.Text)
                    dtUsers.Rows[i]["Name"] = txtName.Text;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {
                if (dtUsers.Rows[i].RowState != DataRowState.Deleted && dtUsers.Rows[i]["Id"].ToString() == txtId.Text)
                    dtUsers.Rows[i].Delete();
            }
        }

        private void btnUpdateSql_Click(object sender, EventArgs e)
        {
            new SqlCommandBuilder(adptr);
            adptr.Update(dtUsers); //from C# to SQL
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow dr = dtUsers.NewRow();

            dr["Id"] = 7;
            dr["Name"] = txtName.Text;
            dr["Family"] = txtFamily.Text;

            dtUsers.Rows.Add(dr);
        }

        private void btnInsertWP_Click(object sender, EventArgs e)
        {
            SqlCommand comm = new SqlCommand(
                "INSERT INTO TBUsers(Name, Family) VALUES(@parName, @parFamily) ", con);
            
            SqlParameter parN = new SqlParameter("@parName",txtName.Text);
            comm.Parameters.Add(parN);

            comm.Parameters.Add(new SqlParameter("@parFamily", txtFamily.Text));
            
            comm.Connection.Open();
            int res =  comm.ExecuteNonQuery();
            comm.Connection.Close();
            
            MessageBox.Show(res.ToString());  
        }
    }
}

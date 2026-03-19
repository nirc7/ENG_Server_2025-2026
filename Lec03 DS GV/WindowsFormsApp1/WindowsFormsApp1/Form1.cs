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
            FillDSWAdptr();
        }

        private void FillDSWAdptr()
        {
            con = new SqlConnection(conStr);
            adptr = new SqlDataAdapter("SELECT * FROM TBUsers", con);
            ds.Clear();
            adptr.Fill(ds, "Users"); //from SQL to C#
            dtUsers = ds.Tables["Users"];

            dataGridView1.DataSource = dtUsers;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            #region over the course scope
            ////0100001111000011
            //int num = 1;//001 0x1A0FD
            ////bool isDoorOpened
            ////    bool is?WindonOpened
            #endregion

            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {
                if (dtUsers.Rows[i].RowState != DataRowState.Deleted && dtUsers.Rows[i]["Id"].ToString() == txtId.Text)
                {
                    dtUsers.Rows[i]["Name"] = txtName.Text;
                    dtUsers.Rows[i]["Family"] = txtFamily.Text;
                }
            }
            RefreshDB();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {
                if (dtUsers.Rows[i].RowState != DataRowState.Deleted && dtUsers.Rows[i]["Id"].ToString() == txtId.Text)
                    dtUsers.Rows[i].Delete();
            }

            RefreshDB();
        }

        private void btnUpdateSql_Click(object sender, EventArgs e)
        {
            RefreshDB();
        }

        private void RefreshDB()
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
            RefreshDB();
        }

        private void btnInsertWP_Click(object sender, EventArgs e)
        {
            SqlCommand comm = new SqlCommand(
                "INSERT INTO TBUsers(Name, Family) VALUES(@parName, @parFamily) ", con);

            SqlParameter parN = new SqlParameter("@parName", txtName.Text);
            comm.Parameters.Add(parN);

            comm.Parameters.Add(new SqlParameter("@parFamily", txtFamily.Text));

            comm.Connection.Open();
            int res = comm.ExecuteNonQuery();
            comm.Connection.Close();

            MessageBox.Show(res.ToString());
        }

        private void btnSelectWSP_Click(object sender, EventArgs e)
        {

            SqlCommand MySPCommand = new SqlCommand("SearchUser", con);
            MySPCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parId = new SqlParameter("@MyId ", SqlDbType.Int);
            parId.Value = txtId.Text;
            parId.Direction = ParameterDirection.Input;
            MySPCommand.Parameters.Add(parId);

            SqlParameter parFamily = new SqlParameter("@FamilyName ", SqlDbType.VarChar, 20);
            parFamily.Direction = ParameterDirection.Output;
            MySPCommand.Parameters.Add(parFamily);

            SqlParameter parReturn = new SqlParameter();
            parReturn.Direction = ParameterDirection.ReturnValue;
            MySPCommand.Parameters.Add(parReturn);

            con.Open();
            MySPCommand.ExecuteNonQuery();
            con.Close();

            if (parReturn.Value.ToString() == "0")
            {
                MessageBox.Show(parFamily.Value.ToString());
            }
            else
            {
                MessageBox.Show("Error" + parReturn.Value);
            }

        }

        private void btnSelectTableWSP_Click(object sender, EventArgs e)
        {
            SqlCommand MySPCommand = new SqlCommand("SearchUserTable", con);
            MySPCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parId = new SqlParameter("@MyId ", SqlDbType.Int);
            parId.Value = txtId.Text;
            parId.Direction = ParameterDirection.Input;
            MySPCommand.Parameters.Add(parId);

            SqlParameter parReturn = new SqlParameter();
            parReturn.Direction = ParameterDirection.ReturnValue;
            MySPCommand.Parameters.Add(parReturn);

            SqlDataAdapter adptr2 = new SqlDataAdapter(MySPCommand);
            if (ds.Tables["Users2"] != null)
            {
                ds.Tables["Users2"].Clear();
            }
            adptr2.Fill(ds, "Users2"); //from SQL to C#
            dataGridView1.DataSource = ds.Tables["Users2"];
        }

        private void btnFillDSWAdptr_Click(object sender, EventArgs e)
        {
            FillDSWAdptr();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            RefreshDB();
        }
    }
}

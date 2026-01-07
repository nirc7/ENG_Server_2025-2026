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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string conStr = @"Data Source=LAB-G700;Initial Catalog=DBUsers;Persist Security Info=True;User ID=Sa;Password=RuppinTech!;";
        SqlConnection con;

        public Form1()
        {
            InitializeComponent();

            con = new SqlConnection(conStr);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            NonQ($"INSERT INTO TBUsers(Name, Family) VALUES('{txtName.Text}','{txtFamily.Text}')");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            NonQ($" UPDATE TBUsers " +
                $" SET Name='{txtName.Text}' , Family='{txtFamily.Text}' " +
                $" WHERE Id={txtId.Text}");
        }

        private void NonQ(string command)
        {
           

            SqlCommand comm = new SqlCommand(command, con);

            //comm.Connection.Open();
            con.Open();
            int res = comm.ExecuteNonQuery();
            con.Close();

            if (res == 1)
            {
                MessageBox.Show(":)");
            }
            else
            {
                MessageBox.Show(":(");
            }

            RefreshLabel();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //good
            //    //
            //    //error
            //    //
            //}
            //catch (InvalidCastException e)
            //{

            //    throw;
            //}
            //catch (Exception e)
            //{
            //    //code excp
                
            //}
            //finally
            //{
            //    //code...close db
            //}

            //code....close db
            NonQ(" DELETE " +
             " FROM TBUsers " +
             " WHERE Id=" + txtId.Text);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            RefreshLabel();
        }

        private void RefreshLabel()
        {
            lblRes.Text = "";

            SqlCommand comm = new SqlCommand(
                " SELECT * " +
                " FROM TBUsers ", con);

            con.Open();

            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                lblRes.Text +=
                    reader["Id"].ToString() + " -- " +
                    reader["Name"].ToString() + " -- " +
                    reader["Family"].ToString() + "\n";
            }

            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshLabel();
        }
    }
}

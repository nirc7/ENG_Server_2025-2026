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
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string conStr = "...";
            SqlConnection con  = new SqlConnection(conStr);
            DataSet ds = new DataSet();
            SqlDataAdapter adptr = new SqlDataAdapter(
                "SELECT * FROM TBUsers", con);
            adptr.Fill(ds,"Users");
            DataTable dtUsers =  ds.Tables["Users"];
            dtUsers.Rows[2]["Name"] = "ben";

        }
    }
}

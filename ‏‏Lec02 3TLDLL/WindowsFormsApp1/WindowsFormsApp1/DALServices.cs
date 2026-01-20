using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    internal static class DALServices
    {
        static string conStr = @"Data Source=LAB-G700;Initial Catalog=DBUsers;Persist Security Info=True;User ID=Sa;Password=RuppinTech!;";
        static SqlConnection con;

        static DALServices()
        {
            con = new SqlConnection(conStr);
        }

        public static User InsertUser(string name, string family)
        {
            int id = GetIdInsert(
                $" INSERT INTO TBUsers(Name, Family) OUTPUT INSERTED.Id VALUES('{name}','{family}'); "); // +
                                                                                                         //$" SELECT SCOPE_IDENTITY() as id2;");

            if (id <= 0)
            {
                return null;
            }

            User user = new User() { Id = id, Name = name, Family = family };
            return user;
            //user.SetId(user.GetId()+1);
            //user.Id++;
            //user.id = -7;
        }

        internal static int DeleteUser(int id)
        {
            return NonQ(
                " DELETE " +
                " FROM TBUsers " +
                " WHERE Id=" + id);
        }

        internal static int UpdateUser(int id, string name, string family)
        {
            return NonQ($" UPDATE TBUsers " +
                 $" SET Name='{name}' , Family='{family}' " +
                 $" WHERE Id={id}");
        }

        internal static int UpdateUser(User u)
        {
            return NonQ($" UPDATE TBUsers " +
                 $" SET Name='{u.Name}' , Family='{u.Family}' " +
                 $" WHERE Id={u.Id}");
        }

        private static int GetIdInsert(string command)
        {
            int id = -1;

            try
            {
                SqlCommand comm = new SqlCommand(command, con);

                //comm.Connection.Open();
                con.Open();
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    id = (int)reader[0];
                }
                return id;
            }
            catch (Exception e)
            {
                File.AppendAllText("log1.txt",
                    "insert err! user is admin..GetIdInsert() -- " + DateTime.Now.ToShortTimeString() + "--" + e.Message);
                return id;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        private static int NonQ(string command)
        {
            try
            {
                SqlCommand comm = new SqlCommand(command, con);
                con.Open();
                int res = comm.ExecuteNonQuery();
                return res;
            }
            catch (Exception e)
            {
                File.AppendAllText("log1.txt",
                   "update err! user is admin.." + DateTime.Now.ToShortTimeString() + "--" + e.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }
    }
}

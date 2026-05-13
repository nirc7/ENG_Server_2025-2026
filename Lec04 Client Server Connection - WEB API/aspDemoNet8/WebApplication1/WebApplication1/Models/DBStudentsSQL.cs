//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Models
{
    public class DBStudentsSQL
    {

        public static List<Student> GetAllStudents()
        {
            
            List<Student> students = new List<Student>();
            //string strCon = @"Data Source=LAB-G700;Initial Catalog=DBUsers;User ID=sa;Password=RuppinTech!;";
            string strCon = @"Data Source=sql.bsite.net\MSSQL2016;User ID=kebewol829_;Password=RuppinTech!;Pooling=False;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Authentication=RuppinTech!;";
            SqlConnection con = new SqlConnection(strCon);

            SqlCommand com = new SqlCommand(
                " SELECT * " +
                " FROM TBUsers", con);

            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student()
                    {
                        Id = (int)reader["ID"],
                        Name = reader["Name"].ToString(),
                        Grade = (int)reader["Grade"]
                    });
                }
            }
            catch (Exception e)
            {
                
            }
            finally
            {
                con.Close();
            }

            return students;
        }
    }
}

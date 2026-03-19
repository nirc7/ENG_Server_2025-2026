namespace WebApplication1.Models
{
    public class DBStudentsMock
    {
        static public List<Student> students = new List<Student>() {
            new Student(){Id=1 , Name = "ben",      Grade= 98},
            new Student(){Id=2 , Name = "avi",      Grade= 100},
            new Student(){Id=3 , Name = "dora",     Grade= 97},
            new Student(){Id=4 , Name = "charlie" , Grade= 95},
        };
    }
}

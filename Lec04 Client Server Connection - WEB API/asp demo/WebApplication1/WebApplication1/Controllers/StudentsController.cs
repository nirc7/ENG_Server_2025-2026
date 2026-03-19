using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            Student[] students = DBStudentsMock.students.ToArray();
            return students;
        }

        [HttpGet("{id}")]
        public Student Get(int id)
        {
            Student s1 = DBStudentsMock.students.FirstOrDefault(stu => stu.Id == id);
            return s1;
        }
    }
}

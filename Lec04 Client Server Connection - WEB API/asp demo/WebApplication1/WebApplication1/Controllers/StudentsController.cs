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

        [HttpPost]
        public int Post([FromBody] Student stu2Insert)
        {
            int newId = DBStudentsMock.students.Max(stu => stu.Id) + 1;
            stu2Insert.Id = newId;
            DBStudentsMock.students.Add(stu2Insert);
            return newId;
        }

        [HttpPut("{id}")]
        public string Put(int id, [FromBody] Student stu2Update )
        {
            Student stu2Find = DBStudentsMock.students.FirstOrDefault(stu=> stu.Id == id);
            stu2Find.Name = stu2Update.Name;
            stu2Find.Grade = stu2Update.Grade;
            return "done:)";
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            Student stu2Del = DBStudentsMock.students.SingleOrDefault(stu=> stu.Id == id);
            DBStudentsMock.students.Remove(stu2Del);
            var v = new {Result = "deleted successfully!" };
            var r = new JsonResult(v);
            return r;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsSQLController : ControllerBase
    {
        //private readonly string _connectionString;

        //public StudentsSQLController(IConfiguration configuration)
        //{
        //    _connectionString =
        //        configuration.GetConnectionString("DefaultConnection");
        //}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Student[]> Get()
        {
            try
            {
                Student[] students = DBStudentsSQL.GetAllStudents().ToArray();
                if (students.Length == 0)
                {
                    return Ok(null);
                }
                return Ok(students);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message + " לא טוב !");
            }
        }
    }
}

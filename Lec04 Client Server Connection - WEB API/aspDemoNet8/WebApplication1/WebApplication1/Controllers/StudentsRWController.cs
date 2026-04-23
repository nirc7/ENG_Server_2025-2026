using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsRWController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Student[]> Get()
        {
            try
            {
                Student[] students = DBStudentsMock.students.ToArray();
                return Ok(students);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message + " לא טוב !");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Student))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            try
            {
                Student stu2Find = DBStudentsMock.students.FirstOrDefault(stu => stu.Id == id);
                if (stu2Find == null)
                {
                    return NotFound($"student with id = {id} was not found in the Get by Id action!");
                }
                return Ok(stu2Find);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Student))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Student value) {
            try
            {
                if (value == null)
                    return BadRequest(value);
                else if (value.Id != 0)
                    return StatusCode(StatusCodes.Status500InternalServerError);

                int maxId = DBStudentsMock.students.Max(stu => stu.Id) + 1;
                value.Id = maxId;
                DBStudentsMock.students.Add(value);
                return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Put(int id, [FromBody] Student value ) 
        {
            try
            {
                if (value == null || value.Id != id)
                    return BadRequest();

                Student stu2Update = DBStudentsMock.students.FirstOrDefault(stu=> stu.Id == id);
                if (stu2Update == null)
                    return NotFound($"student with id={id} was not fount in the PUT action!");

                stu2Update.Name = value.Name;
                stu2Update.Grade = value.Grade;

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);   
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id) 
        {
            try
            {
                if (id == 0)
                    return BadRequest();

                Student stu2Del = DBStudentsMock.students.FirstOrDefault(stu=>stu.Id == id);
                if (stu2Del == null)
                    return NotFound($"student with id={id} was not found in the DELETE action!");

                DBStudentsMock.students.Remove(stu2Del);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

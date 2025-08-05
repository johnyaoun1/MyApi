using Microsoft.AspNetCore.Mvc;
using MyApi.Models;
using MyApi.Services;
using System.Globalization;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public ActionResult<List<Student>> GetStudents() => _studentService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(long id)
        {
            var student = _studentService.GetById(id);
            if (student == null)
                return NotFound();
            return student;
        }

        [HttpGet("filter")]
        public ActionResult<List<Student>> GetStudentsByName([FromQuery] string name) => _studentService.GetByName(name);

        [HttpGet("current-date")]
        public ActionResult<string> GetCurrentDate()
        {
            var culture = Request.Headers["Accept-Language"].ToString();
            var cultureInfo = new CultureInfo(culture ?? "en-US");
            return DateTime.Now.ToString("yyyy-MM-dd", cultureInfo);
        }

        [HttpPost]
        public ActionResult UpdateStudent([FromBody] Student student)
        {
            _studentService.UpdateStudent(student.Id, student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStudent(long id)
        {
            _studentService.DeleteStudent(id);
            return NoContent();
        }
    }
}

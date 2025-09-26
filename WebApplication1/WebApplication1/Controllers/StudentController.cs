using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication1.DATA;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        //private static List<Student> students = new List<Student>
        //{
        //    new Student{Id = 1, Name = "Subrat", Age = 22,Email = "subrat@gmail.com"}
        //};
        private readonly FirstAPIContext _context;
        public StudentController(FirstAPIContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<Student>> GetAllStudents()
        {
            return Ok(await _context.Students.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var Student = await _context.Students.FindAsync(id);
            if (Student == null) return NotFound();
            return Student;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student newStudent)
        {
            if (newStudent == null) return BadRequest();
            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.Id }, newStudent);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student updatedStudent)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.Email = updatedStudent.Email;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

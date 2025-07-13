using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data;
using SchoolAPI.DTOs;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context) => _context = context;

        // ✅ Get all students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var data = await _context.Students
                                     .Include(s => s.School)
                                     .Include(s => s.Class)
                                     .Select(s => new StudentDTO
                                     {
                                         StudentId = s.StudentId,
                                         Name = s.Name,
                                         Age = s.Age,
                                         SchoolId = s.SchoolId,
                                         ClassId = s.ClassId,
                                         SchoolName = s.School.Name,
                                         SchoolAddress = s.School.Address,
                                         ClassName = s.Class.Name,
                                         ClassSection = s.Class.Section,
                                     })
                                     .ToListAsync();

            return Ok(data);
        }

        // ✅ Get student by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students
                                        .Include(s => s.School)
                                        .Include(s => s.Class)
                                        .Select(s => new StudentDTO
                                        {
                                            StudentId = s.StudentId,
                                            Name = s.Name,
                                            Age = s.Age,
                                            SchoolId = s.SchoolId,
                                            ClassId = s.ClassId,
                                            SchoolName = s.School.Name,
                                            SchoolAddress = s.School.Address,
                                            ClassName = s.Class.Name,
                                            ClassSection = s.Class.Section,
                                        })
                                        .FirstOrDefaultAsync(s => s.StudentId == id);

            return student == null ? NotFound() : Ok(student);
        }

        // ✅ Create new student
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // ✅ Update student
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.StudentId) return BadRequest();

            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        // ✅ Delete student
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(true);
        }

        // ✅ Search students by student name, school name, class name, age
        [HttpGet("search")]
        public async Task<IActionResult> SearchStudents(
            [FromQuery] string? studentName,
            [FromQuery] string? schoolName,
            [FromQuery] string? className,
            [FromQuery] int? age)
        {
            var query = _context.Students
                                .Include(s => s.School)
                                .Include(s => s.Class)
                                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(studentName))
                query = query.Where(s => s.Name.Contains(studentName));

            if (!string.IsNullOrWhiteSpace(schoolName))
                query = query.Where(s => s.School.Name.Contains(schoolName));

            if (!string.IsNullOrWhiteSpace(className))
                query = query.Where(s => s.Class.Name.Contains(className));

            if (age.HasValue)
                query = query.Where(s => s.Age == age.Value);

            var students = await query.Select(s => new StudentDTO
                                      {
                                          StudentId = s.StudentId,
                                          Name = s.Name,
                                          Age = s.Age,
                                          SchoolId = s.SchoolId,
                                          ClassId = s.ClassId,
                                          SchoolName = s.School.Name,
                                          SchoolAddress = s.School.Address,
                                          ClassName = s.Class.Name,
                                          ClassSection = s.Class.Section,
                                      })
                                      .ToListAsync();
            return Ok(students);
        }
    }
}

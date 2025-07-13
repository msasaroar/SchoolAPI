using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TeacherController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            return Ok(await _context.Teachers.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            return teacher == null ? NotFound() : Ok(teacher);
        }

        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
        {
            if (id != teacher.TeacherId) return BadRequest();
            _context.Entry(teacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return NotFound();
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchTeachers(
            [FromQuery] string? name,
            [FromQuery] int? schoolid,
            [FromQuery] string? subject)
        {
            var query = _context.Teachers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(t => t.Name.Contains(name));

            if (schoolid.HasValue)
                query = query.Where(t => t.SchoolId == schoolid.Value);

            if (!string.IsNullOrWhiteSpace(subject))
                query = query.Where(t => t.Subject.Contains(subject));

            var teachers = await query.ToListAsync();
            return Ok(teachers);
        }




    }
}

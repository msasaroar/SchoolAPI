using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data;
using SchoolAPI.Model;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TeacherController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers() => await _context.Teachers.ToListAsync();

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
            return CreatedAtAction(nameof(GetTeacher), new { id = teacher.TeacherId }, teacher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
        {
            if (id != teacher.TeacherId) return BadRequest();
            _context.Entry(teacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return NotFound();
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}




/*
using Microsoft.AspNetCore.Mvc;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("TeacherController is working!");
        }
    }
}
*/

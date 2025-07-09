using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data;
using SchoolAPI.Model;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SchoolController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<School>>> GetSchools() => await _context.Schools.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<School>> GetSchool(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            return school == null ? NotFound() : Ok(school);
        }

        [HttpPost]
        public async Task<ActionResult<School>> PostSchool(School school)
        {
            _context.Schools.Add(school);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSchool), new { id = school.SchoolId }, school);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchool(int id, School school)
        {
            if (id != school.SchoolId) return BadRequest();
            _context.Entry(school).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null) return NotFound();
            _context.Schools.Remove(school);
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
    public class SchoolController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("SchoolController is working!");
        }
    }
}
*/

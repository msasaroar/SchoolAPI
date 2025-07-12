using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ClassController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses() => await _context.Classes.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            var @class = await _context.Classes.FindAsync(id);
            return @class == null ? NotFound() : Ok(@class);
        }

        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(Class @class)
        {
            _context.Classes.Add(@class);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, Class @class)
        {
            if (id != @class.ClassId) return BadRequest();
            _context.Entry(@class).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var @class = await _context.Classes.FindAsync(id);
            if (@class == null) return NotFound();
            _context.Classes.Remove(@class);
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchClasses([FromQuery] string? name, [FromQuery] string? section)
        {
            var query = _context.Classes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(c => c.Name.Contains(name));

            if (!string.IsNullOrWhiteSpace(section))
                query = query.Where(c => c.Section.Contains(section));

            var Classes = await query.ToListAsync();
            return Ok(Classes);
        }
    }
}


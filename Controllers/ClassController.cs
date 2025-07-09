using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data;
using SchoolAPI.Model;

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
            return CreatedAtAction(nameof(GetClass), new { id = @class.ClassId }, @class);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, Class @class)
        {
            if (id != @class.ClassId) return BadRequest();
            _context.Entry(@class).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var @class = await _context.Classes.FindAsync(id);
            if (@class == null) return NotFound();
            _context.Classes.Remove(@class);
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
    public class ClassController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("ClassController is working!");
        }
    }
}
*/


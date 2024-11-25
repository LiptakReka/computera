using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerApiHetfo.Models;

namespace ComputerApiHetfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        private readonly ComputerContext _context;

        public ComputerController(ComputerContext context)
        {
            _context = context;
        }

        // GET: api/Computer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comp>>> GetComps()
        {
            return await _context.Comps.Include(c => c.Os).ToListAsync();
        }

        // GET: api/Computer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Comp>> GetComp(Guid id)
        {
            var comp = await _context.Comps.Include(c => c.Os)
                .FirstOrDefaultAsync(c => c.id == id);

            if (comp == null)
            {
                return NotFound();
            }

            return comp;
        }

        // POST: api/Computer
        [HttpPost]
        public async Task<ActionResult<Comp>> PostComp(Comp comp)
        {
            comp.id = Guid.NewGuid();  // Az új rekordhoz egy új GUID azonosítót generálunk
            comp.createdat = DateTime.UtcNow; // Beállítjuk a létrehozás idejét

            _context.Comps.Add(comp);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComp), new { id = comp.id }, comp);
        }

        // PUT: api/Computer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComp(Guid id, Comp comp)
        {
            if (id != comp.id)
            {
                return BadRequest();
            }

            _context.Entry(comp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Computer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComp(Guid id)
        {
            var comp = await _context.Comps.FindAsync(id);
            if (comp == null)
            {
                return NotFound();
            }

            _context.Comps.Remove(comp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompExists(Guid id)
        {
            return _context.Comps.Any(e => e.id == id);
        }
    }
}

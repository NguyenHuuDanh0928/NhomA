using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiFishVetClinicAPI.Data;
using KoiFishVetClinicAPI.Models;

namespace KoiFishVetClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichHenController : ControllerBase
    {
        private readonly KoiFishVetClinicDbContext _context;

        public LichHenController(KoiFishVetClinicDbContext context)
        {
            _context = context;
        }

        // GET: api/LichHen
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<LichHen>>> GetLichHens()
        {
            return await _context.LichHen.ToListAsync();
        }

        // GET: api/LichHen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LichHen>> GetLichHen(int id)
        {
            var lichHen = await _context.LichHen.FindAsync(id);

            if (lichHen == null)
            {
                return NotFound();
            }

            return lichHen;
        }

        // POST: api/LichHen
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<LichHen>> PostLichHen(LichHen lichHen)
        {
            _context.LichHen.Add(lichHen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLichHen", new { id = lichHen.Id }, lichHen);
        }

        // PUT: api/LichHen/5
        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> PutLichHen(int id, LichHen lichHen)
        {
            if (id != lichHen.Id)
            {
                return BadRequest();
            }

            _context.Entry(lichHen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LichHenExists(id))
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

        // DELETE: api/LichHen/5
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteLichHen(int id)
        {
            var lichHen = await _context.LichHen.FindAsync(id);
            if (lichHen == null)
            {
                return NotFound();
            }

            _context.LichHen.Remove(lichHen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LichHenExists(int id)
        {
            return _context.LichHen.Any(e => e.Id == id);
        }
    }
}
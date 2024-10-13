using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiFishVetClinicAPI.Data;
using KoiFishVetClinicAPI.Models;

namespace KoiFishVetClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanHoiController : ControllerBase
    {
        private readonly KoiFishVetClinicDbContext _context;

        public PhanHoiController(KoiFishVetClinicDbContext context)
        {
            _context = context;
        }

        // GET: api/PhanHoi
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<PhanHoi>>> GetPhanHois()
        {
            return await _context.PhanHoi.ToListAsync();
        }

        // GET: api/PhanHoi/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<PhanHoi>> GetPhanHoi(int id)
        {
            var phanHoi = await _context.PhanHoi.FindAsync(id);

            if (phanHoi == null)
            {
                return NotFound();
            }

            return phanHoi;
        }

        // POST: api/PhanHoi
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<PhanHoi>> PostPhanHoi(PhanHoi phanHoi)
        {
            _context.PhanHoi.Add(phanHoi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhanHoi", new { id = phanHoi.Id }, phanHoi);
        }

        // PUT: api/PhanHoi/5
        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> PutPhanHoi(int id, PhanHoi phanHoi)
        {
            if (id != phanHoi.Id)
            {
                return BadRequest();
            }

            _context.Entry(phanHoi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhanHoiExists(id))
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

        // DELETE: api/PhanHoi/5
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeletePhanHoi(int id)
        {
            var phanHoi = await _context.PhanHoi.FindAsync(id);
            if (phanHoi == null)
            {
                return NotFound();
            }

            _context.PhanHoi.Remove(phanHoi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhanHoiExists(int id)
        {
            return _context.PhanHoi.Any(e => e.Id == id);
        }
    }
}
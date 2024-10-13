using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiFishVetClinicAPI.Data;
using KoiFishVetClinicAPI.Models;

namespace KoiFishVetClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichLamViecController : ControllerBase
    {
        private readonly KoiFishVetClinicDbContext _context;

        public LichLamViecController(KoiFishVetClinicDbContext context)
        {
            _context = context;
        }

        // GET: api/LichLamViec
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<LichLamViec>>> GetLichLamViecs()
        {
            return await _context.LichLamViec
                .Include(llv => llv.BacSiThuY) // Include thông tin bác sĩ
                .ToListAsync();
        }

        // GET: api/LichLamViec/5
        [HttpGet("{id}")]

        public async Task<ActionResult<LichLamViec>> GetLichLamViec(int id)
        {
            var lichLamViec = await _context.LichLamViec
                .Include(llv => llv.BacSiThuY) // Include thông tin bác sĩ
                .FirstOrDefaultAsync(llv => llv.Id == id);

            if (lichLamViec == null)
            {
                return NotFound();
            }

            return lichLamViec;
        }

        // POST: api/LichLamViec
        [HttpPost]
        public async Task<ActionResult<LichLamViec>> PostLichLamViec(LichLamViec lichLamViec)
        {
            _context.LichLamViec.Add(lichLamViec);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLichLamViec", new { id = lichLamViec.Id }, lichLamViec);
        }

        // PUT: api/LichLamViec/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLichLamViec(int id, LichLamViec lichLamViec)
        {
            if (id != lichLamViec.Id)
            {
                return BadRequest();
            }

            _context.Entry(lichLamViec).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LichLamViecExists(id))
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

        // DELETE: api/LichLamViec/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLichLamViec(int id)
        {
            var lichLamViec = await _context.LichLamViec.FindAsync(id);
            if (lichLamViec == null)
            {
                return NotFound();
            }

            _context.LichLamViec.Remove(lichLamViec);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LichLamViecExists(int id)
        {
            return _context.LichLamViec.Any(e => e.Id == id);
        }
    }
}
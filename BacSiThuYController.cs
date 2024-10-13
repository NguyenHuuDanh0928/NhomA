using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiFishVetClinicAPI.Data;
using KoiFishVetClinicAPI.Models;

namespace KoiFishVetClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BacSiThuYController : ControllerBase
    {
        private readonly KoiFishVetClinicDbContext _context;

        public BacSiThuYController(KoiFishVetClinicDbContext context)
        {
            _context = context;
        }

        // GET: api/BacSiThuY
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<BacSiThuY>>> GetBacSiThuYs()
        {
            return await _context.BacSiThuY.ToListAsync();
        }

        // GET: api/BacSiThuY/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<BacSiThuY>> GetBacSiThuY(int id)
        {
            var bacSiThuY = await _context.BacSiThuY.FindAsync(id);

            if (bacSiThuY == null)
            {
                return NotFound();
            }

            return bacSiThuY;
        }

        // POST: api/BacSiThuY
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<BacSiThuY>> PostBacSiThuY(BacSiThuY bacSiThuY)
        {
            _context.BacSiThuY.Add(bacSiThuY);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBacSiThuY", new { id = bacSiThuY.Id }, bacSiThuY);
        }

        // PUT: api/BacSiThuY/5
        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> PutBacSiThuY(int id, BacSiThuY bacSiThuY)
        {
            if (id != bacSiThuY.Id)
            {
                return BadRequest();
            }

            _context.Entry(bacSiThuY).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BacSiThuYExists(id))
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

        // DELETE: api/BacSiThuY/5
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteBacSiThuY(int id)
        {
            var bacSiThuY = await _context.BacSiThuY.FindAsync(id);
            if (bacSiThuY == null)
            {
                return NotFound();
            }

            _context.BacSiThuY.Remove(bacSiThuY);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BacSiThuYExists(int id)
        {
            return _context.BacSiThuY.Any(e => e.Id == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDichVu.DuLieu;

namespace WebDichVu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BacSiThuYAPI : ControllerBase
    {
        private readonly PetShopDbContext _context;

        public BacSiThuYAPI(PetShopDbContext context)
        {
            _context = context;
        }

        // GET: api/BacSiThuY
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BacSiThuY>>> GetBacSiThuYs()
        {
            return await _context.BacSiThuYs.ToListAsync();
        }

        // GET: api/BacSiThuY/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BacSiThuY>> GetBacSiThuY(int id)
        {
            var bacSiThuY = await _context.BacSiThuYs.FindAsync(id);

            if (bacSiThuY == null)
            {
                return NotFound();
            }

            return bacSiThuY;
        }

        // PUT: api/BacSiThuY/5
       
        [HttpPut("{id}")]
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

        // POST: api/BacSiThuY
        
        [HttpPost]
        public async Task<ActionResult<BacSiThuY>> PostBacSiThuY(BacSiThuY bacSiThuY)
        {
            _context.BacSiThuYs.Add(bacSiThuY);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBacSiThuY", new { id = bacSiThuY.Id }, bacSiThuY);
        }

        // DELETE: api/BacSiThuY/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBacSiThuY(int id)
        {
            var bacSiThuY = await _context.BacSiThuYs.FindAsync(id);
            if (bacSiThuY == null)
            {
                return NotFound();
            }

            _context.BacSiThuYs.Remove(bacSiThuY);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BacSiThuYExists(int id)
        {
            return _context.BacSiThuYs.Any(e => e.Id == id);
        }
    }
}

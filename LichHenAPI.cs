using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDichVu.DuLieu;

namespace WebDichVu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichHenAPI : ControllerBase
    {
        private readonly PetShopDbContext _context;

        public LichHenAPI(PetShopDbContext context)
        {
            _context = context;
        }

        // GET: api/LichHen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LichHen>>> GetLichHens()
        {
            return await _context.LichHens
                .Include(l => l.BacSiThuY)
                .Include(l => l.DichVu)
                .Include(l => l.KhachHang)
                .ToListAsync();
        }

        // GET: api/LichHen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LichHen>> GetLichHen(int id)
        {
            var lichHen = await _context.LichHens
                .Include(l => l.BacSiThuY)
                .Include(l => l.DichVu)
                .Include(l => l.KhachHang)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lichHen == null)
            {
                return NotFound();
            }

            return lichHen;
        }

        // PUT: api/LichHen/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLichHen(int id, [Bind("KhachHangId, BacSiThuYid, DichVuId, ThoiGian, DiaDiem, TrangThai")] LichHen lichHen)
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

        // POST: api/LichHen
        
        [HttpPost]
        public async Task<ActionResult<LichHen>> PostLichHen([Bind("KhachHangId, BacSiThuYid, DichVuId, ThoiGian, DiaDiem, TrangThai")] LichHen lichHen)
        {
            _context.LichHens.Add(lichHen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLichHen", new { id = lichHen.Id }, lichHen);
        }

        // DELETE: api/LichHen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLichHen(int id)
        {
            var lichHen = await _context.LichHens.FindAsync(id);
            if (lichHen == null)
            {
                return NotFound();
            }

            _context.LichHens.Remove(lichHen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LichHenExists(int id)
        {
            return _context.LichHens.Any(e => e.Id == id);
        }
    }
}

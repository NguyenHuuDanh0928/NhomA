using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDichVu.DuLieu;

namespace WebDichVu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanHoiAPI : ControllerBase
    {
        private readonly PetShopDbContext _context;

        public PhanHoiAPI(PetShopDbContext context)
        {
            _context = context;
        }

        // GET: api/PhanHoi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhanHoi>>> GetPhanHois()
        {
            return await _context.PhanHois
                .Include(p => p.DichVu)
                .Include(p => p.KhachHang)
                .ToListAsync();
        }

        // GET: api/PhanHoi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhanHoi>> GetPhanHoi(int id)
        {
            var phanHoi = await _context.PhanHois
                .Include(p => p.DichVu)
                .Include(p => p.KhachHang)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (phanHoi == null)
            {
                return NotFound();
            }

            return phanHoi;
        }

        // PUT: api/PhanHoi/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhanHoi(int id, [Bind("KhachHangId, DichVuId, NoiDung, ThoiGian")] PhanHoi phanHoi)
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

        // POST: api/PhanHoi
        
        [HttpPost]
        public async Task<ActionResult<PhanHoi>> PostPhanHoi([Bind("KhachHangId, DichVuId, NoiDung, ThoiGian")] PhanHoi phanHoi)
        {
            _context.PhanHois.Add(phanHoi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhanHoi", new { id = phanHoi.Id }, phanHoi);
        }

        // DELETE: api/PhanHoi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhanHoi(int id)
        {
            var phanHoi = await _context.PhanHois.FindAsync(id);
            if (phanHoi == null)
            {
                return NotFound();
            }

            _context.PhanHois.Remove(phanHoi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhanHoiExists(int id)
        {
            return _context.PhanHois.Any(e => e.Id == id);
        }
    }
}

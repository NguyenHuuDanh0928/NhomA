using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDichVu.DuLieu;

namespace WebDichVu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuDatDichVuAPI : ControllerBase
    {
        private readonly PetShopDbContext _context;

        public LichSuDatDichVuAPI(PetShopDbContext context)
        {
            _context = context;
        }

        // GET: api/LichSuDatDichVu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LichSuDatDichVu>>> GetLichSuDatDichVus()
        {
            return await _context.LichSuDatDichVus
                .Include(l => l.DichVu)
                .Include(l => l.KhachHang)
                .ToListAsync();
        }

        // GET: api/LichSuDatDichVu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LichSuDatDichVu>> GetLichSuDatDichVu(int id)
        {
            var lichSuDatDichVu = await _context.LichSuDatDichVus
                .Include(l => l.DichVu)
                .Include(l => l.KhachHang)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lichSuDatDichVu == null)
            {
                return NotFound();
            }

            return lichSuDatDichVu;
        }

        // PUT: api/LichSuDatDichVu/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLichSuDatDichVu(int id, [Bind("KhachHangId, DichVuId, ThoiGian, TrangThai")] LichSuDatDichVu lichSuDatDichVu)
        {
            if (id != lichSuDatDichVu.Id)
            {
                return BadRequest();
            }

            _context.Entry(lichSuDatDichVu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LichSuDatDichVuExists(id))
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

        // POST: api/LichSuDatDichVu
        
        [HttpPost]
        public async Task<ActionResult<LichSuDatDichVu>> PostLichSuDatDichVu([Bind("KhachHangId, DichVuId, ThoiGian, TrangThai")] LichSuDatDichVu lichSuDatDichVu)
        {
            _context.LichSuDatDichVus.Add(lichSuDatDichVu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLichSuDatDichVu", new { id = lichSuDatDichVu.Id }, lichSuDatDichVu);
        }

        // DELETE: api/LichSuDatDichVu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLichSuDatDichVu(int id)
        {
            var lichSuDatDichVu = await _context.LichSuDatDichVus.FindAsync(id);
            if (lichSuDatDichVu == null)
            {
                return NotFound();
            }

            _context.LichSuDatDichVus.Remove(lichSuDatDichVu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LichSuDatDichVuExists(int id)
        {
            return _context.LichSuDatDichVus.Any(e => e.Id == id);
        }
    }
}

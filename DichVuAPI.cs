using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDichVu.DuLieu;

namespace WebDichVu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuAPI : ControllerBase
    {
        private readonly PetShopDbContext _context;

        public DichVuAPI(PetShopDbContext context)
        {
            _context = context;
        }

        // GET: api/DichVu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DichVu>>> GetDichVus()
        {
            return await _context.DichVus.ToListAsync();
        }

        // GET: api/DichVu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DichVu>> GetDichVu(int id)
        {
            var dichVu = await _context.DichVus.FindAsync(id);

            if (dichVu == null)
            {
                return NotFound();
            }

            return dichVu;
        }

        // PUT: api/DichVu/5
       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDichVu(int id, [Bind("TenDichVu, MoTa, Gia, ChiPhiDiChuyen")] DichVu dichVu)
        {
            if (id != dichVu.Id)
            {
                return BadRequest();
            }

            _context.Entry(dichVu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DichVuExists(id))
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

        // POST: api/DichVu
        
        [HttpPost]
        public async Task<ActionResult<DichVu>> PostDichVu([Bind("TenDichVu, MoTa, Gia, ChiPhiDiChuyen")] DichVu dichVu)
        {
            _context.DichVus.Add(dichVu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDichVu", new { id = dichVu.Id }, dichVu);
        }

        // DELETE: api/DichVu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDichVu(int id)
        {
            var dichVu = await _context.DichVus.FindAsync(id);
            if (dichVu == null)
            {
                return NotFound();
            }

            _context.DichVus.Remove(dichVu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DichVuExists(int id)
        {
            return _context.DichVus.Any(e => e.Id == id);
        }
    }
}

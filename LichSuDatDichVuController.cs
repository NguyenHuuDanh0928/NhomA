using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiFishVetClinicAPI.Data;
using KoiFishVetClinicAPI.Models;

namespace KoiFishVetClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuDatDichVuController : ControllerBase
    {
        private readonly KoiFishVetClinicDbContext _context;

        public LichSuDatDichVuController(KoiFishVetClinicDbContext context)
        {
            _context = context;
        }

        // GET: api/LichSuDatDichVu
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<LichSuDatDichVu>>> GetLichSuDatDichVus()
        {
            return await _context.LichSuDatDichVu.ToListAsync();
        }

        // GET: api/LichSuDatDichVu/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<LichSuDatDichVu>> GetLichSuDatDichVu(int id)
        {
            var lichSuDatDichVu = await _context.LichSuDatDichVu.FindAsync(id);

            if (lichSuDatDichVu == null)
            {
                return NotFound();
            }

            return lichSuDatDichVu;
        }

        // POST: api/LichSuDatDichVu
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<LichSuDatDichVu>> PostLichSuDatDichVu(LichSuDatDichVu lichSuDatDichVu)
        {
            _context.LichSuDatDichVu.Add(lichSuDatDichVu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLichSuDatDichVu", new { id = lichSuDatDichVu.Id }, lichSuDatDichVu);
        }

        // PUT: api/LichSuDatDichVu/5
        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> PutLichSuDatDichVu(int id, LichSuDatDichVu lichSuDatDichVu)
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

        // DELETE: api/LichSuDatDichVu/5
        [HttpDelete("{id}")]
         [Produces("application/json")] 
        public async Task<IActionResult> DeleteLichSuDatDichVu(int id)
        {
            var lichSuDatDichVu = await _context.LichSuDatDichVu.FindAsync(id);
            if (lichSuDatDichVu == null)
            {
                return NotFound();
            }

            _context.LichSuDatDichVu.Remove(lichSuDatDichVu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LichSuDatDichVuExists(int id)
        {
            return _context.LichSuDatDichVu.Any(e => e.Id == id);
        }
    }
}
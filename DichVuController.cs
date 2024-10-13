using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiFishVetClinicAPI.Data;
using KoiFishVetClinicAPI.Models;

namespace KoiFishVetClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuController : ControllerBase
    {
        private readonly KoiFishVetClinicDbContext _context;

        public DichVuController(KoiFishVetClinicDbContext context)
        {
            _context = context;
        }

        // GET: api/DichVu
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<DichVu>>> GetDichVus()
        {
            return await _context.DichVu.ToListAsync();
        }

        // GET: api/DichVu/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<DichVu>> GetDichVu(int id)
        {
            var dichVu = await _context.DichVu.FindAsync(id);

            if (dichVu == null)
            {
                return NotFound();
            }

            return dichVu;
        }

        // POST: api/DichVu
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<DichVu>> PostDichVu(DichVu dichVu)
        {
            _context.DichVu.Add(dichVu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDichVu", new { id = dichVu.Id }, dichVu);
        }

        // PUT: api/DichVu/5
        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> PutDichVu(int id, DichVu dichVu)
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

        // DELETE: api/DichVu/5
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteDichVu(int id)
        {
            var dichVu = await _context.DichVu.FindAsync(id);
            if (dichVu == null)
            {
                return NotFound();
            }

            _context.DichVu.Remove(dichVu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DichVuExists(int id)
        {
            return _context.DichVu.Any(e => e.Id == id);
        }
    }
}
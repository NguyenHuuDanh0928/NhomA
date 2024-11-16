using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDichVu.Datas;

namespace WebDichVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class LichLamViecController : Controller
    {
        private readonly ThuYcaKoiVetContext _context;

        public LichLamViecController(ThuYcaKoiVetContext context)
        {
            _context = context;
        }

        // GET: admin/LichLamViec
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var thuYcaKoiVetContext = _context.LichLamViecs.Include(l => l.BacSiThuY);
            return View(await thuYcaKoiVetContext.ToListAsync());
        }

        // GET: admin/LichLamViec/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LichLamViecs == null)
            {
                return NotFound();
            }

            var lichLamViec = await _context.LichLamViecs
                .Include(l => l.BacSiThuY)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lichLamViec == null)
            {
                return NotFound();
            }

            return View(lichLamViec);
        }

        // GET: admin/LichLamViec/Create
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["BacSiThuYid"] = new SelectList(await _context.BacSiThuYs.ToListAsync(), "Id", "TenBacSi");
            return View();
        }

        // POST: admin/LichLamViec/Create
        // POST: admin/BacSi/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BacSiThuYid,Ngay,GioBatDau,GioKetThuc,TrangThai")] LichLamViec lichLamViec)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(lichLamViec);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the exception or display a user-friendly error message.
                    ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi thêm lịch làm việc: " + ex.Message);
                    return View(lichLamViec);
                }
            }

            // Nếu ModelState không hợp lệ, lấy lại danh sách bác sĩ để hiển thị trong dropdown
            var bacSiList = await _context.BacSiThuYs.ToListAsync();
            ViewData["BacSiThuYid"] = new SelectList(bacSiList, "Id", "TenBacSi", lichLamViec.BacSiThuYid);
            return View(lichLamViec);
        }

        // GET: admin/LichLamViec/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichLamViec = await _context.LichLamViecs.FindAsync(id);
            if (lichLamViec == null)
            {
                return NotFound();
            }
            ViewData["BacSiThuYid"] = new SelectList(_context.BacSiThuYs, "Id", "TenBacSi", lichLamViec.BacSiThuYid);
            return View(lichLamViec);
        }

        // POST: admin/LichLamViec/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LichLamViec lichLamViec)
        {
            if (id != lichLamViec.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lichLamViec);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LichLamViecExists(lichLamViec.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BacSiThuYid"] = new SelectList(_context.BacSiThuYs, "Id", "TenBacSi", lichLamViec.BacSiThuYid);
            return View(lichLamViec);
        }


        // GET: admin/LichLamViec/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LichLamViecs == null)
            {
                return NotFound();
            }

            var lichLamViec = await _context.LichLamViecs
                .Include(l => l.BacSiThuY)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lichLamViec == null)
            {
                return NotFound();
            }

            return View(lichLamViec);
        }

        // POST: admin/LichLamViec/Delete/5
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LichLamViecs == null)
            {
                return Problem("Entity set 'ThuYcaKoiVetContext.LichLamViecs'  is null.");
            }
            var lichLamViec = await _context.LichLamViecs.FindAsync(id);
            if (lichLamViec != null)
            {
                _context.LichLamViecs.Remove(lichLamViec);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LichLamViecExists(int id)
        {
            return (_context.LichLamViecs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
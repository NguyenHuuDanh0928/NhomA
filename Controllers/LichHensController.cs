using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDichVu.Datas;

namespace WebDichVu.Controllers
{
    public class LichHensController : Controller
    {
        private readonly ThuYcaKoiVetContext _context;

        public LichHensController(ThuYcaKoiVetContext context)
        {
            _context = context;
        }

        // GET: LichHens
        public async Task<IActionResult> Index()
        {
            var thuYcaKoiVetContext = _context.LichHens.Include(l => l.BacSiThuY).Include(l => l.DichVu).Include(l => l.KhachHang);
            return View(await thuYcaKoiVetContext.ToListAsync());
        }

        // GET: LichHens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichHen = await _context.LichHens
                .Include(l => l.BacSiThuY)
                .Include(l => l.DichVu)
                .Include(l => l.KhachHang)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lichHen == null)
            {
                return NotFound();
            }

            return View(lichHen);
        }

        // GET: LichHens/Create
        public IActionResult Create()
        {
            ViewData["BacSiThuYid"] = new SelectList(_context.BacSiThuYs, "Id", "Id");
            ViewData["DichVuId"] = new SelectList(_context.DichVus, "Id", "Id");
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "Id", "Id");
            return View();
        }

        // POST: LichHens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KhachHangId,BacSiThuYid,DichVuId,ThoiGian,DiaDiem,TrangThai")] LichHen lichHen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lichHen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BacSiThuYid"] = new SelectList(_context.BacSiThuYs, "Id", "Id", lichHen.BacSiThuYid);
            ViewData["DichVuId"] = new SelectList(_context.DichVus, "Id", "Id", lichHen.DichVuId);
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "Id", "Id", lichHen.KhachHangId);
            return View(lichHen);
        }

        // GET: LichHens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichHen = await _context.LichHens.FindAsync(id);
            if (lichHen == null)
            {
                return NotFound();
            }
            ViewData["BacSiThuYid"] = new SelectList(_context.BacSiThuYs, "Id", "Id", lichHen.BacSiThuYid);
            ViewData["DichVuId"] = new SelectList(_context.DichVus, "Id", "Id", lichHen.DichVuId);
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "Id", "Id", lichHen.KhachHangId);
            return View(lichHen);
        }

        // POST: LichHens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KhachHangId,BacSiThuYid,DichVuId,ThoiGian,DiaDiem,TrangThai")] LichHen lichHen)
        {
            if (id != lichHen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lichHen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LichHenExists(lichHen.Id))
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
            ViewData["BacSiThuYid"] = new SelectList(_context.BacSiThuYs, "Id", "Id", lichHen.BacSiThuYid);
            ViewData["DichVuId"] = new SelectList(_context.DichVus, "Id", "Id", lichHen.DichVuId);
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "Id", "Id", lichHen.KhachHangId);
            return View(lichHen);
        }

        // GET: LichHens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichHen = await _context.LichHens
                .Include(l => l.BacSiThuY)
                .Include(l => l.DichVu)
                .Include(l => l.KhachHang)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lichHen == null)
            {
                return NotFound();
            }

            return View(lichHen);
        }

        // POST: LichHens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lichHen = await _context.LichHens.FindAsync(id);
            if (lichHen != null)
            {
                _context.LichHens.Remove(lichHen);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LichHenExists(int id)
        {
            return _context.LichHens.Any(e => e.Id == id);
        }
    }
}

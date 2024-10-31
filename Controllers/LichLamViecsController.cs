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
    public class LichLamViecsController : Controller
    {
        private readonly ThuYcaKoiVetContext _context;

        public LichLamViecsController(ThuYcaKoiVetContext context)
        {
            _context = context;
        }

        // GET: LichLamViecs
        public async Task<IActionResult> Index()
        {
            var thuYcaKoiVetContext = _context.LichLamViecs.Include(l => l.BacSiThuY);
            return View(await thuYcaKoiVetContext.ToListAsync());
        }

        // GET: LichLamViecs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
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

        // GET: LichLamViecs/Create
        public IActionResult Create()
        {
            ViewData["BacSiThuYid"] = new SelectList(_context.BacSiThuYs, "Id", "Id");
            return View();
        }

        // POST: LichLamViecs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BacSiThuYid,Ngay,GioBatDau,GioKetThuc,TrangThai")] LichLamViec lichLamViec)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lichLamViec);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BacSiThuYid"] = new SelectList(_context.BacSiThuYs, "Id", "Id", lichLamViec.BacSiThuYid);
            return View(lichLamViec);
        }

        // GET: LichLamViecs/Edit/5
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
            ViewData["BacSiThuYid"] = new SelectList(_context.BacSiThuYs, "Id", "Id", lichLamViec.BacSiThuYid);
            return View(lichLamViec);
        }

        // POST: LichLamViecs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BacSiThuYid,Ngay,GioBatDau,GioKetThuc,TrangThai")] LichLamViec lichLamViec)
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
            ViewData["BacSiThuYid"] = new SelectList(_context.BacSiThuYs, "Id", "Id", lichLamViec.BacSiThuYid);
            return View(lichLamViec);
        }

        // GET: LichLamViecs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
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

        // POST: LichLamViecs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
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
            return _context.LichLamViecs.Any(e => e.Id == id);
        }
    }
}

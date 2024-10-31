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
    public class BacSiThuYsController : Controller
    {
        private readonly ThuYcaKoiVetContext _context;

        public BacSiThuYsController(ThuYcaKoiVetContext context)
        {
            _context = context;
        }

        // GET: BacSiThuYs
        public async Task<IActionResult> Index()
        {
            return View(await _context.BacSiThuYs.ToListAsync());
        }

        // GET: BacSiThuYs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bacSiThuY = await _context.BacSiThuYs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bacSiThuY == null)
            {
                return NotFound();
            }

            return View(bacSiThuY);
        }

        // GET: BacSiThuYs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BacSiThuYs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenBacSi,ChuyenMon,SoDienThoai,Email")] BacSiThuY bacSiThuY)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bacSiThuY);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bacSiThuY);
        }

        // GET: BacSiThuYs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bacSiThuY = await _context.BacSiThuYs.FindAsync(id);
            if (bacSiThuY == null)
            {
                return NotFound();
            }
            return View(bacSiThuY);
        }

        // POST: BacSiThuYs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenBacSi,ChuyenMon,SoDienThoai,Email")] BacSiThuY bacSiThuY)
        {
            if (id != bacSiThuY.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bacSiThuY);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BacSiThuYExists(bacSiThuY.Id))
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
            return View(bacSiThuY);
        }

        // GET: BacSiThuYs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bacSiThuY = await _context.BacSiThuYs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bacSiThuY == null)
            {
                return NotFound();
            }

            return View(bacSiThuY);
        }

        // POST: BacSiThuYs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bacSiThuY = await _context.BacSiThuYs.FindAsync(id);
            if (bacSiThuY != null)
            {
                _context.BacSiThuYs.Remove(bacSiThuY);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BacSiThuYExists(int id)
        {
            return _context.BacSiThuYs.Any(e => e.Id == id);
        }
    }
}

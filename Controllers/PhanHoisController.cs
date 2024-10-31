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
    public class PhanHoisController : Controller
    {
        private readonly ThuYcaKoiVetContext _context;

        public PhanHoisController(ThuYcaKoiVetContext context)
        {
            _context = context;
        }

        // GET: PhanHois
        public async Task<IActionResult> Index()
        {
            var thuYcaKoiVetContext = _context.PhanHois.Include(p => p.DichVu).Include(p => p.KhachHang);
            return View(await thuYcaKoiVetContext.ToListAsync());
        }

        // GET: PhanHois/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phanHoi = await _context.PhanHois
                .Include(p => p.DichVu)
                .Include(p => p.KhachHang)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phanHoi == null)
            {
                return NotFound();
            }

            return View(phanHoi);
        }

        // GET: PhanHois/Create
        public IActionResult Create()
        {
            ViewData["DichVuId"] = new SelectList(_context.DichVus, "Id", "Id");
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "Id", "Id");
            return View();
        }

        // POST: PhanHois/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KhachHangId,DichVuId,NoiDung,ThoiGian")] PhanHoi phanHoi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phanHoi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DichVuId"] = new SelectList(_context.DichVus, "Id", "Id", phanHoi.DichVuId);
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "Id", "Id", phanHoi.KhachHangId);
            return View(phanHoi);
        }

        // GET: PhanHois/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phanHoi = await _context.PhanHois.FindAsync(id);
            if (phanHoi == null)
            {
                return NotFound();
            }
            ViewData["DichVuId"] = new SelectList(_context.DichVus, "Id", "Id", phanHoi.DichVuId);
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "Id", "Id", phanHoi.KhachHangId);
            return View(phanHoi);
        }

        // POST: PhanHois/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KhachHangId,DichVuId,NoiDung,ThoiGian")] PhanHoi phanHoi)
        {
            if (id != phanHoi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phanHoi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhanHoiExists(phanHoi.Id))
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
            ViewData["DichVuId"] = new SelectList(_context.DichVus, "Id", "Id", phanHoi.DichVuId);
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "Id", "Id", phanHoi.KhachHangId);
            return View(phanHoi);
        }

        // GET: PhanHois/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phanHoi = await _context.PhanHois
                .Include(p => p.DichVu)
                .Include(p => p.KhachHang)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phanHoi == null)
            {
                return NotFound();
            }

            return View(phanHoi);
        }

        // POST: PhanHois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phanHoi = await _context.PhanHois.FindAsync(id);
            if (phanHoi != null)
            {
                _context.PhanHois.Remove(phanHoi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhanHoiExists(int id)
        {
            return _context.PhanHois.Any(e => e.Id == id);
        }
    }
}

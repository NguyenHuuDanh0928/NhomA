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
    public class LichSuDatDichVusController : Controller
    {
        private readonly ThuYcaKoiVetContext _context;

        public LichSuDatDichVusController(ThuYcaKoiVetContext context)
        {
            _context = context;
        }

        // GET: LichSuDatDichVus
        public async Task<IActionResult> Index()
        {
            var thuYcaKoiVetContext = _context.LichSuDatDichVus.Include(l => l.DichVu).Include(l => l.KhachHang);
            return View(await thuYcaKoiVetContext.ToListAsync());
        }

        // GET: LichSuDatDichVus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichSuDatDichVu = await _context.LichSuDatDichVus
                .Include(l => l.DichVu)
                .Include(l => l.KhachHang)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lichSuDatDichVu == null)
            {
                return NotFound();
            }

            return View(lichSuDatDichVu);
        }

        // GET: LichSuDatDichVus/Create
        public IActionResult Create()
        {
            ViewData["DichVuId"] = new SelectList(_context.DichVus, "Id", "Id");
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "Id", "Id");
            return View();
        }

        // POST: LichSuDatDichVus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KhachHangId,DichVuId,ThoiGian,TrangThai")] LichSuDatDichVu lichSuDatDichVu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lichSuDatDichVu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DichVuId"] = new SelectList(_context.DichVus, "Id", "Id", lichSuDatDichVu.DichVuId);
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "Id", "Id", lichSuDatDichVu.KhachHangId);
            return View(lichSuDatDichVu);
        }

        // GET: LichSuDatDichVus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichSuDatDichVu = await _context.LichSuDatDichVus.FindAsync(id);
            if (lichSuDatDichVu == null)
            {
                return NotFound();
            }
            ViewData["DichVuId"] = new SelectList(_context.DichVus, "Id", "Id", lichSuDatDichVu.DichVuId);
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "Id", "Id", lichSuDatDichVu.KhachHangId);
            return View(lichSuDatDichVu);
        }

        // POST: LichSuDatDichVus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KhachHangId,DichVuId,ThoiGian,TrangThai")] LichSuDatDichVu lichSuDatDichVu)
        {
            if (id != lichSuDatDichVu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lichSuDatDichVu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LichSuDatDichVuExists(lichSuDatDichVu.Id))
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
            ViewData["DichVuId"] = new SelectList(_context.DichVus, "Id", "Id", lichSuDatDichVu.DichVuId);
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "Id", "Id", lichSuDatDichVu.KhachHangId);
            return View(lichSuDatDichVu);
        }

        // GET: LichSuDatDichVus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichSuDatDichVu = await _context.LichSuDatDichVus
                .Include(l => l.DichVu)
                .Include(l => l.KhachHang)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lichSuDatDichVu == null)
            {
                return NotFound();
            }

            return View(lichSuDatDichVu);
        }

        // POST: LichSuDatDichVus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lichSuDatDichVu = await _context.LichSuDatDichVus.FindAsync(id);
            if (lichSuDatDichVu != null)
            {
                _context.LichSuDatDichVus.Remove(lichSuDatDichVu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LichSuDatDichVuExists(int id)
        {
            return _context.LichSuDatDichVus.Any(e => e.Id == id);
        }
    }
}

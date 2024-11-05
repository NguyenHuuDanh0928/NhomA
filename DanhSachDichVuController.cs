using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebDichVu.Datas; 

namespace WebDichVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class DanhSachDichVuController : Controller
    {
        private readonly ThuYcaKoiVetContext _context;

        public DanhSachDichVuController(ThuYcaKoiVetContext context)
        {
            _context = context;
        }

        // GET: admin/DanhSachDichVu
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.DichVus.ToListAsync());
        }

        // GET: admin/DanhSachDichVu/Details/5
        [HttpGet("Details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            var dichVu = await _context.DichVus.FindAsync(id);
            return dichVu == null ? NotFound() : View(dichVu);
        }

        // GET: admin/DanhSachDichVu/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/DanhSachDichVu/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenDichVu,MoTa,Gia,ChiPhiDiChuyen")] DichVu dichVu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(dichVu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi thêm dịch vụ: " + ex.Message);
                    return View(dichVu);
                }
            }
            return View(dichVu);
        }

        // GET: admin/DanhSachDichVu/Edit/5
        [HttpGet("Edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            var dichVu = await _context.DichVus.FindAsync(id);
            return dichVu == null ? NotFound() : View(dichVu);
        }

        // POST: admin/DanhSachDichVu/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DichVu dichVu)
        {
            if (id != dichVu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dichVu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DichVuExists(dichVu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi cập nhật dịch vụ. Vui lòng thử lại.");
                    }
                }
            }

            return View(dichVu);
        }

        // GET: admin/DanhSachDichVu/Delete/5
        [HttpGet("Delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var dichVu = await _context.DichVus.FindAsync(id);
            return dichVu == null ? NotFound() : View(dichVu);
        }

        // POST: admin/DanhSachDichVu/Delete/5
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dichVu = await _context.DichVus.FindAsync(id);
            if (dichVu != null)
            {
                _context.DichVus.Remove(dichVu);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DichVuExists(int id)
        {
            return _context.DichVus.Any(e => e.Id == id);
        }
    }
}

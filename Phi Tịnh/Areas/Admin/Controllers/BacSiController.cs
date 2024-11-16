using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDichVu.Datas;

namespace WebDichVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class BacSiController : Controller
    {
        private readonly ThuYcaKoiVetContext _context;

        public BacSiController(ThuYcaKoiVetContext context)
        {
            _context = context;
        }

        // GET: admin/BacSi
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.BacSiThuYs.ToListAsync());
        }

        // GET: admin/BacSi/Details/5
        [HttpGet("Details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            var bacSiThuY = await _context.BacSiThuYs.FindAsync(id);
            return bacSiThuY == null ? NotFound() : View(bacSiThuY);
        }

        // GET: admin/BacSi/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/BacSi/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenBacSi,ChuyenMon,SoDienThoai,Email")] BacSiThuY bacSiThuY)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(bacSiThuY);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the exception or display a user-friendly error message.
                    ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi thêm bác sĩ: " + ex.Message);
                    return View(bacSiThuY);
                }
            }
            return View(bacSiThuY);
        }

        // GET: admin/BacSi/Edit/5
        [HttpGet("Edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            var bacSiThuY = await _context.BacSiThuYs.FindAsync(id);
            return bacSiThuY == null ? NotFound() : View(bacSiThuY);
        }

        // POST: admin/BacSi/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BacSiThuY bacSiThuY)
        {
            if (id != bacSiThuY.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Chỉ cập nhật các thuộc tính mong muốn:
                try
                {
                    // Cập nhật các thuộc tính được chỉ định
                    if (await TryUpdateModelAsync<BacSiThuY>(bacSiThuY, "",
                        b => b.TenBacSi, b => b.ChuyenMon, b => b.SoDienThoai, b => b.Email))
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BacSiThuYExists(bacSiThuY.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi cập nhật thông tin bác sĩ. Vui lòng thử lại.");
                    }
                }
            }

            // Nếu có lỗi, trả lại view với model để hiển thị lỗi
            return View(bacSiThuY);
        }
        // GET: admin/BacSi/Delete/5
        [HttpGet("Delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var bacSiThuY = await _context.BacSiThuYs.FindAsync(id);
            return bacSiThuY == null ? NotFound() : View(bacSiThuY);
        }

        // POST: admin/BacSi/Delete/5
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid) //Thêm kiểm tra ModelState
            {
                var bacSiThuY = await _context.BacSiThuYs.FindAsync(id);
                if (bacSiThuY != null)
                {
                    _context.BacSiThuYs.Remove(bacSiThuY);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }


        private bool BacSiThuYExists(int id)
        {
            return _context.BacSiThuYs.Any(e => e.Id == id);
        }
    }
}
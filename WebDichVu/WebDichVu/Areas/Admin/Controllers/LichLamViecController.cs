using DichVuThuYService.Datas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDichVu.DichVuThuY.Service;


namespace WebDichVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class LichLamViecController : Controller
    {
        private readonly ILichLamViecService _lichLamViecService;
        private readonly IBacSiThuYService _bacSiService;

        public LichLamViecController(ILichLamViecService lichLamViecService, IBacSiThuYService bacSiService)
        {
            _lichLamViecService = lichLamViecService;
            _bacSiService = bacSiService;
        }

        // GET: admin/LichLamViec
        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            return View(await _lichLamViecService.GetAllAsync());
        }

        // GET: admin/LichLamViec/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            var lichLamViec = await _lichLamViecService.GetByIdAsync(id.GetValueOrDefault());
            return lichLamViec == null ? NotFound() : View(lichLamViec);
        }

        // GET: admin/LichLamViec/Create
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["BacSiThuYid"] = new SelectList(await _bacSiService.GetAllAsync(), "Id", "TenBacSi");
            return View();
        }

        // POST: admin/LichLamViec/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LichLamViec lichLamViec)
        {
            var bacSiList = await _bacSiService.GetAllAsync();
            ViewData["BacSiThuYid"] = new SelectList(bacSiList, "Id", "TenBacSi", lichLamViec.BacSiThuYid);

            if (ModelState.IsValid)
            {
                try
                {
                    await _lichLamViecService.AddAsync(lichLamViec);
                    TempData["SuccessMessage"] = "Lịch làm việc đã được tạo thành công.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Lỗi khi tạo lịch làm việc: " + ex.Message;
                    // Vẫn cần đặt lại ViewData trong trường hợp lỗi
                    ViewData["BacSiThuYid"] = new SelectList(bacSiList, "Id", "TenBacSi", lichLamViec.BacSiThuYid);
                    return View(lichLamViec); // Trả về view để hiển thị lỗi
                }
            }

            // Trường hợp ModelState không hợp lệ
            return View(lichLamViec); // Trả về view với ModelState và ViewData
        }

        // GET: admin/LichLamViec/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            var lichLamViec = await _lichLamViecService.GetByIdAsync(id.GetValueOrDefault());
            if (lichLamViec == null) return NotFound();

            ViewData["BacSiThuYid"] = new SelectList(await _bacSiService.GetAllAsync(), "Id", "TenBacSi", lichLamViec.BacSiThuYid);
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

            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                         .Select(e => e.ErrorMessage);
                    TempData["ErrorMessage"] = string.Join("; ", errors);

                    return RedirectToAction(nameof(Index)); // Redirect nếu ModelState không hợp lệ
                }

                await _lichLamViecService.UpdateAsync(lichLamViec);
                TempData["SuccessMessage"] = "Cập nhật lịch làm việc thành công.";

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Cập nhật lịch làm việc thất bại: " + ex.Message;
            }


            return RedirectToAction(nameof(Index)); // Luôn redirect về Index
        }
        // GET: admin/LichLamViec/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var lichLamViec = await _lichLamViecService.GetByIdAsync(id.GetValueOrDefault());
            return lichLamViec == null ? NotFound() : View(lichLamViec);

        }

        // POST: admin/LichLamViec/Delete/5
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _lichLamViecService.DeleteAsync(id);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Xóa lịch làm việc thất bại!" + ex.Message);
                    var lichLamViec = await _lichLamViecService.GetByIdAsync(id);

                    return View(lichLamViec);
                }

            }
            return RedirectToAction("Index");

        }

        private async Task<bool> LichLamViecExists(int id)
        {
            return await _lichLamViecService.ExistsAsync(id); // Gọi service
        }
    }
}
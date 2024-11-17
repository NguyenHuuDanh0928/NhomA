using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDichVu.DichVuThuY.Service;
using DichVuThuYService.Datas;

namespace WebDichVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class BacSiController : Controller
    {
        private readonly IBacSiThuYService _bacSiService; // Inject Service

        public BacSiController(IBacSiThuYService bacSiService)
        {
            _bacSiService = bacSiService;
        }

        // GET: admin/BacSi
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {

            var bacSiList = await _bacSiService.GetAllAsync(); // Giả sử bạn có phương thức này để lấy danh sách bác sĩ
            return View(bacSiList);
        }

        // GET: admin/BacSi/Details/5
        [HttpGet("Details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            var bacSiThuY = await _bacSiService.GetByIdAsync(id.GetValueOrDefault());

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
        public async Task<IActionResult> Create(BacSiThuY bacSiThuY)
        {
            if (!ModelState.IsValid)
            {
                return View(bacSiThuY); // Trả về view nếu ModelState không hợp lệ
            }

            try
            {
                await _bacSiService.AddAsync(bacSiThuY);
                TempData["SuccessMessage"] = "Thêm bác sĩ thành công.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Xử lý exception và đặt thông báo lỗi chung chung vào TempData
                TempData["ErrorMessage"] = "Thêm bác sĩ thất bại: " + ex.Message; // Hoặc xử lý lỗi chi tiết hơn nếu cần
                return RedirectToAction(nameof(Index)); // Redirect về Index để hiển thị thông báo
            }
        }

        // GET: admin/BacSi/Edit/5
        [HttpGet("Edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bacSiThuY = await _bacSiService.GetByIdAsync(id.Value);
            if (bacSiThuY == null)
            {
                return NotFound();
            }
            return View(bacSiThuY);
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
                try
                {
                    await _bacSiService.UpdateAsync(bacSiThuY);
                    // Chỉ định rõ Area khi chuyển hướng
                    return RedirectToAction("Index", "BacSi", new { area = "Admin" });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                }
            }
            return View(bacSiThuY);
        }
        // GET: admin/BacSi/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bacSiThuY = await _bacSiService.GetByIdAsync(id);
            return bacSiThuY == null ? NotFound() : View(bacSiThuY);
        }

        // POST: admin/BacSi/Delete/5
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBacSiConfirmed(int id)
        {
            try
            {
                await _bacSiService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Xóa bác sĩ thành công";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Xóa bác sĩ thất bại: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
        private bool BacSiThuYExists(int id)
        {
            return _bacSiService.Exists(id);
        }
    }
}
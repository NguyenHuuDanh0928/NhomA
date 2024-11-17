using DichVuThuY.Service;
using DichVuThuYService.Datas;
using Microsoft.AspNetCore.Mvc;
using DichVuThuYService.Interfaces;
using System.Threading.Tasks;

namespace WebDichVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class DanhSachDichVuController : Controller
    {
        private readonly IDanhSachDichVuService _danhSachDichVuService;

        public DanhSachDichVuController(IDanhSachDichVuService danhSachDichVuService)
        {
            _danhSachDichVuService = danhSachDichVuService;
        }

        // GET: Admin/DanhSachDichVu
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var dichVuList = await _danhSachDichVuService.GetAllAsync();
            return View(dichVuList);
        }

        // GET: Admin/DanhSachDichVu/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var dichVu = await _danhSachDichVuService.GetByIdAsync(id);
            if (dichVu == null)
            {
                TempData["Error"] = "Không tìm thấy dịch vụ.";
                return RedirectToAction("Index");
            }
            return View(dichVu);
        }

        // GET: Admin/DanhSachDichVu/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/DanhSachDichVu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DichVu model)
        {
            if (ModelState.IsValid)
            {
                await _danhSachDichVuService.AddDichVuAsync(model); // Call the service to add the new service to the database
                return RedirectToAction("Index"); // Redirect to the list of services
            }

            return View(model); // If validation fails, return to the Create view with the model
        }

        // GET: Admin/DanhSachDichVu/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var dichVu = await _danhSachDichVuService.GetByIdAsync(id);
            if (dichVu == null)
            {
                TempData["Error"] = "Không tìm thấy dịch vụ.";
                return RedirectToAction("Index");
            }
            return View(dichVu);
        }

        // POST: Admin/DanhSachDichVu/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DichVu dichVu)
        {
            if (id != dichVu.Id)
            {
                TempData["Error"] = "Dịch vụ không hợp lệ.";
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View(dichVu);
            }

            try
            {
                await _danhSachDichVuService.UpdateAsync(dichVu);
                TempData["Message"] = "Cập nhật dịch vụ thành công.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
                return View(dichVu);
            }
        }

        // GET: Admin/DanhSachDichVu/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dichVu = await _danhSachDichVuService.GetByIdAsync(id);
            if (dichVu == null)
            {
                TempData["Error"] = "Không tìm thấy dịch vụ.";
                return RedirectToAction("Index");
            }
            return View(dichVu);
        }

        // POST: Admin/DanhSachDichVu/Delete/5
        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _danhSachDichVuService.DeleteAsync(id);
                TempData["Message"] = "Xóa dịch vụ thành công.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Xóa dịch vụ thất bại: {ex.Message}";
                return RedirectToAction("Delete", new { id });
            }
        }

        // Kiểm tra xem dịch vụ có tồn tại hay không
        private async Task<bool> DichVuExistsAsync(int id)
        {
            return await _danhSachDichVuService.ExistsAsync(id);
        }
    }
}

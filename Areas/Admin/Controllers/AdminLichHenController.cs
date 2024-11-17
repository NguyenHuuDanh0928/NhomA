using Microsoft.AspNetCore.Mvc;
using DichVuThuYService.Interfaces;
using System.Threading.Tasks;

namespace WebDichVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class AdminLichHenController : Controller
    {
        private readonly ILichHenService _lichHenService;

        public AdminLichHenController(ILichHenService lichHenService)
        {
            _lichHenService = lichHenService;
        }

        // GET: /Admin/AdminLichHen/Index
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            // Lấy danh sách lịch hẹn từ service và truyền vào view
            var lichHens = await _lichHenService.GetAllLichHenAsync();
            return View(lichHens);
        }

        // POST: /Admin/AdminLichHen/Approve/{id}
        [HttpPost("Approve/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            // Kiểm tra xem lịch hẹn có tồn tại và trạng thái là "Chưa duyệt"
            var lichHen = await _lichHenService.GetAllLichHenAsync(id);
            if (lichHen == null)
            {
                TempData["Error"] = "Lịch hẹn không tồn tại.";
                return RedirectToAction("Index");
            }

            // Duyệt lịch hẹn qua service
            await _lichHenService.ApproveLichHenAsync(id);

            TempData["Message"] = "Lịch hẹn đã được phê duyệt.";
            return RedirectToAction("Index");
        }
    }
}

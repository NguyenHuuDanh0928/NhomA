using DichVuThuYRepository.ViewModel;
using DichVuThuYService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebDichVu.Controllers
{
    public class LichHenController : Controller
    {
        private readonly ILichHenService _lichHenService;

        public LichHenController(ILichHenService lichHenService)
        {
            _lichHenService = lichHenService;
        }

        // GET: /LichHen/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new LichHenViewModel
            {
                // Gán danh sách bác sĩ và dịch vụ để khách hàng chọn
                BacSiThuYList = await _lichHenService.GetBacSiThuYListAsync(),
                DichVuList = await _lichHenService.GetDichVuListAsync()
            };
            return View(model);
        }

        // POST: /LichHen/Create
        [HttpPost]
        public async Task<IActionResult> Create(LichHenViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _lichHenService.CreateLichHenAsync(model);
                return RedirectToAction("Success"); // Chuyển hướng đến trang thành công sau khi tạo lịch hẹn
            }

            // Nếu model không hợp lệ, tải lại danh sách và trả lại view
            model.BacSiThuYList = await _lichHenService.GetBacSiThuYListAsync();
            model.DichVuList = await _lichHenService.GetDichVuListAsync();
            return View(model);
        }

        public IActionResult Success()
        {
            return View(); // Trang xác nhận khi lịch hẹn được đặt thành công
        }
    }
}

using DichVuThuYService.Interfaces;
using DichVuThuYRepository.ViewModel;
using DichVuThuYService.Datas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebDichVu.Controllers
{
    [Route("[controller]")]  // Thêm Route attribute
    public class PhanHoiController : Controller
    {
        private readonly IPhanHoiService _phanHoiService;

        public PhanHoiController(IPhanHoiService phanHoiService)
        {
            _phanHoiService = phanHoiService;
        }

        // GET: /PhanHoi
        [HttpGet]
        [Route("Index")]  // Route mặc định
        public async Task<IActionResult> Index()
        {
            var model = new PhanHoiViewModel
            {
                DichVuList = await _phanHoiService.GetDichVuListAsync()
            };
            return View(model);
        }

        // POST: /PhanHoi
        [HttpPost]
        [Route("Index")]  // Route mặc định cho POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PhanHoiViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var phanHoi = new PhanHoi
                    {
                        KhachHangId = model.KhachHangId,
                        DichVuId = model.DichVuId,
                        NoiDung = model.NoiDung,
                        ThoiGian = DateTime.Now
                    };

                    await _phanHoiService.AddPhanHoiAsync(phanHoi);
                    return RedirectToAction("DanhGiaThanhCong");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi: " + ex.Message);
                }
            }

            // Nếu có lỗi, tải lại danh sách dịch vụ
            model.DichVuList = await _phanHoiService.GetDichVuListAsync();
            return View(model);
        }

        // GET: /PhanHoi/DanhGiaThanhCong
        [HttpGet]
        [Route("DanhGiaThanhCong")]  // Route cho trang thành công
        public IActionResult DanhGiaThanhCong()
        {
            return View();
        }
    }
}
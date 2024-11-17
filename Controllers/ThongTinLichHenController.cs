using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DichVuThuYService.Interfaces;
using System.Linq;
using DichVuThuYRepository.ViewModel;

namespace WebDichVu.Controllers
{
    [Authorize] // Yêu cầu phải đăng nhập để truy cập Controller
    public class ThongTinLichHenController : Controller
    {
        private readonly IThongTinLichHenService _service;

        public ThongTinLichHenController(IThongTinLichHenService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Kiểm tra khách hàng đã đăng nhập chưa
            var khachHangId = HttpContext.Session.GetInt32("KhachHangId");
            if (khachHangId == null)
            {
                // Chuyển hướng đến trang đăng nhập nếu chưa đăng nhập
                return RedirectToAction("DangNhap", "KhachHang", new { returnUrl = "/ThongTinLichHen/Index" });
            }

            // Lấy danh sách lịch hẹn
            var lichHenList = _service.GetLichHenByKhachHangId(khachHangId.Value)
                .Select(lh => new ThongTinLichHenViewModel
                {
                    Id = lh.Id,
                    TenKhachHang = lh.KhachHang.TenKhachHang,
                    TenBacSi = lh.BacSiThuY.TenBacSi,
                    TenDichVu = lh.DichVu.TenDichVu,
                    ThoiGian = lh.ThoiGian,
                    DiaDiem = lh.DiaDiem,
                    TrangThai = lh.TrangThai
                }).ToList();

            // Gán Layout `_LayoutProfile.cshtml` cho View này
            ViewData["Layout"] = "~/Views/Shared/_LayoutProfile.cshtml";
            return View(lichHenList);
        }
    }
}

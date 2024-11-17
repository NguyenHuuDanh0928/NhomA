using Microsoft.AspNetCore.Mvc;
using DichVuThuYService.Areas.Admin.Models;

namespace DichVuThuYService.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize] // Áp dụng bộ lọc yêu cầu đăng nhập
    public class HomeAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

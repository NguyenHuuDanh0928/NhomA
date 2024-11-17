using DichVuThuYService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebDichVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class AdminPhanHoiController : Controller
    {
        private readonly IPhanHoiService _phanHoiService;

        public AdminPhanHoiController(IPhanHoiService phanHoiService)
        {
            _phanHoiService = phanHoiService;
        }

        // GET: /Admin/AdminPhanHoi
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            // Lấy danh sách phản hồi từ Service
            var phanHois = await _phanHoiService.GetAllPhanHoiAsync();
            return View(phanHois); // Truyền dữ liệu đến View
        }
    }
}

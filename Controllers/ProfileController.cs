using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DichVuThuYService.Interfaces; // Sử dụng Interface của ProfileService
using DichVuThuYRepository.Models;
using System.Threading.Tasks;

namespace DichVuThuYService.Controllers
{
    [Authorize] // Yêu cầu xác thực
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService) // Sử dụng Interface IProfileService
        {
            _profileService = profileService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

            var profile = _profileService.GetUserProfile(userId);
            if (profile == null)
            {
                return NotFound("Không tìm thấy hồ sơ người dùng.");
            }

            return View("Update", profile); // Trả về view Update.cshtml với thông tin hồ sơ
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserProfile updatedProfile)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", updatedProfile);
            }

            bool isUpdated = await _profileService.UpdateUserProfile(updatedProfile);

            if (!isUpdated)
            {
                ModelState.AddModelError("", "Cập nhật hồ sơ thất bại.");
                return View("Update", updatedProfile);
            }

            return RedirectToAction("Index");
        }
    }
}

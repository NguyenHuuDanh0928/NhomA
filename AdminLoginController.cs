using Microsoft.AspNetCore.Mvc;

namespace WebDichVu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "password123") // Thay thế bằng tài khoản và mật khẩu của bạn
            {
                HttpContext.Session.SetString("AdminLoggedIn", "true");
                return RedirectToAction("Index", "HomeAdmin");
            }

            ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminLoggedIn");
            return RedirectToAction("Login");
        }
    }
}

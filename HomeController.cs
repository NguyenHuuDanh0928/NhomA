using Microsoft.AspNetCore.Mvc;

namespace WebDichVu.Models
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

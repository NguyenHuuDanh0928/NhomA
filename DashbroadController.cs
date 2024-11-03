using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KoiCareService.Models;

namespace YourNamespace.Controllers
{
    public class DashboardController : Controller
    {
        private readonly KoiCareContext _context;

        public DashboardController(KoiCareContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Thống kê số liệu cơ bản
            var totalCustomers = await _context.Customers.CountAsync();
            var totalDoctors = await _context.Doctors.CountAsync();
            var totalServices = await _context.Services.CountAsync();
            var totalBookings = await _context.Bookings.CountAsync();

            // Tạo đối tượng ViewModel để truyền dữ liệu sang View
            var dashboardData = new DashboardViewModel
            {
                TotalCustomers = totalCustomers,
                TotalDoctors = totalDoctors,
                TotalServices = totalServices,
                TotalBookings = totalBookings
            };

            return View(dashboardData);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPI;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private static List<Service> services = new List<Service>
        {
            new Service { Id = 1, Name = "Consultation", Price = 30m, Description = "General consultation" }
        };

        // Lấy danh sách dịch vụ (GET)
        [HttpGet]
        public ActionResult<IEnumerable<Service>> GetServices()
        {
            return services;
        }

        // Thêm mới dịch vụ (POST)
        [HttpPost]
        public ActionResult<Service> AddService(Service service)
        {
            service.Id = services.Count + 1;
            services.Add(service);
            return service;
        }

        // Cập nhật dịch vụ (PUT)
        [HttpPut("{id}")]
        public IActionResult UpdateService(int id, Service updatedService)
        {
            var service = services.Find(s => s.Id == id);
            if (service == null)
                return NotFound();

            service.Name = updatedService.Name;
            service.Price = updatedService.Price;
            service.Description = updatedService.Description;
            return NoContent();
        }

        // Xóa dịch vụ (DELETE)
        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            var service = services.Find(s => s.Id == id);
            if (service == null)
                return NotFound();

            services.Remove(service);
            return NoContent();
        }
    }
}

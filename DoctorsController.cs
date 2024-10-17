using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPI;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        // Danh sách giả lập các bác sĩ
        private static List<Doctor> doctors = new List<Doctor>
        {
            new Doctor { Id = 1, Name = "Dr. John", Specialty = "Neurology", ContactInfo = "123-456-7890" }
        };

        // Lấy danh sách bác sĩ (GET)
        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> GetDoctors()
        {
            return doctors;
        }

        // Thêm mới bác sĩ (POST)
        [HttpPost]
        public ActionResult<Doctor> AddDoctor(Doctor doctor)
        {
            doctor.Id = doctors.Count + 1;
            doctors.Add(doctor);
            return doctor;
        }

        // Cập nhật thông tin bác sĩ (PUT)
        [HttpPut("{id}")]
        public IActionResult UpdateDoctor(int id, Doctor updatedDoctor)
        {
            var doctor = doctors.Find(d => d.Id == id);
            if (doctor == null)
                return NotFound();

            doctor.Name = updatedDoctor.Name;
            doctor.Specialty = updatedDoctor.Specialty;
            doctor.ContactInfo = updatedDoctor.ContactInfo;
            return NoContent();
        }

        // Xóa bác sĩ (DELETE)
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            var doctor = doctors.Find(d => d.Id == id);
            if (doctor == null)
                return NotFound();

            doctors.Remove(doctor);
            return NoContent();
        }
    }
}

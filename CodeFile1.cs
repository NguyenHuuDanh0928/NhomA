using Microsoft.AspNetCore.Mvc;
using WebDichVu.Models;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace WebDichVu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private static List<Appointment> appointments = new List<Appointment>();
        private static List<Doctor> doctors = new List<Doctor>
        {
            new Doctor { Id = 1, Name = "Alice Smith", Specialty = "Cardiology" },
            new Doctor { Id = 2, Name = "Bob Johnson", Specialty = "Dermatology" }
        };

        private static List<Service> services = new List<Service>
        {
            new Service { Id = 1, ServiceName = "Consultation", Duration = 30 },
            new Service { Id = 2, ServiceName = "Skin Check", Duration = 45 }
        };

        [HttpPost]
        public IActionResult ScheduleAppointment(Appointment appointment)
        {
            appointment.Id = appointments.Count + 1; // Simple ID assignment
            appointments.Add(appointment);
            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        [HttpGet("{id}")]
        public ActionResult<Appointment> GetAppointment(int id)
        {
            var appointment = appointments.FirstOrDefault(a => a.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return appointment;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Appointment>> GetAppointments()
        {
            return appointments;
        }
    }
}
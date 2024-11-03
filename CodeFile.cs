using Microsoft.AspNetCore.Mvc;

public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpPost]
    public async Task<IActionResult> BookAppointment([FromBody] AppointmentDto appointmentDto)
    {
        var appointment = await _appointmentService.BookAppointmentAsync(appointmentDto);
        if (appointment == null)
        {
            return BadRequest("Appointment booking failed.");
        }

        return Ok(appointment);
    }
}
using Microsoft.AspNetCore.Mvc;
using OdontoCore.Application.Abstractions;
using OdontoCore.Application.Appointments.ScheduleAppointment;

namespace OdontoCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController: ControllerBase
    {
        private readonly ScheduleAppointmentHandler handler;

        public AppointmentsController(ScheduleAppointmentHandler handler)
        {
            this.handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> Schedule([FromBody] ScheduleAppointmentCommand command,
            CancellationToken ct)
        {
            Guid appointmentId = await handler.HandleAsync(command, ct);

            return Ok(new { Id = appointmentId });
        }

    }
}

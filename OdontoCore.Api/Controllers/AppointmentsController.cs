using Microsoft.AspNetCore.Mvc;
using OdontoCore.Application.Abstractions;
using OdontoCore.Application.Appointments.GetAppointmentById;
using OdontoCore.Application.Appointments.ScheduleAppointment;

namespace OdontoCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController: ControllerBase
    {
        private readonly ScheduleAppointmentHandler handler;
        private readonly GetAppointmentByIdHandler getByIdHandler;

        public AppointmentsController(ScheduleAppointmentHandler handler, GetAppointmentByIdHandler getByIdHandler)
        {
            this.handler = handler;
            this.getByIdHandler = getByIdHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Schedule([FromBody] ScheduleAppointmentCommand command,
            CancellationToken ct)
        {
            Guid appointmentId = await handler.HandleAsync(command, ct);

            return CreatedAtAction(nameof(GetById), new { id = appointmentId }, new { id = appointmentId });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            AppointmentResponse appointment = await getByIdHandler.HandleAsync(id, ct);

            return Ok(appointment);
        }

    }
}

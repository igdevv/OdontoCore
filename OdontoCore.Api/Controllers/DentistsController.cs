using Microsoft.AspNetCore.Mvc;
using OdontoCore.Application.Appointments.GetAppointmentById;
using OdontoCore.Application.Appointments.GetDentistSchedule;

namespace OdontoCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DentistsController: ControllerBase
    {
        private readonly GetDentistScheduleHandler scheduleHandler;

        public DentistsController(GetDentistScheduleHandler scheduleHandler)
        {
            this.scheduleHandler = scheduleHandler;
        }

        [HttpGet("{dentistId:guid/appointments}")]
        public async Task<ActionResult<IReadOnlyList<AppointmentResponse>>> GetSchedule(Guid dentistId, [FromQuery]
        DateOnly date, CancellationToken ct)
        {
            var appointments = await scheduleHandler.HandleAsync(dentistId, date, ct);

            return Ok(appointments);
        }
    }
}

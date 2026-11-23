using OdontoCore.Application.Abstractions;
using OdontoCore.Application.Appointments.GetAppointmentById;
using OdontoCore.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Application.Appointments.GetDentistSchedule
{
    public sealed class GetDentistScheduleHandler
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IDentistRepository dentistRepository;

        public GetDentistScheduleHandler(IAppointmentRepository appointmentRepository, IDentistRepository dentistRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.dentistRepository = dentistRepository;
        }

        public async Task<IReadOnlyList<AppointmentResponse>> HandleAsync(Guid dentistId, DateOnly date, CancellationToken
            ct = default)
        {
            var dentist = await dentistRepository.GetByIdAsync(dentistId, ct);

            if (dentist is null || !dentist.IsActive)
            {
                throw new ResourceNotFoundException("Dentist");
            }

            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Santo_Domingo");

            DateTime localStart = date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Unspecified);

            DateTime localEnd = localStart.AddDays(1);

            DateTime startsAtUtc = TimeZoneInfo.ConvertTimeToUtc(localStart, timeZone);

            DateTime endsAtUtc = TimeZoneInfo.ConvertTimeToUtc(localEnd, timeZone);

            var appointments = await appointmentRepository.GetScheduleAsync(dentistId, startsAtUtc,
                endsAtUtc, ct);

            return appointments
                .Select(a => new AppointmentResponse(
                    a.Id,
                    a.PatientId,
                    a.DentistId,
                    a.DentalServiceId,
                    a.StartsAt,
                    a.EndsAt,
                    a.AgreedPrice,
                    a.Status))
                .ToList();
        }
    }
}

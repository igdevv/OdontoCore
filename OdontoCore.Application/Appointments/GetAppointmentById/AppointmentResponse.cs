using OdontoCore.Domain.Entities;
using OdontoCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Application.Appointments.GetAppointmentById
{
    public sealed record AppointmentResponse(
        Guid Id,
        Guid PatientId,
        Guid DentistId,
        Guid DentalServiceId,
        DateTime StartsAt,
        DateTime EndsAt,
        decimal AgreedPrice,
        AppointmentStatus Status);
}

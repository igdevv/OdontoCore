using OdontoCore.Application.Abstractions;
using OdontoCore.Application.Exceptions;
using OdontoCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Application.Appointments.GetAppointmentById
{
    public sealed class GetAppointmentByIdHandler
    {
        private readonly IAppointmentRepository appointmentRepository;

        public GetAppointmentByIdHandler(IAppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }

        public async Task<AppointmentResponse> HandleAsync(Guid id, CancellationToken ct)
        {
            Appointment? appointment = await appointmentRepository.GetByIdAsync(id, ct);

            if (appointment is null)
            {
                throw new ResourceNotFoundException("Appointment");
            }

            return new AppointmentResponse(
                appointment.Id,
                appointment.PatientId,
                appointment.DentistId,
                appointment.DentalServiceId,
                appointment.StartsAt,
                appointment.EndsAt,
                appointment.AgreedPrice,
                appointment.Status);
        }
    }
}

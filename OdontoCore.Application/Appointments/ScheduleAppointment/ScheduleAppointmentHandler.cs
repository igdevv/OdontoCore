using OdontoCore.Application.Abstractions;
using OdontoCore.Application.Exceptions;
using OdontoCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Application.Appointments.ScheduleAppointment
{
    public class ScheduleAppointmentHandler
    {
        private readonly IPatientRepository patientRepository;
        private readonly IDentistRepository dentistRepository;
        private readonly IDentalServiceRepository dentalServiceRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IUnitOfWork unitOfWork;

        public ScheduleAppointmentHandler(IPatientRepository patientRepository, IDentistRepository dentistRepository,
            IDentalServiceRepository dentalServiceRepository, IAppointmentRepository appointmentRepository, IUnitOfWork
            unitOfWork)
        {
            this.patientRepository = patientRepository;
            this.dentistRepository = dentistRepository;
            this.dentalServiceRepository = dentalServiceRepository;
            this.appointmentRepository = appointmentRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Guid> HandleAsync(ScheduleAppointmentCommand command, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(command);

            Patient? patient = await patientRepository.GetByIdAsync(command.PatientId, ct);

            if (patient is null || !patient.IsActive)
            {
                throw new ResourceNotFoundException("Patient");
            }

            Dentist? dentist = await dentistRepository.GetByIdAsync(command.DentistId, ct);

            if (dentist is null || !dentist.IsActive)
            {
                throw new ResourceNotFoundException("Dentist");
            }

            DentalService? dentalService = await dentalServiceRepository.GetByIdAsync(command.DentalServiceId, ct);

            if (dentalService is null || !dentalService.IsActive)
            {
                throw new ResourceNotFoundException("Dental service");
            }

            if (command.StartsAt.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Start time must be UTC.", nameof(command));
            }

            DateTime endsAt = command.StartsAt.AddMinutes(dentalService.DurationMinutes);

            bool hasConflict = await appointmentRepository.HasConflictAsync(dentist.Id, command.StartsAt,
                endsAt, ct);

            if (hasConflict)
            {
                throw new AppointmentConflictException();
            }

            Appointment appointment = new Appointment(
                patient.Id,
                dentist.Id,
                dentalService.Id,
                command.StartsAt,
                dentalService.DurationMinutes,
                dentalService.Price);

            await appointmentRepository.AddAsync(appointment, ct);

            await unitOfWork.SaveChangesAsync(ct);

            return appointment.Id;
        }
    }
}

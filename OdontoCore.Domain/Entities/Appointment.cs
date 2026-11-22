using OdontoCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Domain.Entities
{
    public class Appointment
    {
        public Guid Id { get; private set; }
        public Guid PatientId { get; private set; }
        public Guid DentistId { get; private set; }
        public Guid DentalServiceId { get; private set; }

        public DateTime StartsAt { get; private set; }
        public DateTime EndsAt { get; private set; }

        public decimal AgreedPrice { get; private set; }

        public AppointmentStatus Status { get; private set; }

        private Appointment() { }
        public Appointment(Guid patientId, Guid dentistId, Guid dentalServiceId, DateTime startsAt, int durationMinutes,
            decimal agreedPrice)
        {
            if (patientId == Guid.Empty)
            {
                throw new ArgumentException("Patient is required.", nameof(patientId));
            }

            if (dentistId == Guid.Empty)
            {
                throw new ArgumentException("Dentist is required.", nameof(dentistId));
            }

            if (dentalServiceId == Guid.Empty)
            {
                throw new ArgumentException("Dental service is required.", nameof(dentalServiceId));
            }

            if (startsAt.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Start time must be UTC.", nameof(startsAt));
            }

            if (durationMinutes <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(durationMinutes), "Duration must be greater than zero.");
            }

            if (agreedPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(agreedPrice), "Agreed price cannot be negative.");
            }

            if (decimal.Round(agreedPrice, 2) != agreedPrice)
            {
                throw new ArgumentException("Agreed price cannot have more than two decimal places.", nameof(agreedPrice));
            }

            Id = Guid.NewGuid();
            PatientId = patientId;
            DentistId = dentistId;
            DentalServiceId = dentalServiceId;

            StartsAt = startsAt;
            EndsAt = startsAt.AddMinutes(durationMinutes);

            AgreedPrice = agreedPrice;
            Status = AppointmentStatus.Scheduled;
        }

        public void Cancel()
        {
            if (Status != AppointmentStatus.Scheduled)
            {
                throw new InvalidOperationException("Only scheduled appointments can be cancelled.");
            }

            Status = AppointmentStatus.Cancelled;
        }

        public void Complete()
        {
            if (Status != AppointmentStatus.Scheduled)
            {
                throw new InvalidOperationException("Only scheduled appointments can be completed.");

            }

            Status = AppointmentStatus.Completed;
        }

        public bool OverlapsWith(Appointment other)
        {
            ArgumentNullException.ThrowIfNull(other);

            if (DentistId != other.DentistId)
                return false;

            if (Status == AppointmentStatus.Cancelled || other.Status == AppointmentStatus.Cancelled)
                return false;

            return StartsAt < other.EndsAt && EndsAt > other.StartsAt;
        }

        public void Reschedule(DateTime newStartsAt)
        {
            if (Status != AppointmentStatus.Scheduled)
            {
                throw new InvalidOperationException("Only scheduled appointments can be rescheduled.");
            }

            if (newStartsAt.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Start time must be UTC.", nameof(newStartsAt));
            }

            TimeSpan duration = EndsAt - StartsAt;

            DateTime newEndsAt = newStartsAt.Add(duration);

            StartsAt = newStartsAt;
            EndsAt = newEndsAt;
        }
    }
}

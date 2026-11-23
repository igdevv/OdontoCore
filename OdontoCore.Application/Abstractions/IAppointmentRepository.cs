using OdontoCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Application.Abstractions
{
    public interface IAppointmentRepository
    {
        Task<bool> HasConflictAsync(Guid dentistId, DateTime startsAt, DateTime endsAt, CancellationToken ct = default);

        Task AddAsync(Appointment appointment, CancellationToken ct = default);
        Task<Appointment?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<Appointment>> GetScheduleAsync(Guid dentistId, DateTime startsAtUtc, DateTime endsAtUtc,
            CancellationToken ct = default);
    }
}

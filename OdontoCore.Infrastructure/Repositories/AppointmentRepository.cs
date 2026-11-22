using Microsoft.EntityFrameworkCore;
using OdontoCore.Application.Abstractions;
using OdontoCore.Domain.Entities;
using OdontoCore.Domain.Enums;
using OdontoCore.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Infrastructure.Repositories
{
    public sealed class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext dbContext;

        public AppointmentRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(Appointment appointment, CancellationToken ct = default)
        {
            await dbContext.Appointments.AddAsync(appointment, ct);
        }

        public async Task<bool> HasConflictAsync(Guid dentistId, DateTime startsAt, DateTime endsAt, CancellationToken ct = default)
        {
            return await dbContext.Appointments
                .AsNoTracking()
                .AnyAsync(appointment =>
                    appointment.DentistId == dentistId &&
                    appointment.Status != AppointmentStatus.Cancelled &&
                    appointment.StartsAt < endsAt &&
                    appointment.EndsAt > startsAt, ct);
        }
    }
}

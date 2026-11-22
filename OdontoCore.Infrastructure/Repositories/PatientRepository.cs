using Microsoft.EntityFrameworkCore;
using OdontoCore.Application.Abstractions;
using OdontoCore.Domain.Entities;
using OdontoCore.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Infrastructure.Repositories
{
    public sealed class PatientRepository: IPatientRepository
    {
        private readonly AppDbContext dbContext;

        public PatientRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Patient?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await dbContext.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(patient => patient.Id == id, ct);
        }
    }
}

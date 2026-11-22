using Microsoft.EntityFrameworkCore;
using OdontoCore.Application.Abstractions;
using OdontoCore.Domain.Entities;
using OdontoCore.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Infrastructure.Repositories
{
    public sealed class DentistRepository : IDentistRepository
    {
        private readonly AppDbContext dbContext;

        public DentistRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Dentist?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await dbContext.Dentists
                .AsNoTracking()
                .FirstOrDefaultAsync(dentist => dentist.Id == id, ct);
        }
    }
}

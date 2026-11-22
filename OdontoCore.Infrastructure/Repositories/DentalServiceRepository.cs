using Microsoft.EntityFrameworkCore;
using OdontoCore.Application.Abstractions;
using OdontoCore.Domain.Entities;
using OdontoCore.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Infrastructure.Repositories
{
    public sealed class DentalServiceRepository : IDentalServiceRepository
    {
        private readonly AppDbContext dbContext;

        public DentalServiceRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<DentalService?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await dbContext.DentalServices
                .AsNoTracking()
                .FirstOrDefaultAsync(service => service.Id == id, ct);
        }
    }
}

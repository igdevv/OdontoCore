using OdontoCore.Application.Abstractions;
using OdontoCore.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Infrastructure.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return dbContext.SaveChangesAsync(ct);
        }
    }
}

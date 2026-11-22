using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Application.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}

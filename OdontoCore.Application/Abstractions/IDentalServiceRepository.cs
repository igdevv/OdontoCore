using OdontoCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Application.Abstractions
{
    public interface IDentalServiceRepository
    {
        Task<DentalService?> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}

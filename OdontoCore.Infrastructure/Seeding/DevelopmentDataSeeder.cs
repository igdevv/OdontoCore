using Microsoft.EntityFrameworkCore;
using OdontoCore.Domain.Entities;
using OdontoCore.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Infrastructure.Seeding
{
    public static class DevelopmentDataSeeder
    {
        public static async Task SeedAsync(AppDbContext dbContext, CancellationToken ct = default)
        {
            if (!await dbContext.Patients.AnyAsync(ct))
            {
                var patient = new Patient(
                    "Carlos",
                    "Rodriguez",
                    "8095551234",
                    "carlos@example.com",
                    null);

                await dbContext.Patients.AddAsync(patient, ct);
            }

            if (!await dbContext.Dentists.AnyAsync(ct))
            {
                var dentist = new Dentist(
                    "Maria",
                    "Perez",
                    "OD-12345",
                    "8095555678",
                    "meri@example.com");

                await dbContext.Dentists.AddAsync(dentist, ct);
            }

            if (!await dbContext.DentalServices.AnyAsync(ct))
            {
                var service = new DentalService(
                    "Dental Cleaning",
                    "General dental cleaning",
                    1500.00m,
                    30);

                await dbContext.DentalServices.AddAsync(service, ct);
            }

            await dbContext.SaveChangesAsync(ct);
        }
    }
}

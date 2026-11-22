using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OdontoCore.Application.Abstractions;
using OdontoCore.Infrastructure.Persistence;
using OdontoCore.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(connectionString);
            });

            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IDentistRepository, DentistRepository>();
            services.AddScoped<IDentalServiceRepository, DentalServiceRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}

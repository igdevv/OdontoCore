using Microsoft.EntityFrameworkCore;
using OdontoCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Infrastructure.Persistence
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Dentist> Dentists => Set<Dentist>();
        public DbSet<DentalService> DentalServices => Set<DentalService>();
        public DbSet<Appointment> Appointments => Set<Appointment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

    }
}

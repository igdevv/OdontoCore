using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OdontoCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Infrastructure.Configurations
{
    public sealed class PatientConfiguration: IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(patient => patient.Id);

            builder.Property(patient => patient.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(patient => patient.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(patient => patient.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(patient => patient.Email)
                .HasMaxLength(254);

            builder.Property(patient => patient.DateOfBirth)
                .HasColumnType("date");

            builder.Property(patient => patient.CreatedAt)
                .IsRequired();

            builder.Property(patient => patient.IsActive)
                .IsRequired();
        }
    }
}

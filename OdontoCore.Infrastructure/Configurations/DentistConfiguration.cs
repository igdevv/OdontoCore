using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OdontoCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Infrastructure.Configurations
{
    public sealed class DentistConfiguration: IEntityTypeConfiguration<Dentist>
    {
        public void Configure(EntityTypeBuilder<Dentist> builder)
        {
            builder.ToTable("Dentists");

            builder.HasKey(dentist => dentist.Id);

            builder.Property(dentist => dentist.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(dentist => dentist.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(dentist => dentist.LicenseNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(dentist => dentist.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(dentist => dentist.Email)
                .HasMaxLength(254);

            builder.Property(dentist => dentist.CreatedAt)
                .IsRequired();

            builder.Property(dentist => dentist.IsActive)
                .IsRequired();

            builder.HasIndex(dentist => dentist.LicenseNumber)
                .IsUnique();
        }
    }
}

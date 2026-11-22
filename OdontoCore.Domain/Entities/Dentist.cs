using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Domain.Entities
{
    public class Dentist
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string LicenseNumber { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string? Email { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Dentist(string firstName, string lastName, string licenseNumber, string phoneNumber, string? email)
        {
            if (string.IsNullOrWhiteSpace(licenseNumber))
                throw new ArgumentException("License number is required.", nameof(licenseNumber));

            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required.", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required.", nameof(lastName));

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number is required.", nameof(phoneNumber));

            Id = Guid.NewGuid();
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            LicenseNumber = licenseNumber.Trim();
            PhoneNumber = phoneNumber.Trim();
            Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim().ToLowerInvariant();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("Dentist is already inactive.");

            IsActive = false;
        }

        public void Activate()
        {
            if (IsActive)
                throw new InvalidOperationException("Dentist is already active.");

            IsActive = true;
        }

        public void UpdateLicenseNumber(string licenseNumber)
        {
            if (string.IsNullOrWhiteSpace(licenseNumber))
                throw new ArgumentException("License number is required.", nameof(licenseNumber));

            LicenseNumber = licenseNumber.Trim();
        }

        public void UpdateContactInfo(string phoneNumber, string? email)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number is required.", nameof(phoneNumber));

            string normalizedPhoneNumber = phoneNumber.Trim();

            string? normalizedEmail = string.IsNullOrWhiteSpace(email) ? null : email.Trim().ToLowerInvariant();

            PhoneNumber = normalizedPhoneNumber;
            Email = normalizedEmail;
        }

        public void UpdateName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required.", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required.", nameof(lastName));

            
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
        }

        
    }
}

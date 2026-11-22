using System;
using System.Collections.Generic;
using System.Text;

namespace OdontoCore.Domain.Entities
{
    public class DentalService
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public int DurationMinutes { get; private set; }
        public bool IsActive { get; private set; }

        public DentalService(string name, string? description, decimal price, int durationMinutes)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Service name is required.", nameof(name));

            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
            }

            if (durationMinutes <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(durationMinutes), "Duration must be greater than zero.");
            }

            if (decimal.Round(price, 2) != price)
            {
                throw new ArgumentException("Price cannot have more than two decimal places.", nameof(price));
            }

            Id = Guid.NewGuid();
            Name = name.Trim();
            Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
            Price = price;
            DurationMinutes = durationMinutes;
            IsActive = true;
        }

        public void ChangePrice(decimal newPrice)
        {
            if (newPrice < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newPrice), "Price cannot be negative.");
            }

            if (decimal.Round(newPrice, 2) != newPrice)
            {
                throw new ArgumentException("Price cannot have more than two decimal places.", nameof(newPrice));
            }

            Price = newPrice;
        }

        public void ChangeDuration(int newDurationInMinutes)
        {
            if (newDurationInMinutes <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newDurationInMinutes), "Duration must be greater than zero.");
            }

            DurationMinutes = newDurationInMinutes;
        }

        public void UpdateDetails(string name, string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Service name is required.", nameof(name));
            }

            Name = name.Trim();
            Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        }

        public void Deactivate()
        {
            if (!IsActive)
            {
                throw new InvalidOperationException("Dental service is already inactive.");
            }

            IsActive = false;
        }

        public void Activate()
        {
            if (IsActive)
            {
                throw new InvalidOperationException("Dental service is already active.");
            }

            IsActive = true;
        }
    }
}

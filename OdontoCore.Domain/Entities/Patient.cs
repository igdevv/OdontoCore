
namespace OdontoCore.Domain.Entities;

public class Patient
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public DateOnly? DateOfBirth { get; private set; }
    public string PhoneNumber { get; private set; } = string.Empty;
    public string? Email { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsActive { get; private set; }

    public Patient(string firstName, string lastName, string phoneNumber, string? email, DateOnly? dateOfBirth)
    {
        string validatedFirstName = ValidateName(firstName, nameof(firstName));

        string validatedLastName = ValidateName(lastName, nameof(lastName));

        string validatedPhoneNumber = ValidatePhoneNumber(phoneNumber);

        string? normalizedEmail = NormalizeEmail(email);

        ValidateDateOfBirth(dateOfBirth);

        Id = Guid.NewGuid();
        FirstName = validatedFirstName;
        LastName = validatedLastName;
        PhoneNumber = validatedPhoneNumber;
        Email = normalizedEmail;
        DateOfBirth = dateOfBirth;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public void Activate()
    {
        if (IsActive)
        {
            throw new InvalidOperationException("Patient is already active.");
        }

        IsActive = true;
    }

    public void Deactivate()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Patient is already inactive.");
        }

        IsActive = false;
    }

    public void UpdateName(string firstName, string lastName)
    {
        string validatedFirstName = ValidateName(firstName, nameof(firstName));

        string validatedLastName = ValidateName(lastName, nameof(lastName));

        FirstName = validatedFirstName;
        LastName = validatedLastName;
    }

    public void UpdateContactInfo(string phoneNumber, string? email)
    {
        string validatedPhoneNumber = ValidatePhoneNumber(phoneNumber);

        string? normalizedEmail = NormalizeEmail(email);

        PhoneNumber = validatedPhoneNumber;
        Email = normalizedEmail;
    }

    public void UpdateDateOfBirth(DateOnly dateOfBirth)
    {
        ValidateDateOfBirth(dateOfBirth);

        DateOfBirth = dateOfBirth;
    }

    private static string ValidateName(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Name is required.", parameterName);
        }

        string normalizedName = value.Trim();

        if (normalizedName.Length > 100)
        {
            throw new ArgumentException("Name cannot exceed 100 characters.", parameterName);
        }

        return normalizedName;
    }

    private static string ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new ArgumentException("Phone number is required.", nameof(phoneNumber));
        }

        return phoneNumber.Trim();
    }

    private static string? NormalizeEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return null;
        }

        string normalizedEmail = email.Trim().ToLowerInvariant();

        if (normalizedEmail.Length > 254 || !System.Net.Mail.MailAddress.TryCreate(normalizedEmail, out var address) ||
            address.Address != normalizedEmail)
        {
            throw new ArgumentException("Invalid email address.", nameof(email));
        }

        return normalizedEmail;
    }

    private static void ValidateDateOfBirth(DateOnly? dateOfBirth)
    {
        if (dateOfBirth.HasValue && dateOfBirth.Value > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new ArgumentException("Date of birth cannot be in the future.", nameof(dateOfBirth));
        }
    }
}

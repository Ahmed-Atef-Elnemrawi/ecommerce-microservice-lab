namespace Basket.Core.ValueObjects;

public sealed record CustomerInfo
{
  public string FirstName { get; init; }
  public string LastName { get; init; }
  public string Email { get; init; }
  public string PhoneNumber { get; init; }

  public CustomerInfo(string firstName, string lastName, string email, string phoneNumber)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
    ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
    ArgumentException.ThrowIfNullOrWhiteSpace(email);
    ArgumentException.ThrowIfNullOrWhiteSpace(phoneNumber);

    FirstName = firstName;
    LastName = lastName;
    Email = email;
    PhoneNumber = phoneNumber;
  }
}
namespace Basket.Core.ValueObjects;

public sealed record Address
{
  public string AddressLine { get; init; }
  public string Country { get; init; }
  public string City { get; init; }
  public string State { get; init; }
  public string ZipCode { get; init; }

  public Address(string addressLine, string country, string city, string state, string zipCode)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);
    ArgumentException.ThrowIfNullOrWhiteSpace(country);
    ArgumentException.ThrowIfNullOrWhiteSpace(city);
    ArgumentException.ThrowIfNullOrWhiteSpace(state);
    ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);

    AddressLine = addressLine;
    Country = country;
    City = city;
    State = state;
    ZipCode = zipCode;
  }
}
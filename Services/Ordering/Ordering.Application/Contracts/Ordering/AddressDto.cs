namespace Ordering.Application.Contracts.Ordering;

public sealed record AddressDto(
  string AddressLine,
  string Country,
  string City,
  string State,
  string ZipCode
);
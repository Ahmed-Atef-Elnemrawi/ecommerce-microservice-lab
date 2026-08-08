namespace Ordering.Application.Contracts.Ordering;

public sealed record CustomerInfoDto(
  string FirstName,
  string LastName,
  string Email,
  string PhoneNumber
);
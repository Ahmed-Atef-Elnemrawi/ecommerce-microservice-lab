namespace Basket.Application.Common.Dto;

public sealed record CustomerInfoDto(
  string FirstName,
  string LastName,
  string Email,
  string PhoneNumber
);
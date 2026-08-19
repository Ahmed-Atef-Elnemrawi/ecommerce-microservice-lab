namespace Basket.Application.Common.Dto;

public sealed record AddressDto(
  string AddressLine,
  string Country,
  string City,
  string State,
  string ZipCode
);
namespace Basket.Application.Common.Dto;

public sealed record CheckoutDto(
  string UserName,
  decimal TotalPrice,
  CustomerInfoDto Customer,
  AddressDto Address,
  PaymentInfoDto Payment);
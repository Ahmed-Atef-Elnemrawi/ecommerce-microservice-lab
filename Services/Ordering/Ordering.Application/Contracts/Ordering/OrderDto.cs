using Ordering.Domain.Enums;

namespace Ordering.Application.Contracts.Ordering;

public sealed record OrderDto(
  int OrderId,
  string UserName,
  decimal TotalPrice,
  CustomerInfoDto CustomerInfo,
  AddressDto Address,
  PaymentInfoDto PaymentInfoDto
);
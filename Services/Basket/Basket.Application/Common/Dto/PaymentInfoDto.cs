using Basket.Core.Enums;

namespace Basket.Application.Common.Dto;

public sealed record PaymentInfoDto(string CardName,
  string CardNumber,
  string CardExpirationDate,
  PaymentMethod PaymentMethods);
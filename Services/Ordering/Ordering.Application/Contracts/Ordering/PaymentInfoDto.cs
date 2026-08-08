using Ordering.Domain.Enums;

namespace Ordering.Application.Contracts.Ordering;

public sealed record PaymentInfoDto(string CardName,
  string CardNumber,
  string CardExpirationDate,
  PaymentMethods PaymentMethods);
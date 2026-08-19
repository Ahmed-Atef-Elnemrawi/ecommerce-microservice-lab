namespace EventBus.Messages.Events;

public sealed class BasketCheckoutEvent : BaseIntegrationEvent
{
  public string UserName { get; init; } = null!;
  public decimal TotalPrice { get; init; }

  public CustomerInfoDto CustomerInfo { get; init; } = null!;
  public AddressDto Address { get; init; } = null!;
  public PaymentInfoDto PaymentInfo { get; init; } = null!;
}

public sealed record CustomerInfoDto(
  string FirstName,
  string LastName,
  string Email,
  string PhoneNumber
);

public sealed record AddressDto(
  string AddressLine,
  string Country,
  string City,
  string State,
  string ZipCode
);

public sealed record PaymentInfoDto(
  string CardName,
  string CardNumber,
  string CardExpirationDate,
  PaymentMethod PaymentMethod
);

public enum PaymentMethod
{
  CreditCard = 1,
  PayPal = 2,
  CashOnDelivery = 3
}
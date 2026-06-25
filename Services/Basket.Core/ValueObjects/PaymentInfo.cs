namespace Basket.Core.ValueObjects;

public sealed record PaymentInfo
{
  public string CardName { get; init; }
  public string CardNumber { get; init; }
  public string CardExpirationDate { get; init; }

  public PaymentInfo(string cardName, string cardNumber, string cardExpirationDate)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
    ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
    ArgumentException.ThrowIfNullOrWhiteSpace(cardExpirationDate);

    CardName = cardName;
    CardNumber = cardNumber;
    CardExpirationDate = cardExpirationDate;
  }
}
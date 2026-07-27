using System.Globalization;
using Ordering.Domain.Enums;

namespace Ordering.Domain.ValueObjects;

public sealed record PaymentInfo
{
  public string CardName { get; init; }
  public string CardNumber { get; init; }
  public string CardExpirationDate { get; init; }
  public PaymentMethods PaymentMethods { get; init; }

  public PaymentInfo(
    string cardName,
    string cardNumber,
    string cardExpirationDate,
    PaymentMethods paymentMethods)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(cardName);

    ValidatePaymentMethod(paymentMethods, cardNumber, cardExpirationDate);
    ValidateCardNumber(cardNumber);
    ValidateExpirationDate(cardExpirationDate);

    CardName = cardName;
    CardNumber = cardNumber;
    CardExpirationDate = cardExpirationDate;
    PaymentMethods = paymentMethods;
  }

  private static void ValidatePaymentMethod(
    PaymentMethods paymentMethod,
    string cardNumber,
    string cardExpirationDate)
  {
    if (paymentMethod != PaymentMethods.CreditCard) return;
    ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
    ArgumentException.ThrowIfNullOrWhiteSpace(cardExpirationDate);

    ValidateCardNumber(cardNumber);
    ValidateExpirationDate(cardExpirationDate);
  }

  private static void ValidateCardNumber(string cardNumber)
  {
    var normalized = cardNumber.Replace(" ", "").Replace("-", "");

    if (!normalized.All(char.IsDigit))
      throw new ArgumentException("Card number must contain only digits.");

    if (normalized.Length is < 13 or > 19)
      throw new ArgumentException("Invalid card number length.");
  }

  private static void ValidateExpirationDate(string expirationDate)
  {
    if (!DateTime.TryParseExact(
          expirationDate,
          "MM/yy",
          CultureInfo.InvariantCulture,
          DateTimeStyles.None,
          out var expiration))
    {
      throw new ArgumentException("Expiration date must be in MM/yy format.");
    }

    var now = DateTime.UtcNow;

    if (expiration.Year < now.Year || (expiration.Year == now.Year && expiration.Month < now.Month))
    {
      throw new ArgumentException("Card has expired.");
    }
  }
}
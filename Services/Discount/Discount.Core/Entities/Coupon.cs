namespace Discount.Core.Entities;

public sealed class Coupon
{
  public int Id { get; private set; }
  public string ProductId { get; private set; } = null!;
  public string Description { get; private set; } = null!;
  public int Amount { get; private set; }
  public int Version { get; private set; }
  
  private Coupon()
  {
  }

  private Coupon(int id, string productId, string description, int amount)
  {
    Id = id;
    ProductId = productId;
    Description = description;
    Amount = amount;
  }

  public void UpdateAmount(int newAmount)
  {
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(newAmount);
    Amount = newAmount;
  }

  public void ChangeDescription(string description)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(description);
    Description = description;
  }

  public decimal ApplyDiscount(decimal price)
  {
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

    var finalPrice = price - Amount;
    return finalPrice < 0 ? 0 : finalPrice;
  }

  public static Coupon Create(string productId, string description, int amount)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(productId);
    ArgumentException.ThrowIfNullOrWhiteSpace(description);
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

    var coupon = new Coupon
    {
      ProductId = productId,
      Description = description,
      Amount = amount
    };

    return coupon;
  }
}
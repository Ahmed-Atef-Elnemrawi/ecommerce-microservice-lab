namespace Basket.Core.Entities;

public sealed class CartItem
{
  public int Quantity { get; private set; }
  public decimal Price { get; private set; }
  public string ProductId { get; private set; } = null!;
  public string ProductName { get; private set; } = null!;
  public string ImageUrl { get; private set; } = null!;
  public decimal Discount { get; private set; }

  public decimal PriceAfterDiscount => Math.Max(0, Price - Discount);

  private CartItem()
  {
    
  }
  
  private CartItem(string productId, string productName, int quantity, decimal price, string imageUrl)
  {
    Quantity = quantity;
    Price = price;
    ProductId = productId;
    ProductName = productName;
    ImageUrl = imageUrl;
    Discount = 0;
  }

  internal static CartItem Create(string productId, string productName, int quantity, decimal price, string imageUrl)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(productId);
    ArgumentException.ThrowIfNullOrWhiteSpace(productName);
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
    ArgumentException.ThrowIfNullOrWhiteSpace(imageUrl);

    return new CartItem(productId, productName, quantity, price, imageUrl);
  }

  internal void IncreaseQuantity(int quantity)
  {
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
    Quantity += quantity;
  }

  internal void DecreaseQuantity(int quantity)
  {
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

    if (Quantity - quantity < 0)
      throw new InvalidOperationException("Quantity cannot be less than 1.");

    Quantity -= quantity;
  }
  

  internal void ApplyDiscount(decimal discountAmount)
  {
    ArgumentOutOfRangeException.ThrowIfNegative(discountAmount);
    Discount = discountAmount;
  }
}
namespace Basket.Core.Entities;

public sealed class Cart
{
  private readonly List<CartItem> _items = [];

  public string UserName { get; private set; } = null!;
  public IReadOnlyCollection<CartItem> Items => _items;

  private Cart()
  {
  }

  private Cart(string userName)
  {
    UserName = userName;
  }

  public static Cart Create(string userName)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(userName);
    return new Cart(userName);
  }

  public void AddItem(string productId, string productName, int quantity, decimal price, string imageUrl)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(productId);
    var existingItem = Items.SingleOrDefault(p => p.ProductId == productId);

    if (existingItem is not null)
    {
      existingItem.IncreaseQuantity(existingItem.Quantity);
      return;
    }

    var newItem = CartItem.Create(productId, productName, quantity, price, imageUrl);

    _items.Add(newItem);
  }

  public void RemoveItem(string productId)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(productId);

    var existingItem = _items.SingleOrDefault(p => p.ProductId == productId);

    if (existingItem is null) return;

    if (existingItem.Quantity > 0)
    {
      existingItem.DecreaseQuantity(existingItem.Quantity);
      return;
    }

    _items.Remove(existingItem);
  }

  public void Clear()
  {
    _items.Clear();
  }
}
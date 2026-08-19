using System.Text.Json;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Infrastructure.Repositories;

public class ShoppingCartRepository(IDistributedCache distributedCache) : IShoppingCartRepository
{
  private static readonly JsonSerializerOptions SerializerOptions = new()
  {
    PropertyNameCaseInsensitive = true
  };

  public async Task<Cart?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
  {
    var shoppingCart = await distributedCache.GetStringAsync(userName, cancellationToken);
    return shoppingCart is null ? null : JsonSerializer.Deserialize<Cart>(shoppingCart, SerializerOptions);
  }

  public async Task<Cart> CreateAsync(Cart shoppingCart, CancellationToken cancellationToken)
  {
    await distributedCache.SetStringAsync(
      shoppingCart.UserName,
      JsonSerializer.Serialize(shoppingCart, SerializerOptions),
      cancellationToken
    );

    return shoppingCart;
  }

  public async Task DeleteAsync(string userName, CancellationToken cancellationToken)
  {
    await distributedCache.RemoveAsync(userName, cancellationToken);
  }
}
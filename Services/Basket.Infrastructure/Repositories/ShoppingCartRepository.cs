using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repositories;

public class ShoppingCartRepository(IDistributedCache distributedCache) : IShoppingCartRepository
{
  private readonly IDistributedCache _distributedCache = distributedCache;

  public async Task<Cart?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
  {
    var shoppingCart = await _distributedCache.GetStringAsync(userName, cancellationToken);

    return shoppingCart is null ? null : JsonConvert.DeserializeObject<Cart>(shoppingCart);
  }

  public async Task<Cart> CreateAsync(Cart shoppingCart, CancellationToken cancellationToken)
  {
    await _distributedCache.SetStringAsync(
      shoppingCart.UserName,
      JsonConvert.SerializeObject(shoppingCart),
      cancellationToken
      );
    
    return shoppingCart;
  }

  public async Task DeleteAsync(string userName, CancellationToken cancellationToken)
  {
    await _distributedCache.RemoveAsync(userName, cancellationToken);
  }
}
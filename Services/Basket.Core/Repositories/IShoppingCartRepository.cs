using Basket.Core.Entities;

namespace Basket.Core.Repositories;

public interface IShoppingCartRepository
{
  Task<Cart?> GetByUserNameAsync(string userName, CancellationToken cancellationToken);
  Task<Cart> CreateAsync(Cart shoppingCart, CancellationToken cancellationToken);
  Task DeleteAsync(string userName, CancellationToken cancellationToken);
}
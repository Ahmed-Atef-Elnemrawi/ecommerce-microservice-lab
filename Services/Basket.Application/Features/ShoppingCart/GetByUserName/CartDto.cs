using Basket.Core.Entities;

namespace Basket.Application.Features.ShoppingCart.GetByUserName;

public sealed record CartDto(string UserName, IReadOnlyList<CartItemDto> CartItems)
{
  public decimal TotalPrice => CartItems.Sum(p => p.Price);
}
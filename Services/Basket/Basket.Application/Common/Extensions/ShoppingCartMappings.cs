using Basket.Application.Common.Dto;
using Basket.Core.Entities;

namespace Basket.Application.Common.Extensions;

public static class ShoppingCartMappings
{
  public static CartDto MapToCartDto(this Cart cart)
  {
    var items = cart.Items.Select(p => new CartItemDto(
        p.ProductId,
        p.ProductName,
        p.Quantity,
        p.Price,
        p.ImageUrl
      )).ToList().AsReadOnly();
    
    return new CartDto(cart.UserName,items);
  }
}
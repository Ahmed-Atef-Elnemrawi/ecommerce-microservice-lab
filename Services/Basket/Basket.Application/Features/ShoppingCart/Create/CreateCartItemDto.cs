namespace Basket.Application.Features.ShoppingCart.Create;

public sealed record CreateCartItemDto(
  string ProductId,
  string ProductName,
  int Quantity,
  decimal Price,
  string ImageUrl);
namespace Basket.Application.Features.ShoppingCart.GetByUserName;

public sealed record CartItemDto(int Quantity, string ProductId, string ProductName, decimal Price, string ImageUrl);
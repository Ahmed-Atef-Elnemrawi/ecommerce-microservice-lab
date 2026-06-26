namespace Basket.Application.Common.Dto;

public sealed record CartItemDto(
  string ProductId,
  string ProductName,
  int Quantity,
  decimal Price,
  string ImageUrl);
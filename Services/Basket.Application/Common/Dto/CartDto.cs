namespace Basket.Application.Common.Dto;

public sealed record CartDto(string UserName, IReadOnlyList<CartItemDto> CartItems)
{
  public decimal TotalPrice => CartItems.Sum(p => p.Price * p.Quantity);
}
namespace Basket.Application.Common.Dto;

public sealed record CartDto(string UserName, IReadOnlyList<CartItemDto> CartItems, decimal TotalPrice);
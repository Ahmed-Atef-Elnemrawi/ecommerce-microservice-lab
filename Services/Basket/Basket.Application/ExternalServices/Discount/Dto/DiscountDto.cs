namespace Basket.Application.ExternalServices.Discount.Dto;

public sealed record DiscountDto(int Id, string ProductId, string Description, int Amount);
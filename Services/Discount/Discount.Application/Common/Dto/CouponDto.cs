namespace Discount.Application.Common.Dto;

public sealed record CouponDto(int Id, string ProductId, string Description, int Quantity);
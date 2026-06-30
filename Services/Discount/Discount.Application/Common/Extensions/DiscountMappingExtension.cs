using Discount.Application.Common.Dto;
using Discount.Core.Entities;

namespace Discount.Application.Common.Extensions;

public static class DiscountMappingExtension
{
  public static CouponDto ToDto(this Coupon coupon)
  {
    return new CouponDto(coupon.Id, coupon.ProductId, coupon.Description, coupon.Amount);
  }

  public static Coupon ToDomain(this CouponDto dto)
  {
    return Coupon.Create(dto.ProductId, dto.Description, dto.Quantity);
  }
}
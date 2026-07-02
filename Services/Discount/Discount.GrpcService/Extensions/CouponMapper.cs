using Discount.Application.Common.Dto;
using Discount.Grpc.Protos;

namespace Discount.GrpcService.Extensions;

public static class CouponMapper
{
  public static CouponModel ToProto(this CouponDto result)
  {
    return new CouponModel
    {
      Id = result.Id,
      ProductId = result.ProductId,
      Description = result.Description,
      Amount = result.Quantity,
    };
  }
}
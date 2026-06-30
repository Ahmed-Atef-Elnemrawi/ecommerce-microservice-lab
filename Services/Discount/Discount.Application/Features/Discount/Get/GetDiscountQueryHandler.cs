using Discount.Application.Common.Dto;
using Discount.Application.Common.Extensions;
using Discount.Application.Common.Models.ResultModel;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Features.Discount.Get;

public sealed class GetDiscountQueryHandler(IDiscountRepository discountRepository)
  : IRequestHandler<GetDiscountQuery, Result<CouponDto>>
{
  public async Task<Result<CouponDto>> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
  {
    var discount = await discountRepository.GetAsync(request.Id, cancellationToken);

    if (discount == null)
      return Result<CouponDto>.Failure("Coupon.NotFound", "Coupon not found", ErrorType.NotFound);

    return Result<CouponDto>.Success(discount.ToDto());
  }
}
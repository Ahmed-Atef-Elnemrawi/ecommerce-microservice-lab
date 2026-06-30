using Discount.Application.Common.Dto;
using Discount.Application.Common.Extensions;
using Discount.Application.Common.Models.ResultModel;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Features.Discount.Update;

public sealed class UpdateDiscountCommandHandler(IDiscountRepository discountRepository)
  : IRequestHandler<UpdateDiscountCommand, Result<CouponDto>>
{
  public async Task<Result<CouponDto>> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
  {
    var coupon = await discountRepository.GetAsync(request.Id, cancellationToken);

    if (coupon is null)
      return Result<CouponDto>.Failure("Coupon.NotFound", "Coupon not found", ErrorType.NotFound);

    coupon.UpdateAmount(request.Amount);
    coupon.ChangeDescription(request.Description);

    var isUpdated = await discountRepository.UpdateAsync(coupon, cancellationToken);

    if (!isUpdated)
      return Result<CouponDto>.Failure("Update.Conflict",
        "Coupon was modified by another request", ErrorType.Conflict);

    return Result<CouponDto>.Success(coupon.ToDto());
  }
}
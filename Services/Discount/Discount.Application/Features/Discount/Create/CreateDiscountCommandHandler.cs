using Discount.Application.Common.Dto;
using Discount.Application.Common.Extensions;
using Discount.Application.Common.Models.ResultModel;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Features.Discount.Create;

public sealed class CreateDiscountCommandHandler(IDiscountRepository discountRepository)
  : IRequestHandler<CreateDiscountCommand, Result<CouponDto>>
{
  public async Task<Result<CouponDto>> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
  {
    var newCoupon = Coupon.Create(request.ProductId, request.Description, request.Amount);
    var isCreated = await discountRepository.CreateAsync(newCoupon, cancellationToken);

    if (!isCreated)
      return Result<CouponDto>.Failure("Coupon.Exist", "Coupon is already exists", ErrorType.AlreadyExist);

    return Result<CouponDto>.Success(newCoupon.ToDto());
  }
}
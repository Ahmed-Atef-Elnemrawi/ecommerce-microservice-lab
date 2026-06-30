using Discount.Application.Common.Models.ResultModel;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Features.Discount.Delete;

public class DeleteDiscountCommandHandler(IDiscountRepository discountRepository)
  : IRequestHandler<DeleteDiscountCommand, Result<Unit>>
{
  public async Task<Result<Unit>> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
  {
    var discount = await discountRepository.GetAsync(request.Id, cancellationToken);

    if (discount is null)
      return Result<Unit>.Failure("Coupon.NotFound", "Coupon not found", ErrorType.NotFound);

    await discountRepository.DeleteAsync(request.Id, cancellationToken);
    return Result<Unit>.Success(Unit.Value);
  }
}
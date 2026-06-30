using Discount.Application.Common.Dto;
using Discount.Application.Common.Models.ResultModel;
using MediatR;

namespace Discount.Application.Features.Discount.Create;

public sealed record CreateDiscountCommand(string ProductId, string Description, int Amount)
  : IRequest<Result<CouponDto>>;
using Discount.Application.Common.Dto;
using Discount.Application.Common.Models.ResultModel;
using MediatR;

namespace Discount.Application.Features.Discount.Update;

public sealed record UpdateDiscountCommand(int Id, string ProductId, string Description, int Amount, int Version)
  : IRequest<Result<CouponDto>>;
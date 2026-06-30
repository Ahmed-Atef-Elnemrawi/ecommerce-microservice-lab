using Discount.Application.Common.Dto;
using Discount.Application.Common.Models.ResultModel;
using MediatR;

namespace Discount.Application.Features.Discount.Get;

public sealed record GetDiscountQuery(int Id) : IRequest<Result<CouponDto>>;
using Discount.Application.Common.Dto;
using Discount.Application.Common.Models.ResultModel;
using MediatR;

namespace Discount.Application.Features.Discount.Get;

public sealed record GetDiscountQuery(string ProductId) : IRequest<Result<CouponDto>>;
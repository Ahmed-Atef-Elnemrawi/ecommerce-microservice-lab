using Discount.Application.Common.Models.ResultModel;
using MediatR;

namespace Discount.Application.Features.Discount.Delete;

public sealed record DeleteDiscountCommand(int Id) : IRequest<Result<Unit>>;
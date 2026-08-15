using MediatR;
using Ordering.Application.Common.Models.ResultModel;
using Ordering.Application.Contracts.Ordering;

namespace Ordering.Application.Features.Ordering.GetById;

public sealed record GetByOrderIdQuery(int OrderId) : IRequest<Result<OrderDto>>;
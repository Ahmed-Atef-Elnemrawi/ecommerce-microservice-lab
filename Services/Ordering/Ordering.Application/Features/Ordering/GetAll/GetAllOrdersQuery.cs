using MediatR;
using Ordering.Application.Common.Models.ResultModel;
using Ordering.Application.Contracts.Ordering;

namespace Ordering.Application.Features.Ordering.GetAll;

public sealed record GetAllOrdersQuery() : IRequest<Result<IEnumerable<OrderDto>>>;
using MediatR;
using Ordering.Application.Common.Models.ResultModel;
using Ordering.Application.Contracts.Ordering;

namespace Ordering.Application.Features.Ordering.GetByUserName;

public sealed record GetOrdersByUserNameQuery(string UserName) : IRequest<Result<IEnumerable<OrderDto>>>;
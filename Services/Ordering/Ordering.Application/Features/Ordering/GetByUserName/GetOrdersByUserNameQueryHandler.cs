using MediatR;
using Ordering.Application.Common.Mappings;
using Ordering.Application.Common.Models.ResultModel;
using Ordering.Application.Contracts.Ordering;
using Ordering.Domain.Repositories;

namespace Ordering.Application.Features.Ordering.GetByUserName;

public sealed class GetOrdersByUserNameQueryHandler(IOrderRepository orderRepository)
  : IRequestHandler<GetOrdersByUserNameQuery, Result<IEnumerable<OrderDto>>>
{
  public async Task<Result<IEnumerable<OrderDto>>> Handle(GetOrdersByUserNameQuery request,
    CancellationToken cancellationToken)
  {
    //TODO:: check if the user is exist
    var orders = await orderRepository.GetByUserNameAsync(request.UserName, cancellationToken);
    var ordersDto = orders.Select(o => o.MapToOrderDto()).AsEnumerable();
    return Result<IEnumerable<OrderDto>>.Success(ordersDto);
  }
}
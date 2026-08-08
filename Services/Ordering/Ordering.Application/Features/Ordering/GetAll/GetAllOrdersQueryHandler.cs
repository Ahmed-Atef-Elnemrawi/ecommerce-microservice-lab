using MediatR;
using Ordering.Application.Common.Mappings;
using Ordering.Application.Common.Models.ResultModel;
using Ordering.Application.Contracts.Ordering;
using Ordering.Domain.Repositories;

namespace Ordering.Application.Features.Ordering.GetAll;

public sealed class GetAllOrdersQueryHandler(IOrderRepository orderRepository)
  : IRequestHandler<GetAllOrdersQuery, Result<IEnumerable<OrderDto>>>
{
  public async Task<Result<IEnumerable<OrderDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
  {
    var orders = await orderRepository.GetAllAsync(cancellationToken);
    var ordersDto = orders.Select(o => o.MapToOrderDto()).AsEnumerable();
    return Result<IEnumerable<OrderDto>>.Success(ordersDto);
  }
}
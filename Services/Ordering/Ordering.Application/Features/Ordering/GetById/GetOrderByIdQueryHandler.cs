using MediatR;
using Ordering.Application.Common.Mappings;
using Ordering.Application.Common.Models.ResultModel;
using Ordering.Application.Contracts.Ordering;
using Ordering.Domain.Repositories;

namespace Ordering.Application.Features.Ordering.GetById;

public sealed class GetOrderByIdQueryHandler(IOrderRepository orderRepository)
  : IRequestHandler<GetByOrderIdQuery, Result<OrderDto>>
{
  public async Task<Result<OrderDto>> Handle(GetByOrderIdQuery request, CancellationToken cancellationToken)
  {
    var order = await orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

    if (order is null)
      return Result<OrderDto>.Failure(
        "GetOrderById",
        $"Order with id {request.OrderId} is not found",
        ErrorType.NotFound
      );
    
    return Result<OrderDto>.Success(order.MapToOrderDto());
  }
}
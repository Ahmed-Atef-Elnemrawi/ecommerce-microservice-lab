using MediatR;
using Ordering.Application.Common.interfaces;
using Ordering.Application.Common.Models.ResultModel;
using Ordering.Domain.Repositories;

namespace Ordering.Application.Features.Ordering.Delete;

public sealed class DeleteOrderCommandHandler(IOrderRepository orderRepository, IPersistenceContext persistenceContext)
  : IRequestHandler<DeleteOrderCommand, Result<Unit>>
{
  public async Task<Result<Unit>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
  {
    var order = await orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

    if (order == null) 
      return Result<Unit>.Failure(
        "Order.NotFound",
        $"Order with id '{request.OrderId}' was not found.",
        ErrorType.NotFound
      );
    
    orderRepository.Delete(order);
    await persistenceContext.SaveChangesAsync(cancellationToken);
    
    return Result<Unit>.Success(Unit.Value);
  }
}
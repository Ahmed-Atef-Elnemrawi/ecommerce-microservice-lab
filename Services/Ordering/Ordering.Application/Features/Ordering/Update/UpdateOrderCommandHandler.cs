using MediatR;
using Ordering.Application.Common.interfaces;
using Ordering.Application.Common.Mappings;
using Ordering.Application.Common.Models.ResultModel;
using Ordering.Application.Contracts.Ordering;
using Ordering.Domain.Repositories;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Features.Ordering.Update;

public sealed class UpdateOrderCommandHandler(IOrderRepository orderRepository, IPersistenceContext persistenceContext)
  : IRequestHandler<UpdateOrderCommand, Result<OrderDto>>
{
  public async Task<Result<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
  {
    
    var order = await orderRepository.GetByIdAsync(request.Id, cancellationToken);
    
    if (order == null)
     return Result<OrderDto>.Failure(
        "Order.NotFound",
        $"Order with id '{request.Id}' was not found.",
        ErrorType.NotFound);

    var customerInfo = new CustomerInfo(
      request.CustomerInfo.FirstName,
      request.CustomerInfo.LastName,
      request.CustomerInfo.Email,
      request.CustomerInfo.PhoneNumber
    );

    var address = new Address(
      request.Address.AddressLine,
      request.Address.Country,
      request.Address.City,
      request.Address.State,
      request.Address.ZipCode
    );

    var paymentInfo = new PaymentInfo(
      request.PaymentInfo.CardName,
      request.PaymentInfo.CardNumber,
      request.PaymentInfo.CardExpirationDate,
      request.PaymentInfo.PaymentMethods
    );

    order.Update(request.UserName, request.TotalPrice, customerInfo, address, paymentInfo);
    
     orderRepository.Update(order);
     await persistenceContext.SaveChangesAsync(cancellationToken);

    return Result<OrderDto>.Success(order.MapToOrderDto());
  }
}
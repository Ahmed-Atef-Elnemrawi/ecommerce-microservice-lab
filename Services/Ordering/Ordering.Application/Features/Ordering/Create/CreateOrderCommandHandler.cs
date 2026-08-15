using MediatR;
using Ordering.Application.Common.interfaces;
using Ordering.Application.Common.Mappings;
using Ordering.Application.Common.Models.ResultModel;
using Ordering.Application.Contracts.Ordering;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Features.Ordering.Create;

public sealed class CreateOrderCommandHandler(IOrderRepository orderRepository, IPersistenceContext persistenceContext)
  : IRequestHandler<CreateOrderCommand, Result<OrderDto>>
{
  public async Task<Result<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
  {
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

    var order = Order.Create(
      request.UserName,
      request.TotalPrice,
      customerInfo,
      address,
      paymentInfo
    );

    var createdOrder = orderRepository.Create(order);
    await persistenceContext.SaveChangesAsync(cancellationToken);

    return Result<OrderDto>.Success(createdOrder.MapToOrderDto());
  }
}
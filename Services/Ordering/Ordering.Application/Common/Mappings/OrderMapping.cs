using Ordering.Application.Contracts.Ordering;
using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Common.Mappings;

public static class OrderMapping
{
  public static OrderDto MapToOrderDto(this Order order)
  {
    var customerInfoDto = new CustomerInfoDto(
      order.CustomerInfo.FirstName,
      order.CustomerInfo.LastName,
      order.CustomerInfo.Email,
      order.CustomerInfo.PhoneNumber
    );

    var addressDto = new AddressDto(
      order.Address.AddressLine,
      order.Address.Country,
      order.Address.City,
      order.Address.State,
      order.Address.ZipCode
    );
    
    var paymentInfoDto = new PaymentInfoDto(
      order.PaymentInfo.CardName,
      order.PaymentInfo.CardNumber,
      order.PaymentInfo.CardExpirationDate,
      order.PaymentInfo.PaymentMethods
    );

    return new OrderDto(
      order.Id,
      order.UserName,
      order.TotalPrice,
      customerInfoDto,
      addressDto,
      paymentInfoDto
    );
  }
}
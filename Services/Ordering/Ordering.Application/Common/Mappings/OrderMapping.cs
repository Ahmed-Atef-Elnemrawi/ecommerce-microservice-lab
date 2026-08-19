using EventBus.Messages.Events;
using Ordering.Application.Contracts.Ordering;
using Ordering.Application.Features.Ordering.Create;
using Ordering.Domain.Entities;

namespace Ordering.Application.Common.Mappings;

public static class OrderMapping
{
  public static OrderDto MapToOrderDto(this Order order)
  {
    var customerInfoDto = new Contracts.Ordering.CustomerInfoDto(
      order.CustomerInfo.FirstName,
      order.CustomerInfo.LastName,
      order.CustomerInfo.Email,
      order.CustomerInfo.PhoneNumber
    );

    var addressDto = new Contracts.Ordering.AddressDto(
      order.Address.AddressLine,
      order.Address.Country,
      order.Address.City,
      order.Address.State,
      order.Address.ZipCode
    );
    
    var paymentInfoDto = new Contracts.Ordering.PaymentInfoDto(
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
  
  public static CreateOrderCommand MapToCreateOderCommand(
    this BasketCheckoutEvent checkout)
  {
    var addressDto = new Contracts.Ordering.AddressDto
    (
      checkout.Address.AddressLine,
      checkout.Address.Country,
      checkout.Address.City,
      checkout.Address.State,
      checkout.Address.ZipCode
    );

    var customerInfo = new Contracts.Ordering.CustomerInfoDto
    (
      checkout.CustomerInfo.FirstName,
      checkout.CustomerInfo.LastName,
      checkout.CustomerInfo.Email,
      checkout.CustomerInfo.PhoneNumber
    );

    var paymentMethod = checkout.PaymentInfo.PaymentMethod switch
    {
      PaymentMethod.CashOnDelivery
        => Domain.Enums.PaymentMethods.CashOnDelivery,

      PaymentMethod.CreditCard
        => Domain.Enums.PaymentMethods.CreditCard,
      
      PaymentMethod.PayPal
        => Domain.Enums.PaymentMethods.PayPal,

      _ => throw new ArgumentOutOfRangeException(
        nameof(checkout.PaymentInfo.PaymentMethod),
        checkout.PaymentInfo.PaymentMethod,
        "Unsupported payment method.")
    };

    var paymentInfo = new Contracts.Ordering.PaymentInfoDto
    (
      checkout.PaymentInfo.CardName,
      checkout.PaymentInfo.CardNumber,
      checkout.PaymentInfo.CardExpirationDate,
      paymentMethod
    );

    return new CreateOrderCommand
    (
      checkout.UserName,
       checkout.TotalPrice,
      customerInfo,
       addressDto,
       paymentInfo
    );
  }
}
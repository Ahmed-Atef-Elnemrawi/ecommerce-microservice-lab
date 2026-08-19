using Basket.Application.Common.Dto;
using Basket.Core.Entities;
using EventBus.Messages.Events;

namespace Basket.Application.Common.Extensions;

public static class ShoppingCartMappings
{
  public static CartDto MapToCartDto(this Cart cart)
  {
    var items = cart.Items.Select(p => new CartItemDto(
        p.ProductId,
        p.ProductName,
        p.Quantity,
        p.Price,
        p.ImageUrl,
        p.PriceAfterDiscount
      )).ToList().AsReadOnly();
    
    return new CartDto(cart.UserName,items, cart.TotalPrice);
  }

  public static BasketCheckoutEvent MapToBasketCheckoutEvent(
    this CheckoutDto checkout)
  {
    var address = new EventBus.Messages.Events.AddressDto
    (
      checkout.Address.AddressLine,
      checkout.Address.Country,
      checkout.Address.City,
      checkout.Address.State,
      checkout.Address.ZipCode
    );

    var customerInfo = new EventBus.Messages.Events.CustomerInfoDto
    (
      checkout.Customer.FirstName,
      checkout.Customer.LastName,
      checkout.Customer.Email,
      checkout.Customer.PhoneNumber
    );

    var paymentMethod = checkout.Payment.PaymentMethods switch
    {
      Core.Enums.PaymentMethod.CashOnDelivery
        => PaymentMethod.CashOnDelivery,

      Core.Enums.PaymentMethod.CreditCard
        => PaymentMethod.CreditCard,
      
      Core.Enums.PaymentMethod.PayPal
        => PaymentMethod.PayPal,

      _ => throw new ArgumentOutOfRangeException(
        nameof(checkout.Payment.PaymentMethods),
        checkout.Payment.PaymentMethods,
        "Unsupported payment method.")
    };

    var paymentInfo = new EventBus.Messages.Events.PaymentInfoDto
    (
      checkout.Payment.CardName,
      checkout.Payment.CardNumber,
      checkout.Payment.CardExpirationDate,
      paymentMethod
    );

    return new BasketCheckoutEvent
      {
      UserName = checkout.UserName,
      TotalPrice = checkout.TotalPrice,
      CustomerInfo = customerInfo,
      Address = address,
      PaymentInfo = paymentInfo
    };
  }
}
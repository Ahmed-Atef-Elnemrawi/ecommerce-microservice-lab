using Basket.Core.Enums;
using Basket.Core.ValueObjects;

namespace Basket.Core.Entities;

public sealed class Checkout
{
  public string UserName { get; private set; }
  public decimal TotalPrice { get; private set; }
  public CustomerInfo Customer { get; private set; }
  public Address Address { get; private set; }
  public PaymentInfo Payment { get; private set; }
  public PaymentMethod PaymentMethod { get; set; }

  private Checkout(string userName, decimal totalPrice, CustomerInfo customer, Address address, PaymentInfo payment,
    PaymentMethod paymentMethod)
  {
    UserName = userName;
    TotalPrice = totalPrice;
    Customer = customer;
    Address = address;
    Payment = payment;
    PaymentMethod = paymentMethod;
  }

  public static Checkout Create(string userName, decimal totalPrice, CustomerInfo customer, Address address,
    PaymentInfo payment, PaymentMethod paymentMethod)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(userName);
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(totalPrice);
    ArgumentNullException.ThrowIfNull(customer);
    ArgumentNullException.ThrowIfNull(address);
    ArgumentNullException.ThrowIfNull(payment);

    return new Checkout(userName, totalPrice, customer, address, payment, paymentMethod);
  }
}
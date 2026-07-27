using System.ComponentModel;
using Ordering.Domain.Common;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Entities;

public sealed class Order : BaseEntity
{
  public string UserName { get; private set; } = null!;
  public decimal TotalPrice { get; private set; }
  public CustomerInfo CustomerInfo { get; private set; } = null!;
  public Address Address { get; private set; } = null!;
  public PaymentInfo PaymentInfo { get; private set; } = null!;

  private Order()
  {
  }

  public static Order Create(string userName, decimal totalPrice, CustomerInfo customerInfo, Address address,
    PaymentInfo paymentInfo)
  {

    ArgumentException.ThrowIfNullOrWhiteSpace(userName);
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(totalPrice);
    
    ArgumentNullException.ThrowIfNull(paymentInfo);
    ArgumentNullException.ThrowIfNull(customerInfo);
    ArgumentNullException.ThrowIfNull(address);

    return new Order
    {
      UserName = userName,
      TotalPrice = totalPrice,
      CustomerInfo = customerInfo,
      Address = address,
      PaymentInfo = paymentInfo
    };
  }
  
  public void Update(string userName, decimal totalPrice, CustomerInfo customerInfo, Address address,
    PaymentInfo paymentInfo)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(userName);
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(totalPrice);
    
    ArgumentNullException.ThrowIfNull(paymentInfo);
    ArgumentNullException.ThrowIfNull(customerInfo);
    ArgumentNullException.ThrowIfNull(address);
    
    UserName = userName;
    TotalPrice = totalPrice;
    CustomerInfo = customerInfo;
    Address = address;
    PaymentInfo = paymentInfo;
  }
}
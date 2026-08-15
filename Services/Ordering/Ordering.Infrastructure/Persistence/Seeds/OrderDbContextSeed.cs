using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;
using Ordering.Infrastructure.Persistence.Context;

namespace Ordering.Infrastructure.Persistence.Seeds;

public sealed class OrderDbContextSeed
{
  public static async Task SeedAsync(OrderDbContext context, CancellationToken cancellationToken = default)
  {
    if (await context.Orders.AnyAsync(cancellationToken))
      return;

    var orders = new List<Order>
    {
      Order.Create(
        userName: "ahmed.atef",
        totalPrice: 1500.00m,

        customerInfo: new CustomerInfo(
          firstName: "Ahmed",
          lastName: "Atef",
          email: "ahmed@example.com",
          phoneNumber: "01012345678"),

        address: new Address(
          addressLine: "123 Main Street",
          country: "Egypt",
          city: "Cairo",
          state: "Cairo",
          zipCode: "11511"),

        paymentInfo: new PaymentInfo(
          cardName: "Ahmed Atef",
          cardNumber: "4111111111111111",
          cardExpirationDate: "12/30",
          paymentMethods: PaymentMethods.CreditCard)
      ),

      Order.Create(
        userName: "mohamed.ali",
        totalPrice: 750.50m,

        customerInfo: new CustomerInfo(
          firstName: "Mohamed",
          lastName: "Ali",
          email: "mohamed@example.com",
          phoneNumber: "01123456789"),

        address: new Address(
          addressLine: "45 Nile Street",
          country: "Egypt",
          city: "Giza",
          state: "Giza",
          zipCode: "12511"),

        paymentInfo: new PaymentInfo(
          cardName: "Mohamed Ali",
          cardNumber: "5555555555554444",
          cardExpirationDate: "12/30",
          paymentMethods: PaymentMethods.CreditCard)
      )
    };

    await context.Orders.AddRangeAsync(orders, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);
  }
}
using FluentValidation;
using Ordering.Application.Contracts.Ordering;
using Ordering.Domain.Enums;

namespace Ordering.Application.Features.Ordering.Create;

public sealed class CreateOrderPaymentInfoValidator
  : AbstractValidator<PaymentInfoDto>
{
  public CreateOrderPaymentInfoValidator()
  {
    RuleFor(x => x.CardName)
      .NotEmpty();
    
    RuleFor(x => x.PaymentMethods)
      .IsInEnum();
    
    When(x => x.PaymentMethods == PaymentMethods.CreditCard, () =>
    {
      RuleFor(x => x.CardNumber)
        .NotEmpty();

      RuleFor(x => x.CardExpirationDate)
        .NotEmpty();
    });
  }
}
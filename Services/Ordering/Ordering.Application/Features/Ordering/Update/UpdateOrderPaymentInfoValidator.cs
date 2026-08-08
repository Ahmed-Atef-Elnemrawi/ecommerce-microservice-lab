using FluentValidation;
using Ordering.Application.Contracts.Ordering;
using Ordering.Domain.Enums;

namespace Ordering.Application.Features.Ordering.Update;

public sealed class UpdateOrderPaymentInfoValidator : AbstractValidator<PaymentInfoDto>
{
  public UpdateOrderPaymentInfoValidator()
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
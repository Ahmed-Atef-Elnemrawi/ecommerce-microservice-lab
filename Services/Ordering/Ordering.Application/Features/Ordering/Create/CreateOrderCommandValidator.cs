using FluentValidation;

namespace Ordering.Application.Features.Ordering.Create;

internal sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
  public CreateOrderCommandValidator()
  {
    RuleFor(x => x.UserName)
      .NotEmpty()
      .MaximumLength(100);

    RuleFor(x => x.TotalPrice)
      .GreaterThan(0);

    RuleFor(x => x.PaymentInfo)
      .NotNull()
      .SetValidator(new CreateOrderPaymentInfoValidator());

    RuleFor(x => x.CustomerInfo)
      .NotNull()
      .SetValidator(new CreateOrderCustomerInfoValidator());

    RuleFor(x => x.Address)
      .NotNull()
      .SetValidator(new CreateOrderAddressValidator());
  }
}
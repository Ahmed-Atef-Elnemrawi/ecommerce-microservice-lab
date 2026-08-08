using FluentValidation;

namespace Ordering.Application.Features.Ordering.Update;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
  public UpdateOrderCommandValidator()
  {
    RuleFor(x => x.Id)
      .GreaterThan(0);
    
    RuleFor(x => x.UserName)
      .NotEmpty()
      .MaximumLength(100);

    RuleFor(x => x.TotalPrice)
      .GreaterThan(0);

    RuleFor(x => x.PaymentInfo)
      .NotNull()
      .SetValidator(new UpdateOrderPaymentInfoValidator());

    RuleFor(x => x.CustomerInfo)
      .NotNull()
      .SetValidator(new UpdateOrderCustomerInfoValidator());

    RuleFor(x => x.Address)
      .NotNull()
      .SetValidator(new UpdateOrderAddressValidator());
  }
}

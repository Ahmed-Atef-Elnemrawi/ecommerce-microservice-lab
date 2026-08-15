using FluentValidation;
using Ordering.Application.Contracts.Ordering;

namespace Ordering.Application.Features.Ordering.Create;

internal sealed class CreateOrderCustomerInfoValidator : AbstractValidator<CustomerInfoDto>
{
  public CreateOrderCustomerInfoValidator()
  {
    RuleFor(x => x.FirstName)
      .NotEmpty()
      .MaximumLength(50);

    RuleFor(x => x.LastName)
      .NotEmpty()
      .MaximumLength(50);

    RuleFor(x => x.Email)
      .NotEmpty()
      .EmailAddress()
      .MaximumLength(255);

    RuleFor(x => x.PhoneNumber)
      .NotEmpty()
      .MaximumLength(20);
  }
}
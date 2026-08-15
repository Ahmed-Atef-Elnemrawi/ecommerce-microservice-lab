using FluentValidation;
using Ordering.Application.Contracts.Ordering;

namespace Ordering.Application.Features.Ordering.Create;

internal sealed class CreateOrderAddressValidator : AbstractValidator<AddressDto>
{
  public CreateOrderAddressValidator()
  {
    RuleFor(x => x.AddressLine)
      .NotEmpty()
      .MaximumLength(200);

    RuleFor(x => x.Country)
      .NotEmpty()
      .MaximumLength(100);

    RuleFor(x => x.City)
      .NotEmpty()
      .MaximumLength(100);

    RuleFor(x => x.State)
      .NotEmpty()
      .MaximumLength(100);

    RuleFor(x => x.ZipCode)
      .NotEmpty()
      .MaximumLength(20);
  }
}
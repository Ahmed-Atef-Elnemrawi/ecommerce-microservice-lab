using FluentValidation;

namespace Discount.Application.Features.Discount.Create;

public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
{
  public CreateDiscountCommandValidator()
  {
    RuleFor(x => x.ProductId)
      .NotEmpty()
      .WithMessage("ProductId is required");

    RuleFor(x => x.Description)
      .NotEmpty()
      .WithMessage("Description is required")
      .MaximumLength(200)
      .WithMessage("Description cannot exceed 200 characters");

    RuleFor(x => x.Amount)
      .GreaterThan(0)
      .WithMessage("Amount must be greater than 0");
  }
}
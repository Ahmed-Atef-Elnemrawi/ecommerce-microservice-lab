using FluentValidation;

namespace Basket.Application.Features.ShoppingCart.Delete;

public class DeleteCartValidator: AbstractValidator<DeleteCartCommand>
{
  public DeleteCartValidator()
  {
    RuleFor(command => command.UserName)
      .NotEmpty()
      .WithMessage("UserName is required");
  }
}
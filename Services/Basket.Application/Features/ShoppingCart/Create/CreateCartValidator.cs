using FluentValidation;

namespace Basket.Application.Features.ShoppingCart.Create;

public sealed class CreateCartValidator : AbstractValidator<CreateCartCommand>
{
  public CreateCartValidator()
  {
    RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
    RuleFor(x => x.CartItemsDto).NotEmpty().WithMessage("CartItems is required");
    RuleForEach(x => x.CartItemsDto).SetValidator(new CreateCartItemValidator());
  }
}
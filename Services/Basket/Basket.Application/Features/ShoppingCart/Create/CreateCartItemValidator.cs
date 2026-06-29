using Basket.Application.Common.Dto;
using FluentValidation;

namespace Basket.Application.Features.ShoppingCart.Create;

public sealed class CreateCartItemValidator : AbstractValidator<CartItemDto>
{
  public CreateCartItemValidator()
  {
    RuleFor(x => x.ProductId)
      .NotEmpty()
      .WithMessage("Product ID is required.");

    RuleFor(x => x.ProductName)
      .NotEmpty()
      .WithMessage("Product name is required.");

    RuleFor(x => x.Quantity)
      .GreaterThanOrEqualTo(1)
      .WithMessage("Quantity must be greater than or equal to 1.");

    RuleFor(x => x.Price)
      .GreaterThan(0)
      .WithMessage("Price must be greater than 0.");

    RuleFor(x => x.ImageUrl)
      .NotEmpty()
      .WithMessage("Image URL is required.");
  }
}
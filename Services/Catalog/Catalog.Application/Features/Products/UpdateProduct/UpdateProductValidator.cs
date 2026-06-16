using FluentValidation;

namespace Catalog.Application.Features.Products.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
  public UpdateProductValidator()
  {
    // Name
    RuleFor(x => x.Name)
      .NotEmpty()
      .WithMessage("Name is required");

    // Description
    RuleFor(x => x.Description)
      .NotEmpty()
      .WithMessage("Description is required")
      .MaximumLength(500)
      .WithMessage("Description cannot exceed 500 characters");

    // Summary
    RuleFor(x => x.Summary)
      .NotEmpty()
      .WithMessage("Summary is required.")
      .MaximumLength(300)
      .WithMessage("Summary cannot exceed 300 characters");

    // Price
    RuleFor(x => x.Price)
      .GreaterThan(0)
      .WithMessage("Price must be greater than 0.");

    // Brand
    RuleFor(x => x.BrandId)
      .NotEmpty()
      .WithMessage("BrandId is required");

    // Type
    RuleFor(x => x.TypeId)
      .NotEmpty()
      .WithMessage("TypeId is required");
  }
}
using FluentValidation;

namespace Ordering.Application.Features.Ordering.Delete;

public sealed class DeleteCommandValidator : AbstractValidator<DeleteOrderCommand>
{
   public DeleteCommandValidator()
   {
      RuleFor(c => c.OrderId)
         .GreaterThan(0);
   }
}
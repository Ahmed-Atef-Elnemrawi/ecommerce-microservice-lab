using Basket.Application.Common.Extensions;
using Basket.Application.Common.Interfaces;
using Basket.Application.Common.Models.ResultModel;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Features.ShoppingCart.Checkout;

public sealed class CheckoutCommandHandler(
  IShoppingCartRepository cartRepository,
  IEventBus eventBus)
  : IRequestHandler<CheckoutCommand, Result<Unit>>
{
  public async Task<Result<Unit>> Handle(CheckoutCommand request, CancellationToken cancellationToken)
  {
    var cart = await cartRepository.GetByUserNameAsync(request.Checkout.UserName, cancellationToken);

    
    if (cart is null)
      return Result<Unit>.Failure("Basket.Checkout", "Cart not found", ErrorType.NotFound);
    
    var checkout = request.Checkout with { TotalPrice = cart.TotalPrice };
    var checkoutEvent = checkout.MapToBasketCheckoutEvent();
   
    await eventBus.PublishAsync(checkoutEvent, cancellationToken);

    await cartRepository.DeleteAsync(request.Checkout.UserName, cancellationToken);

    return Result<Unit>.Success(Unit.Value);
  }
}
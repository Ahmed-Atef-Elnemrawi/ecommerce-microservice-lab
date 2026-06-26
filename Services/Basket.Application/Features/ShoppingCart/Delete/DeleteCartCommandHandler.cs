using Basket.Application.Common.Models.ResultModel;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Features.ShoppingCart.Delete;

public class DeleteCartCommandHandler(IShoppingCartRepository cartRepository):
  IRequestHandler<DeleteCartCommand, Result<Unit>>
{
  public async Task<Result<Unit>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
  {
    var cart = await cartRepository.GetByUserNameAsync(request.UserName, cancellationToken);
    
    if (cart is null) 
     return Result<Unit>.Failure(Error.Create("Cart.DeleteCart", "cart is not found", ErrorType.NotFound));
    
    await cartRepository.DeleteAsync(request.UserName, cancellationToken);
    return Result<Unit>.Success(Unit.Value);
  }
}
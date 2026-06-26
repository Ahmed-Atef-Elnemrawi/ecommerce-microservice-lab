using Basket.Application.Common.Models.ResultModel;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Features.ShoppingCart.GetByUserName;

public class GetByUserNameQueryHandler(IShoppingCartRepository cartRepository)
  : IRequestHandler<GetByUserNameQuery, Result<CartDto?>>
{
  public async Task<Result<CartDto?>> Handle(GetByUserNameQuery request, CancellationToken cancellationToken)
  {
    var cart = await cartRepository.GetByUserNameAsync(request.UserName, cancellationToken);

    if (cart is null)
      return Result<CartDto?>.Failure(Error.Create("Cart.GetByUserName", "Cart not found", ErrorType.NotFound));

    var itemsDto = cart.Items.Select(p => new CartItemDto(
      p.Quantity,
      p.ProductId,
      p.ProductName,
      p.Price,
      p.ImageUrl
    )).ToList().AsReadOnly();

    return  Result<CartDto?>.Success(new CartDto(request.UserName, itemsDto));
  }
}
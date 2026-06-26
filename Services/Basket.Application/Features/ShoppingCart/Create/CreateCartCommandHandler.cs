using Basket.Application.Common.Dto;
using Basket.Application.Common.Extensions;
using Basket.Application.Common.Models.ResultModel;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Features.ShoppingCart.Create;

public sealed class CreateCartCommandHandler(IShoppingCartRepository cartRepository)
  : IRequestHandler<CreateCartCommand, Result<CartDto>>
{
  public async Task<Result<CartDto>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
  {
    var cart = await cartRepository.GetByUserNameAsync(request.UserName, cancellationToken) ??
               Cart.Create(request.UserName);

    cart.Clear();

    foreach (var item in request.CartItemsDto)
      cart.AddItem(item.ProductId, item.ProductName, item.Quantity, item.Price, item.ImageUrl);

    await cartRepository.CreateAsync(cart, cancellationToken);

    return Result<CartDto>.Success(cart.MapToCartDto());
  }
}
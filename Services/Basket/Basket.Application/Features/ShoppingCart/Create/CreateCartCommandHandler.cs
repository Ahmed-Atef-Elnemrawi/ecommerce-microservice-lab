using Basket.Application.Common.Dto;
using Basket.Application.Common.Extensions;
using Basket.Application.Common.Models.ResultModel;
using Basket.Application.ExternalServices.Discount;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Features.ShoppingCart.Create;

public sealed class CreateCartCommandHandler(IShoppingCartRepository cartRepository, IDiscountService discountService)
  : IRequestHandler<CreateCartCommand, Result<CartDto>>
{
  public async Task<Result<CartDto>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
  {
    var cart = await cartRepository.GetByUserNameAsync(request.UserName, cancellationToken);
    
    if (cart is null)
     cart = Cart.Create(request.UserName);

    cart.Clear();

    foreach (var item in request.CartItemsDto)
    {
      cart.AddItem(item.ProductId, item.ProductName, item.Quantity, item.Price, item.ImageUrl);
      // N+1 Problem:
      // For N cart items → N gRPC calls → N database hits.
      // This causes unnecessary network overhead and degrades performance.
      //
      // Solution:
      // Use a batch request to retrieve all discounts in a single call.
      // TODO: Replace with batch discount request
      var discount = await discountService.GetDiscount(item.ProductId);
    
      if (discount is not null)
        cart.ApplyDiscount(item.ProductId, discount.Amount);
    }

    await cartRepository.CreateAsync(cart, cancellationToken);

    return Result<CartDto>.Success(cart.MapToCartDto());
  }
}
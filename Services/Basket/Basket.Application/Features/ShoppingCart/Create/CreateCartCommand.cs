using Basket.Application.Common.Dto;
using Basket.Application.Common.Models.ResultModel;
using MediatR;

namespace Basket.Application.Features.ShoppingCart.Create;

public sealed record CreateCartCommand(string UserName, IReadOnlyCollection<CartItemDto> CartItemsDto)
  : IRequest<Result<CartDto>>;
using Basket.Application.Common.Dto;
using Basket.Application.Common.Models.ResultModel;
using MediatR;

namespace Basket.Application.Features.ShoppingCart.Checkout;

public sealed record CheckoutCommand(CheckoutDto Checkout) : IRequest<Result<Unit>>;
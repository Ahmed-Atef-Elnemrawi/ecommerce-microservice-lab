using Basket.Application.Common.Models.ResultModel;
using MediatR;

namespace Basket.Application.Features.ShoppingCart.Delete;

public record DeleteCartCommand(string UserName): IRequest<Result<Unit>>;
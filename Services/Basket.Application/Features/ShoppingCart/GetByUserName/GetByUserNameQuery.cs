using Basket.Application.Common.Models.ResultModel;
using MediatR;

namespace Basket.Application.Features.ShoppingCart.GetByUserName;

public sealed record GetByUserNameQuery(string UserName) : IRequest<Result<CartDto?>>;
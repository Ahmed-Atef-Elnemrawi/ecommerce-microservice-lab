using MediatR;
using Ordering.Application.Common.Models.ResultModel;
using Ordering.Application.Contracts.Ordering;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Features.Ordering.Create;

public sealed record CreateOrderCommand(
  string UserName,
  decimal TotalPrice,
  CustomerInfoDto CustomerInfo,
  AddressDto Address,
  PaymentInfoDto PaymentInfo
) : IRequest<Result<OrderDto>>;
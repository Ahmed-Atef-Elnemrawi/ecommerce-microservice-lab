using MediatR;
using Ordering.Application.Common.Models.ResultModel;
using Ordering.Application.Contracts.Ordering;
using Ordering.Domain.Enums;

namespace Ordering.Application.Features.Ordering.Update;

public record UpdateOrderCommand(  
  int Id,
  string UserName,
  decimal TotalPrice,
  CustomerInfoDto CustomerInfo,
  AddressDto Address,
  PaymentInfoDto PaymentInfo) : IRequest<Result<OrderDto>>;
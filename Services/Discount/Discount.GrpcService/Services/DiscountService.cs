using Discount.Application.Common.Models.ResultModel;
using Discount.Application.Features.Discount.Create;
using Discount.Application.Features.Discount.Delete;
using Discount.Application.Features.Discount.Get;
using Discount.Application.Features.Discount.Update;
using Discount.Grpc.Protos;
using Discount.GrpcService.Extensions;
using Grpc.Core;
using MediatR;

namespace Discount.GrpcService.Services;

public class DiscountService(ISender sender) : DiscountProtoService.DiscountProtoServiceBase
{
  public override async Task<CouponModel> GetDiscount(GetRequestModel request, ServerCallContext context)
  {
    var query = new GetDiscountQuery(request.ProductId);
    var result = await sender.Send(query);

    if (result is { IsSuccess: false, Error: not null })
      throw new RpcException(new Status(StatusCode.NotFound, result.Error.Message!));

    return result.Data!.ToProto();
  }

  public override async Task<CouponModel> CreateDiscount(CreateCommandModel request, ServerCallContext context)
  {
    var command = new CreateDiscountCommand(request.ProductId, request.Description, request.Amount);
    var result = await sender.Send(command);

    if (result.IsSuccess)
      return result.Data!.ToProto();

    var error = result.Error!;

    var statusCode = error.Type switch
    {
      nameof(ErrorType.AlreadyExist) => StatusCode.AlreadyExists,
      nameof(ErrorType.Validation) => StatusCode.InvalidArgument,
      _ => StatusCode.Internal
    };

    throw new RpcException(new Status(statusCode, error.Message!));
  }

  public override async Task<CouponModel> UpdateDiscount(UpdateCommandModel request, ServerCallContext context)
  {
    var command = new UpdateDiscountCommand(request.Id, request.ProductId,
      request.Description, request.Amount, request.Version);

    var result = await sender.Send(command);

    if (result.IsSuccess)
      return result.Data!.ToProto();

    var error = result.Error!;
    var statusCode = error.Type switch
    {
      nameof(ErrorType.Validation) => StatusCode.InvalidArgument,
      nameof(ErrorType.Conflict) => StatusCode.Aborted,
      _ => StatusCode.Internal
    };

    throw new RpcException(new Status(statusCode, error.Message!));
  }

  public override async Task<DeleteResponseModel> DeleteDiscount(DeleteCommandModel request, ServerCallContext context)
  {
    var command = new DeleteDiscountCommand(request.Id);
    var result = await sender.Send(command);

    if (result.IsSuccess)
      return new DeleteResponseModel { Success = true };

    var error = result.Error!;
    var statusCode = error.Type switch
    {
      nameof(ErrorType.NotFound) => StatusCode.NotFound,
      _ => StatusCode.Internal
    };

    throw new RpcException(new Status(statusCode, error.Message!));
  }
}
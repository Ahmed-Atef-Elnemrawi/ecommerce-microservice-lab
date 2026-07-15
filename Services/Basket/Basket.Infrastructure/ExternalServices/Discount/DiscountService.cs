using Basket.Application.ExternalServices.Discount;
using Basket.Application.ExternalServices.Discount.Dto;
using Discount.Grpc.Protos;

namespace Basket.Infrastructure.ExternalServices.Discount;

public sealed class DiscountService(DiscountProtoService.DiscountProtoServiceClient client)
  : IDiscountService
{
  public async Task<DiscountDto?> GetDiscount(string productId)
  {
    var requestModel = new GetRequestModel{ ProductId = productId };
    var discount = await client.GetDiscountAsync(requestModel);

    return discount is not null
      ? new DiscountDto(discount.Id, discount.ProductId, discount.Description, discount.Amount)
      : null;
  }
}
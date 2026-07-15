using Basket.Application.ExternalServices.Discount.Dto;

namespace Basket.Application.ExternalServices.Discount;

public interface IDiscountService
{
  Task<DiscountDto?> GetDiscount(string productId);
}
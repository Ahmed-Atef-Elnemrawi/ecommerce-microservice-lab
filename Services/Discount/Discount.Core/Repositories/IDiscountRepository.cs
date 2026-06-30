using Discount.Core.Entities;

namespace Discount.Core.Repositories;

public interface IDiscountRepository
{
  Task<Coupon?> GetAsync(int id, CancellationToken cancellationToken = default);
  Task<Coupon?> GetByProductIdAsync(string productId, CancellationToken cancellationToken = default);
  Task<bool> CreateAsync(Coupon coupon, CancellationToken cancellationToken = default);
  Task<bool> UpdateAsync(Coupon coupon, CancellationToken cancellationToken = default);
  Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
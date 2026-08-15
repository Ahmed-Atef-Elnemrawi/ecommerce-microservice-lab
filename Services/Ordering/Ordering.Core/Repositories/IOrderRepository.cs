using Ordering.Domain.Entities;

namespace Ordering.Domain.Repositories;

public interface IOrderRepository
{
  Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken);
  Task<IReadOnlyList<Order>> GetByUserNameAsync(string userName, CancellationToken cancellationToken);
  Task<Order?> GetByIdAsync(int orderId, CancellationToken cancellationToken);
  Order Create(Order order);
  void Update(Order order);
  void Delete(Order order);
}
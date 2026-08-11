using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;
using Ordering.Infrastructure.Persistence.Context;

namespace Ordering.Infrastructure.Persistence.Repositories;

public sealed class OrderRepository(OrderDbContext dbContext)
  : IOrderRepository
{
  public async Task<IReadOnlyList<Order>> GetAllAsync(
    CancellationToken cancellationToken)
  {
    return await dbContext.Orders
      .AsNoTracking()
      .ToListAsync(cancellationToken);
  }

  public async Task<IReadOnlyList<Order>> GetByUserNameAsync(
    string userName,
    CancellationToken cancellationToken)
  {
    return await dbContext.Orders
      .AsNoTracking()
      .Where(o => o.UserName == userName)
      .ToListAsync(cancellationToken);
  }

  public async Task<Order?> GetByIdAsync(int orderId, CancellationToken cancellationToken)
  {
    return await dbContext.Orders
      .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
  }

  public Order Create(Order order)
  {
    dbContext.Orders.Add(order);
    return order;
  }

  public void Update(Order order)
  {
    dbContext.Orders.Update(order);
  }

  public void Delete(Order order)
  {
    dbContext.Orders.Remove(order);
  }
}
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.interfaces;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence.Context;

public sealed class OrderDbContext(DbContextOptions<OrderDbContext> options)
  : DbContext(options), IPersistenceContext
{
  public DbSet<Order> Orders => Set<Order>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Order>(entity =>
    {
      entity.OwnsOne(order => order.CustomerInfo);
      entity.OwnsOne(order => order.Address);
      entity.OwnsOne(order => order.PaymentInfo);
    });
  }

// TODO: Replace hard-coded audit user with the current authenticated user.
  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    foreach (var entry in ChangeTracker.Entries<BaseEntity>())
    {
      switch (entry.State)
      {
        case EntityState.Added:
          entry.Property(p => p.CreatedBy).CurrentValue = "Ahmed_Atef";
          entry.Property(p => p.CreatedOn).CurrentValue = DateTime.UtcNow;
          break;

        case EntityState.Modified:
          entry.Property(p => p.LastModifiedBy).CurrentValue = "Ahmed_Atef";
          entry.Property(p => p.LastModifiedOn).CurrentValue = DateTime.UtcNow;
          break;
        
      }
    }
    
    return base.SaveChangesAsync(cancellationToken);
  }
}

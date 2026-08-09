namespace Ordering.Application.Common.interfaces;

// Persistence capabilities needed by the application from other layers.
public interface IPersistenceContext
{
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
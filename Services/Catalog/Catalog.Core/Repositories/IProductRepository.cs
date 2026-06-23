using System.Linq.Expressions;
using Catalog.Core.Entities;
using Catalog.Core.helpers;

namespace Catalog.Core.Repositories;

public interface IProductRepository
{
  Task<PaginatedList<Product>> GetAllAsync(QueryParams queryParams, CancellationToken cancellationToken);
  Task<Product?> GetByIdAsync(string id, CancellationToken cancellationToken);
  Task<IList<Product>> GetByNameAsync(string name, CancellationToken cancellationToken);
  Task<IList<Product>> GetByBrandAsync(string brand, CancellationToken cancellationToken);
  Task<Product> AddAsync(Product product, CancellationToken cancellationToken);
  Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken);
  Task<bool> DeleteAsync(string id, CancellationToken cancellationToken);
}
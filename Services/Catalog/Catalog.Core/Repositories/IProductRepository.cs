using Catalog.Core.Entities;

namespace Catalog.Core.Repositories;

public interface IProductRepository
{
   Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
   Task<Product?> GetByIdAsync(string id, CancellationToken cancellationToken);
   Task<IEnumerable<Product>> SearchByNameAsync(string name, CancellationToken cancellationToken);
   Task<IEnumerable<Product>> GetByBrandAsync(string brand, CancellationToken cancellationToken);
   Task<Product>  AddAsync(Product product, CancellationToken cancellationToken);
   Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken);
   Task<bool> DeleteAsync(string id, CancellationToken cancellationToken);
}
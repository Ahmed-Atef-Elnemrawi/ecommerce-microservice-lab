using Catalog.Core.Entities;

namespace Catalog.Core.Repositories;

public interface IProductBrandRepository
{
  public Task<IEnumerable<ProductBrand>> GetAllAsync(CancellationToken cancellationToken);
  public Task<ProductBrand?> GetByIdAsync(string id, CancellationToken cancellationToken);
}
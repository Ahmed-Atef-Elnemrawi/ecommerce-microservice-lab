using System.Linq.Expressions;
using Catalog.Core.Entities;

namespace Catalog.Core.Repositories;

public interface IProductTypeRepository
{
  public Task<IList<ProductType>> GetAllAsync(CancellationToken cancellationToken);
  public Task<ProductType?> GetByIdAsync(string id, CancellationToken cancellationToken);
}
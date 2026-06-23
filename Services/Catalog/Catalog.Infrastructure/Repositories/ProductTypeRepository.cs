using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.Contexts;
using Catalog.Infrastructure.Documents;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductTypeRepository(CatalogContext context) : IProductTypeRepository
{
  private readonly CatalogContext _context = context;

  public async Task<IList<ProductType>> GetAllAsync(CancellationToken cancellationToken)
  {
    var types = await _context.ProductTypes.Find(p => true).ToListAsync(cancellationToken);
    return types.Select(TypeDocument.ToDomain).Where(p => p != null).Select(p => p!).ToList();
  }

  public async Task<ProductType?> GetByIdAsync(string id, CancellationToken cancellationToken)
  {
    var type = await _context.ProductTypes.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
    return TypeDocument.ToDomain(type);
  }
}
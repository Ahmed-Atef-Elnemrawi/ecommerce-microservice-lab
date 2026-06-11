using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.Contexts;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductBrandRepository(CatalogContext context) : IProductBrandRepository
{
  private readonly CatalogContext _context = context;


  public async Task<IEnumerable<ProductBrand>> GetAllAsync(CancellationToken cancellationToken)
  {
    return await _context.ProductBrands.Find(_ => true).ToListAsync(cancellationToken);
  }

  public async Task<ProductBrand?> GetByIdAsync(string id, CancellationToken cancellationToken)
  {
    return await _context.ProductBrands.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
  }
}
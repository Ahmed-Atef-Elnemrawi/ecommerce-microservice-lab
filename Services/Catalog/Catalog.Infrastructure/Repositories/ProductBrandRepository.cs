using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.Contexts;
using Catalog.Infrastructure.Documents;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductBrandRepository(CatalogContext context) : IProductBrandRepository
{
  private readonly CatalogContext _context = context;


  public async Task<IList<ProductBrand>> GetAllAsync(CancellationToken cancellationToken)
  {
    var brands = await _context.ProductBrands.Find(_ => true).ToListAsync(cancellationToken);
    return brands.Select(p => BrandDocument.ToDomain(p)!).ToList();
  }

  public async Task<ProductBrand?> GetByIdAsync(string id, CancellationToken cancellationToken)
  {
    var brand = await _context.ProductBrands.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
    return BrandDocument.ToDomain(brand);
  }
}
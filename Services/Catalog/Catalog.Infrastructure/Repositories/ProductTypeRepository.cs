using Catalog.Application.Common.Interfaces;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.Contexts;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductTypeRepository(ICatalogDbContext context) : IProductTypeRepository
{
   private readonly ICatalogDbContext _context = context;

   public async Task<IEnumerable<ProductType>> GetAllAsync(CancellationToken cancellationToken)
  {
     return await _context.ProductTypes.Find(p => true).ToListAsync(cancellationToken);
  }

  public async Task<ProductType?> GetByIdAsync(string id, CancellationToken cancellationToken)
  {
    return await _context.ProductTypes.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
  }
}
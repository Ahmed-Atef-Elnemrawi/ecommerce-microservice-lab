using Catalog.Application.Common.Interfaces;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.Contexts;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(ICatalogDbContext context)
  : IProductRepository
{
  private readonly ICatalogDbContext _context = context;

  // public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
  // {
  //   return await _context.Products.Find(p => true).ToListAsync(cancellationToken);
  // }
  //
  // public async Task<Product?> GetByIdAsync(string id, CancellationToken cancellationToken)
  // {
  //   return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
  // }
  //
  // public async Task<IEnumerable<Product>> SearchByNameAsync(string name, CancellationToken cancellationToken)
  // {
  //   return await _context.Products.Find(p => p.Name == name).ToListAsync(cancellationToken);
  // }
  //
  // public async Task<IEnumerable<Product>> GetByBrandAsync(string brandId, CancellationToken cancellationToken)
  // {
  //   return await _context.Products.Find(p => p.BrandId == brandId).ToListAsync(cancellationToken);
  // }

  public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken)
  {
    await _context.Products.InsertOneAsync(product, cancellationToken: cancellationToken);
    return product;
  }

  public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken)
  {
    var updated = await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product, cancellationToken: cancellationToken);
    return updated.IsAcknowledged && updated.ModifiedCount > 0;
  }

  public async Task<bool> DeleteAsync(string id,  CancellationToken cancellationToken)
  {
    var deleted = await _context.Products.DeleteOneAsync(p => p.Id == id, cancellationToken);
    return deleted.IsAcknowledged && deleted.DeletedCount > 0;
  }

}
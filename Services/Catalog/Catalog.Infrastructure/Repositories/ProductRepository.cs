using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.Contexts;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(ICatalogContext context)
  : IProductRepository, IProductBrandRepository, IProductTypeRepository
{
  private readonly ICatalogContext _context = context;

  public async Task<IEnumerable<Product>> GetAllProducts(CancellationToken cancellationToken)
  {
    return await _context.Products.Find(p => true).ToListAsync(cancellationToken);
  }

  public async Task<Product> GetProductById(string id, CancellationToken cancellationToken)
  {
    return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
  }

  public async Task<IEnumerable<Product>> GetAllProductsByName(string name, CancellationToken cancellationToken)
  {
    return await _context.Products.Find(p => p.Name == name).ToListAsync(cancellationToken);
  }

  public async Task<IEnumerable<Product>> GetAllProductsByBrand(string brand, CancellationToken cancellationToken)
  {
    return await _context.Products.Find(p => p.Brand.Name == brand).ToListAsync(cancellationToken);
  }

  public async Task<Product> CreateProduct(Product product, CancellationToken cancellationToken)
  {
    await _context.Products.InsertOneAsync(product, cancellationToken: cancellationToken);
    return product;
  }

  public async Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken)
  {
    var updated = await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product, cancellationToken: cancellationToken);
    return updated.IsAcknowledged && updated.ModifiedCount > 0;
  }

  public async Task<bool> DeleteProduct(string id,  CancellationToken cancellationToken)
  {
    var deleted = await _context.Products.DeleteOneAsync(p => p.Id == id, cancellationToken);
    return deleted.IsAcknowledged && deleted.DeletedCount > 0;
  }

  public async Task<IEnumerable<ProductBrand>> GetAllProductBrands(CancellationToken cancellationToken)
  {
    return await _context.ProductBrands.Find(p => true).ToListAsync(cancellationToken);
  }

  public async Task<IEnumerable<ProductType>> GetAllProductTypes(CancellationToken cancellationToken)
  {
    return await _context.ProductTypes.Find(p => true).ToListAsync(cancellationToken);
  }
}
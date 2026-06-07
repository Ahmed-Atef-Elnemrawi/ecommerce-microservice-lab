using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.Contexts;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(ICatalogContext context)
  : IProductRepository, IProductBrandRepository, IProductTypeRepository
{
  private readonly ICatalogContext _context = context;

  public async Task<IEnumerable<Product>> GetAllProducts()
  {
    return await _context.Products.Find(p => true).ToListAsync();
  }

  public async Task<Product> GetProductById(string id)
  {
    return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
  }

  public async Task<IEnumerable<Product>> GetAllProductsByName(string name)
  {
    return await _context.Products.Find(p => p.Name == name).ToListAsync();
  }

  public async Task<IEnumerable<Product>> GetAllProductsByBrand(string brand)
  {
    return await _context.Products.Find(p => p.Brand.Name == brand).ToListAsync();
  }

  public async Task<Product> CreateProduct(Product product)
  {
    await _context.Products.InsertOneAsync(product);
    return product;
  }

  public async Task<bool> UpdateProduct(Product product)
  {
    var updated = await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
    return updated.IsAcknowledged && updated.ModifiedCount > 0;
  }

  public async Task<bool> DeleteProduct(string id)
  {
    var deleted = await _context.Products.DeleteOneAsync(p => p.Id == id);
    return deleted.IsAcknowledged && deleted.DeletedCount > 0;
  }

  public async Task<IEnumerable<ProductBrand>> GetAllProductBrands()
  {
    return await _context.ProductBrands.Find(p => true).ToListAsync();
  }

  public async Task<IEnumerable<ProductType>> GetAllProductTypes()
  {
    return await _context.ProductTypes.Find(p => true).ToListAsync();
  }
}
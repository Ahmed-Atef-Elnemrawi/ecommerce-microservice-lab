using Catalog.Core.Entities;
using Catalog.Core.helpers;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.Contexts;
using Catalog.Infrastructure.Documents;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(CatalogContext context)
  : IProductRepository
{
  private readonly CatalogContext _context = context;

  public async Task<PaginatedList<Product>> GetAllAsync(QueryParams queryParams, CancellationToken cancellationToken)
  {
    var filter = Builders<ProductDocument>.Filter.Empty;

    if (!String.IsNullOrEmpty(queryParams.Search))
      filter &= Builders<ProductDocument>.Filter.Regex(p => p.Name, new BsonRegularExpression(queryParams.Search, "i"));

    if (!String.IsNullOrEmpty(queryParams.TypeId))
      filter &= Builders<ProductDocument>.Filter.Eq(p => p.Type.Id, queryParams.TypeId);

    if (!String.IsNullOrEmpty(queryParams.BrandId))
      filter &= Builders<ProductDocument>.Filter.Eq(p => p.Brand.Id, queryParams.BrandId);

    var sort = Builders<ProductDocument>.Sort.Descending(p => p.Name);

    if (!String.IsNullOrEmpty(queryParams.SortBy))
    {
      var isDesc = queryParams.SortDirection?.Equals("desc", StringComparison.InvariantCultureIgnoreCase) == true;

      sort = queryParams.SortBy.Trim().ToLowerInvariant() switch
      {
        "name" => isDesc
          ? Builders<ProductDocument>.Sort.Descending(x => x.Name)
          : Builders<ProductDocument>.Sort.Ascending(x => x.Name),

        "price" => isDesc
          ? Builders<ProductDocument>.Sort.Descending(x => x.Price)
          : Builders<ProductDocument>.Sort.Ascending(x => x.Price),

        _ => Builders<ProductDocument>.Sort.Descending(p => p.Name)
      };
    }

    var documents = await _context.Products.Find(filter).Sort(sort)
      .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
      .Limit(queryParams.PageSize).ToListAsync(cancellationToken);

    var totalCount = await _context.Products.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

    var products = documents.Select(ProductDocument.ToDomain).Where(p => p != null).Select(p => p!).ToList();
    return PaginatedList<Product>.Create(products, queryParams.PageNumber, queryParams.PageSize, totalCount);
  }

  public async Task<Product?> GetByIdAsync(string id, CancellationToken cancellationToken)
  {
    var product = await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
    return ProductDocument.ToDomain(product);
  }

  public async Task<IList<Product>> GetByNameAsync(string name, CancellationToken cancellationToken)
  {
    var filter = Builders<ProductDocument>.Filter.Regex(p => p.Name, new BsonRegularExpression(name, "i"));
    var products = await _context.Products.Find(filter).ToListAsync(cancellationToken);
    return products.Select(ProductDocument.ToDomain).Where(p => p != null).Select(p => p!).ToList();
  }

  public async Task<IList<Product>> GetByBrandAsync(string brandId, CancellationToken cancellationToken)
  {
    var products = await _context.Products.Find(p => p.Brand.Id == brandId).ToListAsync(cancellationToken);
    return products.Select(ProductDocument.ToDomain).Where(p => p != null).Select(p => p!).ToList();
  }

  public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken)
  {
    var doc = ProductDocument.FromDomain(product)!;
    await _context.Products.InsertOneAsync(doc, cancellationToken: cancellationToken);
    return product;
  }

  public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken)
  {
    var doc = ProductDocument.FromDomain(product)!;
    var updated = await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, doc,
      cancellationToken: cancellationToken);

    return updated.IsAcknowledged && updated.ModifiedCount > 0;
  }

  public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken)
  {
    var deleted = await _context.Products.DeleteOneAsync(p => p.Id == id, cancellationToken);
    return deleted.IsAcknowledged && deleted.DeletedCount > 0;
  }
}
using Catalog.Infrastructure.Configuration;
using Catalog.Infrastructure.Documents;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Contexts;

public class CatalogContext
{
  public IMongoCollection<ProductDocument> Products { get; }
  public IMongoCollection<BrandDocument> ProductBrands { get; }
  public IMongoCollection<TypeDocument> ProductTypes { get; }

  public CatalogContext(IOptions<DatabaseSettings> options)
  {
    var db = options.Value;

    var client = new MongoClient(db.ConnectionString);
    var database = client.GetDatabase(db.DatabaseName);

    ProductTypes = database.GetCollection<TypeDocument>(db.ProductTypesCollection);
    ProductBrands = database.GetCollection<BrandDocument>(db.ProductBrandsCollection);
    Products = database.GetCollection<ProductDocument>(db.ProductCollection);
  }
}
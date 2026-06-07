using Catalog.Core.Entities;
using Catalog.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Contexts;

public class CatalogContext : ICatalogContext
{
  public IMongoCollection<Product> Products { get; }
  public IMongoCollection<ProductBrand> ProductBrands { get; }
  public IMongoCollection<ProductType> ProductTypes { get; }

  //TODO:register Database configuration as Option pattern
  public CatalogContext(IOptions<DatabaseSettings> options)
  {
    var db = options.Value;

    var client = new MongoClient(db.ConnectionString);
    var database = client.GetDatabase(db.DatabaseName);

    ProductTypes = database.GetCollection<ProductType>(db.ProductCollection);
    ProductBrands = database.GetCollection<ProductBrand>(db.ProductBrandsCollection);
    Products  = database.GetCollection<Product>(db.ProductTypesCollection);

    _ = ProductContextSeed.SeedDataAsync(Products);
    _ = ProductBrandContextSeed.SeedDataAsync(ProductBrands);
    _ = ProductTypeContextSeed.SeedDataAsync(ProductTypes);
  }
}
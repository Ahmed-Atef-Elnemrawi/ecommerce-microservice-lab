using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Application.Common.Interfaces;

public interface ICatalogDbContext
{
  IMongoCollection<Product> Products { get; }
  IMongoCollection<ProductBrand> ProductBrands { get; }
  IMongoCollection<ProductType> ProductTypes { get; }
}
using Catalog.Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Infrastructure.Documents;

public class BrandDocument
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }

  public string Name { get; set; } = null!;


  public static ProductBrand? ToDomain(BrandDocument? brand)
  {
    return brand != null ? ProductBrand.Create(brand.Name, brand.Id) : null;
  }

  public static BrandDocument? FromDomain(ProductBrand? brand)
  {
    return brand != null
      ? new BrandDocument
      {
        Id = brand.Id,
        Name = brand.Name
      }
      : null;
  }
}
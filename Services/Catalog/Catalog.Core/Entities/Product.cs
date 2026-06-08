using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities;

public class Product : BaseEntity
{
  public string Name { get; private set; } = null!;
  public string Description { get; private set; } = null!;
  public string Summary { get; private set; } = null!;

  [BsonRepresentation(BsonType.Decimal128)]
  public decimal Price { get; private set; }

  public ProductBrand Brand { get; private set; } = null!;
  public ProductType Type { get; private set; } = null!;
}
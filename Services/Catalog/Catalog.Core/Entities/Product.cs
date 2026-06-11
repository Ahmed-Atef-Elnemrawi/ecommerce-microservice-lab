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
  public string BrandId { get; private set; }
  public string TypeId { get; private set; }

  private Product(string name, string description, string summary, decimal price, string brandId, string typeId)
  {
    Name = name;
    Description = description;
    Summary = summary;
    Price = price;
    BrandId = brandId;
    TypeId = typeId;
  }

  public static Product Create(string name, string description, string summary, decimal price, string brandId,
    string typeId)
  {
    if (string.IsNullOrWhiteSpace(name))
      throw new ArgumentNullException(nameof(name), $"Product name is required");

    if (price <= 0)
      throw new ArgumentException($"Price must be greater than zero", nameof(price));


    return new Product(name, description, summary, price, brandId, typeId);
  }
}
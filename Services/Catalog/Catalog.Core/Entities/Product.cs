using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities;

public class Product : BaseEntity
{
  public string Name { get; private set; }
  public string Description { get; private set; }
  public string Summary { get; private set; }

  [BsonRepresentation(BsonType.Decimal128)]
  public decimal Price { get; private set; }

  public ProductBrand Brand { get; private set; }
  public ProductType Type { get; private set; }

  private Product(string name, string description, string summary, decimal price, ProductBrand brand, ProductType type)
  {
    Name = name;
    Description = description;
    Summary = summary;
    Price = price;
    Brand = brand;
    Type = type;
  }

  public static Product Create(string name, string description, string summary, decimal price, ProductBrand brand,
    ProductType type)
  {
    if (string.IsNullOrWhiteSpace(name))
      throw new ArgumentNullException(nameof(name), $"Product name is required");

    if (price <= 0)
      throw new ArgumentException($"Price must be greater than zero", nameof(price));


    return new Product(name, description, summary, price, brand, type);
  }
}
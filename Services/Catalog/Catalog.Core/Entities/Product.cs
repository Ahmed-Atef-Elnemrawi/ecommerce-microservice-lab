using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities;

public class Product : BaseEntity
{
  public string Name { get; private set; } = null!;
  public string Description { get; private set; } = null!;
  public string Summary { get; private set; } = null!;
  public decimal Price { get; private set; }
  public ProductBrand Brand { get; private set; } = null!;
  public ProductType Type { get; private set; } = null!;

  private Product():base()
  {

  }

  public static Product Create(string name, string description, string summary, decimal price, ProductBrand brand,
    ProductType type, string? id = null)
  {
    if (string.IsNullOrWhiteSpace(name))
      throw new ArgumentNullException(nameof(name), $"Product name is required");

    if (price <= 0)
      throw new ArgumentException($"Price must be greater than zero", nameof(price));


    return new Product
    {
      Id = id,
      Name = name,
      Description = description,
      Summary = summary,
      Price = price,
      Type = type,
      Brand = brand,
    };
  }
}
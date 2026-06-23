using Catalog.Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Infrastructure.Documents;

public class ProductDocument
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }

  public string Name { get; set; } = null!;
  public string Description { get; set; } = null!;
  public string Summary { get; set; } = null!;

  [BsonRepresentation(BsonType.Decimal128)]
  public decimal Price { get; set; }

  public BrandDocument Brand { get; set; } = null!;
  public TypeDocument Type { get; set; } = null!;

  public static Product? ToDomain(ProductDocument? document)
  {
    return document != null
      ? Product.Create(
        document.Name,
        document.Description,
        document.Summary,
        document.Price,
        BrandDocument.ToDomain(document.Brand)!,
        TypeDocument.ToDomain(document.Type)!,
        document.Id
      )
      : null;
  }

  public static ProductDocument? FromDomain(Product? product)
  {
    return product != null
      ? new ProductDocument
      {
        Id = product.Id,
        Name = product.Name,
        Description = product.Description,
        Summary = product.Summary,
        Price = product.Price,
        Brand = BrandDocument.FromDomain(product.Brand)!,
        Type = TypeDocument.FromDomain(product.Type)!
      }
      : null;
  }
}
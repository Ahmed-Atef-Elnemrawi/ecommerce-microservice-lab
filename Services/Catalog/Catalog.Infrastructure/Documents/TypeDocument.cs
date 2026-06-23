using Catalog.Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Infrastructure.Documents;

public class TypeDocument
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }

  public string Name { get; set; } = null!;


  public static ProductType? ToDomain(TypeDocument? type)
  {
    return type != null ? ProductType.Create(type.Name, type.Id) : null;
  }

  public static TypeDocument? FromDomain(ProductType? type)
  {
    return type != null
      ? new TypeDocument
      {
        Id = type.Id,
        Name = type.Name
      }
      : null;
  }
}
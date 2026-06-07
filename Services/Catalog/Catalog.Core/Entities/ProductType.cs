using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities;

public class ProductType : BaseEntity
{
  public required string Name { get; set; }
}
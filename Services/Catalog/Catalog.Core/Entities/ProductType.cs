using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities;

public class ProductType : BaseEntity
{
  public string Name { get; private set; } = null!;
}
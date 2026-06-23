using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities;

public class ProductType : BaseEntity
{
  public string Name { get; private set; } = null!;

  public static ProductType Create(string name, string? id = null)
  {
    if (string.IsNullOrEmpty(name))
      throw new ArgumentNullException(nameof(name), $"Type name is required");

    return new ProductType
    {
      Id = id,
      Name = name
    };
  }
}
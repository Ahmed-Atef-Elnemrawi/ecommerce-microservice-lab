using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities;

public class ProductBrand : BaseEntity
{
  public string Name { get; private set; } = null!;

  public static ProductBrand Create(string name, string? id = null)
  {
    if (string.IsNullOrEmpty(name))
      throw new ArgumentNullException(nameof(name), $"Brand name is required");

    return new ProductBrand
    {
      Id = id,
      Name = name
    };
  }
}
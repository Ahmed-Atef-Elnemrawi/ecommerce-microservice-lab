using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Contexts;

public static class ProductContextSeed
{
  public static async Task SeedDataAsync(IMongoCollection<Product> productCollection)
  {
    //Check if any brands exist
    var hasData = await productCollection.Find(p => true).AnyAsync();
    if (hasData) return;

    //Get and check File path Existence
    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "DataSeed", "products.json");
    if (!File.Exists(filePath))
    {
      Console.WriteLine($"File doesn't exist: {filePath}");
    }

    //Read File And Deserialize To ProductBrand Object
    var data = await File.ReadAllTextAsync(filePath);
    var products = JsonSerializer.Deserialize<List<Product>>(data);

    //Insert Data into ProductBrandCollection
    if (products?.Any() is true)
      await productCollection.InsertManyAsync(products);
  }
}

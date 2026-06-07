using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Contexts;

public static class ProductBrandContextSeed
{
  public static async Task SeedDataAsync(IMongoCollection<ProductBrand> productBrandCollection)
  {
    //Check if any brands exist
    var hasData =  await productBrandCollection.Find(_ => true).AnyAsync();
    if (hasData) return;

    //Get and check File path Existence
    var filePath =  Path.Combine(Environment.CurrentDirectory, "Data", "DataSeed", "ProductBrands.json");
    if (!File.Exists(filePath))
    {
      Console.WriteLine($"file doesn't exist: {filePath}");
    }

    //Read File And Deserialize To ProductBrand Object
    var data = await File.ReadAllTextAsync(filePath);
    var productBrands =  JsonSerializer.Deserialize<ProductBrand[]>(data);

    //Insert Data into ProductBrandCollection
    if (productBrands?.Any() is true)
      await productBrandCollection.InsertManyAsync(productBrands);
  }
}
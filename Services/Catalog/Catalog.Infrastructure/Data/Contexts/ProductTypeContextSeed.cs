using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Contexts;

public static class ProductTypeContextSeed
{
  public static async Task SeedDataAsync(IMongoCollection<ProductType> productTypesCollection)
  {
    //Check if data already Exist
    var  hasData = await productTypesCollection.Find(p => true).AnyAsync();
    if (hasData) return;

    //Get and check File path Existence
    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "DataSeed", "ProductTypes.json");
    if (!File.Exists(filePath))
    {
      Console.WriteLine($"file doesn't exist: {filePath}");
    }

    //Read File And Deserialize To ProductBrand Object
    var data = await File.ReadAllTextAsync(filePath);
    var productTypes = JsonSerializer.Deserialize<List<ProductType>>(data);

    //Insert Data into ProductBrandCollection
    if(productTypes?.Any() is true)
      await productTypesCollection.InsertManyAsync(productTypes);
  }
}
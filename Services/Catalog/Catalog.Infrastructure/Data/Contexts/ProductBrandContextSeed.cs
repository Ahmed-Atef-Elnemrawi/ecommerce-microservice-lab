using System.Text.Json;
using Catalog.Infrastructure.Documents;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Contexts;

public static class ProductBrandContextSeed
{
  public static async Task SeedDataAsync(IMongoCollection<BrandDocument> productBrandCollection)
  {
    //Check if any brands exist
    var hasData = await productBrandCollection.Find(_ => true).AnyAsync();
    if (hasData) return;

    //Get and check File path Existence
    var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData", "brands.json");
    if (!File.Exists(filePath)) throw new FileNotFoundException($"The file {filePath} was not found");

    //Read File And Deserialize To ProductBrand Object
    var data = await File.ReadAllTextAsync(filePath);
    var productBrands = JsonSerializer.Deserialize<BrandDocument[]>(data);

    //Insert Data into ProductBrandCollection
    if (productBrands is { Length: > 0 })
      await productBrandCollection.InsertManyAsync(productBrands);
  }
}
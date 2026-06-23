using System.Text.Json;
using Catalog.Infrastructure.Documents;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Contexts;

public static class ProductContextSeed
{
  public static async Task SeedDataAsync(IMongoCollection<ProductDocument> productCollection)
  {
    //Check if any brands exist
    var hasData = await productCollection.Find(p => true).AnyAsync();
    if (hasData) return;

    //Get and check File path Existence
    var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData", "products.json");
    if (!File.Exists(filePath)) throw new FileNotFoundException($"The file {filePath} was not found.");

    //Read File And Deserialize To Product Object
    var data = await File.ReadAllTextAsync(filePath);
    var products = JsonSerializer.Deserialize<ProductDocument[]>(data);

    //Insert Data into ProductBrandCollection
    if (products is { Length: > 0 })
      await productCollection.InsertManyAsync(products);
  }
}
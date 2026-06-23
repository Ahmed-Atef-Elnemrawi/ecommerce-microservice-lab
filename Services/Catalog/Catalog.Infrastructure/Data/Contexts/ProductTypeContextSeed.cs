using System.Text.Json;
using Catalog.Infrastructure.Documents;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Contexts;

public static class ProductTypeContextSeed
{
  public static async Task SeedDataAsync(IMongoCollection<TypeDocument> productTypesCollection)
  {
    //Check if data already Exist
    var hasData = await productTypesCollection.Find(p => true).AnyAsync();
    if (hasData) return;

    //Get and check File path Existence
    var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData", "types.json");
    if (!File.Exists(filePath)) throw new FileNotFoundException($"The file {filePath} was not found");

    //Read File And Deserialize To ProductType Object
    var data = await File.ReadAllTextAsync(filePath);
    var productDocuments = JsonSerializer.Deserialize<TypeDocument[]>(data);

    //Insert Data into ProductBrandCollection
    if (productDocuments is { Length: > 0 })
      await productTypesCollection.InsertManyAsync(productDocuments);
  }
}
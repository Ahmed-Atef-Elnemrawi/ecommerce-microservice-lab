using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Contexts;

public class ProductContextSeed
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

public class SelectionSort
{
  public int[] Sort(int[] items)
  {
    for (int i = 0; i < items.Length-1; i++)
    {
      //assume the current hold the minimum value
      //the fist loop is used select the current value
      var minIndex = i;

      //The inner one is used for comparison
      for (int j = i + 1; j < items.Length; j++)
      {
         if (items[minIndex] < items[j]) continue;
          minIndex = j;

      }

      //swapping
      var temp = items[i];
      items[minIndex] = temp;
      items[i] =  items[minIndex];
    }

    return items;
  }
}
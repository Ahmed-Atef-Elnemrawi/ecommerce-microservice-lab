namespace Catalog.Infrastructure.Configuration;

public class DatabaseSettings
{
   public required string ConnectionString { get; init; }
   public required string DatabaseName { get; set; }
   public required string ProductCollection { get; set; }
   public required string ProductTypesCollection { get; set; }
   public required string ProductBrandsCollection { get; set; }
}
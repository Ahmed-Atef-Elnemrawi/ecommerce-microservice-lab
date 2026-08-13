using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Ordering.Infrastructure.Persistence.Context;
using Ordering.Infrastructure.Settings;

namespace Ordering.Infrastructure.Persistence.Context;

public sealed class OrderDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
{
  public OrderDbContext CreateDbContext(string[] args)
  {
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
    var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Ordering.API"));
    var configuration = new ConfigurationBuilder()
      .SetBasePath(basePath)
      .AddJsonFile("appsettings.json", optional: false)
      .AddJsonFile($"appsettings.{environment}.json", optional: true)
      .AddEnvironmentVariables()
      .Build();

    var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();
    var settings = configuration
      .GetSection(OrderDbSettings.SectionName)
      .Get<OrderDbSettings>();
    
    optionsBuilder.UseSqlServer(settings!.ConnectionString);
    
    return new OrderDbContext(optionsBuilder.Options);
  }
}

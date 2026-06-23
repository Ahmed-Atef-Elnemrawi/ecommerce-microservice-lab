using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Catalog.Infrastructure.Data.Contexts;

public class CatalogDbSeeder(IServiceScopeFactory scopeFactory) : IHostedService
{
  public async Task StartAsync(CancellationToken cancellationToken)
  {
    using var scope = scopeFactory.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<CatalogContext>();

    await Task.WhenAll(
      ProductContextSeed.SeedDataAsync(context.Products),
      ProductBrandContextSeed.SeedDataAsync(context.ProductBrands),
      ProductTypeContextSeed.SeedDataAsync(context.ProductTypes)
    );
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    return Task.CompletedTask;
  }
}
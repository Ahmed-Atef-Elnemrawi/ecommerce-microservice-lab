using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ordering.Infrastructure.Persistence.Context;
using Ordering.Infrastructure.Persistence.Seeds;

namespace Ordering.Infrastructure.Persistence.Initialization;

public class DatabaseInitializer( IServiceScopeFactory scopeFactory) : IHostedService
{
  public async Task StartAsync(CancellationToken cancellationToken)
  {
    await using var scope = scopeFactory.CreateAsyncScope();

    var context = scope.ServiceProvider.GetRequiredService<OrderDbContext>();

    await context.Database.MigrateAsync(cancellationToken);

    await OrderDbContextSeed.SeedAsync(context, cancellationToken);
  }

  public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
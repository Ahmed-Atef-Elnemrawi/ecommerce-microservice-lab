using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ordering.Infrastructure.Persistence.Context;
using Ordering.Infrastructure.Persistence.Seeds;

namespace Ordering.Infrastructure.Persistence.Initialization;

public sealed class DatabaseInitializer(
  IServiceScopeFactory scopeFactory,
  ILogger<DatabaseInitializer> logger) : IHostedService
{
  public async Task StartAsync(CancellationToken cancellationToken)
  {
    logger.LogInformation("Starting database initialization.");

    await using var scope = scopeFactory.CreateAsyncScope();

    var context = scope.ServiceProvider.GetRequiredService<OrderDbContext>();

    await context.Database.MigrateAsync(cancellationToken);

    logger.LogInformation("Database migration completed.");

    await OrderDbContextSeed.SeedAsync(context, cancellationToken);

    logger.LogInformation("Database seeding completed.");
  }

  public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
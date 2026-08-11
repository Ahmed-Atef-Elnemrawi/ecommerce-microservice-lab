using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Common.interfaces;
using Ordering.Domain.Repositories;
using Ordering.Infrastructure.Persistence.Context;
using Ordering.Infrastructure.Persistence.Initialization;
using Ordering.Infrastructure.Persistence.Repositories;
using Ordering.Infrastructure.Settings;

namespace Ordering.Infrastructure.ServiceRegistrations;

public static class InfrastructureServiceRegistrations
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
    IConfiguration configuration)
  {
    services
      .AddOptions<OrderDbSettings>()
      .BindConfiguration(OrderDbSettings.SectionName)
      .ValidateOnStart();

    services.AddDbContext<OrderDbContext>(options =>
    {
      options.UseSqlServer(configuration.GetConnectionString(OrderDbSettings.SectionName));
    });
    
    services.AddScoped<IPersistenceContext>(sp => sp.GetRequiredService<OrderDbContext>());
    
    services.AddScoped<IOrderRepository,  OrderRepository>();

    services.AddHostedService<DatabaseInitializer>();
    
    return services;
  }
}
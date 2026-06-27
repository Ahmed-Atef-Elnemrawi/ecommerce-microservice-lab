using Basket.Core.Repositories;
using Basket.Infrastructure.Configurations;
using Basket.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infrastructure.ServiceRegistrations;

public static class InfrastructureServiceRegistration
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
    IConfiguration configuration)
  {
    
    services.AddOptions<CacheSettings>()
      .BindConfiguration(CacheSettings.SectionName)
      .ValidateOnStart();
    
    services.AddStackExchangeRedisCache(options =>
    {
      var cacheSettings = configuration.GetSection(CacheSettings.SectionName).Get<CacheSettings>();
      options.Configuration =  cacheSettings!.ConnectionString;
      options.InstanceName = "Basket:";
    });

    services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

    return services;
  }
}
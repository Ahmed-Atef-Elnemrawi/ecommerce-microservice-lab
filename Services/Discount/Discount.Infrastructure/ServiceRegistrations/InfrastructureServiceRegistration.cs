using Discount.Core.Repositories;
using Discount.Infrastructure.Migrations;
using Discount.Infrastructure.Persistence.Repositories;
using Discount.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Discount.Infrastructure.ServiceRegistrations;

public static class InfrastructureServiceRegistration
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
    IConfiguration configuration)
  {
    services.AddOptions<DbSettings>()
      .Bind(configuration.GetSection(DbSettings.SectionName))
      .ValidateOnStart();
    
    services.AddSingleton<DbConnectionFactory>();
    
    return services;
  }
}
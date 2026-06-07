using Catalog.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
   public static IServiceCollection AddInfrastructure(this IServiceCollection services,
      IConfiguration configuration)
   {
      //register Database configuration as Option pattern
      services
         .AddOptions<DatabaseSettings>()
         .BindConfiguration("DatabaseSettings")
         .ValidateOnStart();
      return services;
   }
}
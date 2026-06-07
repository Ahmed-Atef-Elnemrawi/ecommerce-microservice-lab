using Catalog.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.DependencyInjections;

public static class ServiceRegistration
{
   public static IServiceCollection ConfigureMongoDbSettings(this IServiceCollection services, IConfiguration configuration)
   {
      services
         .AddOptions<DatabaseSettings>()
         .BindConfiguration("DatabaseSettings")
         .ValidateOnStart();
      return services;
   }
}
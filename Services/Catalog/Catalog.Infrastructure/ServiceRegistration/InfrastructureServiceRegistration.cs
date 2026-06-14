using Catalog.Application.Common.Interfaces;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Configuration;
using Catalog.Infrastructure.Data.Contexts;
using Catalog.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.ServiceRegistration;

public static class InfrastructureServiceRegistration
{
   public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
      IConfiguration configuration)
   {
      //register Database configuration as Option pattern
      services
         .AddOptions<DatabaseSettings>()
         .BindConfiguration("DatabaseSettings")
         .ValidateOnStart();

      services.AddScoped<IProductRepository, ProductRepository>();
      services.AddScoped<IProductBrandRepository, ProductBrandRepository>();
      services.AddScoped<IProductTypeRepository, ProductTypeRepository>();

      services.AddScoped<ICatalogDbContext, CatalogContext>();

      return services;
   }
}
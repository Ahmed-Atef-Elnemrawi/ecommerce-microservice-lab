using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.ServiceRegistration;

public static class ApplicationServiceRegistration
{
   public static IServiceCollection AddApplicationServices(this IServiceCollection services)
   {
      // services.AddAutoMapper(options =>
      // {
      //    options.AddProfile<CatalogMappersProfile>();
      // });

      services.AddMediatR(options =>
      {
         options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
      });

      return services;
   }
}
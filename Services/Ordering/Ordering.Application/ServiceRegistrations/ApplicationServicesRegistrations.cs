using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application.ServiceRegistrations;

public static class ApplicationServicesRegistrations
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddMediatR(options =>
    {
      options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    });
    
    return services;
  }
}
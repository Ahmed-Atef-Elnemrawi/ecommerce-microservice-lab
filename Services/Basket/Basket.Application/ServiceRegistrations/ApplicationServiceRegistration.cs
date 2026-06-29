using System.Reflection;
using Basket.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Application.ServiceRegistrations;

public static class ApplicationServiceRegistration
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services,
    IConfiguration configuration)
  {
    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    
    services.AddMediatR(options =>
    {
      options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    });

    return services;
  }
}
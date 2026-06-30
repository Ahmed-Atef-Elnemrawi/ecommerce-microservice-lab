using System.Reflection;
using Discount.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;

namespace Discount.Application.ServiceRegistrations;

public static class ApplicationServiceRegistrations
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddMediatR(options =>
    {
      options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    });
    
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    
    return services;
  }
}
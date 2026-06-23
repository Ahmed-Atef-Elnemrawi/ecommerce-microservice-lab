using System.Reflection;
using Catalog.Application.Common.Behaviors;
using Catalog.Application.Mappers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.ServiceRegistration;

public static class ApplicationServiceRegistration
{
   public static IServiceCollection AddApplicationServices(this IServiceCollection services)
   {
      services.AddAutoMapper(options =>
      {
         options.AddProfile<CatalogMappersProfile>();
      });

      services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

      services.AddMediatR(options =>
      {
         options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
      });

      return services;
   }
}
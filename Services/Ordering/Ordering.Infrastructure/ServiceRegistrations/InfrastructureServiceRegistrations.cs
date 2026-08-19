using EventBus.Messages.Common;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ordering.Application.Common.interfaces;
using Ordering.Domain.Repositories;
using Ordering.Infrastructure.EventBusConsumer;
using Ordering.Infrastructure.Persistence.Context;
using Ordering.Infrastructure.Persistence.Initialization;
using Ordering.Infrastructure.Persistence.Repositories;
using Ordering.Infrastructure.Settings;

namespace Ordering.Infrastructure.ServiceRegistrations;

public static class InfrastructureServiceRegistrations
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
    IConfiguration configuration)
  {
    services
      .AddOptions<OrderDbSettings>()
      .BindConfiguration(OrderDbSettings.SectionName)
      .ValidateOnStart();
    
    services
      .AddOptions<EventBusSettings>()
      .BindConfiguration(EventBusSettings.Name)
      .ValidateDataAnnotations()
      .ValidateOnStart();

    services.AddDbContext<OrderDbContext>(options =>
    {
      var settings = configuration
        .GetSection(OrderDbSettings.SectionName)
        .Get<OrderDbSettings>();
      
      options.UseSqlServer(settings!.ConnectionString);
    });
    
    services.AddMassTransit(options =>
    {
      options.AddConsumer<BasketCheckoutEventConsumer>();

      options.UsingRabbitMq((context, cfg) =>
      {
        var settings = context.GetRequiredService<IOptions<EventBusSettings>>().Value;
        cfg.Host(settings.HostAddress);

        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueueName, e =>
        {
          e.ConfigureConsumer<BasketCheckoutEventConsumer>(context);
        });
      });
    });
    
    services.AddScoped<IPersistenceContext>(sp => sp.GetRequiredService<OrderDbContext>());
    
    services.AddScoped<IOrderRepository,  OrderRepository>();

    services.AddHostedService<DatabaseInitializer>();
    
    return services;
  }
}
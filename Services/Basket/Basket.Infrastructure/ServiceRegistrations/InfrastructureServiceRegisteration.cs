using Basket.Application.ExternalServices.Discount;
using Basket.Core.Repositories;
using Basket.Infrastructure.Configurations;
using Basket.Infrastructure.ExternalServices.Discount;
using Basket.Infrastructure.Repositories;
using Discount.Grpc.Protos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Basket.Infrastructure.ServiceRegistrations;

public static class InfrastructureServiceRegistration
{
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
    IConfiguration configuration)
  {
    
    services.AddOptions<CacheSettings>()
      .BindConfiguration(CacheSettings.SectionName)
      .ValidateOnStart();
    
    services.AddOptions<DiscountGrpcSettings>()
      .BindConfiguration(DiscountGrpcSettings.SectionName)
      .Validate(x => !string.IsNullOrWhiteSpace(x.ServiceUrl),
        "Discount ServiceUrl must be provided")
      .ValidateOnStart();
    
    services.AddStackExchangeRedisCache(options =>
    {
      var cacheSettings = configuration.GetSection(CacheSettings.SectionName).Get<CacheSettings>();
      options.Configuration =  cacheSettings!.ConnectionString;
      options.InstanceName = "Basket:";
    });

    services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>((service, options) =>
    {
      var discountGrpcSettings = service.GetRequiredService<IOptions<DiscountGrpcSettings>>().Value;
      options.Address = new Uri(discountGrpcSettings.ServiceUrl);
    });

    services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
    services.AddScoped<IDiscountService, DiscountService>();

    return services;
  }
}
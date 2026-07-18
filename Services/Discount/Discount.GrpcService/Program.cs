using Discount.Application.ServiceRegistrations;
using Discount.GrpcService.Services;
using Discount.Infrastructure.ServiceRegistrations;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
  options.ListenAnyIP(8000, o => o.Protocols = HttpProtocols.Http2);
});

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
}

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.MapGet("/",
  () =>
    """
      Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client,
      visit: https://go.microsoft.com/fwlink/?linkid=2086909
    """);

app.Run();
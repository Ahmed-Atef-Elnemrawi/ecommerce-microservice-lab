using System;
using System.Linq;
using Catalog.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "Catalog API nnn",
    Version = "v1",
    Contact = new OpenApiContact
    {
      Email = "ah.at.elnemrawi@gmail.com",
      Name = "Ahmed Atef",
      Url = new Uri("https://github.com/ahatelnemrawi")
    }
  });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();

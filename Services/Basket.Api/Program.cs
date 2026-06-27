using Asp.Versioning;
using Basket.Application.ServiceRegistrations;
using Basket.Infrastructure.ServiceRegistrations;
using Scalar.AspNetCore;

var builder =  WebApplication.CreateBuilder(args);
 builder.Services.AddControllers();
 builder.Services.AddOpenApi();
 
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

 builder.Services.AddApiVersioning(options =>
 {
   options.ReportApiVersions = true;
   options.AssumeDefaultVersionWhenUnspecified = true;
   options.DefaultApiVersion = new ApiVersion(1, 0);
   options.ApiVersionReader = new UrlSegmentApiVersionReader();
 });
 
 var app = builder.Build();

 if (app.Environment.IsDevelopment())
 {
   app.MapOpenApi();
   app.MapScalarApiReference(options =>
   {
     options.Title = "Basket API";
   });
 }
 
 app.UseHttpsRedirection();
 app.MapControllers();

 app.Run();
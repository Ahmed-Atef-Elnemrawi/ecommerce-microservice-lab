using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Ordering.Infrastructure.Persistence.Context;
using Ordering.Infrastructure.Settings;

namespace Ordering.Infrastructure.Persistence.context;

public sealed class OrderDbContextFactory(IConfiguration configuration) 
  : IDesignTimeDbContextFactory<OrderDbContext>
{
  public OrderDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();

    optionsBuilder.UseSqlServer(configuration.GetConnectionString(OrderDbSettings.SectionName));
    
    return new OrderDbContext(optionsBuilder.Options);
  }
}
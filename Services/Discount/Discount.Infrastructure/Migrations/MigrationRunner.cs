using System.Reflection;
using DbUp;

namespace Discount.Infrastructure.Migrations;

public sealed class MigrationRunner
{
  public static void Run(string connectionString)
  {
    var upgrader = DeployChanges.To
      .PostgresqlDatabase(connectionString)
      .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
      .LogToConsole()
      .Build();
    
    var result = upgrader.PerformUpgrade();
    
    if (!result.Successful)
      throw new Exception("Migration failed", result.Error);
  }
}
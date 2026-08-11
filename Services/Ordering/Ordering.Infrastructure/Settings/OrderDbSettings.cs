namespace Ordering.Infrastructure.Settings;

public class OrderDbSettings
{
  public const string SectionName = "OrderingDbSettings";
  public string ConnectionString { get; set; } = null!;
}
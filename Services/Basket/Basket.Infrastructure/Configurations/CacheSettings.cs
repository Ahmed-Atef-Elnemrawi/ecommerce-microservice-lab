
namespace Basket.Infrastructure.Configurations;

public class CacheSettings
{
  public const string SectionName = "CacheSettings";
  public string ConnectionString { get; init; } = string.Empty;
}
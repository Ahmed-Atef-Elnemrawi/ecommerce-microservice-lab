namespace Discount.Infrastructure.Settings;

public sealed class DbSettings
{
  public const string SectionName = "NpgsqlSettings";
  public string ConnectionString { get; init; } = string.Empty;
}
namespace Basket.Infrastructure.Configurations;

public class DiscountGrpcSettings
{
  public const string SectionName = "DiscountGrpcSettings";
  public string ServiceUrl { get; set; } = null!;
}
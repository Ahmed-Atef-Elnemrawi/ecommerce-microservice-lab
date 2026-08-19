using System.ComponentModel.DataAnnotations;

namespace Basket.Infrastructure.Configurations;

public sealed class EventBusSettings
{
  public const string Name = "EventBusSettings";
  
  [Required]
  public string HostAddress { get; set; } = null!;
}
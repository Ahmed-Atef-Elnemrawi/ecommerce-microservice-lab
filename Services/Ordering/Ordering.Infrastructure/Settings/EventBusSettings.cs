using System.ComponentModel.DataAnnotations;

namespace Ordering.Infrastructure.Settings;

public sealed class EventBusSettings
{
  public const string Name = "EventBusSettings";
  
  [Required]
  public string HostAddress { get; set; } = null!;
}
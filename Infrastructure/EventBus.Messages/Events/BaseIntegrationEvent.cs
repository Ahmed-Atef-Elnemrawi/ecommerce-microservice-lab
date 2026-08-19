namespace EventBus.Messages.Events;

public abstract class BaseIntegrationEvent(Guid correlationId, DateTimeOffset creationDate)
{
  public Guid CorrelationId { get; init; } = correlationId;
  public DateTimeOffset CreationDate { get; init; } = creationDate;

  protected BaseIntegrationEvent() : this(Guid.NewGuid(), DateTimeOffset.UtcNow)
  {
  }
}
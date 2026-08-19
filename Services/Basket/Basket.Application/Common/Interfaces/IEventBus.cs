namespace Basket.Application.Common.Interfaces;

public interface IEventBus
{
  Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken)
    where TEvent : class;
}
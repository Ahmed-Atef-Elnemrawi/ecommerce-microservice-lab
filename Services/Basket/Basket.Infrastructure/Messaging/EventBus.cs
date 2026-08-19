using Basket.Application.Common.Interfaces;
using MassTransit;

namespace Basket.Infrastructure.Messaging;

public sealed class EventBus(IPublishEndpoint publishEndpoint) : IEventBus
{
  public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken)
    where TEvent : class
    => publishEndpoint.Publish(@event, cancellationToken);
}
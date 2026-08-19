using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Mappings;

namespace Ordering.Infrastructure.EventBusConsumer;

public class BasketCheckoutEventConsumer(ISender sender, ILogger<BasketCheckoutEventConsumer> logger)
  : IConsumer<BasketCheckoutEvent>
{
  public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
  {
    using var loggerScope = logger.BeginScope(
      "Consume Basket checkout event {CorrelationId}",
      context.CorrelationId
    );

    await sender.Send(context.Message.MapToCreateOderCommand());

    logger.LogInformation("Basket checkout event done!");
  }
}
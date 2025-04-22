using Application.Contracts;
using Application.Messaging.Order;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Consumers.Order;

public class OrderPaidConsumer(
    IEmailService service,
    ILogger<OrderPaidConsumer> logger
    
    ) : IConsumer<OrderPaidIntegrationEvent>
{
    public async Task Consume(ConsumeContext<OrderPaidIntegrationEvent> context)
    {
        await service.SendEmailAsync(context.Message.Email,
            "DTP Cam on quy khach da dat tour",
            $"Order {context.Message.OrderCode} has been paid"
        );
        logger.LogInformation($"Consumed Listened: {nameof(OrderPaidIntegrationEvent)} : {context.Message.OrderId}");
    }
}
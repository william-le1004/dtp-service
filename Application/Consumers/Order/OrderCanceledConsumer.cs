using Application.Contracts;
using Application.Messaging.Order;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Consumers.Order;

public class OrderCanceledConsumer(
    IEmailService service,
    ILogger<OrderCanceledConsumer> logger
) : IConsumer<OrderCanceledIntegrationEvent>
{
    public async Task Consume(ConsumeContext<OrderCanceledIntegrationEvent> context)
    {
        await service.SendEmailAsync(context.Message.Email,
            "DTP Cam on quy khach da huy booking",
            $"Order {context.Message.OrderCode} has been hủy"
        );
        logger.LogInformation($"Consumed Listened: {nameof(OrderCanceledIntegrationEvent)} : {context.Message.OrderId}");
    }
}
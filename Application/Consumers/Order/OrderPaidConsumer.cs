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
        var message = context.Message;
        var emailContent = $"Tour: {message.TourName}\n" +
                          $"Order Code: {message.OrderCode}\n" +
                          $"Tour Date: {message.TourDate?.ToString("dd/MM/yyyy")}\n" +
                          $"Total Cost: {message.FinalCost:N0} VND\n\n" +
                          "Ticket Details:\n";

        foreach (var ticket in message.OrderTickets)
        {
            emailContent += $"- {ticket.TicketKind}: {ticket.Quantity} x {ticket.GrossCost:N0} VND\n";
        }

        await service.SendEmailAsync(
            message.Email,
            $"Xác nhận thanh toán tour {message.TourName}",
            emailContent
        );

        logger.LogInformation($"Consumed {nameof(OrderPaidIntegrationEvent)} for OrderId: {message.OrderId}");
    }
}
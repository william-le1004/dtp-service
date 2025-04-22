using Application.Contracts;
using Application.Messaging.Tour;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Consumers.Tour;

public class TourCancelledConsumer(
    IEmailService service,
    ILogger<TourCancelledConsumer> logger) : IConsumer<TourCancelled>
{
    public async Task Consume(ConsumeContext<TourCancelled> context)
    {
        var message = context.Message;
        
        await service.SendEmailAsync(
            "customer@example.com", // You might want to get this from a configuration or database
            $"DTP-Hủy tour",
            CreateBody(
                message.CompanyName,
                message.TourTitle,
                message.BookingCode,
                message.CustomerName,
                message.StartDate,
                message.Remark,
                message.PaidAmount,
                message.RefundAmount
            )
        );
        
        logger.LogInformation($"Consumed Listened: Tour Cancelled {message.BookingCode}");
    }

    private string CreateBody(
        string companyName,
        string tourTitle,
        string bookingCode,
        string customerName,
        DateTime startDate,
        string remark,
        decimal paidAmount,
        decimal refundAmount)
    {
        return $@"(DTP): {DateTime.Now:dd/MM/yyyy, HH:mm}<br/>
                 Công ty: {companyName}<br/>
                 Tour: {tourTitle}<br/>
                 Mã đặt: {bookingCode}<br/>
                 Khách hàng: {customerName}<br/>
                 Ngày khởi hành: {startDate:dd/MM/yyyy}<br/>
                 Số tiền đã thanh toán: {paidAmount:N0}<br/>
                 Số tiền hoàn lại: {refundAmount:N0}<br/>
                 Ghi chú: {remark}";
    }
} 
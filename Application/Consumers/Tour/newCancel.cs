using Application.Contracts;
using Application.Messaging.Tour;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Application.Consumers.Tour;

public class TourCancelledNewConsumer : IConsumer<TourCancelled>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<TourCancelledConsumer> _logger;
    private readonly string _emailTemplatePath;

    public TourCancelledNewConsumer(
        IEmailService emailService,
        ILogger<TourCancelledConsumer> logger,
        string emailTemplatePath = "Templates/TourCancelledEmailTemplate.html")
    {
        _emailService = emailService;
        _logger = logger;
        _emailTemplatePath = emailTemplatePath;
    }

    public async Task Consume(ConsumeContext<TourCancelled> context)
    {
        var message = context.Message;

        var emailBody = CreateBody(
            message.CompanyName,
            message.TourTitle,
            message.BookingCode,
            message.CustomerName,
            message.StartDate,
            message.Remark,
            message.PaidAmount,
            message.RefundAmount
        );

        await _emailService.SendEmailAsync(
            message.CustomerEmail,
            $"[THÔNG BÁO] Tour \"{message.TourTitle}\" (Mã: {message.BookingCode}) đã bị hủy",
            emailBody
        );

        _logger.LogInformation($"Email sent: Tour Cancelled notification for booking {message.BookingCode}");
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
        try
        {
            string templateContent = File.ReadAllText(_emailTemplatePath);

            // Replace placeholders with actual data
            templateContent = templateContent
                .Replace("{{TourTitle}}", tourTitle)
                .Replace("{{TourCode}}", bookingCode)
                .Replace("{{CustomerName}}", customerName)
                .Replace("{{StartDate}}", startDate.ToString("dd/MM/yyyy"))
                .Replace("{{CancelReason}}", remark)
                .Replace("{{AmountPaid}}", $"{paidAmount:N0} VND")
                .Replace("{{RefundAmount}}", $"{refundAmount:N0} VND")
                .Replace("YourTourCompany", companyName);

            return templateContent;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create email body from template. Using fallback template.");
            return CreateFallbackEmailBody(companyName, tourTitle, bookingCode, customerName, startDate, remark, paidAmount, refundAmount);
        }
    }

    private string CreateFallbackEmailBody(
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
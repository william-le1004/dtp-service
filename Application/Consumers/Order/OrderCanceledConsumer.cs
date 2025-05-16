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
        var message = context.Message;
        var emailContent = CreateBody(message);

        await service.SendEmailAsync(
            message.Email,
            $"[THÔNG BÁO] Tour \"{message.TourName}\" (Mã: {message.OrderCode}) đã bị hủy",
            emailContent
        );

        logger.LogInformation($"Consumed Listened: {nameof(OrderCanceledIntegrationEvent)} : {message.OrderId}");
    }

    private static string CreateBody(OrderCanceledIntegrationEvent message)
    {
        var emailContent = $@"<!DOCTYPE html>
                <html lang=""vi"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>[THÔNG BÁO] Tour ""{{{{TourTitle}}}}"" (Mã: {{{{TourCode}}}}) đã bị hủy</title>
                </head>
                <body style=""font-family: Arial, Helvetica, sans-serif; line-height: 1.6; color: #333333; margin: 0; padding: 0;"">
                    <div style=""max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #dddddd;"">
                        <div style=""text-align: center; padding: 10px; background-color: #f8f8f8; border-bottom: 2px solid #0066cc;"">
                            <h2>[THÔNG BÁO] Tour ""{{{{TourTitle}}}}"" (Mã: {{{{TourCode}}}}) đã bị hủy</h2>
                        </div>

                        <div style=""padding: 20px 0;"">

                            <p>Chúng tôi rất tiếc phải thông báo rằng tour <strong>""{{{{TourTitle}}}}""</strong> (Mã đặt tour: <strong>{{{{TourCode}}}}</strong>) mà Anh/Chị đã đăng ký đã <span style=""font-weight: bold; color: #cc0000;"">bị hủy</span>.</p>

                            <p style=""font-weight: bold; margin-top: 20px; margin-bottom: 10px; color: #0066cc;"">Thông tin chi tiết:</p>
                            <table style=""width: 100%; border-collapse: collapse; margin: 20px 0;"">
                              
                                <tr>
                                    <td style=""padding: 8px; border-bottom: 1px solid #eeeeee; font-weight: bold;"">Mã đặt tour</td>
                                    <td style=""padding: 8px; border-bottom: 1px solid #eeeeee;"">{{{{TourCode}}}}</td>
                                </tr>
                                <tr>
                                    <td style=""padding: 8px; border-bottom: 1px solid #eeeeee; font-weight: bold;"">Tên tour</td>
                                    <td style=""padding: 8px; border-bottom: 1px solid #eeeeee;"">{{{{TourTitle}}}}</td>
                                </tr>
                                <tr>
                                    <td style=""padding: 8px; border-bottom: 1px solid #eeeeee; font-weight: bold;"">Ngày khởi hành</td>
                                    <td style=""padding: 8px; border-bottom: 1px solid #eeeeee;"">{{{{StartDate}}}}</td>
                                </tr>
                                <tr>
                                    <td style=""padding: 8px; border-bottom: 1px solid #eeeeee; font-weight: bold;"">Số tiền đã thanh toán</td>
                                    <td style=""padding: 8px; border-bottom: 1px solid #eeeeee;"">{{{{AmountPaid}}}}</td>
                                </tr>
                            </table>

                            <p style=""font-weight: bold; margin-top: 20px; margin-bottom: 10px; color: #0066cc;"">Hướng dẫn tiếp theo:</p>
                            <p>Anh/Chị vui lòng kiểm tra email/ứng dụng để xác nhận thông tin hoàn tiền.</p>
                            <p>Nếu có bất kỳ thắc mắc nào, xin liên hệ với chúng tôi:</p>

                            <div style=""background-color: #f8f8f8; padding: 15px; border-radius: 5px; margin: 20px 0;"">
                                <p><strong>Email:</strong> {{{{Email}}}}</p>
                                <p><strong>Hotline:</strong> 0123-456-789</p>
                            </div>

                            <p>Chúng tôi rất mong được phục vụ Anh/Chị trong những chuyến đi tiếp theo.</p>
                        </div>

                        <div style=""margin-top: 30px; text-align: center; font-size: 14px; color: #666666; border-top: 1px solid #eeeeee; padding-top: 20px;"">
                            <p>Trân trọng,</p>
                            <p><strong>Đội ngũ chăm sóc khách hàng</strong></p>
                        </div>
                    </div>
                </body>
                </html>";

        return emailContent
            .Replace("{{TourTitle}}", message.TourName)
            .Replace("{{TourCode}}", message.OrderCode)
            .Replace("{{StartDate}}", message.TourDate.ToString())
            .Replace("{{Email}}", message.Email)
            .Replace("{{AmountPaid}}", message.FinalCost.ToString("N0") + " VND");
    }
}
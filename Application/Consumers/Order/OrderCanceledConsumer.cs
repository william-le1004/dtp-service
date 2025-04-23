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
        var emailContent = $@"Kính chào Anh/Chị,

Chúng tôi rất tiếc phải thông báo rằng tour **""{message.TourName}""** (mã đặt tour: **{message.OrderCode}**) mà Anh/Chị đã đăng ký đã **bị hủy**.

**Thông tin chi tiết:**
- Mã đặt tour        : {message.OrderCode}
- Tên tour           : {message.TourName}
- Ngày tour          : {(message.TourDate.HasValue ? message.TourDate.Value.ToString("dd/MM/yyyy") : "Chưa xác định")}
- Tổng giá trị       : {message.FinalCost:N0} VNĐ

**Hướng dẫn tiếp theo:**
Anh/Chị vui lòng kiểm tra email/ứng dụng để xác nhận thông tin hoàn tiền.
Nếu có bất kỳ thắc mắc nào, xin liên hệ với chúng tôi:
- Email    : support@yourtourcompany.com
- Hotline  : 0123-456-789

Chúng tôi rất mong được phục vụ Anh/Chị trong những chuyến đi tiếp theo.

Trân trọng,
Đội ngũ chăm sóc khách hàng
YourTourCompany";

        await service.SendEmailAsync(
            message.Email,
            $"[THÔNG BÁO] Tour \"{message.TourName}\" (Mã: {message.OrderCode}) đã bị hủy",
            emailContent
        );

        logger.LogInformation($"Consumed Listened: {nameof(OrderCanceledIntegrationEvent)} : {message.OrderId}");
    }
}
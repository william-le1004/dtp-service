using Application.Contracts;
using Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Consumers;

public class PasswordForgetConsumer(ILogger<PasswordForgetConsumer> logger, IEmailService emailService)
    : IConsumer<PasswordForget>
{
    public Task Consume(ConsumeContext<PasswordForget> context)
    {
        var messageBody = MessageBody(context);
        var subject = "Quên Mật Khẩu";
        emailService.SendEmailAsync(context.Message.Email, subject, messageBody);

        logger.LogInformation("Email Password Forget Already Send To: {Email}", context.Message.Email);

        return Task.CompletedTask;
    }
    
    private static string MessageBody(ConsumeContext<PasswordForget> context)
    {
        var messageBody = $@"
        <div style=""font-family:Arial,Helvetica,sans-serif;font-size:16px;line-height:1.6;color:#333;"">
            <p>Tài khoản của bạn đã nhận 1 yêu cầu đặt lại mật khẩu, nếu là bạn là người gửi yêu cầu xin vui lòng xác nhận:</p>
            <p>
                <a href=""{context.Message.ConfirmUrl}"" 
                   style=""background-color:#007bff;color:#fff;padding:10px 20px;text-decoration:none;
                          font-weight:bold;border-radius:5px;display:inline-block;"">
                    Đặt lại mật khẩu
                </a>
            </p>
            <p>Nếu nút không hoạt động cho bạn, hãy sao chép và dán URL sau vào trình duyệt của bạn:
                <br />
                <a href=""{context.Message.ConfirmUrl}"" style=""color:#007bff;text-decoration:none;"">{context.Message.ConfirmUrl}</a>
            </p>
            <p>Xin chân thành cảm ơn bạn đã tin tưởng và sử dụng dịch vụ của chúng tôi.,<br />
            DTP Team</p>
        </div>
        ";
        return messageBody;
    }
}
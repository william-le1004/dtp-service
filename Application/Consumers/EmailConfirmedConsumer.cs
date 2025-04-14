using Application.Contracts;
using Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Consumers;

public class EmailConfirmedConsumer(ILogger<EmailConfirmedConsumer> logger, IEmailService emailService)
    : IConsumer<EmailConfirmed>
{
    public Task Consume(ConsumeContext<EmailConfirmed> context)
    {
        var messageBody = MessageBody(context);
        var subject = "Xác Thực Tài Khoản";
        emailService.SendEmailAsync(context.Message.Email, subject, messageBody);

        logger.LogInformation("Email confirmed at: {Email}", context.Message.Email);

        return Task.CompletedTask;
    }

    private static string MessageBody(ConsumeContext<EmailConfirmed> context)
    {
        var messageBody = $@"
        <div style=""font-family:Arial,Helvetica,sans-serif;font-size:16px;line-height:1.6;color:#333;"">
            <p>Xin Chào {context.Message.Name} ,</p>
            <p>Cảm ơn vì đã tạo tài khoản tại <strong>DTP Binh Dinh</strong>.
            Để bắt đầu thưởng thức tất cả các tính năng của chúng tôi, vui lòng xác nhận địa chỉ email của bạn bằng cách nhấp vào nút bên dưới:</p>
            <p>
                <a href=""{context.Message.ConfirmUrl}"" 
                   style=""background-color:#007bff;color:#fff;padding:10px 20px;text-decoration:none;
                          font-weight:bold;border-radius:5px;display:inline-block;"">
                    Xác Nhận Email
                </a>
            </p>
            <p>Nếu nút không hoạt động cho bạn, hãy sao chép và dán URL sau vào trình duyệt của bạn:
                <br />
                <a href=""{context.Message.ConfirmUrl}"" style=""color:#007bff;text-decoration:none;"">{context.Message.ConfirmUrl}</a>
            </p>
            <p>Nếu bạn không đăng ký tài khoản này, vui lòng bỏ qua email này.</p>
            <p>Cảm ơn,<br />
            DTP Team</p>
        </div>
        ";
        return messageBody;
    }
}
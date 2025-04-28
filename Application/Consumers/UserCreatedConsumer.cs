using Application.Contracts;
using Application.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Consumers;

public class UserCreatedConsumer(ILogger<UserCreatedConsumer> logger, IEmailService emailService)
    : IConsumer<UserCreated>
{
    public Task Consume(ConsumeContext<UserCreated> context)
    {
        var body = MessageBody(context);
        var subject = $"Chào Mừng {context.Message.Name}";
        emailService.SendEmailAsync(context.Message.Email, subject, body);

        logger.LogInformation(
            "Received UserCreated event from queue: Name={Name}, UserName={UserName}, Email={Email}",
            context.Message.Name,
            context.Message.UserName,
            context.Message.Email
        );
        return Task.CompletedTask;
    }
    
    private static string MessageBody(ConsumeContext<UserCreated> context)
    {
        var loginUrl = "https://dtp-frontend-three.vercel.app/login";
        var messageBody = $@"
        <div style=""font-family:Arial,Helvetica,sans-serif;font-size:16px;line-height:1.6;color:#333;"">
            <p>Xin Chào {context.Message.Name} ,</p>
            <p>Cảm ơn vì đã tạo tài khoản tại <strong>DTP Binh Dinh</strong>.
            <p>Chúng tôi rất vui mừng chào đón bạn đến với cộng đồng của chúng tôi.</p>
            <p>Thông tin tài khoản của bạn:</p>
                <li><strong>Tên:</strong> {context.Message.Name}</li>
                <li><strong>Tên đăng nhập:</strong> {context.Message.UserName}</li>
                <li><strong>Mật khẩu mặc định:</strong> {context.Message.DefaultPassword}</li>
                <li><strong>Công Ty:</strong> {context.Message.CompanyName}</li>
            <p>
            Để truy cập vào DTP bạn vui lòng xác thực tài khoản
                <a href=""{context.Message.ConfirmUrl}"">
                    tại đây
                </a>
            </p>
            <p>Cảm ơn,<br />
            DTP Team</p>
        </div>
        ";
        
        return messageBody;
    }
}
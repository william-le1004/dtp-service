using Application.Contracts;
using Application.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Consumers;

public class UserCreatedConsumer : IConsumer<UserCreated>
{
    private readonly ILogger<UserCreatedConsumer> _logger;
    private readonly IEmailService _emailService;

    public UserCreatedConsumer(ILogger<UserCreatedConsumer> logger, IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public Task Consume(ConsumeContext<UserCreated> context)
    {
        _logger.LogInformation(
            "User Registered: {Name}, {Email}, {Password}",
            context.Message.UserName, context.Message.Email,
            context.Message.Password
        );
    
        var messageBody = $@"
        <div style=""font-family:Arial,Helvetica,sans-serif;font-size:16px;line-height:1.6;color:#333;"">
            <p>Hi {context.Message.UserName},</p>
            <p>Thank you for creating an account at <strong>Dot Net Tutorials</strong>.
            To start enjoying all of our features, please confirm your email address by clicking the button below:</p>
            <p>
                <a href=""{context.Message.UserName}"" 
                   style=""background-color:#007bff;color:#fff;padding:10px 20px;text-decoration:none;
                          font-weight:bold;border-radius:5px;display:inline-block;"">
                    Confirm Email
                </a>
            </p>
            <p>If the button doesn’t work for you, copy and paste the following URL into your browser:
                <br />
                <a href=""{context.Message.UserName}"" style=""color:#007bff;text-decoration:none;"">{context.Message.UserName}</a>
            </p>
            <p>If you did not sign up for this account, please ignore this email.</p>
            <p>Thanks,<br />
            The Dot Net Tutorials Team</p>
        </div>
        ";
    
        _emailService.SendEmailAsync(context.Message.Email, "Confirm Mail", messageBody);
    
        return Task.CompletedTask;
    }
}
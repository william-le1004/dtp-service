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
        string filePath = $"D:\\Capstone\\dtp-service\\Application\\Templates\\WelcomeTemplate.html";
        
        StreamReader str = new StreamReader(filePath);
        string MailText = str.ReadToEnd();
        str.Close();
        MailText = MailText.Replace("[username]", context.Message.UserName).Replace("[password]", context.Message.Password);
        var subject = $"Welcome {context.Message.Name}";
        _emailService.SendEmailAsync(context.Message.Email, subject, MailText);
    
        _logger.LogInformation(
            "Received UserCreated event from queue: Name={Name}, UserName={UserName}, Email={Email}",
            context.Message.Name, 
            context.Message.UserName, 
            context.Message.Email
        );
        return Task.CompletedTask;
    }
}
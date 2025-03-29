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
        string filePath2 = Path.Combine(Directory.GetCurrentDirectory(), "..", "Application", "Templates",
            "WelcomeTemplate.html");
        filePath2 = Path.GetFullPath(filePath2);

        StreamReader str = new StreamReader(filePath2);
        string mailText = str.ReadToEnd();
        str.Close();
        mailText = mailText.Replace("[username]", context.Message.UserName)
            .Replace("[password]", context.Message.Password);
        var subject = $"Welcome {context.Message.Name}";
        _emailService.SendEmailAsync(context.Message.Email, subject, mailText);

        _logger.LogInformation(
            "Received UserCreated event from queue: Name={Name}, UserName={UserName}, Email={Email}",
            context.Message.Name,
            context.Message.UserName,
            context.Message.Email
        );
        return Task.CompletedTask;
    }
}
using Application.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Consumers;

public class UserRegisteredConsumer : IConsumer<UserRegistered>
{
    private readonly ILogger<UserRegisteredConsumer> _logger;

    public UserRegisteredConsumer(ILogger<UserRegisteredConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<UserRegistered> context)
    {
        _logger.LogInformation
        ("User Registered: {Name}, {Email}, {PhoneNumber}",
            context.Message.Name, context.Message.Email,
            context.Message.PhoneNumber);

        Console.WriteLine(context.Message.Name);

        return Task.CompletedTask;
    }
}
using Application.Consumers;
using Application.Consumers.Wallet;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services,
        IConfiguration configuration)
    {
        var mqConnection = configuration.GetSection("RabbitMQ");

        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();
            config.AddConsumer<UserCreatedConsumer>();
            config.AddConsumer<TransactionRecordedConsumer>();

            config.AddConsumer<EmailConfirmedConsumer>();
            
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(mqConnection["Host"], mqConnection["VirtualHost"], h =>
                {
                    h.Username(mqConnection["Username"]);
                    h.Password(mqConnection["Password"]);
                });

                cfg.ReceiveEndpoint("user-created", e =>
                {
                    e.ConfigureConsumer<UserCreatedConsumer>(ctx);
                });

                cfg.ReceiveEndpoint("transaction-recorded", e =>
                {
                    e.ConfigureConsumer<TransactionRecordedConsumer>(ctx);
                });
                
                cfg.ReceiveEndpoint("email-confirmed", e =>
                {
                    e.ConfigureConsumer<EmailConfirmedConsumer>(ctx);
                });
                
            });
        });

        return services;
    }
}
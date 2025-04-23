using Application.Consumers;
using Application.Consumers.Order;
using Application.Consumers.Wallet;
using Application.Consumers.Tour;
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
            config.AddConsumer<OrderPaidConsumer>();
            config.AddConsumer<WithdrawnConsumer>();
            config.AddConsumer<OrderCanceledConsumer>();
            config.AddConsumer<UserCreatedConsumer>();
            config.AddConsumer<TransactionRecordedConsumer>();
            config.AddConsumer<UserAuthenticatedConsumer>();
            config.AddConsumer<EmailConfirmedConsumer>();
            config.AddConsumer<PasswordForgetConsumer>();
            config.AddConsumer<TourCancelledConsumer>();
            
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(mqConnection["Host"], mqConnection["VirtualHost"], h =>
                {
                    h.Username(mqConnection["Username"]);
                    h.Password(mqConnection["Password"]);
                });

                cfg.ReceiveEndpoint("withdrawn", e =>
                {
                    e.ConfigureConsumer<WithdrawnConsumer>(ctx);
                });
                
                cfg.ReceiveEndpoint("order-paid", e =>
                {
                    e.ConfigureConsumer<OrderPaidConsumer>(ctx);
                });
                      
                cfg.ReceiveEndpoint("order-cancel", e =>
                {
                    e.ConfigureConsumer<OrderCanceledConsumer>(ctx);
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
                
                cfg.ReceiveEndpoint("user-authenticated", e =>
                {
                    e.ConfigureConsumer<UserAuthenticatedConsumer>(ctx);
                });
                
                cfg.ReceiveEndpoint("password-forget", e =>
                {
                    e.ConfigureConsumer<PasswordForgetConsumer>(ctx);
                });

                cfg.ReceiveEndpoint("tour-cancelled", e =>
                {
                    e.ConfigureConsumer<TourCancelledConsumer>(ctx);
                });
                
            });
        });

        return services;
    }
}
using Application.Consumers;
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
            config.AddConsumer<UserRegisteredConsumer>();
            
            config.UsingRabbitMq((ctx, cfg) =>
            {
                // cfg.Host(mqConnection["Host"], mqConnection["VirtualHost"], h =>
                // {
                //     h.Username(mqConnection["Username"]);
                //     h.Password(mqConnection["Password"]);
                // });
                
                cfg.Host("localhost", h =>
                {
                    h.Username("will-e");
                    h.Password("wille");
                });
                
                cfg.ReceiveEndpoint("product-owner", e =>
                {
                    e.ConfigureConsumer<UserRegisteredConsumer>(ctx);
                });
                
            });
        });
        
        return services;
    }
}
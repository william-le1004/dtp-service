using Application.Contracts;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EmailSettings>(options => configuration.GetSection("EmailSettings").Bind(options));
        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
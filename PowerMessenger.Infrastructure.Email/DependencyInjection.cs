using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerMessenger.Application.Layers.Email;
using PowerMessenger.Application.Layers.Email.Services;
using PowerMessenger.Infrastructure.Email.Services;

namespace PowerMessenger.Infrastructure.Email;

public static class DependencyInjection
{
    public static IServiceCollection AddEmail(this IServiceCollection services
        , IConfiguration configuration)
    {
        var emailConfiguration = configuration.GetSection("Email").Get<EmailConfiguration>();

        services.AddSingleton<IEmailService>(email => 
            new EmailService(emailConfiguration!));
        
        return services;
    }
}
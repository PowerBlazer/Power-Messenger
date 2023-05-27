using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerMessenger.Application.Layers.Email.Services;
using PowerMessenger.Infrastructure.Email.Services;

namespace PowerMessenger.Infrastructure.Email;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public static class DependencyInjection
{
    public static IServiceCollection AddEmail(this IServiceCollection services
        , IConfiguration configuration)
    {
        var emailConfiguration = configuration.GetSection("Email").Get<EmailConfiguration>();

        services.AddSingleton<ISmtpEmailService>(_ => 
            new SmtpEmailService(emailConfiguration!));
        
        return services;
    }
}
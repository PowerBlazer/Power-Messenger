using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerMessenger.Application.Layers.Identity.Services;
using PowerMessenger.Infrastructure.Identity.Common;
using PowerMessenger.Infrastructure.Identity.Contexts;
using PowerMessenger.Infrastructure.Identity.Interfaces;
using PowerMessenger.Infrastructure.Identity.Repositories;
using PowerMessenger.Infrastructure.Identity.Services;

namespace PowerMessenger.Infrastructure.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureIdentity(this IServiceCollection services
        , [UsedImplicitly] IConfiguration configuration)
    {
        #region DataBase
        var connectionString = configuration["DB_IDENTITY_CONNECTION_STRING"] is null
            ? configuration.GetConnectionString("LocalDbIdentity")
            : configuration["DB_IDENTITY_CONNECTION_STRING"]!;

        services.AddDbContext<IdentityContext>(options =>
        {
            options.UseNpgsql(
                connectionString,
                provider => provider.EnableRetryOnFailure()
            );
            
            options.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();
        
        #endregion
        
        services.AddSingleton<JwtOptions>(_=> configuration.GetSection("JWT").Get<JwtOptions>()!);

        #region Repositories
        services.AddScoped<IIdentityTokenRepository, IdentityTokenRepository>();
        services.AddScoped<IIdentityUserRepository, IdentityUserRepository>();
        #endregion
        
        #region Services
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ITokenService, TokenService>();
        #endregion
        
        return services;
    }
}
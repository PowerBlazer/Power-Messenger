using System.Reflection;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerMessenger.Application.Features.AuthorizationFeature;
using PowerMessenger.Application.Middlewares;

namespace PowerMessenger.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services
        , [UsedImplicitly] IConfiguration configuration)
    {
        services.AddMediatR(ctg => ctg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly())
            .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));
        
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.StopOnFirstFailure;
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        
        services.AddAutoMapper(typeof(AuthorizationProfile));
        
        return services;
    }
}
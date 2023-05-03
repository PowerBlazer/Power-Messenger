using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerMessenger.Infrastructure.MessageQueues.Common;

namespace PowerMessenger.Infrastructure.MessageQueues;

public static class DependencyInjection
{
    public static IServiceCollection AddMessageQueue(this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitMqConfiguration = configuration.GetSection("RabbitMq").Get<RabbitConfiguration>();
        
        services.AddMassTransit(x =>
        {
            //x.AddConsumer<IUserRegisteredConsumer, UserRegisteredConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri($"rabbitmq://{rabbitMqConfiguration?.Host}:{rabbitMqConfiguration?.Port}/"), h =>
                {
                    h.Username(rabbitMqConfiguration?.UserName);
                    h.Password(rabbitMqConfiguration?.Password);
                });

                // cfg.ReceiveEndpoint("user-registered-queue", e =>
                // {
                //     e.ConfigureConsumer<IUserRegisteredConsumer>(context);
                // });
            });
        });

        return services;
    }
}
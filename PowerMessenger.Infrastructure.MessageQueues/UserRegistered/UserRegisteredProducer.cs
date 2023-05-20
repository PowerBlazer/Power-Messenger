using MassTransit;
using PowerMessenger.Application.Layers.MessageQueues.UserRegistered;

namespace PowerMessenger.Infrastructure.MessageQueues.UserRegistered;

public class UserRegisteredProducer: IUserRegisteredProducer
{
    private readonly IBusControl _busControl;

    public UserRegisteredProducer(IBusControl busControl)
    {
        _busControl = busControl;
    }

    public async Task PublishUserRegistered(UserRegisteredEvent userRegisteredEvent)
    {
        await _busControl.Publish(userRegisteredEvent);
    }
}
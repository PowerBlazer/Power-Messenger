using JetBrains.Annotations;
using MassTransit;
using PowerMessenger.Application.Layers.MessageQueues.UserRegistered;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.MessageQueues.UserRegistered;

[UsedImplicitly]
public class UserRegisteredConsumer: IConsumer<UserRegisteredEvent>
{
    private readonly IUserRepository _userRepository;

    public UserRegisteredConsumer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
    {
        await _userRepository.AddUserAsync(new User
        {
            UserId = context.Message.UserId,
            UserName = context.Message.UserName!
        });
    }
}
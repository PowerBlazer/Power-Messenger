using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.DTOs.Message;

namespace PowerMessenger.Infrastructure.Persistence.Repositories;

public class MessageRepository: IMessageRepository
{
    private readonly IMessengerDapperContext _messengerDapperContext;

    public MessageRepository(IMessengerDapperContext messengerDapperContext)
    {
        _messengerDapperContext = messengerDapperContext;
    }

    public Task<IEnumerable<MessageGroupChatResponse>> GetMessagesGroupChatByUser(long chatId, long userId)
    {
        throw new NotImplementedException();
    }
}
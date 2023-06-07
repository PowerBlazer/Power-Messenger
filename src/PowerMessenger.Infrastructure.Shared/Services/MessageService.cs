using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Application.Layers.Shared.Services;

namespace PowerMessenger.Infrastructure.Shared.Services;

public class MessageService: IMessageService
{
    private readonly IMessageRepository _messageRepository;

    public MessageService(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<bool> ContainMessageInChat(long messageId, long chatId)
    {
        var message = await _messageRepository.GetMessageInTheChatById(messageId, chatId);

        return message is not null;
    }
}
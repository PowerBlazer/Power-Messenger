using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Application.Layers.Shared.Services;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.Shared.Services;

public class MessageService: IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMessageStatusRepository _messageStatusRepository;
    private readonly IMessageTypeRepository _messageTypeRepository;

    public MessageService(IMessageRepository messageRepository, 
        IMessageStatusRepository messageStatusRepository, 
        IMessageTypeRepository messageTypeRepository)
    {
        _messageRepository = messageRepository;
        _messageStatusRepository = messageStatusRepository;
        _messageTypeRepository = messageTypeRepository;
    }

    public async Task<bool> ContainMessageByMessageId(long messageId)
    {
        return await _messageRepository.GetMessageByMessageId(messageId) is not null;
    }

    public async Task<bool> ContainMessageInChatAsync(long messageId, long chatId)
    {
       return await _messageRepository.GetMessageInTheChatByIdAsync(messageId, chatId) is not null;
    }

    public async Task<bool> CheckExistingMessageType(string type)
    {
        return await _messageTypeRepository.GetMessageTypeByTypeAsync(type) is not null;
    }

    public async Task SetMessageAsReadAsync(long chatId, long messageId, long userId)
    {
        var messageStatus = await _messageStatusRepository
            .GetMessageStatusByChatIdAndUserIdAsync(chatId, userId);

        if (messageStatus is null)
        {
            await _messageStatusRepository.AddMessageStatusAsync(new MessageStatus
            {
                UserId = userId,
                ChatId = chatId,
                LastMessageReadId = messageId
            });
            
            return;
        }

        if (messageStatus.LastMessageReadId != messageId)
        {
            messageStatus.LastMessageReadId = messageId;
            await _messageStatusRepository.UpdateMessageStatusRepositoryAsync(messageStatus);
        }
        
    }
}
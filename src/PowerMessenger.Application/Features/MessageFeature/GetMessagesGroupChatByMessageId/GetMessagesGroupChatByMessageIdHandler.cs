using MediatR;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;

namespace PowerMessenger.Application.Features.MessageFeature.GetMessagesGroupChatByMessageId;

public class GetMessagesGroupChatByMessageIdHandler: 
    IRequestHandler<GetMessagesGroupChatByMessageIdQuery,MessagesGroupChatResponse>
{
    private readonly IMessageGroupChatRepository _messageGroupChatRepository;
    private readonly IMessageRepository _messageRepository;

    public GetMessagesGroupChatByMessageIdHandler(IMessageGroupChatRepository messageGroupChatRepository, 
        IMessageRepository messageRepository)
    {
        _messageGroupChatRepository = messageGroupChatRepository;
        _messageRepository = messageRepository;
    }

    public async Task<MessagesGroupChatResponse> Handle(GetMessagesGroupChatByMessageIdQuery request, CancellationToken cancellationToken)
    {
        var messagesGroupChat = (await _messageGroupChatRepository.GetMessagesGroupChatByMessageId(
            request.ChatId,
            request.MessageId,
            request.UserId,
            request.Next,
            request.Prev)).ToList();
        
        var unreadMessagesCount = await _messageRepository.GetUnreadMessagesCountChatAsync(
            request.ChatId,
            request.UserId);

        var nextMessagesCount = 0;
        var prevMessagesCount = 0;

        if (messagesGroupChat.Count > 0)
        {
            nextMessagesCount = await _messageRepository.GetNextMessagesCountChatAsync(
                request.ChatId,
                request.UserId,
                messagesGroupChat.Last().Id);

            prevMessagesCount = await _messageRepository.GetPrevMessagesCountChatAsync(
                request.ChatId,
                request.UserId,
                messagesGroupChat.First().Id);
        }

        return new MessagesGroupChatResponse(
            messagesGroupChat,
            nextMessagesCount,
            prevMessagesCount,
            unreadMessagesCount);
    
    }
}
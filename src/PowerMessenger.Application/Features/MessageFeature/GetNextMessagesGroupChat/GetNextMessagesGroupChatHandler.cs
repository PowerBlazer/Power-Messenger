using JetBrains.Annotations;
using MediatR;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;

namespace PowerMessenger.Application.Features.MessageFeature.GetNextMessagesGroupChat;

[UsedImplicitly]
public class GetNextMessagesGroupChatHandler: IRequestHandler<GetNextMessagesGroupChatQuery,NextMessagesGroupChatResponse>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMessageGroupChatRepository _messageGroupChatRepository;

    public GetNextMessagesGroupChatHandler(IMessageRepository messageRepository, 
        IMessageGroupChatRepository messageGroupChatRepository)
    {
        _messageRepository = messageRepository;
        _messageGroupChatRepository = messageGroupChatRepository;
    }

    public async Task<NextMessagesGroupChatResponse> Handle(GetNextMessagesGroupChatQuery request, CancellationToken cancellationToken)
    {
        var messagesGroup = (await _messageGroupChatRepository.GetNextMessagesGroupChatByUserAsync(
            request.ChatId,
            request.UserId,
            request.MessageId,
            request.Count)).ToList();
        
        var countNextMessages = 0;
        if (messagesGroup.Count != 0)
        {
            countNextMessages = await _messageRepository.GetNextMessagesCountChatAsync(
                request.ChatId,
                request.UserId,
                messagesGroup.Last().Id);
        }
        
        return new NextMessagesGroupChatResponse(messagesGroup, countNextMessages);
    }
}
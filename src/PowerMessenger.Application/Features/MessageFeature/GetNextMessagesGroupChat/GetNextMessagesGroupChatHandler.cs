using JetBrains.Annotations;
using MediatR;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;

namespace PowerMessenger.Application.Features.MessageFeature.GetNextMessagesGroupChat;

[UsedImplicitly]
public class GetNextMessagesGroupChatHandler: IRequestHandler<GetNextMessagesGroupChatQuery,NextMessagesGroupChatResponse>
{
    private readonly IMessageRepository _messageRepository;

    public GetNextMessagesGroupChatHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<NextMessagesGroupChatResponse> Handle(GetNextMessagesGroupChatQuery request, CancellationToken cancellationToken)
    {
        var messagesGroup = (await _messageRepository.GetNextMessagesGroupChatByUserAsync(
            request.ChatId,
            request.UserId,
            request.MessageId,
            request.Count)).ToList();
        
        var countNextMessages = 0;
        if (messagesGroup.Count != 0)
        {
            countNextMessages = await _messageRepository.GetCountNextMessagesChatAsync(
                request.ChatId,
                request.UserId,
                messagesGroup.Last().Id);
        }
        
        return new NextMessagesGroupChatResponse(messagesGroup, countNextMessages);
    }
}
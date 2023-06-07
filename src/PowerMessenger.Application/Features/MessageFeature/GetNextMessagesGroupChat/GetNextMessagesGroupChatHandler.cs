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
        var messagesGroup = await _messageRepository.GetNextMessagesGroupChatByUser(
            request.ChatId,
            request.UserId,
            request.MessageId,
            request.Count);

        var countNextMessages = await _messageRepository.GetCountNextMessagesChat(
            request.ChatId,
            request.UserId,
            request.MessageId);

        return new NextMessagesGroupChatResponse(messagesGroup.ToList(), countNextMessages);
    }
}
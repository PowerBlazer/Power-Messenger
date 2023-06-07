using JetBrains.Annotations;
using MediatR;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;

namespace PowerMessenger.Application.Features.MessageFeature.GetPrevMessagesGroupChat;

[UsedImplicitly]
public class GetPrevMessagesGroupChatHandler: IRequestHandler<GetPrevMessagesGroupChatQuery,PrevMessagesGroupChatResponse>
{
    private readonly IMessageRepository _messageRepository;

    public GetPrevMessagesGroupChatHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<PrevMessagesGroupChatResponse> Handle(GetPrevMessagesGroupChatQuery request, CancellationToken cancellationToken)
    {
        var messagesGroup = await _messageRepository.GetPrevMessagesGroupChatByUser(
            request.ChatId,
            request.UserId,
            request.MessageId,
            request.Count);

        var countNextMessages = await _messageRepository.GetCountPrevMessagesChat(
            request.ChatId,
            request.UserId,
            request.MessageId);

        return new PrevMessagesGroupChatResponse(messagesGroup.ToList(), countNextMessages);
    }
}
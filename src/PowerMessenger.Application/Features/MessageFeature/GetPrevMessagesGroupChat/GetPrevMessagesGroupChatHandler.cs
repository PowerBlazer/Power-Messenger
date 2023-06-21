using JetBrains.Annotations;
using MediatR;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;

namespace PowerMessenger.Application.Features.MessageFeature.GetPrevMessagesGroupChat;

[UsedImplicitly]
public class GetPrevMessagesGroupChatHandler: IRequestHandler<GetPrevMessagesGroupChatQuery,PrevMessagesGroupChatResponse>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMessageGroupChatRepository _messageGroupChatRepository;

    public GetPrevMessagesGroupChatHandler(IMessageRepository messageRepository, 
        IMessageGroupChatRepository messageGroupChatRepository)
    {
        _messageRepository = messageRepository;
        _messageGroupChatRepository = messageGroupChatRepository;
    }

    public async Task<PrevMessagesGroupChatResponse> Handle(GetPrevMessagesGroupChatQuery request, CancellationToken cancellationToken)
    {
        var messagesGroup = (await _messageGroupChatRepository.GetPrevMessagesGroupChatByUserAsync(
            request.ChatId,
            request.UserId,
            request.MessageId,
            request.Count)).ToList();
        
        var countPrevMessages = 0;
        if (messagesGroup.Count > 0)
        {
            countPrevMessages = await _messageRepository.GetPrevMessagesCountChatAsync(
                request.ChatId,
                request.UserId,
                messagesGroup.First().Id);
        }

        return new PrevMessagesGroupChatResponse(messagesGroup, countPrevMessages);
    }
}
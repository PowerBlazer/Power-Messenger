using JetBrains.Annotations;
using MediatR;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;
// ReSharper disable InvertIf

namespace PowerMessenger.Application.Features.MessageFeature.GetMessagesGroupChat;

[UsedImplicitly]
public class GetChatsByUserHandler: IRequestHandler<GetMessagesGroupChatQuery,MessagesGroupChatResponse>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMessageGroupChatRepository _messageGroupChatRepository;

    public GetChatsByUserHandler(IMessageRepository messageRepository, IMessageGroupChatRepository messageGroupChatRepository)
    {
        _messageRepository = messageRepository;
        _messageGroupChatRepository = messageGroupChatRepository;
    }

    public async Task<MessagesGroupChatResponse> Handle(GetMessagesGroupChatQuery request, CancellationToken cancellationToken)
    {
       var messages = (await _messageGroupChatRepository.GetMessagesGroupChatByUserAsync(
           request.ChatId,
           request.UserId,
           request.Next,
           request.Prev
       )).ToList();

       var unreadMessagesCount = await _messageRepository.GetUnreadMessagesCountChatAsync(
           request.ChatId,
           request.UserId);

       var nextMessagesCount = 0;
       var prevMessagesCount = 0;

       if (messages.Count > 0)
       {
           nextMessagesCount = await _messageRepository.GetNextMessagesCountChatAsync(
               request.ChatId,
               request.UserId,
               messages.Last().Id);

           prevMessagesCount = await _messageRepository.GetPrevMessagesCountChatAsync(
               request.ChatId,
               request.UserId,
               messages.First().Id);
       }

       return new MessagesGroupChatResponse(
           messages,
           nextMessagesCount,
           prevMessagesCount,
           unreadMessagesCount);
    }
}
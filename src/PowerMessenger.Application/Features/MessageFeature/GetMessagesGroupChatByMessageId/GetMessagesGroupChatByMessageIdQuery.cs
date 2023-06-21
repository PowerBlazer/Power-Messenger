using MediatR;
using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;

namespace PowerMessenger.Application.Features.MessageFeature.GetMessagesGroupChatByMessageId;

public record GetMessagesGroupChatByMessageIdQuery
    (long ChatId, long UserId, int Next, int Prev) : IRequest<MessagesGroupChatResponse>;

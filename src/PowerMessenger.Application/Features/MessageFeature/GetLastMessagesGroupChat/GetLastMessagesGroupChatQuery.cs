using MediatR;
using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;

namespace PowerMessenger.Application.Features.MessageFeature.GetLastMessagesGroupChat;

public record GetLastMessagesGroupChatQuery(long ChatId, long UserId,int Count): IRequest<MessagesGroupChatResponse>;

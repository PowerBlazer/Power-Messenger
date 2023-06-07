using MediatR;
using PowerMessenger.Domain.DTOs.Message;
using PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;

namespace PowerMessenger.Application.Features.MessageFeature.GetMessagesGroupChat;

public record GetMessagesGroupChatQuery(long ChatId,long UserId,int Next,int Prev) : IRequest<MessagesGroupChatResponse>;

using MediatR;
using PowerMessenger.Domain.DTOs.Chat;

namespace PowerMessenger.Application.Features.ChatFeature.GetChatsByUser;

public record GetChatsByUserQuery(long UserId): IRequest<IList<ChatResponse>>;
using JetBrains.Annotations;

namespace PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;

[UsedImplicitly]
public record NextMessagesGroupChatResponse(IList<MessageGroupChatResponse> Messages,int NextCount);
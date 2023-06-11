using JetBrains.Annotations;

namespace PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;

[UsedImplicitly]
public record MessagesGroupChatResponse(
    IList<MessageGroupChatResponse> Messages, 
    int NextMessagesCount,
    int PrevMessagesCount,
    int UnreadMessagesCount);
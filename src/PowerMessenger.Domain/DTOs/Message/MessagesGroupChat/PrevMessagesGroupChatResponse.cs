using JetBrains.Annotations;

namespace PowerMessenger.Domain.DTOs.Message.MessagesGroupChat;

[UsedImplicitly]
public record PrevMessagesGroupChatResponse(IList<MessageGroupChatResponse> Messages,int PrevCount);
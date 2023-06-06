

namespace PowerMessenger.Domain.DTOs.Message;

public class MessagesGroupChatResponse
{
   public MessagesGroupChatResponse(IList<MessageGroupChatResponse> messages, int countUnreadMessages)
   {
      Messages = messages;
      CountUnreadMessages = countUnreadMessages;
   }

   public IList<MessageGroupChatResponse> Messages { get; }
   public int CountUnreadMessages { get; }
}
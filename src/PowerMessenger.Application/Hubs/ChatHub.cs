using Microsoft.AspNetCore.Authorization;
using PowerMessenger.Application.Layers.Persistence.Repositories;


namespace PowerMessenger.Application.Hubs;

[Authorize]
public class ChatHub: BaseHub
{
    private readonly IChatParticipantsRepository _chatParticipantsRepository;
    private readonly IChatRepository _chatRepository;

    public ChatHub(IChatParticipantsRepository chatParticipantsRepository, 
        IChatRepository chatRepository)
    {
        _chatParticipantsRepository = chatParticipantsRepository;
        _chatRepository = chatRepository;
    }
    
    public override async Task OnConnectedAsync()
    {
        var chats = await _chatRepository.GetChatsByUserAsync(UserId);

        foreach (var chat in chats)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,chat.Id.ToString());
        }
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        
        var chats = await _chatRepository.GetChatsByUserAsync(UserId);

        foreach (var chat in chats)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chat.Id.ToString());
        }
    }
}
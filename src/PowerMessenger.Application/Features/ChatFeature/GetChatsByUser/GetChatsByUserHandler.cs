using JetBrains.Annotations;
using MediatR;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Application.Layers.Redis.Services;
using PowerMessenger.Domain.Common;
using PowerMessenger.Domain.DTOs.Chat;

namespace PowerMessenger.Application.Features.ChatFeature.GetChatsByUser;

[UsedImplicitly]
public class GetChatsByUserHandler: IRequestHandler<GetChatsByUserQuery,IList<ChatResponse>>
{
    private readonly IChatRepository _chatRepository;
    private readonly ICacheService _cacheService;

    public GetChatsByUserHandler(IChatRepository chatRepository, 
        ICacheService cacheService)
    {
        _chatRepository = chatRepository;
        _cacheService = cacheService;
    }

    public async Task<IList<ChatResponse>> Handle(GetChatsByUserQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = RedisCacheKeys.GetKeyChatsByUser(request.UserId);
        var cachedChats = await _cacheService.GetValue<IList<ChatResponse>>(cacheKey);

        if (cachedChats != null)
        {
            return cachedChats;
        }

        var userChats = (await _chatRepository.GetChatsByUser(request.UserId)).ToList();

        await _cacheService.SetData(cacheKey, userChats, TimeSpan.FromMinutes(5));

        return userChats;
    }
}
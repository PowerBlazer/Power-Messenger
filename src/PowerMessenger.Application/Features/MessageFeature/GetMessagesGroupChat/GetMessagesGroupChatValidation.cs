using FluentValidation;
using PowerMessenger.Application.Layers.Shared.Services;

namespace PowerMessenger.Application.Features.MessageFeature.GetMessagesGroupChat;

public class GetMessagesGroupChatValidation: AbstractValidator<GetMessagesGroupChatQuery>
{
    public GetMessagesGroupChatValidation(IChatService chatService)
    {
        RuleFor(p => p.ChatId)
            .NotEmpty()
            .WithMessage("Поле не может быть пустым")
            .MustAsync(async (chatId, _) => await chatService.CheckChatExistenceById(chatId))
            .WithMessage("Такого чата не сущесвует");

        RuleFor(p => p.UserId)
            .NotEmpty()
            .WithMessage("Поле не может быть пустым")
            .MustAsync(async (request, userId, _) => await chatService.ContainUserInChat(request.ChatId, userId))
            .WithMessage("Пользователь не является участником чата");
    }
}
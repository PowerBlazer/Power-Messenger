using FluentValidation;
using PowerMessenger.Application.Layers.Shared.Services;

namespace PowerMessenger.Application.Features.MessageFeature.GetLastMessagesGroupChat;

public class GetLastMessagesGroupChatValidator: AbstractValidator<GetLastMessagesGroupChatQuery>
{
    public GetLastMessagesGroupChatValidator(IChatService chatService)
    {
        RuleFor(p => p.ChatId)
            .NotEmpty()
            .WithMessage("Поле не может быть пустым")
            .MustAsync(async (chatId, _) => await chatService.CheckChatExistenceByIdAsync(chatId))
            .WithMessage("Такого чата не сущесвует")
            .MustAsync(async (chatId,_) => await chatService.ValidateChatTypeAsync(chatId,"Group"))
            .WithMessage("Не соответсвует тип чата");

        RuleFor(p => p.UserId)
            .NotEmpty()
            .WithMessage("Поле не может быть пустым")
            .MustAsync(async (request, userId, _) => await chatService.ContainUserInChatAsync(request.ChatId, userId))
            .WithMessage("Пользователь не является участником чата");

        RuleFor(p => p.Count)
            .NotNull()
            .WithMessage("Поле не может быть пустым");

    }
}
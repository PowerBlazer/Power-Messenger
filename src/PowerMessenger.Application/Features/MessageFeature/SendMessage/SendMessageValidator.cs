using FluentValidation;
using JetBrains.Annotations;
using PowerMessenger.Application.Layers.Shared.Services;

namespace PowerMessenger.Application.Features.MessageFeature.SendMessage;

[UsedImplicitly]
public class SendMessageValidator: AbstractValidator<SendMessageCommand>
{
    public SendMessageValidator(IMessageService messageService,
        IChatService chatService)
    {
        RuleFor(p => p.ChatId)
            .NotEmpty()
            .WithMessage("Поле не может быть пустым")
            .MustAsync(async (chatId, _) => await chatService.CheckChatExistenceByIdAsync(chatId))
            .WithMessage("Такого чата не сущесвует");
        
        RuleFor(p => p.UserId)
            .NotEmpty()
            .WithMessage("Поле не может быть пустым")
            .MustAsync(async (request, userId, _) => 
                await chatService.ContainUserInChatAsync(request.ChatId, userId))
            .WithMessage("Пользователь не является участником чата");

        RuleFor(p => p.Type)
            .NotEmpty()
            .WithMessage("Поле не может быть пустым")
            .MustAsync(async (type, _) => await messageService.CheckExistingMessageType(type))
            .WithMessage("Такого типа не существует");
    }
}
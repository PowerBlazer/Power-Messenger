using FluentValidation;
using JetBrains.Annotations;
using PowerMessenger.Application.Layers.Shared.Services;

namespace PowerMessenger.Application.Features.MessageFeature.SetMessageAsRead;

[UsedImplicitly]
public class SetMessageAsReadValidator: AbstractValidator<SetMessageAsReadCommand>
{
    public SetMessageAsReadValidator(IChatService chatService,
        IMessageService messageService)
    {
        RuleFor(p => p.ChatId)
            .NotNull()
            .NotEmpty()
            .WithMessage("Поле не может быть пустым")
            .MustAsync(async (chatId, _) => await chatService.CheckChatExistenceByIdAsync(chatId))
            .WithMessage("Такого чата не сущесвует");
            
        
        RuleFor(p => p.UserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("Поле не может быть пустым")
            .MustAsync(async (request, userId, _) => 
                await chatService.ContainUserInChatAsync(request.ChatId, userId))
            .WithMessage("Пользователь не является участником чата");
        
        RuleFor(p => p.MessageId)
            .NotNull()
            .NotEmpty()
            .WithMessage("Поле не может быть пустым")
            .MustAsync(async (request, messageId, _) =>
                await messageService.ContainMessageInChatAsync(messageId, request.ChatId))
            .WithMessage("Такое сообщение в чате отсутствует");
    }
}
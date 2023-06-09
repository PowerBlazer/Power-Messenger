﻿using FluentValidation;
using JetBrains.Annotations;
using PowerMessenger.Application.Layers.Shared.Services;

namespace PowerMessenger.Application.Features.MessageFeature.GetMessagesGroupChat;

[UsedImplicitly]
public class GetMessagesGroupChatValidator: AbstractValidator<GetMessagesGroupChatQuery>
{
    public GetMessagesGroupChatValidator(IChatService chatService)
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

        RuleFor(p => p.Next)
            .NotNull()
            .WithMessage("Поле не может быть пустым")
            .Must(next => next >= 10)
            .WithMessage("Не может быть меньше 10");
        
        RuleFor(p => p.Prev)
            .NotNull()
            .WithMessage("Поле не может быть пустым")
            .Must(prev => prev >= 10)
            .WithMessage("Не может быть меньше 10");
    }
}
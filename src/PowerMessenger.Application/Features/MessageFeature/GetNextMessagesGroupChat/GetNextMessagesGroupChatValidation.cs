﻿using FluentValidation;
using PowerMessenger.Application.Layers.Shared.Services;

namespace PowerMessenger.Application.Features.MessageFeature.GetNextMessagesGroupChat;

public class GetNextMessagesGroupChatValidation: AbstractValidator<GetNextMessagesGroupChatQuery>
{
    public GetNextMessagesGroupChatValidation(IChatService chatService,
        IMessageService messageService)
    {
        RuleFor(p => p.ChatId)
            .NotEmpty()
            .WithMessage("Поле не может быть пустым")
            .MustAsync(async (chatId, _) => await chatService.CheckChatExistenceById(chatId))
            .WithMessage("Такого чата не сущесвует")
            .MustAsync(async (chatId,_) => await chatService.ValidateChatType(chatId,"Group"))
            .WithMessage("Не соответсвует тип чата");
        
        RuleFor(p => p.UserId)
            .NotEmpty()
            .WithMessage("Поле не может быть пустым")
            .MustAsync(async (request, userId, _) => await chatService.ContainUserInChat(request.ChatId, userId))
            .WithMessage("Пользователь не является участником чата");
        
        RuleFor(p => p.Count)
            .NotNull()
            .WithMessage("Поле не может быть пустым")
            .Must(next => next >= 10)
            .WithMessage("Не может быть меньше 10");

        RuleFor(p => p.MessageId)
            .NotNull()
            .NotEmpty()
            .WithMessage("Поле не может быть пустым")
            .MustAsync(async (request, messageId, _) =>
                await messageService.ContainMessageInChat(messageId, request.ChatId))
            .WithMessage("Такое сообщение в чате отсутствует");

    }
}
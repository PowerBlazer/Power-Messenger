﻿using FluentValidation;
using PowerMessenger.Application.Layers.Identity.Services;

namespace PowerMessenger.Application.Features.AuthorizationFeature.SendEmailVerificationCode;

public class SendEmailVerificationValidation: AbstractValidator<SendEmailVerificationCommand>
{
    public SendEmailVerificationValidation(IEmailService emailService)
    {
        RuleFor(p => p.Email)
            .NotEmpty()
                .WithMessage("Поле не может быть пустым")
            .Must(email => emailService.ValidationEmail(email!))
                .WithMessage("Неправильный формат почты")
            .MustAsync(async (email, _) => !await emailService.ContainEmailAsync(email!))
                .WithMessage("Пользователь с такой почтой уже зарегестрирован");
    }
}
﻿using FluentValidation;

namespace PowerMessenger.Application.Features.AuthorizationFeature.RegisterUser;

public class RegisterUserValidation: AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidation()
    {
        RuleFor(p => p.SessionId)
            .NotEmpty().WithMessage("Поле не может быть пустым")
            .NotNull().WithMessage("Поле не может быть пустым");

        RuleFor(p => p.Password)
            .NotNull().WithMessage("Поле не может быть пустым")
            .NotEmpty().WithMessage("Поле не может быть пустым")
            .MinimumLength(5).WithMessage("Длина пароля должно быть больше 5 символов")
            .Equal(p => p.PasswordConfirm).WithMessage("Пароли не совпадают");

        RuleFor(p => p.UserName)
            .NotNull().WithMessage("Поле не может быть пустым")
            .NotEmpty().WithMessage("Поле не может быть пустым")
            .MinimumLength(3).WithMessage("Длина имя пользователя должно быть больше 3 символов");
    }
}
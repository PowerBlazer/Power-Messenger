using FluentValidation;
using JetBrains.Annotations;
using PowerMessenger.Application.Layers.Identity.Services;

namespace PowerMessenger.Application.Features.AuthorizationFeature.LoginUser;

[UsedImplicitly]
public class LoginUserValidation: AbstractValidator<LoginUserCommand>
{
    public LoginUserValidation(IEmailService emailService)
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage("Поле не может быть пустым")
            .MustAsync(async (email, _) => await emailService.ContainEmailAsync(email))
            .WithMessage("Пользователь с такой почтой не зарегестрирован");

        RuleFor(p => p.Password)
            .NotEmpty()
            .WithMessage("Поле не может быть пустым");

    }
}
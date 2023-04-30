using FluentValidation;
using JetBrains.Annotations;
using PowerMessenger.Application.Services;

namespace PowerMessenger.Application.Features.AuthorizationFeature.Login;

[UsedImplicitly]
public class LoginValidator: AbstractValidator<LoginCommand>
{
    public LoginValidator(IAccountService accountService)
    {
        RuleFor(p => p.Email)
            .NotNull().WithMessage("Поле не может быть пустым")
            .MustAsync(async (email,_)=> await accountService.ContainEmail(email!)).WithMessage("Такой email не зарегестрирован");
        
        RuleFor(p => p.Password)
            .NotNull().WithMessage("Поле не может быть пустым");
    }
}
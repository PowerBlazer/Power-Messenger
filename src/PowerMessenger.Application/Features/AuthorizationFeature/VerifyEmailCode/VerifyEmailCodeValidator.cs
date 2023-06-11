using FluentValidation;
using JetBrains.Annotations;

namespace PowerMessenger.Application.Features.AuthorizationFeature.VerifyEmailCode;

[UsedImplicitly]
public class VerifyEmailCodeValidator: AbstractValidator<VerifyEmailCodeCommand>
{
    public VerifyEmailCodeValidator()
    {
        RuleFor(p => p.SessionId)
            .NotEmpty().WithMessage("Поле не может быть пустым");

        RuleFor(p => p.VerificationCode)
            .NotEmpty().WithMessage("Поле не может быть пустым");
    }
}
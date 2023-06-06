using FluentValidation;
using JetBrains.Annotations;

namespace PowerMessenger.Application.Features.AuthorizationFeature.RefreshToken;

[UsedImplicitly]
public class RefreshTokenValidation: AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidation()
    {
        RuleFor(p => p.AccessToken)
            .NotEmpty().WithMessage("Поле не может быть пустым");

        RuleFor(p => p.RefreshToken)
            .NotEmpty().WithMessage("Поле не может быть пустым");
    }
}
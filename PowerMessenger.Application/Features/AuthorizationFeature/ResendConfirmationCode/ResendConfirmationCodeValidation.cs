using FluentValidation;

namespace PowerMessenger.Application.Features.AuthorizationFeature.ResendConfirmationCode;

public class ResendConfirmationCodeValidation: AbstractValidator<ResendConfirmationCodeCommand>
{
    public ResendConfirmationCodeValidation()
    {
        RuleFor(p => p.SessionId)
            .NotNull().WithMessage("Поле не может быть пустым");
        
        RuleFor(p=>p.Email)
            .NotNull().WithMessage("Поле не может быть пустым");
    }
}
using FluentValidation;

namespace PowerMessenger.Application.Features.AuthorizationFeature.RefreshToken;

public class RefreshTokenValidation: AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidation()
    {
        
    }
}
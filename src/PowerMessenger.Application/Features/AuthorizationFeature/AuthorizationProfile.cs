using AutoMapper;
using PowerMessenger.Application.DTOs.Authorization;
using PowerMessenger.Application.Features.AuthorizationFeature.LoginUser;
using PowerMessenger.Application.Features.AuthorizationFeature.RefreshToken;
using PowerMessenger.Application.Features.AuthorizationFeature.RegisterUser;

namespace PowerMessenger.Application.Features.AuthorizationFeature;

public class AuthorizationProfile: Profile
{
    public AuthorizationProfile()
    {
        CreateMap<RegistrationInput,RegisterUserCommand>()
            .ReverseMap();
        CreateMap<LoginInput, LoginUserCommand>()
            .ReverseMap();
        CreateMap<RefreshTokenInput, RefreshTokenCommand>()
            .ReverseMap();
    }
}
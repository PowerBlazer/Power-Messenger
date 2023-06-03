using AutoMapper;
using PowerMessenger.Application.Features.AuthorizationFeature.LoginUser;
using PowerMessenger.Application.Features.AuthorizationFeature.RefreshToken;
using PowerMessenger.Application.Features.AuthorizationFeature.RegisterUser;
using PowerMessenger.Domain.DTOs.Authorization;

namespace PowerMessenger.Application.Features.AuthorizationFeature;

public class AuthorizationProfile: Profile
{
    public AuthorizationProfile()
    {
        CreateMap<RegistrationRequest,RegisterUserCommand>()
            .ReverseMap();
        CreateMap<LoginRequest, LoginUserCommand>()
            .ReverseMap();
        CreateMap<RefreshTokenRequest, RefreshTokenCommand>()
            .ReverseMap();
    }
}
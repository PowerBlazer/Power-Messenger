using AutoMapper;
using PowerMessenger.Application.DTOs.Authorization;
using PowerMessenger.Application.Features.AuthorizationFeature.Login;
using PowerMessenger.Application.Features.AuthorizationFeature.Register;

namespace PowerMessenger.Application.Features;

public class AuthorizationProfile: Profile
{
    public AuthorizationProfile()
    {
        CreateMap<LoginDto, LoginCommand>()
            .ReverseMap();
        CreateMap<RegisterDto, RegisterCommand>()
            .ReverseMap();
    }
}
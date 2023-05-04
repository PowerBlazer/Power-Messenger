using AutoMapper;
using PowerMessenger.Application.DTOs.Authorization;
using PowerMessenger.Application.Features.AuthorizationFeature.Login;

namespace PowerMessenger.Application.Features.AuthorizationFeature;

public class AuthorizationProfile: Profile
{
    public AuthorizationProfile()
    {
        CreateMap<LoginDto, LoginCommand>()
            .ReverseMap();
    }
}
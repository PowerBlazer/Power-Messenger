using AutoMapper;
using PowerMessenger.Application.DTOs.Authorization;
using PowerMessenger.Application.Features.AuthorizationFeature.Login;
using PowerMessenger.Application.Features.AuthorizationFeature.RegisterUser;

namespace PowerMessenger.Application.Features.AuthorizationFeature;

public class AuthorizationProfile: Profile
{
    public AuthorizationProfile()
    {
        CreateMap<LoginDto, LoginCommand>()
            .ReverseMap();

        CreateMap<RegisterUserCommand, RegistrationInput>()
            .ReverseMap();
    }
}
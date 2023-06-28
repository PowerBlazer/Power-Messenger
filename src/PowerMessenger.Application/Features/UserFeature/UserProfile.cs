using AutoMapper;
using PowerMessenger.Domain.DTOs.User;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Features.UserFeature;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDataResponse>()
            .ReverseMap();
    }
}
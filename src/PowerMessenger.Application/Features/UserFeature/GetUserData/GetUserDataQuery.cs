using MediatR;
using PowerMessenger.Domain.DTOs.User;

namespace PowerMessenger.Application.Features.UserFeature.GetUserData;

public record GetUserDataQuery(long UserId): IRequest<UserDataResponse>;

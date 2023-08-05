using AutoMapper;
using MediatR;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.DTOs.User;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Features.UserFeature.GetUserData;

public class GetUserDataHandler: IRequestHandler<GetUserDataQuery,UserDataResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserDataHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDataResponse> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(p=>p.UserId == request.UserId);

        var userResponse = _mapper.Map<User, UserDataResponse>(user!);

        return userResponse;
    }
}
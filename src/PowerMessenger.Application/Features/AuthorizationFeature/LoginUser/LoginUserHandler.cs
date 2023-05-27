using AutoMapper;
using MediatR;
using PowerMessenger.Application.DTOs.Authorization;
using PowerMessenger.Application.Layers.Identity.Services;

namespace PowerMessenger.Application.Features.AuthorizationFeature.LoginUser;

public class LoginUserHandler: IRequestHandler<LoginUserCommand,LoginResult>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;

    public LoginUserHandler(IAuthorizationService authorizationService, 
        IMapper mapper)
    {
        _authorizationService = authorizationService;
        _mapper = mapper;
    }

    public async Task<LoginResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var loginInput = _mapper.Map<LoginUserCommand,LoginInput>(request);

        return await _authorizationService.LoginUserAsync(loginInput);
    }
}
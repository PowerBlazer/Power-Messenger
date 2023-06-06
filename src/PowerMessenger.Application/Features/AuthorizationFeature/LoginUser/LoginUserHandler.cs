using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using PowerMessenger.Application.Layers.Identity.Services;
using PowerMessenger.Domain.DTOs.Authorization;

namespace PowerMessenger.Application.Features.AuthorizationFeature.LoginUser;

[UsedImplicitly]
public class LoginUserHandler: IRequestHandler<LoginUserCommand,LoginResponse>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;

    public LoginUserHandler(IAuthorizationService authorizationService, 
        IMapper mapper)
    {
        _authorizationService = authorizationService;
        _mapper = mapper;
    }

    public async Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var loginInput = _mapper.Map<LoginUserCommand,LoginRequest>(request);

        return await _authorizationService.LoginUserAsync(loginInput);
    }
}
using AutoMapper;
using MediatR;
using PowerMessenger.Application.Common;
using PowerMessenger.Application.DTOs.Authorization;
using PowerMessenger.Application.Layers.Identity.Services;

namespace PowerMessenger.Application.Features.AuthorizationFeature.Login;

public class LoginHandler: IRequestHandler<LoginCommand,AuthorizationResult>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;

    public LoginHandler(IAuthorizationService authorizationService, IMapper mapper)
    {
        _authorizationService = authorizationService;
        _mapper = mapper;
    }

    public async Task<AuthorizationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        throw new Exception();
    }
}
using AutoMapper;
using MediatR;
using PowerMessenger.Application.DTOs.Authorization;
using PowerMessenger.Application.Services;
using PowerMessenger.Domain.Common;

namespace PowerMessenger.Application.Features.AuthorizationFeature.Login;

public class LoginHandler: IRequestHandler<LoginCommand,AuthorizationResult>
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public LoginHandler(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    public async Task<AuthorizationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var loginDto = _mapper.Map<LoginCommand, LoginDto>(request);

        var authorizationResult = await _accountService.Login(loginDto);

        return authorizationResult;
    }
}
using AutoMapper;
using MediatR;
using PowerMessenger.Application.DTOs.Authorization;
using PowerMessenger.Application.Services;
using PowerMessenger.Domain.Common;

namespace PowerMessenger.Application.Features.AuthorizationFeature.Register;

public class RegisterHandler: IRequestHandler<RegisterCommand,AuthorizationResult>
{
    private readonly IMapper _mapper;
    private readonly IAccountService _accountService;

    public RegisterHandler(IMapper mapper, IAccountService accountService)
    {
        _mapper = mapper;
        _accountService = accountService;
    }

    public async Task<AuthorizationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var registerDto = _mapper.Map<RegisterCommand, RegisterDto>(request);

        var authorizationResult = await _accountService.Register(registerDto);

        return authorizationResult;
    }
}
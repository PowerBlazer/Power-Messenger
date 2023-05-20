using AutoMapper;
using MediatR;
using PowerMessenger.Application.DTOs.Authorization;
using PowerMessenger.Application.Layers.Identity.Services;

namespace PowerMessenger.Application.Features.AuthorizationFeature.RegisterUser;

public class RegisterUserHandler: IRequestHandler<RegisterUserCommand,RegistrationResult>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;

    public RegisterUserHandler(IAuthorizationService authorizationService, 
        IMapper mapper)
    {
        _authorizationService = authorizationService;
        _mapper = mapper;
    }

    public async Task<RegistrationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var registerInput = _mapper.Map<RegisterUserCommand, RegistrationInput>(request);

        return await _authorizationService.RegisterUserAsync(registerInput);
    }
}
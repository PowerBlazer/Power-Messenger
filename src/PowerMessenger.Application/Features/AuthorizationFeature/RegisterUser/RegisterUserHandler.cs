﻿using AutoMapper;
using MediatR;
using PowerMessenger.Application.Layers.Identity.Services;
using PowerMessenger.Domain.DTOs.Authorization;

namespace PowerMessenger.Application.Features.AuthorizationFeature.RegisterUser;

public class RegisterUserHandler: IRequestHandler<RegisterUserCommand,RegistrationResponse>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;

    public RegisterUserHandler(IAuthorizationService authorizationService, 
        IMapper mapper)
    {
        _authorizationService = authorizationService;
        _mapper = mapper;
    }

    public async Task<RegistrationResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var registerInput = _mapper.Map<RegisterUserCommand, RegistrationRequest>(request);

        return await _authorizationService.RegisterUserAsync(registerInput);
    }
}
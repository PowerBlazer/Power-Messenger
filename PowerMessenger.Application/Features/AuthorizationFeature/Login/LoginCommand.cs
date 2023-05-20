﻿using JetBrains.Annotations;
using MediatR;
using PowerMessenger.Application.Common;

namespace PowerMessenger.Application.Features.AuthorizationFeature.Login;

[UsedImplicitly]
public class LoginCommand: IRequest<AuthorizationResult>
{
    public string? Email { get; set; } 
    public string? Password { get; set; }
}
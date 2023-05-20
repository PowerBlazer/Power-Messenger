﻿using JetBrains.Annotations;
using MediatR;

namespace PowerMessenger.Application.Features.AuthorizationFeature.SendEmailVerificationCode;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class SendEmailVerificationCommand: IRequest<string>
{
    /// <summary>
    /// Почта для подтверждения
    /// </summary>
    public string? Email { get; set; }
}
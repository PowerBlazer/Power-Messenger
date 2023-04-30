﻿using JetBrains.Annotations;

namespace PowerMessenger.Domain.Common;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class AuthorizationResult
{
    public AuthorizationResult()
    {
        IsSuccess = true;
    }
    
    public bool IsSuccess { get; set; }
    public Dictionary<string, List<string>> Errors { get; set; } = new();
    public string? Token { get; set; }
    
    public void Failed(Dictionary<string, List<string>> errors)
    {
        IsSuccess = false;

        Errors = errors;
    }
}
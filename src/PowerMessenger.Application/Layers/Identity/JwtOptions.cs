﻿using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PowerMessenger.Application.Layers.Identity;

public class JwtOptions
{
    public string? Issuer { get; set; }
    public string? Audience { get; set; }

    public string? Secret { get; set; }
    public int AccessExpirationMinutes { get; set; }
    public int RefreshExpirationDays { get; set; }
    
    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret!));
    }
}
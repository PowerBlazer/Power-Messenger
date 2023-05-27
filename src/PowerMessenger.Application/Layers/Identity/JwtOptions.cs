using System.Text;
using JetBrains.Annotations;
using Microsoft.IdentityModel.Tokens;

namespace PowerMessenger.Application.Layers.Identity;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
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
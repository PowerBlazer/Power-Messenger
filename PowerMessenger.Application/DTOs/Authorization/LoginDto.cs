using JetBrains.Annotations;

namespace PowerMessenger.Application.DTOs.Authorization;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class LoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
}
using JetBrains.Annotations;

namespace PowerMessenger.Application.DTOs.Authorization;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class RegisterDto
{
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
}
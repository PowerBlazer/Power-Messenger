using PowerMessenger.Domain.Entities.Abstractions;

namespace PowerMessenger.Infrastructure.Identity.Entities;

public class IdentityUser:BaseEntity<long>
{
    public string? Email { get; set; }
    public string? PasswordHash { get; set; } 
    public string? PhoneNumber { get; set; }
    public DateTime DateCreated { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    
    public IdentityToken? IdentityToken { get; set; }
}
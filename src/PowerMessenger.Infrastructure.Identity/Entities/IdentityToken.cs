using PowerMessenger.Domain.Entities.Abstractions;

namespace PowerMessenger.Infrastructure.Identity.Entities;

public class IdentityToken: BaseEntity<long>
{
    public long UserId { get; set; }
    public string? Token { get; set; }
    public DateTime Expiration { get; set; }
    public IdentityUser? User { get; set; }
}
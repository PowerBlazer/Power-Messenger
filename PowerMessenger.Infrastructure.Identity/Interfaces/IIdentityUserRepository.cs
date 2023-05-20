using PowerMessenger.Infrastructure.Identity.Entities;

namespace PowerMessenger.Infrastructure.Identity.Interfaces;

public interface IIdentityUserRepository
{
    Task<IdentityUser> AddUserAsync(IdentityUser identityUser);
    Task<IdentityUser?> GetUserByEmail(string email);
}

using Microsoft.EntityFrameworkCore;
using PowerMessenger.Infrastructure.Identity.Contexts;
using PowerMessenger.Infrastructure.Identity.Entities;
using PowerMessenger.Infrastructure.Identity.Interfaces;

namespace PowerMessenger.Infrastructure.Identity.Repositories;

public class IdentityUserRepository: IIdentityUserRepository
{
    private readonly IdentityContext _identityContext;

    public IdentityUserRepository(IdentityContext identityContext)
    {
        _identityContext = identityContext;
    }

    public async Task<IdentityUser> AddUserAsync(IdentityUser identityUser)
    {
        var userEntity = await _identityContext.IdentityUsers.AddAsync(identityUser);

        await _identityContext.SaveChangesAsync();

        return userEntity.Entity;
    }

    public async Task<IdentityUser?> GetUserByEmail(string email)
    {
        return await _identityContext.IdentityUsers.FirstOrDefaultAsync(p => p.Email == email);
    }
}
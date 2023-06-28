using Microsoft.EntityFrameworkCore;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.Persistence.Repositories;

public class UserRepository: IUserRepository
{
    private readonly IMessengerEfContext _efContext;

    public UserRepository(IMessengerEfContext efContext)
    {
        _efContext = efContext;
    }

    public async Task<User> AddUserAsync(User user)
    {
        var result = await _efContext.Users.AddAsync(user);

        await _efContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<User?> GetUserByIdAsync(long userId)
    {
        return await _efContext.Users
            .FirstOrDefaultAsync(p => p.UserId == userId);
    }
}
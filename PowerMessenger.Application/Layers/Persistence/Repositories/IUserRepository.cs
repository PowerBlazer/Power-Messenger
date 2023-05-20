using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user);
}
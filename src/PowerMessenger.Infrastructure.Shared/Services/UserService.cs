using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Application.Layers.Shared.Services;

namespace PowerMessenger.Infrastructure.Shared.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> ContainUserById(long userId)
    {
        return await _userRepository.GetUserByIdAsync(userId) is not null;
    }
}
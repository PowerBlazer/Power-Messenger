namespace PowerMessenger.Application.Layers.Shared.Services;

public interface IUserService
{
    Task<bool> ContainUserById(long userId);
}
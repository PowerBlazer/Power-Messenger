using Microsoft.EntityFrameworkCore;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Application.Layers.Persistence.Repository;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Infrastructure.Persistence.Repositories;

public class UserRepository: RepositoryBase<User>, IUserRepository
{
    private readonly IMessengerEfContext _messengerEfContext;

    public UserRepository(IMessengerEfContext messengerEfContext): base(messengerEfContext)
    {
        _messengerEfContext = messengerEfContext;
    }
    
}
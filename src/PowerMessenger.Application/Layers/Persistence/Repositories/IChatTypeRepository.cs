﻿using PowerMessenger.Application.Layers.Persistence.Repository.Abstraction;
using PowerMessenger.Domain.Entities;

namespace PowerMessenger.Application.Layers.Persistence.Repositories;

public interface IChatTypeRepository: IRepository<ChatType>
{
    Task<ChatType?> GetChatTypeByTypeAsync(string type);
}
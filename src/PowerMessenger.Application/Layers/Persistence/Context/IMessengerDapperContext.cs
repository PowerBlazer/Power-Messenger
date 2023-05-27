using System.Data;

namespace PowerMessenger.Application.Layers.Persistence.Context;

public interface IMessengerDapperContext
{
    IDbConnection CreateNpgConnection();
}
using System.Data;
using Npgsql;
using PowerMessenger.Application.Layers.Persistence.Context;

namespace PowerMessenger.Infrastructure.Persistence.Context;

public class MessengerDapperContext : IMessengerDapperContext
{
    private readonly string _connectionString;

    public MessengerDapperContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateNpgConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}
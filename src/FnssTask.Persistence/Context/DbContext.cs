using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace FnssTask.Persistence.Context;

public class DbContext
{
    private readonly string _connectionString;

    public DbContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ConnectionString");
    }
}

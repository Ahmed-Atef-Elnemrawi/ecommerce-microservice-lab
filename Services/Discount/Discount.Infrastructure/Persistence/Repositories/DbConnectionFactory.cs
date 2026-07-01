using System.Data;
using Discount.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Discount.Infrastructure.Persistence.Repositories;

public sealed class DbConnectionFactory(IOptions<DbSettings> options)
{
   private readonly DbSettings _dbSettings =  options.Value;

   public async Task<IDbConnection> CreateConnectionAsync()
   {
     var connection = new NpgsqlConnection(_dbSettings.ConnectionString);
     await connection.OpenAsync();
     return connection;
   }
}
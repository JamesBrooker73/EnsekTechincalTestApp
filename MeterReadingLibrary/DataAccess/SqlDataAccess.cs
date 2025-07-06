using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MeterReadingLibrary.DataAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config)); 
    }

    public async Task<int> UploadMeterRead<T, U>(string query, U parameters, string connectionStringName)
    {
        string connectionString = _config.GetConnectionString(connectionStringName)!;

        using IDbConnection connection = new SqlConnection(connectionString);

        var rowsAffected = await connection.ExecuteAsync(query, parameters);

        return rowsAffected;
    }
}

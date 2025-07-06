using CsvHelper;
using CsvHelper.Configuration;
using Dapper;
using MeterReadingLibrary.DataMap;
using MeterReadingLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;

namespace MeterReadingLibrary.Seeding;

public class CustomerAccountSeeder
{
    private readonly IConfiguration _config;

    public CustomerAccountSeeder(IConfiguration config)
    {
        _config = config;
    }

    public void SeedCustomerDatabase()
    {
        using var reader = new StreamReader($"C:/Users/james/source/repos/EnsekTechincalTestApp/MeterReadingLibrary/Seeding/Data/Test_Accounts.csv");
        var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
        using var csv = new CsvReader(reader, csvConfiguration);

        csv.Context.RegisterClassMap<CustomerDataMap>();
        var records = csv.GetRecords<CustomerModel>().ToList();


        string connectionString = "";

        using IDbConnection connection = new SqlConnection(connectionString);

        foreach (var record in records)
        {
            connection.Execute("spCustomers_SeedDatabase", new { record.Id, record.FirstName, record.LastName },
            commandType: CommandType.StoredProcedure);
        }

    }
}

using CsvHelper.Configuration;
using CsvHelper;
using MeterReadingLibrary.DataMap;
using MeterReadingLibrary.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using Dapper;

namespace EnsekTechincalTest.Extensions
{
    public static class SeedDb
    {
        public static void SeedCustomerDatabase(this WebApplicationBuilder builder)
        {
            using var reader = new StreamReader($"C:/Users/james/source/repos/EnsekTechincalTestApp/MeterReadingLibrary/Seeding/Data/Test_Accounts.csv");
            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
            using var csv = new CsvReader(reader, csvConfiguration);

            csv.Context.RegisterClassMap<CustomerDataMap>();
            var records = csv.GetRecords<CustomerModel>().ToList();


            string connectionString = builder.Configuration.GetConnectionString("Default");

            using IDbConnection connection = new SqlConnection(connectionString);

            foreach (var record in records)
            {
                connection.Execute("spCustomers_SeedDatabase", new { record.Id, record.FirstName, record.LastName },
                commandType: CommandType.StoredProcedure);
            }

        }
    }
}

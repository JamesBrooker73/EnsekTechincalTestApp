using CsvHelper;
using CsvHelper.Configuration;
using Dapper;
using MeterReadingLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;

namespace MeterReadingLibrary.Seeding
{
    public class CustomerAccountSeeder
    {
        private readonly IConfiguration _config;

        public CustomerAccountSeeder(IConfiguration config)
        {
            _config = config;
        }

        public void SeedCustomerDatabase()
        {
            using var reader = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}/Seeding/Data/Test_Accounts.csv");
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

            var records = csv.GetRecords<CustomerModel>().ToList();

            string connectionString = _config.GetConnectionString(default);

            using IDbConnection connection = new SqlConnection(connectionString);

            foreach (var record in records)
            {
                connection.Execute("spCustomers_SeedDatabase", new { record.Id, record.FirstName, record.LastName },
                commandType: CommandType.StoredProcedure);
            }
            

   

        }
    }
}

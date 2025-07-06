using CsvHelper.Configuration;
using CsvHelper;
using EnsekTechincalTest.Services;
using MeterReadingLibrary.DataAccess;
using MeterReadingLibrary.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using Dapper;
using MeterReadingLibrary.DataMap;
namespace EnsekTechincalTest.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddCustomServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
        builder.Services.AddSingleton<IMeterReadingData, MeterReadingData>();
        builder.Services.AddScoped<IMeterReadUploadService, MeterReadUploadService>();
        builder.Services.AddScoped<IMeterReadCsvParserServcice, MeterReadCsvParserServcice>();
    }

    public static void AddHealthCheckServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddSqlServer(builder.Configuration.GetConnectionString("Default"));
    }

    
}

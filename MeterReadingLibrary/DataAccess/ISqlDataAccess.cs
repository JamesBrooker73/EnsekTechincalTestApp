
namespace MeterReadingLibrary.DataAccess;

public interface ISqlDataAccess
{
    Task<int> UploadMeterRead<T, U>(string query, U parameters, string connectionStringName);
}
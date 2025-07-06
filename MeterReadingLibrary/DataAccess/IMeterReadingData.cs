using EnsekTechincalTest.Models;

namespace MeterReadingLibrary.DataAccess
{
    public interface IMeterReadingData
    {
        Task<MeterReadingUploadResult> UploadMeterRead(IEnumerable<MeterReadingModel> meterReadings);
    }
}
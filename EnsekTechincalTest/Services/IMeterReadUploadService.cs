using EnsekTechincalTest.Models;

namespace EnsekTechincalTest.Services
{
    public interface IMeterReadUploadService
    {
        Task<MeterReadUploadResultModel> ParseAndUploadMeterRead(Stream stream);
        Task<MeterReadUploadResultModel> UploadMeterReads(Stream stream);
    }
}
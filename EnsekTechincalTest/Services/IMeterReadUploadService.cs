using EnsekTechincalTest.Models;

namespace EnsekTechincalTest.Services;

public interface IMeterReadUploadService
{
    Task<MeterReadUploadResultModel> UploadMeterReads(Stream stream);
}
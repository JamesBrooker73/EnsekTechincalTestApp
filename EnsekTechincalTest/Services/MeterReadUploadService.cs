using EnsekTechincalTest.Models;
using EnsekTechincalTest.Results;
using MeterReadingLibrary.DataAccess;
using System.IO;

namespace EnsekTechincalTest.Services;

public class MeterReadUploadService : IMeterReadUploadService
{
    private readonly IMeterReadCsvParserServcice _meterReadCsvParserServcice;
    private readonly IMeterReadingData _meterReadingData;

    public MeterReadUploadService(IMeterReadCsvParserServcice meterReadCsvParserServcice, IMeterReadingData meterReadingData)
    {
        _meterReadCsvParserServcice = meterReadCsvParserServcice;
        _meterReadingData = meterReadingData;

    }

    public async Task<MeterReadUploadResultModel> UploadMeterReads(Stream stream)
    {
        if (stream is null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        try
        {
            var result = await ParseAndUploadMeterRead(stream);
            return result;
        }
        
        catch (Exception ex)
        {
            throw new Exception("An error occured uploading meter reads.", ex);
        }
    }


    public async Task<MeterReadUploadResultModel> ParseAndUploadMeterRead(Stream stream)
    {
        var parsedResult = _meterReadCsvParserServcice.ParseCsv(stream);

        var updateResult = await _meterReadingData.UploadMeterRead(parsedResult.ParsedMeterReadings);

        return new MeterReadUploadResultModel
        {
            failedParseCount = parsedResult.FailedParseCount,
            successfulUploadCount = updateResult.successfulUploadCount,
            failedUploadoutCount = updateResult.RejectedUploadCount
        };
    }
}
